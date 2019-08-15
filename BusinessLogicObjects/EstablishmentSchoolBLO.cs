using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Diagnostics;

namespace CAPRES.BusinessLogicObjects
{
    public class EstablishmentSchoolBLO
    {
        private EstablishmentSchoolDAO establishmentSchoolDAO
            = new EstablishmentSchoolDAO();

        public string ManageEstablishmentSchoolsWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable establishmentSchoolsInTextFile = new DataTable();
                establishmentSchoolsInTextFile.Columns.Add("education_establishment_code");
                establishmentSchoolsInTextFile.Columns.Add("school_code");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    if (fields.Count() == 2)
                    {
                        DataRow dr = establishmentSchoolsInTextFile.NewRow();
                        dr.ItemArray = fields;
                        establishmentSchoolsInTextFile.Rows.Add(dr);
                        DataTable establishmentSchool
                            = establishmentSchoolDAO.SelectEstablishmentSchool(fields[0], fields[1]);
                        if (establishmentSchool.Rows.Count == 0)
                        {
                            establishmentSchoolDAO.InsertEstablishmentSchool(fields[0], fields[1]);
                        }
                        establishmentSchool.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Education Establishment Code]|[School Code]";
                    }
                }
                tfp.Close();
                DataTable establishmentSchoolsInDatabase
                    = establishmentSchoolDAO.SelectAllEstablishmentSchool();
                IEnumerable<DataRow> dt3 = (from r in establishmentSchoolsInDatabase.AsEnumerable()
                                            where !establishmentSchoolsInTextFile.AsEnumerable().Any(r2 => r["education_establishment_code"].ToString().Trim().ToLower() == r2["education_establishment_code"].ToString().Trim().ToLower()
                                                && r["school_code"].ToString().Trim().ToLower() == r2["school_code"].ToString().Trim())
                                            select r);
                if (dt3.Any())
                {
                    DataTable establishmentCertificatesToBeDeleted = dt3.CopyToDataTable();
                    foreach (DataRow dr in establishmentCertificatesToBeDeleted.Rows)
                    {
                        establishmentSchoolDAO.DeleteEstablishmentSchool(dr[0].ToString(), dr[1].ToString());
                    }
                }
                return "Establishment School data successfully edited.";
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