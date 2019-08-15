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
    public class OrganizationTypeBLO
    {
        private OrganizationTypeVO organizationTypeVO = new OrganizationTypeVO();
        private OrganizationTypeDAO organizationTypeDAO = new OrganizationTypeDAO();

        public DataTable SelectAllOrganizationType()
        {
            return organizationTypeDAO.SelectAllOrganizationType();
        }

        public string ManageOrganizationTypesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable organizationTypesInTextFile = new DataTable();
                organizationTypesInTextFile.Columns.Add("organization_type_code");
                organizationTypesInTextFile.Columns.Add("organization_type");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = organizationTypesInTextFile.NewRow();
                    dr.ItemArray = fields;
                    organizationTypesInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable organizationType = organizationTypeDAO.SelectOrganizationType(fields[0]);
                        if (organizationType.Rows.Count == 0)
                        {
                            organizationTypeDAO.InsertOrganizationType(fields[0], fields[1]);
                        }
                        else
                        {
                            if (organizationType.Rows[0][1].ToString() != fields[1])
                            {
                                organizationTypeDAO.UpdateOrganizationType(fields[0], fields[1]);
                            }
                        }
                        organizationType.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Organization Type Code]|[Organization Type Value]";
                    }
                }
                tfp.Close();
                DataTable organizationTypesInDatabase = organizationTypeDAO.SelectAllOrganizationType();
                IEnumerable<string> organizationTypesNotInTextFile = organizationTypesInDatabase.AsEnumerable().Select(r => r.Field<string>("organization_type_code"))
                    .Except(organizationTypesInTextFile.AsEnumerable().Select(r => r.Field<string>("organization_type_code")));
                if (organizationTypesNotInTextFile.Any())
                {
                    DataTable organizationTypesToBeDeleted = (from row in organizationTypesInDatabase.AsEnumerable()
                                                    join id in organizationTypesNotInTextFile
                                                    on row.Field<string>("organization_type_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in organizationTypesToBeDeleted.Rows)
                    {
                        organizationTypeDAO.DeleteOrganizationType(dr[0].ToString());
                    }
                }
                return "Organization Type data successfully edited.";
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