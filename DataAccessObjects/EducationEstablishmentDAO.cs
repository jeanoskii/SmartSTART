using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CAPRES.DataAccessObjects
{
    public class EducationEstablishmentDAO
    {
        private DB db = new DB();

        public DataTable SelectAllEducationEstablishment()
        {
            return db.GetData("SELECT education_establishment_code, education_establishment "
                + "FROM Education_Establishments");
        }

        public DataTable SelectEducationEstablishment(string educationEstablishmentCode)
        {
            return db.GetData("SELECT education_establishment_code, education_establishment "
                + "FROM Education_Establishments WHERE education_establishment_code = '"
                + educationEstablishmentCode + "'");
        }

        public void InsertEducationEstablishment(string educationEstablishmentCode,
            string educationEstablishment)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@education_establishment_code", educationEstablishmentCode);
            parameters[1] = new SqlParameter("@education_establishment", educationEstablishment);
            db.Execute("InsertEducationEstablishment", "INSERT INTO Education_Establishments "
                + "(education_establishment_code, education_establishment) VALUES ("
                + "@education_establishment_code, @education_establishment)", parameters);
        }

        public void UpdateEducationEstablishment(string educationEstablishmentCode,
            string educationEstablishment)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@education_establishment_code", educationEstablishmentCode);
            parameters[1] = new SqlParameter("@education_establishment", educationEstablishment);
            db.Execute("UpdateEducationEstablishment", "UPDATE Education_Establishments SET "
                + "education_establishment = @education_establishment WHERE "
                + "education_establishment_code = @education_establishment_code", parameters);
        }

        public void DeleteEducationEstablishment(string educationEstabishmentCode)
        {
            db.Execute("DeleteEducationEstablishment", "DELETE FROM Education_Establishments WHERE "
                + "education_establishment_code = '" + educationEstabishmentCode + "'");
        }
    }
}