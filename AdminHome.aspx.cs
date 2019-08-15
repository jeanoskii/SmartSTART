using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPRES.ValueObjects;
using CAPRES.BusinessLogicObjects;
using System.Data;
using System.Diagnostics;

namespace CAPRES
{
    public partial class AdminHome : System.Web.UI.Page
    {
        #region ObjectInstantiation
        HRVO hrVO = new HRVO();
        HRBLO hrBLO = new HRBLO();
        AffixBLO affixBLO = new AffixBLO();
        BloodTypeBLO bloodTypeBLO = new BloodTypeBLO();
        CertificateBLO certificateBLO = new CertificateBLO();
        CivilStatusBLO civilStatusBLO = new CivilStatusBLO();
        FamilyMemberBLO familyMemberBLO = new FamilyMemberBLO();
        GenderBLO genderBLO = new GenderBLO();
        IndustryBLO industryBLO = new IndustryBLO();
        LicenseBLO licenseBLO = new LicenseBLO();
        OrganizationTypeBLO organizationTypeBLO = new OrganizationTypeBLO();
        PositionTypeBLO positionTypeBLO = new PositionTypeBLO();
        ReasonBLO reasonBLO = new ReasonBLO();
        NationalityBLO nationalityBLO = new NationalityBLO();
        ReligionBLO religionBLO = new ReligionBLO();
        SpecializationBLO specializationBLO = new SpecializationBLO();
        CountryBLO countryBLO = new CountryBLO();
        EducationEstablishmentBLO educationEstablishmentBLO = new EducationEstablishmentBLO();
        BranchOfStudyBLO branchOfStudyBLO = new BranchOfStudyBLO();
        SchoolBLO schoolBLO = new SchoolBLO();
        EstablishmentCertificateBLO establishmentCertificateBLO
            = new EstablishmentCertificateBLO();
        EstablishmentBranchBLO establishmentBranchBLO = new EstablishmentBranchBLO();
        EstablishmentSchoolBLO establishmentSchoolBLO = new EstablishmentSchoolBLO();
        #endregion
        private string hrEmail;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            CheckAdminLogin();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataBindItems();
            }
        }

        private void CheckAdminLogin()
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
                    hrEmail = list == null
                        ? Request.Cookies["HR"]["Email"] : list[0];
                    Label lblHeader = this.Master.Master.FindControl("lblHeader") as Label;
                    lblHeader.Text = "ADMINISTRATOR PANEL";
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
                DataTable dt = hrBLO.SelectAll();
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    gvHR.DataSource = dt;
                    gvHR.DataBind();
                    gvHR.Rows[0].Visible = false;
                }
                else
                {
                    if (Session["gvHR"] != null)
                    {
                        gvHR.DataSource = Session["gvHR"];
                        gvHR.DataBind();
                    }
                    else
                    {
                        gvHR.DataSource = dt;
                        Session["gvHR"] = gvHR.DataSource;
                        gvHR.DataBind();
                    }
                }
            }
            catch (Exception e1)
            {
                Debug.WriteLine("DataBindItems Type: " + e1.GetType() +
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
                if (searchTerms.Length > 0)
                {
                    if (searchTerms.Contains("@"))
                    {
                        gvHR.DataSource = hrBLO.SelectHRUsingEmail(searchTerms);
                        Session["gvHR"] = gvHR.DataSource;
                        gvHR.DataBind();
                    }
                    else if (searchTerms == "recruiter" || searchTerms == "preboarder"
                        || searchTerms == "data manager" || searchTerms == "datamanager"
                        || searchTerms == "dm" || searchTerms == "administrator"
                        || searchTerms == "admin" || searchTerms == "revoked")
                    {
                        if (searchTerms == "recruiter")
                        {
                            gvHR.DataSource = hrBLO.SelectAllRecruiters();
                            Session["gvHR"] = gvHR.DataSource;
                            gvHR.DataBind();
                        }
                        else if (searchTerms == "preboarder")
                        {
                            gvHR.DataSource = hrBLO.SelectAllPreboarders();
                            Session["gvHR"] = gvHR.DataSource;
                            gvHR.DataBind();
                        }
                        else if (searchTerms == "data manager" || searchTerms == "datamanager"
                            || searchTerms == "dm")
                        {
                            gvHR.DataSource = hrBLO.SelectAllDataManagers();
                            Session["gvHR"] = gvHR.DataSource;
                            gvHR.DataBind();
                        }
                        else if (searchTerms == "administrator" || searchTerms == "admin")
                        {
                            gvHR.DataSource = hrBLO.SelectAllAdmins();
                            Session["gvHR"] = gvHR.DataSource;
                            gvHR.DataBind();
                        }
                        else
                        {
                            gvHR.DataSource = hrBLO.SelectAllRevoked();
                            Session["gvHR"] = gvHR.DataSource;
                            gvHR.DataBind();
                        }
                    }
                    else
                    {
                        gvHR.DataSource = hrBLO.SelectHRByName(searchTerms);
                        Session["gvHR"] = gvHR.DataSource;
                        gvHR.DataBind();
                    }
                    lblMessage.Text = string.Empty;
                }
                else
                {
                    lblMessage.Text = "Please input your search terms. Search HR accounts using their "
                        + "email or account type.";
                }
            }
            catch (Exception e1)
            {
                Debug.WriteLine("Search HR Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblMessage.Text = "Sorry, an error occured. Please try again.";
            }
        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = hrBLO.SelectAll();
            gvHR.DataSource = dt;
            Session["gvHR"] = gvHR.DataSource;
            gvHR.DataBind();
            txtSearch.Text = string.Empty;
        }

        protected void gvHR_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvHR.EditIndex = e.NewEditIndex;
            DataBindItems();
        }

        protected void gvHR_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvHR.EditIndex = -1;
            DataBindItems();
        }

        protected void gvHR_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string email = (gvHR.Rows[e.RowIndex].FindControl("lblEmail") as Label).Text;
            hrBLO.DeleteHR(email, hrEmail);
            gvHR.EditIndex = -1;
            btnResetSearch_Click(sender, e);
        }

        protected void gvHR_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow gvr = gvHR.Rows[e.RowIndex] as GridViewRow;
                string firstName = (gvr.FindControl("txtEditFirstName") as TextBox).Text.Trim();
                string lastName = (gvr.FindControl("txtEditLastName") as TextBox).Text.Trim();
                string email = (gvr.FindControl("lblEmail") as Label).Text;
                string newType = (gvr.FindControl("ddlEditType") as DropDownList).SelectedValue;
                hrBLO.UpdateHR(firstName, lastName, email, newType);
                gvHR.EditIndex = -1;
                btnResetSearch_Click(sender, e);
                lblMessage.Text = "HR Account edited successfully.";
            }
            catch (Exception e1)
            {
                Debug.WriteLine("HR Account Edit Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblMessage.Text = "Sorry, an error occured. Please try again.";
            }
        }

        protected void gvHR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvHR.EditIndex == e.Row.RowIndex)
            {
                string email = gvHR.DataKeys[e.Row.RowIndex].Value.ToString();
                DataTable dt = hrBLO.SelectHRUsingEmail(email);
                TextBox txtEditFirstName = (TextBox)e.Row.FindControl("txtEditFirstName");
                txtEditFirstName.Text = dt.Rows[e.Row.RowIndex]["first_name"].ToString();
                TextBox txtEditLastName = (TextBox)e.Row.FindControl("txtEditLastName");
                txtEditLastName.Text = dt.Rows[e.Row.RowIndex]["last_name"].ToString();
                DropDownList ddlEditType = (DropDownList)e.Row.FindControl("ddlEditType");
                ddlEditType.SelectedValue = dt.Rows[e.Row.RowIndex]["type"].ToString();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnAdd = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)btnAdd.NamingContainer;
                string firstName = (gvr.FindControl("txtFirstName") as TextBox).Text.Trim();
                string lastName = (gvr.FindControl("txtLastName") as TextBox).Text.Trim();
                string email = (gvr.FindControl("txtEmail") as TextBox).Text.Trim();
                string type = (gvr.FindControl("ddlType") as DropDownList).SelectedValue;
                if (firstName.Length == 0 || lastName.Length == 0 || email.Length == 0 || type == "0")
                {
                    lblMessage.Text = "Please fill out the form completely.";
                }
                else
                {
                    if (hrBLO.SelectHRUsingEmail(email).Rows.Count == 0)
                    {
                        hrBLO.InsertHR(firstName, lastName, email, type);
                        lblMessage.Text = "HR Account added successfully.";
                    }
                    else
                    {
                        hrBLO.UpdateHR(firstName, lastName, email, type);
                        lblMessage.Text = "HR Account edited successfully.";
                    }
                    btnResetSearch_Click(sender, e);
                }
            }
            catch (Exception e1)
            {
                Debug.WriteLine("HR Account Add Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblMessage.Text = "Sorry, an error occured. Please try again.";
            }
        }

        protected void btnResendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnResendEmail = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)btnResendEmail.NamingContainer;
                string email = (gvr.FindControl("lblEmail") as Label).Text;
                hrBLO.NewPassword(email);
                lblMessage.Text = "Successfully sent email to " + email + ".";
            }
            catch (Exception e1)
            {
                Debug.WriteLine("Resend HR Email Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblMessage.Text = "Sorry, an error occured. Please try again.";
            }
        }

        protected void gvHR_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells.RemoveAt(5);
                e.Row.Cells.RemoveAt(5);
                e.Row.Cells[4].ColumnSpan = 3;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells.RemoveAt(5);
                e.Row.Cells.RemoveAt(5);
                e.Row.Cells[4].ColumnSpan = 3;
            }
        }

        protected void gvHR_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = Session["gvHR"] as DataTable;
            dt.DefaultView.Sort = e.SortExpression;
            gvHR.DataSource = dt;
            gvHR.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuData.HasFile)
                {
                    if (fuData.FileName.Contains(".txt"))
                    {
                        switch (fuData.FileName.ToLower())
                        {
                            case "affixes.txt":
                                lblUploadError.Text = affixBLO.ManageAffixesWithTextFile(fuData.FileContent);
                                break;
                            case "blood_types.txt":
                                lblUploadError.Text = bloodTypeBLO.ManageBloodTypesWithTextFile(fuData.FileContent);
                                break;
                            case "branches_of_study.txt":
                                lblUploadError.Text = branchOfStudyBLO.ManageBranchesOfStudyWithTextFile(fuData.FileContent);
                                break;
                            case "certificates.txt":
                                lblUploadError.Text = certificateBLO.ManageCertificatesWithTextFile(fuData.FileContent);
                                break;
                            case "civil_statuses.txt":
                                lblUploadError.Text = civilStatusBLO.ManageCivilStatusesWithTextFile(fuData.FileContent);
                                break;
                            case "countries.txt":
                                lblUploadError.Text = countryBLO.ManageCountriesWithTextFile(fuData.FileContent);
                                break;
                            case "education_establishments.txt":
                                lblUploadError.Text = educationEstablishmentBLO.ManageEducationEstablishmentsWithTextFile(fuData.FileContent);
                                break;
                            case "establishment_branches.txt":
                                lblUploadError.Text = establishmentBranchBLO.ManageEstablishmentBranchesWithTextFile(fuData.FileContent);
                                break;
                            case "establishment_certificates.txt":
                                lblUploadError.Text = establishmentCertificateBLO.ManageEstablishmentCertificatesWithTextFile(fuData.FileContent);
                                break;
                            case "establishment_schools.txt":
                                lblUploadError.Text = establishmentSchoolBLO.ManageEstablishmentSchoolsWithTextFile(fuData.FileContent);
                                break;
                            case "family_members.txt":
                                lblUploadError.Text = familyMemberBLO.ManageFamilyMembersWithTextFile(fuData.FileContent);
                                break;
                            case "genders.txt":
                                lblUploadError.Text = genderBLO.ManageGendersWithTextFile(fuData.FileContent);
                                break;
                            case "industries.txt":
                                lblUploadError.Text = industryBLO.ManageIndustriesWithTextFile(fuData.FileContent);
                                break;
                            case "licenses.txt":
                                lblUploadError.Text = licenseBLO.ManageLicensesWithTextFile(fuData.FileContent);
                                break;
                            case "nationalities.txt":
                                lblUploadError.Text = nationalityBLO.ManageNationalitiesWithTextFile(fuData.FileContent);
                                break;
                            case "organization_types.txt":
                                lblUploadError.Text = organizationTypeBLO.ManageOrganizationTypesWithTextFile(fuData.FileContent);
                                break;
                            case "position_types.txt":
                                lblUploadError.Text = positionTypeBLO.ManagePositionTypesWithTextFile(fuData.FileContent);
                                break;
                            case "reasons.txt":
                                lblUploadError.Text = reasonBLO.ManageReasonsWithTextFile(fuData.FileContent);
                                break;
                            case "religions.txt":
                                lblUploadError.Text = religionBLO.ManageReligionsWithTextFile(fuData.FileContent);
                                break;
                            case "schools.txt":
                                lblUploadError.Text = schoolBLO.ManageSchoolsWithTextFile(fuData.FileContent);
                                break;
                            case "specializations.txt":
                                lblUploadError.Text = specializationBLO.ManageSpecializationsWithTextFile(fuData.FileContent);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        lblUploadError.Text = "Please upload only text (.txt) files.";
                    }
                }
                else
                {
                    lblUploadError.Text = "Please select a file to upload.";
                }
            }
            catch (Exception e1)
            {
                Debug.WriteLine("File Upload Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                lblUploadError.Text = "Sorry, an error occured. Please try again.";
            }
        }
    }
}