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
    public class GenderBLO
    {
        private GenderVO genderVO = new GenderVO();
        private GenderDAO genderDAO = new GenderDAO();

        public DataTable SelectAllGender()
        {
            return genderDAO.SelectAllGender();
        }

        public string ManageGendersWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable gendersInTextFile = new DataTable();
                gendersInTextFile.Columns.Add("gender_code");
                gendersInTextFile.Columns.Add("gender");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = gendersInTextFile.NewRow();
                    dr.ItemArray = fields;
                    gendersInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable gender = genderDAO.SelectGender(fields[0]);
                        if (gender.Rows.Count == 0)
                        {
                            genderDAO.InsertGender(fields[0], fields[1]);
                        }
                        else
                        {
                            if (gender.Rows[0][1].ToString() != fields[1])
                            {
                                genderDAO.UpdateGender(fields[0], fields[1]);
                            }
                        }
                        gender.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Gender Code]|[Gender Value]";
                    }
                }
                tfp.Close();
                DataTable gendersInDatabase = genderDAO.SelectAllGender();
                IEnumerable<string> gendersNotInTextFile = gendersInDatabase.AsEnumerable().Select(r => r.Field<string>("gender_code"))
                    .Except(gendersInTextFile.AsEnumerable().Select(r => r.Field<string>("gender_code")));
                if (gendersNotInTextFile.Any())
                {
                    DataTable gendersToBeDeleted = (from row in gendersInDatabase.AsEnumerable()
                                                    join id in gendersNotInTextFile
                                                    on row.Field<string>("gender_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in gendersToBeDeleted.Rows)
                    {
                        genderDAO.DeleteGender(dr[0].ToString());
                    }
                }
                return "Gender data successfully edited.";
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