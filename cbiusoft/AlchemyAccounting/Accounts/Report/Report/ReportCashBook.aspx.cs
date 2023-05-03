﻿using System;
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

namespace AlchemyAccounting.Accounts.Report.Report
{
    public partial class ReportCashBook : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

                DateTime PrintDate = DateTime.Today.Date;
                string td = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                lblTime.Text = td;

                string debitCD = Session["AccCode"].ToString();
                string accNM = Session["AccNM"].ToString();
                string frmDT = Session["From"].ToString();
                string toDT = Session["To"].ToString();
                lblHeadNM.Text = accNM;
                lblFrom.Text = frmDT;
                lblTo.Text = toDT;
                lbltimepm.Text = DateTime.Now.ToString("hh:mm tt");

                DateTime From = DateTime.Parse(frmDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                DateTime To = DateTime.Parse(toDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string FdT = From.ToString("yyyy/MM/dd");
                string TdT = To.ToString("yyyy/MM/dd");

                Global.lblAdd(@"SELECT SUM(DEBITAMT) - SUM(CREDITAMT) as OpeningBalance FROM dbo.GL_MASTER WHERE (TRANSDT < '" + FdT + "') AND (DEBITCD = '" + debitCD + "')", lblOpenBal);

                if (lblOpenBal.Text == "")
                {
                    lblOpenBal.Text = "0.00";
                    lblOpenBalComma.Text = "0.00";
                }
                else
                {
                    string OpenBal = SpellAmount.comma(Convert.ToDecimal(lblOpenBal.Text));
                    lblOpenBalComma.Text = OpenBal;
                }
                //DataTable table = new DataTable();
                //AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
                //table = dob.LedgerBook(debitCD, From, To, accNM);
                showGrid();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void showGrid()
        {
            
            SqlConnection conn = new SqlConnection(Global.connection);

            string debitCD = Session["AccCode"].ToString();
            string accNM = Session["AccNM"].ToString();
            string frmDT = Session["From"].ToString();
            string toDT = Session["To"].ToString();

            DateTime From = DateTime.Parse(frmDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime To = DateTime.Parse(toDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FdT = From.ToString("yyyy/MM/dd");
            string TdT = To.ToString("yyyy/MM/dd");

            if (conn.State != ConnectionState.Open)conn.Open();



            SqlCommand cmd = new SqlCommand(@" SELECT *,CASE WHEN ISNULL(STUDENTID,'')<>'' THEN (SELECT NEWSTUDENTID+SUBSTRING(B.REMARKS,12,LEN(B.REMARKS)) FROM EIM_STUDENT WHERE STUDENTID=B.STUDENTID) ELSE B.REMARKS END REMARKS1 FROM (
SELECT *,CASE WHEN A.TABLEID='EIM_TRANS' THEN SUBSTRING(A.REMARKS,0,12) ELSE '' END STUDENTID FROM (
SELECT  '" + FdT + "' AS FromDate, '" + TdT + "' AS ToDate,  " +
                                            " GL_MASTER_1.TRANSTP, CONVERT(nvarchar(10), GL_MASTER_1.TRANSDT, 103) AS TRANSDT, GL_MASTER_1.TRANSDT AS TR, GL_MASTER_1.TRANSMY, " +
                                            " CONVERT(nvarchar(10), GL_MASTER_1.TRANSNO, 103) AS TRANSNO, GL_MASTER_1.SERIALNO, GL_MASTER_1.TRANSDRCR, " +
                                            " GL_MASTER_1.TRANSFOR, GL_MASTER_1.COSTPID, GL_MASTER_1.TRANSMODE, GL_MASTER_1.DEBITCD, GL_MASTER_1.CREDITCD, " +
                                            " CONVERT(varchar, CONVERT(money, GL_MASTER_1.DEBITAMT), 1) AS DEBITAMT, CONVERT(varchar, CONVERT(money, " +
                                            " GL_MASTER_1.CREDITAMT), 1) AS CREDITAMT, GL_MASTER_1.CHEQUENO, CONVERT(nvarchar(20), GL_MASTER_1.CHEQUEDT, 105) AS CHEQUEDT, " +
                                            " GL_MASTER_1.REMARKS, GL_MASTER_1.TABLEID, GL_MASTER_1.USERPC, GL_MASTER_1.USERID, GL_MASTER_1.ACTDTI, GL_MASTER_1.INTIME, " +
                                            " GL_MASTER_1.IPADDRESS, GL_ACCHART_1.ACCOUNTNM AS DebitNM, " +
                                            " CASE WHEN GL_MASTER_1.DEBITAMT > 0 THEN 'TO ' + dbo.GL_ACCHART.ACCOUNTNM + ((CASE WHEN GL_MASTER_1.CHEQUENO != '' THEN ' Chq #' + GL_MASTER_1.CHEQUENO + ' ' + CONVERT(nvarchar(20), GL_MASTER_1.CHEQUEDT, 105) WHEN GL_MASTER_1.CHEQUENO = '' THEN '' ELSE '' END)) WHEN GL_MASTER_1.CREDITAMT > 0 THEN 'BY ' + dbo.GL_ACCHART.ACCOUNTNM + ((CASE WHEN GL_MASTER_1.CHEQUENO != '' THEN ' Chq #' + GL_MASTER_1.CHEQUENO + ' ' + CONVERT(nvarchar(20), GL_MASTER_1.CHEQUEDT, 105) WHEN GL_MASTER_1.CHEQUENO = '' THEN '' ELSE '' END)) END + ' <br/> &nbsp;' + GL_MASTER_1.REMARKS AS CreditNM, " +
                                            " (CASE WHEN GL_MASTER_1.TABLEID = 'GL_STRANS' THEN SUBSTRING(GL_MASTER_1.TABLEID, 4, 1) + (CASE WHEN TRANSTP = 'MREC' THEN 'RV ' WHEN TRANSTP = 'MPAY' THEN 'PV ' WHEN TRANSTP = 'JOUR' THEN 'JV ' WHEN TRANSTP = 'CONT' THEN 'CV ' ELSE 'APV ' END) WHEN GL_MASTER_1.TABLEID = 'STK_TRANS' THEN 'AJV ' WHEN GL_MASTER_1.TABLEID = 'LC_EXPENSE' THEN 'APV ' WHEN GL_MASTER_1.TABLEID = 'EIM_TRANS' THEN 'ARV ' ELSE '' END) + CONVERT(nvarchar(10), GL_MASTER_1.TRANSNO, 103) AS DOCNO " +
                                            " FROM dbo.GL_ACCHART INNER JOIN dbo.GL_MASTER AS GL_MASTER_1 INNER JOIN dbo.GL_ACCHART AS GL_ACCHART_1 ON GL_MASTER_1.DEBITCD = GL_ACCHART_1.ACCOUNTCD ON  " +
                                            " dbo.GL_ACCHART.ACCOUNTCD = GL_MASTER_1.CREDITCD WHERE (GL_MASTER_1.TRANSDT BETWEEN '" + FdT + "' AND '" + TdT + "') AND (GL_MASTER_1.DEBITCD = '" + debitCD + "') AND GL_MASTER_1.DEBITAMT <> GL_MASTER_1.CREDITAMT " +
                                            " "+
            	") A )B ORDER BY TR, TRANSMY DESC, TRANSDT,TRANSTP DESC, TRANSNO,SERIALNO, DEBITAMT DESC", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;

                Balance();

                Decimal totDebitAmnt = 0;
                Decimal a = 0;
                foreach (GridViewRow grid in GridView1.Rows)
                {
                    string Debit = grid.Cells[3].Text;
                    totDebitAmnt = Convert.ToDecimal(Debit);
                    a += totDebitAmnt;
                    decimal tot = a;
                    lblPeriodicDB.Text = SpellAmount.comma(tot);
                }
                //a += totDebitAmnt;

                Decimal totCreditAmnt = 0;
                Decimal b = 0;
                foreach (GridViewRow grid in GridView1.Rows)
                {
                    string Credit = grid.Cells[4].Text;
                    totCreditAmnt = Convert.ToDecimal(Credit);
                    b += totCreditAmnt;
                    decimal tot = b;
                    lblPeriodicCR.Text = SpellAmount.comma(tot);
                }
                //b += totCreditAmnt;

                decimal totPeriodicDB = decimal.Parse(lblPeriodicDB.Text);
                decimal totPeriodicCR = decimal.Parse(lblPeriodicCR.Text);
                decimal totPeriodic = totPeriodicDB - totPeriodicCR;
                lblPeriodicBalance.Text = SpellAmount.comma(totPeriodic);



                lblTotCR.Text = lblPeriodicCR.Text;
                decimal TotCR = decimal.Parse(lblTotCR.Text);
                decimal LastCumBalance = decimal.Parse(lblLastCumBalC.Text);
                decimal TotBalance = TotCR + LastCumBalance;
                lblTotBalance.Text = SpellAmount.comma(TotBalance);

            }
            else
            {
                lblLastCumBalC.Text = lblOpenBalComma.Text;
                lblPeriodicBalance.Text = "0.00";
                lblPeriodicCR.Text = "0.00";
                lblPeriodicDB.Text = "0.00";
                lblTotCR.Text = "0.00";
                lblTotBalance.Text = lblLastCumBalC.Text;
            }
        }

        public void Balance()
        {
            try
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        string Debit = GridView1.Rows[i].Cells[3].Text;
                        decimal DbAmt = decimal.Parse(Debit);
                        string Credit = GridView1.Rows[i].Cells[4].Text;
                        decimal CdAmt = decimal.Parse(Credit);
                        decimal OpenBal = Convert.ToDecimal(lblOpenBal.Text);
                        decimal Balance = OpenBal + DbAmt - CdAmt;
                        string BalanceComma = SpellAmount.comma(Balance);
                        GridView1.Rows[i].Cells[5].Text = BalanceComma;
                        lblLastCumBalance.Text = Balance.ToString();
                        lblLastCumBalC.Text = GridView1.Rows[i].Cells[5].Text;
                    }
                    else
                    {

                        //string nastyNumber = "1,234";
                        //int result = int.Parse(nastyNumber.Replace(",", ""));

                        string BlnC = GridView1.Rows[i - 1].Cells[5].Text;
                        decimal PrevBal = decimal.Parse(BlnC);
                        decimal CumulativeBalance = PrevBal;

                        string Debit = GridView1.Rows[i].Cells[3].Text;
                        decimal DbAmt = Convert.ToDecimal(Debit);
                        string Credit = GridView1.Rows[i].Cells[4].Text;
                        decimal CdAmt = Convert.ToDecimal(Credit);
                        decimal Balance = CumulativeBalance + DbAmt - CdAmt;
                        //GridView1.Rows[i].Cells[5].Text = Balance.ToString();
                        string BalanceComma = SpellAmount.comma(Balance);
                        GridView1.Rows[i].Cells[5].Text = BalanceComma;
                        lblLastCumBalance.Text = Balance.ToString();
                        lblLastCumBalC.Text = GridView1.Rows[i].Cells[5].Text;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    string TRANSDT = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                    e.Row.Cells[0].Text = TRANSDT;

                    string DOCNO = DataBinder.Eval(e.Row.DataItem, "DOCNO").ToString();
                    e.Row.Cells[1].Text = DOCNO;

                    string CreditNM = DataBinder.Eval(e.Row.DataItem, "CreditNM").ToString();
                    e.Row.Cells[2].Text = "&nbsp;" + CreditNM;

                    decimal DEBITAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DEBITAMT").ToString());
                    string DRAMT = SpellAmount.comma(DEBITAMT);
                    e.Row.Cells[3].Text = DRAMT;

                    decimal CREDITAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CREDITAMT").ToString());
                    string CRAMT = SpellAmount.comma(CREDITAMT);
                    e.Row.Cells[4].Text = CRAMT;
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

            MakeGridViewPrinterFriendly(GridView1);
        }

        private void MakeGridViewPrinterFriendly(GridView grid)
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