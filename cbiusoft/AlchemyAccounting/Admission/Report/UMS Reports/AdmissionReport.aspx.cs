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
    public partial class AdmissionReport : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Global.dropDownAdd(ddlYR, "SELECT DISTINCT(TESTYY) FROM EIM_ADMISSION");
                Global.dropDownAdd(ddlSemNM, @"SELECT SEMESTERNM FROM  EIM_SEMESTER");
                Global.dropDownAdd(ddlProgNM, @"SELECT PROGRAMNM FROM EIM_PROGRAM");
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
                gv_Admission.Visible = false;
                ddlSemNM.Focus();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }
        private void gridShow()
        {

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT     ROLLNO, STUDENTNM, MOBNO, convert(nvarchar(10),MRDT,103) AS MRDT , MRNO, MRAMT
            FROM EIM_ADMISSION WHERE TESTYY='" + ddlYR.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND SEMESTERID='" + lblSemID.Text + "' ORDER BY ROLLNO", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Admission.DataSource = ds;
                gv_Admission.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Admission.DataSource = ds;
                gv_Admission.DataBind();
                int columncount = gv_Admission.Rows[0].Cells.Count;
                gv_Admission.Rows[0].Cells.Clear();
                gv_Admission.Rows[0].Cells.Add(new TableCell());
                gv_Admission.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Admission.Rows[0].Visible = false;
            }
        }
        protected void ddlSemNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                gv_Admission.Visible = false;
            }
            else
            {
                Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM ='" + ddlSemNM.Text + "'", lblSemID);
                ddlYR.Focus();
            }
        }

        protected void ddlProgNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlYR.SelectedIndex = -1;
                gv_Admission.Visible = false;
            }
            else if (ddlYR.Text == "Select")
            {
                ddlYR.Focus();
                ddlProgNM.SelectedIndex = -1;
                gv_Admission.Visible = false;
            }
            else if (ddlProgNM.Text == "Select")
            {
                gv_Admission.Visible = false;
            }
            else
            {
                Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM ='" + ddlProgNM.Text + "'", lblProgID);
                gv_Admission.Visible = true;
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
                gv_Admission.Visible = false;
            }
            else if (ddlYR.Text == "Select")
            {
                gv_Admission.Visible = false;
                ddlYR.Focus();
            }
            else
            {
                ddlProgNM.Focus();
            }
        }

        protected void gv_CourseProgram_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string ROLLNO = DataBinder.Eval(e.Row.DataItem, "ROLLNO").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ROLLNO;
                string STUDENTNM = DataBinder.Eval(e.Row.DataItem, "STUDENTNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + STUDENTNM;
                string MOBNO = DataBinder.Eval(e.Row.DataItem, "MOBNO").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + MOBNO;
                string MRDT = DataBinder.Eval(e.Row.DataItem, "MRDT").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + MRDT;
                string MRNO = DataBinder.Eval(e.Row.DataItem, "MRNO").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + MRNO;
                string MRAMT = DataBinder.Eval(e.Row.DataItem, "MRAMT").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + MRAMT;


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

                Session["SEMESTERNM"] = ddlSemNM.Text;
                Session["YEAR"] = ddlYR.Text;
                Session["PROGRAMNM"] = ddlProgNM.Text;
                Session["SEMESTERID"] = lblSemID.Text;
                Session["PROGRAMID"] = lblProgID.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                       "OpenWindow", "window.open('/Admission/Report/AdmissionReportPrint.aspx','_newtab');", true);
            }
        }
    }
}