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

namespace AlchemyAccounting.Admission.Report
{
    public partial class CourseEnrollPrint : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string SEMESTER = Session["SEMESTER"].ToString();
            string YEAR = Session["YEAR"].ToString();
            string COURSE = Session["COURSE"].ToString();
            lblSem.Text = SEMESTER;
            lblYR.Text = YEAR;
            lblCrs.Text = COURSE;
            gridShow();
        }
        private void gridShow()
        {

            string YEAR = Session["YEAR"].ToString();
            string SEMESTERID = Session["SEMESTERID"].ToString();
            string COURSEID = Session["COURSEID"].ToString();
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT     EIM_STUDENT.STUDENTID, EIM_STUDENT.NEWSTUDENTID, EIM_STUDENT.STUDENTNM,convert(nvarchar(10),EIM_COURSEREG.ENRLDT,103) AS ENRLDT, EIM_COURSEREG.REMARKS, EIM_COURSE.COURSENM
                        FROM  EIM_STUDENT INNER JOIN
                      EIM_COURSEREG ON EIM_STUDENT.STUDENTID = EIM_COURSEREG.STUDENTID INNER JOIN
                      EIM_COURSE ON EIM_COURSEREG.COURSEID = EIM_COURSE.COURSEID WHERE EIM_COURSEREG.REGYY='" + YEAR + "' AND EIM_COURSEREG.COURSEID='" + COURSEID + "' AND EIM_COURSEREG.SEMESTERID='" + SEMESTERID + "' ORDER BY EIM_COURSEREG.STUDENTID", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Course.DataSource = ds;

                gv_Course.DataBind();


            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Course.DataSource = ds;
                gv_Course.DataBind();
                int columncount = gv_Course.Rows[0].Cells.Count;
                gv_Course.Rows[0].Cells.Clear();
                gv_Course.Rows[0].Cells.Add(new TableCell());
                gv_Course.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Course.Rows[0].Visible = false;

            }
        }
        protected void gv_Course_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string STUDENTNM = DataBinder.Eval(e.Row.DataItem, "STUDENTNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + STUDENTNM;
                string STUDENTID = DataBinder.Eval(e.Row.DataItem, "STUDENTID").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + STUDENTID;
                string ENRLDT = DataBinder.Eval(e.Row.DataItem, "ENRLDT").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ENRLDT;
                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + REMARKS;


            }
        }
    }
}