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
    public class CountryBLO
    {
        private CountryVO countryVO = new CountryVO();
        private CountryDAO countryDAO = new CountryDAO();

        public DataTable SelectAllCountry()
        {
            return countryDAO.SelectAllCountry();
        }

        public string ManageCountriesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable countriesInTextFile = new DataTable();
                countriesInTextFile.Columns.Add("country_code");
                countriesInTextFile.Columns.Add("country");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = countriesInTextFile.NewRow();
                    dr.ItemArray = fields;
                    countriesInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable country = countryDAO.SelectCountry(fields[0]);
                        if (country.Rows.Count == 0)
                        {
                            countryDAO.InsertCountry(fields[0], fields[1]);
                        }
                        else
                        {
                            if (country.Rows[0][1].ToString() != fields[1])
                            {
                                countryDAO.UpdateCountry(fields[0], fields[1]);
                            }
                        }
                        country.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Country Code]|[Country Value]";
                    }
                }
                tfp.Close();
                DataTable countriesInDatabase = countryDAO.SelectAllCountry();
                IEnumerable<string> countriesNotInTextFile = countriesInDatabase.AsEnumerable().Select(r => r.Field<string>("country_code"))
                    .Except(countriesInTextFile.AsEnumerable().Select(r => r.Field<string>("country_code")));
                if (countriesNotInTextFile.Any())
                {
                    DataTable countriesToBeDeleted = (from row in countriesInDatabase.AsEnumerable()
                                                    join id in countriesNotInTextFile
                                                    on row.Field<string>("country_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in countriesToBeDeleted.Rows)
                    {
                        countryDAO.DeleteCountry(dr[0].ToString());
                    }
                }
                return "Country data successfully edited.";
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