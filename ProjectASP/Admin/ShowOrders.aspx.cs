using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectASP.Admin
{
    public partial class ShowOrders : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs = new Class1();

        void getcon()
        {
            con = cs.startcon();
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillgrid();
            }
        }

        void fillgrid()
        {
            try
            {
                getcon();
                string query = "SELECT Order_Id, Cart_Id, User_Id, Product_Id, OrderDate, Status FROM Order_tbl";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
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
            try
            {
                getcon();
                string query = "DELETE FROM Order_tbl WHERE Order_Id = @ID";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Delete Error: " + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
        }
    }
}
