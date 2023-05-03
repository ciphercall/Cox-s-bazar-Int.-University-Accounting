using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.Report.UMS_Reports
{
    public partial class Student_wise_due_statement : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.dropDownAddWithSelect(DropDownList1, "SELECT PROGRAMNM FROM EIM_PROGRAM");
                Global.dropDownAddWithSelect(ddlYYSem, @"SELECT DISTINCT EIM_STUDENT.BATCH
FROM            EIM_TRANS INNER JOIN
                         EIM_STUDENT ON EIM_TRANS.STUDENTID = EIM_STUDENT.STUDENTID ORDER BY BATCH");
                txtDate.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");  
            }
            
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblProgramID.Text = "";
            if (DropDownList1.Text != "Select")
            {
                Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM where PROGRAMNM='" + DropDownList1.Text + "'", lblProgramID);
                ddlYYSem.Focus();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (DropDownList1.Text == "Select")
                DropDownList1.Focus();
            else if (txtDate.Text == "")
                txtDate.Focus();
            else
            {
                Session["PROGRAMID"] = "";
                Session["PROGRAMNM"] = "";
                Session["DATE"] = "";
                Session["SEM-YEAR"] = "";
                Session["BATCHH"] = ddlYYSem.Text;
                Session["PROGRAMID"] = lblProgramID.Text;
                Session["PROGRAMNM"] = DropDownList1.Text;
                Session["DATE"] = txtDate.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                 "OpenWindow", "window.open('/Admission/Report/Student_wise_due_statement_Report.aspx','_newtab');", true);
            }
        }

        protected void ddlYYSem_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSubmit.Focus();
        }
    }
}