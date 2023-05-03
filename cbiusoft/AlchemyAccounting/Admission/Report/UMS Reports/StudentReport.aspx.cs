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

namespace AlchemyAccounting.Admission.Report.UMS_Reports
{
    public partial class StudentReport : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Global.connection);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Global.dropDownAdd(ddlYR, "SELECT DISTINCT(ADMITYY) FROM EIM_STUDENT");
                //                Global.dropDownAdd(ddlSemNM, @"SELECT     DISTINCT(EIM_SEMESTER.SEMESTERNM)
                //                                                FROM EIM_STUDENT INNER JOIN
                //                                                 EIM_SEMESTER ON EIM_STUDENT.SEMESTERID = EIM_SEMESTER.SEMESTERID");
                //                Global.dropDownAdd(ddlProgNM, @"SELECT     DISTINCT(EIM_PROGRAM.PROGRAMNM)
                //                                                FROM EIM_STUDENT INNER JOIN
                //                                                EIM_PROGRAM ON EIM_STUDENT.PROGRAMID = EIM_PROGRAM.PROGRAMID");
                gv_Student.Visible = false;
                Global.DropDownAddAllTextWithValue(ddlSemNM, @"SELECT  SEMESTERNM,SEMESTERID  FROM  EIM_SEMESTER");
                Global.DropDownAddAllTextWithValue(ddlProgNM, @"SELECT  PROGRAMNM,PROGRAMID FROM  EIM_PROGRAM ORDER BY PROGRAMNM");
                Global.dropDownAdd(ddlBatch, @"SELECT DISTINCT BATCH FROM  EIM_STUDENT");
                string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                int i, m;
                int a = int.Parse(yr);
                m = a + 10;
                ddlYR.Items.Add("Select");
                for (i = a - 10; i <= m; i++)
                {
                    ddlYR.Items.Add(i.ToString());
                }
                ddlYR.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                ddlSemNM.Focus();
            }
        }

        protected void ddlSemNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlSemNM.Text == "Select")
            //{
            //    ddlSemNM.Focus();
            //    gv_Student.Visible = false;
            //}
            //else
            //{
            //    Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM ='" + ddlSemNM.Text + "'", lblSemID);
            //    ddlYR.Focus();
            //}
        }
        private void gridShow()
        {

            if (conn.State != ConnectionState.Open) conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  ROW_NUMBER() over (order by STUDENTID) as SL,NEWSTUDENTID,STUDENTNM, MOBNO, SESSION, BATCH, CONVERT(NVARCHAR(10),ADMITDT,103) ADMITTP
                                                      FROM EIM_STUDENT 
            WHERE ADMITYY='" + ddlYR.Text + "' AND PROGRAMID='" + ddlProgNM.Text + "' AND BATCH='" + ddlBatch.Text + "'", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed) conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Student.DataSource = ds;
                gv_Student.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Student.DataSource = ds;
                gv_Student.DataBind();
                int columncount = gv_Student.Rows[0].Cells.Count;
                gv_Student.Rows[0].Cells.Clear();
                gv_Student.Rows[0].Cells.Add(new TableCell());
                gv_Student.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Student.Rows[0].Visible = false;
            }
        }
        protected void gv_Student_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string ROLLNO = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ROLLNO;
                string STUDENTID = DataBinder.Eval(e.Row.DataItem, "NEWSTUDENTID").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + STUDENTID;
                string STUDENTNM = DataBinder.Eval(e.Row.DataItem, "STUDENTNM").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + STUDENTNM;
                string MOBNO = DataBinder.Eval(e.Row.DataItem, "MOBNO").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + MOBNO;
                string REMARKS = DataBinder.Eval(e.Row.DataItem, "SESSION").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + REMARKS;
                string MRNO = DataBinder.Eval(e.Row.DataItem, "BATCH").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + MRNO;
                string MRAMT = DataBinder.Eval(e.Row.DataItem, "ADMITTP").ToString();
                e.Row.Cells[6].Text = "&nbsp;" + MRAMT;


            }
        }
        protected void ddlProgNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgNM.Text == "Select")
            {
                gv_Student.Visible = false;
            }
            else
            {
                string yr = Global.GetData("SELECT ADMITYY FROM EIM_STUDENT WHERE BATCH='" + ddlBatch.Text + "' AND PROGRAMID='" + ddlProgNM.Text + "'");
                if (yr == "")
                    yr = "Select";
                ddlYR.Text = yr;
                string Sem = Global.GetData("SELECT SEMESTERID FROM EIM_STUDENT WHERE BATCH='" + ddlBatch.Text + "' AND PROGRAMID='" + ddlProgNM.Text + "'");
                if (Sem == "")
                    Sem = "--SELECT--";
                ddlSemNM.Text = Sem;
                gv_Student.Visible = true;
                gridShow();
            }
        }

        protected void ddlYR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlYR.SelectedIndex = -1;
                ddlProgNM.SelectedIndex = -1;
                gv_Student.Visible = false;
            }
            else if (ddlYR.Text == "Select")
            {
                gv_Student.Visible = false;
                ddlYR.Focus();
            }
            else
            {
                ddlProgNM.Focus();
            }
        }



        protected void btnPrint_Click1(object sender, EventArgs e)
        {
            if (ddlSemNM.Text == "Select")
                ddlSemNM.Focus();
            else if (ddlYR.Text == "Select")
                ddlYR.Focus();
            else if (ddlProgNM.Text == "Select")
                ddlProgNM.Focus();
            else
            {
                Session["SEMESTERNM"] = "";
                Session["YEAR"] = "";
                Session["PROGRAMNM"] = "";
                Session["SEMESTERID"] = "";
                Session["PROGRAMID"] = "";


                Session["SEMESTERNM"] = ddlSemNM.SelectedItem;
                Session["YEAR"] = ddlYR.Text;
                Session["PROGRAMNM"] = ddlProgNM.SelectedItem;
                Session["SEMESTERID"] = ddlSemNM.Text;
                Session["PROGRAMID"] = ddlProgNM.Text;
                Session["BATCH"] = ddlBatch.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                       "OpenWindow", "window.open('/Admission/Report/StudentReportPrint.aspx','_newtab');", true);
            }
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProgNM.Focus();
        }
    }
}