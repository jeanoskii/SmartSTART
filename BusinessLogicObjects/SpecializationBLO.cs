using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.ValueObjects;
using CAPRES.DataAccessObjects;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;

namespace CAPRES.BusinessLogicObjects
{
    public class SpecializationBLO
    {
        private SpecializationVO specializationVO = new SpecializationVO();
        private SpecializationDAO specializationDAO = new SpecializationDAO();

        public DataTable SelectAllSpecialization()
        {
            return specializationDAO.SelectAllSpecialization();
        }

        public string ManageSpecializationsWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable specializationsInTextFile = new DataTable();
                specializationsInTextFile.Columns.Add("specialization_code");
                specializationsInTextFile.Columns.Add("specialization");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = specializationsInTextFile.NewRow();
                    dr.ItemArray = fields;
                    specializationsInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable specialization = specializationDAO.SelectSpecialization(fields[0]);
                        if (specialization.Rows.Count == 0)
                        {
                            specializationDAO.InsertSpecialization(fields[0], fields[1]);
                        }
                        else
                        {
                            if (specialization.Rows[0][1].ToString() != fields[1])
                            {
                                specializationDAO.UpdateSpecialization(fields[0], fields[1]);
                            }
                        }
                        specialization.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Specialization Code]|[Specialization Value]";
                    }
                }
                tfp.Close();
                DataTable specializationsInDatabase = specializationDAO.SelectAllSpecialization();
                IEnumerable<string> specializationsNotInTextFile = specializationsInDatabase.AsEnumerable().Select(r => r.Field<string>("specialization_code"))
                    .Except(specializationsInTextFile.AsEnumerable().Select(r => r.Field<string>("specialization_code")));
                if (specializationsNotInTextFile.Any())
                {
                    DataTable specializationsToBeDeleted = (from row in specializationsInDatabase.AsEnumerable()
                                                    join id in specializationsNotInTextFile
                                                    on row.Field<string>("specialization_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in specializationsToBeDeleted.Rows)
                    {
                        specializationDAO.DeleteSpecialization(dr[0].ToString());
                    }
                }
                return "Specialization data successfully edited.";
            }
            catch (Exception e1)
            {
                Debug.WriteLine("Text File Parse Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                return "Sorry, an error occured. Please try again.";
            }
        }
    }
}