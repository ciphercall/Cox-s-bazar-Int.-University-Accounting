using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.IO;
using System.Collections.Specialized;

namespace AlchemyAccounting.payroll.report.ui
{
    public partial class salary_sheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    DateTime td = DateTime.Now;
                    string mon = td.ToString("MMM").ToUpper();
                    string year = td.ToString("yy");
                    txtMonth.Text = mon + "-" + year;
                    txtMonth.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListMonth(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT TRANSMY FROM HR_SALGRANT WHERE TRANSMY LIKE '" + prefixText + "%' ", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["TRANSMY"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtMonth_TextChanged(object sender, EventArgs e)
        {
            if (txtMonth.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Month can't empty.";
                txtMonth.Focus();
            }
            else
            {
                btnSearch.Focus();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Month can't empty.";
                txtMonth.Focus();
            }
            else
            {
                Session["month"] = null;
                Session["month"] = txtMonth.Text;

                Page.ClientScript.RegisterStartupScript(
                   this.GetType(), "OpenWindow", "window.open('../vis-report/rpt-Salary-Sheet.aspx','_newtab');", true);
            }
        }
    }
}