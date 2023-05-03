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
    public partial class TransReportPrint : System.Web.UI.Page
    {
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        decimal totAmount = 0;
        decimal totVat = 0;
        decimal totVatAmount = 0;

        string totAmountComma = "0";
        string totVatComma = "0";
        string totVatAmountComma = "0";


        decimal dblGrandTotalAmount = 0;
        decimal dblGrandtotVat = 0;
        decimal dblGrandtotVatAmount = 0;


        string grandtotalamt = "0";
        string dblGrandTotalAmountComma = "0";

        string grandtotalVat = "0";
        string dblGrandTotalVatComma = "0";

        string grandtotalVatAmount = "0";
        string dblGrandTotalVatAmountComma = "0";

        SqlConnection conn = new SqlConnection(Global.connection);
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                showrepeat1();
                string FROMDT = Session["FORMDT"].ToString();
                string TODT = Session["TODT"].ToString();
                DateTime FRDT = DateTime.Parse(FROMDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string Date1 = FRDT.ToString("yyyy/MM/dd");
                DateTime ToDT = DateTime.Parse(TODT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string Date2 = ToDT.ToString("yyyy/MM/dd");

                //if (conn.State != ConnectionState.Open)conn.Open();
                //SqlCommand cmd = new SqlCommand(@" SELECT  MAX(AMOUNT) FROM EIM_TRANS WHERE TRANSDT BETWEEN '" + Date1 + "' AND '" + Date2 + "'", conn);
                //cmd.Parameters.Clear();
                // Global.lblAdd("SELECT SUM(AMOUNT) FROM EIM_TRANS WHERE TRANSDT BETWEEN '" + Date1 + "' AND '" + Date2 + "'", lblTtlAmnt);
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }
        protected void gv_Trans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "h2").ToString();


                string FEESNM = DataBinder.Eval(e.Row.DataItem, "FEESNM").ToString();
                e.Row.Cells[0].Text = @"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;***
                             " + FEESNM;
                decimal AMOUNT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString());
                string AMNT = SpellAmount.comma(AMOUNT);
                e.Row.Cells[1].Text = "&nbsp;" + AMNT + "&nbsp;&nbsp;&nbsp;&nbsp; ";

                totAmount += AMOUNT;
                totAmountComma = SpellAmount.comma(totAmount);

                dblGrandTotalAmount += AMOUNT;
                grandtotalamt = dblGrandTotalAmount.ToString();
                lblTtlAmnt.Text = SpellAmount.comma(dblGrandTotalAmount);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Sub Total : ";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = totAmountComma + "&nbsp;&nbsp;&nbsp;&nbsp; ";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;
                totAmount = 0;
            }
        }
        protected void gv_Trans_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "h2") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "h2").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "h2") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "h2") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = " " + DataBinder.Eval(e.Row.DataItem, "h2").ToString();
                cell.ColumnSpan = 2;
                cell.Font.Bold = true;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Sub Total Row
                GridView GridView1 = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                //Adding Total Cell          
                TableCell cell = new TableCell();

                //Adding the Row at the RowIndex position in the Grid      
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "h2") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();

                    cell.Text = DataBinder.Eval(e.Row.DataItem, "h2").ToString();
                    cell.ColumnSpan = 2;
                    cell.Font.Bold = true;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion

            }


            if (IsGrandTotalRowNeedtoAdd)
            {

            }
        }
        public void showrepeat1()
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            string FROMDT = Session["FORMDT"].ToString();
            string TODT = Session["TODT"].ToString();
            DateTime FRDT = DateTime.Parse(FROMDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date1 = FRDT.ToString("yyyy/MM/dd");
            DateTime ToDT = DateTime.Parse(TODT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date2 = ToDT.ToString("yyyy/MM/dd");
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand();
            if (Session["CNBCD"].ToString() != "102010100001")
            {
                cmd = new SqlCommand(@" SELECT  DISTINCT TRANSDT AS DT , convert(nvarchar(10),TRANSDT,103) AS TRANSDT
                        FROM EIM_TRANS WHERE TRANSTP='MREC' AND CNBCD!='102010100001' and TRANSDT BETWEEN @d1 AND @d2 ORDER BY DT", conn);
            }
            else
            {
                cmd = new SqlCommand(@" SELECT  DISTINCT TRANSDT AS DT , convert(nvarchar(10),TRANSDT,103) AS TRANSDT
                        FROM EIM_TRANS WHERE TRANSTP='MREC' AND CNBCD='102010100001' and TRANSDT BETWEEN @d1 AND @d2 ORDER BY DT", conn);
            }
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("d1", Date1);
            cmd.Parameters.AddWithValue("d2", Date2);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater1.Visible = true;
            }
            else
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater1.Visible = true;
            }
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Label lblText = (Label)e.Item.FindControl("lblText");
            Label lbldate = (Label)e.Item.FindControl("lbldate");
            Label lblAmount = (Label)e.Item.FindControl("lblAmount");
            DateTime DT = DateTime.Parse(lbldate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date = DT.ToString("yyyy/MM/dd");
            Repeater Repeater2 = (Repeater)(e.Item.FindControl("Repeater2"));
            DataTable DT1 = LoadData1(Date);
            if (DT1.Rows.Count > 0)
            {
                Repeater2.DataSource = DT1;
                Repeater2.DataBind();
            }
            else
            {
                //lblText.Visible = false;
                lbldate.Visible = false;
                Repeater2.Visible = false;
            }

        }
        private DataTable LoadData1(string DT1)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            DataTable dtGrid = new DataTable();
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand();
            if (Session["CNBCD"].ToString() != "102010100001")
            {
                cmd = new SqlCommand(@"SELECT     DISTINCT ('&nbsp;&nbsp;&nbsp;Mr No :'+CONVERT(NVARCHAR(20),EIM_TRANS.TRANSNO,103) +'&nbsp;&nbsp;|&nbsp;&nbsp;Student ID :' + EIM_STUDENT.NEWSTUDENTID+ '&nbsp;&nbsp;|&nbsp;&nbsp; Student :' + EIM_STUDENT.STUDENTNM+'&nbsp;&nbsp;|&nbsp;&nbsp;'+EIM_PROGRAM.PROGRAMSID+'&nbsp;&nbsp;|&nbsp;&nbsp;'+EIM_SEMESTER.SEMESTERNM+'-'+  SUBSTRING(Convert(nvarchar(10),EIM_TRANS.TRANSYY,103),3,2))  as h2,EIM_TRANS.TRANSNO,convert(nvarchar(10),EIM_TRANS.TRANSDT,103) AS TRANSDT 
                      FROM EIM_TRANS INNER JOIN
                      EIM_STUDENT ON EIM_TRANS.STUDENTID = EIM_STUDENT.STUDENTID INNER JOIN
                      EIM_PROGRAM ON EIM_TRANS.PROGRAMID = EIM_PROGRAM.PROGRAMID INNER JOIN
                      EIM_SEMESTER ON EIM_TRANS.SEMESTERID = EIM_SEMESTER.SEMESTERID  WHERE EIM_TRANS.TRANSTP='MREC' AND CNBCD!='102010100001' and   EIM_TRANS.TRANSDT='" + DT1 + "' ORDER BY TRANSNO", conn);
            }
            else
            {
                cmd = new SqlCommand(@"SELECT     DISTINCT ('&nbsp;&nbsp;&nbsp;Mr No :'+CONVERT(NVARCHAR(20),EIM_TRANS.TRANSNO,103) +'&nbsp;&nbsp;|&nbsp;&nbsp;Student ID :' + EIM_STUDENT.NEWSTUDENTID+ '&nbsp;&nbsp;|&nbsp;&nbsp; Student :' + EIM_STUDENT.STUDENTNM+'&nbsp;&nbsp;|&nbsp;&nbsp;'+EIM_PROGRAM.PROGRAMSID+'&nbsp;&nbsp;|&nbsp;&nbsp;'+EIM_SEMESTER.SEMESTERNM+'-'+  SUBSTRING(Convert(nvarchar(10),EIM_TRANS.TRANSYY,103),3,2))  as h2,EIM_TRANS.TRANSNO,convert(nvarchar(10),EIM_TRANS.TRANSDT,103) AS TRANSDT 
                      FROM EIM_TRANS INNER JOIN
                      EIM_STUDENT ON EIM_TRANS.STUDENTID = EIM_STUDENT.STUDENTID INNER JOIN
                      EIM_PROGRAM ON EIM_TRANS.PROGRAMID = EIM_PROGRAM.PROGRAMID INNER JOIN
                      EIM_SEMESTER ON EIM_TRANS.SEMESTERID = EIM_SEMESTER.SEMESTERID  WHERE EIM_TRANS.TRANSTP='MREC' AND CNBCD='102010100001' and   EIM_TRANS.TRANSDT='" + DT1 + "' ORDER BY TRANSNO", conn);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtGrid);
            if (conn.State != ConnectionState.Closed)conn.Close();
            return dtGrid;
        }
        //        public void showrepeat2()
        //        {

        //            SqlConnection conn = new SqlConnection(Global.connection);
        //            string FROMDT = Session["FORMDT"].ToString();
        //            string TODT = Session["TODT"].ToString();
        //            DateTime FRDT = DateTime.Parse(FROMDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //            string Date1 = FRDT.ToString("yyyy/MM/dd");
        //            DateTime ToDT = DateTime.Parse(TODT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //            string Date2 = ToDT.ToString("yyyy/MM/dd");
        //            if (conn.State != ConnectionState.Open)conn.Open();
        //            SqlCommand cmd = new SqlCommand(@" SELECT 'Mr No :'+CONVERT(NVARCHAR(20),EIM_TRANS.TRANSNO,103) + '&nbsp;&nbsp;|&nbsp;&nbsp;Student ID :' + EIM_STUDENT.STUDENTID + '&nbsp;&nbsp;|&nbsp;&nbsp; Student :' + EIM_STUDENT.STUDENTNM + '&nbsp;&nbsp;|&nbsp;&nbsp;'+ EIM_SEMESTER.SEMESTERNM+'-'+ SUBSTRING(Convert(nvarchar(10),EIM_TRANS.TRANSYY,103),3,2)  as h2 FROM  EIM_STUDENT ON EIM_TRANS.STUDENTID = EIM_STUDENT.STUDENTID INNER JOIN EIM_SEMESTER on EIM_SEMESTER.SEMESTERID=EIM_TRANS.SEMESTERID
        //                      WHERE TRANSDT BETWEEN @d1 AND @d2", conn);
        //            cmd.Parameters.Clear();
        //            cmd.Parameters.AddWithValue("d1", Date1);
        //            cmd.Parameters.AddWithValue("d2", Date2);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();
        //            da.Fill(ds);
        //            if (conn.State != ConnectionState.Closed)conn.Close();
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                RepeaterHead.DataSource = ds;
        //                RepeaterHead.DataBind();
        //                RepeaterHead.Visible = true;
        //            }
        //            else
        //            {
        //                RepeaterHead.DataSource = ds;
        //                RepeaterHead.DataBind();
        //                RepeaterHead.Visible = true;
        //            }
        //        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Repeater Repeater1= (Repeater)(e.Item.FindControl("Repeater1"));
            Label lbldate1 = (Label)e.Item.FindControl("lbldate1");
            Label lblTransDT = (Label)e.Item.FindControl("lblTransDT");
            Label lblHead = (Label)e.Item.FindControl("lblHead");

            string Head = lblHead.Text;
            DateTime DT = DateTime.Parse(lblTransDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string Date = DT.ToString("yyyy/MM/dd");
            string TransNO = lbldate1.Text;
            GridView gv_Trans = (GridView)e.Item.FindControl("gv_Trans");
            DataTable Head2 = LoadData2(TransNO, Date);
            if (Head2.Rows.Count > 0)
            {
                gv_Trans.DataSource = Head2;
                gv_Trans.DataBind();
            }
            else
            {
                //lblText.Visible = false;
                lblHead.Visible = false;
                gv_Trans.Visible = false;
            }
        }
        private DataTable LoadData2(string TransNO, string TransDT)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            DataTable dtGrid = new DataTable();
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand();
            if (Session["CNBCD"].ToString() != "102010100001")
            {
                cmd = new SqlCommand(@"  SELECT EIM_TRANS.TRANSNO,convert(nvarchar(10),EIM_TRANS.TRANSDT,103) AS TRANSDT, EIM_STUDENT.NEWSTUDENTID,EIM_STUDENT.STUDENTNM,  EIM_FEES.FEESNM,  EIM_TRANS.AMOUNT 
                      FROM EIM_FEES INNER JOIN
                      EIM_TRANS ON EIM_FEES.FEESID = EIM_TRANS.FEESID INNER JOIN
                      EIM_STUDENT ON EIM_TRANS.STUDENTID = EIM_STUDENT.STUDENTID 
                      WHERE EIM_TRANS.TRANSTP='MREC' AND CNBCD!='102010100001' and EIM_TRANS.TRANSNO=" + TransNO + " AND EIM_TRANS.TRANSDT='" + TransDT + "'ORDER BY EIM_TRANS.TRANSNO ", conn);
            }
            else
            {
                cmd = new SqlCommand(@"  SELECT EIM_TRANS.TRANSNO,convert(nvarchar(10),EIM_TRANS.TRANSDT,103) AS TRANSDT, EIM_STUDENT.NEWSTUDENTID,EIM_STUDENT.STUDENTNM,  EIM_FEES.FEESNM,  EIM_TRANS.AMOUNT 
                      FROM EIM_FEES INNER JOIN
                      EIM_TRANS ON EIM_FEES.FEESID = EIM_TRANS.FEESID INNER JOIN
                      EIM_STUDENT ON EIM_TRANS.STUDENTID = EIM_STUDENT.STUDENTID 
                      WHERE EIM_TRANS.TRANSTP='MREC' AND CNBCD='102010100001'  and EIM_TRANS.TRANSNO=" + TransNO + " AND EIM_TRANS.TRANSDT='" + TransDT + "'ORDER BY EIM_TRANS.TRANSNO ", conn);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtGrid);
            if (conn.State != ConnectionState.Closed)conn.Close();
            return dtGrid;
        }
    }
}