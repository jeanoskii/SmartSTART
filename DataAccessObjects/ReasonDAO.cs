using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class ReasonDAO
    {
        private DB db = new DB();

        public DataTable SelectAllReason()
        {
            return db.GetData("SELECT reason_code, reason FROM Reasons");
        }

        public DataTable SelectReason(string reasonCode)
        {
            return db.GetData("SELECT reason_code, reason FROM Reasons WHERE reason_code = '"
                + reasonCode + "'");
        }

        public void InsertReason(string reasonCode, string reason)
        {
            db.Execute("InsertReason", "INSERT INTO Reasons (reason_code, reason) VALUES ('"
                + reasonCode + "','" + reason + "')");
        }

        public void UpdateReason(string reasonCode, string reason)
        {
            db.Execute("UpdateReason", "UPDATE Reasons SET reason = '" + reason + "' WHERE "
                + "reason_code = '" + reasonCode + "'");
        }

        public void DeleteReason(string reasonCode)
        {
            db.Execute("DeleteReason", "DELETE FROM Reasons WHERE reason_code = '"
                + reasonCode + "'");
        }
    }
}