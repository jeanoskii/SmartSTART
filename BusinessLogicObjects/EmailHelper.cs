using System;
using System.Diagnostics;
using System.Net.Mail;

namespace CAPRES.BusinessLogicObjects
{
    public class EmailHelper
    {
        public void SendEmail(string email, string subject, string body)
        {
            try
            {
                MailMessage mm = new MailMessage();
                SmtpClient sc = new SmtpClient("smtp.gmail.com");
                mm.From = new MailAddress("autobot.capres.smart@gmail.com", "SmartSTART AutoBot");
                mm.To.Add(email);
                mm.Subject = subject;
                mm.IsBodyHtml = true;
                mm.Body = body +
                    "<br />" +
                    "<p>Live more!</p>" +
                    "<br/>" +
                    "<br/>" +
                    "<hr>" +
                    "<p style='color:#6D6D6D;font-size:10px;font-style:italic'>This is an auto-" +
                    "generated email sent to <a href='" + email + "'>" + email + "</a>. " +
                    "For inquiries, please contact <a href='#'>support@smart.com.ph</a>.</p>";
                sc.Port = 587;
                sc.Credentials = new System.Net.NetworkCredential("autobot.capres.smart@gmail.com",
                    "transformandrollout");
                sc.EnableSsl = true;
                sc.Send(mm);
                Debug.WriteLine("Send Email success.");
                mm.Dispose();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Send Email Exception Type: " + e.GetType() +
                    "\nMessage: " + e.Message +
                    "\nStack Trace: " + e.StackTrace);
            }
        }
    }
}