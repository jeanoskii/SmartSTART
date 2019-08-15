using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class File2DAO
    {
        private DB db = new DB();

        public DataTable SelectFile2ForCSV(string startDate, string endDate)
        {
            return db.GetData("SELECT File2.candidate_id, education_establishment_code, school_code, "
                + "school_others, CONVERT(char(10), start_date, 101) as start_date, "
                + "CONVERT(char(10), end_date, 101) as end_date, branch_of_study_1_code, "
                + "certificate_code, course_appraisal, branch_of_study_2_code FROM File2, AFE, Candidates "
                + "WHERE File2.candidate_id = Candidates.candidate_id "
                + "AND Candidates.candidate_id = AFE.candidate_id AND Candidates.status = 'hired' "
                + "AND Candidates.hiring_date BETWEEN '" + startDate + "' AND '" + endDate + "'");
        }

        public DataTable SelectFile2OfCandidate(int candidateId)
        {
            return db.GetData("SELECT File2.file2_id, File2.candidate_id, "
                + "ee.education_establishment, s.school, File2.school_others, "
                + "File2.start_date, File2.end_date, bs1.branch_of_study AS branch_of_study_1, "
                + "c.certificate, File2.course_appraisal, bs2.branch_of_study "
                + "AS branch_of_study_2 FROM File2 INNER JOIN Education_Establishments ee "
                + "ON File2.education_establishment_code = ee.education_establishment_code "
                + "INNER JOIN Schools s ON File2.school_code = s.school_code "
                + "LEFT JOIN Branches_of_Study bs1 ON File2.branch_of_study_1_code = "
                + "bs1.branch_of_study_code LEFT JOIN Branches_of_Study bs2 ON "
                + "File2.branch_of_study_2_code = bs2.branch_of_study_code INNER JOIN "
                + "Certificates c ON File2.certificate_code = c.certificate_code WHERE "
                + "File2.candidate_id = '" + candidateId + "'");
        }

        public DataTable SelectSpecificFile2(int file2Id)
        {
            return db.GetData("SELECT * FROM File2 WHERE file2_id = '" + file2Id + "'");
        }

        public DataTable File2OfCandidateHasSchoolOthers(int candidateId)
        {
            return db.GetData("SELECT school_others FROM File2 WHERE candidate_id = '"
                + candidateId + "'");
        }

        public void InsertFile2(int candidateId, string educationEstablishment, string school,
            string schoolOthers, string startDate, string endDate, string branchOfStudy1,
            string certificate, string courseAppraisal, string branchOfStudy2)
        {
            if (branchOfStudy1 == "" && branchOfStudy2 == "")
            {
                db.Execute("InsertFile2", "INSERT INTO File2 (candidate_id, education_establishment_code, "
                    + "school_code, school_others, start_date, end_date, branch_of_study_1_code, "
                    + "certificate_code, course_appraisal, branch_of_study_2_code) VALUES ('"
                    + candidateId + "','" + educationEstablishment + "','" + school + "','"
                    + schoolOthers + "','" + startDate + "','" + endDate + "', NULL,'"
                    + certificate + "','" + courseAppraisal + "', NULL)");
            }
            else
            {
                db.Execute("InsertFile2", "INSERT INTO File2 (candidate_id, education_establishment_code, "
                    + "school_code, school_others, start_date, end_date, branch_of_study_1_code, "
                    + "certificate_code, course_appraisal, branch_of_study_2_code) VALUES ('"
                    + candidateId + "','" + educationEstablishment + "','" + school + "','"
                    + schoolOthers + "','" + startDate + "','" + endDate + "','" + branchOfStudy1 + "','"
                    + certificate + "','" + courseAppraisal + "','" + branchOfStudy2 + "')");
            }
        }

        public void UpdateFile2(int file2Id, int candidateId, string educationEstablishment,
            string school, string schoolOthers, string startDate, string endDate,
            string branchOfStudy1, string certificate, string courseAppraisal, string branchOfStudy2)
        {
            db.Execute("UpdateFile2", "UPDATE File2 SET education_establishment_code = '"
                + educationEstablishment + "', school_code = '" + school + "', school_others = '"
                + schoolOthers + "', start_date = '" + startDate + "', end_date = '"
                + endDate + "', branch_of_study_1_code = '" + branchOfStudy1 + "', "
                + "certificate_code = '" + certificate + "', course_appraisal = '"
                + courseAppraisal + "', branch_of_study_2_code = '" + branchOfStudy2 + "' WHERE "
                + "file2_id = '" + file2Id + "' AND candidate_id = '" + candidateId + "'");
        }

        public void DeleteFile2(int file2Id)
        {
            db.Execute("DeleteFile2", "DELETE FROM File2 WHERE file2_id = '" + file2Id + "'");
        }
    }
}