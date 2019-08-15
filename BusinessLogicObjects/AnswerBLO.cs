using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class AnswerBLO
    {
        private AnswerDAO answerDAO = new AnswerDAO();
        public DataTable SelectAnswerOfAFE(int afeId)
        {
            return answerDAO.SelectAnswerOfAFE(afeId);
        }

        public DataTable SelectAnswerWithQuestionOfAFE(int afeId)
        {
            return answerDAO.SelectAnswerWithQuestionOfAFE(afeId);
        }

        public DataTable InsertAnswer(int afeId, string questionCode, string answer)
        {
            return answerDAO.InsertAnswer(afeId, questionCode, answer);
        }

        public void UpdateAnswer(int answerId, string answer)
        {
            answerDAO.UpdateAnswer(answerId, answer);
        }
    }
}