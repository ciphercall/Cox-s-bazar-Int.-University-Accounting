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
using AlchemyAccounting.payroll.model;
using AlchemyAccounting.payroll.dataAccess;

namespace AlchemyAccounting.payroll.report.ui
{
    public partial class employee_payment : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmdd;

        payroll_model iob = new payroll_model();
        payroll_data dob = new payroll_data();

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
                    DateTime today = DateTime.Now;
                    string m = today.ToString("MMMM").ToUpper();
                    ddlMonth.Text = m;
                    string month = ddlMonth.Text;
                    string mon = month.Substring(0, 3);
                    string year = today.ToString("yy");
                    lblMy.Text = mon + "-" + year;
                    ddlMonth.Focus();
                }
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            string month = ddlMonth.Text;
            string mon = month.Substring(0, 3);
            string year = today.ToString("yy");
            lblMy.Text = mon + "-" + year;
            txtEmp.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmployeeInfo(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT (EMPNM  + '|' + QATARID + '|' + EMPID) AS EMP FROM HR_EMP WHERE (EMPNM  + '|' + QATARID + '|' + EMPID) LIKE '" + prefixText + "%' ORDER BY EMPID", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["EMP"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtEmp_TextChanged(object sender, EventArgs e)
        {
            if (txtEmp.Text == "")
            {
                txtEmp.Text = "";
                lblQID.Text = "";
                lblEmpID.Text = "";
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Select Employee Name.";
                txtEmp.Focus();
            }
            else
            {
                lblErrMsg.Visible = false;

                string empNM = "";
                string qID = "";
                string empID = "";

                string searchPar = txtEmp.Text;
                int splitter = searchPar.IndexOf("|");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    empNM = lineSplit[0];
                    qID = lineSplit[1];
                    empID = lineSplit[2];

                    txtEmp.Text = empNM.Trim();
                    lblQID.Text = qID.Trim();
                    lblEmpID.Text = empID.Trim();
                    btnSearch.Focus();
                }
                else
                {
                    txtEmp.Text = "";
                    lblQID.Text = "";
                    lblEmpID.Text = "";
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Select Employee Name.";
                    txtEmp.Focus();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (txtEmp.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Select an employee";
                    txtEmp.Focus();
                }
                else if (lblEmpID.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Select an employee";
                    txtEmp.Focus();
                }
                else if (lblQID.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Select an employee";
                    txtEmp.Focus();
                }
                else
                {
                    lblErrMsg.Visible = false;

                    Session["month"] = null;
                    Session["my"] = null;
                    Session["empNm"] = null;
                    Session["empID"] = null;
                    Session["qID"] = null;

                    Session["month"] = ddlMonth.Text;
                    Session["my"] = lblMy.Text;
                    Session["empNm"] = txtEmp.Text;
                    Session["empID"] = lblEmpID.Text;
                    Session["qID"] = lblQID.Text;

                    ScriptManager.RegisterStartupScript(this,
                                this.GetType(), "OpenWindow", "window.open('../vis-report/rpt-emp-pay.aspx','_newtab');", true);
                }
            }
        }
    }
}