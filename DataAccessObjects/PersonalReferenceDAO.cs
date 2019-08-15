using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class PersonalReferenceDAO
    {
        private DB db = new DB();

        public DataTable SelectPersonalReferenceOfAFE(int afeId)
        {
            return db.GetData("SELECT personal_reference_id, last_name, first_name, " +
                "contact_number, relationship FROM Personal_References WHERE afe_id = '" +
                afeId + "'");
        }

        public DataTable SelectSpecificPersonalReference(int personalReferenceId)
        {
            return db.GetData("SELECT last_name, first_name, contact_number, relationship " +
                "FROM Personal_References WHERE personal_reference_id = '" + personalReferenceId + "'");
        }

        public void InsertPersonalReference(int afeId, string lastName, string firstName,
            string contactNumber, string relationship)
        {
            db.Execute("InsertPersonalRef", "INSERT INTO Personal_References (afe_id, " +
                "last_name, first_name, contact_number, relationship) VALUES ('" + afeId +
                "','" + lastName + "','" + firstName + "','" + contactNumber + "','" +
                relationship + "')");
        }

        public void UpdatePersonalReference(int personalReferenceId, string lastName,
            string firstName, string contactNumber, string relationship)
        {
            db.Execute("UpdatePersonalRef", "UPDATE Personal_References SET last_name = '" +
                lastName + "', first_name = '" + firstName + "', contact_number = '" +
                contactNumber + "', relationship = '" + relationship + "' WHERE " +
                "personal_reference_id = '" + personalReferenceId + "'");
        }

        public void DeletePersonalReference(int personalReferenceId)
        {
            db.Execute("DeletePersonalRef", "DELETE FROM Personal_References WHERE " +
                "personal_reference_id = '" + personalReferenceId + "'");
        }
    }
}