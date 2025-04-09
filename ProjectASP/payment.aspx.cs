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
    public partial class payment : System.Web.UI.Page
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
            getcon();
            if (!IsPostBack)
            {
                LoadCartSummary();
            }
        }
        private void LoadCartSummary()
        {
            getcon(); // Initialize your connection
            string userId = Session["UserId"].ToString();
            decimal totalAmount = 0;

            SqlCommand cmd = new SqlCommand("SELECT SUM(Quantity * Price) FROM Cart WHERE UserId = @UserId", con);
            
                cmd.Parameters.AddWithValue("@UserId", userId);
                

                object result = cmd.ExecuteScalar();
               

                if (result != DBNull.Value)
                {
                    totalAmount = Convert.ToDecimal(result);
                }

                ltlTotalAmount.Text = $"<h3>Total Amount - ₹{totalAmount:F2}</h3>";
            
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            string userId = Session["UserId"].ToString();
            getcon();

            // Step 1: Get Cart Items
            SqlCommand getCartCmd = new SqlCommand("SELECT * FROM Cart WHERE UserId = @UserId", con);
            getCartCmd.Parameters.AddWithValue("@UserId", userId);
            SqlDataAdapter da = new SqlDataAdapter(getCartCmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Step 2: Insert Each Cart Item into Order_tbl
            foreach (DataRow row in dt.Rows)
            {
                SqlCommand insertCmd = new SqlCommand(@"
            INSERT INTO Order_tbl (Cart_Id, User_Id, Product_Id)
            VALUES (@Cart_Id, @User_Id, @Product_Id)", con);

                insertCmd.Parameters.AddWithValue("@Cart_Id", row["Id"]);
                insertCmd.Parameters.AddWithValue("@User_Id", row["UserId"]);
                insertCmd.Parameters.AddWithValue("@Product_Id", row["ProductId"]);

                insertCmd.ExecuteNonQuery();
            }

            // Step 3: First delete from Order_tbl to remove dependency (optional safety step)
            SqlCommand deleteOrderCmd = new SqlCommand(@"
        DELETE FROM Order_tbl 
        WHERE Cart_Id IN (SELECT Id FROM Cart WHERE UserId = @UserId)", con);
            deleteOrderCmd.Parameters.AddWithValue("@UserId", userId);
            deleteOrderCmd.ExecuteNonQuery();

            // Step 4: Delete from Cart
            SqlCommand deleteCartCmd = new SqlCommand("DELETE FROM Cart WHERE UserId = @UserId", con);
            deleteCartCmd.Parameters.AddWithValue("@UserId", userId);
            deleteCartCmd.ExecuteNonQuery();

            // Step 5: Redirect to confirmation
            Response.Redirect("OrderConfirmation.aspx");
        }


    }
}