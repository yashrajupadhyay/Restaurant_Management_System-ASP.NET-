using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ProjectASP.Admin
{
    public partial class ShowOrders : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                getcon();
                fillgrid();
            }
        }
        void fillgrid()
        {
            getcon();
            string query = "SELECT Order_Id, Cart_Id, User_Id, Product_Id, OrderDate, Status FROM Order_tbl";
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
                delete_order(id);
                fillgrid();
            }
        }

        void delete_order(int id)
        {
            getcon();
            string query = "DELETE FROM Order_tbl WHERE Order_Id = @ID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}