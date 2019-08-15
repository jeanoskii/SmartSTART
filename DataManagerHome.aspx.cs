using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPRES.ValueObjects;
using CAPRES.BusinessLogicObjects;
using System.Text;
using System.Data;
using System.IO;
using Ionic.Zip;
using System.Diagnostics;

namespace CAPRES
{
    public partial class DataManagerHome : System.Web.UI.Page
    {
        #region ObjectInstantiation
        CandidateBLO candidateBLO = new CandidateBLO();
        File1BLO file1BLO = new File1BLO();
        File2BLO file2BLO = new File2BLO();
        File3BLO file3BLO = new File3BLO();
        File4BLO file4BLO = new File4BLO();
        File5BLO file5BLO = new File5BLO();
        #endregion
        protected void Page_PreInit(object sender, EventArgs e)
        {
            CheckDataManagerLogin();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CheckDataManagerLogin()
        {
            List<string> list;
            if (Session["Candidate"] != null || Request.Cookies["Candidate"] != null)
            {
                list = (List<string>)Session["Candidate"];
                string status = list == null
                    ? Request.Cookies["Candidate"]["Status"] : list[1];
                if (status.Equals("preboarding"))
                {
                    Response.Redirect("PreboardingHome.aspx");
                }
                else if (status.Equals("hiring"))
                {
                    Response.Redirect("HiringHome.aspx");
                }
                else if (status.Equals("applicant") || status.Equals("pending"))
                {
                    Response.Redirect("ApplicantHome");
                }
            }
            else if (Session["HR"] != null || Request.Cookies["HR"] != null)
            {
                list = (List<string>)Session["HR"];
                string hrEmail;
                string type = list == null
                    ? Request.Cookies["HR"]["Type"] : list[1];
                if (type.Equals("recruiter"))
                {
                    Response.Redirect("RecruiterHome.aspx");
                }
                else if (type.Equals("preboarder"))
                {
                    Response.Redirect("PreboarderHome.aspx");
                }
                else if (type.Equals("datamanager"))
                {
                    hrEmail = list == null
                        ? Request.Cookies["HR"]["Email"] : list[0];
                    Label lblHeader = this.Master.FindControl("lblHeader") as Label;
                    lblHeader.Text = "DATA MANAGER HOME";
                    Label lblUser = this.Master.FindControl("lblUser") as Label;
                    lblUser.Text = hrEmail;
                    
                }
                else if (type.Equals("admin"))
                {
                    this.MasterPageFile = "SmartStartAdmin.master";
                    hrEmail = list == null
                        ? Request.Cookies["HR"]["Email"] : list[0];
                    Label lblHeader = this.Master.Master.FindControl("lblHeader") as Label;
                    lblHeader.Text = "DATA MANAGER HOME";
                    Label lblUser = this.Master.Master.FindControl("lblUser") as Label;
                    lblUser.Text = hrEmail;
                }
            }
            else
            {
                Response.Redirect("index.aspx");
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string applicationStart = txtApplicationDateStart.Text.Trim();
                string applicationEnd = txtApplicationDateEnd.Text.Trim();
                if (applicationStart.Length == 0 || applicationEnd.Length == 0)
                {
                    lblMessage.Text = "Please select a range.";
                }
                else
                {
                    DateTime startDate = DateTime.Parse(applicationStart);
                    DateTime endDate = DateTime.Parse(applicationEnd);
                    if (startDate > endDate || startDate == endDate)
                    {
                        lblMessage.Text = "Range is invalid.";
                    }
                    else
                    {
                        DataTable[] dtCSV =
                        {
                            file1BLO.SelectFile1ForCSV(applicationStart, applicationEnd),
                            file2BLO.SelectFile2ForCSV(applicationStart, applicationEnd),
                            file3BLO.SelectFile3ForCSV(applicationStart, applicationEnd),
                            file4BLO.SelectFile4ForCSV(applicationStart, applicationEnd),
                            file5BLO.SelectFile5ForCSV(applicationStart, applicationEnd)
                        };
                        StringBuilder sb = new StringBuilder();
                        string textFileNameTemplate = Server.MapPath(@"~\CSV") + @"\file";
                        Response.Clear();
                        Response.BufferOutput = false;
                        Response.ContentType = "application/zip";
                        Response.AppendHeader("content-disposition", "attachment;filename=CAPRES-" +
                            DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".zip");
                        using (ZipFile zip = new ZipFile())
                        {
                            for (int i = 0; i <= 4; i++)
                            {
                                DataTable dt = dtCSV[i];
                                if (dt != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        string[] fields = dr.ItemArray.Select(field => field.ToString()).ToArray();
                                        sb.AppendLine(string.Join("|", fields));
                                    }
                                    string textFileName = textFileNameTemplate + (i + 1) + ".txt";
                                    var textFile = new StreamWriter(textFileName);
                                    textFile.WriteLine(sb.ToString());
                                    textFile.Flush();
                                    textFile.Close();
                                    sb.Clear();
                                    zip.AddFile(textFileName, @"\");
                                }
                            }
                            zip.Save(Response.OutputStream);
                        }
                        /// deletes the text files
                        for (int i = 1; i <= 5; i++)
                        {
                            File.Delete(textFileNameTemplate + i + ".txt");
                        }
                        /// changes statuses of exported candidates
                        /* foreach (DataRow dr in dtCSV[1].Rows)
                        {
                            int candidateId = int.Parse(dr["candidate_id"].ToString());
                            candidateBLO.ExportedToCSV(candidateId);
                        } */
                        Response.Flush();
                        Response.End();

                        #region ExportToExcel
                        //ExcelPackage ep = new ExcelPackage();
                        //ExcelWorksheet file1 = ep.Workbook.Worksheets.Add("File1");
                        //file1.Cells["A1"].LoadFromDataTable(file1BLO.SelectAllFile1(), true);
                        //ExcelWorksheet file2 = ep.Workbook.Worksheets.Add("File2");
                        //file2.Cells["A1"].LoadFromDataTable(file2BLO.SelectAllFile2(), true);
                        //ExcelWorksheet file3 = ep.Workbook.Worksheets.Add("File3");
                        //file3.Cells["A1"].LoadFromDataTable(file3BLO.SelectAllFile3(), true);
                        //ExcelWorksheet file4 = ep.Workbook.Worksheets.Add("File4");
                        //file4.Cells["A1"].LoadFromDataTable(file4BLO.SelectAllFile4(), true);
                        //ExcelWorksheet file5 = ep.Workbook.Worksheets.Add("File5");
                        //file5.Cells["A1"].LoadFromDataTable(file5BLO.SelectAllFile5(), true);
                        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //Response.AddHeader("content-disposition", "attachment;  filename=CAPRES-"
                        //    + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xlsx");
                        //Response.BinaryWrite(ep.GetAsByteArray());
                        //Response.Flush();
                        //Response.End();
                        #endregion
                    }
                }
            }
            catch (Exception e1)
            {
                Debug.WriteLine("Get Connection String Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
            }
        }
    }
}