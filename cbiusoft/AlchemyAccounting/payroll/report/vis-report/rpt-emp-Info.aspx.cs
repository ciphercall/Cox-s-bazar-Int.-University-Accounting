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
    public partial class rpt_emp_Info : System.Web.UI.Page
    {
        string strPreviousRowID = string.Empty;
        string strPreviousRowID2 = string.Empty;
        string strPreviousRowID3 = string.Empty;
        string strPreviousRowID4 = string.Empty;
        string strPreviousRowID5 = string.Empty;

        int intSubTotalIndex = 1;
        int intSubTotalIndex2 = 1;
        decimal dblSubTotalAmount = 0;
        decimal dblGrandTotalAmount = 0;
        string dblSubTotalAmountComma = "0";
        string dblGrandTotalAmountComma = "0";
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("/user/Login.aspx");
            }

            else
            {
                if (!IsPostBack)
                {
                    Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompanyNM);
                    Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

                    ShowGrid();

                    DateTime t = DateTime.Now;
                    lblPrintDate.Text = t.ToString("dd/MM/yyy hh:mm:ss:tt");
                }
            }
        }

        private void ShowGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);


            SqlCommand cmd = new SqlCommand("SELECT COMPANYNM, EMPID, EMPNM, QATARID,  CONVERT(NVARCHAR(20),IDEXPDT,103) AS IDEXPD, PPNO,  CONVERT(NVARCHAR(20),PPEXPDT,103) AS PPEXPD, FILENO, CONTACTNO, OCCUPATION, ADDRESS FROM HR_EMP", conn);
            cmd.Parameters.Clear();

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
            bool IsSubTotalRowNeedToAdd = false;
            //bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "COMPANYNM") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "COMPANYNM").ToString())

                    IsSubTotalRowNeedToAdd = true;


            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "COMPANYNM") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                //IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;

            }

            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "COMPANYNM") != null))
            {
                GridView gvReport = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();

                cell.Text = DataBinder.Eval(e.Row.DataItem, "COMPANYNM").ToString();
                cell.ColumnSpan = 5;
                cell.Visible = true;
                cell.CssClass = "GroupHeaderStyle";
                cell.Font.Bold = true;
                row.Cells.Add(cell);

                //cell = new TableCell();
                //cell.Text = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                ////cell.ColumnSpan = 5;
                //cell.CssClass = "GroupHeaderStyle";
                //cell.Font.Bold = true;
                //row.Cells.Add(cell);

                //cell = new TableCell();
                //cell.Text = "Party :  " + DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                ////cell.ColumnSpan = 5;
                //cell.CssClass = "GroupHeaderStyle";
                //cell.Font.Bold = true;
                //row.Cells.Add(cell);

                gvReport.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;



            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Sub Total Row
                GridView gvReport = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                //cell.Text = "Sub Total : ";
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.ColumnSpan = 2;
                //cell.Font.Bold = true;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);

                ////Adding Amount Column         
                //cell = new TableCell();
                //cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma);
                //cell.HorizontalAlign = HorizontalAlign.Right;
                //cell.Font.Bold = true;
                //cell.CssClass = "SubTotalRowStyle";
                //row.Cells.Add(cell);


                //Adding the Row at the RowIndex position in the Grid      
                gvReport.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "COMPANYNM") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "COMPANYNM").ToString();
                    cell.ColumnSpan = 5;
                    cell.Visible = true;
                    cell.CssClass = "GroupHeaderStyle";
                    cell.Font.Bold = true;
                    row.Cells.Add(cell);


                   


                    gvReport.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables
                dblSubTotalAmount = 0;
                #endregion
            }

        }

        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "COMPANYNM").ToString();



                string EMPID = DataBinder.Eval(e.Row.DataItem, "EMPID").ToString();
                e.Row.Cells[0].Text = EMPID;

                string EMPNM = DataBinder.Eval(e.Row.DataItem, "EMPNM").ToString();
                e.Row.Cells[1].Text = EMPNM;

                string QATARID = DataBinder.Eval(e.Row.DataItem, "QATARID").ToString();
                e.Row.Cells[2].Text = QATARID;

                string IDEXPD = DataBinder.Eval(e.Row.DataItem, "IDEXPD").ToString();
                e.Row.Cells[3].Text = IDEXPD;

                string PPNO = DataBinder.Eval(e.Row.DataItem, "PPNO").ToString();
                e.Row.Cells[4].Text = PPNO;

                string PPEXPD = DataBinder.Eval(e.Row.DataItem, "PPEXPD").ToString();
                e.Row.Cells[5].Text = PPEXPD;

                string FILENO = DataBinder.Eval(e.Row.DataItem, "FILENO").ToString();
                e.Row.Cells[6].Text = FILENO;

                string CONTACTNO = DataBinder.Eval(e.Row.DataItem, "CONTACTNO").ToString();
                e.Row.Cells[7].Text = CONTACTNO;

                string OCCUPATION = DataBinder.Eval(e.Row.DataItem, "OCCUPATION").ToString();
                e.Row.Cells[8].Text = OCCUPATION;

                string ADDRESS = DataBinder.Eval(e.Row.DataItem, "ADDRESS").ToString();
                e.Row.Cells[9].Text = ADDRESS;
  


                //decimal EXPAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "EXPAMT").ToString());
                //string INWORDS = SpellAmount.comma(EXPAMT);
                ////e.Row.Cells[3].Text = styleQty;
                //e.Row.Cells[2].Text = INWORDS;


                //dblSubTotalAmount += EXPAMT;
                //dblSubTotalAmountComma = SpellAmount.comma(dblSubTotalAmount);
                //e.Row.Font.Bold = true;

                //dblGrandTotalAmount += EXPAMT;
                //dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);


            }
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.Cells[1].Text = "Grand Total : ";
            //    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            //    e.Row.Cells[2].Text = dblGrandTotalAmountComma;
            //    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            //    e.Row.Font.Bold = true;
            //}
        }
    }
}