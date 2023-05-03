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
    public partial class CourseEnrollment : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Global.dropDownAdd(ddlYR, "SELECT DISTINCT(REGYY) FROM EIM_COURSEREG");
                Global.dropDownAdd(ddlSemNM, @"SELECT  SEMESTERNM FROM EIM_SEMESTER");
                //                Global.dropDownAdd(ddlProNM, @"SELECT DISTINCT(EIM_PROGRAM.PROGRAMNM)
                //                                        FROM EIM_COURSEREG INNER JOIN
                //                      EIM_PROGRAM ON EIM_COURSEREG.PROGRAMID = EIM_PROGRAM.PROGRAMID");
                //                Global.dropDownAdd(ddlCrsNM, @"SELECT    DISTINCT(EIM_COURSE.COURSENM)
                //                                      FROM EIM_COURSE INNER JOIN
                //                                      EIM_COURSEREG ON EIM_COURSE.COURSEID = EIM_COURSEREG.COURSEID");
                Global.dropDownAdd(ddlCrsNM, @"SELECT DISTINCT COURSENM FROM EIM_COURSE");
                string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                int i, m;
                int a = int.Parse(yr);
                m = a + 5;
                ddlYR.Items.Add("Select");
                for (i = a - 5; i <= m; i++)
                {
                    ddlYR.Items.Add(i.ToString());
                }
                ddlYR.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                ddlSemNM.Focus();
            }
        }
        private void gridShow()
        {

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT     EIM_STUDENT.NEWSTUDENTID, EIM_STUDENT.STUDENTNM,convert(nvarchar(10),EIM_COURSEREG.ENRLDT,103) AS ENRLDT, EIM_COURSEREG.REMARKS, EIM_COURSE.COURSENM
                        FROM  EIM_STUDENT INNER JOIN
                      EIM_COURSEREG ON EIM_STUDENT.STUDENTID = EIM_COURSEREG.STUDENTID INNER JOIN
                      EIM_COURSE ON EIM_COURSEREG.COURSEID = EIM_COURSE.COURSEID WHERE EIM_COURSEREG.REGYY='" + ddlYR.Text + "' AND EIM_COURSEREG.COURSEID='" + lblCrsID.Text + "' AND EIM_COURSEREG.SEMESTERID='" + lblSemID.Text + "' ORDER BY EIM_COURSEREG.STUDENTID", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Course.DataSource = ds;

                gv_Course.DataBind();


            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Course.DataSource = ds;
                gv_Course.DataBind();
                int columncount = gv_Course.Rows[0].Cells.Count;
                gv_Course.Rows[0].Cells.Clear();
                gv_Course.Rows[0].Cells.Add(new TableCell());
                gv_Course.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Course.Rows[0].Visible = false;

            }
        }

        protected void ddlSemNM_SelectedIndexChanged(object sender, EventArgs e)
        {

            Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM ='" + ddlSemNM.Text + "'", lblSemID);
            ddlYR.Focus();

        }

        protected void ddlYR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlYR.SelectedIndex = -1;
            }
            else
            {
                ddlCrsNM.Focus();
            }
        }

        protected void ddlCrsNM_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlCrsNM.SelectedIndex = -1;
                ddlYR.SelectedIndex = -1;
            }
            else if (ddlYR.Text == "Select")
            {
                ddlYR.Focus();
                ddlCrsNM.SelectedIndex = -1;
            }
            else if (ddlCrsNM.Text == "Select")
            {
                gv_Course.Visible = false;
            }
            else
            {
                Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE  COURSENM ='" + ddlCrsNM.Text + "'", lblCrsID);
                gv_Course.Visible = true;
                gridShow();
            }
        }

        protected void gv_Course_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string STUDENTNM = DataBinder.Eval(e.Row.DataItem, "STUDENTNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + STUDENTNM;
                string STUDENTID = DataBinder.Eval(e.Row.DataItem, "NEWSTUDENTID").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + STUDENTID;
                string ENRLDT = DataBinder.Eval(e.Row.DataItem, "ENRLDT").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + ENRLDT;
                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + REMARKS;


            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (ddlSemNM.Text == "Select")
                ddlSemNM.Focus();
            else if (ddlYR.Text == "Select")
                ddlYR.Focus();
            else if (ddlCrsNM.Text == "Select")
                ddlCrsNM.Focus();
            else
            {
                Session["SEMESTER"] = ddlSemNM.Text;
                Session["YEAR"] = ddlYR.Text;
                Session["COURSE"] = ddlCrsNM.Text;
                Session["SEMESTERID"] = lblSemID.Text;
                Session["COURSEID"] = lblCrsID.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                       "OpenWindow", "window.open('/Admission/Report/CourseEnrollPrint.aspx','_newtab');", true);
            }
        }
    }
}