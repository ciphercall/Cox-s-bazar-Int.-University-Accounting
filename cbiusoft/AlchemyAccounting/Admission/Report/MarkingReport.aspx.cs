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
using System.Text;
using System.Drawing;

namespace AlchemyAccounting.Admission.Report.UMS_Reports
{
    public partial class MarkingReport : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Global.connection);

        protected void Page_Load(object sender, EventArgs e)
        {
            SubjectShow();
            gridShow();
        }
        private string GL(string Data)
        {
            string GL = "";
            if (Data == "0.00")
                GL = "F";
            else if (Data == "2.00")
                GL = "D";
            else if (Data == "2.25")
                GL = "C";
            else if (Data == "2.50")
                GL = "C+";
            else if (Data == "2.75")
                GL = "B-";
            else if (Data == "3.00")
                GL = "B";
            else if (Data == "3.25")
                GL = "B+";
            else if (Data == "3.50")
                GL = "A-";
            else if (Data == "3.75")
                GL = "A";
            else if (Data == "4.00")
                GL = "A+";
            return GL;
        }
        private void SubjectShow()
        {
            SqlConnection conn = new SqlConnection(Global.connection);

            if (conn.State != ConnectionState.Open)conn.Open();
            int i = 1;
            for (i = 1; i < 7; i++)
            {
                string PROGRAMID = Session["PROGRAMID"].ToString();
                string SEMESTERID = Session["SEMESTERID"].ToString();

                string ID = PROGRAMID + SEMESTERID + i;
                SqlCommand cmd = new SqlCommand(@"select COURSENM as SUB" + i + " from dbo.EIM_COURSE where semsl='" + ID + "'", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (i == 1)
                        lblSub1.Text = dr["SUB" + i + ""].ToString();
                    if (i == 2)
                        lblSub2.Text = dr["SUB" + i + ""].ToString();
                    if (i == 3)
                        lblSub3.Text = dr["SUB" + i + ""].ToString();
                    if (i == 4)
                        lblSub4.Text = dr["SUB" + i + ""].ToString();
                    if (i == 5)
                        lblSub5.Text = dr["SUB" + i + ""].ToString();
                    if (i == 6)
                        lblSub6.Text = dr["SUB" + i + ""].ToString();

                }
                dr.Close();

            }
            if (conn.State != ConnectionState.Closed)conn.Close();

        }
        private void gridShow()
        {
            lblProgNM.Text = Session["PROGRAMNM"].ToString();
            lblSemesterNM.Text = Session["SEMESTERNM"].ToString();
            lblSemester.Text = Session["SESSIONNM"].ToString();
            string PROGRAMID = Session["PROGRAMID"].ToString();
            string SEMESTERID = Session["SEMESTERID"].ToString();
            string SEMID = Session["SESSIONID"].ToString(); if (conn.State != ConnectionState.Open)conn.Open();
            string BATCH = Session["BATCH"].ToString();
            lblBatch.Text = BATCH;
            SqlCommand cmd = new SqlCommand(@"SELECT A.STUDENTNM, A.NEWSTUDENTID,A.STUDENTID, SUM(M40C1) M40C1, SUM(M60C1) M60C1, SUM(TOTC1) TOTC1, SUM(CGPAC1) CGPAC1, SUM(M40C2) M40C2, SUM(M60C2) M60C2, SUM(TOTC2) TOTC2, SUM(CGPAC2) CGPAC2, 
                SUM(M40C3) M40C3, SUM(M60C3) M60C3, SUM(TOTC3) TOTC3, SUM(CGPAC3) CGPAC3, SUM(M40C4) M40C4, SUM(M60C4) M60C4, SUM(TOTC4) TOTC4, SUM(CGPAC4) CGPAC4, 
                SUM(M40C5) M40C5, SUM(M60C5) M60C5, SUM(TOTC5) TOTC5, SUM(CGPAC5) CGPAC5, SUM(M40C6) M40C6, SUM(M60C6) M60C6, SUM(TOTC6) TOTC6, SUM(CGPAC6) CGPAC6 
                FROM(
                SELECT EIM_STUDENT.STUDENTNM,EIM_STUDENT.NEWSTUDENTID,EIM_STUDENT.STUDENTID, M40 M40C1, M60 M60C1, M40+M60 TOTC1, GPA CGPAC1, 0 M40C2, 0 M60C2, 0 TOTC2, 0 CGPAC2, 0 M40C3, 0 M60C3, 0 TOTC3, 0 CGPAC3, 0 M40C4, 0 M60C4, 0 TOTC4, 0 CGPAC4, 0 M40C5, 0 M60C5, 0 TOTC5, 0 CGPAC5, 0 M40C6, 0 M60C6, 0 TOTC6, 0 CGPAC6
                FROM  EIM_COURSEREG INNER JOIN EIM_COURSE ON EIM_COURSE.COURSEID = EIM_COURSEREG.COURSEID INNER JOIN EIM_STUDENT ON EIM_STUDENT.STUDENTID=EIM_COURSEREG.STUDENTID
                LEFT OUTER JOIN EIM_RESULT ON EIM_RESULT.STUDENTID = EIM_COURSEREG.STUDENTID AND EIM_RESULT.COURSEID = EIM_COURSEREG.COURSEID 
                LEFT OUTER JOIN EIM_GRADE ON M40+M60 BETWEEN EIM_GRADE.MARKSFR AND EIM_GRADE.MARKSTO 
                WHERE SEMSL = '" + PROGRAMID + SEMESTERID + 1 + "' AND EIM_COURSEREG.PROGRAMID ='" + PROGRAMID + "' AND EIM_COURSEREG.SEMESTERID = '" + SEMID + "' AND EIM_COURSE.SEMID = '" + SEMESTERID + "' " +
            "UNION " +
            "SELECT EIM_STUDENT.STUDENTNM,EIM_STUDENT.NEWSTUDENTID,EIM_STUDENT.STUDENTID, 0 M40C1, 0 M60C1, 0 TOTC1, 0 CGPAC1, M40 M40C2,  M60 M60C2, M40+M60 TOTC2, GPA CGPAC2, 0 M40C3, 0 M60C3, 0 TOTC3, 0 CGPAC3, 0 M40C4, 0 M60C4, 0 TOTC4, 0 CGPAC4, 0 M40C5, 0 M60C5, 0 TOTC5, 0 CGPAC5, 0 M40C6, 0 M60C6, 0 TOTC6, 0 CGPAC6 " +
            "FROM  EIM_COURSEREG INNER JOIN EIM_COURSE ON EIM_COURSE.COURSEID = EIM_COURSEREG.COURSEID INNER JOIN EIM_STUDENT ON EIM_STUDENT.STUDENTID=EIM_COURSEREG.STUDENTID " +
            "LEFT OUTER JOIN EIM_RESULT ON EIM_RESULT.STUDENTID = EIM_COURSEREG.STUDENTID AND EIM_RESULT.COURSEID = EIM_COURSEREG.COURSEID  " +
            "LEFT OUTER JOIN EIM_GRADE ON M40+M60 BETWEEN EIM_GRADE.MARKSFR AND EIM_GRADE.MARKSTO  " +
            "WHERE SEMSL = '" + PROGRAMID + SEMESTERID + 2 + "' AND EIM_COURSEREG.PROGRAMID = '" + PROGRAMID + "' AND EIM_COURSEREG.SEMESTERID = '" + SEMID + "' AND EIM_COURSE.SEMID = '" + SEMESTERID + "' " +
            "UNION " +
            "SELECT EIM_STUDENT.STUDENTNM,EIM_STUDENT.NEWSTUDENTID,EIM_STUDENT.STUDENTID, 0 M40C1, 0 M60C1, 0 TOTC1, 0 CGPAC1, 0 M40C2, 0 M60C2, 0 TOTC2, 0 CGPAC2, M40 M40C3,  M60 M60C3, M40+M60 TOTC3, GPA CGPAC3, 0 M40C4, 0 M60C4, 0 TOTC4, 0 CGPAC4, 0 M40C5, 0 M60C5, 0 TOTC5, 0 CGPAC5, 0 M40C6, 0 M60C6, 0 TOTC6, 0 CGPAC6 " +
            "FROM  EIM_COURSEREG INNER JOIN EIM_COURSE ON EIM_COURSE.COURSEID = EIM_COURSEREG.COURSEID INNER JOIN EIM_STUDENT ON EIM_STUDENT.STUDENTID=EIM_COURSEREG.STUDENTID " +
            "LEFT OUTER JOIN EIM_RESULT ON EIM_RESULT.STUDENTID = EIM_COURSEREG.STUDENTID AND EIM_RESULT.COURSEID = EIM_COURSEREG.COURSEID  " +
            "LEFT OUTER JOIN EIM_GRADE ON M40+M60 BETWEEN EIM_GRADE.MARKSFR AND EIM_GRADE.MARKSTO  " +
            "WHERE SEMSL = '" + PROGRAMID + SEMESTERID + 3 + "' AND EIM_COURSEREG.PROGRAMID = '" + PROGRAMID + "' AND EIM_COURSEREG.SEMESTERID = '" + SEMID + "' AND EIM_COURSE.SEMID = '" + SEMESTERID + "' " +
            "UNION " +
            "SELECT EIM_STUDENT.STUDENTNM,EIM_STUDENT.NEWSTUDENTID, EIM_STUDENT.STUDENTID,0 M40C1, 0 M60C1, 0 TOTC1, 0 CGPAC1, 0 M40C2, 0 M60C2, 0 TOTC2, 0 CGPAC2, 0 M40C3, 0 M60C3, 0 TOTC3, 0 CGPAC3, M40 M40C4,  M60 M60C4, M40+M60 TOTC4, GPA CGPAC4, 0 M40C5, 0 M60C5, 0 TOTC5, 0 CGPAC5, 0 M40C6, 0 M60C6, 0 TOTC6, 0 CGPAC6 " +
            "FROM  EIM_COURSEREG INNER JOIN EIM_COURSE ON EIM_COURSE.COURSEID = EIM_COURSEREG.COURSEID INNER JOIN EIM_STUDENT ON EIM_STUDENT.STUDENTID=EIM_COURSEREG.STUDENTID " +
            "LEFT OUTER JOIN EIM_RESULT ON EIM_RESULT.STUDENTID = EIM_COURSEREG.STUDENTID AND EIM_RESULT.COURSEID = EIM_COURSEREG.COURSEID  " +
            "LEFT OUTER JOIN EIM_GRADE ON M40+M60 BETWEEN EIM_GRADE.MARKSFR AND EIM_GRADE.MARKSTO  " +
            "WHERE SEMSL = '" + PROGRAMID + SEMESTERID + 4 + "' AND EIM_COURSEREG.PROGRAMID = '" + PROGRAMID + "' AND EIM_COURSEREG.SEMESTERID = '" + SEMID + "' AND EIM_COURSE.SEMID = '" + SEMESTERID + "' " +
            "UNION " +
            "SELECT EIM_STUDENT.STUDENTNM,EIM_STUDENT.NEWSTUDENTID,EIM_STUDENT.STUDENTID, 0 M40C1, 0 M60C1, 0 TOTC1, 0 CGPAC1, 0 M40C2, 0 M60C2, 0 TOTC2, 0 CGPAC2, 0 M40C3, 0 M60C3, 0 TOTC3, 0 CGPAC3, 0 M40C4, 0 M60C4, 0 TOTC4, 0 CGPAC4, M40 M40C5,  M60 M60C5, M40+M60 TOTC5, GPA CGPAC5, 0 M40C6, 0 M60C6, 0 TOTC6, 0 CGPAC6 " +
            "FROM  EIM_COURSEREG INNER JOIN EIM_COURSE ON EIM_COURSE.COURSEID = EIM_COURSEREG.COURSEID INNER JOIN EIM_STUDENT ON EIM_STUDENT.STUDENTID=EIM_COURSEREG.STUDENTID " +
            "LEFT OUTER JOIN EIM_RESULT ON EIM_RESULT.STUDENTID = EIM_COURSEREG.STUDENTID AND EIM_RESULT.COURSEID = EIM_COURSEREG.COURSEID  " +
            "LEFT OUTER JOIN EIM_GRADE ON M40+M60 BETWEEN EIM_GRADE.MARKSFR AND EIM_GRADE.MARKSTO  " +
            "WHERE SEMSL = '" + PROGRAMID + SEMESTERID + 5 + "' AND EIM_COURSEREG.PROGRAMID = '" + PROGRAMID + "' AND EIM_COURSEREG.SEMESTERID = '" + SEMID + "' AND EIM_COURSE.SEMID = '" + SEMESTERID + "' " +
            "UNION " +
            "SELECT EIM_STUDENT.STUDENTNM,EIM_STUDENT.NEWSTUDENTID,EIM_STUDENT.STUDENTID, 0 M40C1, 0 M60C1, 0 TOTC1, 0 CGPAC1, 0 M40C2, 0 M60C2, 0 TOTC2, 0 CGPAC2, 0 M40C3, 0 M60C3, 0 TOTC3, 0 CGPAC3, 0 M40C4, 0 M60C4, 0 TOTC4, 0 CGPAC4, 0 M40C5, 0 M60C5, 0 TOTC5, 0 CGPAC5, M40 M40C6,  M60 M60C6, M40+M60 TOTC6, GPA CGPAC6  " +
            "FROM  EIM_COURSEREG INNER JOIN EIM_COURSE ON EIM_COURSE.COURSEID = EIM_COURSEREG.COURSEID INNER JOIN EIM_STUDENT ON EIM_STUDENT.STUDENTID=EIM_COURSEREG.STUDENTID " +
            "LEFT OUTER JOIN EIM_RESULT ON EIM_RESULT.STUDENTID = EIM_COURSEREG.STUDENTID AND EIM_RESULT.COURSEID = EIM_COURSEREG.COURSEID  " +
            "LEFT OUTER JOIN EIM_GRADE ON M40+M60 BETWEEN EIM_GRADE.MARKSFR AND EIM_GRADE.MARKSTO  " +
            "WHERE SEMSL ='" + PROGRAMID + SEMESTERID + 6 + "' AND EIM_COURSEREG.PROGRAMID = '" + PROGRAMID + "' AND EIM_COURSEREG.SEMESTERID = '" + SEMID + "' AND EIM_COURSE.SEMID = '" + SEMESTERID + "' " +
            ") A GROUP BY A.NEWSTUDENTID,A.STUDENTID,A.STUDENTNM", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_CrsReg.DataSource = ds;
                gv_CrsReg.DataBind();

            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_CrsReg.DataSource = ds;
                gv_CrsReg.DataBind();
                int columncount = gv_CrsReg.Rows[0].Cells.Count;
                gv_CrsReg.Rows[0].Cells.Clear();
                gv_CrsReg.Rows[0].Cells.Add(new TableCell());
                gv_CrsReg.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_CrsReg.Rows[0].Visible = false;

            }
        }

        protected void gv_CrsReg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string STUDENTNM = DataBinder.Eval(e.Row.DataItem, "STUDENTNM").ToString();
                e.Row.Cells[0].Text = STUDENTNM;
                string STUID = DataBinder.Eval(e.Row.DataItem, "STUDENTID").ToString();
                string STUDENTID = DataBinder.Eval(e.Row.DataItem, "NEWSTUDENTID").ToString();
                e.Row.Cells[1].Text = STUDENTID;

                string M40C1 = DataBinder.Eval(e.Row.DataItem, "M40C1").ToString();
                e.Row.Cells[2].Text = M40C1;

                string M60C1 = DataBinder.Eval(e.Row.DataItem, "M60C1").ToString();
                e.Row.Cells[3].Text = M60C1;

                string TOTC1 = DataBinder.Eval(e.Row.DataItem, "TOTC1").ToString();
                e.Row.Cells[4].Text = TOTC1;

                string CGPAC1 = DataBinder.Eval(e.Row.DataItem, "CGPAC1").ToString();
                e.Row.Cells[5].Text = CGPAC1.ToString();

                e.Row.Cells[6].Text = GL(CGPAC1);
                if (e.Row.Cells[6].Text == "F")
                {
                    e.Row.Cells[6].ForeColor = Color.Red;
                    e.Row.Cells[6].Font.Bold = true;
                }

                string M40C2 = DataBinder.Eval(e.Row.DataItem, "M40C2").ToString();
                e.Row.Cells[7].Text = M40C2;

                string M60C2 = DataBinder.Eval(e.Row.DataItem, "M60C2").ToString();
                e.Row.Cells[8].Text = M60C2;

                string TOTC2 = DataBinder.Eval(e.Row.DataItem, "TOTC2").ToString();
                e.Row.Cells[9].Text = TOTC2;

                string CGPAC2 = DataBinder.Eval(e.Row.DataItem, "CGPAC2").ToString();
                e.Row.Cells[10].Text = CGPAC2.ToString();


                e.Row.Cells[11].Text = GL(CGPAC2);
                if (e.Row.Cells[11].Text == "F")
                {
                    e.Row.Cells[11].ForeColor = Color.Red;
                    e.Row.Cells[11].Font.Bold = true;
                }

                string M40C3 = DataBinder.Eval(e.Row.DataItem, "M40C3").ToString();
                e.Row.Cells[12].Text = M40C3;

                string M60C3 = DataBinder.Eval(e.Row.DataItem, "M60C3").ToString();
                e.Row.Cells[13].Text = M60C3;

                string TOTC3 = DataBinder.Eval(e.Row.DataItem, "TOTC3").ToString();
                e.Row.Cells[14].Text = TOTC3;

                string CGPAC3 = DataBinder.Eval(e.Row.DataItem, "CGPAC3").ToString();
                e.Row.Cells[15].Text = CGPAC3.ToString();

                e.Row.Cells[16].Text = GL(CGPAC3);
                if (e.Row.Cells[16].Text == "F")
                {
                    e.Row.Cells[16].ForeColor = Color.Red;
                    e.Row.Cells[16].Font.Bold = true;
                }

                string M40C4 = DataBinder.Eval(e.Row.DataItem, "M40C4").ToString();
                e.Row.Cells[17].Text = M40C4;

                string M60C4 = DataBinder.Eval(e.Row.DataItem, "M60C4").ToString();
                e.Row.Cells[18].Text = M60C4;

                string TOTC4 = DataBinder.Eval(e.Row.DataItem, "TOTC4").ToString();
                e.Row.Cells[19].Text = TOTC4;

                string CGPAC4 = DataBinder.Eval(e.Row.DataItem, "CGPAC4").ToString();
                e.Row.Cells[20].Text = CGPAC4.ToString();

                e.Row.Cells[21].Text = GL(CGPAC4);
                if (e.Row.Cells[21].Text == "F")
                {
                    e.Row.Cells[21].ForeColor = Color.Red;
                    e.Row.Cells[21].Font.Bold = true;
                }

                string M40C5 = DataBinder.Eval(e.Row.DataItem, "M40C5").ToString();
                e.Row.Cells[22].Text = M40C5;

                string M60C5 = DataBinder.Eval(e.Row.DataItem, "M60C5").ToString();
                e.Row.Cells[23].Text = M60C5;

                string TOTC5 = DataBinder.Eval(e.Row.DataItem, "TOTC5").ToString();
                e.Row.Cells[24].Text = TOTC5;

                string CGPAC5 = DataBinder.Eval(e.Row.DataItem, "CGPAC5").ToString();
                e.Row.Cells[25].Text = CGPAC5.ToString();

                e.Row.Cells[26].Text = GL(CGPAC5);
                if (e.Row.Cells[26].Text == "F")
                {
                    e.Row.Cells[26].ForeColor = Color.Red;
                    e.Row.Cells[26].Font.Bold = true;
                }

                string M40C6 = DataBinder.Eval(e.Row.DataItem, "M40C6").ToString();
                e.Row.Cells[27].Text = M40C6;

                string M60C6 = DataBinder.Eval(e.Row.DataItem, "M60C6").ToString();
                e.Row.Cells[28].Text = M60C6;

                string TOTC6 = DataBinder.Eval(e.Row.DataItem, "TOTC6").ToString();
                e.Row.Cells[29].Text = TOTC6;

                string CGPAC6 = DataBinder.Eval(e.Row.DataItem, "CGPAC6").ToString();
                e.Row.Cells[30].Text = CGPAC6.ToString();

                e.Row.Cells[31].Text = GL(CGPAC6);
                if (e.Row.Cells[31].Text == "F")
                {
                    e.Row.Cells[31].ForeColor = Color.Red;
                    e.Row.Cells[31].Font.Bold = true;
                }


                string SEMID = "0" + Session["SESSIONID"].ToString();
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
WHERE        (EIM_COURSEREG.SEMID <> '00') AND (EIM_COURSEREG.STUDENTID = '" + STUID + "') AND EIM_RESULT.SEMID='" + SEMID + "') A )B )C");
                if (RESULT == "0.00" || RESULT == "")
                {
                    RESULT = "0";
                    if (RESULT == "F")
                    {
                        e.Row.Cells[32].ForeColor = Color.Red;
                        e.Row.Cells[32].Font.Bold = true;
                    }
                }
                e.Row.Cells[32].Text = Convert.ToDecimal(RESULT).ToString("F2");

            }
        }

        protected void gv_CrsReg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}