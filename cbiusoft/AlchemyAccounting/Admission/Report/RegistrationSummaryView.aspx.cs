using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.Report
{
    public partial class RegistrationSummaryView : System.Web.UI.Page
    {
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        decimal totAmount = 0;
        string totAmountComma = "0";
        decimal dblGrandTotalAmount = 0;
        string grandtotalamt = "0";
        string dblGrandTotalAmountComma = "0";
        SqlConnection conn = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            gridShow();
        }
        private void gridShow()
        {
            String SemID = Session["SEMID"].ToString();
            lblSemester.Text = Session["SEMNM"].ToString();
            String ProgID = Session["PROGID"].ToString();
            lblProgram.Text = Session["PROGNM"].ToString();
            lblBatch.Text = Session["BATCH"].ToString();
            lblSession.Text = Session["SESSION"].ToString();
            SqlConnection conn = new SqlConnection(Global.connection);
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  ('ID: '+ EIM_STUDENT.NEWSTUDENTID+'  Name: '+EIM_STUDENT.STUDENTNM+' | Guardian Name & Address: '+(CASE WHEN EIM_STUDENT.GUARDIANNM='' THEN '' ELSE EIM_STUDENT.GUARDIANNM END)+(CASE WHEN EIM_STUDENT.GADDRESS='' THEN '' ELSE  ' / '+EIM_STUDENT.GADDRESS END)) as GNM_ADDRESS, EIM_STUEDUQ.INSTITUTE, EIM_STUEDUQ.EXAMNM, " +
            "EIM_STUEDUQ.PASSYY, EIM_STUEDUQ.EXAMROLL+' - '+EIM_STUEDUQ.DIVGRADE EXAMROLL  FROM EIM_STUDENT INNER  JOIN EIM_STUEDUQ ON EIM_STUDENT.STUDENTID = EIM_STUEDUQ.STUDENTID WHERE EIM_STUDENT.PROGRAMID='" + ProgID + "' AND EIM_STUDENT.SEMESTERID='" + SemID + "' " +
            "AND EIM_STUDENT.SESSION=@SESSION AND EIM_STUDENT.BATCH='" + lblBatch.Text + "' ORDER BY EIM_STUDENT.STUDENTID", conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SESSION", lblSession.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gv_RegSummery.DataSource = ds;
                Gv_RegSummery.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                Gv_RegSummery.DataSource = ds;
                Gv_RegSummery.DataBind();
                int columncount = Gv_RegSummery.Rows[0].Cells.Count;
                Gv_RegSummery.Rows[0].Cells.Clear();
                Gv_RegSummery.Rows[0].Cells.Add(new TableCell());
                Gv_RegSummery.Rows[0].Cells[0].ColumnSpan = columncount;
                Gv_RegSummery.Rows[0].Visible = false;
            }
        }

        protected void Gv_RegSummery_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "GNM_ADDRESS").ToString();

                string INSTITUTE = DataBinder.Eval(e.Row.DataItem, "INSTITUTE").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + INSTITUTE;
                string EXAMNM = DataBinder.Eval(e.Row.DataItem, "EXAMNM").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + EXAMNM;
                string PASSYY = DataBinder.Eval(e.Row.DataItem, "PASSYY").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + PASSYY;
                string EXAMROLL = DataBinder.Eval(e.Row.DataItem, "EXAMROLL").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + EXAMROLL;


            }
        }
        protected void Gv_RegSummery_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "GNM_ADDRESS") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "GNM_ADDRESS").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "GNM_ADDRESS") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "GNM_ADDRESS") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = " " + DataBinder.Eval(e.Row.DataItem, "GNM_ADDRESS").ToString();
                cell.ColumnSpan = 3;
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
                if (DataBinder.Eval(e.Row.DataItem, "GNM_ADDRESS") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();

                    cell.Text = DataBinder.Eval(e.Row.DataItem, "GNM_ADDRESS").ToString();
                    cell.ColumnSpan = 3;
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
    }
}