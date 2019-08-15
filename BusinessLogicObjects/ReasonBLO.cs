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
    public class ReasonBLO
    {
        private ReasonVO reasonVO = new ReasonVO();
        private ReasonDAO reasonDAO = new ReasonDAO();

        public DataTable SelectAllReason()
        {
            return reasonDAO.SelectAllReason();
        }

        public string ManageReasonsWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable reasonsInTextFile = new DataTable();
                reasonsInTextFile.Columns.Add("reason_code");
                reasonsInTextFile.Columns.Add("reason");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = reasonsInTextFile.NewRow();
                    dr.ItemArray = fields;
                    reasonsInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable reason = reasonDAO.SelectReason(fields[0]);
                        if (reason.Rows.Count == 0)
                        {
                            reasonDAO.InsertReason(fields[0], fields[1]);
                        }
                        else
                        {
                            if (reason.Rows[0][1].ToString() != fields[1])
                            {
                                reasonDAO.UpdateReason(fields[0], fields[1]);
                            }
                        }
                        reason.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Reason Code]|[Reason Value]";
                    }
                }
                tfp.Close();
                DataTable reasonsInDatabase = reasonDAO.SelectAllReason();
                IEnumerable<string> reasonsNotInTextFile = reasonsInDatabase.AsEnumerable().Select(r => r.Field<string>("reason_code"))
                    .Except(reasonsInTextFile.AsEnumerable().Select(r => r.Field<string>("reason_code")));
                if (reasonsNotInTextFile.Any())
                {
                    DataTable reasonsToBeDeleted = (from row in reasonsInDatabase.AsEnumerable()
                                                    join id in reasonsNotInTextFile
                                                    on row.Field<string>("reason_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in reasonsToBeDeleted.Rows)
                    {
                        reasonDAO.DeleteReason(dr[0].ToString());
                    }
                }
                return "Reason data successfully edited.";
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