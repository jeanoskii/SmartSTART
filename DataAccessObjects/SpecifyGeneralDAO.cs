using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class SpecifyGeneralDAO
    {
        private DB db = new DB();

        public DataTable SelectSpecifyGeneralOfAnswer(int answerId)
        {
            return db.GetData("SELECT specify_general_id, value FROM Specify_General WHERE " +
                "answer_id = '" + answerId + "'");
        }

        public void InsertSpecifyGeneral(int answerId, string value)
        {
            db.Execute("InsertSpecifyGeneral", "INSERT INTO Specify_General (answer_id, value) " +
                "VALUES ('" + answerId + "','" + value + "')");
        }

        public void UpdateSpecifyGeneral(int specifyGeneralId, string value)
        {
            db.Execute("UpdateSpecifyGeneral", "UPDATE Specify_General SET value = '" + value +
                "' WHERE specify_general_id = '" + specifyGeneralId + "'");
        }
    }
}