using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CAPRES.DataAccessObjects
{
    public class SchoolDAO
    {
        private DB db = new DB();

        public DataTable SelectAllSchool()
        {
            return db.GetData("SELECT school_code, school FROM Schools ORDER BY school");
        }

        public DataTable SelectSchoolOfEstablishment(string educationEstablishmentCode)
        {
            return db.GetData("SELECT Schools.school_code, Schools.school "
                + "FROM Schools, Establishment_Schools, Education_Establishments "
                + "WHERE Education_Establishments.education_establishment_code = '"
                + educationEstablishmentCode + "' AND Schools.school_code = "
                + "Establishment_Schools.school_code "
                + "AND Education_Establishments.education_establishment_code = "
                + "Establishment_Schools.education_establishment_code "
                + "ORDER BY Schools.school_code");
        }

        public DataTable SelectSchool(string schoolCode)
        {
            return db.GetData("SELECT school_code, school FROM Schools WHERE school_code = '"
                + schoolCode + "'");
        }

        public void InsertSchool(string schoolCode, string school)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@school_code", schoolCode);
            parameters[1] = new SqlParameter("@school", school);
            db.Execute("InsertSchool", "INSERT INTO Schools (school_code, school) VALUES ("
                + "@school_code, @school)", parameters);
        }

        public void UpdateSchool(string schoolCode, string school)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@school_code", schoolCode);
            parameters[1] = new SqlParameter("@school", school);
            db.Execute("UpdateSchool", "UPDATE Schools SET school = @school WHERE school_code = "
                + "@school_code", parameters);
        }

        public void DeleteSchool(string schoolCode)
        {
            db.Execute("DeleteSchool", "DELETE FROM Schools WHERE school_code = '"
                + schoolCode + "'");
        }
    }
}