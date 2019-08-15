using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class CertificateDAO
    {
        private DB db = new DB();

        public DataTable SelectCertificateOfEstablishment(string educationEstablishmentCode)
        {
            return db.GetData("SELECT Certificates.certificate_code, Certificates.certificate "
                + "FROM Certificates, Establishment_Certificates, Education_Establishments "
                + "WHERE Education_Establishments.education_establishment_code = '"
                + educationEstablishmentCode + "' AND Certificates.certificate_code "
                + "= Establishment_Certificates.certificate_code AND "
                + "Establishment_Certificates.education_establishment_code "
                + "= Education_Establishments.education_establishment_code "
                + "ORDER BY Certificates.certificate_code");
        }

        public DataTable SelectAllCertificate()
        {
            return db.GetData("SELECT certificate_code, certificate FROM Certificates");
        }

        public DataTable SelectCertificate(string certificateCode)
        {
            return db.GetData("SELECT certificate_code, certificate FROM Certificates WHERE "
                + "certificate_code = '" + certificateCode + "'");
        }

        public void InsertCertificate(string certificateCode, string certificate)
        {
            db.Execute("InsertCertificate", "INSERT INTO Certificates (certificate_code, certificate) "
                + "VALUES ('" + certificateCode + "','" + certificate + "')");
        }

        public void UpdateCertificate(string certificateCode, string certificate)
        {
            db.Execute("UpdateCertificate", "UPDATE Certificates SET certificate = '" + certificate
                + "' WHERE certificate_code = '" + certificateCode + "'");
        }

        public void DeleteCertificate(string certificateCode)
        {
            db.Execute("DeleteCertificate", "DELETE FROM Certificates WHERE certificate_code = '"
                + certificateCode + "'");
        }
    }
}