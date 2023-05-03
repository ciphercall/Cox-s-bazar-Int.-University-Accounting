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
    public partial class Student_wise_due_statement_Report : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection conn = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            gridShow();
        }
        private void gridShow()
        {
            string PROGID = Session["PROGRAMID"].ToString();
            string PROGNM = Session["PROGRAMNM"].ToString();
            string Date = Session["DATE"].ToString();
            string batch = Session["BATCHH"].ToString();
            //string semyy=Session["SEM-YEAR"].ToString();
            lblProgNM.Text = PROGNM + " - " + batch+" Batch";
            lblDate.Text = Date;
            //string Year = Global.Slipt(semyy,1,'-');
            //string Sem = Global.Slipt(semyy, 0, '-');
            //Sem = Global.GetData("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='"+Sem+"'");
            DateTime FRDT = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string DT = FRDT.ToString("yyyy/MM/dd");
            //SqlCommand cmd = new SqlCommand(@"SELECT A.STUDENTID, STUDENTNM, ADMITYY, MOBNO, SUM(ISNULL(DRAMT,0)) DRAMT, SUM(ISNULL(CRAMT,0)) CRAMT, SUM(ISNULL(DRAMT,0)) - SUM(ISNULL(CRAMT,0)) BAMT " +
            //"FROM( " +
            //"SELECT STUDENTID, SUM(ISNULL(AMOUNT,0)) DRAMT, 0 CRAMT FROM EIM_TRANS " +
            //"WHERE PROGRAMID = '" + PROGID + "' AND TRANSDT <= '" + DT + "' AND TRANSTP = 'JOUR' " +
            //"GROUP BY STUDENTID " +
            //"UNION " +
            //"SELECT STUDENTID, 0 DRAMT, SUM(ISNULL(AMOUNT,0)) CRAMT FROM EIM_TRANS " +
            //"WHERE PROGRAMID = '" + PROGID + "'  AND TRANSDT <= '" + DT + "' AND TRANSTP = 'MREC' " +
            //"GROUP BY STUDENTID " +
            //") A INNER JOIN EIM_STUDENT ON EIM_STUDENT.STUDENTID = A.STUDENTID " +
            //"GROUP BY A.STUDENTID, STUDENTNM, ADMITYY, MOBNO", conn);
            SqlCommand cmd = new SqlCommand(@"SELECT A.STUDENTID,NEWSTUDENTID, STUDENTNM, ADMITYY, MOBNO, SUM(ISNULL(DRAMT,0)) DRAMT, SUM(ISNULL(CRAMT,0)) CRAMT, SUM(ISNULL(DRAMT,0)) - SUM(ISNULL(CRAMT,0)) BAMT FROM
            ( SELECT STUDENTID, SUM(ISNULL(AMOUNT,0)) DRAMT, 0 CRAMT FROM EIM_TRANS 
            WHERE PROGRAMID = '" + PROGID + "' AND TRANSDT <= '" + DT + "' AND TRANSTP = 'JOUR' " +
            "GROUP BY STUDENTID  " +
            "UNION " +
            "SELECT B.STUDENTID, SUM(ISNULL(AMOUNT,0)) DRAMT, 0 CRAMT FROM EIM_TRANS A LEFT OUTER JOIN EIM_STUDENT B ON A.STUDENTID = B.MIGRATESID " +
            "WHERE B.PROGRAMID = '" + PROGID + "' AND TRANSDT <= '" + DT + "' AND TRANSTP = 'JOUR'  AND B.BATCH='" + batch + "' " +
            "GROUP BY B.STUDENTID  " +
            "UNION  " +
            "SELECT STUDENTID, 0 DRAMT, SUM(ISNULL(AMOUNT,0)) CRAMT FROM EIM_TRANS  " +
            "WHERE PROGRAMID = '" + PROGID + "'  AND TRANSDT <= '" + DT + "' AND TRANSTP = 'MREC' " +
            "GROUP BY STUDENTID " +
            "UNION " +
            "SELECT B.STUDENTID, 0 DRAMT, SUM(ISNULL(AMOUNT,0)) CRAMT FROM EIM_TRANS A LEFT OUTER JOIN EIM_STUDENT B ON A.STUDENTID = B.MIGRATESID " +
            "WHERE  B.PROGRAMID = '" + PROGID + "'  AND TRANSDT <= '" + DT + "' AND TRANSTP = 'MREC'  AND B.BATCH='" + batch + "' " +
            "GROUP BY B.STUDENTID " +
            ") A INNER JOIN EIM_STUDENT ON EIM_STUDENT.STUDENTID = A.STUDENTID WHERE BATCH='" + batch + "' " +
            "GROUP BY A.STUDENTID,NEWSTUDENTID, STUDENTNM, ADMITYY, MOBNO ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grid.DataSource = ds;
                Grid.DataBind();

            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                Grid.DataSource = ds;
                Grid.DataBind();
                int columncount = Grid.Rows[0].Cells.Count;
                Grid.Rows[0].Cells.Clear();
                Grid.Rows[0].Cells.Add(new TableCell());
                Grid.Rows[0].Cells[0].ColumnSpan = columncount;
                Grid.Rows[0].Visible = false;

            }
        }
        protected void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string STUDENTID = DataBinder.Eval(e.Row.DataItem, "NEWSTUDENTID").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + STUDENTID;
                string STUDENTNM = DataBinder.Eval(e.Row.DataItem, "STUDENTNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + STUDENTNM;
                string ADMITYY = DataBinder.Eval(e.Row.DataItem, "ADMITYY").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ADMITYY;
                string MOBNO = DataBinder.Eval(e.Row.DataItem, "MOBNO").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + MOBNO;
                string DRAMT = DataBinder.Eval(e.Row.DataItem, "DRAMT").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + DRAMT;
                string CRAMT = DataBinder.Eval(e.Row.DataItem, "CRAMT").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + CRAMT;
                decimal BAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BAMT").ToString());
                e.Row.Cells[6].Text = "&nbsp;" + BAMT;
            }
        }
    }
}