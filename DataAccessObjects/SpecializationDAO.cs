using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class SpecializationDAO
    {
        private DB db = new DB();

        public DataTable SelectAllSpecialization()
        {
            return db.GetData("SELECT specialization_code, specialization FROM Specializations");
        }

        public DataTable SelectSpecialization(string specializationCode)
        {
            return db.GetData("SELECT specialization_code, specialization FROM Specializations "
                + "WHERE specialization_code = '" + specializationCode + "'");
        }

        public void InsertSpecialization(string specializationCode, string specialization)
        {
            db.Execute("InsertSpecialization", "INSERT INTO Specializations (specialization_code, "
                + "specialization) VALUES ('" + specializationCode + "','" + specialization + "')");
        }

        public void UpdateSpecialization(string specializationCode, string specialization)
        {
            db.Execute("UpdateSpecialization", "UPDATE Specializations SET specialization = '"
                + specialization + "' WHERE specialization_code = '" + specializationCode + "'");
        }

        public void DeleteSpecialization(string specializationCode)
        {
            db.Execute("DeleteSpecialization", "DELETE FROM Specializations WHERE "
                + "specialization_code = '" + specializationCode + "'");
        }
    }
}