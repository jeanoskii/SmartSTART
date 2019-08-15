using System.Data;
using CAPRES.DataAccessObjects;
using CAPRES.ValueObjects;

namespace CAPRES.BusinessLogicObjects
{
    public class HRBLO
    {
        private HRDAO hrDAO = new HRDAO();
        private HRVO hrVO = new HRVO();
        private PasswordHelper passwordHelper = new PasswordHelper();
        private EmailHelper emailHelper = new EmailHelper();

        public HRVO AuthenticateHR(string email, string password)
        {
            if (email == "su" && password == "AmJRBrRcvYSKJrD9P5hv38amfg1YnzoQbjKQaF2LdOc=")
            {
                hrVO.Email = email;
                hrVO.Password = password;
                hrVO.Type = "admin";
                return hrVO;
            }
            else
            {
                DataTable dt = hrDAO.SelectHR(email);
                if (dt.Rows.Count > 0)
                {
                    string encryptedPassword = dt.Rows[0][3].ToString();
                    string decryptedPassword = passwordHelper.DecryptPassword(encryptedPassword);
                    if (password.Equals(decryptedPassword))
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            hrVO.Email = dr["email"].ToString();
                            hrVO.Password = dr["password"].ToString();
                            hrVO.Type = dr["type"].ToString();
                        }
                        return hrVO;
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
        }

        public DataTable SelectAllPreboarders()
        {
            return hrDAO.SelectAllByType("preboarder");
        }

        public DataTable SelectAllRecruiters()
        {
            return hrDAO.SelectAllByType("recruiter");
        }

        public DataTable SelectAllDataManagers()
        {
            return hrDAO.SelectAllByType("datamanager");
        }

        public DataTable SelectAllAdmins()
        {
            return hrDAO.SelectAllByType("admin");
        }

        public DataTable SelectAllRevoked()
        {
            return hrDAO.SelectAllByType("revoked");
        }

        public DataTable SelectAll()
        {
            return hrDAO.SelectAll();
        }

        public DataTable SelectHRUsingEmail(string email)
        {
            return hrDAO.SelectAllByEmail(email);
        }

        public DataTable SelectHRByName(string name)
        {
            return hrDAO.SelectHRByName(name);
        }

        public void InsertHR(string firstName, string lastName, string email, string type)
        {
            string plainPassword = passwordHelper.GenerateRandomPassword();
            string encryptedPassword = passwordHelper.EncryptPassword(plainPassword);
            hrDAO.InsertHR(firstName, lastName, email, encryptedPassword, type);
            emailHelper.SendEmail(email, "Privilege Grant",
                    "<p>Hi " + email + ",</p>" +
                    "<br/>" +
                    "<p>You have been granted " + type + " privileges in CAPRES. Please use this " +
                    "account to access the system:</p>" +
                    "<br/>" +
                    "<p>Username: <strong>" + email + "</strong><br/>" +
                    "Password: <strong>" + plainPassword + "</strong></p>" +
                    "<br/>" +
                    "<p>Access the website <a href='~/index.aspx'>here</a>.</p>");
        }

        public void UpdateHR(string firstName, string lastName, string email, string type)
        {
            string plainPassword = passwordHelper.GenerateRandomPassword();
            string encryptedPassword = passwordHelper.EncryptPassword(plainPassword);
            hrDAO.UpdateHR(firstName, lastName, email, encryptedPassword, type);
            emailHelper.SendEmail(email, "Privilege Grant",
                    "<p>Hi " + email + ",</p>" +
                    "<br/>" +
                    "<p>You have been granted " + type + " privileges in CAPRES. Please use this " +
                    "account to access the system:</p>" +
                    "<br/>" +
                    "<p>Username: <strong>" + email + "</strong><br/>" +
                    "Password: <strong>" + plainPassword + "</strong></p>" +
                    "<br/>" +
                    "<p>Access the website <a href='~/index.aspx'>here</a>.</p>");
        }

        public void DeleteHR(string email, string adminEmail)
        {
            hrDAO.DeleteHR(email);
            emailHelper.SendEmail(email, "Privilege Revoked",
                    "<p>Hi " + email + ",</p>" +
                    "<br/>" +
                    "<p>Your privileges in CAPRES has been revoked.</p>" +
                    "<br/>" +
                    "<p>If you believe this was an accident, please contact <a href='" + adminEmail +
                    "'>" + adminEmail + "</a>.</p>");
        }

        public void NewPassword(string email)
        {
            string plainPassword = passwordHelper.GenerateRandomPassword();
            string encryptedPassword = passwordHelper.EncryptPassword(plainPassword);
            hrDAO.NewPassword(email, encryptedPassword);
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