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
    public partial class ApplicantEducationalForm : System.Web.UI.Page
    {
        #region ObjectInstantiation
        private File2VO file2VO = new File2VO();
        private File2BLO file2BLO = new File2BLO();
        private EducationEstablishmentVO educationEstablishmentVO
            = new EducationEstablishmentVO();
        private EducationEstablishmentBLO educationEstablishmentBLO
            = new EducationEstablishmentBLO();
        private SchoolVO schoolVO = new SchoolVO();
        private SchoolBLO schoolBLO = new SchoolBLO();
        private BranchOfStudyVO branchOfStudyVO = new BranchOfStudyVO();
        private BranchOfStudyBLO branchOfStudyBLO = new BranchOfStudyBLO();
        private CertificateVO certificateVO = new CertificateVO();
        private CertificateBLO certificateBLO = new CertificateBLO();
        #endregion
        private int candidateId;
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
                lblStep.Text = "Step 2 of 6 - Educational Background";
            }
            else
            {
                lblStep.Text = "Educational Background";
                lblError.Text = "Error Text";
                gvEducations.Rows[0].Visible = true;
                LinkButton btnAdd = gvEducations.FooterRow.FindControl("btnAdd")
                    as LinkButton;
                btnAdd.Enabled = false;
                LinkButton btnEdit = gvEducations.Rows[0].FindControl("btnEdit")
                    as LinkButton;
                btnEdit.Enabled = false;
                LinkButton btnDelete = gvEducations.Rows[0].FindControl("btnDelete")
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
                string status = list == null
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
                    Response.Redirect("ApplicantHome.aspx");
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
                    lblHeader.Text = "PRE-EMPLOYMENT REQUIREMENTS";
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
            DataTable dt = file2BLO.SelectFile2OfCandidate(candidateId);
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                gvEducations.DataSource = dt;
                gvEducations.DataBind();
                gvEducations.Rows[0].Visible = false;
            }
            else
            {
                gvEducations.DataSource = dt;
                gvEducations.DataBind();
            }
            DropDownList ddlEducationEstablishment = gvEducations.FooterRow.FindControl("ddlEducationEstablishment")
                as DropDownList;
            ddlEducationEstablishment.DataSource
                = educationEstablishmentBLO.SelectAllEducationEstablishment();
            ddlEducationEstablishment.DataValueField = "education_establishment_code";
            ddlEducationEstablishment.DataTextField = "education_establishment";
            ddlEducationEstablishment.DataBind();
            DropDownList ddlSchool = gvEducations.FooterRow.FindControl("ddlSchool")
                as DropDownList;
            ddlSchool.Enabled = false;
            TextBox txtSchoolOthers = gvEducations.FooterRow.FindControl("txtSchoolOthers")
                as TextBox;
            txtSchoolOthers.Enabled = false;
            TextBox txtStartDate = gvEducations.FooterRow.FindControl("txtStartDate")
                as TextBox;
            txtStartDate.Enabled = false;
            TextBox txtEndDate = gvEducations.FooterRow.FindControl("txtEndDate")
                as TextBox;
            txtEndDate.Enabled = false;
            DropDownList ddlBranchOfStudy1 = gvEducations.FooterRow.FindControl("ddlBranchOfStudy1")
                as DropDownList;
            ddlBranchOfStudy1.Enabled = false;
            DropDownList ddlCertificate = gvEducations.FooterRow.FindControl("ddlCertificate")
                as DropDownList;
            ddlCertificate.Enabled = false;
            TextBox txtCourseAppraisal = gvEducations.FooterRow.FindControl("txtCourseAppraisal")
                as TextBox;
            txtCourseAppraisal.Enabled = false;
            DropDownList ddlBranchOfStudy2 = gvEducations.FooterRow.FindControl("ddlBranchOfStudy2")
                as DropDownList;
            ddlBranchOfStudy2.Enabled = false;
        }

        protected void ddlEducationEstablishment_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            DropDownList ddlEducationEstablishment = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)ddlEducationEstablishment.NamingContainer;
            string educationEstablishmentCode = ddlEducationEstablishment.SelectedValue;
            DropDownList ddlSchool = gvr.FindControl("ddlSchool") as DropDownList;
            ddlSchool.Enabled = true;
            ddlSchool.DataSource
                = schoolBLO.SelectSchoolOfEstablishment(educationEstablishmentCode);
            ddlSchool.DataValueField = "school_code";
            ddlSchool.DataTextField = "school";
            ddlSchool.DataBind();
            TextBox txtSchoolOthers = gvEducations.FooterRow.FindControl("txtSchoolOthers")
                as TextBox;
            txtSchoolOthers.Enabled = true;
            TextBox txtStartDate = gvEducations.FooterRow.FindControl("txtStartDate")
                as TextBox;
            txtStartDate.Enabled = true;
            TextBox txtEndDate = gvEducations.FooterRow.FindControl("txtEndDate")
                as TextBox;
            txtEndDate.Enabled = true;
            DropDownList ddlBranchOfStudy1 = gvr.FindControl("ddlBranchOfStudy1") as DropDownList;
            ddlBranchOfStudy1.Enabled = true;
            ddlBranchOfStudy1.DataSource
                = branchOfStudyBLO.SelectBranchOfEstablishment(educationEstablishmentCode);
            ddlBranchOfStudy1.DataValueField = "branch_of_study_code";
            ddlBranchOfStudy1.DataTextField = "branch_of_study";
            ddlBranchOfStudy1.DataBind();
            DropDownList ddlBranchOfStudy2 = gvr.FindControl("ddlBranchOfStudy2") as DropDownList;
            ddlBranchOfStudy2.Enabled = true;
            ddlBranchOfStudy2.DataSource
                = branchOfStudyBLO.SelectBranchOfEstablishment(educationEstablishmentCode);
            ddlBranchOfStudy2.DataValueField = "branch_of_study_code";
            ddlBranchOfStudy2.DataTextField = "branch_of_study";
            ddlBranchOfStudy2.DataBind();
            TextBox txtCourseAppraisal = gvEducations.FooterRow.FindControl("txtCourseAppraisal")
                as TextBox;
            txtCourseAppraisal.Enabled = true;
            DropDownList ddlCertificate = gvr.FindControl("ddlCertificate") as DropDownList;
            ddlCertificate.Enabled = true;
            ddlCertificate.DataSource
                = certificateBLO.SelectCertificateOfEstablishment(educationEstablishmentCode);
            ddlCertificate.DataValueField = "certificate_code";
            ddlCertificate.DataTextField = "certificate";
            ddlCertificate.DataBind();
        }

        protected void ddlEditEducationEstablishment_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            DropDownList ddlEditEducationEstablishment = (DropDownList)sender;
            string educationEstablishmentCode = ddlEditEducationEstablishment.SelectedValue;
            GridViewRow gvr = (GridViewRow)ddlEditEducationEstablishment.NamingContainer;
            DropDownList ddlEditSchool = (DropDownList)gvr.FindControl("ddlEditSchool");
            ddlEditSchool.DataSource =
                schoolBLO.SelectSchoolOfEstablishment(educationEstablishmentCode);
            ddlEditSchool.DataValueField = "school_code";
            ddlEditSchool.DataTextField = "school";
            ddlEditSchool.DataBind();
            DropDownList ddlEditBranchOfStudy1 = (DropDownList)gvr.FindControl
                ("ddlEditBranchOfStudy1");
            ddlEditBranchOfStudy1.DataSource = branchOfStudyBLO.
                SelectBranchOfEstablishment(educationEstablishmentCode);
            ddlEditBranchOfStudy1.DataValueField = "branch_of_study_code";
            ddlEditBranchOfStudy1.DataTextField = "branch_of_study";
            ddlEditBranchOfStudy1.DataBind();
            DropDownList ddlEditCertificate = (DropDownList)gvr.FindControl
                ("ddlEditCertificate");
            ddlEditCertificate.DataSource = certificateBLO.
                SelectCertificateOfEstablishment(educationEstablishmentCode);
            ddlEditCertificate.DataValueField = "certificate_code";
            ddlEditCertificate.DataTextField = "certificate";
            ddlEditCertificate.DataBind();
            DropDownList ddlEditBranchOfStudy2 = (DropDownList)gvr.
                FindControl("ddlEditBranchOfStudy2");
            ddlEditBranchOfStudy2.DataSource = branchOfStudyBLO.
                SelectBranchOfEstablishment(educationEstablishmentCode);
            ddlEditBranchOfStudy2.DataValueField = "branch_of_study_code";
            ddlEditBranchOfStudy2.DataTextField = "branch_of_study";
            ddlEditBranchOfStudy2.DataBind();
        }

        protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            DropDownList ddlSchool = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)ddlSchool.NamingContainer;
            TextBox txtSchoolOthers = gvr.FindControl("txtSchoolOthers") as TextBox;
            if (ddlSchool.SelectedValue == "1000000000")
            {
                txtSchoolOthers.Enabled = true;
            }
            else
            {
                txtSchoolOthers.Enabled = false;
            }
        }
        
        protected void ddlEditSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            DropDownList ddlEditSchool = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)ddlEditSchool.NamingContainer;
            TextBox txtEditSchoolOthers = gvr.FindControl("txtEditSchoolOthers") as TextBox;
            if (ddlEditSchool.SelectedValue == "1000000000")
            {
                txtEditSchoolOthers.Enabled = true;
            }
            else
            {
                txtEditSchoolOthers.Enabled = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            LinkButton btnAdd = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnAdd.NamingContainer;
            string educationEstablishment = (gvr.FindControl("ddlEducationEstablishment") as DropDownList).SelectedValue;
            string school = (gvr.FindControl("ddlSchool") as DropDownList).SelectedValue;
            string schoolOthers = (gvr.FindControl("txtSchoolOthers") as TextBox).Text.Trim();
            string startDate = (gvr.FindControl("txtStartDate") as TextBox).Text.Trim();
            string endDate = (gvr.FindControl("txtEndDate") as TextBox).Text.Trim();
            string branchOfStudy1 = (gvr.FindControl("ddlBranchOfStudy1") as DropDownList).SelectedValue;
            string certificate = (gvr.FindControl("ddlCertificate") as DropDownList).SelectedValue;
            string courseAppaisal = (gvr.FindControl("txtCourseAppraisal") as TextBox).Text.Trim();
            string branchOfStudy2 = (gvr.FindControl("ddlBranchOfStudy2") as DropDownList).SelectedValue;
            if (branchOfStudy1 == "0")
            {
                branchOfStudy1 = null;
            }
            if (branchOfStudy2 == "0")
            {
                branchOfStudy2 = null;
            }
            if (school == "1000000000")
            {
                if (educationEstablishment == "0" || school == "0" || schoolOthers == "" ||
                    startDate == "" || endDate == "" || certificate == "0")
                {
                    lblError.Text = "Please fill out the form completely.";
                }
                else
                {
                    file2BLO.InsertFile2(candidateId, educationEstablishment, school, schoolOthers,
                        startDate, endDate, branchOfStudy1, certificate, courseAppaisal, branchOfStudy2);
                    DataBindItems();
                }
            }
            else
            {
                if (educationEstablishment == "0" || school == "0" ||
                    startDate == "" || endDate == "" || certificate == "0")
                {
                    lblError.Text = "Please fill out the form completely.";
                }
                else
                {
                    schoolOthers = null;
                    file2BLO.InsertFile2(candidateId, educationEstablishment, school, schoolOthers,
                        startDate, endDate, branchOfStudy1, certificate, courseAppaisal, branchOfStudy2);
                    DataBindItems();
                }
            }
        }

        protected void gvEducations_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEducations.EditIndex = -1;
            DataBindItems();
        }

        protected void gvEducations_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int file2Id = int.Parse(gvEducations.DataKeys[e.RowIndex].Value.ToString());
            file2BLO.DeleteFile2(file2Id);
            gvEducations.EditIndex = -1;
            DataBindItems();
        }

        protected void gvEducations_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEducations.EditIndex = e.NewEditIndex;
            DataBindItems();
        }

        protected void gvEducations_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int file2Id = int.Parse(gvEducations.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow gvr = gvEducations.Rows[e.RowIndex] as GridViewRow;
            string newEducationEstablishment = (gvr.FindControl("ddlEditEducationEstablishment")
                as DropDownList).SelectedValue;
            string newSchool = (gvr.FindControl("ddlEditSchool") as DropDownList).SelectedValue;
            string newSchoolOthers = (gvr.FindControl("txtEditSchoolOthers") as TextBox).Text.Trim();
            string newStartDate = (gvr.FindControl("txtEditStartDate") as TextBox).Text.Trim();
            string newEndDate = (gvr.FindControl("txtEditEndDate") as TextBox).Text.Trim();
            string newBranchOfStudy1 = (gvr.FindControl("ddlEditBranchOfStudy1") as DropDownList)
                .SelectedValue;
            string newCertificate = (gvr.FindControl("ddlEditCertificate") as DropDownList).SelectedValue;
            string newCourseAppraisal = (gvr.FindControl("txtEditCourseAppraisal") as TextBox)
                .Text.Trim();
            string newBranchOfStudy2 = (gvr.FindControl("ddlEditBranchOfStudy2") as DropDownList)
                .SelectedValue;
            if (newSchool == "1000000000")
            {
                if (newEducationEstablishment == "0" || newSchool == "0" || newSchoolOthers == "" ||
                    newStartDate == "" || newEndDate == "" || newCertificate == "0")
                {
                    lblError.Text = "Please fill out the form completely.";
                }
                else
                {
                    file2BLO.UpdateFile2(file2Id, candidateId, newEducationEstablishment, newSchool,
                        newSchoolOthers, newStartDate, newEndDate, newBranchOfStudy1, newCertificate,
                        newCourseAppraisal, newBranchOfStudy2);
                    gvEducations.EditIndex = -1;
                    DataBindItems();
                }
            }
            else
            {
                newSchoolOthers = string.Empty;
                if (newEducationEstablishment == "0" || newSchool == "0" ||
                    newStartDate == "" || newEndDate == "" || newCertificate == "0")
                {
                    lblError.Text = "Please fill out the form completely.";
                }
                else
                {
                    file2BLO.UpdateFile2(file2Id, candidateId, newEducationEstablishment, newSchool,
                        newSchoolOthers, newStartDate, newEndDate, newBranchOfStudy1, newCertificate,
                        newCourseAppraisal, newBranchOfStudy2);
                    gvEducations.EditIndex = -1;
                    DataBindItems();
                }
            }
        }

        protected void gvEducations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvEducations.EditIndex == e.Row.RowIndex)
            {
                int file2Id = int.Parse(gvEducations.DataKeys[e.Row.RowIndex].Value.ToString());
                DataTable dt = new DataTable();
                dt = file2BLO.SelectSpecificFile2(file2Id);
                DropDownList ddlEditEducationEstablishment =
                    (DropDownList)e.Row.FindControl("ddlEditEducationEstablishment");
                ddlEditEducationEstablishment.DataSource =
                    educationEstablishmentBLO.SelectAllEducationEstablishment();
                ddlEditEducationEstablishment.DataValueField = "education_establishment_code";
                ddlEditEducationEstablishment.DataTextField = "education_establishment";
                ddlEditEducationEstablishment.DataBind();
                ddlEditEducationEstablishment.SelectedValue =
                    dt.Rows[0]["education_establishment_code"].ToString();
                string educationEstablishmentCode = ddlEditEducationEstablishment.SelectedValue;
                DropDownList ddlEditSchool = (DropDownList)e.Row.FindControl("ddlEditSchool");
                ddlEditSchool.DataSource =
                    schoolBLO.SelectSchoolOfEstablishment(educationEstablishmentCode);
                ddlEditSchool.DataValueField = "school_code";
                ddlEditSchool.DataTextField = "school";
                ddlEditSchool.DataBind();
                ddlEditSchool.SelectedValue = dt.Rows[0]["school_code"].ToString();
                DropDownList ddlEditBranchOfStudy1 = (DropDownList)e.Row.FindControl
                    ("ddlEditBranchOfStudy1");
                ddlEditBranchOfStudy1.DataSource = branchOfStudyBLO.
                    SelectBranchOfEstablishment(educationEstablishmentCode);
                ddlEditBranchOfStudy1.DataValueField = "branch_of_study_code";
                ddlEditBranchOfStudy1.DataTextField = "branch_of_study";
                ddlEditBranchOfStudy1.DataBind();
                ddlEditBranchOfStudy1.SelectedValue = dt.Rows[0]["branch_of_study_1_code"].ToString();
                DropDownList ddlEditCertificate = (DropDownList)e.Row.FindControl
                    ("ddlEditCertificate");
                ddlEditCertificate.DataSource = certificateBLO.
                    SelectCertificateOfEstablishment(educationEstablishmentCode);
                ddlEditCertificate.DataValueField = "certificate_code";
                ddlEditCertificate.DataTextField = "certificate";
                ddlEditCertificate.DataBind();
                ddlEditCertificate.SelectedValue = dt.Rows[0]["certificate_code"].ToString();
                DropDownList ddlEditBranchOfStudy2 = (DropDownList)e.Row.
                    FindControl("ddlEditBranchOfStudy2");
                ddlEditBranchOfStudy2.DataSource = branchOfStudyBLO.
                    SelectBranchOfEstablishment(educationEstablishmentCode);
                ddlEditBranchOfStudy2.DataValueField = "branch_of_study_code";
                ddlEditBranchOfStudy2.DataTextField = "branch_of_study";
                ddlEditBranchOfStudy2.DataBind();
                ddlEditBranchOfStudy2.SelectedValue = dt.Rows[0]["branch_of_study_2_code"].ToString();
            }
        }

        protected void gvEducations_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells.RemoveAt(11);
                e.Row.Cells[10].ColumnSpan = 2;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells.RemoveAt(11);
                e.Row.Cells[10].ColumnSpan = 2;
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantPersonalForm.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (gvEducations.Rows.Count == 0)
            {
                lblError.Text = "Please provide at least one (1) education.";
            }
            else
            {
                Response.Redirect("ApplicantEmploymentForm.aspx");
            }
        }
    }
}