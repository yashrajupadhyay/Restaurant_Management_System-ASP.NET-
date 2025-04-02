using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectASP.Admin
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear session variables related to the admin user
            Session["UserID"] = null;
            Session["UserRole"] = null;
            Session["UserEmail"] = null;

            // Optional: Abandon the session to clear all session data
            Session.Abandon();

            // Redirect the user to the login page (admin login page)
            Response.Redirect("login2_master.aspx"); // Replace with your admin login page
        }

    }
}