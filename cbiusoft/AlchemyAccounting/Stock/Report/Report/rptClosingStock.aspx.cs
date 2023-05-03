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
using System.Threading;

namespace AlchemyAccounting.Stock.Report.Report
{
    public partial class rptClosingStock : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        // To keep track of the previous row Group Identifier    
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        // To temporarily store Sub Total    
        decimal dblSubTotalCartonQty = 0;
        decimal dblSubTotalPieces = 0;
        decimal dblSubTotalCLQty = 0;
        //decimal dblSubTotalRate = 0;
        decimal dblSubTotalAmount = 0;
        // To temporarily store Grand Total    
        decimal dblGrandTotalCartonQty = 0;
        decimal dblGrandTotalPieces = 0;
        decimal dblGrandTotalCLQty = 0;
        //decimal dblGrandTotalRate = 0;
        decimal dblGrandTotalAmount = 0;
        //string AmountComma = "";
        string dblSubTotalCartonQtyComma = "";
        string dblSubTotalPiecesComma = "";
        string dblSubTotalCLQtyComma = "";
        string dblSubTotalRateComma = "";
        string dblSubTotalAmountComma = "";

        string dblGrandTotalCartonQtyComma = "";
        string dblGrandTotalPiecesComma = "";
        string dblGrandTotalCLQtyComma = "";
        string dblGrandTotalRateComma = "";
        string dblGrandTotalAmountComma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string StoreNM = Session["StoreNM"].ToString();
                string StoreID = Session["StoreID"].ToString();
                string Date = Session["Date"].ToString();

