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
    public class EducationEstablishmentBLO
    {
        private EducationEstablishmentVO educationEstablishmentVO
            = new EducationEstablishmentVO();
        private EducationEstablishmentDAO educationEstablishmentDAO
            = new EducationEstablishmentDAO();

        public DataTable SelectAllEducationEstablishment()
        {
            return educationEstablishmentDAO.SelectAllEducationEstablishment();
        }

        public string ManageEducationEstablishmentsWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable educationEstablishmentsInTextFile = new DataTable();
                educationEstablishmentsInTextFile.Columns.Add("education_establishment_code");
                educationEstablishmentsInTextFile.Columns.Add("education_establishment");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = educationEstablishmentsInTextFile.NewRow();
                    dr.ItemArray = fields;
                    educationEstablishmentsInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable educationEstablishment = educationEstablishmentDAO.SelectEducationEstablishment(fields[0]);
                        if (educationEstablishment.Rows.Count == 0)
                        {
                            educationEstablishmentDAO.InsertEducationEstablishment(fields[0], fields[1]);
                        }
                        else
                        {
                            if (educationEstablishment.Rows[0][1].ToString() != fields[1])
                            {
                                educationEstablishmentDAO.UpdateEducationEstablishment(fields[0], fields[1]);
                            }
                        }
                        educationEstablishment.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Education Establishment Code]|[Education Establishment Value]";
                    }
                }
                tfp.Close();
                DataTable educationEstablishmentsInDatabase = educationEstablishmentDAO.SelectAllEducationEstablishment();
                IEnumerable<string> educationEstablishmentsNotInTextFile = educationEstablishmentsInDatabase.AsEnumerable().Select(r => r.Field<string>("education_establishment_code"))
                    .Except(educationEstablishmentsInTextFile.AsEnumerable().Select(r => r.Field<string>("education_establishment_code")));
                if (educationEstablishmentsNotInTextFile.Any())
                {
                    DataTable educationEstablishmentsToBeDeleted = (from row in educationEstablishmentsInDatabase.AsEnumerable()
                                                    join id in educationEstablishmentsNotInTextFile
                                                    on row.Field<string>("education_establishment_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in educationEstablishmentsToBeDeleted.Rows)
                    {
                        educationEstablishmentDAO.DeleteEducationEstablishment(dr[0].ToString());
                    }
                }
                return "Education Establishment data successfully edited.";
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