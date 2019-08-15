using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CAPRES.DataAccessObjects;
using CAPRES.ValueObjects;
using Microsoft.VisualBasic.FileIO;

namespace CAPRES.BusinessLogicObjects
{
    public class CandidateBLO
    {
        private CandidateDAO candidateDAO = new CandidateDAO();
        private CandidateVO candidateVO = new CandidateVO();
        private HRBLO hrBLO = new HRBLO();
        private PasswordHelper passwordHelper = new PasswordHelper();
        private EmailHelper emailHelper = new EmailHelper();

        public DataTable SelectAll()
        {
            return candidateDAO.SelectAll();
        }

        public DataTable SelectCandidate(string email)
        {
            return candidateDAO.SelectCandidate(email);
        }

        public DataTable SelectCandidate(int candidateId)
        {
            return candidateDAO.SelectCandidateNoPassword(candidateId);
        }

        public string SelectCandidateEmail(int candidateId)
        {
            return candidateDAO.SelectCandidateEmail(candidateId).Rows[0][0].ToString();
        }

        public DataTable SelectAllPendingApplicantPreboardingHired()
        {
            return candidateDAO.SelectAllPendingApplicantPreboardingHired();
        }

        public DataTable SelectAllPending()
        {
            return candidateDAO.SelectAll("pending");
        }

        public DataTable SelectAllApplicant()
        {
            return candidateDAO.SelectAll("applicant");
        }

        public DataTable SelectAllPreboarding()
        {
            return candidateDAO.SelectAll("preboarding");
        }

        public DataTable SelectAllHired()
        {
            return candidateDAO.SelectAll("hired");
        }

        public CandidateVO AuthenticateCandidate(int candidateId, string password)
        {
            DataTable dt = candidateDAO.SelectCandidate(candidateId);
            if (dt.Rows.Count > 0)
            {
                string encryptedPassword = dt.Rows[0][1].ToString();
                string decryptedPassword = passwordHelper.DecryptPassword(encryptedPassword);
                if (password.Equals(decryptedPassword))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        candidateVO.CandidateId = int.Parse(dr["candidate_id"].ToString());
                        candidateVO.Password = dr["password"].ToString();
                        candidateVO.Email = dr["email"].ToString();
                        candidateVO.Status = dr["status"].ToString();
                    }
                    return candidateVO;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void RegisterCandidate(int candidateId, string email)
        {
            string plainPassword = passwordHelper.GenerateRandomPassword();
            string encryptedPassword = passwordHelper.EncryptPassword(plainPassword);
            DataTable dt = hrBLO.SelectAllRecruiters();
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<li>" + dr[0].ToString() + " " + dr[1].ToString()
                        + " (" + dr[2].ToString() + ")</li>");
            }
            candidateDAO.RegisterCandidate(candidateId, encryptedPassword, email);
            emailHelper.SendEmail(email, "Online Application Form",
                    "<p>Hi " + email + ",</p>" +
                    "<br/>" +
                    "<p>Thank you for applying at Smart! We have registered you in our " +
                    "application system. Please use this account to access the online " +
                    "application form:</p>" +
                    "<br/>" +
                    "<p>Username: <strong>" + candidateId + "</strong><br/>" +
                    "Password: <strong>" + plainPassword + "</strong></p><br/>" +
                    "<p>Kindly fill out the application form <a href='#'>here</a>.</p>" +
                    "<br/>" +
                    "<p>If you need assistance, you can contact the following persons:</p>" +
                    "<ul>" + sb.ToString() + "</ul>" +
                    "<p>We wish you a pleasant experience at Smart and we hope to hear from " +
                    "you soon.</p>");
        }

        /// <summary>
        /// Registers candidates in the text file.
        /// </summary>
        /// <param name="stream">
        /// Stream of the text file.
        /// </param>
        /// <returns>
        /// Error or Success messages
        /// </returns>
        public string RegisterCandidatesFromTextFile(Stream stream)
        {
            try
            {
                TextFieldParser tfp = new TextFieldParser(stream);
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters("|");
                int candidateId;
                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    if (fields.Count() == 2 && int.TryParse(fields[0], out candidateId)
                            && fields[1].Contains("@"))
                    {
                        RegisterCandidate(int.Parse(fields[0]), fields[1]);  
                    }
                    else
                    {
                        return "Please upload a text file with the correct format. " +
                            "Make sure to follow this format: [Candidate ID]|[Email Address]";
                    }
                }
                tfp.Close();
                return "Candidates registered successfully";
            }
            catch (Exception e1)
            {
                Debug.WriteLine("Text File Parse Exception Type: " + e1.GetType() +
                    "\nMessage: " + e1.Message +
                    "\nStack Trace: " + e1.StackTrace);
                return "Sorry, an error occured. Please try again.";
            }
        }

        public void DeleteCandidate(int candidateId)
        {
            candidateDAO.ChangeStatus(candidateId, "deleted");
        }

        public void SubmittedForms(int candidateId)
        {
            candidateDAO.ChangeStatus(candidateId, "applicant");
        }

        public void ExportedToCSV(int candidateId)
        {
            candidateDAO.ChangeStatus(candidateId, "exported");
        }
        
        public void PreboardCandidate(int candidateId, string email)
        {
            DataTable dt = hrBLO.SelectAllPreboarders();
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<li>" + dr[0].ToString() + " " + dr[1].ToString()
                        + " (" + dr[2].ToString() + ")</li>");
            }
            candidateDAO.ChangeStatus(candidateId, "preboarding");
            emailHelper.SendEmail(email, "Pre-employed",
                "<p>Hi " + email + ",</p>" +
                "<br/>" +
                "<p>Congratulations! " +
                "Your application has been considered. Please comply " +
                "with the following requirements:</p>" +
                "<ul>" +
                "<li>Social Security Number (SSS)</li>" +
                "<li>Tax Identification Number (TIN)</li>" +
                "<li>PhilHealth</li>" +
                "<li>Pag-Ibig</li>" +
                "</ul>" +
                "<p>Kindly submit these documents to one of these persons:</p>" +
                "<ul>" + sb.ToString() + "</ul>"
                );
        }

        public void HireCandidate(int candidateId, string email, string hiringDate)
        {
            candidateDAO.Hire(candidateId, hiringDate);
            emailHelper.SendEmail(email, "Welcome Aboard!",
                "<p>Hi " + email + ",</p>" +
                "<br/>" +
                "<p>Welcome to the team! Let us help you start our journey together. " +
                "We created success plans just for you. Access these resources via " +
                "www.smartlife.com. We hope you have a fun and enjoyable experience with us.</p>");
        }

        public void NewPassword(int candidateId, string email)
        {
            string plainPassword = passwordHelper.GenerateRandomPassword();
            string encryptedPassword = passwordHelper.EncryptPassword(plainPassword);
            candidateDAO.NewPassword(candidateId, encryptedPassword);
            emailHelper.SendEmail(email, "Password Reset",
                    "<p>Hi " + email + ",</p>" +
                    "<br/>" +
                    "<p>Please use this password to access the system:</p>" +
                    "<br/>" +
                    "<p>Password: <strong>" + plainPassword + "</strong></p>" +
                    "<br/>" +
                    "<p>Access the website <a href='~/index.aspx'>here</a>.</p>");
        }
    }
}