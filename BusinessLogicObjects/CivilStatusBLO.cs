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
    public class CivilStatusBLO
    {
        private CivilStatusVO civilStatusVO = new CivilStatusVO();
        private CivilStatusDAO civilStatusDAO = new CivilStatusDAO();

        public DataTable SelectAllCivilStatus()
        {
            return civilStatusDAO.SelectAllCivilStatus();
        }

        public string ManageCivilStatusesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable civilStatusInTextFile = new DataTable();
                civilStatusInTextFile.Columns.Add("civil_status_code");
                civilStatusInTextFile.Columns.Add("civil_status");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = civilStatusInTextFile.NewRow();
                    dr.ItemArray = fields;
                    civilStatusInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable civilStatus = civilStatusDAO.SelectCivilStatus(fields[0]);
                        if (civilStatus.Rows.Count == 0)
                        {
                            civilStatusDAO.InsertCivilStatus(fields[0], fields[1]);
                        }
                        else
                        {
                            if (civilStatus.Rows[0][1].ToString() != fields[1])
                            {
                                civilStatusDAO.UpdateCivilStatus(fields[0], fields[1]);
                            }
                        }
                        civilStatus.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Civil Status Code]|[Civil Status Value]";
                    }
                }
                tfp.Close();
                DataTable civilStatusesInDatabase = civilStatusDAO.SelectAllCivilStatus();
                IEnumerable<string> civilStatusesNotInTextFile = civilStatusesInDatabase.AsEnumerable().Select(r => r.Field<string>("civil_status_code"))
                    .Except(civilStatusInTextFile.AsEnumerable().Select(r => r.Field<string>("civil_status_code")));
                if (civilStatusesNotInTextFile.Any())
                {
                    DataTable affixesToBeDeleted = (from row in civilStatusesInDatabase.AsEnumerable()
                                                    join id in civilStatusesNotInTextFile
                                                    on row.Field<string>("civil_status_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in affixesToBeDeleted.Rows)
                    {
                        civilStatusDAO.DeleteCivilStatus(dr[0].ToString());
                    }
                }
                return "Civil Status data successfully edited.";
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