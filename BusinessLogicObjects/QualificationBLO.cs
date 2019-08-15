using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class QualificationBLO
    {
        private QualificationDAO qualificationDAO = new QualificationDAO();

        public DataTable SelectQualificationOfAFE(int afeId)
        {
            return qualificationDAO.SelectQualificationOfAFE(afeId);
        }

        public DataTable SelectSpecificQualification(int otherQualificationid)
        {
            return qualificationDAO.SelectSpecificQualification(otherQualificationid);
        }

        public bool QualificationOfAFEHasOthers(int afeId)
        {
            DataTable dt = qualificationDAO.QualificationOfAFEHasOthers(afeId);
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

        public void InsertQualification(int afeId, string licenseCode, string othersName,
            string number, string rating)
        {
            qualificationDAO.InsertQualification(afeId, licenseCode, othersName,
                number, rating);
        }

        public void UpdateQualification(int otherQualificationId, string licenseCode,
            string othersName, string number, string rating)
        {
            qualificationDAO.UpdateQualification(otherQualificationId, licenseCode, othersName,
                number, rating);
        }

        public void DeleteQualification(int otherQualificationId)
        {
            qualificationDAO.DeleteQualification(otherQualificationId);
        }
    }
}