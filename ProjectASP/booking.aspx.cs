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

    public partial class booking : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;
        protected void Page_Load(object sender, EventArgs e)
        {
            getcon();
        }

        void getcon()
        {
            cs = new Class1();
            cs.startcon();
        }


        protected void btnbooking_Click(object sender, EventArgs e)
        {
            if(btnbooking.Text== "Book Now")
            {
                getcon();
                cs.insert_booking(txtname.Text, txtemail.Text, txtdate.Text, txtpeople.Text, txtrequest.Text);

            }

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

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}