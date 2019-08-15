using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class BloodTypeDAO
    {
        private DB db = new DB();

        public DataTable SelectAllBloodType()
        {
            return db.GetData("SELECT blood_type FROM Blood_Types");
        }

        public DataTable SelectBloodType(string bloodType)
        {
            return db.GetData("SELECT blood_type FROM Blood_Types WHERE blood_type = '" + bloodType + "'");
        }

        public void InsertBloodType(string bloodType)
        {
            db.Execute("InsertBloodType", "INSERT INTO Blood_Types (blood_type) VALUES ('" + bloodType + "')");
        }

        public void UpdateBloodType(string bloodType)
        {
            db.Execute("UpdateBloodType", "UPDATE Blood_Types SET blood_type = '" + bloodType + "' WHERE blood_type = '" + bloodType + "'");
        }

        public void DeleteBloodType(string bloodType)
        {
            db.Execute("DeleteBloodType", "DELETE FROM Blood_Types WHERE blood_type = '" + bloodType + "'");
        }
    }
}