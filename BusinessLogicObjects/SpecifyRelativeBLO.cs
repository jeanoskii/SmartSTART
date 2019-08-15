using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using CAPRES.ValueObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class SpecifyRelativeBLO
    {
        private SpecifyRelativeDAO specifyRelativeDAO = new SpecifyRelativeDAO();
        private SpecifyRelativeVO specifyRelativeVO = new SpecifyRelativeVO();
        private DataTable dt = new DataTable();

        public SpecifyRelativeVO SelectSpecifyRelativeOfAnswer(int answerId)
        {
            dt = specifyRelativeDAO.SelectSpecifyRelativeOfAnsewr(answerId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    specifyRelativeVO.SpecifyRelativeId = int.Parse(
                        dr["specify_relative_id"].ToString());
                    specifyRelativeVO.CompanyName = dr["company_name"].ToString();
                    specifyRelativeVO.RelativeName = dr["relative_name"].ToString();
                }
                return specifyRelativeVO;
            }
            else
            {
                return null;
            }
        }

        public void InsertSpecifyRelative(int answerId, string companyName,
            string relativeName)
        {
            specifyRelativeDAO.InsertSpecifyRelative(answerId, companyName, relativeName);
        }

        public void UpdateSpecifyRelative(int specifyRelativeId, string companyName,
            string relativeName)
        {
            specifyRelativeDAO.UpdateSpecifyRelative(specifyRelativeId, companyName,
                relativeName);
        }
    }
}