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

        // Establish database connection
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
                //SetDefaultCategory();
                ViewState["pid"] = 0; // Initialize page index for pagination
            }
            //display();
        }
        //void SetDefaultCategory()
        //{
        //    try
        //    {
        //        getcon();
        //        cmd = new SqlCommand("SELECT Id FROM Categories WHERE Name = 'Breakfast'", con);
        //        object result = cmd.ExecuteScalar();

        //        if (result != null)
        //        {
        //            ddlCategory.SelectedValue = result.ToString();
        //            ViewState["pid"] = 0; // Reset pagination
        //            display(); // Load products for Breakfast
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('Error loading default category: " + ex.Message + "');</script>");
        //    }
        //}
        // Load Categories into DropDownList
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

        // Display paginated products based on selected category
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

                // Pagination setup
                pg = new PagedDataSource();
                pg.AllowPaging = true;
                pg.PageSize = 3; // Number of products per page
                pg.DataSource = ds.Tables[0].DefaultView;
                pg.CurrentPageIndex = Convert.ToInt32(ViewState["pid"]);

                // Enable/Disable Navigation Buttons
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

        // Dropdown selection event handler
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["pid"] = 0; // Reset pagination on category change
            display();
        }

        // Next Page Button Click
        protected void btnNext_Click(object sender, EventArgs e)
        {
            ViewState["pid"] = Convert.ToInt32(ViewState["pid"]) + 1;
            display();
        }

        // Previous Page Button Click
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            ViewState["pid"] = Convert.ToInt32(ViewState["pid"]) - 1;
            display();
        }

        //protected void LinkButton1_Click(object sender, EventArgs e)
        //{
        //    btnPrev.Enabled = true;
        //    p += Convert.ToInt32(ViewState["pid"]) - 1;
        //    ViewState["pid"] = Convert.ToInt32(p);
        //    int temp = row / pg.PageSize;
        //    if (p == temp)
        //    {
        //        btnNext.Enabled = false;
        //    }
        //    display();
        //}
        protected void LinkButton1_Click(object sender, EventArgs e)
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


        protected void dlProducts_ItemCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "cmd_detailV")
            {
                int pid = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("ViewDetail.aspx?pid=" + pid);

            }
        }

        protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkAddToCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add_To_Cart.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
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
    }
}
