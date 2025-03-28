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
    public partial class login2_master : System.Web.UI.Page
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;
        int i;
        void getcon()
        {
            cs = new Class1();
            cs.startcon();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            getcon();

        }

        protected void signin_Click(object sender, EventArgs e)
        {
            getcon();
            cmd = new SqlCommand("SELECT ID, Role FROM SignUp_tbl WHERE Email='" + txteml.Text + "' AND Password='" + txtpass.Text + "'", cs.startcon());
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                // Store user session
                Session["UserID"] = reader["ID"].ToString(); // Store user ID
                Session["UserRole"] = reader["Role"].ToString();
                Session["UserEmail"] = txteml.Text;

                reader.Close();

                // Check if booking details exist in session
                if (Session["Name"] != null)
                {
                    cs.insert_booking(Session["Name"].ToString(), Session["Email"].ToString(),
                                      Session["Date"].ToString(), Session["People"].ToString(),
                                      Session["Request"].ToString());

                    // Clear session after inserting booking
                    Session["Name"] = null;
                    Session["Email"] = null;
                    Session["Date"] = null;
                    Session["People"] = null;
                    Session["Request"] = null;

                    // Show alert for successful booking
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Booking Successful!');", true);
                }

                // Redirect user to the intended page or booking page
                if (Session["ReturnUrl"] != null)
                {
                    string returnUrl = Session["ReturnUrl"].ToString();
                    Session["ReturnUrl"] = null;
                    Response.Redirect(returnUrl);
                }
                else
                {
                    Response.Redirect("booking.aspx");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid Email or Password!');", true);
            }
        }




        protected void btnsignup_Click(object sender, EventArgs e)
        {
            if (btnsignup.Text == "SIGN UP")
            {
                getcon();
                cmd = new SqlCommand("INSERT INTO SignUp_tbl (Name, Email, Password, Role) VALUES ('" + txtname.Text + "', '" + txtemail_signup.Text + "', '" + txtpswd.Text + "', '" + ddlRole.SelectedValue + "')", cs.startcon());
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Signup Successful! Please login.');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Signup Failed. Try Again.');", true);
                }
            }
        }

    }
}