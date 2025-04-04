using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectASP
{
    public partial class order : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;
        PagedDataSource pg;
        int row;
        int p, pid;

        void getcon()
        {
            cs = new Class1();
            con = cs.startcon();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                SetDefaultCategory();
                ViewState["pid"] = 0; // Initialize pagination index
            }
        }

        void SetDefaultCategory()
        {
            try
            {
                getcon();
                cmd = new SqlCommand("SELECT Id FROM Categories WHERE Name = 'Breakfast'", con);
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    ddlCategory.SelectedValue = result.ToString();
                    ViewState["pid"] = 0; // Reset pagination
                    display();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error loading default category: " + ex.Message + "');</script>");
            }
        }

        void LoadCategories()
        {
            try
            {
                getcon();
                cmd = new SqlCommand("SELECT Id, Name FROM Categories", con);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlCategory.DataSource = dt;
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select Category", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error loading categories: " + ex.Message + "');</script>");
            }
        }

        void display()
        {
            try
            {
                getcon();
                int categoryId;
                if (!int.TryParse(ddlCategory.SelectedValue, out categoryId) || categoryId <= 0)
                {
                    dlProducts.DataSource = null;
                    dlProducts.DataBind();
                    Response.Write("<script>alert('Please select a valid category');</script>");
                    return;
                }

                da = new SqlDataAdapter("SELECT * FROM Products WHERE CategoryId = @CategoryId", con);
                da.SelectCommand.Parameters.AddWithValue("@CategoryId", categoryId);
                ds = new DataSet();
                da.Fill(ds);
                row = ds.Tables[0].Rows.Count;

                if (row == 0)
                {
                    dlProducts.DataSource = null;
                    dlProducts.DataBind();
                    Response.Write("<script>alert('No products found for this category!');</script>");
                    return;
                }

                pg = new PagedDataSource
                {
                    AllowPaging = true,
                    PageSize = 3,
                    DataSource = ds.Tables[0].DefaultView,
                    CurrentPageIndex = Convert.ToInt32(ViewState["pid"])
                };

                btnPrev.Enabled = !pg.IsFirstPage;
                btnNext.Enabled = !pg.IsLastPage;

                dlProducts.DataSource = pg;
                dlProducts.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error fetching products: " + ex.Message + "');</script>");
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["pid"] = 0;
            display();
        }





        protected void btnBookTable_Click1(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Session["ReturnUrl"] = "booking.aspx";
                Response.Redirect("login2_master.aspx");
            }
            else
            {
                Response.Redirect("booking.aspx");
            }
        }



        protected void dlProducts_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "cmd_detailV")
            {
                int pid = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("ViewDetail.aspx?pid=" + pid);
            }
            else if (e.CommandName == "AddToCart")
            {
                if (Session["UserID"] == null)
                {
                    Session["ReturnUrl"] = "order.aspx";
                    Response.Redirect("login2_master.aspx");
                    return;
                }

                int productId = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(Session["UserID"]);

                try
                {
                    getcon();

                    // Check if product already exists in the cart
                    cmd = new SqlCommand("SELECT Quantity FROM Cart WHERE UserId = @UserId AND ProductId = @ProductId", con);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    object existingQuantity = cmd.ExecuteScalar();

                    if (existingQuantity != null)
                    {
                        int quantity = Convert.ToInt32(existingQuantity) + 1;
                        cmd = new SqlCommand("UPDATE Cart SET Quantity = @Quantity WHERE UserId = @UserId AND ProductId = @ProductId", con);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT Price FROM Products WHERE Id = @ProductId", con);
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        object priceObj = cmd.ExecuteScalar();

                        if (priceObj != null)
                        {
                            decimal price = Convert.ToDecimal(priceObj);

                            cmd = new SqlCommand("INSERT INTO Cart (UserId, ProductId, Quantity, Price) VALUES (@UserId, @ProductId, @Quantity, @Price)", con);
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@ProductId", productId);
                            cmd.Parameters.AddWithValue("@Quantity", 1);
                            cmd.Parameters.AddWithValue("@Price", price);
                        }
                        else
                        {
                            Response.Write("<script>alert('Error retrieving product price');</script>");
                        }
                    }

                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Item added to cart successfully!');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error adding to cart: " + ex.Message + "');</script>");
                }
            }
        }
        protected void btnNext_Click1(object sender, EventArgs e)
        {
            btnPrev.Enabled = true;
            p += Convert.ToInt32(ViewState["pid"]) + 1;
            ViewState["pid"] = Convert.ToInt32(p);
            if (p == 0)
            {
                btnPrev.Enabled = false;
            }
            display();
        }

        protected void btnPrev_Click1(object sender, EventArgs e)
        {

            btnPrev.Enabled = true;
            p = Convert.ToInt32(ViewState["pid"]) - 1;
            ViewState["pid"] = p;

            // Ensure `pg` is initialized
            display();

            if (pg != null)  // ✅ Check if `pg` is initialized
            {
                int temp = row / pg.PageSize;
                if (p == temp)
                {
                    btnNext.Enabled = false;
                }
            }
        }

    }
}