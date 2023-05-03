using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AlchemyAccounting.payroll.report.ui
{
    public partial class rpt_workingHour_Periodic_Site_Form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    DateTime td = DateTime.Now;
                    txtFromDate.Text = td.ToString("dd/MM/yyyy");
                    txtToDate.Text = td.ToString("dd/MM/yyyy");
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListSiteID(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = ("SELECT COSTPNM FROM GL_COSTP WHERE COSTPNM LIKE '" + prefixText + "%'");
           

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["COSTPNM"].ToString());
            return CompletionSet.ToArray();

        }


        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            txtToDate.Focus();
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            txtSiteNM.Focus();
        }

        protected void txtSiteNM_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd("select COSTPID from GL_COSTP where COSTPNM ='"+txtSiteNM.Text+"' ",txtSiteID);

            btnSubmit.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["fromdate"] = txtFromDate.Text;
            Session["todate"] = txtToDate.Text;
            Session["SiteID"] = txtSiteID.Text;

            Page.ClientScript.RegisterStartupScript(
                   this.GetType(), "OpenWindow", "window.open('../vis-report/rpt-workingHour-Periodic-Site.aspx','_newtab');", true);
        }
    }
}