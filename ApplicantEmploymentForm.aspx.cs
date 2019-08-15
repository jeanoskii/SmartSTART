using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPRES.ValueObjects;
using CAPRES.BusinessLogicObjects;
using System.Data;

namespace CAPRES
{
    public partial class ApplicantEmploymentForm : System.Web.UI.Page
    {
        #region ObjectInstantiation
        private File3VO file3VO = new File3VO();
        private File3BLO file3BLO = new File3BLO();
        private SpecializationVO specializationVO = new SpecializationVO();
        private SpecializationBLO specializationBLO = new SpecializationBLO();
        private IndustryVO industryVO = new IndustryVO();
        private IndustryBLO industryBLO = new IndustryBLO();
        private ReasonVO reasonVO = new ReasonVO();
        private ReasonBLO reasonBLO = new ReasonBLO();
        #endregion
        private int candidateId;
        private string status;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            CheckApplicantLogin();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataBindItems();
            }
            if (candidateId > 0)
            {
                if (status.Equals("applicant") || status.Equals("pending"))
                {
                    lblStep.Text = "Step 4 of 8 - Employment History";
                }
                else if (status.Equals("preboarding"))
                {
                    lblStep.Text = "Step 3 of 6 - Employment History";
                }
            }
            else
            {
                lblStep.Text = "Employment History";
                lblError.Text = "Error Text";
                gvEmployers.Rows[0].Visible = true;
                LinkButton btnAdd = gvEmployers.FooterRow.FindControl("btnAdd")
                    as LinkButton;
                btnAdd.Enabled = false;
                LinkButton btnEdit = gvEmployers.Rows[0].FindControl("btnEdit")
                    as LinkButton;
                btnEdit.Enabled = false;
                LinkButton btnDelete = gvEmployers.Rows[0].FindControl("btnDelete")
                    as LinkButton;
                btnDelete.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Enabled = false;
            }
        }

        private void CheckApplicantLogin()
        {
            List<string> list;
            if (Session["Candidate"] != null || Request.Cookies["Candidate"] != null)
            {
                list = (List<string>)Session["Candidate"];
                status = list == null
                    ? Request.Cookies["Candidate"]["Status"] : list[1];
                if (status.Equals("preboarding"))
                {
                    candidateId = list == null
                        ? int.Parse(Request.Cookies["Candidate"]["ID"].ToString())
                        : int.Parse(list[0].ToString());
                    Label lblHeader = this.Master.FindControl("lblHeader") as Label;
                    lblHeader.Text = "PRE-EMPLOYMENT REQUIREMENTS";
                    Label lblUser = this.Master.FindControl("lblUser") as Label;
                    lblUser.Text = "Hello Candidate #" + candidateId;
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
                    lblHeader.Text = "PRE-EMPLOYMENT REQUIREMENTS";
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

        private void DataBindItems()
        {
            lblError.Text = string.Empty;
            DataTable dt = file3BLO.SelectFile3OfCandidate(candidateId);
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                gvEmployers.DataSource = dt;
                gvEmployers.DataBind();
                gvEmployers.Rows[0].Visible = false;
            }
            else
            {
                gvEmployers.DataSource = dt;
                gvEmployers.DataBind();
            }
            DropDownList ddlSpecialization = gvEmployers.FooterRow.FindControl("ddlSpecialization")
                as DropDownList;
            ddlSpecialization.DataSource = specializationBLO.SelectAllSpecialization();
            ddlSpecialization.DataValueField = "specialization_code";
            ddlSpecialization.DataTextField = "specialization";
            ddlSpecialization.DataBind();
            DropDownList ddlIndustry = gvEmployers.FooterRow.FindControl("ddlIndustry")
                as DropDownList;
            ddlIndustry.DataSource = industryBLO.SelectAllIndustry();
            ddlIndustry.DataValueField = "industry_code";
            ddlIndustry.DataTextField = "industry";
            ddlIndustry.DataBind();
            DropDownList ddlReason = gvEmployers.FooterRow.FindControl("ddlReason")
                as DropDownList;
            ddlReason.DataSource = reasonBLO.SelectAllReason();
            ddlReason.DataValueField = "reason_code";
            ddlReason.DataTextField = "reason";
            ddlReason.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (gvEmployers.Rows.Count > 4)
            {
                lblError.Text = "Maximum of three (3) employers.";
            }
            else
            {
                var addButton = (Control)sender;
                GridViewRow gvr = (GridViewRow)addButton.NamingContainer;
                string previousEmployer = (gvr.FindControl("txtPreviousEmployer") as TextBox).Text.Trim();
                string city = (gvr.FindControl("txtCity") as TextBox).Text.Trim();
                string positionHeld = (gvr.FindControl("txtPositionHeld") as TextBox).Text.Trim();
                string specialization = (gvr.FindControl("ddlSpecialization") as DropDownList).SelectedValue;
                string immediateSuperior = (gvr.FindControl("txtImmediateSuperior") as TextBox).Text.Trim();
                string previousSalary = (gvr.FindControl("txtPreviousSalary") as TextBox).Text.Trim();
                string industry = (gvr.FindControl("ddlIndustry") as DropDownList).SelectedValue;
                string reason = (gvr.FindControl("ddlReason") as DropDownList).SelectedValue;
                string startDate = (gvr.FindControl("txtStartDate") as TextBox).Text.Trim();
                string endDate = (gvr.FindControl("txtEndDate") as TextBox).Text.Trim();
                if (previousEmployer.Length == 0 || city.Length == 0 || positionHeld.Length == 0
                    || specialization.Length == 0 || immediateSuperior.Length == 0
                    || previousEmployer.Length == 0 || industry.Length == 0 || reason.Length == 0
                    || startDate.Length == 0 || endDate.Length == 0)
                {
                    lblError.Text = "Please fill out the form completely.";
                }
                else
                {
                    file3BLO.InsertFile3(candidateId, previousEmployer, city,
                        positionHeld, specialization, immediateSuperior,
                        previousSalary, industry, reason, startDate, endDate);
                    DataBindItems();
                }
            }
        }

        protected void gvEmployers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployers.EditIndex = -1;
            DataBindItems();
        }

        protected void gvEmployers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvEmployers.EditIndex == e.Row.RowIndex)
            {
                int file3Id = int.Parse(gvEmployers.DataKeys[e.Row.RowIndex].Value.ToString());
                DataTable dt = new DataTable();
                dt = file3BLO.SelectSpecificFile3(file3Id);
                TextBox txtEditPreviousSalary = (TextBox)e.Row.FindControl("txtEditPreviousSalary");
                txtEditPreviousSalary.Text = dt.Rows[0][7].ToString();
                DropDownList ddlEditSpecialization =
                    (DropDownList)e.Row.FindControl("ddlEditSpecialization");
                ddlEditSpecialization.DataSource = specializationBLO.SelectAllSpecialization();
                ddlEditSpecialization.DataValueField = "specialization_code";
                ddlEditSpecialization.DataTextField = "specialization";
                ddlEditSpecialization.DataBind();
                ddlEditSpecialization.SelectedValue = dt.Rows[0]["specialization_code"].ToString();
                DropDownList ddlEditIndustry =
                    (DropDownList)e.Row.FindControl("ddlEditIndustry");
                ddlEditIndustry.DataSource = industryBLO.SelectAllIndustry();
                ddlEditIndustry.DataValueField = "industry_code";
                ddlEditIndustry.DataTextField = "industry";
                ddlEditIndustry.DataBind();
                ddlEditIndustry.SelectedValue = dt.Rows[0]["industry_code"].ToString();
                DropDownList ddlEditReason =
                    (DropDownList)e.Row.FindControl("ddlEditReason");
                ddlEditReason.DataSource = reasonBLO.SelectAllReason();
                ddlEditReason.DataValueField = "reason_code";
                ddlEditReason.DataTextField = "reason";
                ddlEditReason.DataBind();
                ddlEditReason.SelectedValue = dt.Rows[0]["reason_code"].ToString();
            }
        }

        protected void gvEmployers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int file3Id = int.Parse(gvEmployers.DataKeys[e.RowIndex].Value.ToString());
            file3BLO.DeleteFile3(file3Id);
            gvEmployers.EditIndex = -1;
            DataBindItems();
        }

        protected void gvEmployers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int file3Id = int.Parse(gvEmployers.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow gvr = gvEmployers.Rows[e.RowIndex] as GridViewRow;
            string newPreviousEmployer = (gvr.FindControl("txtEditPreviousEmployer")
                as TextBox).Text.Trim();
            string newCity = (gvr.FindControl("txtEditCity") as TextBox).Text.Trim();
            string newPositionHeld = (gvr.FindControl("txtEditPositionHeld")
                as TextBox).Text.Trim();
            string newSpecialization = (gvr.FindControl("ddlEditSpecialization")
                as DropDownList).SelectedValue;
            string newImmediateSuperior = (gvr.FindControl("txtEditImmediateSuperior")
                as TextBox).Text.Trim();
            string newPreviousSalary = (gvr.FindControl("txtEditPreviousSalary")
                as TextBox).Text.Trim();
            string newIndustry = (gvr.FindControl("ddlEditIndustry")
                as DropDownList).SelectedValue;
            string newReason = (gvr.FindControl("ddlEditReason")
                as DropDownList).SelectedValue;
            string newStartDate = (gvr.FindControl("txtEditStartDate") as TextBox).Text.Trim();
            string newEndDate = (gvr.FindControl("txtEditEndDate") as TextBox).Text.Trim();
            if (newPreviousEmployer == "" || newCity == "" || newPositionHeld == "" ||
                newSpecialization == "" || newImmediateSuperior == "" ||
                newPreviousSalary == "" || newIndustry == "" || newReason == "" ||
                newStartDate == "" || newEndDate == "")
            {
                lblError.Text = "Please fill out the form completely.";
            }
            else
            {
                file3BLO.UpdateFile3(file3Id, candidateId, newPreviousEmployer, newCity,
                    newPositionHeld, newSpecialization, newImmediateSuperior, newPreviousSalary,
                    newIndustry, newReason, newStartDate, newEndDate);
                gvEmployers.EditIndex = -1;
                DataBindItems();
            }
        }

        protected void gvEmployers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployers.EditIndex = e.NewEditIndex;
            DataBindItems();
        }

        protected void gvEmployers_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells.RemoveAt(12);
                e.Row.Cells[11].ColumnSpan = 2;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells.RemoveAt(12);
                e.Row.Cells[11].ColumnSpan = 2;
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (status.Equals("preboarding"))
            {
                Response.Redirect("ApplicantEducationalForm.aspx");
            }
            else
            {
                Response.Redirect("ApplicantFamilyForm.aspx");
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (status.Equals("preboarding"))
            {
                Response.Redirect("ApplicantOrganizationalForm.aspx");
            }
            else
            {
                Response.Redirect("ApplicantOtherQualificationsForm.aspx");
            }
        }
    }
}