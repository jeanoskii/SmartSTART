using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class CivilStatusDAO
    {
        private DB db = new DB();

        public DataTable SelectAllCivilStatus()
        {
            return db.GetData("SELECT civil_status_code, civil_status FROM Civil_Statuses");
        }

        public DataTable SelectCivilStatus(string civilStatusCode)
        {
            return db.GetData("SELECT civil_status_code, civil_status FROM Civil_Statuses "
                + "WHERE civil_status_code = '" + civilStatusCode + "'");
        }

        public void InsertCivilStatus(string civilStatusCode, string civilStatus)
        {
            db.Execute("InsertCivilStatus", "INSERT INTO Civil_Statuses (civil_status_code, "
                + "civil_status) VALUES ('" + civilStatusCode + "','" + civilStatus + "')");
        }

        public void UpdateCivilStatus(string civilStatusCode, string civilStatus)
        {
            db.Execute("UpdateCivilStatus", "UPDATE Civil_Statuses SET civil_status = '"
                + civilStatus + "' WHERE civil_status_code = '" + civilStatusCode + "'");
        }

        public void DeleteCivilStatus(string civilStatusCode)
        {
            db.Execute("DeleteCivilStatus", "DELETE FROM Civil_Statuses WHERE civil_status_code = '"
                + civilStatusCode + "'");
        }
    }
}