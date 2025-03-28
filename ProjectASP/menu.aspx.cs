using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectASP
{
    public partial class menu : System.Web.UI.Page
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        Class1 cs;
        PagedDataSource pg;
        int row;

        void getcon()
        {
            cs = new Class1();
            cs.startcon();
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (ViewState["pid"] == null) ViewState["pid"] = 0; // Ensure ViewState exists
            int pid = Convert.ToInt32(ViewState["pid"]);
            if (pid > 0)
            {
                ViewState["pid"] = pid - 1;
                display();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ViewState["pid"] == null) ViewState["pid"] = 0; // Ensure ViewState exists
            int pid = Convert.ToInt32(ViewState["pid"]);
            int maxPage = (row / 3); // Assuming 3 items per page
            if (pid < maxPage)
            {
                ViewState["pid"] = pid + 1;
                display();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["pid"] = 0; // Initialize only once
            }
            getcon();
            display();
        }

        void display()
        {
            try
            {
                getcon();

                da = new SqlDataAdapter("SELECT * FROM Products", cs.startcon());
                ds = new DataSet();
                da.Fill(ds);
                row = ds.Tables[0].Rows.Count;

                if (row == 0)
                {
                    DataList1.DataSource = null;
                    DataList1.DataBind();
                    Response.Write("<script>alert('No products available!');</script>");
                    return;
                }

                pg = new PagedDataSource();
                pg.AllowPaging = true;
                pg.PageSize = 3;
                pg.DataSource = ds.Tables[0].DefaultView;
                pg.CurrentPageIndex = Convert.ToInt32(ViewState["pid"]);

                btnPrev.Enabled = !pg.IsFirstPage;
                btnNext.Enabled = !pg.IsLastPage;

                DataList1.DataSource = pg;
                DataList1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error fetching products: " + ex.Message + "');</script>");
            }
        }
    }
}
