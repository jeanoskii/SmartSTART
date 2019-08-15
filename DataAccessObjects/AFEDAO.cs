using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class AFEDAO
    {
        private DB db = new DB();
        public DataTable SelectAFEOfCandidate(int candidateId)
        {
            return db.GetData("SELECT afe_id, application_date, position_applied, interviewer_name " +
                "FROM AFE WHERE candidate_id = '" + candidateId + "'");
        }

        public DataTable SelectAFEIDOfCandidate(int candidateId)
        {
            return db.GetData("SELECT afe_id FROM AFE WHERE candidate_id = '" + candidateId + "'");
        }

        public void InsertAFE(int candidateId, string applicationDate,
            string positionAppliedFor, string interviewerName)
        {
            db.Execute("InsertAFE", "INSERT INTO AFE (candidate_id, application_date, " +
                "position_applied, interviewer_name) VALUES ('" + candidateId +
                "','" + applicationDate + "','" + positionAppliedFor + "','" +
                interviewerName + "')");
        }

        public void UpdateAFE(int afeId, string applicationDate, string positionAppliedFor,
            string interviewerName)
        {
            db.Execute("UpdateAFE", "UPDATE AFE SET application_date = '" + applicationDate +
                "', position_applied = '" + positionAppliedFor +
                "', interviewer_name = '" + interviewerName + "' WHERE afe_id = '" + afeId + "'");
        }
    }
}