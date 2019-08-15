using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAPRES.BusinessLogicObjects;
using CAPRES.ValueObjects;
using System.Data;

namespace CAPRES
{
    public partial class ApplicantOtherQualificationsForm : System.Web.UI.Page
    {
        #region ObjectInstantiation
        private AFEBLO afeBLO = new AFEBLO();
        private AnswerBLO answerBLO = new AnswerBLO();
        private QuestionBLO questionBLO = new QuestionBLO();
        private LicenseBLO licenseBLO = new LicenseBLO();
        private QualificationBLO qualificationBLO = new QualificationBLO();
        private SpecifyGeneralBLO specifyGeneralBLO = new SpecifyGeneralBLO();
        private SpecifyGeneralVO specifyGeneralVO = new SpecifyGeneralVO();
        private SpecifyRelativeVO specifyRelativeVO = new SpecifyRelativeVO();
        private SpecifyRelativeBLO specifyRelativeBLO = new SpecifyRelativeBLO();
        #endregion
        private int candidateId;
        private int afeId;
        private string status;
        private List<string> questionCodes = new List<string>();
        private List<Label> questions = new List<Label>();
        private List<RadioButtonList> answers = new List<RadioButtonList>();
        private List<Panel> specifyPanels = new List<Panel>();
        private DataTable dt = new DataTable();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            CheckApplicantLogin();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            #region RadioButtonList
            questions.Add(lblQuestion1);
            questions.Add(lblQuestion2);
            questions.Add(lblQuestion3);
            questions.Add(lblQuestion4);
            questions.Add(lblQuestion5);
            questions.Add(lblQuestion6);
            questions.Add(lblQuestion7);
            answers.Add(rblQ1);
            answers.Add(rblQ2);
            answers.Add(rblQ3);
            answers.Add(rblQ4);
            answers.Add(rblQ5);
            answers.Add(rblQ6);
            answers.Add(rblQ7);
            specifyPanels.Add(pnlQ1Yes);
            specifyPanels.Add(pnlQ2Yes);
            specifyPanels.Add(pnlQ3Yes);
            specifyPanels.Add(pnlQ4Yes);
            specifyPanels.Add(pnlQ5Yes);
            specifyPanels.Add(pnlQ6Yes);
            specifyPanels.Add(pnlQ7Yes);
            #endregion
            SetQuestions();
            if (!Page.IsPostBack)
            {
                DataBindItems();
                SetAnswers();
            }
            if (candidateId > 0)
            {
                lblStep.Text = "Step 5 of 8 - Other Qualifications";
            }
            else
            {
                lblStep.Text = "Other Qualifications";
                lblError.Text = "Error Text";
                gvLicenses.Rows[0].Visible = true;
                LinkButton btnAdd = gvLicenses.FooterRow.FindControl("btnAdd")
                    as LinkButton;
                btnAdd.Enabled = false;
                LinkButton btnEdit = gvLicenses.Rows[0].FindControl("btnEdit")
                    as LinkButton;
                btnEdit.Enabled = false;
                LinkButton btnDelete = gvLicenses.Rows[0].FindControl("btnDelete")
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
            DataTable dt = qualificationBLO.SelectQualificationOfAFE(afeId);
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                gvLicenses.DataSource = dt;
                gvLicenses.DataBind();
                gvLicenses.Rows[0].Visible = false;
            }
            else
            {
                gvLicenses.DataSource = dt;
                gvLicenses.DataBind();
            }
            DropDownList ddlParticulars = gvLicenses.FooterRow.FindControl("ddlParticulars")
                as DropDownList;
            ddlParticulars.DataSource = licenseBLO.SelectAllLicense();
            ddlParticulars.DataTextField = "license";
            ddlParticulars.DataValueField = "license_code";
            ddlParticulars.DataBind();
        }

