using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace ProjectASP
{
    
    public partial class ViewDetail : System.Web.UI.Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;
        protected void Page_Load(object sender, EventArgs e)
        {
            getcon();
            display();
        }
        void getcon()
        {
            cs = new Class1();
            cs.startcon();
        }

        void display()
        {
            if (Convert.ToInt32(Request.QueryString["pid"]) != 0)
            {
                getcon();
                da = new SqlDataAdapter("select * from Products where Id='" + Request.QueryString["pid"] + "' ", cs.startcon());
                ds = new DataSet();
                da.Fill(ds);
                DataList1.DataSource = ds;
                DataList1.DataBind();
            }
        }

        protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}