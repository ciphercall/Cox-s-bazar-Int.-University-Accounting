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
namespace AlchemyAccounting.Admission.Report
{
    public partial class StudentReportPrint : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string SEMESTERNM = Session["SEMESTERNM"].ToString();
            string YEAR = Session["YEAR"].ToString();
            string PROGRAMNM = Session["PROGRAMNM"].ToString();
            lblSem.Text = SEMESTERNM;
            lblYR.Text = YEAR;
            lblProg.Text = PROGRAMNM;
            gridShow();
        }
        private void gridShow()
        {
            string YEAR = Session["YEAR"].ToString();
            string SEMESTERID = Session["SEMESTERID"].ToString();
            string PROGRAMID = Session["PROGRAMID"].ToString();
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  ROW_NUMBER() over (order by NEWSTUDENTID) as SL,NEWSTUDENTID,STUDENTNM, MOBNO, SESSION, BATCH,CONVERT(NVARCHAR(10),ADMITDT,103) ADMITTP
                                                      FROM EIM_STUDENT 
            WHERE ADMITYY='" + YEAR + "' AND PROGRAMID='" + PROGRAMID + "' AND SEMESTERID='" + SEMESTERID + "' ", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Student.DataSource = ds;
                gv_Student.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Student.DataSource = ds;
                gv_Student.DataBind();
                int columncount = gv_Student.Rows[0].Cells.Count;
                gv_Student.Rows[0].Cells.Clear();
                gv_Student.Rows[0].Cells.Add(new TableCell());
                gv_Student.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Student.Rows[0].Visible = false;
            }
        }

        protected void gv_Student_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string ROLLNO = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ROLLNO;
                string STUDENTID = DataBinder.Eval(e.Row.DataItem, "NEWSTUDENTID").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + STUDENTID;
                string STUDENTNM = DataBinder.Eval(e.Row.DataItem, "STUDENTNM").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + STUDENTNM;
                string MOBNO = DataBinder.Eval(e.Row.DataItem, "MOBNO").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + MOBNO;
                string REMARKS = DataBinder.Eval(e.Row.DataItem, "SESSION").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + REMARKS;
                string MRNO = DataBinder.Eval(e.Row.DataItem, "BATCH").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + MRNO;
                string MRAMT = DataBinder.Eval(e.Row.DataItem, "ADMITTP").ToString();
                e.Row.Cells[6].Text = "&nbsp;" + MRAMT;
            }
        }
    }
}