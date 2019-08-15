using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class QualificationDAO
    {
        private DB db = new DB();

        public DataTable SelectQualificationOfAFE(int afeId)
        {
            return db.GetData("SELECT other_qualification_id, Licenses.license, " +
                "others_name, number, rating FROM Other_Qualifications, Licenses " +
                "WHERE Other_Qualifications.license_code = Licenses.license_code AND afe_id = '" +
                afeId + "'");
        }

        public DataTable SelectSpecificQualification(int otherQualificationId)
        {
            return db.GetData("SELECT other_qualification_id, license_code, " +
                "others_name, number, rating FROM Other_Qualifications " +
                "WHERE other_qualification_id = '" + otherQualificationId + "'");
        }

        public DataTable QualificationOfAFEHasOthers(int afeId)
        {
            return db.GetData("SELECT others_name FROM Other_Qualifications WHERE afe_id = '" + afeId + "'");
        }

        public void InsertQualification(int afeId, string licenseCode, string othersName, string number,
            string rating)
        {
            db.Execute("InsertQualification", "INSERT INTO Other_Qualifications (afe_id, license_code, " +
                "others_name, number, rating) VALUES ('" + afeId + "','" + licenseCode + "','" +
                othersName + "','" + number + "','" + rating + "')");
        }

        public void UpdateQualification(int otherQualificationId, string licenseCode, string othersName,
            string number, string rating)
        {
            db.Execute("UpdateQualification", "UPDATE Other_Qualifications SET license_code = '" +
                licenseCode + "', others_name = '" + othersName + "', number = '" + number + "', " +
                "rating = '" + rating + "' WHERE other_qualification_id = '" + otherQualificationId + "'");
        }

        public void DeleteQualification(int otherQualificationid)
        {
            db.Execute("DeleteQualification", "DELETE FROM Other_Qualifications WHERE " +
                "other_qualification_id = '" + otherQualificationid + "'");
        }
    }
}