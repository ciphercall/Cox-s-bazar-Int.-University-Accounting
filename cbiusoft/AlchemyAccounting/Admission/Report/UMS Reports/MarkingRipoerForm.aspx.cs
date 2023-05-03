using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.Report
{
    public partial class MarkingRipoerForm : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.dropDownAdd(ddlProgramNM, "SELECT PROGRAMNM FROM EIM_PROGRAM ORDER BY PROGRAMID");
                Global.DropDownAddAllTextWithValue(ddlSemester, "SELECT SEMESTERNM,SEMESTERID FROM EIM_SEMESTER ORDER BY SEMESTERNM");
                Global.dropDownAdd(ddlBatch, "SELECT DISTINCT BATCH FROM EIM_STUDENT ORDER BY BATCH");
            }
        }

        protected void ddlProgramNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgramNM.Text != "Select")
                Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgramNM.Text + "'", lblProID);
        }

        protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSEM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Submit(object sender, EventArgs e)
        {
            if (ddlProgramNM.Text == "Select")
                ddlProgramNM.Focus();
            else if (ddlSemester.Text == "Select")
                ddlSemester.Focus();
            else if (ddlSEM.Text == "Select")
                ddlSEM.Focus();
            else
            {
                ddlSemester.Text = Global.GetData("SELECT distinct SEMESTERID FROM EIM_STUDENT WHERE BATCH='"+ddlBatch.Text+"' AND PROGRAMID='"+lblProID.Text+"'");
                Session["PROGRAMNM"] = "";
                Session["PROGRAMID"] = "";
                Session["SESSIONNM"] = "";
                Session["SESSIONID"] = "";
                Session["SEMESTERNM"] = "";
                Session["SEMESTERID"] = "";
                Session["BATCH"] = "";

                Session["PROGRAMNM"] = ddlProgramNM.Text;
                Session["PROGRAMID"] = lblProID.Text;
                Session["SESSIONNM"] = ddlSemester.SelectedItem.ToString();
                Session["SESSIONID"] = ddlSemester.SelectedValue;
                Session["SEMESTERNM"] = ddlSEM.Text;
                Session["SEMESTERID"] = ddlSEM.SelectedValue;
                Session["BATCH"] = ddlBatch.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                      "OpenWindow", "window.open('/Admission/Report/MarkingReport.aspx','_newtab');", true);
            }
        }
    }
}