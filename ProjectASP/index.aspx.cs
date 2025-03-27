using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;

namespace ProjectASP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;
        protected void Page_Load(object sender, EventArgs e)
        {
            getcon();
             lblUser.ForeColor = System.Drawing.Color.White;
            // Debugging: Print session values to the Output Window
            System.Diagnostics.Debug.WriteLine("Session Check: UserEmail = " + (Session["UserEmail"] ?? "NULL"));

            if (Session["UserEmail"] != null)
            {
                lblUser.Text = "Welcome, " + Session["UserEmail"].ToString();
            }
            else
            {
                lblUser.Text = "Welcome, Guest!";
              //  ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Session is not active! Please log in.');", true);
            }
        }

        void getcon()
        {
            cs = new Class1();
            cs.startcon();
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtdate.Text = (Calendar1.SelectedDate.ToShortDateString() + " " + txttime.Text + " " + DropDownList1.SelectedItem).ToString();
            Calendar1.Visible = false;
        }

        protected void btnselectdate_Click(object sender, EventArgs e)
        {
            Calendar1.Visible = true;

        }

        protected void btnbooking_Click(object sender, EventArgs e)
        {
            if (btnbooking.Text == "Book Now")
            {
                getcon();
                cs.insert_booking(txtname.Text, txtemail.Text, txtdate.Text, txtpeople.Text, txtrequest.Text);

            }
        }
    }
}