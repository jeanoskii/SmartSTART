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
    public partial class ApplicantFamilyForm : System.Web.UI.Page
    {
        #region ObjectInstantiation
        private File5VO file5VO = new File5VO();
        private File5BLO file5BLO = new File5BLO();
        private FamilyMemberVO familyMemberVO = new FamilyMemberVO();
        private FamilyMemberBLO familyMemberBLO = new FamilyMemberBLO();
        private GenderVO genderVO = new GenderVO();
        private GenderBLO genderBLO = new GenderBLO();
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
                    lblStep.Text = "Step 3 of 8 - Family Members";
                }
                else if (status.Equals("preboarding"))
                {
                    lblStep.Text = "Step 5 of 6 - Family Members";
                }
            }
            else
            {
                lblStep.Text = "Family Members";
                lblError.Text = "Error Text";
                gvDependents.Rows[0].Visible = true;
                LinkButton btnAdd = gvDependents.FooterRow.FindControl("btnAdd")
                    as LinkButton;
                btnAdd.Enabled = false;
                LinkButton btnEdit = gvDependents.Rows[0].FindControl("btnEdit")
                    as LinkButton;
                btnEdit.Enabled = false;
                LinkButton btnDelete = gvDependents.Rows[0].FindControl("btnDelete")
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

        private void DataBindItems()
        {
            lblError.Text = string.Empty;
            DataTable dt = file5BLO.SelectFile5OfCandidate(candidateId);
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                gvDependents.DataSource = dt;
                gvDependents.DataBind();
                gvDependents.Rows[0].Visible = false;
            }
            else
            {
                gvDependents.DataSource = dt;
                gvDependents.DataBind();
            }
            DropDownList ddlFamilyMember = gvDependents.FooterRow.FindControl("ddlFamilyMember")
                as DropDownList;
            ddlFamilyMember.DataSource = familyMemberBLO.SelectAllFamilyMember();
            ddlFamilyMember.DataValueField = "family_member_code";
            ddlFamilyMember.DataTextField = "family_member";
            ddlFamilyMember.DataBind();
            DropDownList ddlGender = gvDependents.FooterRow.FindControl("ddlGender")
                as DropDownList;
            ddlGender.DataSource = genderBLO.SelectAllGender();
            ddlGender.DataValueField = "gender_code";
            ddlGender.DataTextField = "gender";
            ddlGender.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            LinkButton btnAdd = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnAdd.NamingContainer;
            string dependentsName = (gvr.FindControl("txtDependentsName") as TextBox).Text.Trim();
            string familyMember = (gvr.FindControl("ddlFamilyMember") as DropDownList).SelectedValue;
            string gender = (gvr.FindControl("ddlGender") as DropDownList).SelectedValue;
            string birthdate = (gvr.FindControl("txtBirthdate") as TextBox).Text.Trim();
            string companyName = (gvr.FindControl("txtCompanyName") as TextBox).Text.Trim();
            string positionTitle = (gvr.FindControl("txtPositionTitle") as TextBox).Text.Trim();
            if (dependentsName.Length == 0 || familyMember == "0" || gender == "0"
                    || birthdate.Length == 0)
            {
                lblError.Text = "Please fill out the form completely.";
            }
            else
            {
                file5BLO.InsertFile5(candidateId, dependentsName, familyMember, gender, birthdate,
                    companyName, positionTitle);
                DataBindItems();
            }
        }

        protected void gvDependents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDependents.EditIndex = e.NewEditIndex;
            DataBindItems();
        }

        protected void gvDependents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDependents.EditIndex = -1;
            DataBindItems();
        }

        protected void gvDependents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int file5Id = int.Parse(gvDependents.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow gvr = gvDependents.Rows[e.RowIndex] as GridViewRow;
            string newDependentsName = (gvr.FindControl("txtEditDependentsName")
                as TextBox).Text.Trim();
            string newFamilyMember = (gvr.FindControl("ddlEditFamilyMember")
                as DropDownList).SelectedValue;
            string newGender = (gvr.FindControl("ddlEditGender") as DropDownList).SelectedValue;
            string newBirthdate = (gvr.FindControl("txtEditBirthdate") as TextBox).Text.Trim();
            string newCompanyName = (gvr.FindControl("txtEditCompanyName") as TextBox).Text.Trim();
            string newPositionTitle = (gvr.FindControl("txtEditPositionTitle") as TextBox).Text.Trim();
            if (newDependentsName.Length == 0 || newFamilyMember == "0" || newGender == "0" ||
                newBirthdate.Length == 0)
            {
                lblError.Text = "Please fill out the form completely.";
            }
            else
            {
                file5BLO.UpdateFile5(file5Id, candidateId, newDependentsName, newFamilyMember,
                    newGender, newBirthdate, newCompanyName, newPositionTitle);
                gvDependents.EditIndex = -1;
                DataBindItems();
            }
        }

        protected void gvDependents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int file5Id = int.Parse(gvDependents.DataKeys[e.RowIndex].Value.ToString());
            file5BLO.DeleteFile5(file5Id);
            gvDependents.EditIndex = -1;
            DataBindItems();
        }

        protected void gvDependents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvDependents.EditIndex == e.Row.RowIndex)
            {
                int file5Id = int.Parse(gvDependents.DataKeys[e.Row.RowIndex].Value.ToString());
                DataTable dt = new DataTable();
                dt = file5BLO.SelectSpecificFile5(file5Id);
                DropDownList ddlEditFamilyMember = (DropDownList)e.Row.FindControl("ddlEditFamilyMember");
                ddlEditFamilyMember.DataSource = familyMemberBLO.SelectAllFamilyMember();
                ddlEditFamilyMember.DataValueField = "family_member_code";
                ddlEditFamilyMember.DataTextField = "family_member";
                ddlEditFamilyMember.DataBind();
                ddlEditFamilyMember.SelectedValue = dt.Rows[0]["family_member_code"].ToString();
                DropDownList ddlEditGender = (DropDownList)e.Row.FindControl("ddlEditGender");
                ddlEditGender.DataSource = genderBLO.SelectAllGender(); ;
                ddlEditGender.DataValueField = "gender_code";
                ddlEditGender.DataTextField = "gender";
                ddlEditGender.DataBind();
                ddlEditGender.SelectedValue = dt.Rows[0]["gender_code"].ToString();
            }
        }

        protected void gvDependents_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells.RemoveAt(8);
                e.Row.Cells[7].ColumnSpan = 2;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells.RemoveAt(8);
                e.Row.Cells[7].ColumnSpan = 2;
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (status.Equals("preboarding"))
            {
                Response.Redirect("ApplicantOrganizationalForm.aspx");
            }
            else
            {
                Response.Redirect("ApplicantPersonalForm.aspx");
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (gvDependents.Rows[0].Visible == false)
            {
                lblError.Text = "Please provide at least one (1) family member.";
            }
            else
            {
                if (status.Equals("preboarding"))
                {
                    Response.Redirect("ApplicantReviewForm.aspx");
                }
                else
                {
                    Response.Redirect("ApplicantEmploymentForm.aspx");
                }
            }
        }
    }
}