        private void SetAnswers()
        {
            dt = answerBLO.SelectAnswerOfAFE(afeId);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    answers[i].SelectedValue = dt.Rows[i][2].ToString();
                    if (answers[i].SelectedValue.Equals("True"))
                    {
                        int answerId = int.Parse(dt.Rows[i][0].ToString());
                        if (specifyPanels[i].Controls.Count == 3)
                        {
                            specifyGeneralVO = specifyGeneralBLO.SelectSpecifyGeneralOfAnswer(
                                answerId);
                            string specifiedValue = specifyGeneralVO.SpecifyValue;
                            TextBox txtSpecify = (TextBox)specifyPanels[i].FindControl
                                    ("txtQ" + (i + 1) + "Specify");
                            txtSpecify.Text = specifiedValue;
                            specifyPanels[i].Visible = true;
                        }
                        else if (specifyPanels[i].Controls.Count == 5)
                        {
                            specifyRelativeVO = specifyRelativeBLO.SelectSpecifyRelativeOfAnswer(
                                answerId);
                            string specifiedCompany = specifyRelativeVO.CompanyName;
                            string specifiedRelative = specifyRelativeVO.RelativeName;
                            TextBox txtSpecifyCompany = (TextBox)specifyPanels[i].FindControl
                                ("txtQ" + (i + 1) + "SpecifyCompany");
                            TextBox txtSpecifyRelative = (TextBox)specifyPanels[i].FindControl
                                ("txtQ" + (i + 1) + "SpecifyRelative");
                            txtSpecifyCompany.Text = specifiedCompany;
                            txtSpecifyRelative.Text = specifiedRelative;
                            specifyPanels[i].Visible = true;
                        }
                    }
                }
            }
        }

        private void SetQuestions()
        {
            dt = new DataTable();
            dt = questionBLO.SelectAllQuestion();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                questionCodes.Add(dt.Rows[i][0].ToString());
                questions[i].Text = dt.Rows[i][1].ToString();
            }
        }

        #region RadioButtons
        protected void rblQ1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblQ1.SelectedValue.Equals("True"))
            {
                pnlQ1Yes.Visible = true;
            }
            else
            {
                pnlQ1Yes.Visible = false;
            }
            DataBindItems();
        }

        protected void rblQ2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblQ2.SelectedValue.Equals("True"))
            {
                pnlQ2Yes.Visible = true;
            }
            else
            {
                pnlQ2Yes.Visible = false;
            }
            DataBindItems();
        }

        protected void rblQ3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblQ3.SelectedValue.Equals("True"))
            {
                pnlQ3Yes.Visible = true;
            }
            else
            {
                pnlQ3Yes.Visible = false;
            }
            DataBindItems();
        }

        protected void rblQ4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblQ4.SelectedValue.Equals("True"))
            {
                pnlQ4Yes.Visible = true;
            }
            else
            {
                pnlQ4Yes.Visible = false;
            }
            DataBindItems();
        }

        protected void rblQ5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblQ5.SelectedValue.Equals("True"))
            {
                pnlQ5Yes.Visible = true;
            }
            else
            {
                pnlQ5Yes.Visible = false;
            }
            DataBindItems();
        }

        protected void rblQ6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblQ6.SelectedValue.Equals("True"))
            {
                pnlQ6Yes.Visible = true;
            }
            else
            {
                pnlQ6Yes.Visible = false;
            }
            DataBindItems();
        }

        protected void rblQ7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblQ7.SelectedValue.Equals("True"))
            {
                pnlQ7Yes.Visible = true;
            }
            else
            {
                pnlQ7Yes.Visible = false;
            }
            DataBindItems();
        }
        #endregion

        protected void ddlParticulars_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlParticulars = gvLicenses.FooterRow.FindControl("ddlParticulars")
                as DropDownList;
            TextBox txtOthersName = gvLicenses.FooterRow.FindControl("txtOthersName")
                as TextBox;
            if (ddlParticulars.SelectedValue == "00")
            {
                txtOthersName.Enabled = true;
            }
            else
            {
                txtOthersName.Enabled = false;
            }
        }

        protected void ddlEditParticulars_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlEditParticulars = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)ddlEditParticulars.NamingContainer;
            TextBox txtEditSpecifyValue = (TextBox)gvr.FindControl("txtEditSpecifyValue");
            if (ddlEditParticulars.SelectedValue == "00")
            {
                txtEditSpecifyValue.Enabled = true;
            }
            else
            {
                txtEditSpecifyValue.Enabled = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            LinkButton btnAdd = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)btnAdd.NamingContainer;
            string licenseCode = (gvr.FindControl("ddlParticulars") as DropDownList).SelectedValue;
            TextBox txtSpecifyValue = (TextBox)gvr.FindControl("txtOthersName");
            string othersName = txtSpecifyValue.Text.Trim();
            string number = (gvr.FindControl("txtLicenseNumber") as TextBox).Text.Trim();
            string rating = (gvr.FindControl("txtRating") as TextBox).Text.Trim();
            if (txtSpecifyValue.Enabled == true)
            {
                if (licenseCode == "0" || othersName.Length == 0 || number.Length == 0
                || rating.Length == 0)
                {
                    lblError.Text = "Please fill out the form completely.";
                }
                else
                {
                    qualificationBLO.InsertQualification(afeId, licenseCode, othersName, number, rating);
                    DataBindItems();
                }
            }
            else
            {
                if (licenseCode == "0" || number.Length == 0
                || rating.Length == 0)
                {
                    lblError.Text = "Please fill out the form completely.";
                }
                else
                {
                    qualificationBLO.InsertQualification(afeId, licenseCode, othersName, number, rating);
                    DataBindItems();
                }
            }
            
        }

        protected void gvLicenses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvLicenses.EditIndex = e.NewEditIndex;
            DataBindItems();
        }

        protected void gvLicenses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvLicenses.EditIndex = -1;
            DataBindItems();
        }

        protected void gvLicenses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gvLicenses.EditIndex == e.Row.RowIndex)
            {
                int otherQualificationId = int.Parse(gvLicenses.DataKeys[e.Row.RowIndex].Value.ToString());
                DataTable dt = new DataTable();
                dt = qualificationBLO.SelectSpecificQualification(otherQualificationId);
                DropDownList ddlEditParticulars = (DropDownList)e.Row.FindControl("ddlEditParticulars");
                ddlEditParticulars.DataSource = licenseBLO.SelectAllLicense();
                ddlEditParticulars.DataTextField = "license";
                ddlEditParticulars.DataValueField = "license_code";
                ddlEditParticulars.DataBind();
                ddlEditParticulars.SelectedValue = dt.Rows[0]["license_code"].ToString();
                TextBox txtEditSpecifyValue = (TextBox)e.Row.FindControl("txtEditSpecifyValue");
                txtEditSpecifyValue.Text = dt.Rows[0]["others_name"].ToString();
                TextBox txtEditLicenseNumber = (TextBox)e.Row.FindControl("txtEditLicenseNumber");
                txtEditLicenseNumber.Text = dt.Rows[0]["number"].ToString();
                TextBox txtEditRating = (TextBox)e.Row.FindControl("txtEditRating");
                txtEditRating.Text = dt.Rows[0]["rating"].ToString();
            }
        }

        protected void gvLicenses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int otherQualificationId = int.Parse(gvLicenses.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow gvr = gvLicenses.Rows[e.RowIndex] as GridViewRow;
            string newLicense = (gvr.FindControl("ddlEditParticulars") as DropDownList).SelectedValue;
            string newOthersName = "";
            string newNumber = (gvr.FindControl("txtEditLicenseNumber") as TextBox).Text.Trim();
            string newRating = (gvr.FindControl("txtEditRating") as TextBox).Text.Trim();
            if (newLicense == "00")
            {
                newOthersName = (gvr.FindControl("txtEditSpecifyValue") as TextBox).Text.Trim();
                if (newLicense.Length == 0 || newNumber.Length == 0 || newRating.Length == 0
                    || newOthersName.Length == 0)
                {
                    lblError.Text = "Please fill out the form completely.";
                }
                else
                {
                    qualificationBLO.UpdateQualification(otherQualificationId, newLicense, newOthersName,
                        newNumber, newRating);
                    gvLicenses.EditIndex = -1;
                    DataBindItems();
                }
            }
            if (newLicense.Length == 0 || newNumber.Length == 0 || newRating.Length == 0)
            {
                lblError.Text = "Please fill out the form completely.";
            }
            else
            {
                qualificationBLO.UpdateQualification(otherQualificationId, newLicense, newOthersName,
                    newNumber, newRating);
                gvLicenses.EditIndex = -1;
                DataBindItems();
            }
            
        }

        protected void gvLicenses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int otherQualificationId = int.Parse(gvLicenses.DataKeys[e.RowIndex].Value.ToString());
            qualificationBLO.DeleteQualification(otherQualificationId);
            gvLicenses.EditIndex = -1;
            DataBindItems();
        }
        
        protected void gvLicenses_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells.RemoveAt(6);
                e.Row.Cells[5].ColumnSpan = 2;
                e.Row.Cells[5].Text = "ACTION";
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells.RemoveAt(6);
                e.Row.Cells[5].ColumnSpan = 2;
            }
        }
        
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantEmploymentForm.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (rblQ1.SelectedValue == "" || (pnlQ1Yes.Visible == true && txtQ1Specify.Text.Length == 0) ||
                rblQ2.SelectedValue == "" || (pnlQ2Yes.Visible == true && txtQ2Specify.Text.Length == 0) ||
                rblQ3.SelectedValue == "" || (pnlQ3Yes.Visible == true && txtQ3Specify.Text.Length == 0) ||
                rblQ4.SelectedValue == "" || (pnlQ4Yes.Visible == true && (txtQ4SpecifyCompany.Text.Length == 0 || txtQ4SpecifyRelative.Text.Length == 0)) ||
                rblQ5.SelectedValue == "" || (pnlQ5Yes.Visible == true && (txtQ5SpecifyCompany.Text.Length == 0 || txtQ5SpecifyRelative.Text.Length == 0)) ||
                rblQ6.SelectedValue == "" || (pnlQ6Yes.Visible == true && txtQ6Specify.Text.Length == 0) ||
                rblQ7.SelectedValue == "" || (pnlQ7Yes.Visible == true && txtQ7Specify.Text.Length == 0))
            {
                lblError.Text = "Please answer all questions.";
            }
            else
            {
                dt = answerBLO.SelectAnswerOfAFE(afeId);
                if (dt.Rows.Count == 0)
                {
                    for (int i = 0; i < 7; i++)
                   {
                        int answerId = int.Parse(answerBLO.InsertAnswer(afeId, questionCodes[i],
                            answers[i].SelectedValue).Rows[i][0].ToString());
                        if (answers[i].SelectedValue.Equals("True"))
                        {
                            if (specifyPanels[i].Controls.Count == 3)
                            {
                                TextBox txtSpecify = (TextBox)specifyPanels[i].FindControl
                                    ("txtQ" + (i + 1) + "Specify");
                                specifyGeneralBLO.InsertSpecifyGeneral(answerId, txtSpecify.Text.Trim());
                            }
                            if (specifyPanels[i].Controls.Count == 5)
                            {
                                TextBox txtSpecifyCompany = (TextBox)specifyPanels[i].FindControl
                                    ("txtQ" + (i + 1) + "SpecifyCompany");
                                TextBox txtSpecifyRelative = (TextBox)specifyPanels[i].FindControl
                                    ("txtQ" + (i + 1) + "SpecifyRelative");
                                specifyRelativeBLO.InsertSpecifyRelative(answerId,
                                    txtSpecifyCompany.Text.Trim(), txtSpecifyRelative.Text.Trim());
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int answerId = int.Parse(dt.Rows[i][0].ToString());
                        answerBLO.UpdateAnswer(answerId, answers[i].SelectedValue);
                        if (answers[i].SelectedValue.Equals("True"))
                        {
                            if (specifyPanels[i].Controls.Count == 3)
                            {
                                specifyGeneralVO = specifyGeneralBLO.
                                    SelectSpecifyGeneralOfAnswer(answerId);
                                TextBox txtSpecify = (TextBox)specifyPanels[i].FindControl
                                        ("txtQ" + (i + 1) + "Specify");
                                if (specifyGeneralVO == null)
                                {
                                    specifyGeneralBLO.InsertSpecifyGeneral(answerId, txtSpecify.Text.Trim());
                                }
                                else
                                {
                                    int specifyGeneralId = specifyGeneralVO.SpecifyGeneralId;
                                    specifyGeneralBLO.UpdateSpecifyGeneral(specifyGeneralId,
                                    txtSpecify.Text.Trim());
                                }
                            }
                            else if (specifyPanels[i].Controls.Count == 5)
                            {
                                specifyRelativeVO = specifyRelativeBLO.
                                    SelectSpecifyRelativeOfAnswer(answerId);
                                TextBox txtSpecifyCompany = (TextBox)specifyPanels[i].FindControl
                                ("txtQ" + (i + 1) + "SpecifyCompany");
                                TextBox txtSpecifyRelative = (TextBox)specifyPanels[i].FindControl
                                    ("txtQ" + (i + 1) + "SpecifyRelative");
                                if (specifyRelativeVO == null)
                                {
                                    specifyRelativeBLO.InsertSpecifyRelative(answerId,
                                        txtSpecifyCompany.Text.Trim(), txtSpecifyRelative.Text.Trim());
                                }
                                else
                                {
                                    int specifyRelativeId = specifyRelativeVO.SpecifyRelativeId;
                                    specifyRelativeBLO.UpdateSpecifyRelative(specifyRelativeId,
                                        txtSpecifyCompany.Text.Trim(), txtSpecifyRelative.Text.Trim());
                                }
                            }
                        }
                    }
                }
                Response.Redirect("ApplicantProfessionalReferencesForm.aspx");
            }
        }
    }
}