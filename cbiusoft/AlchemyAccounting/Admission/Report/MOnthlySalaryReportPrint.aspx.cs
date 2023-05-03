using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.Admission.Report
{
    public partial class MOnthlySalaryReportPrint : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection conn = new SqlConnection(Global.connection);
        int intSubTotalIndex = 1;
        string strPreviousRowID = string.Empty;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gridShow();

            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }

        }
        private void gridShow()
        {
            lblMonth.Text = Session["TRANSMY"].ToString();
            lblDept.Text = Session["DEPTNM"].ToString();
            string DEPTID = Session["DEPTID"].ToString();
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  HR_EMP.EMPNM, HR_POST.POSTNM, CONVERT(NVARCHAR(10),HR_EMP.JOININGDT,103) AS JOININGDT, HR_SALGRANT.MMDAY, HR_SALGRANT.HDAY, HR_SALGRANT.PREDAY, HR_SALGRANT.ABSDAY, HR_SALGRANT.LDAY, 
                      HR_SALGRANT.BASICSAL, HR_SALGRANT.ALLOWANCE, HR_SALGRANT.TOTPAID, HR_SALGRANT.ADVANCE, HR_SALGRANT.NETPAID, HR_EMP.BANKACNO
                      FROM  HR_SALGRANT INNER JOIN
                      HR_EMP ON HR_SALGRANT.EMPID = HR_EMP.EMPID INNER JOIN
                      HR_POST ON HR_SALGRANT.POSTID = HR_POST.POSTID WHERE HR_SALGRANT.TRANSMY='" + lblMonth.Text + "' AND  HR_EMP.DEPTID='" + DEPTID + "' ORDER BY HR_EMP.EMPID", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Visible = false;

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string EMPNM = DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + EMPNM;

                string POSTNM = DataBinder.Eval(e.Row.DataItem, "POSTNM").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + POSTNM;

                string JOININGDT = DataBinder.Eval(e.Row.DataItem, "JOININGDT").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + JOININGDT;

                string MMDAY = DataBinder.Eval(e.Row.DataItem, "MMDAY").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + MMDAY;

                string HDAY = DataBinder.Eval(e.Row.DataItem, "HDAY").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + HDAY;

                string PREDAY = DataBinder.Eval(e.Row.DataItem, "PREDAY").ToString();
                e.Row.Cells[6].Text = "&nbsp;" + PREDAY;

                string ABSDAY = DataBinder.Eval(e.Row.DataItem, "ABSDAY").ToString();
                e.Row.Cells[7].Text = "&nbsp;" + ABSDAY;

                string LDAY = DataBinder.Eval(e.Row.DataItem, "LDAY").ToString();
                e.Row.Cells[8].Text = "&nbsp;" + LDAY;

                string BASICSAL = DataBinder.Eval(e.Row.DataItem, "BASICSAL").ToString();
                e.Row.Cells[9].Text = "&nbsp;" + BASICSAL;

                string ALLOWANCE = DataBinder.Eval(e.Row.DataItem, "ALLOWANCE").ToString();
                e.Row.Cells[10].Text = "&nbsp;" + ALLOWANCE;

                string TOTPAID = DataBinder.Eval(e.Row.DataItem, "TOTPAID").ToString();
                e.Row.Cells[11].Text = "&nbsp;" + TOTPAID;

                string ADVANCE = DataBinder.Eval(e.Row.DataItem, "ADVANCE").ToString();
                e.Row.Cells[12].Text = "&nbsp;" + ADVANCE;

                string NETPAID = DataBinder.Eval(e.Row.DataItem, "NETPAID").ToString();
                e.Row.Cells[13].Text = "&nbsp;" + NETPAID;

                string BANKACNO = DataBinder.Eval(e.Row.DataItem, "BANKACNO").ToString();
                e.Row.Cells[14].Text = "&nbsp;" + BANKACNO;
            }
        }
    }
}
