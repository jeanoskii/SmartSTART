using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class HRDAO
    {
        private DB db = new DB();
        public DataTable SelectAll()
        {
            return db.GetData("SELECT first_name, last_name, email, type FROM HR");
        }

        public DataTable SelectAllByEmail(string email)
        {
            return db.GetData("SELECT first_name, last_name, email, type FROM HR WHERE email = '" + email + "'");
        }

        public DataTable SelectAllByType(string type)
        {
            return db.GetData("SELECT first_name, last_name, email, type FROM HR WHERE type = '" + type + "'");
        }

        public DataTable SelectHR(string email)
        {
            return db.GetData("SELECT first_name, last_name, email, password, type FROM HR WHERE email = '" + email + "'");
        }

        public DataTable SelectHRByName(string name)
        {
            return db.GetData("SELECT first_name, last_name, email, type FROM HR WHERE last_name = '" + name + "' OR "
                + "first_name = '" + name + "'");
        }

        public void InsertHR(string firstName, string lastName, string email, string password, string type)
        {
            db.Execute("InsertHR", "INSERT INTO HR (first_name, last_name, email, password, type) VALUES ('" +
                firstName + "','" + lastName + "','" + email + "','" + password + "','" + type + "')");
        }

        public void UpdateHR(string firstName, string lastName, string email, string password, string type)
        {
            db.Execute("UpdateHR", "UPDATE HR SET first_name = '" + firstName + "', last_name = '" + lastName + "', "
                + "password = '" + password + "', type = '" + type + "' WHERE email = '" + email + "'");
        }

        public void DeleteHR(string email)
        {
            db.Execute("DeleteHR", "UPDATE HR SET type = 'revoked' WHERE email = '" + email + "'");
        }

        public void NewPassword(string email, string password)
        {
            db.Execute("NewPassword", "UPDATE HR SET password = '" + password + "' WHERE email = '" +
                email + "'");
        }
    }
}