using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class QuestionDAO
    {
        private DB db = new DB();

        public DataTable SelectAllQuestion()
        {
            return db.GetData("SELECT question_code, question from Questions");
        }
    }
}