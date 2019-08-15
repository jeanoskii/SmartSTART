using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;

namespace CAPRES.BusinessLogicObjects
{
    public class LicenseBLO
    {
        private LicenseDAO licenseDAO = new LicenseDAO();

        public DataTable SelectAllLicense()
        {
            return licenseDAO.SelectAllLicense();
        }

        public string ManageLicensesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable licensesInTextFile = new DataTable();
                licensesInTextFile.Columns.Add("license_code");
                licensesInTextFile.Columns.Add("license");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = licensesInTextFile.NewRow();
                    dr.ItemArray = fields;
                    licensesInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable license = licenseDAO.SelectLicense(fields[0]);
                        if (license.Rows.Count == 0)
                        {
                            licenseDAO.InsertLicense(fields[0], fields[1]);
                        }
                        else
                        {
                            if (license.Rows[0][1].ToString() != fields[1])
                            {
                                licenseDAO.UpdateLicense(fields[0], fields[1]);
                            }
                        }
                        license.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [License Code]|[License Value]";
                    }
                }
                tfp.Close();
                DataTable licensesInDatabase = licenseDAO.SelectAllLicense();
                IEnumerable<string> licensesNotInTextFile = licensesInDatabase.AsEnumerable().Select(r => r.Field<string>("license_code"))
                    .Except(licensesInTextFile.AsEnumerable().Select(r => r.Field<string>("license_code")));
                if (licensesNotInTextFile.Any())
                {
                    DataTable licensesToBeDeleted = (from row in licensesInDatabase.AsEnumerable()
                                                     join id in licensesNotInTextFile
                                                     on row.Field<string>("license_code") equals id
                                                     select row).CopyToDataTable();
                    foreach (DataRow dr in licensesToBeDeleted.Rows)
                    {
                        licenseDAO.DeleteLicense(dr[0].ToString());
                    }
                }
                return "License data successfully edited.";
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