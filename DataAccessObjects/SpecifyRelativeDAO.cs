using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class SpecifyRelativeDAO
    {
        private DB db = new DB();

        public DataTable SelectSpecifyRelativeOfAnsewr(int answerId)
        {
            return db.GetData("SELECT specify_relative_id, company_name, relative_name " +
                "FROM Specify_Relatives WHERE answer_id = '" + answerId + "'");
        }

        public void InsertSpecifyRelative(int answerId, string companyName,
            string relativeName)
        {
            db.Execute("InsertSpecifyRelative", "INSERT INTO Specify_Relatives (answer_id, " +
                "company_name, relative_name) VALUES ('" + answerId + "','" + companyName +
                "','" + relativeName + "')");
        }

        public void UpdateSpecifyRelative(int specifyRelativeId, string companyName,
            string relativeName)
        {
            db.Execute("UpdateSpecifyRelative", "UPDATE Specify_Relatives SET company_name = '" +
                companyName + "', relative_name = '" + relativeName + "' WHERE specify_relative_id = '" +
                specifyRelativeId + "'");
        }
    }
}