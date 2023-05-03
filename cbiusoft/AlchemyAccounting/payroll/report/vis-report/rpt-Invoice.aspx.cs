using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AlchemyAccounting.payroll.report.vis_report
{
    public partial class rpt_Invoice : System.Web.UI.Page
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
                string compnm = Session["companyname"].ToString();
                string partyID = Session["partyID"].ToString();
                string partyNM = Session["partyNM"].ToString();
                string billdt = Session["billdt"].ToString();
                string year = Session["YY"].ToString();
                string billno = Session["billno"].ToString();
                string site = Session["site"].ToString();
                string billtp = Session["billtp"].ToString();
                string name= Session["name"].ToString();
                string contact = Session["contact"].ToString();


                lblCompNM.Text = compnm;
                lblPartyName.Text = partyNM;
                lblDate.Text = billdt;
                lblDocNo.Text = billno;
                lblSite.Text = site;

                lblcompany.Text = compnm;
                lblName.Text = name;
                lblph.Text = contact;
                lblmail.Text = "pancacitraqatar@yahoo.com";

                DateTime td = DateTime.Now;
                lblPrintDate.Text = td.ToString("dd/MM/yyyy");

                string mon = td.ToString("MMM").ToUpper();
                lblmonth.Text= mon + "" + year;
                ShowGrid();

            }
        }

        private void ShowGrid()
        {
            string compnm = Session["companyname"].ToString();
            string partyID = Session["partyID"].ToString();
            string billdt = Session["billdt"].ToString();
            string year = Session["YY"].ToString();
            string billno = Session["billno"].ToString();
            string site = Session["site"].ToString();
            string billtp = Session["billtp"].ToString();


            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT BILLSL, BILLNM, TWORKER, RATEPTP, TOTQPTP, AMTPTP FROM HR_BILL  WHERE BILLNO=" + billno + " AND BILLYY=" + year + " AND BILLTP='" + billtp + "' ", conn);

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
            string billtp = Session["billtp"].ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string BILLSL = DataBinder.Eval(e.Row.DataItem, "BILLSL").ToString();
                e.Row.Cells[0].Text = BILLSL;

                string BILLNM = DataBinder.Eval(e.Row.DataItem, "BILLNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + BILLNM;

                if (billtp == "Meter")
                {

                    string TWORKER = DataBinder.Eval(e.Row.DataItem, "TWORKER").ToString();
                    e.Row.Cells[2].Text = TWORKER;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[2].Enabled = false;
                    gvReport.HeaderRow.Cells[2].Visible = false;

                    string RATEPTP = DataBinder.Eval(e.Row.DataItem, "RATEPTP").ToString();
                    e.Row.Cells[3].Text = RATEPTP;
                    gvReport.HeaderRow.Cells[3].Text = "Per Meter Rate";

                    string TOTQPTP = DataBinder.Eval(e.Row.DataItem, "TOTQPTP").ToString();
                    e.Row.Cells[4].Text = TOTQPTP;
                    gvReport.HeaderRow.Cells[4].Text = "Total Meter";

                }
                else if (billtp == "Hour")
                {
                    string TWORKER = DataBinder.Eval(e.Row.DataItem, "TWORKER").ToString();
                    e.Row.Cells[2].Text = TWORKER;

                    string RATEPTP = DataBinder.Eval(e.Row.DataItem, "RATEPTP").ToString();
                    e.Row.Cells[3].Text = RATEPTP;
                    gvReport.HeaderRow.Cells[3].Text = "Per Hour Rate";

                    string TOTQPTP = DataBinder.Eval(e.Row.DataItem, "TOTQPTP").ToString();
                    e.Row.Cells[4].Text = TOTQPTP;
                    gvReport.HeaderRow.Cells[4].Text = "Total Hour";
                }



                decimal AMTPTP = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMTPTP").ToString());
                string Amnt = AMTPTP.ToString("#,##0.00");
                e.Row.Cells[5].Text = Amnt + "&nbsp;";

                totAmount += AMTPTP;
                ttAmt = totAmount.ToString();
                totAmountComma = totAmount.ToString("#,##0.00");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (billtp == "Meter")
                {
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[2].Enabled = false;

                    e.Row.Cells[4].Text = "Total : ";
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].Text = totAmountComma;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;
                }
                else if (billtp == "Hour")
                {
                    e.Row.Cells[4].Text = "Total : ";
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].Text = totAmountComma;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;
                }

                

                lblInWords.Text = "";
                decimal dec;
                Boolean ValidInput = Decimal.TryParse(ttAmt, out dec);
                if (!ValidInput)
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Enter the Proper Amount...";
                    return;
                }
                if (ttAmt.ToString().Trim() == "")
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Amount Cannot Be Empty...";
                    return;
                }
                else
                {
                    if (Convert.ToDecimal(ttAmt) == 0)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                }

                string x1 = "";
                string x2 = "";

                if (ttAmt.Contains("."))
                {
                    x1 = ttAmt.ToString().Trim().Substring(0, ttAmt.ToString().Trim().IndexOf("."));
                    x2 = ttAmt.ToString().Trim().Substring(ttAmt.ToString().Trim().IndexOf(".") + 1);
                }
                else
                {
                    x1 = ttAmt.ToString().Trim();
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

                ttAmt = x1 + "." + x2;

                if (x2.Length > 2)
                {
                    ttAmt = Math.Round(Convert.ToDouble(ttAmt), 2).ToString().Trim();
                }

                string AmtConv = SpellAmount.MoneyConvFn(ttAmt.ToString().Trim());

                lblInWords.Text = AmtConv.Trim();

            }
            ShowHeader(gvReport);
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