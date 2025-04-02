using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectASP
{
    public partial class Add_To_Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBookTable_Click1(object sender, EventArgs e)
        {


            if (Session["UserID"] == null) // If user is not logged in
            {
                Session["ReturnUrl"] = "booking.aspx"; // Store return URL before redirecting
                Response.Redirect("login2_master.aspx"); // Redirect to login
                return;
            }
        }
    }
}