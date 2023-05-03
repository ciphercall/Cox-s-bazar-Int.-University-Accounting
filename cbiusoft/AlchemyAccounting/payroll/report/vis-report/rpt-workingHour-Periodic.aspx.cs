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
    public partial class rpt_workingHour_Periodic : System.Web.UI.Page
    {
        string strPreviousRowID = string.Empty;
        string strPreviousRowID2 = string.Empty;
        string strPreviousRowID3 = string.Empty;
        string strPreviousRowID4 = string.Empty;
        string strPreviousRowID5 = string.Empty;

        int intSubTotalIndex = 1;
        int intSubTotalIndex2 = 1;
        decimal dblSubTotalAmount = 0;
        decimal dblSubTotalAmount1 = 0;
        decimal dblSubTotalAmount2 = 0;
        decimal dblSubTotalAmount3 = 0;

        string dblSubTotalAmountComma = "0";
        string dblSubTotalAmountComma1 = "0";
        string dblSubTotalAmountComma2 = "0";
        string dblSubTotalAmountComma3 = "0";

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);


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

                    string fromDate = Session["fromdate"].ToString();
                    string todate = Session["todate"].ToString();

                    lblDate.Text = fromDate;
                    lblToDate.Text = todate;
                    ShowGrid();
                    lblCompNM.Text = " Helmi Trading & Contracting W.L.L";
                }
            }
        }

        private void ShowGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string fromDate = Session["fromdate"].ToString();
            string todate = Session["todate"].ToString();

            DateTime FRDT = DateTime.Parse(fromDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FDT = FRDT.ToString("yyyy-MM-dd");

            DateTime TRDT = DateTime.Parse(todate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TDT = TRDT.ToString("yyyy-MM-dd");

            SqlCommand cmd = new SqlCommand("SELECT  GL_COSTP.COSTPNM, HR_EMP.EMPNM, SUM(HR_HOUR.NORMALHR) AS NORMALHR, SUM(HR_HOUR.NORMALOT) AS NORMALOT, SUM(HR_HOUR.FRIDAYOT) AS FRIDAYOT, ( SUM(HR_HOUR.NORMALHR) + SUM(HR_HOUR.NORMALOT) + SUM(HR_HOUR.FRIDAYOT)) AS TOTALHOUR " +
                                        " FROM  HR_EMP INNER JOIN "+
                      " HR_HOUR ON HR_EMP.EMPID = HR_HOUR.EMPID INNER JOIN "+
                       " GL_COSTP ON HR_HOUR.COSTPID = GL_COSTP.COSTPID "+
                        " WHERE     HR_HOUR.TRANSDT BETWEEN @FROMDATE AND @TODATE " +
                        " GROUP BY  HR_EMP.EMPNM , GL_COSTP.COSTPNM ", conn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@FROMDATE", FDT);
            cmd.Parameters.AddWithValue("@TODATE", TDT);
            conn.Open();
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
                gvReport.DataSource = ds;
                gvReport.DataBind();
                gvReport.Visible = true;
            }
        }



        protected void gvReport_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //bool IsSubTotalRowNeedToAdd = false;
            ////bool IsGrandTotalRowNeedtoAdd = false;
            //if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "COSTPNM") != null))
            //    if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "COSTPNM").ToString())

            //        IsSubTotalRowNeedToAdd = true;


            //if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "COSTPNM") == null))
            //{
            //    IsSubTotalRowNeedToAdd = true;
            //    //IsGrandTotalRowNeedtoAdd = true;
            //    intSubTotalIndex = 0;

            //}

            //#region Inserting first Row and populating fist Group Header details
            //if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "COSTPNM") != null))
            //{
            //    GridView gvReport = (GridView)sender;
            //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //    TableCell cell = new TableCell();
            //    cell.Text = "  Site : " + DataBinder.Eval(e.Row.DataItem, "COSTPNM").ToString();
            //    //cell.ColumnSpan = 5;
            //    cell.Visible = true;
            //    cell.CssClass = "GroupHeaderStyle";
            //    cell.Font.Bold = true;
            //    row.Cells.Add(cell);



            //    //cell = new TableCell();
            //    //cell.Text = "Party :  " + DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
            //    ////cell.ColumnSpan = 5;
            //    //cell.CssClass = "GroupHeaderStyle";
            //    //cell.Font.Bold = true;
            //    //row.Cells.Add(cell);

            //    gvReport.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
            //    intSubTotalIndex++;



            //}
            //#endregion
            //if (IsSubTotalRowNeedToAdd)
            //{
            //    #region Adding Sub Total Row
            //    GridView gvReport = (GridView)sender;
            //    // Creating a Row          
            //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //    //Adding Total Cell          
            //    TableCell cell = new TableCell();
            //    cell.Text = "Total : ";
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.ColumnSpan = 0;
            //    cell.Font.Bold = true;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    //Adding Amount Column   
            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma3);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.Font.Bold = true;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma2);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.Font.Bold = true;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma1);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.Font.Bold = true;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);

            //    cell = new TableCell();
            //    cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma);
            //    cell.HorizontalAlign = HorizontalAlign.Right;
            //    cell.Font.Bold = true;
            //    cell.CssClass = "SubTotalRowStyle";
            //    row.Cells.Add(cell);



            //    //Adding the Row at the RowIndex position in the Grid      
            //    gvReport.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
            //    intSubTotalIndex++;
            //    #endregion
            //    #region Adding Next Group Header Details
            //    if (DataBinder.Eval(e.Row.DataItem, "COSTPNM") != null)
            //    {
            //        row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //        cell = new TableCell();
            //        cell.Text = DataBinder.Eval(e.Row.DataItem, "COSTPNM").ToString();
            //        //cell.ColumnSpan = 5;
            //        cell.Visible = true;
            //        cell.CssClass = "GroupHeaderStyle";
            //        cell.Font.Bold = true;
            //        row.Cells.Add(cell);





            //        gvReport.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
            //        intSubTotalIndex++;
            //    }
            //    #endregion
            //    #region Reseting the Sub Total Variables
            //    dblSubTotalAmount = 0;
            //    #endregion
            //}
        }

        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "COSTPNM").ToString();



                string EMPNM = DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + "&nbsp;" + "&nbsp;" + "&nbsp;" + EMPNM;

                decimal NORMALHR = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NORMALHR").ToString());
                e.Row.Cells[1].Text =  "&nbsp;" + NORMALHR;

                dblSubTotalAmount3 += NORMALHR;
                dblSubTotalAmountComma3 = dblSubTotalAmount3.ToString("#,##0.00");
                


                decimal NORMALOT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NORMALOT").ToString());
                e.Row.Cells[2].Text = "&nbsp;" + NORMALOT;

                dblSubTotalAmount2 += NORMALOT;
                dblSubTotalAmountComma2 = dblSubTotalAmount2.ToString("#,##0.00");
                



                decimal FRIDAYOT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FRIDAYOT").ToString());

                e.Row.Cells[3].Text =  "&nbsp;" + FRIDAYOT;

                dblSubTotalAmount1 += FRIDAYOT;
                dblSubTotalAmountComma1 = dblSubTotalAmount1.ToString("#,##0.00");
               


                decimal TOTALHOUR = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTALHOUR").ToString());
                string INWORDS = TOTALHOUR.ToString("#,##0.00");
                e.Row.Cells[4].Text = INWORDS;


                dblSubTotalAmount += TOTALHOUR;
                dblSubTotalAmountComma = dblSubTotalAmount.ToString("#,##0.00");
                



                //dblGrandTotalAmount += TOTALHOUR;
                //dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);


            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = " Total : ";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[1].Text = dblSubTotalAmountComma3;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Font.Bold = true;

                e.Row.Cells[2].Text = dblSubTotalAmountComma2;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Font.Bold = true;

                e.Row.Cells[3].Text = dblSubTotalAmountComma1;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Font.Bold = true;

                e.Row.Cells[4].Text = dblSubTotalAmountComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Font.Bold = true;
            }
        }


    }
}