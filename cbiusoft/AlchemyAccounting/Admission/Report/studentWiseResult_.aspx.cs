using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.Report
{
    public partial class studentWiseResult_ : System.Web.UI.Page
    {
        int intSubTotalIndex = 1;
        string strPreviousRowID = string.Empty;
        decimal credit = 0;
        decimal GradeP = 0;
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection conn = new SqlConnection(Global.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string StudentID = Session["STUID"].ToString();
                    showrepeat1();
                    Global.FormView(FormView1, @"SELECT EIM_STUDENT.STUDENTNM, EIM_STUDENT.NEWSTUDENTID,EIM_STUDENT.BATCH, EIM_SEMESTER.SEMESTERNM+'-'+EIM_STUDENT.ADMITYY SEM, EIM_PROGRAM.PROGRAMNM
FROM            EIM_PROGRAM INNER JOIN
                         EIM_STUDENT ON EIM_PROGRAM.PROGRAMID = EIM_STUDENT.PROGRAMID INNER JOIN
                         EIM_SEMESTER ON EIM_STUDENT.SEMESTERID = EIM_SEMESTER.SEMESTERID WHERE EIM_STUDENT.STUDENTID='" + StudentID + "'");
                }

            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }
        public void showrepeat1()
        {
            string StudentID = Session["STUID"].ToString();
            SqlConnection conn = new SqlConnection(Global.connection);

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand(@" SELECT DISTINCT  EIM_COURSEREG.SEMID   
FROM            EIM_COURSE INNER JOIN
                         EIM_COURSEREG ON EIM_COURSE.COURSEID = EIM_COURSEREG.COURSEID INNER JOIN
                         EIM_RESULT ON EIM_COURSEREG.STUDENTID = EIM_RESULT.STUDENTID AND EIM_COURSEREG.COURSEID = EIM_RESULT.COURSEID AND EIM_COURSEREG.SEMID = EIM_RESULT.SEMID AND 
                         EIM_COURSEREG.PROGRAMID = EIM_RESULT.PROGRAMID
WHERE        (EIM_COURSEREG.SEMID <> '00') AND (EIM_COURSEREG.STUDENTID = '" + StudentID + "') ORDER BY SEMID ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater1.Visible = true;
            }
            else
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater1.Visible = true;
            }
        }
        public int o = 1;
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Label lblText = (Label)e.Item.FindControl("lblText");
            Label lblSemID = (Label)e.Item.FindControl("lblSemID");

            GridView GridView1 = (GridView)(e.Item.FindControl("GridView1"));
            DataTable DT1 = LoadData1(lblSemID.Text);
            if (DT1.Rows.Count > 0)
            {
                if (o == 1)
                {
                    GridView1.ShowHeader = true;
                    o++;
                }
                GridView1.DataSource = DT1;
                GridView1.DataBind();
                MergeRows(GridView1);
            }
            else
            {
                GridView1.Visible = false;
            }

        }
        private DataTable LoadData1(string SEMID)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            DataTable dtGrid = new DataTable();
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand();
            string StudentID = Session["STUID"].ToString();
            cmd = new SqlCommand(@"SELECT ROW_NUMBER( ) over(order by SEMID,COURSECD) AS SL, *,CASE WHEN SEMID='01' THEN '1st Semester' WHEN SEMID='02' THEN '2nd Semester' WHEN SEMID='03' THEN '3rd Semester' WHEN SEMID='04' THEN '4th Semester' 
WHEN SEMID='5th' THEN '5th Semester' WHEN SEMID='06' THEN '6th Semester' WHEN SEMID='07' THEN '7th Semester' WHEN SEMID='08' THEN '8th Semester' END SEMESTERNM,
CASE WHEN MARK>79 THEN 4.00 WHEN  MARK>74 THEN  3.75 WHEN MARK>69 THEN 3.50 WHEN MARK>64 THEN 3.25 
WHEN MARK>59 THEN 3.00 WHEN MARK>54 THEN 2.75 WHEN MARK BETWEEN 49 AND 55 THEN 2.50 WHEN MARK>44 THEN 2.25 WHEN MARK>39 THEN 2.00 ELSE 0.00 END   GP ,
CASE WHEN MARK>79 THEN 'A+' WHEN  MARK>74 THEN  'A' WHEN MARK>69 THEN 'A-' WHEN MARK>64 THEN 'B+' 
WHEN MARK>59 THEN 'B' WHEN MARK>54 THEN 'B-' WHEN MARK>49 THEN 'C+' WHEN MARK>44 THEN 'C' WHEN MARK>39 THEN 'D' ELSE 'F' END LG
 FROM(
SELECT DISTINCT  EIM_COURSEREG.SEMID, EIM_COURSE.COURSECD, EIM_COURSE.COURSENM, EIM_COURSE.CREDITHH, EIM_RESULT.CGPA, EIM_RESULT.GRADE, ISNULL(EIM_RESULT.M40, 0) + ISNULL(EIM_RESULT.M60, 
                         0) AS Mark, EIM_COURSEREG.STUDENTID
FROM            EIM_COURSE INNER JOIN
                         EIM_COURSEREG ON EIM_COURSE.COURSEID = EIM_COURSEREG.COURSEID INNER JOIN
                         EIM_RESULT ON EIM_COURSEREG.STUDENTID = EIM_RESULT.STUDENTID AND EIM_COURSEREG.COURSEID = EIM_RESULT.COURSEID AND EIM_COURSEREG.SEMID = EIM_RESULT.SEMID AND 
                         EIM_COURSEREG.PROGRAMID = EIM_RESULT.PROGRAMID
WHERE        (EIM_COURSEREG.SEMID <> '00') AND (EIM_COURSEREG.STUDENTID = '" + StudentID + "') AND EIM_RESULT.SEMID='" + SEMID + "') A ORDER BY COURSECD", conn);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtGrid);
            if (conn.State != ConnectionState.Closed)conn.Close();
            return dtGrid;
        }

        protected void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = 0; i <= 6; i++)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text && (i == 0 || i == 6))
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }

                }

            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "SEMID").ToString();
                Decimal CREDITHH = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CREDITHH").ToString());
                //e.Row.Cells[0].Text = CREDITHH.ToString(); 
                string GP = DataBinder.Eval(e.Row.DataItem, "GP").ToString();
                string STUDENTID = DataBinder.Eval(e.Row.DataItem, "STUDENTID").ToString();
                string RESULT = Global.GetData(@"SELECT SUM(TOTAL)/SUM(CREDITHH) POINT FROM (
SELECT CREDITHH*GP TOTAL,* FROM (
SELECT CREDITHH,  
CASE WHEN MARK>79 THEN 4.00 WHEN  MARK>74 THEN  3.75 WHEN MARK>69 THEN 3.50 WHEN MARK>64 THEN 3.25 
WHEN MARK>59 THEN 3.00 WHEN MARK>54 THEN 2.75 WHEN MARK BETWEEN 49 AND 55 THEN 2.50 WHEN MARK>44 THEN 2.25 WHEN MARK>39 THEN 2.00 ELSE 0.00 END   GP
FROM(SELECT DISTINCT  EIM_COURSE.CREDITHH, ISNULL(EIM_RESULT.M40, 0) + ISNULL(EIM_RESULT.M60, 0) AS Mark 
FROM            EIM_COURSE INNER JOIN
EIM_COURSEREG ON EIM_COURSE.COURSEID = EIM_COURSEREG.COURSEID INNER JOIN
EIM_RESULT ON EIM_COURSEREG.STUDENTID = EIM_RESULT.STUDENTID AND EIM_COURSEREG.COURSEID = EIM_RESULT.COURSEID AND EIM_COURSEREG.SEMID = EIM_RESULT.SEMID AND 
EIM_COURSEREG.PROGRAMID = EIM_RESULT.PROGRAMID
WHERE        (EIM_COURSEREG.SEMID <> '00') AND (EIM_COURSEREG.STUDENTID = '" + STUDENTID + "') AND EIM_RESULT.SEMID='" + strPreviousRowID + "') A )B )C");
                if (RESULT == "")
                    RESULT = "0";
                e.Row.Cells[6].Text = Convert.ToDecimal(RESULT).ToString("F2");
                credit += CREDITHH;
            }
        }

    }
}