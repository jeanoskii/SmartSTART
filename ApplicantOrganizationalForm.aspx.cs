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
    public partial class ApplicantOrganizationalForm : System.Web.UI.Page
    {
        #region ObjectInstantiation
        private File4VO file4VO = new File4VO();
        private File4BLO file4BLO = new File4BLO();
        private OrganizationTypeVO organizationTypeVO = new OrganizationTypeVO();
        private OrganizationTypeBLO organizationTypeBLO = new OrganizationTypeBLO();
        private PositionTypeVO positionTypeVO = new PositionTypeVO();
        private PositionTypeBLO positionTypeBLO = new PositionTypeBLO();
        #endregion
        private int candidateId;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            CheckApplicantLogin();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (candidateId > 0)
            {
                lblStep.Text = "Step 4 of 6 - Organizational Membership";
            }
            else
            {
                lblStep.Text = "Organizational Membership";
                lblError.Text = "Error Text";
                gvOrganizations.Rows[0].Visible = true;
                LinkButton btnAdd = gvOrganizations.FooterRow.FindControl("btnAdd")
                    as LinkButton;
                btnAdd.Enabled = false;
                LinkButton btnEdit = gvOrganizations.Rows[0].FindControl("btnEdit")
                    as LinkButton;
                btnEdit.Enabled = false;
                LinkButton btnDelete = gvOrganizations.Rows[0].FindControl("btnDelete")
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
            DataTable dt = file4BLO.SelectFile4OfCandidate(candidateId);
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                gvOrganizations.DataSource = dt;
                gvOrganizations.DataBind();
                gvOrganizations.Rows[0].Visible = false;
            }
            else
            {
                gvOrganizations.DataSource = dt;
                gvOrganizations.DataBind();
            }
            DropDownList ddlOrganizationType = gvOrganizations.FooterRow.FindControl("ddlOrganizationType")
                as DropDownList;
            ddlOrganizationType.DataSource = organizationTypeBLO.SelectAllOrganizationType();
            ddlOrganizationType.DataValueField = "organization_type_code";
            ddlOrganizationType.DataTextField = "organization_type";
            ddlOrganizationType.DataBind();
            DropDownList ddlPositionType = gvOrganizations.FooterRow.FindControl("ddlPositionType")
                as DropDownList;
            ddlPositionType.DataSource = positionTypeBLO.SelectAllPositionType();
            ddlPositionType.DataValueField = "position_type_code";
            ddlPositionType.DataTextField = "position_type";
            ddlPositionType.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            LinkButton btnAdd = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnAdd.NamingContainer;
            string organizationType = (gvr.FindControl("ddlOrganizationType")
                as DropDownList).SelectedValue;
            string organizationName = (gvr.FindControl("txtOrganizationName")
                as TextBox).Text.Trim();
            string startDate = (gvr.FindControl("txtStartDate")
                as TextBox).Text.Trim();
            string endDate = (gvr.FindControl("txtEndDate")
                as TextBox).Text.Trim();
            string positionType = (gvr.FindControl("ddlPositionType")
                as DropDownList).SelectedValue;
            if (endDate.Length == 0)
            {
                endDate = null;
            }
            if (organizationType == "0" || organizationName.Length == 0
                || startDate.Length == 0 || startDate.Length == 0
                || positionType == "0")
            {
                lblError.Text = "Please fill out the form completely.";
            }
            else
            {
                file4BLO.InsertFile4(candidateId, organizationType, organizationName,
                    startDate, endDate, positionType);
                DataBindItems();
            }
        }

        protected void gvOrganizations_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvOrganizations.EditIndex = e.NewEditIndex;
            DataBindItems();
        }

        protected void gvOrganizations_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOrganizations.EditIndex = -1;
            DataBindItems();
        }

        protected void gvOrganizations_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int file4Id = int.Parse(gvOrganizations.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow gvr = gvOrganizations.Rows[e.RowIndex] as GridViewRow;
            string newOrganizationType = (gvr.FindControl("ddlEditOrganizationType")
                as DropDownList).SelectedValue;
            string newOrganizationName = (gvr.FindControl("txtEditOrganizationName")
                as TextBox).Text.Trim();
            string newStartDate = (gvr.FindControl("txtEditStartDate") as TextBox).Text.Trim();
            string newEndDate = (gvr.FindControl("txtEditEndDate") as TextBox).Text.Trim();
            string newPositionType = (gvr.FindControl("ddlEditPositionType") as DropDownList).SelectedValue;
            if (newEndDate.Length == 0)
            {
                newEndDate = null;
            }
            if (newOrganizationType == "0" || newOrganizationName.Length == 0 || newStartDate.Length == 0
                || newPositionType == "0")
            {
                lblError.Text = "Please fill out the form completely.";
            }
            else
            {
                file4BLO.UpdateFile4(file4Id, candidateId, newOrganizationType, newOrganizationName,
                    newStartDate, newEndDate, newPositionType);
                gvOrganizations.EditIndex = -1;
                DataBindItems();
            }
        }

        protected void gvOrganizations_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int file4Id = int.Parse(gvOrganizations.DataKeys[e.RowIndex].Value.ToString());
            file4BLO.DeleteFile4(file4Id);
            gvOrganizations.EditIndex = -1;
            DataBindItems();
        }

        protected void gvOrganizations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvOrganizations.EditIndex == e.Row.RowIndex)
            {
                int file4Id = int.Parse(gvOrganizations.DataKeys[e.Row.RowIndex].Value.ToString());
                DataTable dt = new DataTable();
                dt = file4BLO.SelectSpecificFile4(file4Id);
                DropDownList ddlEditOrganizationType =
                    (DropDownList)e.Row.FindControl("ddlEditOrganizationType");
                ddlEditOrganizationType.DataSource = organizationTypeBLO.SelectAllOrganizationType();
                ddlEditOrganizationType.DataValueField = "organization_type_code";
                ddlEditOrganizationType.DataTextField = "organization_type";
                ddlEditOrganizationType.DataBind();
                ddlEditOrganizationType.SelectedValue = dt.Rows[0]["organization_type_code"].ToString();
                DropDownList ddlEditPositionType =
                    (DropDownList)e.Row.FindControl("ddlEditPositionType");
                ddlEditPositionType.DataSource = positionTypeBLO.SelectAllPositionType();
                ddlEditPositionType.DataValueField = "position_type_code";
                ddlEditPositionType.DataTextField = "position_type";
                ddlEditPositionType.DataBind();
                ddlEditPositionType.SelectedValue = dt.Rows[0]["position_type_code"].ToString();
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantEmploymentForm.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantFamilyForm.aspx");
        }

        protected void gvOrganizations_RowCreated(object sender, GridViewRowEventArgs e)
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
    }
}