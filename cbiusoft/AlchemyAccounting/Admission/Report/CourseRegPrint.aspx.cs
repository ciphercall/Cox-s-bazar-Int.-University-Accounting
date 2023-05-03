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
    public partial class CourseRegPrint : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {

            string REGYR = Session["REGYR"].ToString();
            string DATE = Session["DATE"].ToString();
            string SEMESTERNM = Session["SEMESTERNM"].ToString();
            string PROHRAMNM = Session["PROHRAMNM"].ToString();
            string BATCH = Session["BATCH"].ToString();
            string SESSION = Session["SESSION"].ToString();
            string STUDENTID = Session["STUDENTID"].ToString();
            lblRegYR.Text = REGYR;
            lblSemNM.Text = SEMESTERNM;
            lblProNM.Text = PROHRAMNM;
            lblBtch.Text = BATCH;
            lblSession.Text = SESSION;
            lblStuID.Text = STUDENTID;
            gridShow();
        }
        private void gridShow()
        {
            string SEMESTERID = Session["SEMESTERID"].ToString();
            string PROGRAMID = Session["PROHRAMID"].ToString();
            string STUDENTID = Session["STUDENTID"].ToString();
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  (CASE WHEN EIM_COURSEREG.SEMID='01' then '1st' when EIM_COURSEREG.SEMID='02' then '2nd' when EIM_COURSEREG.SEMID='03' then '3rd' when EIM_COURSEREG.SEMID='04' then '4th' " +
            "when EIM_COURSEREG.SEMID='05' then '5th' when EIM_COURSEREG.SEMID='06' then '6th' when EIM_COURSEREG.SEMID='07' then '7th' when EIM_COURSEREG.SEMID='08' then '8th' " +
            "else '' end) AS SEMESTER,EIM_COURSE.COURSECD, EIM_COURSEREG.CREDITHH, EIM_COURSEREG.REMARKS, EIM_COURSEREG.CRCOST " +
                                              "FROM         EIM_COURSEREG INNER JOIN " +
                                              "EIM_COURSE ON EIM_COURSEREG.COURSEID = EIM_COURSE.COURSEID WHERE STUDENTID='" + STUDENTID + "' ORDER BY EIM_COURSE.COURSECD", conn);

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
                string SEMESTER = DataBinder.Eval(e.Row.DataItem, "SEMESTER").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + SEMESTER;
                string COURSEID = DataBinder.Eval(e.Row.DataItem, "COURSECD").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + COURSEID;
                string CREDITHH = DataBinder.Eval(e.Row.DataItem, "CREDITHH").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + CREDITHH;
                string CRCOST = DataBinder.Eval(e.Row.DataItem, "CRCOST").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + CRCOST;
                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + REMARKS;

            }
        }
    }
}