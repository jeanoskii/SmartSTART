using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.ValueObjects;
using CAPRES.DataAccessObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class File4BLO
    {
        private File4VO file4VO = new File4VO();
        private File4DAO file4DAO = new File4DAO();

        public DataTable SelectFile4ForCSV(string startDate, string endDate)
        {
            return file4DAO.SelectFile4ForCSV(startDate, endDate);
        }

        public DataTable SelectFile4OfCandidate(int candidateId)
        {
            return file4DAO.SelectFile4OfCandidate(candidateId);
        }

        public DataTable SelectSpecificFile4(int file4Id)
        {
            return file4DAO.SelectSpecificFile4(file4Id);
        }

        public void InsertFile4(int candidateId, string organizationType,
            string organizationName, string startDate, string endDate,
            string positionType)
        {
            file4DAO.InsertFile4(candidateId, organizationType, organizationName,
                startDate, endDate, positionType);
        }

        public void UpdateFile4(int file4Id, int candidateId, string organizationType,
            string organizationName, string startDate, string endDate,
            string positionType)
        {
            file4DAO.UpdateFile4(file4Id, candidateId, organizationType,
                organizationName, startDate, endDate, positionType);
        }

        public void DeleteFile4(int file4Id)
        {
            file4DAO.DeleteFile4(file4Id);
        }
    }
}