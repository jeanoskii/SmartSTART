using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CAPRES.DataAccessObjects
{
    public class CountryDAO
    {
        private DB db = new DB();

        public DataTable SelectAllCountry()
        {
            return db.GetData("SELECT country_code, country FROM Countries ORDER BY country");
        }

        public DataTable SelectCountry(string countryCode)
        {
            return db.GetData("SELECT country_code, country FROM Countries WHERE country_code = '"
                + countryCode + "'");
        }

        public void InsertCountry(string countryCode, string country)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@country_code", countryCode);
            parameters[1] = new SqlParameter("@country", country);
            db.Execute("InsertCountry", "INSERT INTO Countries (country_code, country) VALUES ("
                + "@country_code, @country)", parameters);
        }

        public void UpdateCountry(string countryCode, string country)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@country_code", countryCode);
            parameters[1] = new SqlParameter("@country", country);
            db.Execute("UpdateCountry", "UPDATE Countries SET country = @country WHERE "
                + "country_code = @country_code", parameters);
        }

        public void DeleteCountry(string countryCode)
        {
            db.Execute("DeleteCountry", "DELETE FROM Countries WHERE country_code = '"
                + countryCode + "'");
        }
    }
}