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
    public partial class login : System.Web.UI.Page
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;
        int i;
        protected void Page_Load(object sender, EventArgs e)
        {
            getcon();

        }
        void getcon()
        {
            cs = new Class1();
            cs.startcon();
        }

        protected void signin_Click(object sender, EventArgs e)
        {
            getcon();
            cmd = new SqlCommand("select count(*) from SignUp_tbl where Email='" + txteml.Text + "' and Password='" + txtpass.Text + "'", cs.startcon());
            i = Convert.ToInt32(cmd.ExecuteScalar());
            if (i > 0)
            {
                Response.Redirect("index.aspx");
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
                cs.signup_insert(txtname.Text, txtemail_signup.Text, txtpswd.Text);
            }
        }

        protected void signin_Click1(object sender, EventArgs e)
        {

        }
    }
}