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
    public partial class AdmissionReportPrint : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand(@"SELECT     ROLLNO, STUDENTNM, MOBNO, MRDT, MRNO, MRAMT
            FROM EIM_ADMISSION WHERE TESTYY='" + YEAR + "' AND PROGRAMID='" + PROGRAMID + "' AND SEMESTERID='" + SEMESTERID + "' ORDER BY ROLLNO", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Admission.DataSource = ds;

                gv_Admission.DataBind();


            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Admission.DataSource = ds;
                gv_Admission.DataBind();
                int columncount = gv_Admission.Rows[0].Cells.Count;
                gv_Admission.Rows[0].Cells.Clear();
                gv_Admission.Rows[0].Cells.Add(new TableCell());
                gv_Admission.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Admission.Rows[0].Visible = false;

            }
        }

        protected void gv_Admission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string ROLLNO = DataBinder.Eval(e.Row.DataItem, "ROLLNO").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ROLLNO;
                string STUDENTNM = DataBinder.Eval(e.Row.DataItem, "STUDENTNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + STUDENTNM;
                string MOBNO = DataBinder.Eval(e.Row.DataItem, "MOBNO").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + MOBNO;
                string REMARKS = DataBinder.Eval(e.Row.DataItem, "MRDT").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + REMARKS;
                string MRNO = DataBinder.Eval(e.Row.DataItem, "MRNO").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + MRNO;
                string MRAMT = DataBinder.Eval(e.Row.DataItem, "MRAMT").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + MRAMT;


            }
        }
    }
}