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
    public partial class AddToCart : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;

        void getcon()
        {
            cs = new Class1();
            con = cs.startcon();  // ✅ Assign the returned connection
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            getcon();
            if (!IsPostBack)
            {
                BindCartData();
            }
        }


        private void BindCartData()
        {
            getcon();
            if (Session["UserId"] != null)
            {
                int userId = Convert.ToInt32(Session["UserId"]);

                string query = @"
            SELECT 
                C.Id AS CartId,
                C.ProductId, -- ✅ Required for CommandArgument
                P.Name,
                P.Description,
                P.Image,
                C.Quantity,
                C.Price,
                (C.Quantity * C.Price) AS TotalPrice
            FROM Cart C
            INNER JOIN Products P ON C.ProductId = P.Id
            WHERE C.UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataListCart.DataSource = dt;
                    DataListCart.DataBind();
                }
            }
            else
            {
                Response.Redirect("login2_master.aspx");
            }
        }

        protected void DataListCart_ItemCommand(object source, DataListCommandEventArgs e)
        {
            getcon();

            if (e.CommandName == "RemoveItem")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                string userId = Session["UserId"].ToString();

                string query = "DELETE FROM Cart WHERE ProductId = @ProductId AND UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    
                    cmd.ExecuteNonQuery();
                    
                }

                BindCartData(); // Refresh the cart list
            }
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("addDetails.aspx");
        }

        protected void DataListCart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //protected void DataListCart_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
    }
}