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
    public class NationalityBLO
    {
        private NationalityDAO nationalityDAO = new NationalityDAO();
        private NationalityVO nationalityVO = new NationalityVO();

        public DataTable SelectAllNationality()
        {
            return nationalityDAO.SelectAllNationality();
        }

        public string ManageNationalitiesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable nationalitiesInTextFile = new DataTable();
                nationalitiesInTextFile.Columns.Add("nationality_code");
                nationalitiesInTextFile.Columns.Add("nationality");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = nationalitiesInTextFile.NewRow();
                    dr.ItemArray = fields;
                    nationalitiesInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable nationality = nationalityDAO.SelectNationality(fields[0]);
                        if (nationality.Rows.Count == 0)
                        {
                            nationalityDAO.InsertNationality(fields[0], fields[1]);
                        }
                        else
                        {
                            if (nationality.Rows[0][1].ToString() != fields[1])
                            {
                                nationalityDAO.UpdateNationality(fields[0], fields[1]);
                            }
                        }
                        nationality.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Nationality Code]|[Nationality Value]";
                    }
                }
                tfp.Close();
                DataTable nationalitiesInDatabase = nationalityDAO.SelectAllNationality();
                IEnumerable<string> nationalitiesNotInTextFile = nationalitiesInDatabase.AsEnumerable().Select(r => r.Field<string>("nationality_code"))
                    .Except(nationalitiesInTextFile.AsEnumerable().Select(r => r.Field<string>("nationality_code")));
                if (nationalitiesNotInTextFile.Any())
                {
                    DataTable nationalitiesToBeDeleted = (from row in nationalitiesInDatabase.AsEnumerable()
                                                    join id in nationalitiesNotInTextFile
                                                    on row.Field<string>("nationality_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in nationalitiesToBeDeleted.Rows)
                    {
                        nationalityDAO.DeleteNationality(dr[0].ToString());
                    }
                }
                return "Nationality data successfully edited.";
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