using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using CAPRES.DataAccessObjects;
using System.Diagnostics;

namespace CAPRES.BusinessLogicObjects
{
    public class EstablishmentCertificateBLO
    {
        private EstablishmentCertificateDAO establishmentCertificateDAO
            = new EstablishmentCertificateDAO();

        public string ManageEstablishmentCertificatesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable establishmentCertificatesInTextFile = new DataTable();
                establishmentCertificatesInTextFile.Columns.Add("education_establishment_code");
                establishmentCertificatesInTextFile.Columns.Add("certificate_code");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    if (fields.Count() == 2)
                    {

                        DataRow dr = establishmentCertificatesInTextFile.NewRow();
                        dr.ItemArray = fields;
                        establishmentCertificatesInTextFile.Rows.Add(dr);
                        DataTable establishmentCertificate
                            = establishmentCertificateDAO.SelectEstablishmentCertificate(fields[0], fields[1]);
                        if (establishmentCertificate.Rows.Count == 0)
                        {
                            establishmentCertificateDAO.InsertEstablishmentCertificate(fields[0], fields[1]);
                        }
                        establishmentCertificate.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Education Establishment Code]|[Certificate Code]";
                    }
                }
                tfp.Close();
                DataTable establishmentCertificatesInDatabase
                    = establishmentCertificateDAO.SelectAllEstablishmentCertificate();
                IEnumerable<DataRow> dt3 = (from r in establishmentCertificatesInDatabase.AsEnumerable()
                                 where !establishmentCertificatesInTextFile.AsEnumerable().Any(r2 => r["education_establishment_code"].ToString().Trim().ToLower() == r2["education_establishment_code"].ToString().Trim().ToLower()
                                     && r["certificate_code"].ToString().Trim().ToLower() == r2["certificate_code"].ToString().Trim())
                                 select r);
                if (dt3.Any())
                {
                    DataTable establishmentCertificatesToBeDeleted = dt3.CopyToDataTable();
                    foreach (DataRow dr in establishmentCertificatesToBeDeleted.Rows)
                    {
                        establishmentCertificateDAO.DeleteEstablishmentCertificate(dr[0].ToString(), dr[1].ToString());
                    }
                }
                return "Establishment Certificate data successfully edited.";
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