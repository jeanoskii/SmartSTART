using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class EstablishmentCertificateDAO
    {
        private DB db = new DB();

        public DataTable SelectEstablishmentCertificate(string educationEstablishmentCode,
            string certificateCode)
        {
            return db.GetData("SELECT education_establishment_code, certificate_code FROM "
                + "Establishment_Certificates WHERE education_establishment_code = '"
                + educationEstablishmentCode + "' AND certificate_code = '"
                + certificateCode + "'");
        }

        public DataTable SelectAllEstablishmentCertificate()
        {
            return db.GetData("SELECT education_establishment_code, certificate_code FROM "
                + "Establishment_Certificates");
        }

        public void InsertEstablishmentCertificate(string educationEstablishmentCode,
            string certificateCode)
        {
            db.Execute("InsertEstablishmentCertificate", "INSERT INTO Establishment_Certificates "
                + "(education_establishment_code, certificate_code) VALUES ('"
                + educationEstablishmentCode + "','" + certificateCode + "')");
        }

        public void DeleteEstablishmentCertificate(string educationEstablishmentCode,
            string certificateCode)
        {
            db.Execute("DeleteEstablishmentCertificate", "DELETE FROM Establishment_Certificates "
                + "WHERE education_establishment_code = '" + educationEstablishmentCode
                + "' AND certificate_code = '" + certificateCode + "'");
        }
    }
}