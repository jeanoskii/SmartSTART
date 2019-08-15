using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.ValueObjects;
using CAPRES.DataAccessObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class File3BLO
    {
        private File3VO file3VO = new File3VO();
        private File3DAO file3DAO = new File3DAO();

        public DataTable SelectFile3ForCSV(string startDate, string EndDate)
        {
            return file3DAO.SelectFile3ForCSV(startDate, EndDate);
        }

        public DataTable SelectFile3OfCandidate(int candidateId)
        {
            return file3DAO.SelectFile3OfCandidate(candidateId);
        }

        public DataTable SelectSpecificFile3(int file3Id)
        {
            return file3DAO.SelectSpecificFile3(file3Id);
        }

        public void InsertFile3(int candidateId, string previousEmployer,
            string city, string positionHeld, string specialization,
            string immediateSuperior, string previousSalary,
            string industry, string reason, string startDate,
            string endDate)
        {
            file3DAO.InsertFile3(candidateId, previousEmployer,
                city, positionHeld, specialization, immediateSuperior,
                previousSalary, industry, reason, startDate, endDate);
        }

        public void UpdateFile3(int file3Id, int candidateId,
            string previousEmployer, string city, string positionHeld,
            string specialization, string immediateSuperior, string previousSalary,
            string industry, string reason, string startDate,
            string endDate)
        {
            file3DAO.UpdateFile3(file3Id, candidateId, previousEmployer,
                city, positionHeld, specialization, immediateSuperior,
                previousSalary, industry, reason, startDate, endDate);
        }

        public void DeleteFile3(int file3Id)
        {
            file3DAO.DeleteFile3(file3Id);
        }
    }
}