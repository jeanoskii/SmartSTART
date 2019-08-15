using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAPRES.DataAccessObjects;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Diagnostics;

namespace CAPRES.BusinessLogicObjects
{
    public class EstablishmentBranchBLO
    {
        private EstablishmentBranchDAO establishmentBranchDAO
            = new EstablishmentBranchDAO();

        public string ManageEstablishmentBranchesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable establishmentBranchesInTextFile = new DataTable();
                establishmentBranchesInTextFile.Columns.Add("education_establishment_code");
                establishmentBranchesInTextFile.Columns.Add("branch_of_study_code");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    if (fields.Count() == 2)
                    {
                        DataRow dr = establishmentBranchesInTextFile.NewRow();
                        dr.ItemArray = fields;
                        establishmentBranchesInTextFile.Rows.Add(dr);
                        DataTable establishmentBranch
                            = establishmentBranchDAO.SelectEstablishmentBranch(fields[0], fields[1]);
                        if (establishmentBranch.Rows.Count == 0)
                        {
                            establishmentBranchDAO.InsertEstablishmentBranch(fields[0], fields[1]);
                        }
                        establishmentBranch.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Education Establishment Code]|[Branch of Study Code]";
                    }
                }
                tfp.Close();
                DataTable establishmentBranchesInDatabase
                    = establishmentBranchDAO.SelectAllEstablishmentBranch();
                IEnumerable<DataRow> dt3 = (from r in establishmentBranchesInDatabase.AsEnumerable()
                                            where !establishmentBranchesInTextFile.AsEnumerable().Any(r2 => r["education_establishment_code"].ToString().Trim().ToLower() == r2["education_establishment_code"].ToString().Trim().ToLower()
                                                && r["branch_of_study_code"].ToString().Trim().ToLower() == r2["branch_of_study_code"].ToString().Trim())
                                            select r);
                if (dt3.Any())
                {
                    DataTable establishmentCertificatesToBeDeleted = dt3.CopyToDataTable();
                    foreach (DataRow dr in establishmentCertificatesToBeDeleted.Rows)
                    {
                        establishmentBranchDAO.DeleteEstablishmentBranch(dr[0].ToString(), dr[1].ToString());
                    }
                }
                return "Establishment Branch data successfully edited.";
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