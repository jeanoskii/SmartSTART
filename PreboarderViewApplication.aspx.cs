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
    public partial class PreboardingViewApplication : System.Web.UI.Page
    {
        #region ObjectInstantiation
        AFEVO afeVO = new AFEVO();
        AFEBLO afeBLO = new AFEBLO();
        File1VO file1VO = new File1VO();
        File1BLO file1BLO = new File1BLO();
        File2BLO file2BLO = new File2BLO();
        File3BLO file3BLO = new File3BLO();
        File4BLO file4BLO = new File4BLO();
        File5BLO file5BLO = new File5BLO();
        CandidateBLO candidateBLO = new CandidateBLO();
        AnswerBLO answerBLO = new AnswerBLO();
        QualificationBLO qualificationBLO = new QualificationBLO();
        ProfessionalReferenceBLO professionalReferenceBLO = new ProfessionalReferenceBLO();
        PersonalReferenceBLO personalReferenceBLO = new PersonalReferenceBLO();
        #endregion
        private int candidateId;
        private string status;
        private int afeId;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckPreboarderLogin();
            afeId = afeBLO.SelectAFEIDOfCandidate(candidateId);
            if (!Page.IsPostBack)
            {
                candidateId = int.Parse(Request.QueryString["cid"]);
                DataBindItems();
            }
        }

        private void CheckPreboarderLogin()
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
                    string hrEmail = list == null
                        ? Request.Cookies["HR"]["Email"] : list[0];
                    lblUser.Text = "Hello, " + hrEmail;
                }
                else if (type.Equals("datamanager"))
                {
                    Response.Redirect("DataManagerHome.aspx");
                }
                else if (type.Equals("admin"))
                {
                    string hrEmail = list == null
                        ? Request.Cookies["HR"]["Email"] : list[0];
                    lblUser.Text = "Hello, " + hrEmail;
                    Button btnGoToCandidateApplicant = new Button();
                    btnGoToCandidateApplicant.Text = "Go To Candidate-Applicant View";
                    btnGoToCandidateApplicant.PostBackUrl = "ApplicantHome.aspx";
                    pnlAdminLinks.Controls.Add(btnGoToCandidateApplicant);
                    Button btnGoToCandidatePreboarding = new Button();
                    btnGoToCandidatePreboarding.Text = "Go To Candidate-Preboarding View";
                    btnGoToCandidatePreboarding.PostBackUrl = "PreboardingHome.aspx";
                    pnlAdminLinks.Controls.Add(btnGoToCandidatePreboarding);
                    Button btnGoToCandidateHiring = new Button();
                    btnGoToCandidateHiring.Text = "Go To Candidate-Hiring View";
                    btnGoToCandidateHiring.PostBackUrl = "HiringHome.aspx";
                    pnlAdminLinks.Controls.Add(btnGoToCandidateHiring);
                    Button btnGoToHRRecruiter = new Button();
                    btnGoToHRRecruiter.Text = "Go To HR-Recruiter View";
                    btnGoToHRRecruiter.PostBackUrl = "RecruiterHome.aspx";
                    pnlAdminLinks.Controls.Add(btnGoToHRRecruiter);
                    Button btnGoToHRDataManager = new Button();
                    btnGoToHRDataManager.Text = "Go To HR-Data Manager View";
                    btnGoToHRDataManager.PostBackUrl = "DataManagerHome.aspx";
                    pnlAdminLinks.Controls.Add(btnGoToHRDataManager);
                    Button btnGoToHRAdmin = new Button();
                    btnGoToHRAdmin.Text = "Go To HR-Admin View";
                    btnGoToHRAdmin.PostBackUrl = "AdminHome.aspx";
                    pnlAdminLinks.Controls.Add(btnGoToHRAdmin);
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PreboarderHome.aspx");
        }

        protected void btnHire_Click(object sender, EventArgs e)
        {
            string email = lblEmailAddress.Text;
            //candidateBLO.HireCandidate(candidateId, email);
            Response.Redirect("PreboarderHome.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            candidateBLO.DeleteCandidate(candidateId);
            Response.Redirect("PreboarderHome.aspx");
        }
    }
}