using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Accounts.Report.Report
{
    public partial class rptCheckRegister : System.Web.UI.Page
    {
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;

        decimal dblSubTotalAmount = 0;

        decimal dblGrandTotalAmount = 0;

        string dblSubTotalAmountComma = "0";

        string dblGrandTotalAmountComma = "0";
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
                lbltimepm.Text = Global.Dayformat1(DateTime.Now).ToString("hh:mm tt");
                gridShow();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void gridShow()
        {
            string frmDT = Session["FRDT"].ToString();
            string toDT = Session["TODT"].ToString();
            lblHeadNM.Text = Session["TYPENM"].ToString();
            lblFrom.Text = frmDT;
            lblTo.Text = toDT;
            DateTime From = DateTime.Parse(frmDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime To = DateTime.Parse(toDT, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FdT = From.ToString("yyyy/MM/dd");
            string TdT = To.ToString("yyyy/MM/dd");
            string TYPE = Session["TYPE"].ToString();
            SqlConnection conn = new SqlConnection(Global.connection);
            if (conn.State != ConnectionState.Open)conn.Open();
            string Script = "";
            if (TYPE != "MREC")
            {
                Script = @"SELECT A.ACCOUNTNM, CONVERT(NVARCHAR(10),A.TRANSDT,103) TRANSDT, (CASE  WHEN A.TABLEID = 'GL_STRANS' THEN SUBSTRING(A.TABLEID, 4, 1) +  (CASE 
            WHEN TRANSTP = 'MREC' THEN 'RV '  WHEN TRANSTP = 'MPAY' THEN 'PV '  WHEN TRANSTP = 'JOUR' THEN 'JV ' 
            WHEN TRANSTP = 'CONT' THEN 'CV ' ELSE 'APV ' END)  WHEN A.TABLEID = 'STK_TRANS' THEN 'AJV ' 
            WHEN A.TABLEID = 'LC_EXPENSE' THEN 'APV '  WHEN A.TABLEID = 'EIM_TRANS' THEN 'ARV ' ELSE '' END) + CONVERT(nvarchar(10), A.TRANSNO, 103) AS DOCNO
            ,A.CHEQUENO, CONVERT(NVARCHAR(10),A.CHEQUEDT,103) CHEQUEDT,TRANSMODE,A.[TO-FROM],A.REMARKS,A.DEBITAMT FROM 
            (SELECT GL2.ACCOUNTNM, GL_MASTER.TRANSDT, GL_MASTER.TABLEID,GL_MASTER.TRANSTP,GL_MASTER.TRANSNO,
            GL_MASTER.CHEQUENO, GL_MASTER.CHEQUEDT, GL_MASTER.TRANSMODE, GL_ACCHART.ACCOUNTNM [TO-FROM], GL_MASTER.REMARKS, GL_MASTER.DEBITAMT
            FROM            GL_MASTER INNER JOIN GL_ACCHART ON GL_MASTER.CREDITCD = GL_ACCHART.ACCOUNTCD INNER JOIN GL_ACCHART AS GL2 ON GL_MASTER.DEBITCD = GL2.ACCOUNTCD  " +
            "WHERE TRANSTP='MRECN'  AND GL_MASTER.TRANSDT BETWEEN  '" + FdT + "' AND '" + TdT + "'  AND SUBSTRING(GL_MASTER.DEBITCD,1,7)='1020102' AND GL_MASTER.TRANSDRCR='DEBIT' AND GL_MASTER.TRANSMODE!='CASH' " +
            "UNION  " +
            "SELECT GL2.ACCOUNTNM, GL_MASTER.TRANSDT,  GL_MASTER.TABLEID, GL_MASTER.TRANSTP, GL_MASTER.TRANSNO,  " +
            "GL_MASTER.CHEQUENO, GL_MASTER.CHEQUEDT, GL_MASTER.TRANSMODE, GL_ACCHART.ACCOUNTNM [TO-FROM], GL_MASTER.REMARKS, GL_MASTER.DEBITAMT DEBITAMT  " +
            "FROM            GL_MASTER INNER JOIN GL_ACCHART ON GL_MASTER.DEBITCD = GL_ACCHART.ACCOUNTCD INNER JOIN GL_ACCHART AS GL2 ON GL_MASTER.CREDITCD = GL2.ACCOUNTCD   " +
            "WHERE TRANSTP IN ('MPAY','CONT') AND GL_MASTER.TABLEID!='EIM_TRANS' AND GL_MASTER.TRANSDT BETWEEN  '" + FdT + "'  AND '" + TdT + "'  AND SUBSTRING(GL_MASTER.CREDITCD,1,7)='1020102' AND GL_MASTER.TRANSDRCR='DEBIT' AND  GL_MASTER.TRANSMODE!='CASH'" +
            ") A ";
            }
            else
            {
                Script = @"SELECT A.ACCOUNTNM, CONVERT(NVARCHAR(10),A.TRANSDT,103) TRANSDT, (CASE  WHEN A.TABLEID = 'GL_STRANS' THEN SUBSTRING(A.TABLEID, 4, 1) +  (CASE 
            WHEN TRANSTP = 'MREC' THEN 'RV '  WHEN TRANSTP = 'MPAY' THEN 'PV '  WHEN TRANSTP = 'JOUR' THEN 'JV ' 
            WHEN TRANSTP = 'CONT' THEN 'CV ' ELSE 'APV ' END)  WHEN A.TABLEID = 'STK_TRANS' THEN 'AJV ' 
            WHEN A.TABLEID = 'LC_EXPENSE' THEN 'APV '  WHEN A.TABLEID = 'EIM_TRANS' THEN 'ARV ' ELSE '' END) + CONVERT(nvarchar(10), A.TRANSNO, 103) AS DOCNO
            ,A.CHEQUENO, CONVERT(NVARCHAR(10),A.CHEQUEDT,103) CHEQUEDT,TRANSMODE,A.[TO-FROM],A.REMARKS,A.DEBITAMT FROM 
            (SELECT GL2.ACCOUNTNM, GL_MASTER.TRANSDT, GL_MASTER.TABLEID,GL_MASTER.TRANSTP,GL_MASTER.TRANSNO,
            GL_MASTER.CHEQUENO, GL_MASTER.CHEQUEDT, GL_MASTER.TRANSMODE, GL_ACCHART.ACCOUNTNM [TO-FROM], GL_MASTER.REMARKS, GL_MASTER.DEBITAMT
            FROM            GL_MASTER INNER JOIN GL_ACCHART ON GL_MASTER.CREDITCD = GL_ACCHART.ACCOUNTCD INNER JOIN GL_ACCHART AS GL2 ON GL_MASTER.DEBITCD = GL2.ACCOUNTCD  " +
           "WHERE TRANSTP='MREC'  AND GL_MASTER.TABLEID!='EIM_TRANS' AND GL_MASTER.TRANSDT BETWEEN  '" + FdT + "' AND '" + TdT + "'  AND SUBSTRING(GL_MASTER.DEBITCD,1,7)='1020102' AND GL_MASTER.TRANSDRCR='DEBIT' " +
           "UNION  " +
           "SELECT GL2.ACCOUNTNM, GL_MASTER.TRANSDT,  GL_MASTER.TABLEID, GL_MASTER.TRANSTP, GL_MASTER.TRANSNO,  " +
           "GL_MASTER.CHEQUENO, GL_MASTER.CHEQUEDT, GL_MASTER.TRANSMODE, GL_ACCHART.ACCOUNTNM [TO-FROM], GL_MASTER.REMARKS, GL_MASTER.DEBITAMT DEBITAMT  " +
           "FROM            GL_MASTER INNER JOIN GL_ACCHART ON GL_MASTER.DEBITCD = GL_ACCHART.ACCOUNTCD INNER JOIN GL_ACCHART AS GL2 ON GL_MASTER.CREDITCD = GL2.ACCOUNTCD   " +
           "WHERE TRANSTP='NULLLLL'  AND GL_MASTER.TRANSDT BETWEEN  '" + FdT + "'  AND '" + TdT + "'  AND SUBSTRING(GL_MASTER.CREDITCD,1,7)='1020102' AND GL_MASTER.TRANSDRCR='DEBIT' AND  GL_MASTER.TRANSMODE!='CASH'" +
           ") A ";
            }
            SqlCommand cmd = new SqlCommand(Script, conn);

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
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM") != null))
            {
                GridView gvRep = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                cell.ColumnSpan = 9;
                cell.CssClass = "gridHeadStyle";
                cell.Font.Bold = true;
                row.Cells.Add(cell);
                gvRep.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Sub Total Row
                GridView gvRep = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Sub Total : ";
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 8;
                cell.CssClass = "gridHeadStyle";
                row.Cells.Add(cell);
                cell.Font.Bold = true;

                //Adding Carton Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalAmountComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "gridHeadStyle";
                row.Cells.Add(cell);



                //Adding the Row at the RowIndex position in the Grid      
                gvRep.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                    cell.ColumnSpan = 8;
                    cell.CssClass = "gridHeadStyle";
                    row.Cells.Add(cell);
                    cell.Font.Bold = true;
                    gvRep.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables
                dblSubTotalAmount = 0;
                #endregion
            }
            if (IsGrandTotalRowNeedtoAdd)
            {
                #region Grand Total Row
                GridView gvRep = (GridView)sender;
                // Creating a Row      
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell           
                TableCell cell = new TableCell();
                cell.Text = "Grand Total";
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.ColumnSpan = 8;
                cell.Font.Bold = true;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                ////Adding Carton Qty Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalAmountComma);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                cell.Font.Bold = true;
                //Adding the Row at the RowIndex position in the Grid     
                gvRep.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                #endregion
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();

                    string TRANSDT = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                    e.Row.Cells[1].Text = TRANSDT;

                    string DOCNO = DataBinder.Eval(e.Row.DataItem, "DOCNO").ToString();
                    e.Row.Cells[2].Text = "&nbsp;" + DOCNO;

                    string CHEQUENO = DataBinder.Eval(e.Row.DataItem, "CHEQUENO").ToString();
                    e.Row.Cells[3].Text = CHEQUENO;

                    string CHEQUEDT = DataBinder.Eval(e.Row.DataItem, "CHEQUEDT").ToString();
                    if (CHEQUEDT == "01/01/1900")
                        CHEQUEDT = "";
                    e.Row.Cells[4].Text = CHEQUEDT;

                    string TRANSMODE = DataBinder.Eval(e.Row.DataItem, "TRANSMODE").ToString();
                    e.Row.Cells[5].Text = TRANSMODE;

                    string TOFROM = DataBinder.Eval(e.Row.DataItem, "[TO-FROM]").ToString();
                    e.Row.Cells[6].Text = TOFROM;

                    string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                    e.Row.Cells[7].Text = "&nbsp;" + REMARKS;

                    decimal DEBITAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DEBITAMT").ToString());
                    string DAmnt = SpellAmount.comma(DEBITAMT);
                    e.Row.Cells[8].Text = "&nbsp;" + DAmnt;



                    dblSubTotalAmount += DEBITAMT;
                    dblSubTotalAmountComma = SpellAmount.comma(dblSubTotalAmount);


                    // Add Grand total
                    dblGrandTotalAmount += DEBITAMT;
                    dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

            ShowHeader(GridView1);
        }

        private void ShowHeader(GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.UseAccessibleHeader = true;
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}