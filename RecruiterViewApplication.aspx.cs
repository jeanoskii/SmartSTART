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
    public partial class RecruiterViewApplication : System.Web.UI.Page
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
        private int afeId;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            CheckRecruiterLogin();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            candidateId = int.Parse(Request.QueryString["cid"]);
            afeId = afeBLO.SelectAFEIDOfCandidate(candidateId);
            if (!Page.IsPostBack)
            {
                DataBindItems();
            }
        }

        private void CheckRecruiterLogin()
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
                if (type.Equals("preboarder"))
                {
                    btnPreboard.Visible = false;
                }
                else if (type.Equals("datamanager"))
                {
                    Response.Redirect("DataManagerHome.aspx");
                }
                else if (type.Equals("admin"))
                {
                    Response.Redirect("AdminHome.aspx");
                }
            }
            else
            {
                Response.Redirect("index.aspx");
            }
        }

        private void DataBindItems()
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

        protected void btnPreboard_Click(object sender, EventArgs e)
        {
            string email = candidateBLO.SelectCandidateEmail(candidateId);
            candidateBLO.PreboardCandidate(candidateId, email);
            Response.Redirect("RecruiterHome.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RecruiterHome.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            candidateBLO.DeleteCandidate(candidateId);
            Response.Redirect("RecruiterHome.aspx");
        }
    }
}