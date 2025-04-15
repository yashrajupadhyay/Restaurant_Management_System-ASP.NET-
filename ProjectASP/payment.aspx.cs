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

        //protected void btnPlaceOrder_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        getcon(); // Ensure DB connection is open
        //        string userId = Session["UserId"]?.ToString();

        //        if (string.IsNullOrEmpty(userId))
        //        {
        //            Response.Redirect("Login.aspx");
        //            return;
        //        }

        //        // Begin Transaction
        //        SqlTransaction transaction = con.BeginTransaction();

        //        try
        //        {
        //            // Step 1: Get Cart Items
        //            SqlCommand getCartCmd = new SqlCommand("SELECT * FROM Cart WHERE UserId = @UserId", con, transaction);
        //            getCartCmd.Parameters.AddWithValue("@UserId", userId);
        //            SqlDataAdapter da = new SqlDataAdapter(getCartCmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);

        //            if (dt.Rows.Count == 0)
        //            {
        //                Response.Write("No items in cart.");
        //                return;
        //            }

        //            // Step 2: Insert each item into Order_tbl
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                SqlCommand insertCmd = new SqlCommand(@"
        //            INSERT INTO Order_tbl (Cart_Id, User_Id, Product_Id, OrderDate, Status)
        //            VALUES (@Cart_Id, @User_Id, @Product_Id, @OrderDate, @Status)", con, transaction);

        //                insertCmd.Parameters.Add("@Cart_Id", SqlDbType.Int).Value = row["Id"];
        //                insertCmd.Parameters.Add("@User_Id", SqlDbType.Int).Value = userId;
        //                insertCmd.Parameters.Add("@Product_Id", SqlDbType.Int).Value = row["ProductId"];
        //                insertCmd.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = DateTime.Now;
        //                insertCmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = "Pending";

        //                // Log the SQL query being executed
        //                string sqlQuery = insertCmd.CommandText;
        //                foreach (SqlParameter param in insertCmd.Parameters)
        //                {
        //                    sqlQuery = sqlQuery.Replace(param.ParameterName, param.Value.ToString());
        //                }
        //                Response.Write("Executing SQL: " + sqlQuery); // Log query for debugging

        //                insertCmd.ExecuteNonQuery();
        //            }

        //            // Step 3: Commit the transaction before deleting cart items
        //            transaction.Commit();

        //            // Step 4: Delete Cart Items after committing the transaction
        //            // Create a new connection to execute the delete commands outside of the transaction scope
        //            using (SqlConnection conn = new SqlConnection(con.ConnectionString))
        //            {
        //                conn.Open();
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    SqlCommand deleteCartCmd = new SqlCommand("DELETE FROM Cart WHERE Id = @CartId AND UserId = @UserId", conn);
        //                    deleteCartCmd.Parameters.AddWithValue("@CartId", row["Id"]);
        //                    deleteCartCmd.Parameters.AddWithValue("@UserId", userId);
        //                    deleteCartCmd.ExecuteNonQuery();
        //                }
        //            }

        //            // Redirect to confirmation page
        //            Response.Redirect("OrderConfirmation.aspx");
        //        }
        //        catch (Exception ex)
        //        {
        //            // Rollback if an error occurs
        //            transaction.Rollback();
        //            // Log the exception
        //            Response.Write("Error during order placement: " + ex.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle connection errors
        //        Response.Write("Error: " + ex.Message);
        //    }
        //}
        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            
            
                getcon(); // Ensure DB connection is open
                string userId = Session["UserId"]?.ToString();

                if (string.IsNullOrEmpty(userId))
                {
                    Response.Redirect("login2_master.aspx");
                    return;
                }

                // Begin Transaction
                SqlTransaction transaction = con.BeginTransaction();

                    // Step 1: Get Cart Items
                    SqlCommand getCartCmd = new SqlCommand("SELECT * FROM Cart WHERE UserId = @UserId", con, transaction);
                    getCartCmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataAdapter da = new SqlDataAdapter(getCartCmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        Response.Write("No items in cart.");
                        return;
                    }

                    // Step 2: Insert each item into Order_tbl
                    foreach (DataRow row in dt.Rows)
                    {
                        SqlCommand insertCmd = new SqlCommand(@"
                INSERT INTO Order_tbl (Cart_Id, User_Id, Product_Id, OrderDate, Status)
                VALUES (@Cart_Id, @User_Id, @Product_Id, @OrderDate, @Status)", con, transaction);

                        insertCmd.Parameters.Add("@Cart_Id", SqlDbType.Int).Value = row["Id"];
                        insertCmd.Parameters.Add("@User_Id", SqlDbType.Int).Value = userId;
                        insertCmd.Parameters.Add("@Product_Id", SqlDbType.Int).Value = row["ProductId"];
                        insertCmd.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = DateTime.Now;
                        insertCmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = "Pending";

                        // Remove the logging line to prevent SQL execution details from displaying
                        // Response.Write("Executing SQL: " + sqlQuery); // Log query for debugging

                        insertCmd.ExecuteNonQuery();
                    }

                    // Step 3: Commit the transaction before deleting cart items
                    transaction.Commit();

                    // Step 4: Delete Cart Items after committing the transaction
                    //using (SqlConnection conn = new SqlConnection(con.ConnectionString))
                    //{
                       
                    //    foreach (DataRow row in dt.Rows)
                    //    {
                    //        SqlCommand deleteCartCmd = new SqlCommand("DELETE FROM Cart WHERE Id = @CartId AND UserId = @UserId", conn);
                    //        deleteCartCmd.Parameters.AddWithValue("@CartId", row["Id"]);
                    //        deleteCartCmd.Parameters.AddWithValue("@UserId", userId);
                    //        deleteCartCmd.ExecuteNonQuery();
                    //    }
                    //}

                    // Redirect to confirmation page
                    Response.Redirect("OrderConfirmation.aspx");
                
                
            
        }

    }
}
