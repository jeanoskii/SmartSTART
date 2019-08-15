using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class File5DAO
    {
        private DB db = new DB();

        public DataTable SelectFile5ForCSV(string startDate, string endDate)
        {
            return db.GetData("SELECT File5.candidate_id, dependents_name, family_member_code, "
                + "gender_code, CONVERT(char(10), birthdate, 101) as birthdate FROM File5, AFE, Candidates "
                + "WHERE File5.candidate_id = Candidates.candidate_id "
                + "AND Candidates.candidate_id = AFE.candidate_id AND Candidates.status = 'hired' "
                + "AND Candidates.hiring_date BETWEEN '" + startDate + "' AND '" + endDate + "'");
        }

        public DataTable SelectFile5OfCandidate(int candidateId)
        {
            return db.GetData("SELECT File5.file5_id, File5.candidate_id, File5.dependents_name, "
                + "Family_Members.family_member, Genders.gender, File5.birthdate, File5.company_name, "
                + "File5.position_title from File5, Genders, Family_Members where File5.candidate_id = '"
                + candidateId + "' AND File5.gender_code = Genders.gender_code "
                + "AND File5.family_member_code = Family_Members.family_member_code");
        }

        public DataTable SelectSpecificFile5(int file5Id)
        {
            return db.GetData("SELECT * FROM File5 WHERE file5_id = '" + file5Id + "'");
        }

        public void InsertFile5(int candidateId, string dependentsName,
            string familyMember, string gender, string birthdate, string companyName,
            string positionTitle)
        {
            db.Execute("InsertFile5", "INSERT INTO File5 (candidate_id, "
                + "dependents_name, family_member_code, gender_code, birthdate, "
                + "company_name, position_title) VALUES ('" + candidateId + "','" + dependentsName + "','"
                + familyMember + "','" + gender + "','" + birthdate + "','" + companyName + "','"
                + positionTitle + "')");
        }

        public void UpdateFile5(int file5Id, int candidateId,
            string dependentsName, string familyMember, string gender,
            string birthdate, string companyName, string positionTitle)
        {
            db.Execute("UpdateFile5", "UPDATE File5 SET dependents_name = '"
                + dependentsName + "', family_member_code = '"
                + familyMember + "', gender_code = '" + gender + "', birthdate = '"
                + birthdate + "', company_name = '" + companyName + "', position_title = '"
                + positionTitle + "' WHERE file5_id = '" + file5Id + "' AND candidate_id = '"
                + candidateId + "'");
        }

        public void DeleteFile5(int file5Id)
        {
            db.Execute("DeleteFile5", "DELETE FROM File5 WHERE file5_id ='"
                + file5Id + "'");
        }
    }
}