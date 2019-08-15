using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class PersonalReferenceBLO
    {
        private PersonalReferenceDAO personalReferenceDAO = new PersonalReferenceDAO();

        public DataTable SelectPersonalReferenceOfAFE(int afeId)
        {
            return personalReferenceDAO.SelectPersonalReferenceOfAFE(afeId);
        }

        public DataTable SelectSpecificPersonalReference(int personalReferenceId)
        {
            return personalReferenceDAO.SelectSpecificPersonalReference(personalReferenceId);
        }

        public void InsertPersonalReference(int afeId, string lastName, string firstName,
            string contactNumber, string relationship)
        {
            personalReferenceDAO.InsertPersonalReference(afeId, lastName, firstName,
                contactNumber, relationship);
        }

        public void UpdatePersonalReference(int personalReferenceId, string lastName,
            string firstName, string contactNumber, string relationship)
        {
            personalReferenceDAO.UpdatePersonalReference(personalReferenceId, lastName,
                firstName, contactNumber, relationship);
        }

        public void DeletePersonalReference(int personalReferenceId)
        {
            personalReferenceDAO.DeletePersonalReference(personalReferenceId);
        }
    }
}