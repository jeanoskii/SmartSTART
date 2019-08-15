using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CAPRES
{
    public partial class SmartStart : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            if (Request.Cookies["Candidate"] != null)
            {
                HttpCookie cookie = new HttpCookie("Candidate");
                cookie.Expires = DateTime.Now.AddYears(-2);
                Response.Cookies.Add(cookie);
            }
            else if (Request.Cookies["HR"] != null)
            {
                HttpCookie cookie = new HttpCookie("HR");
                cookie.Expires = DateTime.Now.AddYears(-2);
                Response.Cookies.Add(cookie);
            }
            Response.Redirect("index.aspx");
        }
    }
}