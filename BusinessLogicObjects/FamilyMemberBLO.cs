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
    public class FamilyMemberBLO
    {
        private FamilyMemberVO familyMemberVO = new FamilyMemberVO();
        private FamilyMemberDAO familyMemberDAO = new FamilyMemberDAO();

        public DataTable SelectAllFamilyMember()
        {
            return familyMemberDAO.SelectAllFamilyMember();
        }

        public string ManageFamilyMembersWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable familyMembersInTextFile = new DataTable();
                familyMembersInTextFile.Columns.Add("family_member_code");
                familyMembersInTextFile.Columns.Add("family_member");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = familyMembersInTextFile.NewRow();
                    dr.ItemArray = fields;
                    familyMembersInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable familyMember = familyMemberDAO.SelectFamilyMember(fields[0]);
                        if (familyMember.Rows.Count == 0)
                        {
                            familyMemberDAO.InsertFamilyMember(fields[0], fields[1]);
                        }
                        else
                        {
                            if (familyMember.Rows[0][1].ToString() != fields[1])
                            {
                                familyMemberDAO.UpdateFamilyMember(fields[0], fields[1]);
                            }
                        }
                        familyMember.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Family Member Code]|[Family Member Value]";
                    }
                }
                tfp.Close();
                DataTable familyMembersInDatabase = familyMemberDAO.SelectAllFamilyMember();
                IEnumerable<string> familyMembersNotInTextFile = familyMembersInDatabase.AsEnumerable().Select(r => r.Field<string>("family_member_code"))
                    .Except(familyMembersInTextFile.AsEnumerable().Select(r => r.Field<string>("family_member_code")));
                if (familyMembersNotInTextFile.Any())
                {
                    DataTable familyMembersToBeDeleted = (from row in familyMembersInDatabase.AsEnumerable()
                                                    join id in familyMembersNotInTextFile
                                                    on row.Field<string>("family_member_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in familyMembersToBeDeleted.Rows)
                    {
                        familyMemberDAO.DeleteFamilyMember(dr[0].ToString());
                    }
                }
                return "Family Member data successfully edited.";
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