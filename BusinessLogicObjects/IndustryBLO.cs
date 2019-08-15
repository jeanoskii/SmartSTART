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
    public class IndustryBLO
    {
        private IndustryVO industryVO = new IndustryVO();
        private IndustryDAO industryDAO = new IndustryDAO();

        public DataTable SelectAllIndustry()
        {
            return industryDAO.SelectAllIndustry();
        }

        public string ManageIndustriesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable industriesInTextFile = new DataTable();
                industriesInTextFile.Columns.Add("industry_code");
                industriesInTextFile.Columns.Add("industry");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = industriesInTextFile.NewRow();
                    dr.ItemArray = fields;
                    industriesInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable industry = industryDAO.SelectIndustry(fields[0]);
                        if (industry.Rows.Count == 0)
                        {
                            industryDAO.InsertIndustry(fields[0], fields[1]);
                        }
                        else
                        {
                            if (industry.Rows[0][1].ToString() != fields[1])
                            {
                                industryDAO.UpdateIndustry(fields[0], fields[1]);
                            }
                        }
                        industry.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Industry Code]|[Industry Value]";
                    }
                }
                tfp.Close();
                DataTable industriesInDatabase = industryDAO.SelectAllIndustry();
                IEnumerable<string> industriesNotInTextFile = industriesInDatabase.AsEnumerable().Select(r => r.Field<string>("industry_code"))
                    .Except(industriesInTextFile.AsEnumerable().Select(r => r.Field<string>("industry_code")));
                if (industriesNotInTextFile.Any())
                {
                    DataTable industriesToBeDeleted = (from row in industriesInDatabase.AsEnumerable()
                                                    join id in industriesNotInTextFile
                                                    on row.Field<string>("industry_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in industriesToBeDeleted.Rows)
                    {
                        industryDAO.DeleteIndustry(dr[0].ToString());
                    }
                }
                return "Industry data successfully edited.";
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