using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CAPRES.DataAccessObjects
{
    public class NationalityDAO
    {
        private DB db = new DB();
        public DataTable SelectAllNationality()
        {
            return db.GetData("SELECT nationality_code, nationality FROM Nationalities "
                + "ORDER BY nationality");
        }

        public DataTable SelectNationality(string nationalityCode)
        {
            return db.GetData("SELECT nationality_code, nationality FROM Nationalities "
                + "WHERE nationality_code = '" + nationalityCode + "'");
        }

        public void InsertNationality(string nationalityCode, string nationality)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@nationality_code", nationalityCode);
            parameters[1] = new SqlParameter("@nationality", nationality);
            db.Execute("InsertNationality", "INSERT INTO Nationalities (nationality_code, "
                + "nationality) VALUES (@nationality_code, @nationality)", parameters);
        }

        public void UpdateNationality(string nationalityCode, string nationality)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@nationality_code", nationalityCode);
            parameters[1] = new SqlParameter("@nationality", nationality);
            db.Execute("UpdateNationality", "UPDATE Nationalities SET nationality = @nationality "
                + "WHERE nationality_code = @nationality_code", parameters);
        }

        public void DeleteNationality(string nationalityCode)
        {
            db.Execute("DeleteNationality", "DELETE FROM Nationalities WHERE nationality_code = '"
                + nationalityCode + "'");
        }
    }
}