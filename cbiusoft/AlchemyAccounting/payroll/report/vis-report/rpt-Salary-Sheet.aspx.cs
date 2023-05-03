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
using System.Collections.Specialized;
using System.Threading;
using System.Collections;

namespace AlchemyAccounting.payroll.report.vis_report
{
    public partial class rpt_Salary_Sheet : System.Web.UI.Page
    {
        decimal netAmt = 0;
        string netAmtComma = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string month = Session["month"].ToString();
                lblMonth.Text = month;
                Global.lblAdd("SELECT DAYS FROM HR_MONTH WHERE MONTH ='" + lblMonth.Text + "'", lblMonthDay);

                ShowGrid();
            }
        }

        private void ShowGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string month = Session["month"].ToString();

            SqlCommand cmd = new SqlCommand("SELECT HR_SALGRANT.TRANSMY, HR_SALGRANT.MMDAYS, HR_SALGRANT.NMDAYS, HR_SALGRANT.OTDAYS, HR_SALGRANT.RATEPD, HR_SALGRANT.RATEPH, " +
                      " HR_SALGRANT.TOTHOUR, HR_SALGRANT.TOTAMT, HR_SALGRANT.BASIC, HR_SALGRANT.FOOD, HR_SALGRANT.OTCADD, HR_SALGRANT.GROSSAMT, " +
                      " HR_SALGRANT.ADVANCE, HR_SALGRANT.PENALTY, HR_SALGRANT.OTCDED, HR_SALGRANT.NETAMT, HR_EMP.EMPNM FROM HR_SALGRANT INNER JOIN " +
                      " HR_EMP ON HR_SALGRANT.EMPID = HR_EMP.EMPID WHERE HR_SALGRANT.TRANSMY=@month AND HR_SALGRANT.BASIC <>0 ORDER BY HR_SALGRANT.EMPID", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@month", month);

            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string EMPNM = DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + EMPNM;

                string NMDAYS = DataBinder.Eval(e.Row.DataItem, "NMDAYS").ToString();
                e.Row.Cells[1].Text = NMDAYS;

                string OTDAYS = DataBinder.Eval(e.Row.DataItem, "OTDAYS").ToString();
                e.Row.Cells[2].Text = OTDAYS;

                decimal RATEPD = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATEPD").ToString());
                string rateD = RATEPD.ToString("#,##0.00");
                e.Row.Cells[3].Text = rateD + "&nbsp;";

                decimal RATEPH = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RATEPH").ToString());
                string rateH = RATEPH.ToString("#,##0.00");
                e.Row.Cells[4].Text = rateH + "&nbsp;";

                string OTHOUR = DataBinder.Eval(e.Row.DataItem, "TOTHOUR").ToString();
                e.Row.Cells[5].Text = OTHOUR;

                decimal OTAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTAMT").ToString());
                string otAmt = OTAMT.ToString("#,##0.00");
                e.Row.Cells[6].Text = otAmt + "&nbsp;";

                decimal BASIC = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BASIC").ToString());
                string bas = BASIC.ToString("#,##0.00");
                e.Row.Cells[7].Text = bas + "&nbsp;";

                decimal FOOD = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FOOD").ToString());
                string foo = FOOD.ToString("#,##0.00");
                e.Row.Cells[8].Text = foo + "&nbsp;";

                decimal OTCADD = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OTCADD").ToString());
                string otAdd = OTCADD.ToString("#,##0.00");
                e.Row.Cells[9].Text = otAdd + "&nbsp;";

                decimal GROSSAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GROSSAMT").ToString());
                string gross = GROSSAMT.ToString("#,##0.00");
                e.Row.Cells[10].Text = gross + "&nbsp;";

                decimal ADVANCE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ADVANCE").ToString());
                string adv = ADVANCE.ToString("#,##0.00");
                e.Row.Cells[11].Text = adv + "&nbsp;";

                decimal PENALTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PENALTY").ToString());
                string pen = PENALTY.ToString("#,##0.00");
                e.Row.Cells[12].Text = pen + "&nbsp;";

                decimal OTCDED = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OTCDED").ToString());
                string otDed = OTCDED.ToString("#,##0.00");
                e.Row.Cells[13].Text = otDed + "&nbsp;";

                decimal NETAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NETAMT").ToString());
                string net = NETAMT.ToString("#,##0.00");
                e.Row.Cells[14].Text = net + "&nbsp;";

                netAmt += NETAMT;
                netAmtComma = netAmt.ToString("#,##0.00");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[13].Text = "Total : " + "&nbsp;";
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].Text = netAmtComma + "&nbsp;";
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;
            }

            ShowHeader(GridView1);
        }

        private void ShowHeader(GridView gvReport)
        {
            if (gvReport.Rows.Count > 0)
            {
                gvReport.UseAccessibleHeader = true;
                gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gridView.HeaderRow.Style["display"] = "table-header-group";
            }
        }
    }
}