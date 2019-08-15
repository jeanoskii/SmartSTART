using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CAPRES.BusinessLogicObjects;
using CAPRES.ValueObjects;

namespace CAPRES
{
    public partial class ApplicantProfessionalReferencesForm : System.Web.UI.Page
    {
        #region ObjectInstantiation
        private AFEBLO afeBLO = new AFEBLO();
        private ProfessionalReferenceBLO professionalReferenceBLO =
            new ProfessionalReferenceBLO();
        #endregion
        private int candidateId;
        private int afeId;
        private string status;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            CheckApplicantLogin();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (candidateId > 0)
            {
                lblStep.Text = "Step 6 of 8 - Professional References";
            }
            else
            {
                lblStep.Text = "Professional References";
                lblError.Text = "Error Text";
                gvProfessional.Rows[0].Visible = true;
                LinkButton btnAdd = gvProfessional.FooterRow.FindControl("btnAdd")
                    as LinkButton;
                btnAdd.Enabled = false;
                LinkButton btnEdit = gvProfessional.Rows[0].FindControl("btnEdit")
                    as LinkButton;
                btnEdit.Enabled = false;
                LinkButton btnDelete = gvProfessional.Rows[0].FindControl("btnDelete")
                    as LinkButton;
                btnDelete.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Enabled = false;
            }
            if (!Page.IsPostBack)
            {
                DataBindItems();
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
                    Response.Redirect("ApplicantHome.aspx");
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
                    afeId = afeBLO.SelectAFEIDOfCandidate(candidateId);
                    if (afeId == 0)
                    {
                        Response.Redirect("ApplicantAFE.aspx");
                    }
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
            DataTable dt = professionalReferenceBLO.SelectProfessionalReferenceOfAFE(afeId);
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                gvProfessional.DataSource = dt;
                gvProfessional.DataBind();
                gvProfessional.Rows[0].Visible = false;
            }
            else
            {
                gvProfessional.DataSource = dt;
                gvProfessional.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (gvProfessional.Rows.Count > 4)
            {
                lblError.Text = "Maximum of three (3) references.";
            }
            else
            {
                LinkButton btnAdd = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)btnAdd.NamingContainer;
                string lastName = (gvr.FindControl("txtLastName") as TextBox).Text.Trim();
                string firstName = (gvr.FindControl("txtFirstName") as TextBox).Text.Trim();
                string contactNumber = (gvr.FindControl("txtContactNumber") as TextBox).Text.Trim();
                string companyName = (gvr.FindControl("txtCompanyName") as TextBox).Text.Trim();
                string positionTitle = (gvr.FindControl("txtPositionTitle") as TextBox).Text.Trim();
                if (lastName.Length == 0 || firstName.Length == 0 || contactNumber.Length == 0
                    || companyName.Length == 0 || positionTitle.Length == 0)
                {
                    lblError.Text = "Please fill out the form completely.";
                }
                else
                {
                    professionalReferenceBLO.InsertProfessionalReference(afeId, lastName, firstName,
                        contactNumber, companyName, positionTitle);
                    DataBindItems();
                }
            }
        }

        protected void gvProfessional_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProfessional.EditIndex = -1;
            DataBindItems();
        }

        protected void gvProfessional_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvProfessional.EditIndex == e.Row.RowIndex)
            {
                int professionalReferenceId = int.Parse(
                    gvProfessional.DataKeys[e.Row.RowIndex].Value.ToString());
                DataTable dt = new DataTable();
                dt = professionalReferenceBLO.SelectSpecificProfessionalReference(professionalReferenceId);
                TextBox txtEditLastName = (TextBox)e.Row.FindControl("txtEditLastName");
                txtEditLastName.Text = dt.Rows[0]["last_name"].ToString();
                TextBox txtEditFirstName = (TextBox)e.Row.FindControl("txtEditFirstName");
                txtEditFirstName.Text = dt.Rows[0]["first_name"].ToString();
                TextBox txtEditContactNumber = (TextBox)e.Row.FindControl("txtEditContactNumber");
                txtEditContactNumber.Text = dt.Rows[0]["contact_number"].ToString();
                TextBox txtEditCompanyName = (TextBox)e.Row.FindControl("txtEditCompanyName");
                txtEditCompanyName.Text = dt.Rows[0]["company_name"].ToString();
                TextBox txtEditPositionTitle = (TextBox)e.Row.FindControl("txtEditPositionTitle");
                txtEditPositionTitle.Text = dt.Rows[0]["position_title"].ToString();
            }
        }

        protected void gvProfessional_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int professionalReferenceId =
                int.Parse(gvProfessional.DataKeys[e.RowIndex].Value.ToString());
            professionalReferenceBLO.DeleteProfessionalReference(professionalReferenceId);
            gvProfessional.EditIndex = -1;
            DataBindItems();
        }

        protected void gvProfessional_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProfessional.EditIndex = e.NewEditIndex;
            DataBindItems();
        }

        protected void gvProfessional_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int professionalReferenceId = int.Parse(gvProfessional.DataKeys[e.RowIndex].
                Value.ToString());
            GridViewRow gvr = gvProfessional.Rows[e.RowIndex] as GridViewRow;
            string newLastName = (gvr.FindControl("txtEditLastName") as TextBox).Text.Trim();
            string newFirstname = (gvr.FindControl("txtEditFirstName") as TextBox).Text.Trim();
            string newContactNumber = (gvr.FindControl("txtEditContactNumber") as TextBox).Text.Trim();
            string newCompanyName = (gvr.FindControl("txtEditCompanyName") as TextBox).Text.Trim();
            string newPositionTitle = (gvr.FindControl("txtEditPositionTitle") as TextBox).Text.Trim();
            if (newLastName == "" || newFirstname == "" || newContactNumber == "" ||
                newCompanyName == "" || newPositionTitle == "")
            {
                lblError.Text = "Please fill out the form completely.";
            }
            else
            {
                professionalReferenceBLO.UpdateProfessionalReference(professionalReferenceId,
                    newLastName, newFirstname, newContactNumber, newCompanyName, newPositionTitle);
                gvProfessional.EditIndex = -1;
                DataBindItems();
            }
        }

        protected void gvProfessional_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells.RemoveAt(7);
                e.Row.Cells[6].ColumnSpan = 2;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells.RemoveAt(7);
                e.Row.Cells[6].ColumnSpan = 2;
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantOtherQualificationsForm.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantPersonalReferencesForm.aspx");
        }
    }
}