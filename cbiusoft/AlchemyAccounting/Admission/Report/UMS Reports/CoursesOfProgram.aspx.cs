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

namespace AlchemyAccounting.Admission.Report.UMS_Reports
{
    public partial class CoursesOfProgram : System.Web.UI.Page
    {

        int intSubTotalIndex = 1;
        string strPreviousRowID = string.Empty;
        SqlConnection conn = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            gridShow();
        }
        private void gridShow()
        {

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  EIM_COURSE.PROGRAMID,EIM_COURSE.COURSENM, EIM_COURSE.COURSECD,EIM_COURSE.COURSEID, EIM_PROGRAM.PROGRAMNM
                        FROM EIM_COURSE INNER JOIN
                      EIM_PROGRAM ON EIM_COURSE.PROGRAMID = EIM_PROGRAM.PROGRAMID ORDER BY EIM_COURSE.PROGRAMID", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_CourseProgram.DataSource = ds;
                gv_CourseProgram.DataBind();

            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_CourseProgram.DataSource = ds;
                gv_CourseProgram.DataBind();
                int columncount = gv_CourseProgram.Rows[0].Cells.Count;
                gv_CourseProgram.Rows[0].Cells.Clear();
                gv_CourseProgram.Rows[0].Cells.Add(new TableCell());
                gv_CourseProgram.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_CourseProgram.Rows[0].Visible = false;

            }
        }
        protected void gv_CourseProgram_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "PROGRAMID").ToString();

                string COURSENM = DataBinder.Eval(e.Row.DataItem, "COURSENM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + COURSENM;

                string COURSECD = DataBinder.Eval(e.Row.DataItem, "COURSECD").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + COURSECD;

                string COURSEID = DataBinder.Eval(e.Row.DataItem, "COURSEID").ToString();

                e.Row.Cells[3].Text = "&nbsp;" + COURSEID;

            }
        }

        protected void gv_CourseProgram_RowCreated(object sender, GridViewRowEventArgs e)
        {

            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "PROGRAMID") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "PROGRAMID").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "PROGRAMID") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "PROGRAMID") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = " " + DataBinder.Eval(e.Row.DataItem, "PROGRAMNM").ToString();
                cell.ColumnSpan = 4;
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
                if (DataBinder.Eval(e.Row.DataItem, "PROGRAMID") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();

                    cell.Text = " " + DataBinder.Eval(e.Row.DataItem, "PROGRAMNM").ToString();
                    cell.ColumnSpan = 4;
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


