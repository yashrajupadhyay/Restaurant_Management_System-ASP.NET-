using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

using CrystalDecisions.Shared;
namespace ProjectASP.Admin
{
    public partial class BookingDetails : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;

        private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        static string Crypath = "";

        void getcon()
        {
            cs = new Class1();
            con = cs.startcon();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getcon();
                fillgrid();
            }
        }

        void fillgrid()
        {
            getcon();
            string query = "SELECT Id, Name, Email, Date, People, Request FROM booking_tbl";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);

            GridView1.DataSource = ds;
            GridView1.DataBind();

            con.Close();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd_delete")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                delete_booking(id);
                fillgrid();
            }
        }

        void delete_booking(int id)
        {
            getcon();
            string query = "DELETE FROM booking_tbl WHERE Id = @ID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            getcon();
            da = new SqlDataAdapter("select * from booking_tbl  ", cs.startcon());
            ds = new DataSet();
            da.Fill(ds);
            string xml = @"D:/Programming/Asp.Net/ProjectASP/ProjectASP/data2.xml";
            ds.WriteXmlSchema(xml);

            Crypath = @" D:/Programming/Asp.Net/ProjectASP/ProjectASP/BTD.rpt";
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            cr.Database.Tables[0].SetDataSource(ds);
            cr.Refresh();
            CrystalReportViewer1.ReportSource = cr;
           


            cr.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "BookTableDetail");
        }
    }
}
