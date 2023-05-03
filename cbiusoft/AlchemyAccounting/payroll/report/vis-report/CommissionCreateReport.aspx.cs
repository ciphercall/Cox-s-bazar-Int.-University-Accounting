using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.payroll.report.vis_report
{
    public partial class CommissionCreateReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    DateTime t = DateTime.Now;
                    lblPrintDate.Text = t.ToString("dd/MM/yyy hh:mm:ss:tt");


                    string date = Session["date"].ToString();

                    string partyID = Session["partyID"].ToString();
                    string partyNM = Session["partyNM"].ToString();
                    string siteID = Session["siteID"].ToString();
                    string siteNM = Session["siteNM"].ToString();
                    string billAmt = Session["billAmt"].ToString();
                    string percentage = Session["percentage"].ToString();
                    string commission = Session["commission"].ToString();
                    string carRent = Session["carRent"].ToString();
                    string advanceAmount = Session["advanceAmount"].ToString();
                    string total = Session["total"].ToString();
                    string advanceAmtComp = Session["advanceAmtComp"].ToString();
                    string nettotal = Session["nettotal"].ToString();
                    string remarks= Session["remarks"].ToString();

                    lblCompanyNM.Text = "Commission Report";
                    lblCompanyNM0.Text = "Helmi Trading & Contracting W.L.L";
                    
                    lblWorkSite.Text = siteNM;
                    lblSL.Text = "1";
                    lblIssueDate.Text = date;
                    lblinvoice.Text = billAmt;
                    lblpercent.Text = percentage;
                    lblComAmt.Text = commission;
                    lblcarrent.Text = carRent;
                    lblAdvance.Text = advanceAmount;
                    lblAmount.Text = total;
                    lblCompanyAdvance.Text = advanceAmtComp;
                    lblGrandTotal.Text = nettotal;
                    lblRemarks.Text = remarks;

                    Global.lblAdd("select REMARKS from GL_COSTP where COSTPID ='"+siteID+"'",lblParticulars);
                    Global.lblAdd("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD='" + partyID + "'", lblGroup);
                    decimal dec;
                    Boolean ValidInput = Decimal.TryParse(nettotal, out dec);
                    if (!ValidInput)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Enter the Proper Amount...";
                        return;
                    }
                    if (nettotal.ToString().Trim() == "")
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(nettotal) == 0)
                        {
                            lblInWords.ForeColor = System.Drawing.Color.Red;
                            lblInWords.Text = "Amount Cannot Be Empty...";
                            return;
                        }
                    }

                    string x1 = "";
                    string x2 = "";

                    if (nettotal.Contains("."))
                    {
                        x1 = nettotal.ToString().Trim().Substring(0, nettotal.ToString().Trim().IndexOf("."));
                        x2 = nettotal.ToString().Trim().Substring(nettotal.ToString().Trim().IndexOf(".") + 1);
                    }
                    else
                    {
                        x1 = nettotal.ToString().Trim();
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

                    nettotal = x1 + "." + x2;

                    if (x2.Length > 2)
                    {
                        nettotal = Math.Round(Convert.ToDouble(nettotal), 2).ToString().Trim();
                    }

                    string AmtConv = SpellAmount.MoneyConvFn(nettotal.ToString().Trim());
                    lblInWords.Text = AmtConv.Trim();

                }
            }
        }



    }
}