using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPRES.ValueObjects;
using CAPRES.BusinessLogicObjects;
using System.Diagnostics;
using System.Data;

namespace CAPRES
{
    public partial class RecruiterHome : System.Web.UI.Page
    {
        #region ObjectInstantiation
        CandidateVO candidateVO = new CandidateVO();
        CandidateBLO candidateBLO = new CandidateBLO();
        #endregion
        protected void Page_PreInit(object sender, EventArgs e)
        {
            CheckRecruiterLogin();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataBindItems();
            }
        }

        private void CheckRecruiterLogin()
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
                    hrEmail = list == null
                        ? Request.Cookies["HR"]["Email"] : list[0];
                    Label lblHeader = this.Master.FindControl("lblHeader") as Label;
                    lblHeader.Text = "RECRUITER HOME";
                    Label lblUser = this.Master.FindControl("lblUser") as Label;
                    lblUser.Text = hrEmail;
                }
                else if (type.Equals("preboarder"))
                {
                    Response.Redirect("PreboarderHome.aspx");
                }
                else if (type.Equals("datamanager"))
                {
                    Response.Redirect("DataManagerHome.aspx");
                }
                else if (type.Equals("admin"))
                {
                    this.MasterPageFile = "SmartStartAdmin.master";
                    hrEmail = list == null
                        ? Request.Cookies["HR"]["Email"] : list[0];
                    Label lblHeader = this.Master.Master.FindControl("lblHeader") as Label;
                    lblHeader.Text = "RECRUITER HOME";
                    Label lblUser = this.Master.Master.FindControl("lblUser") as Label;
                    lblUser.Text = hrEmail;
                }
            }
            else
            {
                Response.Redirect("index.aspx");
            }
        }

        private void DataBindItems()
        {
            try
            {
                lblMessage.Text = string.Empty;
                txtSearch.Text = string.Empty;
                txtFileUpload.Text = string.Empty;
                DataTable dt = candidateBLO.
                    SelectAllPendingApplicantPreboardingHired();
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    gvCandidates.DataSource = dt;
                    gvCandidates.DataBind();
                    gvCandidates.Rows[0].Visible = false;
                }
                else
                {
                    if (Session["gvC"] != null)
                    {
                        gvCandidates.DataSource = Session["gvC"];
                        gvCandidates.DataBind();
                    }
                    else
                    {
                        gvCandidates.DataSource = dt;
                        Session["gvC"] = gvCandidates.DataSource;
                        gvCandidates.DataBind();
                    }
                }
            }
            catch (Exception e1)
            {
                Debug.WriteLine("DataBindItems Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblMessage.Text = "Sorry, an error occured. Please try again.";
            }
        }

        private void DataBindItemsWithoutClear()
        {
            try
            {
                DataTable dt = candidateBLO.
                    SelectAllPendingApplicantPreboardingHired();
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    gvCandidates.DataSource = dt;
                    gvCandidates.DataBind();
                    gvCandidates.Rows[0].Visible = false;
                }
                else
                {
                    if (Session["gvC"] != null)
                    {
                        gvCandidates.DataSource = Session["gvC"];
                        gvCandidates.DataBind();
                    }
                    else
                    {
                        gvCandidates.DataSource = dt;
                        Session["gvC"] = gvCandidates.DataSource;
                        gvCandidates.DataBind();
                    }
                }
            }
            catch (Exception e1)
            {
                Debug.WriteLine("DataBindItems Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblMessage.Text = "Sorry, an error occured. Please try again.";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerms = txtSearch.Text.Trim().ToLower();
                int searchCandidateId = 0;
                if (searchTerms.Length > 0)
                {
                    if (searchTerms.Contains("@"))
                    {
                        gvCandidates.DataSource = candidateBLO.SelectCandidate(searchTerms);
                        Session["gvC"] = gvCandidates.DataSource;
                        gvCandidates.DataBind();
                        lblMessage.Text = string.Empty;
                    }
                    else if (searchTerms == "pending" || searchTerms == "applicant"
                        || searchTerms == "preboarding" || searchTerms == "hired")
                    {
                        if (searchTerms == "pending")
                        {
                            gvCandidates.DataSource = candidateBLO.SelectAllPending();
                            Session["gvC"] = gvCandidates.DataSource;
                            gvCandidates.DataBind();
                            lblMessage.Text = string.Empty;
                        }
                        else if (searchTerms == "applicant")
                        {
                            gvCandidates.DataSource = candidateBLO.SelectAllApplicant();
                            Session["gvC"] = gvCandidates.DataSource;
                            gvCandidates.DataBind();
                            lblMessage.Text = string.Empty;
                        }
                        else if (searchTerms == "preboarding")
                        {
                            gvCandidates.DataSource = candidateBLO.SelectAllPreboarding();
                            Session["gvC"] = gvCandidates.DataSource;
                            gvCandidates.DataBind();
                            lblMessage.Text = string.Empty;
                        }
                        else
                        {
                            gvCandidates.DataSource = candidateBLO.SelectAllHired();
                            Session["gvC"] = gvCandidates.DataSource;
                            gvCandidates.DataBind();
                            lblMessage.Text = string.Empty;
                        }
                    }
                    else if (int.TryParse(searchTerms, out searchCandidateId))
                    {
                        gvCandidates.DataSource = candidateBLO.SelectCandidate(searchCandidateId);
                        Session["gvC"] = gvCandidates.DataSource;
                        gvCandidates.DataBind();
                        lblMessage.Text = string.Empty;
                    }
                    else
                    {
                        lblMessage.Text = "Invalid search term. Search candidates using their candidate ID, "
                            + "email, or status.";
                    }
                }
                else
                {
                    lblMessage.Text = "Please input your search terms.";
                }
            }
            catch (Exception e1)
            {
                Debug.WriteLine("Search Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblMessage.Text = "Sorry, an error occured. Please try again.";
            }
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = candidateBLO.SelectAllPendingApplicantPreboardingHired();
            gvCandidates.DataSource = dt;
            Session["gvC"] = gvCandidates.DataSource;
            gvCandidates.DataBind();
            txtSearch.Text = string.Empty;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var registerButton = (Control)sender;
                GridViewRow gvr = (GridViewRow)registerButton.NamingContainer;
                int candidateId = int.Parse((gvr.FindControl("txtCandidateId") as TextBox).Text.Trim());
                string candidateEmail = (gvr.FindControl("txtCandidateEmail") as TextBox).Text.Trim();
                if (candidateId <= 0 || candidateEmail.Length == 0)
                {
                    lblMessage.Text = "Please fill out the forms completely.";
                }
                else
                {
                    if (candidateEmail.Contains("@") && candidateEmail.Contains("."))
                    {
                        candidateBLO.RegisterCandidate(candidateId, candidateEmail);
                        btnResetSearch_Click(sender, e);
                        lblMessage.Text = "Candidate Number " + candidateId +
                            " registered successfully.";
                    }
                    else
                    {
                        lblMessage.Text = "Please input a valid email address.";
                    }
                }
            }
            catch (Exception e1)
            {
                Debug.WriteLine("Register Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblMessage.Text = "Sorry, an error occured. Please try again.";
            }
        }

        protected void btnViewApplication_Click(object sender, EventArgs e)
        {
            try
            {
                var btnViewApplication = (Control)sender;
                GridViewRow gvr = (GridViewRow)btnViewApplication.NamingContainer;
                Label lblCandidateId = gvr.FindControl("lblCandidateId") as Label;
                Response.Redirect("RecruiterViewApplication.aspx?cid=" + lblCandidateId.Text);
            }
            catch (Exception e1)
            {
                Debug.WriteLine("View Application Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblMessage.Text = "Sorry, an error occured. Please try again.";
            }
        }

        protected void btnResendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                var btnResendEmail = (Control)sender;
                GridViewRow gvr = (GridViewRow)btnResendEmail.NamingContainer;
                Label lblCandidateId = gvr.FindControl("lblCandidateId") as Label;
                Label lblCandidateEmail = gvr.FindControl("lblCandidateEmail") as Label;
                string email = lblCandidateEmail.Text;
                candidateBLO.NewPassword(int.Parse(lblCandidateId.Text), email);
                DataBindItems();
                lblMessage.Text = "New password sent to " + email + ".";
            }
            catch (Exception e1)
            {
                Debug.WriteLine("Resend Email Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblMessage.Text = "Sorry, an error occured. Please try again.";
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuCandidates.HasFile)
                {
                    if (fuCandidates.FileName.Contains(".txt"))
                    {
                        lblMessage.Text =
                            candidateBLO.RegisterCandidatesFromTextFile(fuCandidates.FileContent);
                        DataBindItemsWithoutClear();
                    }
                    else
                    {
                        lblMessage.Text = "Please upload only text (.txt) files.";
                    }
                }
                else
                {
                    lblMessage.Text = "Please select a file to upload.";
                }
            }
            catch (Exception e1)
            {
                Debug.WriteLine("File Upload Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblMessage.Text = "Sorry, an error occured. Please try again.";
            }
        }

        protected void gvCandidates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCandidates.PageIndex = e.NewPageIndex;
            DataBindItems();
        }

        protected void gvCandidates_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = Session["gvC"] as DataTable;
            dt.DefaultView.Sort = e.SortExpression;
            gvCandidates.DataSource = dt;
            gvCandidates.DataBind();
        }

        protected void gvCandidates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = (e.Row.FindControl("lblCandidateStatus") as Label).Text;
                if (status == "pending")
                {
                    LinkButton btnView = (LinkButton)e.Row.FindControl("btnViewApplication");
                    btnView.Visible = false;
                }
            }
        } 
    }
}