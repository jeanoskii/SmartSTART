using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.ValueObjects;
using CAPRES.DataAccessObjects;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;

namespace CAPRES.BusinessLogicObjects
{
    public class BranchOfStudyBLO
    {
        private BranchOfStudyVO branchOfStudyVO = new BranchOfStudyVO();
        private BranchOfStudyDAO branchOfStudyDAO = new BranchOfStudyDAO();

        public DataTable SelectBranchOfEstablishment(string educationEstablishmentCode)
        {
            return branchOfStudyDAO.SelectBranchOfEstablishment(educationEstablishmentCode);
        }

        public string ManageBranchesOfStudyWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable branchesOfStudyInTextFile = new DataTable();
                branchesOfStudyInTextFile.Columns.Add("branch_of_study_code");
                branchesOfStudyInTextFile.Columns.Add("branch_of_study");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = branchesOfStudyInTextFile.NewRow();
                    dr.ItemArray = fields;
                    branchesOfStudyInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable branchOfStudy = branchOfStudyDAO.SelectBranchOfStudy(fields[0]);
                        if (branchOfStudy.Rows.Count == 0)
                        {
                            branchOfStudyDAO.InsertBranchOfStudy(fields[0], fields[1]);
                        }
                        else
                        {
                            if (branchOfStudy.Rows[0][1].ToString() != fields[1])
                            {
                                branchOfStudyDAO.UpdateBranchOfStudy(fields[0], fields[1]);
                            }
                        }
                        branchOfStudy.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Branch of Study Code]|[Branch of Study Value]";
                    }
                }
                tfp.Close();
                DataTable branchesOfStudyInDatabase = branchOfStudyDAO.SelectAllBranchOfStudy();
                IEnumerable<string> branchesOfStudyNotInTextFile = branchesOfStudyInDatabase.AsEnumerable().Select(r => r.Field<string>("branch_of_study_code"))
                    .Except(branchesOfStudyInTextFile.AsEnumerable().Select(r => r.Field<string>("branch_of_study_code")));
                if (branchesOfStudyNotInTextFile.Any())
                {
                    DataTable branchesOfStudyToBeDeleted = (from row in branchesOfStudyInDatabase.AsEnumerable()
                                                    join id in branchesOfStudyNotInTextFile
                                                    on row.Field<string>("branch_of_study_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in branchesOfStudyToBeDeleted.Rows)
                    {
                        branchOfStudyDAO.DeleteBranchOfStudy(dr[0].ToString());
                    }
                }
                return "Branch of Study data successfully edited.";
            }
            catch (Exception e1)
            {
                Debug.WriteLine("Text File Parse Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                return "Sorry, an error occured. Please try again.";
            }
        }
    }
}