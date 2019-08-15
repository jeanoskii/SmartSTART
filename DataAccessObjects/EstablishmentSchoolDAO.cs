using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class EstablishmentSchoolDAO
    {
        private DB db = new DB();

        public DataTable SelectEstablishmentSchool(string educationEstablishmentCode,
            string schoolCode)
        {
            return db.GetData("SELECT education_establishment_code, school_code FROM "
                + "Establishment_Schools WHERE education_establishment_code = '"
                + educationEstablishmentCode + "' AND school_code = '"
                + schoolCode + "'");
        }

        public DataTable SelectAllEstablishmentSchool()
        {
            return db.GetData("SELECT education_establishment_code, school_code FROM "
                + "Establishment_Schools");
        }

        public void InsertEstablishmentSchool(string educationEstablishmentCode,
            string schoolCode)
        {
            db.Execute("InsertEstablishmentSchool", "INSERT INTO Establishment_Schools "
                + "(education_establishment_code, school_code) VALUES ('"
                + educationEstablishmentCode + "','" + schoolCode + "')");
        }

        public void DeleteEstablishmentSchool(string educationEstablishmentCode,
            string schoolCode)
        {
            db.Execute("DeleteEstablishmentSchool", "DELETE FROM Establishment_Schools "
                + "WHERE education_establishment_code = '" + educationEstablishmentCode
                + "' AND school_code = '" + schoolCode + "'");
        }
    }
}