using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.ValueObjects;
using CAPRES.DataAccessObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class File2BLO
    {
        private File2VO file2VO = new File2VO();
        private File2DAO file2DAO = new File2DAO();

        public DataTable SelectFile2ForCSV(string startDate, string endDate)
        {
            return file2DAO.SelectFile2ForCSV(startDate, endDate);
        }

        public DataTable SelectFile2OfCandidate(int candidateId)
        {
            return file2DAO.SelectFile2OfCandidate(candidateId);
        }

        public DataTable SelectSpecificFile2(int file2Id)
        {
            return file2DAO.SelectSpecificFile2(file2Id);
        }

        public bool File2OfCandidateHasSchoolOthers(int candidateId)
        {
            DataTable dt = file2DAO.File2OfCandidateHasSchoolOthers(candidateId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[0].ToString() != "")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void InsertFile2(int candidateId, string educationEstablishment, string school,
            string schoolOthers, string startDate, string endDate, string branchOfStudy1,
            string certificate, string courseAppraisal, string branchOfStudy2)
        {
            file2DAO.InsertFile2(candidateId, educationEstablishment, school, schoolOthers,
                startDate, endDate, branchOfStudy1, certificate, courseAppraisal, branchOfStudy2);
        }

        public void UpdateFile2(int file2Id, int candidateId, string educationEstablishment,
            string school, string schoolOthers, string startDate, string endDate,
            string branchOfStudy1, string certificate, string courseAppraisal, string branchOfStudy2)
        {
            file2DAO.UpdateFile2(file2Id, candidateId, educationEstablishment, school, schoolOthers,
                startDate, endDate, branchOfStudy1, certificate, courseAppraisal, branchOfStudy2);
        }

        public void DeleteFile2(int file2Id)
        {
            file2DAO.DeleteFile2(file2Id);
        }
    }
}