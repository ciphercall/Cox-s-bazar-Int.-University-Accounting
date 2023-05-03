using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;


namespace AlchemyAccounting.Admission.Report.UMS_Reports
{
    public partial class ResultsByStudent : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    // Global.dropDownAdd(ddlYR, "SELECT DISTINCT(REGYY) FROM EIM_RESULT");
                    //                    Global.dropDownAdd(ddlSemNM, @"SELECT     EIM_SEMESTER.SEMESTERNM
                    //                                                FROM  EIM_RESULT INNER JOIN
                    //                      EIM_SEMESTER ON EIM_RESULT.SEMESTERID = EIM_SEMESTER.SEMESTERID");
                    //                    Global.dropDownAdd(ddlProgNM, @"SELECT     EIM_PROGRAM.PROGRAMNM
                    //                                                        FROM  EIM_PROGRAM INNER JOIN
                    //                          EIM_RESULT ON EIM_PROGRAM.PROGRAMID = EIM_RESULT.PROGRAMID");
                    Global.dropDownAdd(ddlSemNM, @"SELECT  SEMESTERNM  FROM  EIM_SEMESTER");
                    Global.dropDownAdd(ddlProgNM, @"SELECT  PROGRAMNM FROM  EIM_PROGRAM");
                    string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    Session["YR"] = yr;
                    int i, m;
                    int a = int.Parse(yr);
                    m = a + 5;
                    ddlYR.Items.Add("Select");
                    for (i = a - 5; i <= m; i++)
                    {
                        ddlYR.Items.Add(i.ToString());
                    }
                    ddlYR.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    ddlYR.Focus();
                    //gridShow();
                }
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionStudentD(string prefixText, int count, string contextKey)
        {


            string YR = HttpContext.Current.Session["YR"].ToString();
            string SemID = HttpContext.Current.Session["SemID"].ToString();
            string ProgID = HttpContext.Current.Session["ProgID"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlConnection con = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT dbo.EIM_STUDENT.NEWSTUDENTID
FROM  dbo.EIM_RESULT INNER JOIN dbo.EIM_STUDENT ON dbo.EIM_RESULT.STUDENTID = dbo.EIM_STUDENT.STUDENTID WHERE EIM_STUDENT.NEWSTUDENTID LIKE '" + prefixText + "%' AND EIM_RESULT.PROGRAMID='" + ProgID + "'  AND EIM_RESULT.SEMESTERID='" + SemID + "'  AND EIM_RESULT.REGYY='" + YR + "'", con);
            SqlDataReader oReader;
            if (con.State != ConnectionState.Open)con.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["NEWSTUDENTID"].ToString());
            }
            if (con.State != ConnectionState.Closed)con.Close();
            return CompletionSet.ToArray();

        }
        private void gridShow()
        {
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  SL, COURSENM,
             MARK, CASE WHEN MARK>79 THEN 4.00 WHEN  MARK>74 THEN  3.75 WHEN MARK>69 THEN 3.50 WHEN MARK>64 THEN 3.25 
            WHEN MARK>59 THEN 3.00 WHEN MARK>54 THEN 2.75 WHEN MARK BETWEEN 49 AND 55 THEN 2.50 WHEN MARK>44 THEN 2.25 WHEN MARK>39 THEN 2.00
            ELSE 0.00 END   GP ,
            CASE WHEN MARK>79 THEN 'A+' WHEN  MARK>74 THEN  'A' WHEN MARK>69 THEN 'A-' WHEN MARK>64 THEN 'B+' 
            WHEN MARK>59 THEN 'B' WHEN MARK>54 THEN 'B-' WHEN MARK>49 THEN 'C+' WHEN MARK>44 THEN 'C' WHEN MARK>39 THEN 'D' 
            ELSE 'F' END LG,
            REMARKS
            FROM(SELECT    ROW_NUMBER() over (order by EIM_COURSE.COURSEID) as SL , EIM_COURSE.COURSENM, (EIM_RESULT.M40+EIM_RESULT.M60) MARK,  EIM_RESULT.REMARKS
            FROM EIM_RESULT INNER JOIN EIM_COURSE ON EIM_RESULT.COURSEID = EIM_COURSE.COURSEID
            WHERE EIM_RESULT.REGYY='" + ddlYR.Text + "' AND EIM_RESULT.PROGRAMID='" + lblProgID.Text + "' AND EIM_RESULT.SEMESTERID='" + lblSemID.Text + "'  AND EIM_RESULT.SEMID='" + ddlSemesterID.SelectedValue.ToString() + "' AND EIM_RESULT.STUDENTID='" + txtstuIDNew.Text + "') A  ", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Result.DataSource = ds;
                gv_Result.DataBind();

            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Result.DataSource = ds;
                gv_Result.DataBind();
                int columncount = gv_Result.Rows[0].Cells.Count;
                gv_Result.Rows[0].Cells.Clear();
                gv_Result.Rows[0].Cells.Add(new TableCell());
                gv_Result.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Result.Rows[0].Visible = false;

            }
        }
        protected void ddlYR_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["YR"] = "";
            Session["YR"] = ddlYR.Text;
            if (ddlYR.Text == "Select")
                ddlYR.Focus();
            else
                ddlSemNM.Focus();
        }

        protected void ddlSemNM_SelectedIndexChanged(object sender, EventArgs e)
        {

            Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
            Session["SemID"] = lblSemID.Text;
            if (ddlYR.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlSemNM.SelectedIndex = -1;
                ddlProgNM.SelectedIndex = -1;
            }
            else
                ddlProgNM.Focus();
        }

        protected void ddlProgNM_SelectedIndexChanged(object sender, EventArgs e)
        {

            Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProgID);
            Session["ProgID"] = lblProgID.Text;

            if (ddlYR.Text == "Select")
            {
                ddlYR.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlSemNM.SelectedIndex = -1;
            }
            else if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlProgNM.SelectedIndex = -1;

            }
            else if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
            }
            else
            {
                txtstuID.Focus();
            }
        }

        protected void txtstuID_TextChanged(object sender, EventArgs e)
        {

            if (ddlYR.Text == "Select")
            {
                ddlYR.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlSemNM.SelectedIndex = -1;

                txtstuID.Text = "";txtstuIDNew.Text = "";
            }
            else if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlProgNM.SelectedIndex = -1;
                txtstuID.Text = "";

            }
            else if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
                txtstuID.Text = "";txtstuIDNew.Text = "";
            }
            else
            {
                txtstuIDNew.Text = Global.GetData("SELECT STUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID='"+txtstuID.Text+"'");
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtstuID.Text + "'", txtstuNM);
                if (txtstuNM.Text == "")
                {
                    txtstuID.Text = "";txtstuIDNew.Text = "";
                    txtstuNM.Focus();
                }
                else
                    ddlSemesterID.Focus();
            }
        }

        protected void gv_Result_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + SL;

                string COURSENM = DataBinder.Eval(e.Row.DataItem, "COURSENM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + COURSENM;
                string CGPA = DataBinder.Eval(e.Row.DataItem, "GP").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + CGPA;
                string GRADE = DataBinder.Eval(e.Row.DataItem, "LG").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + GRADE;
                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + REMARKS;


            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (ddlYR.Text == "Select")
                ddlYR.Focus();
            else if (ddlSemNM.Text == "Select")
                ddlSemNM.Focus();
            else if (ddlProgNM.Text == "Select")
                ddlProgNM.Focus();
            else if (txtstuID.Text == "" || txtstuIDNew.Text == "")
            {
                txtstuID.Focus();
            }
            else
            {
                Session["STUDENTNM"] = "";
                Session["STUDENTID"] = "";
                Session["SEMESTERNM"] = "";
                Session["YEAR"] = "";
                Session["PROGRAMNM"] = "";
                Session["SEMESTERID"] = "";
                Session["SEMID"] = "";
                Session["SEMIDNM"] = "";
                Session["PROGRAMID"] = "";

                Session["STUDENTNM"] = txtstuNM.Text;
                Session["STUDENTID"] = txtstuIDNew.Text;
                Session["STUDENTIDNEW"] = txtstuID.Text;
                Session["SEMESTERNM"] = ddlSemNM.Text;
                Session["YEAR"] = ddlYR.Text;
                Session["PROGRAMNM"] = ddlProgNM.Text;
                Session["SEMESTERID"] = lblSemID.Text;
                Session["SEMID"] = ddlSemesterID.SelectedValue.ToString();
                Session["SEMIDNM"] = ddlSemesterID.SelectedItem;
                Session["PROGRAMID"] = lblProgID.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                       "OpenWindow", "window.open('/Admission/Report/ResultsByStudentPrint.aspx','_newtab');", true);
            }
        }

        protected void btnSerach_Click(object sender, EventArgs e)
        {
            if (ddlYR.Text == "Select")
                ddlYR.Focus();
            else if (ddlSemNM.Text == "Select")
                ddlSemNM.Focus();
            else if (ddlProgNM.Text == "Select")
                ddlProgNM.Focus();
            else if (txtstuNM.Text == "")
                txtstuID.Focus();
            else
            {
                gridShow();
            }
        }

        protected void ddlSemesterID_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridShow();
            btnSerach.Focus();
        }

        protected void btnPrintFinal_Click(object sender, EventArgs e)
        {
            if (ddlYR.Text == "Select")
                ddlYR.Focus();
            else if (ddlSemNM.Text == "Select")
                ddlSemNM.Focus();
            else if (ddlProgNM.Text == "Select")
                ddlProgNM.Focus();
            else if (txtstuID.Text == "" || txtstuIDNew.Text == "")
            {
                txtstuID.Focus();
            }
            else
            {
                Session["STUDENTNM"] = "";
                Session["STUDENTID"] = "";
                Session["SEMESTERNM"] = "";
                Session["YEAR"] = "";
                Session["PROGRAMNM"] = "";
                Session["SEMESTERID"] = "";
                Session["SEMID"] = "";
                Session["SEMIDNM"] = "";
                Session["PROGRAMID"] = "";

                Session["STUDENTNM"] = txtstuNM.Text;
                Session["STUDENTID"] = txtstuIDNew.Text;
                Session["STUDENTIDNEW"] = txtstuID.Text;
                Session["SEMESTERNM"] = ddlSemNM.Text;
                Session["YEAR"] = ddlYR.Text;
                Session["PROGRAMNM"] = ddlProgNM.Text;
                Session["SEMESTERID"] = lblSemID.Text;
                Session["SEMID"] = ddlSemesterID.SelectedValue.ToString();
                Session["SEMIDNM"] = ddlSemesterID.SelectedItem;
                Session["PROGRAMID"] = lblProgID.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                       "OpenWindow", "window.open('/Admission/Report/ResultsByStudentPrint.aspx','_newtab');", true);
            }
        }
    }
}