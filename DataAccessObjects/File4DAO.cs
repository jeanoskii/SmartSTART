using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class File4DAO
    {
        private DB db = new DB();

        public DataTable SelectFile4ForCSV(string startDate, string endDate)
        {
            return db.GetData("SELECT File4.candidate_id, organization_type_code, "
                + "organization_name, CONVERT(char(10), start_date, 101) as start_date, "
                + "CONVERT(char(10), end_date, 101) as end_date, position_type_code FROM File4, "
                + "AFE, Candidates WHERE File4.candidate_id = Candidates.candidate_id "
                + "AND Candidates.candidate_id = AFE.candidate_id AND Candidates.status = 'hired' "
                + "AND Candidates.hiring_date BETWEEN '" + startDate + "' AND '" + endDate + "'");
        }

        public DataTable SelectFile4OfCandidate(int candidateId)
        {
            return db.GetData("SELECT File4.file4_id, File4.candidate_id, "
                + "Organization_Types.organization_type, File4.organization_name, "
                + "File4.start_date, File4.end_date, Position_Types.position_type "
                + "FROM File4, Organization_Types, Position_Types "
                + "WHERE File4.candidate_id = '" + candidateId + "' "
                + "AND File4.organization_type_code = Organization_Types.organization_type_code "
                + "AND File4.position_type_code = Position_Types.position_type_code");
        }

        public DataTable SelectSpecificFile4(int file4Id)
        {
            return db.GetData("SELECT file4_id, candidate_id, organization_type_code, "
                + "organization_name, start_date, end_date, position_type_code "
                + "FROM File4 WHERE file4_id = '" + file4Id + "'");
        }

        public void InsertFile4(int candidateId, string organizationType,
            string organizationName, string startDate, string endDate,
            string positionType)
        {
            if (endDate == null)
            {
                db.Execute("InsertFile4", "INSERT INTO File4 (candidate_id, "
                    + "organization_type_code, organization_name, start_date, end_date, "
                    + "position_type_code) VALUES ('" + candidateId + "','" + organizationType + "','"
                    + organizationName + "','" + startDate + "',NULL,'"
                    + positionType + "')");
            }
            else
            {
                db.Execute("InsertFile4", "INSERT INTO File4 (candidate_id, "
                    + "organization_type_code, organization_name, start_date, end_date, "
                    + "position_type_code) VALUES ('" + candidateId + "','" + organizationType + "','"
                    + organizationName + "','" + startDate + "','" + endDate + "','"
                    + positionType + "')");
            }
        }

        public void UpdateFile4(int file4Id, int candidateId, string organizationType,
            string organizationName, string startDate, string endDate, string positionType)
        {
            if (endDate == null)
            {
                db.Execute("UpdateFile4", "UPDATE File4 SET organization_type_code = '"
                    + organizationType + "', organization_name = '"
                    + organizationName + "', start_date = '" + startDate + "', end_date = NULL "
                    + "WHERE file4_id = '" + file4Id + "' AND candidate_id = '"
                    + candidateId + "'");
            }
            else
            {
                db.Execute("UpdateFile4", "UPDATE File4 SET organization_type_code = '"
                    + organizationType + "', organization_name = '"
                    + organizationName + "', start_date = '" + startDate + "', end_date = '" + endDate + "' "
                    + "WHERE file4_id = '" + file4Id + "' AND candidate_id = '"
                    + candidateId + "'");
            }
        }

        public void DeleteFile4(int file4Id)
        {
            db.Execute("DeleteFile4", "DELETE FROM File4 WHERE file4_id = '" + file4Id + "'");
        }
    }
}