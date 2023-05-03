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
    public partial class rpt_commission : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal totAmount = 0;

        string totAmountComma = "0";
        string ttAmt = "0";

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
                lblPrintDate.Text = td;

                string sitenm = Session["sitenm"].ToString();
                string siteid = Session["siteid"].ToString();

                lblSiteNM.Text = sitenm;

                Global.lblAdd("SELECT CONVERT(NVARCHAR(20), TRANSDT, 103) AS TRANSDT FROM GL_MASTER WHERE COSTPID ='" + siteid + "' AND CREDITCD ='301010100001' AND TRANSTP ='JOUR'", lblBillDate);
                DateTime bldt =new DateTime();
                if (lblBillDate.Text == "")
                    lblBillDate.Text = "";
                else
                    bldt = DateTime.Parse(lblBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblBillDate.Text = bldt.ToString("dd-MMM-yyyy");

                showGrid();

                Global.lblAdd("SELECT DEBITAMT FROM GL_MASTER WHERE COSTPID ='" + siteid + "' AND CREDITCD ='301010100001' AND TRANSTP ='JOUR'", lblBillAmount);
                decimal billamt = 0;
                if (lblBillAmount.Text == "")
                    lblBillAmount.Text = "0";
                else
                    billamt = Convert.ToDecimal(lblBillAmount.Text);
                lblBillAmount.Text = billamt.ToString("#,##0.00");
                Global.lblAdd("SELECT REMARKS FROM GL_COSTP WHERE COSTPID ='" + siteid + "'", lblSiteDes);
                Global.lblAdd("SELECT CPCNT FROM GL_COSTP WHERE COSTPID ='" + siteid + "'", lblCommission);
                if (lblCommission.Text == "")
                    lblCommission.Text = "0";
                decimal comPer = Convert.ToDecimal(lblCommission.Text);
                decimal comAmt = billamt * (comPer / 100);
                lblCommissionAmt.Text = comAmt.ToString("F");
                decimal amt = Convert.ToDecimal(lblCommissionAmt.Text) + Convert.ToDecimal(totAmountComma);
                string varamt = amt.ToString("F");
                decimal convAmt = Convert.ToDecimal(varamt);
                lblAmt.Text = convAmt.ToString("#,##0.00");
                decimal grtot = Convert.ToDecimal(lblBillAmount.Text) - Convert.ToDecimal(lblAmt.Text);
                string vartot = grtot.ToString("F");
                decimal convTot = Convert.ToDecimal(vartot);
                lblGrandTotal.Text = convTot.ToString("#,##0.00");

                lblInWords.Text = "";
                decimal dec;
                Boolean ValidInput = Decimal.TryParse(vartot, out dec);
                if (!ValidInput)
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Enter the Proper Amount...";
                    return;
                }
                if (vartot.ToString().Trim() == "")
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Amount Cannot Be Empty...";
                    return;
                }
                else
                {
                    if (Convert.ToDecimal(vartot) == 0)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                }

                string x1 = "";
                string x2 = "";

                if (vartot.Contains("."))
                {
                    x1 = vartot.ToString().Trim().Substring(0, vartot.ToString().Trim().IndexOf("."));
                    x2 = vartot.ToString().Trim().Substring(vartot.ToString().Trim().IndexOf(".") + 1);
                }
                else
                {
                    x1 = vartot.ToString().Trim();
                    x2 = "00";
                }

                if (x1.ToString().Trim() != "")
                {
                    x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
                }
                else
                {
                    x1 = "0";
                }

                vartot = x1 + "." + x2;

                if (x2.Length > 2)
                {
                    vartot = Math.Round(Convert.ToDouble(vartot), 2).ToString().Trim();
                }

                string AmtConv = SpellAmount.MoneyConvFn(vartot.ToString().Trim());

                lblInWords.Text = AmtConv.Trim();
            }
        }

        public void showGrid()
        {
            SqlConnection conn = new SqlConnection(Global.connection);

            string sitenm = Session["sitenm"].ToString();
            string siteid = Session["siteid"].ToString();

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT DEBITCD, AMOUNT, ACCOUNTNM, BIG FROM  (SELECT     TOP (100) PERCENT GL_MASTER.DEBITCD, SUM(GL_MASTER.DEBITAMT) AS AMOUNT, GL_ACCHART.ACCOUNTNM, ':' AS BIG " +
                       " FROM          GL_MASTER INNER JOIN GL_ACCHART ON GL_MASTER.DEBITCD = GL_ACCHART.ACCOUNTCD WHERE (GL_MASTER.COSTPID = '" + siteid + "') AND (GL_MASTER.TRANSTP = 'MPAY') AND (GL_MASTER.TRANSDRCR = 'DEBIT') " +
                       " GROUP BY GL_MASTER.DEBITCD, GL_ACCHART.ACCOUNTNM, GL_ACCHART.ACCOUNTCD ORDER BY GL_ACCHART.ACCOUNTCD, GL_ACCHART.ACCOUNTNM " +
                       " UNION " +
                       " SELECT TOP (100) PERCENT GL_MASTER.DEBITCD, SUM(GL_MASTER.CREDITAMT) AS AMOUNT, GL_ACCHART.ACCOUNTNM, ':' AS BIG FROM GL_MASTER INNER JOIN GL_ACCHART ON GL_MASTER.DEBITCD = GL_ACCHART.ACCOUNTCD " +
                       " WHERE (GL_MASTER.COSTPID = '" + siteid + "') AND (GL_MASTER.TRANSTP = 'MREC') AND (GL_MASTER.TRANSDRCR = 'CREDIT') AND  (SUBSTRING(GL_MASTER.DEBITCD, 1, 7) = '3030201') " +
                       " GROUP BY GL_MASTER.DEBITCD, GL_ACCHART.ACCOUNTNM, GL_ACCHART.ACCOUNTCD ORDER BY ACCOUNTCD, ACCOUNTNM) AS A ", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvReport.DataSource = ds;
                gvReport.DataBind();
                gvReport.Visible = true;
            }
            else
            {
                gvReport.Visible = true;
            }
        }

        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ACCOUNTNM = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + ACCOUNTNM;

                string BIG = DataBinder.Eval(e.Row.DataItem, "BIG").ToString();
                e.Row.Cells[2].Text = BIG;

                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string Amnt = AMOUNT.ToString("#,##0.00");
                e.Row.Cells[3].Text = "&nbsp;" + Amnt;

                totAmount += AMOUNT;
                ttAmt = totAmount.ToString();
                totAmountComma = totAmount.ToString("#,##0.00");
                lblTotAmount.Text = totAmountComma;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = totAmountComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            }
            ShowHeader(gvReport);
        }

        private void ShowHeader(GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.UseAccessibleHeader = true;
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gridView.HeaderRow.Style["display"] = "table-header-group";
            }
        }
    }
}