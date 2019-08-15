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
    public class SchoolBLO
    {
        private SchoolVO schoolVO = new SchoolVO();
        private SchoolDAO schoolDAO = new SchoolDAO();

        public DataTable SelectAllSchool()
        {
            return schoolDAO.SelectAllSchool();
        }

        public DataTable SelectSchoolOfEstablishment(string educationEstablishmentCode)
        {
            return schoolDAO.SelectSchoolOfEstablishment(educationEstablishmentCode);
        }

        public string ManageSchoolsWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable schoolsInTextFile = new DataTable();
                schoolsInTextFile.Columns.Add("school_code");
                schoolsInTextFile.Columns.Add("school");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    if (fields.Count() == 2)
                    {
                        DataRow dr = schoolsInTextFile.NewRow();
                        dr.ItemArray = fields;
                        schoolsInTextFile.Rows.Add(dr);
                        DataTable school = schoolDAO.SelectSchool(fields[0]);
                        if (school.Rows.Count == 0)
                        {
                            schoolDAO.InsertSchool(fields[0], fields[1]);
                        }
                        else
                        {
                            if (school.Rows[0][1].ToString() != fields[1])
                            {
                                schoolDAO.UpdateSchool(fields[0], fields[1]);
                            }
                        }
                        school.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [School Code]|[School Value]";
                    }
                }
                tfp.Close();
                DataTable schoolsInDatabase = schoolDAO.SelectAllSchool();
                IEnumerable<string> schoolsNotInTextFile = schoolsInDatabase.AsEnumerable().Select(r => r.Field<string>("school_code"))
                    .Except(schoolsInTextFile.AsEnumerable().Select(r => r.Field<string>("school_code")));
                if (schoolsNotInTextFile.Any())
                {
                    DataTable schoolsToBeDeleted = (from row in schoolsInDatabase.AsEnumerable()
                                                    join id in schoolsNotInTextFile
                                                    on row.Field<string>("school_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in schoolsToBeDeleted.Rows)
                    {
                        schoolDAO.DeleteSchool(dr[0].ToString());
                    }
                }
                return "School data successfully edited.";
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