                DateTime asON = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblDate.Text = asON.ToString("dd-MMM-yyyy");
                lblStoreNM.Text = StoreNM;
                showGrid();
            }
            catch
            {
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string StoreNM = Session["StoreNM"].ToString();
            string StoreID = Session["StoreID"].ToString();
            string Date = Session["Date"].ToString();
            DateTime asON = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string aOn = asON.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT B.CATID, B.ITEMID, B.CLQTY, B.AVGRATE, (CASE WHEN B.CLQTY < 0 THEN 0.00 ELSE B.CLQTY * B.AVGRATE END) AS STOCKVALUE, STK_ITEMMST.CATNM, STK_ITEM.ITEMNM, " +
                             " STK_ITEM.PQTY, (CASE WHEN STK_ITEM.PQTY = 0 THEN 0 ELSE FLOOR(B.CLQTY/PQTY)END) AS CARTONQTY, (B.CLQTY - (CASE WHEN STK_ITEM.PQTY = 0 THEN 0 ELSE (FLOOR(B.CLQTY/PQTY)*PQTY) END)) AS PIECES " +
                             " FROM (SELECT CATID, ITEMID, (SUM(INQTY) + SUM(BQTY)) - (SUM(OUTQTY) + SUM(SQTY)) AS CLQTY, SUM(BQTY) AS BQTY, SUM(BAMT) AS BAMT, SUM(SQTY) AS SQTY, " +
                                              " SUM(SAMT) AS SAMT, (CASE WHEN SUM(BAMTR) = 0.00 THEN 0.00 ELSE CONVERT(decimal(18, 2), (SUM(BAMTR)) / SUM(BQTYR)) END) AS AVGRATE " +
                               " FROM (SELECT CATID, ITEMID, SUM(QTY) AS BQTY, SUM(AMOUNT) AS BAMT, 0 AS SQTY, 0 AS SAMT, 0 AS INQTY, 0 AS OUTQTY, 0 AS BQTYR, 0 AS BAMTR " +
                                               " FROM STK_TRANS " +
                                               " WHERE (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'BUY') AND (STORETO = '" + StoreID + "')  " +
                                               " GROUP BY CATID, ITEMID " + 
                                               " UNION " +
                                               " SELECT CATID, ITEMID, 0 AS BQTY, 0 AS BAMT, SUM(QTY) AS SQTY, SUM(AMOUNT) AS SAMT, 0 AS INQTY, 0 AS OUTQTY, 0 AS BQTYR, 0 AS BAMTR " +
                                               " FROM STK_TRANS AS STK_TRANS_1 " +
                                               " WHERE     (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'SALE') AND (STOREFR = '" + StoreID + "') " +
                                               " GROUP BY CATID, ITEMID  " +
                                               " UNION " +
                                               " SELECT CATID, ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS SAMT, SUM(QTY) AS INQTY, 0 AS OUTQTY, 0 AS BQTYR, 0 AS BAMTR  " +
                                               " FROM STK_TRANS AS STK_TRANS_2 " +
                                               " WHERE     (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'ITRF') AND (STORETO = '" + StoreID + "') " +
                                               " GROUP BY CATID, ITEMID  " +
                                               " UNION  " +
                                               " SELECT     CATID, ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS SAMT, 0 AS INQTY, SUM(QTY) AS OUTQTY, 0 AS BQTYR, 0 AS BAMTR " +
                                               " FROM         STK_TRANS AS STK_TRANS_1  " +
                                               " WHERE     (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'ITRF') AND (STOREFR = '" + StoreID + "')  " +
                                               " GROUP BY CATID, ITEMID " +
                                               " UNION " +
                                               " SELECT CATID, ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS SAMT, 0 AS INQTY, 0 AS OUTQTY,SUM(QTY) AS BQTYR, SUM(AMOUNT) AS BAMTR " +
                                               " FROM STK_TRANS " +
                                               " WHERE (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'BUY') " +
                                               " GROUP BY CATID, ITEMID ) AS A  " +
                      " GROUP BY CATID, ITEMID) AS B INNER JOIN " +
                      " STK_ITEM ON B.CATID = STK_ITEM.CATID AND B.ITEMID = STK_ITEM.ITEMID INNER JOIN " +
                      " STK_ITEMMST ON STK_ITEM.CATID = STK_ITEMMST.CATID " +
" WHERE     (B.CLQTY <> 0)", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Label1.Visible = false;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "No Data Found.";
            }
        }

        /// <summary>   
        /// /// Event fires for every row creation   
        /// /// Used for creating SubTotal row when next group starts by adding Group Total at previous row manually    
        /// </summary>    /// <param name="sender"></param>    /// <param name="e"></param>   
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATID") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "CATID").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "CATID") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Category Particulars : " + DataBinder.Eval(e.Row.DataItem, "CATNM").ToString();
                cell.ColumnSpan = 6;
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
                cell.Text = "Category Wise Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 2;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Carton Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCartonQtyComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Pieces Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalPiecesComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CLQTY Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCLQtyComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Amount Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalRateComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Amount Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle"; 
                row.Cells.Add(cell);
                //Adding the Row at the RowIndex position in the Grid      
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "CATID") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "Category Particulars : " + DataBinder.Eval(e.Row.DataItem, "CATNM").ToString();
                    cell.ColumnSpan = 6;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables
                dblSubTotalCartonQty = 0;
                dblSubTotalPieces = 0;
                dblSubTotalCLQty = 0;
                dblSubTotalAmount = 0;
                #endregion
            }
            if (IsGrandTotalRowNeedtoAdd)
            {
                #region Grand Total Row
                GridView GridView1 = (GridView)sender;
                // Creating a Row      
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell           
                TableCell cell = new TableCell();
                cell.Text = "Store Wise Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 2;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                
                ////Adding Carton Qty Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalCartonQtyComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                
                ////Adding Pieces Column           
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalPiecesComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                
                ////Adding CLQty Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalCLQtyComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Amount Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalRateComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Amount Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalAmountComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                //Adding the Row at the RowIndex position in the Grid     
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                #endregion
            }
        }

        /// <summary>    
        /// Event fires when data binds to each row   
        /// Used for calculating Group Total     
        /// </summary>   
        /// /// <param name="sender"></param>    
        /// <param name="e"></param>    
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "CATID").ToString();


                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ItemNM;

                decimal Car_Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CARTONQTY").ToString());
                string CARTONQTY = Car_Qty.ToString("#,##0.00");
                e.Row.Cells[1].Text = CARTONQTY;

                decimal dblCartonQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CARTONQTY").ToString());

                decimal P_Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PIECES").ToString());
                string PIECES = P_Qty.ToString("#,##0.00");
                e.Row.Cells[2].Text = PIECES;

                decimal dblPieces = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PIECES").ToString());

                decimal CL_Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CLQTY").ToString());
                string CLQTY = CL_Qty.ToString("#,##0.00");
                e.Row.Cells[3].Text = CLQTY;

                decimal dblCLQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CLQTY").ToString());

                decimal Stk_V = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STOCKVALUE").ToString());
                string STOCKVALUE = Stk_V.ToString("#,##0.00");
                e.Row.Cells[5].Text = STOCKVALUE;

                decimal dblAmount = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STOCKVALUE").ToString());

                decimal rt = (dblAmount / dblCLQTY);
                string RATE = rt.ToString("#,##0.00");
                e.Row.Cells[4].Text = RATE;


                // Cumulating Sub Total            
                dblSubTotalCartonQty += dblCartonQty;
                dblSubTotalCartonQtyComma = dblSubTotalCartonQty.ToString("#,##0.00");
                dblSubTotalPieces += dblPieces;
                dblSubTotalPiecesComma = dblSubTotalPieces.ToString("#,##0.00");
                dblSubTotalCLQty += dblCLQTY;
                dblSubTotalCLQtyComma = dblSubTotalCLQty.ToString("#,##0.00");
                //dblSubTotalRate += rt;
                //dblSubTotalRateComma = dblSubTotalRate.ToString("#,##0.00");
                dblSubTotalAmount += dblAmount;
                dblSubTotalAmountComma = dblSubTotalAmount.ToString("#,##0.00");
                // Cumulating Grand Total           
                dblGrandTotalCartonQty += dblCartonQty;
                dblGrandTotalCartonQtyComma = dblGrandTotalCartonQty.ToString("#,##0.00");
                dblGrandTotalPieces += dblPieces;
                dblGrandTotalPiecesComma = dblGrandTotalPieces.ToString("#,##0.00");
                dblGrandTotalCLQty += dblCLQTY;
                dblGrandTotalCLQtyComma = dblGrandTotalCLQty.ToString("#,##0.00");
                //dblGrandTotalRate += rt;
                //dblGrandTotalRateComma = SpellAmount.comma(dblGrandTotalRate);

                dblGrandTotalAmount += dblAmount;
                dblGrandTotalAmountComma = dblGrandTotalAmount.ToString("#,##0.00");

                // This is for cumulating the values  
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ddd'");
                //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
                //    e.Row.Attributes.Add("style", "cursor:pointer;");
                //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                //}
            }

            ShowHeader(GridView1);
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

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Select")
        //    {
        //        string productID = GridView1.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[0].Text;
        //        string productName = GridView1.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[1].Text;
        //        //lblProductID.Text = productID;
        //        //lblProduct.Text = productName;
        //    }
        //}

        string parseValueIntoCurrency(double number)
        {
            // set currency format
            string curCulture = Thread.CurrentThread.CurrentCulture.ToString();
            System.Globalization.NumberFormatInfo currencyFormat = new
                System.Globalization.CultureInfo(curCulture).NumberFormat;

            currencyFormat.CurrencyNegativePattern = 1;

            return number.ToString("c", currencyFormat);
        }

    }
}