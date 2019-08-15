using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class AnswerDAO
    {
        private DB db = new DB();

        public DataTable SelectAnswerOfAFE(int afeId)
        {
            return db.GetData("SELECT answer_id, question_code, answer FROM " +
                "Answers WHERE afe_id = '" + afeId + "'");
        }

        public DataTable SelectAnswerWithQuestionOfAFE(int afeId)
        {
            return db.GetData("SELECT q.question, a.answer, sg.value, sr.relative_name, " +
                "sr.company_name FROM Answers a INNER JOIN Questions q ON a.question_code = q.question_code " +
                "LEFT JOIN Specify_General sg ON a.answer_id = sg.answer_id " +
                "LEFT JOIN Specify_Relatives sr ON a.answer_id = sr.answer_id " +
                "WHERE a.afe_id = '" + afeId + "'");
        }

        public DataTable InsertAnswer(int afeId, string questionCode, string answer)
        {
            return db.ExecuteWithOutput("InsertAnswer", "INSERT INTO Answers (afe_id, question_code, " +
                "answer) OUTPUT INSERTED.answer_id VALUES ('" + afeId + "','" + questionCode + "','" +
                answer + "')");
        }

        public void UpdateAnswer(int answerId, string answer)
        {
            db.Execute("UpdateAnswer", "UPDATE Answers SET answer = '" + answer + "' " +
                "WHERE answer_id = '" + answerId + "'");
        }
    }
}