using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPRES.BusinessLogicObjects;
using CAPRES.ValueObjects;

namespace CAPRES
{
    public partial class ApplicantAFE : System.Web.UI.Page
    {
        private AFEBLO afeBLO = new AFEBLO();
        private AFEVO afeVO = new AFEVO();
        private int candidateId;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            CheckApplicantLogin();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (candidateId > 0)
            {
                lblStep.Text = "Step 1 of 8 - Application Details";
            }
            else
            {
                lblStep.Text = "Application Details";
                btnNext.Enabled = false;
                btnHome.Enabled = false;
            }
            if (!Page.IsPostBack)
            {
                FillControlsWithData();
            }
        }

        private void CheckApplicantLogin()
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
                    candidateId = list == null
                        ? int.Parse(Request.Cookies["Candidate"]["ID"].ToString())
                        : int.Parse(list[0].ToString());
                    Label lblHeader = this.Master.FindControl("lblHeader") as Label;
                    lblHeader.Text = "APPLICATION FOR EMPLOYMENT";
                    Label lblUser = this.Master.FindControl("lblUser") as Label;
                    lblUser.Text = "Hello Candidate #" + candidateId;
                }
            }
            else if (Session["HR"] != null || Request.Cookies["HR"] != null)
            {
                list = (List<string>)Session["HR"];
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
                    Response.Redirect("DataManagerHome.aspx");
                }
                else if (type.Equals("admin"))
                {
                    this.MasterPageFile = "SmartStartAdmin.master";
                    string hrEmail = list == null
                        ? Request.Cookies["HR"]["Email"] : list[0];
                    Label lblHeader = this.Master.Master.FindControl("lblHeader") as Label;
                    lblHeader.Text = "APPLICATION FOR EMPLOYMENT";
                    Label lblUser = this.Master.Master.FindControl("lblUser") as Label;
                    lblUser.Text = hrEmail;
                }
            }
            else
            {
                Response.Redirect("index.aspx");
            }
        }

        private void FillControlsWithData()
        {
            if (candidateId > 0 || candidateId != null)
            {
                afeVO = afeBLO.SelectAFEOfCandidate(candidateId);
                if (afeVO != null)
                {
                    hidAFEId.Value = afeVO.AfeId.ToString();
                    txtApplicationDate.Text =
                        DateTime.Parse(afeVO.ApplicationDate).ToString("yyyy-MM-dd");
                    txtPositionAppliedFor.Text = afeVO.PositionAppliedFor;
                    txtInterviewerName.Text = afeVO.InterviewerName;
                }
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantHome.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string applicationDate = txtApplicationDate.Text.Trim();
            string positionAppliedFor = txtPositionAppliedFor.Text.Trim();
            string interviewerName = txtInterviewerName.Text.Trim();
            if (hidAFEId.Value == "")
            {
                afeBLO.InsertAFE(candidateId, applicationDate, positionAppliedFor,
                    interviewerName);
                Response.Redirect("ApplicantPersonalForm.aspx");
            }
            else
            {
                afeBLO.UpdateAFE(int.Parse(hidAFEId.Value), applicationDate, positionAppliedFor,
                    interviewerName);
                Response.Redirect("ApplicantPersonalForm.aspx");
            }
        }
    }
}