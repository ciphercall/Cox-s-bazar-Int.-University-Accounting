using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.payroll.report.ui
{
    public partial class rpt_workingHour_Daily_Form : System.Web.UI.Page
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
                    txtFromDate.Text = td.ToString("dd/MM/yyyy");
                    
                }
            }
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            btnSubmit.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["Date"] = txtFromDate.Text;

            Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../vis-report/rpt-workingHour-Daily.aspx','_newtab');", true);
        }
    }
}