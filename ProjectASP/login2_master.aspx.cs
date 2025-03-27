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
            cmd = new SqlCommand("select Role from SignUp_tbl where Email='" + txteml.Text + "' and Password='" + txtpass.Text + "'", cs.startcon());
            object role = cmd.ExecuteScalar();

            if (role != null)
            {
                // Set Session
                Session["UserRole"] = role.ToString();
                Session["UserEmail"] = txteml.Text;
               // Session["Name"] = txtname.Text;
                // Debugging Statements
                System.Diagnostics.Debug.WriteLine("Session UserEmail: " + Session["UserEmail"]);
               // System.Diagnostics.Debug.WriteLine("Session Name: " + Session["Name"]);
                System.Diagnostics.Debug.WriteLine("Session UserRole: " + Session["UserRole"]);

                if (role.ToString() == "Admin")
                {
                    Response.Redirect("admin_dashboard.aspx");
                }
                else
                {
                    Response.Redirect("index.aspx");
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