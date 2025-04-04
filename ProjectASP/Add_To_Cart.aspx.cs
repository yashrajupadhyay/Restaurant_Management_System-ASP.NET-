using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectASP
{
    public partial class addtocart : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.GridView gvCart;
        protected global::System.Web.UI.WebControls.Label lblGrandTotal;
        protected global::System.Web.UI.WebControls.Button btnCheckout;

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        Class1 cs;

        void getcon()
        {
            cs = new Class1();
            con = cs.startcon();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("login2_master.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadCart();
            }
        }

        void LoadCart()
        {
            try
            {
                getcon();
                int userId = Convert.ToInt32(Session["UserID"]);

                string query = @"
                    SELECT C.Id, P.Name AS ProductName, C.Quantity, P.Price, 
                           (C.Quantity * P.Price) AS TotalPrice
                    FROM Cart C
                    JOIN Products P ON C.ProductId = P.Id
                    WHERE C.UserId = @UserId";

                da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                dt = new DataTable();
                da.Fill(dt);

                gvCart.DataSource = dt;
                gvCart.DataBind();

                // Calculate Grand Total
                decimal grandTotal = 0;
                foreach (DataRow row in dt.Rows)
                {
                    grandTotal += Convert.ToDecimal(row["TotalPrice"]);
                }
                lblGrandTotal.Text = "Grand Total: $" + grandTotal;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error loading cart: " + ex.Message + "');</script>");
            }
        }

        protected void gvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                getcon();
                int cartId = Convert.ToInt32(gvCart.DataKeys[e.RowIndex].Value);

                cmd = new SqlCommand("DELETE FROM Cart WHERE Id = @CartId", con);
                cmd.Parameters.AddWithValue("@CartId", cartId);
                cmd.ExecuteNonQuery();

                LoadCart();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error deleting item: " + ex.Message + "');</script>");
            }
        }

        protected void gvCart_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCart.EditIndex = e.NewEditIndex;
            LoadCart();
        }

        protected void gvCart_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCart.EditIndex = -1;
            LoadCart();
        }

        protected void gvCart_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                getcon();
                int cartId = Convert.ToInt32(gvCart.DataKeys[e.RowIndex].Value);
                TextBox txtQuantity = (TextBox)gvCart.Rows[e.RowIndex].FindControl("txtQuantity");
                int quantity = Convert.ToInt32(txtQuantity.Text);

                cmd = new SqlCommand("UPDATE Cart SET Quantity = @Quantity WHERE Id = @CartId", con);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@CartId", cartId);
                cmd.ExecuteNonQuery();

                gvCart.EditIndex = -1;
                LoadCart();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error updating quantity: " + ex.Message + "');</script>");
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx");
        }
    }
}
