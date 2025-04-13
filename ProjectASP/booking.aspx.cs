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

        //private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        //static string Crypath = "";

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
            getcon();

            // Check if the user is logged in
            if (Session["UserID"] == null)
            {
                // Store booking details in session before redirecting
                Session["Name"] = txtname.Text;
                Session["Email"] = txtemail.Text;
                Session["Date"] = txtdate.Text;
                Session["People"] = txtpeople.Text;
                Session["Request"] = txtrequest.Text;

                // Store return URL
                Session["ReturnUrl"] = "booking.aspx";

                // Redirect user to login page
                Response.Redirect("login2_master.aspx");
                return;
            }

            // User is logged in, proceed with booking
            cs.insert_booking(txtname.Text, txtemail.Text, txtdate.Text, txtpeople.Text, txtrequest.Text);

            // Show success message
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Booking Successful!');", true);
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

        protected void btnBookTable_Click1(object sender, EventArgs e)
        {

            if (Session["UserID"] == null) // If user is not logged in
            {
                Session["ReturnUrl"] = "booking.aspx"; // Store return URL before redirecting
                Response.Redirect("login2_master.aspx"); // Redirect to login
                return;
            }
        }

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    da = new SqlDataAdapter("select * from booking_tbl  ", cs.startcon());
        //    ds = new DataSet();
        //    da.Fill(ds);
        //    string xml = @"D:/Programming/Asp.Net/ProjectASP/ProjectASP/data.xml";
        //    ds.WriteXmlSchema(xml);

        //    Crypath = @"D:/Programming/Asp.Net/ProjectASP/ProjectASP/BookTable.rpt";
        //    cr.Load(Crypath);
        //    cr.SetDataSource(ds);
        //    cr.Database.Tables[0].SetDataSource(ds);
        //    cr.Refresh();
        //    CrystalReportViewer1.ReportSource = cr;

        //    cr.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "BookTable");


        //}
    }
}