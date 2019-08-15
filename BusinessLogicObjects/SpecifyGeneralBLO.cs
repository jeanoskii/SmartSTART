using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using CAPRES.ValueObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class SpecifyGeneralBLO
    {
        private SpecifyGeneralDAO specifyGeneralDAO = new SpecifyGeneralDAO();
        private SpecifyGeneralVO specifyGeneralVO = new SpecifyGeneralVO();
        private DataTable dt = new DataTable();

        public SpecifyGeneralVO SelectSpecifyGeneralOfAnswer(int answerId)
        {
            dt = specifyGeneralDAO.SelectSpecifyGeneralOfAnswer(answerId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    specifyGeneralVO.SpecifyGeneralId = int.Parse(
                        dr["specify_general_id"].ToString());
                    specifyGeneralVO.SpecifyValue = dr["value"].ToString();
                }
                return specifyGeneralVO;
            }
            else
            {
                return null;
            }
        }

        public void InsertSpecifyGeneral(int answerId, string value)
        {
            specifyGeneralDAO.InsertSpecifyGeneral(answerId, value);
        }

        public void UpdateSpecifyGeneral(int specifyGeneralId, string value)
        {
            specifyGeneralDAO.UpdateSpecifyGeneral(specifyGeneralId, value);
        }
    }
}