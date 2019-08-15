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
    public class CertificateBLO
    {
        private CertificateVO certificateVO = new CertificateVO();
        private CertificateDAO certificateDAO = new CertificateDAO();

        public DataTable SelectCertificateOfEstablishment(string educationEstablishmentCode)
        {
            return certificateDAO.SelectCertificateOfEstablishment(educationEstablishmentCode);
        }

        public string ManageCertificatesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable certificatesInTextFile = new DataTable();
                certificatesInTextFile.Columns.Add("certificate_code");
                certificatesInTextFile.Columns.Add("certificate");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = certificatesInTextFile.NewRow();
                    dr.ItemArray = fields;
                    certificatesInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable certificate = certificateDAO.SelectCertificate(fields[0]);
                        if (certificate.Rows.Count == 0)
                        {
                            certificateDAO.InsertCertificate(fields[0], fields[1]);
                        }
                        else
                        {
                            if (certificate.Rows[0][1].ToString() != fields[1])
                            {
                                certificateDAO.UpdateCertificate(fields[0], fields[1]);
                            }
                        }
                        certificate.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Certificate Code]|[Certificate Value]";
                    }
                }
                tfp.Close();
                DataTable certificatesInDatabase = certificateDAO.SelectAllCertificate();
                IEnumerable<string> certificatesNotInTextFile = certificatesInDatabase.AsEnumerable().Select(r => r.Field<string>("certificate_code"))
                    .Except(certificatesInTextFile.AsEnumerable().Select(r => r.Field<string>("certificate_code")));
                if (certificatesNotInTextFile.Any())
                {
                    DataTable certificatesToBeDeleted = (from row in certificatesInDatabase.AsEnumerable()
                                                    join id in certificatesNotInTextFile
                                                    on row.Field<string>("certificate_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in certificatesToBeDeleted.Rows)
                    {
                        certificateDAO.DeleteCertificate(dr[0].ToString());
                    }
                }
                return "Certificate data successfully edited.";
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