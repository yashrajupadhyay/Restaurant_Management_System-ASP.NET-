using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace ProjectASP
{
    public partial class login2_master : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        Class1 cs;

        void getcon()
        {
            cs = new Class1();
            cs.startcon();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getcon();
            }
        }

        protected void signin_Click(object sender, EventArgs e)
        {
            getcon();

            if (string.IsNullOrWhiteSpace(txteml.Text) || string.IsNullOrWhiteSpace(txtpass.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Email and Password are required!');", true);
                return;
            }

            if (!txteml.Text.Contains("@"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Enter a valid email address!');", true);
                return;
            }

            cmd = new SqlCommand("SELECT ID, Role FROM SignUp_tbl WHERE Email=@Email AND Password=@Password", cs.startcon());
            cmd.Parameters.AddWithValue("@Email", txteml.Text);
            cmd.Parameters.AddWithValue("@Password", txtpass.Text);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Session["UserID"] = reader["ID"].ToString();
                Session["UserRole"] = reader["Role"].ToString();
                Session["UserEmail"] = txteml.Text;
                reader.Close();

                Response.Redirect(Session["UserRole"].ToString().Equals("Admin", StringComparison.OrdinalIgnoreCase) ? "Admin/admin.aspx" : "booking.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid Email or Password!');", true);
            }
        }

        protected void btnsignup_Click(object sender, EventArgs e)
        {
            getcon();

            if (string.IsNullOrWhiteSpace(txtname.Text) || string.IsNullOrWhiteSpace(txtemail_signup.Text) ||
                string.IsNullOrWhiteSpace(txtpswd.Text) || ddlRole.SelectedIndex == -1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('All fields are required!');", true);
                return;
            }

            if (!txtemail_signup.Text.Contains("@"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Enter a valid email address!');", true);
                return;
            }

            int result = cs.signup_insert(txtname.Text, txtemail_signup.Text, txtpswd.Text, ddlRole.SelectedValue);

            ClientScript.RegisterStartupScript(this.GetType(), "alert", result > 0 ?
                "alert('Signup Successful! Please login.')" : "alert('Signup Failed. Try Again.')", true);
        }
    }
}
