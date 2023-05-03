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
    public partial class StudentTotalResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtStuID.Focus();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionStudentID(string prefixText, int count, string contextKey)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection  
            SqlCommand cmd = new SqlCommand(@"SELECT EIM_STUDENT.NEWSTUDENTID
FROM EIM_STUDENT INNER JOIN
EIM_COURSEREG ON EIM_STUDENT.PROGRAMID = EIM_COURSEREG.PROGRAMID 
AND EIM_STUDENT.STUDENTID = EIM_COURSEREG.STUDENTID WHERE EIM_STUDENT.NEWSTUDENTID LIKE '" + prefixText + "%'", conn);
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
            else
            {
                lblMsg.Text = "";
                Session["STUID"] = "";
                Session["STUNM"] = ""; 
                Session["STUID"] = lblOldStudent.Text;
                Session["STUNM"] = txtStuNM.Text; 
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                 "OpenWindow", "window.open('/Admission/Report/studentWiseResult_.aspx','_newtab');", true);
            }
        }

        protected void txtStuID_TextChanged(object sender, EventArgs e)
        {
            txtStuNM.Text = "";
            if (txtStuID.Text != "")
            {
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtStuID.Text + "'", txtStuNM);
                Global.lblAdd("SELECT STUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtStuID.Text + "'", lblOldStudent);
                btnSubmit.Focus();
            }
        }
    }
} 