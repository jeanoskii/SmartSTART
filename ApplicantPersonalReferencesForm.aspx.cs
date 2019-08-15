using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPRES.BusinessLogicObjects;
using System.Data;

namespace CAPRES
{
    public partial class ApplicantPersonalReferencesForm : System.Web.UI.Page
    {
        #region ObjectInstantiation
        private AFEBLO afeBLO = new AFEBLO();
        private PersonalReferenceBLO personalReferenceBLO =
            new PersonalReferenceBLO();
        #endregion
        private int candidateId;
        private int afeId;
        private int personalReferenceId;
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
                lblStep.Text = "Step 7 of 8 - Personal References";
            }
            else
            {
                lblStep.Text = "Personal References";
                lblError.Text = "Error Text";
                gvPersonal.Rows[0].Visible = true;
                LinkButton btnAdd = gvPersonal.FooterRow.FindControl("btnAdd")
                    as LinkButton;
                btnAdd.Enabled = false;
                LinkButton btnEdit = gvPersonal.Rows[0].FindControl("btnEdit")
                    as LinkButton;
                btnEdit.Enabled = false;
                LinkButton btnDelete = gvPersonal.Rows[0].FindControl("btnDelete")
                    as LinkButton;
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
                    if (candidateId == 0)
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
            DataTable dt = personalReferenceBLO.SelectPersonalReferenceOfAFE(afeId);
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                gvPersonal.DataSource = dt;
                gvPersonal.DataBind();
                gvPersonal.Rows[0].Visible = false;
            }
            else
            {
                gvPersonal.DataSource = dt;
                gvPersonal.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (gvPersonal.Rows.Count > 4)
            {
                lblError.Text = "Maximum of three (3) references";
            }
            else
            {
                LinkButton btnAdd = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)btnAdd.NamingContainer;
                string lastName = (gvr.FindControl("txtLastName") as TextBox).Text.Trim();
                string firstName = (gvr.FindControl("txtFirstName") as TextBox).Text.Trim();
                string contactNumber = (gvr.FindControl("txtContactNumber") as TextBox).Text.Trim();
                string relationship = (gvr.FindControl("txtRelationship") as TextBox).Text.Trim();
                if (lastName.Length == 0 || firstName.Length == 0 || contactNumber.Length == 0
                    || relationship.Length == 0)
                {
                    lblError.Text = "Please fill out the form completely";
                }
                else
                {
                    personalReferenceBLO.InsertPersonalReference(afeId, lastName, firstName,
                        contactNumber, relationship);
                    DataBindItems();
                }
            }
        }

        protected void gvPersonal_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPersonal.EditIndex = -1;
            DataBindItems();
        }

        protected void gvPersonal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvPersonal.EditIndex == e.Row.RowIndex)
            {
                personalReferenceId = int.Parse(gvPersonal.DataKeys[e.Row.RowIndex]
                    .Value.ToString());
                DataTable dt = new DataTable();
                dt = personalReferenceBLO.SelectSpecificPersonalReference(personalReferenceId);
                TextBox txtEditLastName = (TextBox)e.Row.FindControl("txtEditLastName");
                txtEditLastName.Text = dt.Rows[0]["last_name"].ToString();
                TextBox txtEditFirstName = (TextBox)e.Row.FindControl("txtEditFirstName");
                txtEditFirstName.Text = dt.Rows[0]["first_name"].ToString();
                TextBox txtEditContactNumber = (TextBox)e.Row.FindControl("txtEditContactNumber");
                txtEditContactNumber.Text = dt.Rows[0]["contact_number"].ToString();
                TextBox txtEditRelationship = (TextBox)e.Row.FindControl("txtEditRelationship");
                txtEditRelationship.Text = dt.Rows[0]["relationship"].ToString();
            }
        }

        protected void gvPersonal_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            personalReferenceId = int.Parse(gvPersonal.DataKeys[e.RowIndex].Value.ToString());
            personalReferenceBLO.DeletePersonalReference(personalReferenceId);
            DataBindItems();
        }

        protected void gvPersonal_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPersonal.EditIndex = e.NewEditIndex;
            DataBindItems();
        }

        protected void gvPersonal_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            personalReferenceId = int.Parse(gvPersonal.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow gvr = gvPersonal.Rows[e.RowIndex] as GridViewRow;
            string newLastName = (gvr.FindControl("txtEditLastName") as TextBox).Text.Trim();
            string newFirstName = (gvr.FindControl("txtEditFirstName") as TextBox).Text.Trim();
            string newContactNumber = (gvr.FindControl("txtEditContactNumber") as TextBox).Text.Trim();
            string newRelationship = (gvr.FindControl("txtEditRelationship") as TextBox).Text.Trim();
            if (newLastName == "" || newFirstName == "" || newContactNumber == "" ||
                newRelationship == "")
            {
                lblError.Text = "Please fill out the form completely.";
            }
            else
            {
                personalReferenceBLO.UpdatePersonalReference(personalReferenceId, newLastName,
                    newFirstName, newContactNumber, newRelationship);
                gvPersonal.EditIndex = -1;
                DataBindItems();
            }
        }

        protected void gvPersonal_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells.RemoveAt(6);
                e.Row.Cells[5].ColumnSpan = 2;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells.RemoveAt(6);
                e.Row.Cells[5].ColumnSpan = 2;
            }
        }
    
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantProfessionalReferencesForm.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantReviewForm.aspx");
        }
    }
}