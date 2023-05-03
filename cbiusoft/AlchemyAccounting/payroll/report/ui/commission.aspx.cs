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
    public partial class commission : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmdd;

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

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
                    txtSite.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListSite(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT COSTPNM FROM GL_COSTP WHERE COSTPNM LIKE '" + prefixText + "%' ", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["COSTPNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtSite_TextChanged(object sender, EventArgs e)
        {
            if (txtSite.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Select site.";
                txtSite.Focus();
            }
            else
            {
                lblSiteID.Text = "";
                Global.lblAdd("SELECT COSTPID FROM GL_COSTP WHERE COSTPNM ='" + txtSite.Text + "'", lblSiteID);
                if (lblSiteID.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Select site.";
                    txtSite.Text = "";
                    txtSite.Focus();
                }
                else
                {
                    lblErrMsg.Visible = false;
                    btnSearch.Focus();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSite.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Select site.";
                txtSite.Focus();
            }
            else
            {
                lblErrMsg.Visible = false;

                Session["sitenm"] = null;
                Session["siteid"] = null;

                Session["sitenm"] = txtSite.Text;
                Session["siteid"] = lblSiteID.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../vis-report/rpt-commission.aspx','_newtab');", true);
            }
        }
    }
}