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
    public class AffixBLO
    {
        private AffixVO affixVO = new AffixVO();
        private AffixDAO affixDAO = new AffixDAO();

        public DataTable SelectAllAffix()
        {
            return affixDAO.SelectAllAffix();
        }

        public string ManageAffixesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable affixesInTextFile = new DataTable();
                affixesInTextFile.Columns.Add("affix_code");
                affixesInTextFile.Columns.Add("affix");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = affixesInTextFile.NewRow();
                    dr.ItemArray = fields;
                    affixesInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable affix = affixDAO.SelectAffix(fields[0]);
                        if (affix.Rows.Count == 0)
                        {
                            affixDAO.InsertAffix(fields[0], fields[1]);
                        }
                        else
                        {
                            if (affix.Rows[0][1].ToString() != fields[1])
                            {
                                affixDAO.UpdateAffix(fields[0], fields[1]);
                            }
                        }
                        affix.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Affix Code]|[Affix Value]";
                    }
                }
                tfp.Close();
                DataTable affixesInDatabase = affixDAO.SelectAllAffix();
                IEnumerable<string> affixesNotInTextFile = affixesInDatabase.AsEnumerable().Select(r => r.Field<string>("affix_code"))
                    .Except(affixesInTextFile.AsEnumerable().Select(r => r.Field<string>("affix_code")));
                if (affixesNotInTextFile.Any())
                {
                    DataTable affixesToBeDeleted = (from row in affixesInDatabase.AsEnumerable()
                                                    join id in affixesNotInTextFile
                                                    on row.Field<string>("affix_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in affixesToBeDeleted.Rows)
                    {
                        affixDAO.DeleteAffix(dr[0].ToString());
                    }
                }
                return "Affix data successfully edited.";
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