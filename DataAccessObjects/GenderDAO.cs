using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class GenderDAO
    {
        private DB db = new DB();

        public DataTable SelectAllGender()
        {
            return db.GetData("SELECT gender_code, gender FROM Genders");
        }

        public DataTable SelectGender(string genderCode)
        {
            return db.GetData("SELECT gender_code, gender FROM Genders "
                + "WHERE gender_code = '" + genderCode + "'");
        }

        public void InsertGender(string genderCode, string gender)
        {
            db.Execute("InsertGender", "INSERT INTO Genders (gender_code, gender)"
                + " VALUES ('" + genderCode + "','" + gender + "')");
        }

        public void UpdateGender(string genderCode, string gender)
        {
            db.Execute("UpdateGender", "UPDATE Genders SET gender = '" + gender
                + "' WHERE gender_code = '" + genderCode + "'");
        }

        public void DeleteGender(string genderCode)
        {
            db.Execute("DeleteGender", "DELETE FROM Genders WHERE gender_code = '"
                + genderCode + "'");
        }
    }
}