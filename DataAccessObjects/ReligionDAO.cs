using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CAPRES.DataAccessObjects
{
    public class ReligionDAO
    {
        private DB db = new DB();

        public DataTable SelectAllReligion()
        {
            return db.GetData("SELECT religion_code, religion FROM Religions");
        }

        public DataTable SelectReligion(string religionCode)
        {
            return db.GetData("SELECT religion_code, religion FROM Religions WHERE "
                + "religion_code = '" + religionCode + "'");
        }

        public void InsertReligion(string religionCode, string religion)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@religion_code", religionCode);
            parameters[1] = new SqlParameter("@religion", religion);
            db.Execute("InsertReligion", "INSERT INTO Religions (religion_code, religion) "
                + "VALUES (@religion_code, @religion)", parameters);
        }

        public void UpdateReligion(string religionCode, string religion)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@religion_code", religionCode);
            parameters[1] = new SqlParameter("@religion", religion);
            db.Execute("UpdateReligion", "UPDATE Religions SET religion = @religion WHERE "
                + "religion_code = @religion_code", parameters);
        }

        public void DeleteReligion(string religionCode)
        {
            db.Execute("DeleteReligion", "DELETE FROM Religions WHERE religion_code = '"
                + religionCode + "'");
        }
    }
}