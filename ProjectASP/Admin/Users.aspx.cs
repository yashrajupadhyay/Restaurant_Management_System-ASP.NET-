using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectASP.Admin
{
    public partial class Users : System.Web.UI.Page
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
            if (con.State == ConnectionState.Closed)
            {
                con.Open();  // 🔥 Fix: Ensure connection is open
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
            string query = "SELECT ID, Name, Email,Role, Password FROM SignUp_tbl";
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
            if (e.CommandName == "cmd_dtl")  // 🔥 Fixed: Using correct command name
            {
                int id = Convert.ToInt32(e.CommandArgument);
                delete_user(id);
                fillgrid();
            }
        }

        void delete_user(int id)
        {
            getcon();
            string query = "DELETE FROM SignUp_tbl WHERE ID = @ID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
