using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class File3DAO
    {
        private DB db = new DB();

        public DataTable SelectFile3ForCSV(string startDate, string endDate)
        {
            return db.GetData("SELECT File3.candidate_id, previous_employer, city, "
                + "position_held, specialization_code, immediate_superior, "
                + "previous_salary, industry_code, reason_code, "
                + "CONVERT(char(10), start_date, 101) as start_date, "
                + "CONVERT(char(10), end_date, 101) as end_date FROM File3, AFE, Candidates "
                + "WHERE File3.candidate_id = Candidates.candidate_id "
                + "AND Candidates.candidate_id = AFE.candidate_id AND Candidates.status = 'hired' "
                + "AND Candidates.hiring_date BETWEEN '" + startDate + "' AND '" + endDate + "'");
        }

        public DataTable SelectFile3OfCandidate(int candidateId)
        {
            return db.GetData("SELECT File3.file3_id, File3.candidate_id, "
                + "File3.previous_employer, File3.city, File3.position_held, "
                + "Specializations.specialization, File3.immediate_superior, "
                + "File3.previous_salary, Industries.industry, Reasons.reason, "
                + "File3.start_date, File3.end_date FROM File3, Specializations, "
                + "Industries, Reasons WHERE candidate_id = '" + candidateId + "' "
                + "AND File3.specialization_code = Specializations.specialization_code AND "
                + "File3.industry_code = Industries.industry_code AND "
                + "File3.reason_code = Reasons.reason_code");
        }

        public DataTable SelectSpecificFile3(int file3Id)
        {
            return db.GetData("SELECT * FROM File3 WHERE file3_id = '" + file3Id + "'");
        }

        public void InsertFile3(int candidateId, string previousEmployer, string city,
            string positionHeld, string specialization, string immediateSuperior,
            string previousSalary, string industry, string reason, string startDate,
            string endDate)
        {
            db.Execute("InsertFile3", "INSERT INTO File3 (candidate_id, previous_employer, "
                + "city, position_held, specialization_code, immediate_superior, previous_salary, "
                + "industry_code, reason_code, start_date, end_date) VALUES ('" + candidateId + "','"
                + previousEmployer + "','" + city + "','" + positionHeld + "','"
                + specialization + "','" + immediateSuperior + "','" + previousSalary + "','"
                + industry + "','" + reason + "','" + startDate + "','" + endDate + "')");
        }

        public void UpdateFile3(int file3Id, int candidateId, string previousEmployer,
            string city, string positionHeld, string specialization, string immediateSuperior,
            string previousSalary, string industry, string reason, string startDate,
            string endDate)
        {
            db.Execute("UpdateFile3", "UPDATE File3 SET previous_employer = '"
                + previousEmployer + "', city = '" + city + "', specialization_code = '"
                + specialization + "', immediate_superior = '"
                + immediateSuperior + "', previous_salary = '"
                + previousSalary + "', industry_code = '" + industry + "', reason_code = '"
                + reason + "', start_date = '" + startDate + "', end_date = '" + endDate + "' "
                + "WHERE file3_id = '" + file3Id + "' AND candidate_id = '" + candidateId + "'");
        }

        public void DeleteFile3(int file3Id)
        {
            db.Execute("DeleteFile3", "DELETE FROM File3 WHERE file3_id = '" + file3Id + "'");
        }
    }
}