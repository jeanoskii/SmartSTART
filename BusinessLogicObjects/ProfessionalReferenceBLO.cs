using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using System.Data;

namespace CAPRES.BusinessLogicObjects
{
    public class ProfessionalReferenceBLO
    {
        private ProfessionalReferenceDAO professionalReferenceDAO =
            new ProfessionalReferenceDAO();

        public DataTable SelectProfessionalReferenceOfAFE(int afeId)
        {
            return professionalReferenceDAO.SelectProfessionalReferenceOfAFE(afeId);
        }

        public DataTable SelectSpecificProfessionalReference(int professionalReferenceId)
        {
            return professionalReferenceDAO.SelectSpecificProfessionalReference(
                professionalReferenceId);
        }

        public void InsertProfessionalReference(int afeId, string lastName,
            string firstName, string contactNumber, string companyName,
            string positionTitle)
        {
            professionalReferenceDAO.InsertProfessionalReference(afeId, lastName,
                firstName, contactNumber, companyName, positionTitle);
        }

        public void UpdateProfessionalReference(int professionalReferenceId,
            string lastName, string firstName, string contactNumber, string companyName,
            string positionTitle)
        {
            professionalReferenceDAO.UpdateProfessionalReference(professionalReferenceId,
                lastName, firstName, contactNumber, companyName, positionTitle);
        }

        public void DeleteProfessionalReference(int professionalRefereneId)
        {
            professionalReferenceDAO.DeleteProfessionalReference(professionalRefereneId);
        }
    }
}