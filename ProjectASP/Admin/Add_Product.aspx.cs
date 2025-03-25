using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO; // Required for file handling

namespace ProjectASP.Admin
{
    public partial class Add_Product : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;
        string fnm; // Image path variable

        protected void Page_Load(object sender, EventArgs e)
        {
            getcon(); // Initialize DB connection
            fillgrid();

            if (!IsPostBack) // Load categories only on first load
            {
                LoadCategories();
            }
        }

        void fillgrid()
        {
            cs = new Class1();
            getcon();
            GridView1.DataSource = cs.filldata();
            GridView1.DataBind();
        }

        private void LoadCategories()
        {
            try
            {
                getcon(); // Ensure connection is initialized
                string query = "SELECT Id, Name FROM Categories"; // Fetch categories
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    ddlCategory.DataSource = dr;
                    ddlCategory.DataTextField = "Name";
                    ddlCategory.DataValueField = "Id";
                    ddlCategory.DataBind();
                }

                dr.Close();
                con.Close();

                ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        //public void imgupload()
        //{
        //    // Correct folder path
        //    string uploadFolder = Server.MapPath("~/Admin/Images1/");

        //    // Check if the directory exists; if not, create it
        //    if (!Directory.Exists(uploadFolder))
        //    {
        //        Directory.CreateDirectory(uploadFolder);
        //    }

        //    if (fldimg.HasFile)
        //    {
        //        // Get the filename and append it to the correct path
        //        string fileName = Path.GetFileName(fldimg.FileName);
        //        string savePath = Path.Combine(uploadFolder, fileName);

        //        // Save the file
        //        fldimg.SaveAs(savePath);

        //        // Store the relative path (for database storage)
        //        fnm = "/Admin/Images1/" + fileName; // ✅ Corrected path
        //    }
        //}
        void imgupload()
        {
            getcon();
            if (fldimg.HasFile)
            {
                fnm = "/Admin/Images1/" + fldimg.FileName;
                fldimg.SaveAs(Server.MapPath(fnm));
            }
        }



        void getcon()
        {
            cs = new Class1();
            con = cs.startcon(); // Ensure startcon() returns SqlConnection
        }

        void clear()
        {
            txtProductName.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            ddlCategory.SelectedIndex = 0;
            imgPreview.ImageUrl = "~/Admin/Images1/default.png"; // Reset image preview

            Response.Write("<script>alert('Product added successfully');</script>");
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            getcon(); // Ensure connection is established
            try
            {
                if (ddlCategory.SelectedValue == "0")
                {
                    Response.Write("<script>alert('Please select a category');</script>");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    Response.Write("<script>alert('Please enter all required fields');</script>");
                    return;
                }

                // Upload Image
                imgupload();

                // Get Values
                string productName = txtProductName.Text.Trim();
                string description = txtDescription.Text.Trim();
                decimal price = Convert.ToDecimal(txtPrice.Text);
                int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);

                // If updating, use the existing image if a new one is not uploaded
                if (btnAddProduct.Text == "Update")
                {
                    if (string.IsNullOrEmpty(fnm)) // No new image uploaded
                    {
                        DataSet ds = cs.select(Convert.ToInt32(ViewState["id"]));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            fnm = ds.Tables[0].Rows[0]["Image"].ToString(); // Use existing image
                        }
                    }

                    cs.updateProduct(Convert.ToInt32(ViewState["id"]), productName, description, price, categoryId, fnm);
                    btnAddProduct.Text = "Add Product"; // Reset button text
                }
                else
                {
                    // Insert Product
                    cs.insertProduct(productName, description, price, fnm, categoryId);
                }

                // Refresh Grid & Clear Form
                fillgrid();
                clear();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        void filltext()
        {
            cs = new Class1();
            getcon();
            ds = new DataSet();

            if (ViewState["id"] != null)
            {
                ds = cs.select(Convert.ToInt32(ViewState["id"])); // Fetch product details by ID

                if (ds.Tables[0].Rows.Count > 0) // Ensure data exists
                {
                    txtProductName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    txtPrice.Text = ds.Tables[0].Rows[0]["Price"].ToString();
                    ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["CategoryID"].ToString();

                    // Store image path globally for later use
                    fnm = ds.Tables[0].Rows[0]["Image"].ToString().Trim();

                    if (!string.IsNullOrEmpty(fnm))
                    {
                        imgPreview.ImageUrl = fnm; // ✅ Display image in <asp:Image>
                    }
                    else
                    {
                        imgPreview.ImageUrl = "~/Admin/Images1/default.png"; // Default if no image
                    }
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd_edt")
            {
                int id = Convert.ToInt16(e.CommandArgument);
                ViewState["id"] = id;
                btnAddProduct.Text = "Update";
                filltext();
            }
            else
            {
                cs = new Class1();
                int id = Convert.ToInt32(e.CommandArgument);
                ViewState["id"] = id;

                cs.deleteProduct(Convert.ToInt32(ViewState["id"]));
                fillgrid();
            }
        }
    }
}
