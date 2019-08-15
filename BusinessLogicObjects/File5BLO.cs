using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.ValueObjects;
using CAPRES.DataAccessObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class File5BLO
    {
        private File5VO file5VO = new File5VO();
        private File5DAO file5DAO = new File5DAO();
        private DataTable dt;

        public DataTable SelectFile5ForCSV(string startDate, string endDate)
        {
            return file5DAO.SelectFile5ForCSV(startDate, endDate);
        }

        public DataTable SelectFile5OfCandidate(int candidateId)
        {
            //dt = file5DAO.SelectFile5OfCandidate(candidateId);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    file5VO.File5Id = int.Parse(dr["file5_id"].ToString());
            //    file5VO.CandidateID = int.Parse(dr["candidate_id"].ToString());
            //    file5VO.DependentsName = dr["dependents_name"].ToString();
            //    file5VO.Gender = dr["gender_code"].ToString();
            //    file5VO.Birthdate = dr["birthdate"].ToString();
            //}
            return file5DAO.SelectFile5OfCandidate(candidateId);
        }

        public DataTable SelectSpecificFile5(int file5Id)
        {
            return file5DAO.SelectSpecificFile5(file5Id);
        }

        public void InsertFile5(int candidateId, string dependentsName,
            string familyMember, string gender, string birthdate,
            string companyName, string positionTitle)
        {
            file5DAO.InsertFile5(candidateId, dependentsName,
                familyMember, gender, birthdate, companyName, positionTitle);
        }

        public void UpdateFile5(int file5Id, int candidateId,
            string dependentsName, string familyMember, string gender,
            string birthdate, string companyName, string positionTitle)
        {
            file5DAO.UpdateFile5(file5Id, candidateId, dependentsName,
                familyMember, gender, birthdate, companyName, positionTitle);
        }

        public void DeleteFile5(int file5Id)
        {
            file5DAO.DeleteFile5(file5Id);
        }
    }
}