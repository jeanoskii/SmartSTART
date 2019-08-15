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
    public partial class ApplicantReviewForm : System.Web.UI.Page
    {
        #region ObjectInstantiation
        File1VO file1VO = new File1VO();
        AFEVO afeVO = new AFEVO();
        AnswerVO answerVO = new AnswerVO();
        File1BLO file1BLO = new File1BLO();
        File2BLO file2BLO = new File2BLO();
        File3BLO file3BLO = new File3BLO();
        File4BLO file4BLO = new File4BLO();
        File5BLO file5BLO = new File5BLO();
        CandidateBLO candidateBLO = new CandidateBLO();
        AFEBLO afeBLO = new AFEBLO();
        AnswerBLO answerBLO = new AnswerBLO();
        QualificationBLO qualificationBLO = new QualificationBLO();
        ProfessionalReferenceBLO professionalReferenceBLO = new ProfessionalReferenceBLO();
        PersonalReferenceBLO personalReferenceBLO = new PersonalReferenceBLO();
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
                if (status.Equals("applicant") || status.Equals("pending"))
                {
                    lblStep.Text = "Step 8 of 8 - Review";
                    pnlApplicantPersonalInfo.Visible = true;
                    pnlPreboardingPersonalInfo.Visible = false;
                    pnlPreboardingEducational.Visible = false;
                    pnlPreboardingOrganizational.Visible = false;
                }
                else
                {
                    lblStep.Text = "Step 6 of 6 - Review";
                    pnlApplicantPersonalInfo.Visible = false;
                    pnlPreboardingPersonalInfo.Visible = true;
                    pnlPreboardingEducational.Visible = true;
                    pnlPreboardingOrganizational.Visible = true;
                }
            }
            else
            {
                lblStep.Text = "Review";
                pnlApplicantPersonalInfo.Visible = false;
                btnSubmit.Enabled = false;
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
                    candidateId = list == null
                        ? int.Parse(Request.Cookies["Candidate"]["ID"].ToString())
                        : int.Parse(list[0].ToString());
                    afeId = afeBLO.SelectAFEIDOfCandidate(candidateId);
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
            if (Session["Candidate"] != null || Request.Cookies["Candidate"] != null)
            {
                afeVO = afeBLO.SelectAFEOfCandidate(candidateId);
                lblApplicationDate.Text = DateTime.Parse(afeVO.ApplicationDate).
                        ToString("MMMM d, yyyy");
                lblPositionAppliedFor.Text = afeVO.PositionAppliedFor;
                lblInterviewerName.Text = afeVO.InterviewerName;
                file1VO = file1BLO.SelectFile1OfCandidateForPrint(candidateId);
                lblName.Text = file1VO.FirstName
                    + " &ldquo;"
                    + file1VO.NickName
                    + "&rdquo; "
                    + file1VO.MiddleName
                    + " "
                    + file1VO.LastName
                    + " "
                    + file1VO.Affix;
                if (status.Equals("applicant") || status.Equals("pending"))
                {
                    lblApplicantMobileNumber.Text = file1VO.PresentTelephoneMobile;
                    lblApplicantEmailAddress.Text = candidateBLO.SelectCandidateEmail(candidateId);
                    lblApplicantDateOfBirth.Text = DateTime.Parse(file1VO.DateOfBirth).
                        ToString("MMMM d, yyyy");
                    lblApplicantPlaceOfBirth.Text = file1VO.PlaceOfBirth;
                    lblApplicantGender.Text = file1VO.Gender;
                    if (file1VO.CivilStatus != "Single")
                    {
                        lblApplicantCivilStatus.Text = file1VO.CivilStatus + " since " +
                            DateTime.Parse(file1VO.CivilStatusDate).ToString("MMMM d, yyyy");
                    }
                    else
                    {
                        lblApplicantCivilStatus.Text = file1VO.CivilStatus;
                    }
                    lblApplicantPresentAddress.Text =
                        file1VO.PresentAddress
                        + ", "
                        + file1VO.PresentCity
                        + ", "
                        + file1VO.PresentDistrict
                        + ", "
                        + file1VO.PresentCountry;
                    lblApplicantProvincialAddress.Text =
                        file1VO.ProvincialAddress
                        + ", "
                        + file1VO.ProvincialCity
                        + ", "
                        + file1VO.ProvincialDistrict
                        + ", "
                        + file1VO.ProvincialCountry;
                    lblApplicantHeight.Text = file1VO.Height;
                    lblApplicantWeight.Text = file1VO.Weight;
                    lblApplicantBloodType.Text = file1VO.BloodType;
                }
                else
                {
                    lblDateOfBirth.Text = DateTime.Parse(file1VO.DateOfBirth).ToString("MMMM d, yyyy");
                    lblPlaceOfBirth.Text = file1VO.PlaceOfBirth;
                    lblNationality.Text = file1VO.Nationality;
                    lblGender.Text = file1VO.Gender;
                    lblCivilStatus.Text = file1VO.CivilStatus;
                    lblReligion.Text = file1VO.Religion;
                    lblBloodType.Text = file1VO.BloodType;
                    lblSSS.Text = file1VO.SSS;
                    lblTIN.Text = file1VO.TIN;
                    lblPresentAddress.Text =
                        file1VO.PresentAddress
                        + ", "
                        + file1VO.PresentCity
                        + ", "
                        + file1VO.PresentDistrict
                        + ", "
                        + file1VO.PresentCountry;
                    lblPermanentAddress.Text =
                        file1VO.PermanentAddress
                        + ", "
                        + file1VO.PermanentCity
                        + ", "
                        + file1VO.PermanentDistrict
                        + ", "
                        + file1VO.PermanentCountry;
                    lblProvincialAddress.Text =
                        file1VO.ProvincialAddress
                        + ", "
                        + file1VO.ProvincialCity
                        + ", "
                        + file1VO.ProvincialDistrict
                        + ", "
                        + file1VO.ProvincialCountry;
                    lblHomeNumber.Text = file1VO.PresentTelephoneHome;
                    lblAdditionalNumber.Text = file1VO.PresentTelephoneAdditional;
                    lblMobileNumber.Text = file1VO.PresentTelephoneMobile;
                    lblFaxNumber.Text = file1VO.PresentFax;
                    lblProvincialNumber.Text = file1VO.ProvincialTelephone;
                    gvEducations.DataSource = file2BLO.SelectFile2OfCandidate(candidateId);
                    gvEducations.DataBind();
                    if (file2BLO.File2OfCandidateHasSchoolOthers(candidateId) == false)
                    {
                        gvEducations.Columns[2].Visible = false;
                    }
                    gvOrganizations.DataSource = file4BLO.SelectFile4OfCandidate(candidateId);
                    gvOrganizations.DataBind();
                }
                gvEmployers.DataSource = file3BLO.SelectFile3OfCandidate(candidateId);
                gvEmployers.DataBind();
                gvOtherQualifications.DataSource = qualificationBLO.SelectQualificationOfAFE(afeId);
                gvOtherQualifications.DataBind();
                if (qualificationBLO.QualificationOfAFEHasOthers(afeId) == false)
                {
                    gvOtherQualifications.Columns[2].Visible = false;
                }
                List<Label> answers = new List<Label>();
                answers.Add(lblAnswer1);
                answers.Add(lblAnswer2);
                answers.Add(lblAnswer3);
                answers.Add(lblAnswer4);
                answers.Add(lblAnswer5);
                answers.Add(lblAnswer6);
                answers.Add(lblAnswer7);
                List<Label> questions = new List<Label>();
                questions.Add(lblQuestion1);
                questions.Add(lblQuestion2);
                questions.Add(lblQuestion3);
                questions.Add(lblQuestion4);
                questions.Add(lblQuestion5);
                questions.Add(lblQuestion6);
                questions.Add(lblQuestion7);
                DataTable dt = new DataTable();
                dt = answerBLO.SelectAnswerWithQuestionOfAFE(afeId);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    questions[i].Text = dt.Rows[i]["question"].ToString();
                    if (dt.Rows[i]["answer"].ToString() == "False")
                    {
                        answers[i].Text = "No";
                    }
                    else if (dt.Rows[i]["answer"].ToString() == "True")
                    {
                        if (dt.Rows[i]["value"].ToString() != "")
                        {
                            answers[i].Text = "Yes: " + dt.Rows[i]["value"].ToString();
                        }
                        else if (dt.Rows[i]["relative_name"].ToString() != "" &&
                            dt.Rows[i]["company_name"].ToString() != "")
                        {
                            answers[i].Text = "Yes: " + dt.Rows[i]["relative_name"].ToString() +
                                " at " + dt.Rows[i]["company_name"].ToString();
                        }
                    }
                }
                gvDependents.DataSource = file5BLO.SelectFile5OfCandidate(candidateId);
                gvDependents.DataBind();
                gvProfessionalReferences.DataSource =
                    professionalReferenceBLO.SelectProfessionalReferenceOfAFE(afeId);
                gvProfessionalReferences.DataBind();
                gvPersonalReferences.DataSource =
                    personalReferenceBLO.SelectPersonalReferenceOfAFE(afeId);
                gvPersonalReferences.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (chkAgree.Checked)
            {
                if (gvDependents.Rows.Count > 0 || gvEducations.Rows.Count > 0)
                {
                    if (status.Equals("applicant") || status.Equals("pending"))
                    {
                        candidateBLO.SubmittedForms(candidateId);
                    }
                    Response.Redirect("ApplicantFinished.aspx");
                }
                else
                {
                    lblError.Text = "Please provide at least one (1) dependent and education.";
                }
            }
            else
            {
                lblError.Text = "Please agree to our terms";
            }
        }

        protected void btnEditAFE_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantAFE.aspx");
        }

        protected void btnEditPersonal_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantPersonalForm.aspx");
        }

        protected void btnEditEducationalBackground_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantEducationalForm.aspx");
        }

        protected void btnEditEmployment_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantEmploymentForm.aspx");
        }

        protected void btnEditQualifications_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantOtherQualificationsForm.aspx");
        }

        protected void btnEditOrganization_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantOrganizationalForm.aspx");
        }

        protected void btnEditFamily_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantFamilyForm.aspx");
        }

        protected void btnEditProfessionalRef_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantProfessionalReferencesForm.aspx");
        }

        protected void btnEditPersonalRef_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantPersonalReferencesForm.aspx");
        }
    }
}