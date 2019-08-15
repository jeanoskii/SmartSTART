using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class ProfessionalReferenceDAO
    {
        private DB db = new DB();

        public DataTable SelectProfessionalReferenceOfAFE(int afeId)
        {
            return db.GetData("SELECT professional_reference_id, last_name, first_name, " +
                "contact_number, company_name, position_title FROM Professional_References WHERE afe_id = '" + afeId + "'");
        }

        public DataTable SelectSpecificProfessionalReference(int professionalReferenceId)
        {
            return db.GetData("SELECT last_name, first_name, contact_number, company_name, " +
                "position_title FROM Professional_References WHERE professional_reference_id = '" +
                professionalReferenceId + "'");
        }

        public void InsertProfessionalReference(int afeId, string lastName, string firstName,
            string contactNumber, string companyName, string positionTitle)
        {
            db.Execute("InsertProfessionalRef", "INSERT INTO Professional_References (afe_id, " +
                "last_name, first_name, contact_number, company_name, position_title) VALUES ('" +
                afeId + "','" + lastName + "','" + firstName + "','" + contactNumber + "','" +
                companyName + "','" + positionTitle + "')");
        }

        public void UpdateProfessionalReference(int professionalReferenceId, string lastName,
            string firstName, string contactNumber, string companyName, string positionTitle)
        {
            db.Execute("UpdateProfessionalRef", "UPDATE Professional_References SET last_name = '" +
                lastName + "', first_name = '" + firstName + "', contact_number = '" + contactNumber +
                "', company_name = '" + companyName + "', position_title = '" + positionTitle +
                "' WHERE professional_reference_id = '" + professionalReferenceId + "'");
        }

        public void DeleteProfessionalReference(int professionalReferenceId)
        {
            db.Execute("DeleteProfessionalRef", "DELETE FROM Professional_References WHERE " +
                "professional_reference_id = '" + professionalReferenceId + "'");
        }
    }
}