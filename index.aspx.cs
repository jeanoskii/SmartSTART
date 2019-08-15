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
    public partial class index : System.Web.UI.Page
    {
        private CandidateVO candidateVO = new CandidateVO();
        private CandidateBLO candidateBLO = new CandidateBLO();
        private HRVO hrVO = new HRVO();
        private HRBLO hrBLO = new HRBLO();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["Candidate"] != null || Request.Cookies["Candidate"] != null)
            {
                CheckCandidateLogin();
            }
            else if (Session["HR"] != null || Request.Cookies["HR"] != null)
            {
                CheckHRLogin();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CheckCandidateLogin()
        {
            List<string> list = (List<string>)Session["Candidate"];
            string status = list == null
                ? Request.Cookies["Candidate"]["Status"] : list[1];
            if (status.Equals("applicant") || status.Equals("pending"))
            {
                Response.Redirect("ApplicantHome.aspx");
            }
            else if (status.Equals("preboarding"))
            {
                Response.Redirect("PreboardingHome.aspx");
            }
            else if (status.Equals("hiring"))
            {
                Response.Redirect("HiringHome.aspx");
            }
        }

        private void CheckHRLogin()
        {
            List<string> list = (List<string>)Session["HR"];
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
                Response.Redirect("AdminHome.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();
                int candidateId;
                if (int.TryParse(username, out candidateId) == true)
                {
                    candidateVO = candidateBLO.AuthenticateCandidate(candidateId, password);
                    if (candidateVO != null)
                    {
                        if (chkRemember.Checked)
                        {
                            HttpCookie cookie = new HttpCookie("Candidate");
                            cookie.Values["ID"] = candidateVO.CandidateId.ToString();
                            cookie.Values["Status"] = candidateVO.Status.ToString();
                            cookie.Expires = DateTime.Now.AddYears(1);
                            //cookie.Secure = true;
                            Response.Cookies.Add(cookie);
                        }
                        List<string> list = new List<string>();
                        list.Add(candidateVO.CandidateId.ToString());
                        list.Add(candidateVO.Status.ToString());
                        Session.Add("Candidate", list);
                        CheckCandidateLogin();
                    }
                    else
                    {
                        lblError.Text = "Candidate login failed.";
                    }
                }
                else
                {
                    hrVO = hrBLO.AuthenticateHR(username, password);
                    if (hrVO != null)
                    {
                        if (chkRemember.Checked)
                        {
                            HttpCookie cookie = new HttpCookie("HR");
                            cookie.Values["Email"] = hrVO.Email.ToString();
                            cookie.Values["Type"] = hrVO.Type.ToString();
                            cookie.Expires = DateTime.Now.AddYears(1);
                            //cookie.Secure = true;
                            Response.Cookies.Add(cookie);
                        }
                        List<string> list = new List<string>();
                        list.Add(hrVO.Email.ToString());
                        list.Add(hrVO.Type.ToString());
                        Session.Add("HR", list);
                        CheckHRLogin();
                    }
                    else
                    {
                        lblError.Text = "HR login failed.";
                    }
                }
            }
            catch(Exception e1)
            {
                lblError.Text = "Login failed. Please try again";
                Debug.WriteLine("Login Exception Type: " + e1.GetType() +
                        "\nMessage: " + e1.Message +
                        "\nStack Trace: " + e1.StackTrace);
            }
        }
    }
}