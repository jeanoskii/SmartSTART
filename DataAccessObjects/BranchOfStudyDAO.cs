using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CAPRES.DataAccessObjects
{
    public class BranchOfStudyDAO
    {
        private DB db = new DB();

        public DataTable SelectBranchOfEstablishment(string educationEstablishmentCode)
        {
            return db.GetData("SELECT Branches_of_Study.branch_of_study_code, "
            + "Branches_of_Study.branch_of_study FROM Branches_of_Study, "
            + "Establishment_Branches, Education_Establishments WHERE "
            + "Education_Establishments.education_establishment_code = '"
            + educationEstablishmentCode + "' AND Branches_of_Study.branch_of_study_code "
            + "= Establishment_Branches.branch_of_study_code AND "
            + "Education_Establishments.education_establishment_code "
            + "= Establishment_Branches.education_establishment_code ORDER BY "
            + "Branches_of_Study.branch_of_study_code");
        }

        public DataTable SelectAllBranchOfStudy()
        {
            return db.GetData("SELECT branch_of_study_code, branch_of_study FROM "
                + "Branches_of_Study");
        }

        public DataTable SelectBranchOfStudy(string branchOfStudyCode)
        {
            return db.GetData("SELECT branch_of_study_code, branch_of_study FROM "
                + "Branches_of_Study WHERE branch_of_study_code = '"
                + branchOfStudyCode + "'");
        }

        public void InsertBranchOfStudy(string branchOfStudyCode, string branchOfStudy)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@branch_of_study_code", branchOfStudyCode);
            parameters[1] = new SqlParameter("@branch_of_study", branchOfStudy);
            db.Execute("InsertBranchOfStudy", "INSERT INTO Branches_of_Study (branch_of_study_code, "
                + "branch_of_study) VALUES (@branch_of_study_code, @branch_of_study)", parameters);
        }

        public void UpdateBranchOfStudy(string branchOfStudyCode, string branchOfStudy)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@branch_of_study_code", branchOfStudyCode);
            parameters[1] = new SqlParameter("@branch_of_study", branchOfStudy);
            db.Execute("UpdateBranchOfStudy", "UPDATE Branches_of_Study SET branch_of_study = "
                + "@branch_of_study WHERE branch_of_study_code = @branch_of_study_code", parameters);
        }

        public void DeleteBranchOfStudy(string branchOfStudyCode)
        {
            db.Execute("DeleteBranchOfStudy", "DELETE FROM Branches_of_Study WHERE "
                + "branch_of_study_code = '" + branchOfStudyCode + "'");
        }
    }
}