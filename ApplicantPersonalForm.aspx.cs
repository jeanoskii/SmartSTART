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
    public partial class ApplicantPersonalForm : System.Web.UI.Page
    {
        #region ObjectInstantiation
        private File1VO file1VO = new File1VO();
        private File1BLO file1BLO = new File1BLO();
        private NationalityVO nationalityVO = new NationalityVO();
        private NationalityBLO nationalityBLO = new NationalityBLO();
        private GenderVO genderVO = new GenderVO();
        private GenderBLO genderBLO = new GenderBLO();
        private CivilStatusVO civilStatusVO = new CivilStatusVO();
        private CivilStatusBLO civilStatusBLO = new CivilStatusBLO();
        private AffixVO affixVO = new AffixVO();
        private AffixBLO affixBLO = new AffixBLO();
        private ReligionVO religionVO = new ReligionVO();
        private ReligionBLO religionBLO = new ReligionBLO();
        private BloodTypeVO bloodTypeVO = new BloodTypeVO();
        private BloodTypeBLO bloodTypeBLO = new BloodTypeBLO();
        private CountryVO countryVO = new CountryVO();
        private CountryBLO countryBLO = new CountryBLO();
        #endregion
        private int candidateId;
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
                    lblStep.Text = "Step 2 of 8 - Personal Information";
                    pnlFile1.Visible = false;
                    btnPrevious.Visible = true;
                    btnHome.Visible = false;
                }
                else if (status.Equals("preboarding"))
                {
                    lblStep.Text = "Step 1 of 6 - Personal Information";
                    pnlFile1.Visible = true;
                    btnPrevious.Visible = false;
                    btnHome.Visible = true;
                }
            }
            else
            {
                lblStep.Text = "Personal Information";
                pnlFile1.Visible = true;
                btnPrevious.Enabled = false;
                btnHome.Enabled = false;
                btnNext.Enabled = false;
            }
            if (!Page.IsPostBack)
            {
                DataBindItems();
                FillControlsWithData();
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
            ddlNationality.DataSource = nationalityBLO.SelectAllNationality();
            ddlNationality.DataValueField = "nationality_code";
            ddlNationality.DataTextField = "nationality";
            ddlNationality.DataBind();
            ddlGender.DataSource = genderBLO.SelectAllGender();
            ddlGender.DataValueField = "gender_code";
            ddlGender.DataTextField = "gender";
            ddlGender.DataBind();
            ddlCivilStatus.DataSource = civilStatusBLO.SelectAllCivilStatus();
            ddlCivilStatus.DataValueField = "civil_status_code";
            ddlCivilStatus.DataTextField = "civil_status";
            ddlCivilStatus.DataBind();
            ddlAffix.DataSource = affixBLO.SelectAllAffix();
            ddlAffix.DataValueField = "affix_code";
            ddlAffix.DataTextField = "affix";
            ddlAffix.DataBind();
            ddlReligion.DataSource = religionBLO.SelectAllReligion();
            ddlReligion.DataValueField = "religion_code";
            ddlReligion.DataTextField = "religion";
            ddlReligion.DataBind();
            ddlBloodType.DataSource = bloodTypeBLO.SelectAllBloodType();
            ddlBloodType.DataValueField = "blood_type";
            ddlBloodType.DataTextField = "blood_type";
            ddlBloodType.DataBind();
            ddlPresentCountry.DataSource = countryBLO.SelectAllCountry();
            ddlPresentCountry.DataValueField = "country_code";
            ddlPresentCountry.DataTextField = "country";
            ddlPresentCountry.DataBind();
            ddlPermanentCountry.DataSource = countryBLO.SelectAllCountry();
            ddlPermanentCountry.DataValueField = "country_code";
            ddlPermanentCountry.DataTextField = "country";
            ddlPermanentCountry.DataBind();
            ddlProvincialCountry.DataSource = countryBLO.SelectAllCountry();
            ddlProvincialCountry.DataValueField = "country_code";
            ddlProvincialCountry.DataTextField = "country";
            ddlProvincialCountry.DataBind();
        }

        private void FillControlsWithData()
        {
            if (candidateId > 0)
            {
                file1VO = file1BLO.SelectFile1OfCandidate(candidateId);
                if (file1VO != null)
                {
                    hidFile1ID.Value = file1VO.File1Id.ToString();
                    txtFirstName.Text = file1VO.FirstName;
                    txtLastName.Text = file1VO.LastName;
                    txtNickName.Text = file1VO.NickName;
                    txtMiddleName.Text = file1VO.MiddleName;
                    txtDateOfBirth.Text = DateTime.Parse(file1VO.DateOfBirth).ToString("yyyy-MM-dd");
                    txtPlaceOfBirth.Text = file1VO.PlaceOfBirth;
                    ddlNationality.SelectedValue = file1VO.Nationality;
                    ddlGender.SelectedValue = file1VO.Gender;
                    ddlCivilStatus.SelectedValue = file1VO.CivilStatus;
                    if (!ddlCivilStatus.SelectedValue.Equals("0"))
                    {
                        pnlCivilStatusDate.Visible = true;
                    }
                    else
                    {
                        pnlCivilStatusDate.Visible = false;
                    }
                    if (file1VO.CivilStatusDate != "")
                    {
                        txtCivilStatusDate.Text =
                            DateTime.Parse(file1VO.CivilStatusDate).ToString("yyyy-MM-dd");
                    }
                    ddlAffix.SelectedValue = file1VO.Affix;
                    ddlReligion.SelectedValue = file1VO.Religion;
                    if (file1VO.Height != "")
                    {
                        txtHeightFeet.Text = file1VO.Height.Substring(0, 1);
                        txtHeightInches.Text = file1VO.Height.Substring(1);
                    }
                    txtWeight.Text = file1VO.Weight;
                    ddlBloodType.SelectedValue = file1VO.BloodType;
                    txtSSS.Text = file1VO.SSS;
                    txtTIN.Text = file1VO.TIN;
                    txtPresentAddress.Text = file1VO.PresentAddress;
                    txtPresentCity.Text = file1VO.PresentCity;
                    txtPresentDistrict.Text = file1VO.PresentDistrict;
                    ddlPresentCountry.SelectedValue = file1VO.PresentCountry;
                    txtPresentTelephoneHome.Text = file1VO.PresentTelephoneHome;
                    txtPresentTelephoneAdditional.Text = file1VO.PresentTelephoneAdditional;
                    txtPresentMobileNumber.Text = file1VO.PresentTelephoneMobile;
                    txtPresentFax.Text = file1VO.PresentFax;
                    txtPermanentAddress.Text = file1VO.PermanentAddress;
                    txtPermanentCity.Text = file1VO.PermanentCity;
                    txtPermanentDistrict.Text = file1VO.PermanentDistrict;
                    ddlPermanentCountry.SelectedValue = file1VO.PermanentCountry;
                    txtProvincialAddress.Text = file1VO.ProvincialAddress;
                    txtProvincialCity.Text = file1VO.ProvincialCity;
                    txtProvincialDistrict.Text = file1VO.ProvincialDistrict;
                    ddlProvincialCountry.SelectedValue = file1VO.ProvincialCountry;
                    txtProvincialTelephone.Text = file1VO.ProvincialTelephone;
                }
            }
        }

        protected void ddlCivilStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCivilStatus.SelectedValue != "0")
            {
                pnlCivilStatusDate.Visible = true;
            }
            else
            {
                pnlCivilStatusDate.Visible = false;
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicantAFE.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("PreboardingHome.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string nickName = txtNickName.Text.Trim();
            string middleName = txtMiddleName.Text.Trim();
            string dateOfBirth = txtDateOfBirth.Text.Trim();
            string placeOfBirth = txtPlaceOfBirth.Text.Trim();
            string nationality = ddlNationality.SelectedValue;
            string gender = ddlGender.SelectedValue;
            string civilStatus = ddlCivilStatus.SelectedValue;
            string civilStatusDate = txtCivilStatusDate.Text.Trim();
            string affix = ddlAffix.SelectedValue;
            string religion = ddlReligion.SelectedValue;
            string height = txtHeightFeet.Text.Trim() + txtHeightInches.Text.Trim();
            string weight = txtWeight.Text.Trim();
            string bloodType = ddlBloodType.SelectedValue;
            string sss = txtSSS.Text.Trim();
            string tin = txtTIN.Text.Trim();
            string presentAddress = txtPresentAddress.Text.Trim();
            string presentCity = txtPresentCity.Text.Trim();
            string presentDistrict = txtPresentDistrict.Text.Trim();
            string presentCountry = ddlPresentCountry.SelectedValue;
            string presentTelephoneHome = txtPresentTelephoneHome.Text.Trim();
            string presentTelephoneAdditional = txtPresentTelephoneAdditional.Text.Trim();
            string presentTelephoneMobile = txtPresentMobileNumber.Text.Trim();
            string presentFax = txtPresentFax.Text.Trim();
            string permanentAddress = txtPermanentAddress.Text.Trim();
            string permanentCity = txtPermanentCity.Text.Trim();
            string permanentDistrict = txtPermanentDistrict.Text.Trim();
            string permanentCountry = ddlPermanentCountry.SelectedValue;
            string provincialAddress = txtProvincialAddress.Text.Trim();
            string provincialCity = txtProvincialCity.Text.Trim();
            string provincialDistrict = txtProvincialDistrict.Text.Trim();
            string provincialCountry = ddlProvincialCountry.SelectedValue;
            string provincialTelephone = txtProvincialTelephone.Text.Trim();
            if (hidFile1ID.Value == "")
            {
                file1BLO.InsertFile1(candidateId, status, firstName, lastName, nickName, middleName,
                    dateOfBirth, placeOfBirth, nationality, gender, civilStatus, civilStatusDate,
                    affix, religion, height, weight, bloodType, sss, tin, presentAddress, presentCity,
                    presentDistrict, presentCountry, presentTelephoneHome, presentTelephoneAdditional,
                    presentTelephoneMobile, presentFax, permanentAddress, permanentCity,
                    permanentDistrict, permanentCountry, provincialAddress, provincialCity,
                    provincialDistrict, provincialCountry, provincialTelephone);
            }
            else
            {
                file1BLO.UpdateFile1(int.Parse(hidFile1ID.Value), firstName,
                    lastName, nickName, middleName, dateOfBirth, placeOfBirth, nationality,
                    gender, civilStatus, civilStatusDate, affix, religion, height, weight,
                    bloodType, sss, tin, presentAddress, presentCity, presentDistrict, presentCountry,
                    presentTelephoneHome, presentTelephoneAdditional, presentTelephoneMobile, presentFax,
                    permanentAddress, permanentCity, permanentDistrict, permanentCountry,
                    provincialAddress, provincialCity, provincialDistrict, provincialCountry,
                    provincialTelephone);
            }
            if (status.Equals("preboarding"))
            {
                Response.Redirect("ApplicantEducationalForm.aspx");
            }
            else
            {
                Response.Redirect("ApplicantFamilyForm.aspx");
            }
        }
    }
}