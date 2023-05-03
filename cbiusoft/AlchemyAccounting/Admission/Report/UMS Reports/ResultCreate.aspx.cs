using System;
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
    public partial class ResultCreate : System.Web.UI.Page
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
                    //Global.dropDownAdd(ddlYR, "SELECT DISTINCT(REGYY) FROM EIM_RESULT");
                    //                    Global.dropDownAdd(ddlSemNM, @"SELECT     EIM_SEMESTER.SEMESTERNM
                    //                                                FROM  EIM_RESULT INNER JOIN
                    //                      EIM_SEMESTER ON EIM_RESULT.SEMESTERID = EIM_SEMESTER.SEMESTERID");
                    //                    Global.dropDownAdd(ddlProgNM, @"SELECT     EIM_PROGRAM.PROGRAMNM
                    //                                                        FROM  EIM_PROGRAM INNER JOIN
                    //                          EIM_RESULT ON EIM_PROGRAM.PROGRAMID = EIM_RESULT.PROGRAMID");
                    //                    Global.dropDownAdd(ddlCourseNM, @"SELECT     EIM_COURSE.COURSENM
                    //                                FROM  EIM_COURSE INNER JOIN
                    //                                      EIM_RESULT ON EIM_COURSE.COURSEID = EIM_RESULT.COURSEID");
                    
                    string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
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
        private void gridShow()
        {
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  SL,STUDENTID, STUDENTNM,
             MARK, CASE WHEN MARK>79 THEN 4.00 WHEN  MARK>74 THEN  3.75 WHEN MARK>69 THEN 3.50 WHEN MARK>64 THEN 3.25 
            WHEN MARK>59 THEN 3.00 WHEN MARK>54 THEN 2.75 WHEN MARK BETWEEN 49 AND 55 THEN 2.50 WHEN MARK>44 THEN 2.25 WHEN MARK>39 THEN 2.00
            ELSE 0.00 END   GP ,
            CASE WHEN MARK>79 THEN 'A+' WHEN  MARK>74 THEN  'A' WHEN MARK>69 THEN 'A-' WHEN MARK>64 THEN 'B+' 
            WHEN MARK>59 THEN 'B' WHEN MARK>54 THEN 'B-' WHEN MARK>49 THEN 'C+' WHEN MARK>44 THEN 'C' WHEN MARK>39 THEN 'D' 
            ELSE 'F' END LG,
            REMARKS
            FROM( SELECT    ROW_NUMBER() over (order by EIM_RESULT.STUDENTID) as SL ,EIM_RESULT.STUDENTID,EIM_STUDENT.STUDENTNM,
            (EIM_RESULT.M40+EIM_RESULT.M60) MARK, EIM_RESULT.REMARKS 
            FROM EIM_RESULT INNER JOIN EIM_STUDENT ON EIM_RESULT.STUDENTID = EIM_STUDENT.STUDENTID   
            WHERE EIM_RESULT.REGYY='" + ddlYR.Text + "' AND EIM_RESULT.PROGRAMID='" + ddlProgNM.Text + "' AND EIM_RESULT.SEMESTERID='" + lblSemID.Text + "'  AND COURSEID='" + lblCrsID.Text + "' AND EIM_RESULT.SEMID='" + ddlSemesterID.SelectedValue.ToString() + "') A  ", conn);

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
            {
                gv_Result.Visible = false;
                ddlYR.Focus();
            }
            else
            {
                Global.dropDownAdd(ddlSemNM, @"SELECT     DISTINCT   EIM_SEMESTER.SEMESTERNM
FROM            EIM_SEMESTER INNER JOIN
                         EIM_RESULT ON EIM_SEMESTER.SEMESTERID = EIM_RESULT.SEMESTERID WHERE EIM_RESULT.REGYY='" + ddlYR.Text + "'");
                
                ddlSemNM.Focus();
            }
        }

        protected void ddlSemNM_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlSemNM.Text == "Select")
            {
                gv_Result.Visible = false;
                ddlSemNM.Focus();
            }
            else
            {
                Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
                Global.BindDropDown(ddlSemesterID, @"SELECT DISTINCT CASE WHEN SEMID='01' THEN '1st Semester' WHEN SEMID='02' THEN '2nd Semester' WHEN SEMID='03' THEN '3rd Semester' WHEN SEMID='04' THEN '4th Semester' 
WHEN SEMID='5th' THEN '5th Semester' WHEN SEMID='06' THEN '6th Semester' WHEN SEMID='07' THEN '7th Semester' WHEN SEMID='08' THEN '8th Semester' END NM,SEMID ID FROM EIM_RESULT WHERE REGYY='" + ddlYR.Text + "' AND SEMESTERID='" + lblSemID.Text + "' AND ISNULL(SEMID,'')<>''");
               
                ddlSemesterID.Focus();
            }
        }

        protected void ddlProgNM_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            if(ddlProgNM.Text!="--SELECT--")
            {
                Global.BindDropDown(ddlCourseNM,@"SELECT   DISTINCT     EIM_COURSE.COURSENM NM, EIM_RESULT.COURSEID ID
FROM            EIM_RESULT INNER JOIN EIM_COURSE ON EIM_RESULT.COURSEID = EIM_COURSE.COURSEID 
WHERE EIM_RESULT.PROGRAMID='" + ddlProgNM.Text + "' AND EIM_RESULT.REGYY='" + ddlYR.Text + "' AND EIM_RESULT.SEMESTERID='" + lblSemID.Text + "' AND EIM_RESULT.SEMID='" + ddlSemesterID.Text + "'");
                ddlCourseNM.Focus();

            }
        }

        protected void ddlCourseNM_SelectedIndexChanged(object sender, EventArgs e)
        {

                txtBatch.Focus(); 
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (ddlSemNM.Text == "Select")
                ddlSemNM.Focus();
            else if (ddlYR.Text == "Select")
                ddlYR.Focus();
            else if (ddlProgNM.Text == "Select")
                ddlProgNM.Focus();
            else if (ddlCourseNM.Text == "--SELECT--")
                ddlCourseNM.Focus();
            else
            {
                Session["SEMESTER"] = "";
                Session["SEMIDNM"] = "";
                Session["SEMID"] = "";
                Session["YEAR"] = "";
                Session["PROGRAMNM"] = "";
                Session["COURSENM"] = "";
                Session["SEMESTERID"] = "";
                Session["PROGRAMID"] = "";
                Session["COURSEID"] = "";
                Session["COURSECD"] = "";
                Session["BATCH"] = "";

                Session["SEMESTER"] = ddlSemNM.Text;
                Session["SEMIDNM"] = ddlSemesterID.SelectedItem;
                Session["SEMID"] = ddlSemesterID.SelectedValue;
                Session["YEAR"] = ddlYR.Text;
                Session["PROGRAMNM"] = ddlProgNM.Text;
                Session["COURSENM"] = ddlCourseNM.SelectedItem;
                Session["COURSECD"] = ddlCourseNM.SelectedValue;
                Session["SEMESTERID"] = lblSemID.Text;
                Session["PROGRAMID"] = lblProgID.Text;
                Session["COURSEID"] = lblCrsID.Text;
                Session["BATCH"] = txtBatch.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                       "OpenWindow", "window.open('/Admission/Report/ResultPrint.aspx','_newtab');", true);
            }
        }

        protected void gv_Result_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + SL;
                string STUDENTID = DataBinder.Eval(e.Row.DataItem, "STUDENTID").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + STUDENTID;
                string STUDENTNM = DataBinder.Eval(e.Row.DataItem, "STUDENTNM").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + STUDENTNM;
                string CGPA = DataBinder.Eval(e.Row.DataItem, "GP").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + CGPA;
                string GRADE = DataBinder.Eval(e.Row.DataItem, "LG").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + GRADE;
                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + REMARKS;


            }
        }

        protected void ddlSemesterID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemesterID.Text != "--SELECT--")
            {
                Global.BindDropDown(ddlProgNM, @"SELECT distinct EIM_PROGRAM.PROGRAMNM NM, EIM_RESULT.PROGRAMID ID
FROM            EIM_PROGRAM INNER JOIN
                         EIM_RESULT ON EIM_PROGRAM.PROGRAMID = EIM_RESULT.PROGRAMID WHERE REGYY='" + ddlYR.Text + "' AND SEMESTERID='" + lblSemID.Text + "' AND SEMID='" + ddlSemesterID.Text + "'");
                ddlProgNM.Focus();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridShow();
        }
    }
}