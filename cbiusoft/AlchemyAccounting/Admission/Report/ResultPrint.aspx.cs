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
using System.Text;
using System.Collections.Specialized;

namespace AlchemyAccounting.Admission.Report
{
    public partial class ResultPrint : System.Web.UI.Page
    {
        int intSubTotalIndex = 1;
        string strPreviousRowID = string.Empty;
        SqlConnection conn = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string SEMESTER = Session["SEMESTER"].ToString();
            string YEAR = Session["YEAR"].ToString();
            string PROGRAMNM = Session["PROGRAMNM"].ToString();
            string COURSENM = Session["COURSENM"].ToString();
            string SEMID = Session["SEMID"].ToString();
            string BATCH = Session["BATCH"].ToString();
            string COURSECD = Session["COURSECD"].ToString();
            lblSemesterID.Text = Session["SEMIDNM"].ToString(); ;
            lblSemNM.Text = SEMESTER;
            lblYR.Text = YEAR;
            lblProgNM.Text = PROGRAMNM;
            lblCourseNM.Text = COURSENM;
            lblBatch.Text = BATCH;
            lblCourseCD.Text = COURSECD;
            gridShow();
        }
        private void gridShow()
        {
            string SEMID = Session["SEMID"].ToString();
            string YEAR = Session["YEAR"].ToString();
            string SEMESTERID = Session["SEMESTERID"].ToString();
            string PROGRAMID = Session["PROGRAMID"].ToString();
            string COURSEID = Session["COURSEID"].ToString();
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  SL,NEWSTUDENTID, STUDENTNM,
             MARK, CASE WHEN MARK>79 THEN 4.00 WHEN  MARK>74 THEN  3.75 WHEN MARK>69 THEN 3.50 WHEN MARK>64 THEN 3.25 
            WHEN MARK>59 THEN 3.00 WHEN MARK>54 THEN 2.75 WHEN MARK BETWEEN 49 AND 55 THEN 2.50 WHEN MARK>44 THEN 2.25 WHEN MARK>39 THEN 2.00
            ELSE 0.00 END   GP ,
            CASE WHEN MARK>79 THEN 'A+' WHEN  MARK>74 THEN  'A' WHEN MARK>69 THEN 'A-' WHEN MARK>64 THEN 'B+' 
            WHEN MARK>59 THEN 'B' WHEN MARK>54 THEN 'B-' WHEN MARK>49 THEN 'C+' WHEN MARK>44 THEN 'C' WHEN MARK>39 THEN 'D' 
            ELSE 'F' END LG,
            REMARKS
            FROM( SELECT    ROW_NUMBER() over (order by EIM_RESULT.STUDENTID) as SL ,EIM_STUDENT.NEWSTUDENTID,EIM_STUDENT.STUDENTNM,
            (EIM_RESULT.M40+EIM_RESULT.M60) MARK, EIM_RESULT.REMARKS 
            FROM EIM_RESULT INNER JOIN EIM_STUDENT ON EIM_RESULT.STUDENTID = EIM_STUDENT.STUDENTID  
            WHERE EIM_RESULT.REGYY='" + YEAR + "' AND EIM_RESULT.PROGRAMID='" + PROGRAMID + "' AND EIM_RESULT.SEMESTERID='" + SEMESTERID + "'  AND COURSEID='" + COURSEID + "'  AND EIM_RESULT.SEMID='" + SEMID + "') A  ", conn);

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
        protected void gv_Result_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + SL;
                //string SEM = DataBinder.Eval(e.Row.DataItem, "SEM").ToString();
                //e.Row.Cells[1].Text = "&nbsp;" + SEM;
                string STUDENTID = DataBinder.Eval(e.Row.DataItem, "NEWSTUDENTID").ToString();
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
    }
}