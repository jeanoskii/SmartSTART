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
    public class BloodTypeBLO
    {
        private BloodTypeVO bloodTypeVO = new BloodTypeVO();
        private BloodTypeDAO bloodTypeDAO = new BloodTypeDAO();

        public DataTable SelectAllBloodType()
        {
            return bloodTypeDAO.SelectAllBloodType();
        }

        public string ManageBloodTypesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable bloodTypesInTextFile = new DataTable();
                bloodTypesInTextFile.Columns.Add("blood_type");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = bloodTypesInTextFile.NewRow();
                    dr.ItemArray = fields;
                    bloodTypesInTextFile.Rows.Add(dr);
                    if (fields.Count() == 1)
                    {
                        DataTable bloodType = bloodTypeDAO.SelectBloodType(fields[0]);
                        if (bloodType.Rows.Count == 0)
                        {
                            bloodTypeDAO.InsertBloodType(fields[0]);
                        }
                        bloodType.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Blood Type]";
                    }
                }
                tfp.Close();
                DataTable bloodTypesInDatabase = bloodTypeDAO.SelectAllBloodType();
                IEnumerable<string> bloodTypesNotInTextFile = bloodTypesInDatabase.AsEnumerable().Select(r => r.Field<string>("blood_type"))
                    .Except(bloodTypesInTextFile.AsEnumerable().Select(r => r.Field<string>("blood_type")));
                if (bloodTypesNotInTextFile.Any())
                {
                    DataTable bloodTypesToBeDeleted = (from row in bloodTypesInDatabase.AsEnumerable()
                                                    join id in bloodTypesNotInTextFile
                                                    on row.Field<string>("blood_type") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in bloodTypesToBeDeleted.Rows)
                    {
                        bloodTypeDAO.DeleteBloodType(dr[0].ToString());
                    }
                }
                return "Blood Type data successfully edited.";
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