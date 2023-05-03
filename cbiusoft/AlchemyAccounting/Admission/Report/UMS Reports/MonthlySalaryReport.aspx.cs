using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.Report.UMS_Reports
{
    public partial class MonthlySalaryReport : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.dropDownAdd(ddlMonth_Year, "SELECT TRANSMY FROM HR_SALDRCR");
                Global.dropDownAdd(ddlDept, "SELECT DEPTNM FROM HR_DEPT");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["TRANSMY"] = "";
            Session["DEPTNM"] = "";
            Session["DEPTID"] = "";


            Session["TRANSMY"] = ddlMonth_Year.Text;
            Session["DEPTNM"] = ddlDept.Text;
            Session["DEPTID"] = lblDeptID.Text;
            ScriptManager.RegisterStartupScript(this,
                      this.GetType(), "OpenWindow", "window.open('/Admission/Report/MonthlySalaryReportPrint.aspx','_newtab');", true);
        }
        protected void ddlMonth_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDept.Text == "Select")
                ddlDept.Focus();
            else
            {
                lblDeptID.Text = "";
                Global.lblAdd("SELECT DEPTID FROM HR_DEPT WHERE DEPTNM='" + ddlDept.Text + "'", lblDeptID);
                btnSubmit.Focus();
            }
        }

    }
}