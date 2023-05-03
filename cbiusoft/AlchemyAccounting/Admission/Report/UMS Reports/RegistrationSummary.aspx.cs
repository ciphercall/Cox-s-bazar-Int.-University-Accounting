using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.Report.UMS_Reports
{
    public partial class RegistrationSummary : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.dropDownAdd(ddlSem, @"SELECT DISTINCT EIM_SEMESTER.SEMESTERNM FROM EIM_SEMESTER INNER JOIN
                      EIM_STUDENT ON EIM_SEMESTER.SEMESTERID = EIM_STUDENT.SEMESTERID");
                Global.dropDownAdd(ddlProg, @"SELECT DISTINCT EIM_PROGRAM.PROGRAMNM FROM EIM_STUDENT INNER JOIN
                      EIM_PROGRAM ON EIM_STUDENT.PROGRAMID = EIM_PROGRAM.PROGRAMID");
            }
        }

        protected void ddlSemNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSem.Text != "")
            {
                Global.lblAdd(@"SELECT     EIM_STUDENT.PROGRAMID FROM  EIM_STUDENT INNER JOIN
                      EIM_PROGRAM ON EIM_STUDENT.PROGRAMID = EIM_PROGRAM.PROGRAMID WHERE EIM_PROGRAM.PROGRAMNM='" + ddlProg.Text + "'", lblProgram);
                ddlSem.Focus();
            }
            else
                ddlSem.Focus();
        }

        protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSem.Text != "")
            {
                Global.lblAdd(@"SELECT     EIM_STUDENT.SEMESTERID FROM  EIM_STUDENT INNER JOIN
                      EIM_SEMESTER ON EIM_STUDENT.SEMESTERID = EIM_SEMESTER.SEMESTERID WHERE EIM_SEMESTER.SEMESTERNM='" + ddlSem.Text + "'", lblSemester);
            }
            else
                txtBatch.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionBatch(string prefixText, int count, string contextKey)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT BATCH FROM EIM_STUDENT WHERE BATCH LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["BATCH"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionSession(string prefixText, int count, string contextKey)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT SESSION FROM EIM_STUDENT WHERE SESSION LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["SESSION"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (lblProgram.Text == "")
                ddlProg.Focus();
            else if (lblSemester.Text == "")
                ddlSem.Focus();
            else
            {
                Session["SEMID"] = "";
                Session["SEMNM"] = "";
                Session["PROGID"] = "";
                Session["PROGNM"] = "";
                Session["BATCH"] = "";
                Session["SESSION"] = "";

                Session["SEMID"] = lblSemester.Text;
                Session["SEMNM"] = ddlSem.Text;
                Session["PROGID"] = lblProgram.Text;
                Session["PROGNM"] = ddlProg.Text;
                Session["BATCH"] = txtBatch.Text;
                Session["SESSION"] = txtSession.Text;
                ScriptManager.RegisterStartupScript(this,
                             this.GetType(), "OpenWindow", "window.open('/Admission/Report/RegistrationSummaryView.aspx','_newtab');", true);
            }
        }

        protected void txtBatch_TextChanged(object sender, EventArgs e)
        {
            txtSession.Focus();
        }
    }
}