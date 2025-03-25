using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjectASP
{
    public class DbHelper
    {
        private static string connString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        // Method to get all categories
        public static DataTable GetCategories()
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Categories", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Method to get products by category
        public static DataTable GetProductsByCategory(int categoryId)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE CategoryId = @CategoryId", con);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
