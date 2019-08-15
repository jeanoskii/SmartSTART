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
    public class PositionTypeBLO
    {
        private PositionTypeVO positionTypeVO = new PositionTypeVO();
        private PositionTypeDAO positionTypeDAO = new PositionTypeDAO();

        public DataTable SelectAllPositionType()
        {
            return positionTypeDAO.SelectAllPositionType();
        }

        public string ManagePositionTypesWithTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                DataTable positionTypesInTextFile = new DataTable();
                positionTypesInTextFile.Columns.Add("position_type_code");
                positionTypesInTextFile.Columns.Add("position_type");
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    DataRow dr = positionTypesInTextFile.NewRow();
                    dr.ItemArray = fields;
                    positionTypesInTextFile.Rows.Add(dr);
                    if (fields.Count() == 2)
                    {
                        DataTable positionType = positionTypeDAO.SelectPositionType(fields[0]);
                        if (positionType.Rows.Count == 0)
                        {
                            positionTypeDAO.InsertPositionType(fields[0], fields[1]);
                        }
                        else
                        {
                            if (positionType.Rows[0][1].ToString() != fields[1])
                            {
                                positionTypeDAO.UpdatePositionType(fields[0], fields[1]);
                            }
                        }
                        positionType.Clear();
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Position Type Code]|[Position Type Value]";
                    }
                }
                tfp.Close();
                DataTable positionTypesInDatabase = positionTypeDAO.SelectAllPositionType();
                IEnumerable<string> positionTypesNotInTextFile = positionTypesInDatabase.AsEnumerable().Select(r => r.Field<string>("position_type_code"))
                    .Except(positionTypesInTextFile.AsEnumerable().Select(r => r.Field<string>("position_type_code")));
                if (positionTypesNotInTextFile.Any())
                {
                    DataTable positionTypesToBeDeleted = (from row in positionTypesInDatabase.AsEnumerable()
                                                    join id in positionTypesNotInTextFile
                                                    on row.Field<string>("position_type_code") equals id
                                                    select row).CopyToDataTable();
                    foreach (DataRow dr in positionTypesToBeDeleted.Rows)
                    {
                        positionTypeDAO.DeletePositionType(dr[0].ToString());
                    }
                }
                return "Position Type data successfully edited.";
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