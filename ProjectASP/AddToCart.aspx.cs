using System;
using System.Data;
using System.Data.SqlClient;

namespace ProjectASP
{
    public partial class AddToCart : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
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
                LoadCartItems();
            }
        }

        void LoadCartItems()
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("login2_master.aspx");
                return;
            }

            getcon();
            int userId = Convert.ToInt32(Session["UserID"]);

            string query = @"
                SELECT 
                    P.Name AS ProductName,
                    C.Quantity,
                    C.Price,
                    (C.Quantity * C.Price) AS Total
                FROM Cart C
                JOIN Products P ON C.ProductId = P.Id
                WHERE C.UserId = @UserId";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvCart.DataSource = dt;
            gvCart.DataBind();
        }
    }
}
