using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CAPRES.DataAccessObjects
{
    public class LicenseDAO
    {
        private DB db = new DB();

        public DataTable SelectAllLicense()
        {
            return db.GetData("SELECT license_code, license FROM Licenses");
        }

        public DataTable SelectLicense(string licenseCode)
        {
            return db.GetData("SELECT license_code, license FROM Licenses "
                + "WHERE license_code = '" + licenseCode + "'");
        }

        public void InsertLicense(string licenseCode, string license)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@license_code", licenseCode);
            parameters[1] = new SqlParameter("@license", license);
            db.Execute("InsertLicense", "INSERT INTO Licenses (license_code, license) "
                + "VALUES (@license_code, @license)", parameters);
        }

        public void UpdateLicense(string licenseCode, string license)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@license_code", licenseCode);
            parameters[1] = new SqlParameter("@license", license);
            db.Execute("UpdateLicense", "UPDATE Licenses SET license = @license "
                + "WHERE license_code = @license_code", parameters);
        }

        public void DeleteLicense(string licenseCode)
        {
            db.Execute("DeleteLicense", "DELETE FROM Licenses WHERE license_code = '"
                + licenseCode + "'");
        }
    }
}