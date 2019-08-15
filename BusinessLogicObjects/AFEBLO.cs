using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using CAPRES.ValueObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class AFEBLO
    {
        private AFEDAO afeDAO = new AFEDAO();
        private AFEVO afeVO = new AFEVO();
        private DataTable dt;
        public AFEVO SelectAFEOfCandidate(int candidateId)
        {
            dt = afeDAO.SelectAFEOfCandidate(candidateId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    afeVO.AfeId = int.Parse(dr["afe_id"].ToString());
                    afeVO.ApplicationDate = dr["application_date"].ToString();
                    afeVO.PositionAppliedFor = dr["position_applied"].ToString();
                    afeVO.InterviewerName = dr["interviewer_name"].ToString();
                }
                return afeVO;
            }
            else
            {
                return null;
            }
        }

        public int SelectAFEIDOfCandidate(int candidateId)
        {
            return int.Parse(afeDAO.SelectAFEIDOfCandidate(candidateId)
                .Rows[0][0].ToString());
        }
        public void InsertAFE(int candidateId, string applicationDate,
            string positionAppliedFor, string interviewerName)
        {
            afeDAO.InsertAFE(candidateId, applicationDate, positionAppliedFor,
                interviewerName);
        }

        public void UpdateAFE(int afeId, string applicationDate,
            string positionAppliedFor, string interviewerName)
        {
            afeDAO.UpdateAFE(afeId, applicationDate, positionAppliedFor, interviewerName);
        }
    }
}