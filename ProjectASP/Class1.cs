using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace ProjectASP
{
    public class Class1
    {

        string s = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        public SqlConnection startcon()
        {
            con = new SqlConnection(s);
            con.Open();
            return con;
        }

        public void insert(string name , string email, string subject , string message)
        {
            cmd = new SqlCommand("insert into contact_tbl (Name,Email,Subject,Message)values('"+name+"','"+email+"','"+subject+"','"+message+"')", con);
            cmd.ExecuteNonQuery();
        }
        public void insert_addDetails(string name, string ph, string add)
        {
            string query = "INSERT INTO AddressDetail1_tbl (Name, PhoneNo, Address) VALUES ('" + name + "', '" + ph + "', '" + add + "')";
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
        }


        public void addCategory(string name)
        {
            cmd = new SqlCommand("insert into Categories(Name) values('" + name + "')", con);
            cmd.ExecuteNonQuery();
        }

        public void insert_booking(string name,string Email,string Date , string People , string Request)
        {
            cmd = new SqlCommand("insert into booking_tbl (Name,Email,Date,People,Request)values('" + name + "','" + Email + "','" + Date + "','" + People + "','" + Request + "')", con);
            cmd.ExecuteNonQuery();
        }

        //public void signup_insert(string name , string email , string pass)
        //{
        //    cmd = new SqlCommand("insert into SignUp_tbl (Name , Email , Password)values('"+name+"','"+email+"','"+pass+"')", con);
        //    cmd.ExecuteNonQuery();
        //}
        public int signup_insert(string name, string email, string password, string role)
        {
            int result = 0;

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO SignUp_tbl (Name, Email, Password, Role) VALUES (@Name, @Email, @Password, @Role)", startcon());
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log error or handle accordingly
                Console.WriteLine(ex.Message);
            }

            return result;
        }



        //public void insertProduct(string nm, string des, decimal price,string img)
        //{
        //    cmd = new SqlCommand("insert into Products(Name,Description,Price,Image)values('" + nm + "','" + des + "','" + price + "','" + img + "')", startcon());
        //    cmd.ExecuteNonQuery();
        //}
        public void insertProduct(string nm, string des, decimal price, string img, int categoryId)
        {
            cmd = new SqlCommand("INSERT INTO Products (Name, Description, Price, Image, CategoryID) VALUES (@Name, @Description, @Price, @Image, @CategoryID)", startcon());

            cmd.Parameters.AddWithValue("@Name", nm);
            cmd.Parameters.AddWithValue("@Description", des);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Image", img);
            cmd.Parameters.AddWithValue("@CategoryID", categoryId);

            cmd.ExecuteNonQuery();
        }

        public void updateProduct(int id, string name, string des, decimal price, int catid, string img)
        {
            startcon();
            string query = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, CategoryId = @CategoryId, Image = @Image WHERE Id = @Id";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Description", des);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@CategoryId", catid);
            cmd.Parameters.AddWithValue("@Image", img);

            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void delete_product(int id)
        {
            startcon();
            cmd = new SqlCommand("DELETE FROM Products WHERE Id='" + id + "';", con);
            cmd.ExecuteNonQuery();
        }
        public void delete_user(int id )
        {
            startcon();
            cmd = new SqlCommand("DELETE FROM SignUp_tbl WHERE Id='" + id + "';", con);
            cmd.ExecuteNonQuery();
        }

        public DataSet filldata()
        {
            da = new SqlDataAdapter("select * from Products", con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet select(int id)
        {
            da = new SqlDataAdapter("select * from Products where Id='" + id + "'", con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //internal void insertProduct(string text1, string text2, string text3, string fnm)
        //{
        //    throw new NotImplementedException();
        //}

        ////internal void insertProduct(string text1, string text2, string text3, string fnm)
        ////{
        ////    throw new NotImplementedException();
        ////}

        //internal void insertProduct(string text1, string text2, string text3, string fnm)
        //{
        //    throw new NotImplementedException();
        //}
    }
}