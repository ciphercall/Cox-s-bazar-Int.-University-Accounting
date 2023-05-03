using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.payroll.report.ui
{
    public partial class rpt_employee_info_form : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
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
                    txtFromDate.Focus();


                    Session["fromdate"] = "";
                    Session["todate"] = "";
                    Session["empid"] = "";
                    Session["empname"] = "";
                }
            }
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmpID(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = ("SELECT (EMPNM + '-' + EMPID) AS EMP FROM HR_EMP WHERE (EMPNM + '-' + EMPID) LIKE '" + prefixText + "%'");


            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["EMP"].ToString());
            return CompletionSet.ToArray();

        }



        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            txtToDate.Focus();
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            txtEmpName.Focus();
        }

        protected void txtEmpName_TextChanged(object sender, EventArgs e)
        {

            if (txtEmpName.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Select Employee Name";
            }
            else
            {
                string empNM = "";
                string empID = "";
                string searchPar = txtEmpName.Text;

                int splitter = searchPar.IndexOf("-");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('-');

                    empNM = lineSplit[0];
                    empID = lineSplit[1];

                    txtEmpID.Text = empID;
                    txtEmpName.Text = empNM;

                }

                btnSubmit.Focus();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime frdt = DateTime.Parse(txtFromDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime todt = DateTime.Parse(txtToDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            if (txtEmpName.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "select employee name.";
            }
            else if (frdt > todt)
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "From date should be smaller than To date";

            }

            else
            {
                Session["fromdate"] = txtFromDate.Text;
                Session["todate"] = txtToDate.Text;
                Session["empid"] = txtEmpID.Text;
                Session["empname"] = txtEmpName.Text;

                Page.ClientScript.RegisterStartupScript(
                       this.GetType(), "OpenWindow", "window.open('../vis-report/rpt-single-employee-info.aspx','_newtab');", true);
            }

        }
    }
}