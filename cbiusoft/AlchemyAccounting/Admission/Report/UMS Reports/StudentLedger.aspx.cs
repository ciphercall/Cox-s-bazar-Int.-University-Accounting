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
    public partial class StudentLedger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDate.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionStudentID(string prefixText, int count, string contextKey)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection  
            SqlCommand cmd = new SqlCommand("SELECT NEWSTUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["NEWSTUDENTID"].ToString());
            }
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtStuNM.Text == "")
            {
                lblMsg.Text = "Student ID Empty !";
                txtStuID.Focus();
            }
            else if (txtDate.Text == "")
            {
                lblMsg.Text = "Date is Empty !";
                txtDate.Focus();
            }
            else
            {
                lblMsg.Text = "";
                Session["STUID"] = "";
                Session["STUNM"] = "";
                Session["LEDGERDT"] = "";
                Session["STUID"] = txtStuID.Text;
                Session["STUNM"] = txtStuNM.Text;
                Session["LEDGERDT"] = txtDate.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                 "OpenWindow", "window.open('/Admission/Report/StudentLedgerReport.aspx','_newtab');", true);
            }
        }

        protected void txtStuID_TextChanged(object sender, EventArgs e)
        {
            if (txtStuID.Text != "")
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtStuID.Text + "'", txtStuNM);
        }
    }
}