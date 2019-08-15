using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class QuestionBLO
    {
        private QuestionDAO questionDAO = new QuestionDAO();

        public DataTable SelectAllQuestion()
        {
            return questionDAO.SelectAllQuestion();
        }
    }
}