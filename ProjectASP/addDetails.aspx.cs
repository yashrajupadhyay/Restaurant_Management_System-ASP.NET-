using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace ProjectASP
{
    public partial class addDetails : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;

        void getcon()
        {
            cs = new Class1();
            con = cs.startcon();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            getcon();
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            getcon();
            cs.insert_addDetails(txtName.Text, txtPhone.Text, txtAddress.Text);
            Response.Redirect("payment.aspx");

        }
    }
}