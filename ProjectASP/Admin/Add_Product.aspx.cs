using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ProjectASP.Admin
{
    public partial class Add_Product : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;
        string fnm;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getcon();
                fillgrid();
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
                getcon();
                string query = "SELECT Id, Name FROM Categories";
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
                // Log error instead of showing an alert
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

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
            con = cs.startcon();
        }

        void clear()
        {
            txtProductName.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            ddlCategory.SelectedIndex = 0;
            imgPreview.ImageUrl = "~/Admin/Images1/default.png";
            ViewState["id"] = null;
        }

        bool IsDuplicate(string productName, int categoryId)
        {
            getcon();
            string query = "SELECT COUNT(*) FROM Products WHERE Name = @Name AND CategoryID = @CategoryID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", productName);
            cmd.Parameters.AddWithValue("@CategoryID", categoryId);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return count > 0;
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            getcon();
            try
            {
                if (ddlCategory.SelectedValue == "0")
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    return;
                }

                // Upload Image
                imgupload();

                string productName = txtProductName.Text.Trim();
                string description = txtDescription.Text.Trim();
                decimal price = Convert.ToDecimal(txtPrice.Text);
                int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);

                if (btnAddProduct.Text == "Update" && ViewState["id"] != null)
                {
                    int productId = Convert.ToInt32(ViewState["id"]);

                    if (string.IsNullOrEmpty(fnm))
                    {
                        DataSet ds = cs.select(productId);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            fnm = ds.Tables[0].Rows[0]["Image"].ToString();
                        }
                    }

                    cs.updateProduct(productId, productName, description, price, categoryId, fnm);
                    btnAddProduct.Text = "Add Product";
                    ViewState["id"] = null;
                }
                else
                {
                    if (!IsDuplicate(productName, categoryId))
                    {
                        cs.insertProduct(productName, description, price, fnm, categoryId);
                    }
                }

                fillgrid();
                clear();
            }
            catch (Exception ex)
            {
                // Log error
            }
        }

        void filltext()
        {
            cs = new Class1();
            getcon();
            ds = new DataSet();

            if (ViewState["id"] != null)
            {
                ds = cs.select(Convert.ToInt32(ViewState["id"]));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtProductName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                    txtPrice.Text = ds.Tables[0].Rows[0]["Price"].ToString();
                    ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["CategoryID"].ToString();

                    fnm = ds.Tables[0].Rows[0]["Image"].ToString().Trim();

                    if (!string.IsNullOrEmpty(fnm))
                    {
                        imgPreview.ImageUrl = fnm;
                    }
                    else
                    {
                        imgPreview.ImageUrl = "~/Admin/Images1/default.png";
                    }
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd_edt")
            {
                int id;
                if (int.TryParse(e.CommandArgument.ToString(), out id))
                {
                    ViewState["id"] = id;
                    btnAddProduct.Text = "Update";
                    filltext();
                }
            }
           else
            {
                cs = new Class1();
                int id = Convert.ToInt32(e.CommandArgument);
                ViewState["id"] = id;

                cs.delete_product(Convert.ToInt32(ViewState["id"]));
                fillgrid();
            }
        }

    }
}
