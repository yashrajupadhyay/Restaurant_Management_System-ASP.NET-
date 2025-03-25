using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;

namespace ProjectASP.Admin
{
    public partial class Add_Category : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;
        protected void Page_Load(object sender, EventArgs e)
        {
            getcon();
        }
        void getcon()
        {
            cs = new Class1();
            cs.startcon();
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnAddCategory_Click1(object sender, EventArgs e)
        {
            if (btnAddCategory.Text == "Add Category")
            {
                getcon();
                cs.addCategory(txtCategoryName.Text);

            }
        }
    }
}