using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using System.Data;

namespace CAPRES.DataAccessObjects
{
    public class EstablishmentBranchDAO
    {
        private DB db = new DB();

        public DataTable SelectEstablishmentBranch(string educationEstablishmentCode,
            string branchOfStudyCode)
        {
            return db.GetData("SELECT education_establishment_code, branch_of_study_code FROM "
                + "Establishment_Branches WHERE education_establishment_code = '"
                + educationEstablishmentCode + "' AND branch_of_study_code = '"
                + branchOfStudyCode + "'");
        }

        public DataTable SelectAllEstablishmentBranch()
        {
            return db.GetData("SELECT education_establishment_code, branch_of_study_code FROM "
                + "Establishment_Branches");
        }

        public void InsertEstablishmentBranch(string educationEstablishmentCode,
            string branchOfStudyCode)
        {
            db.Execute("InsertEstablishmentBranch", "INSERT INTO Establishment_Branches "
                + "(education_establishment_code, branch_of_study_code) VALUES ('"
                + educationEstablishmentCode + "','" + branchOfStudyCode + "')");
        }

        public void DeleteEstablishmentBranch(string educationEstablishmentCode,
            string branchOfStudyCode)
        {
            db.Execute("DeleteEstablishmentBranch", "DELETE FROM Establishment_Branches "
                + "WHERE education_establishment_code = '" + educationEstablishmentCode
                + "' AND branch_of_study_code = '" + branchOfStudyCode + "'");
        }
    }
}