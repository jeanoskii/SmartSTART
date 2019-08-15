using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class CandidateDAO
    {
        private DB db = new DB();

        public DataTable SelectCandidate(int candidateId)
        {
            return db.GetData("SELECT candidate_id, password, email, status " +
                "FROM Candidates WHERE candidate_id = '" + candidateId + "'");
        }

        public DataTable SelectCandidate(string email)
        {
            return db.GetData("SELECT candidate_id, email, status " +
                "FROM Candidates WHERE email = '" + email + "'");
        }

        public DataTable SelectCandidateNoPassword(int candidateId)
        {
            return db.GetData("SELECT candidate_id, email, status " +
                "FROM Candidates WHERE candidate_id = '" + candidateId + "'");
        }

        public DataTable SelectCandidateEmail(int candidateId)
        {
            return db.GetData("SELECT email FROM Candidates WHERE candidate_id = '" +
                candidateId + "'");
        }

        public DataTable SelectAll()
        {
            return db.GetData("SELECT candidate_id, email, status FROM Candidates");
        }

        public DataTable SelectAll(string status)
        {
            return db.GetData("SELECT candidate_id, email, status FROM Candidates WHERE " +
                "status = '" + status + "'");
        }

        public DataTable SelectAllPendingApplicantPreboardingHired()
        {
            return db.GetData("select candidate_id, email, status FROM Candidates WHERE " +
                "status = 'pending' OR status = 'applicant' OR status = 'preboarding' " +
                "OR status = 'hired'");
        }

        public void RegisterCandidate(int candidateId, string password, string email)
        {
            db.Execute("RegisterCandidate", "INSERT INTO Candidates (candidate_id, password, " +
                "email, status) VALUES ('" + candidateId + "','" + password + "','" + email +
                "','pending')");
        }

        public void ChangeStatus(int candidateId, string status)
        {
            db.Execute("ChangeStatus", "UPDATE Candidates SET status = '" + status + "' WHERE " +
                "candidate_id = '" + candidateId + "'");
        }

        public void NewPassword(int candidateId, string password)
        {
            db.Execute("NewPassword", "UPDATE Candidates SET password = '" + password +
                "' WHERE candidate_id = '" + candidateId + "'");
        }

        public void Hire(int candidateId, string hiringDate)
        {
            db.Execute("Hire", "UPDATE Candidates SET status = 'hired', hiring_date = '" + hiringDate + "' WHERE " +
                "candidate_id = '" + candidateId + "'");
        }
    }
}