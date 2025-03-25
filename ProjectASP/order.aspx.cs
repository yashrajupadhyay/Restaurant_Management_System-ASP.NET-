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
    public partial class order : System.Web.UI.Page
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        Class1 cs = new Class1(); // Database connection class
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
            }

        }

        private void LoadCategories()
        {
            DataTable dt = DbHelper.GetCategories();
            ddlCategories.DataSource = dt;
            ddlCategories.DataTextField = "Name";
            ddlCategories.DataValueField = "Id";
            ddlCategories.DataBind();
            ddlCategories.Items.Insert(0, new ListItem("-- Select Category --", "0"));
        }

        protected void ddlMealType_SelectedIndexChanged(object sender, EventArgs e)
        {

           
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlCategories_SelectedIndexChanged1(object sender, EventArgs e)
        {
            
        }

        protected void ddlCategories_SelectedIndexChanged2(object sender, EventArgs e)
        {
            int categoryId = Convert.ToInt32(ddlCategories.SelectedValue);
            if (categoryId > 0)
            {
                DataTable dt = DbHelper.GetProductsByCategory(categoryId);
                dlProducts.DataSource = dt;
                dlProducts.DataBind();
            }
            else
            {
                dlProducts.DataSource = null;
                dlProducts.DataBind();
            }
        }
    }
}