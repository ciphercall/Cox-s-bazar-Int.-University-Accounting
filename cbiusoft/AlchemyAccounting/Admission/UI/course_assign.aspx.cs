using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.UI
{
    public partial class course_assign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Global.BindDropDown(ddlUser, "SELECT Name NM,UserID ID FROM User_Registration where USERTP='TEACHER' ORDER BY Name");
                    Global.BindDropDown(ddlProgram, "SELECT PROGRAMNM NM,PROGRAMID ID FROM EIM_PROGRAM  ORDER BY PROGRAMNM");
                    ddlUser.Focus();
                }
            }
        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridShow();
        }
        private void GridShow()
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  ROW_NUMBER() over (order by COURSENM) as SL,* FROM (
SELECT    DISTINCT   EIM_COURSE.COURSECD,EIM_COURSE.COURSEID, EIM_COURSE.COURSENM, EIM_COURSE.CREDITHH,  EIM_COURSE.PROGRAMID,
                         CASE WHEN SEMID = '01' THEN '1ST SEMESTER' WHEN SEMID = '02' THEN '2ND SEMESTER' WHEN SEMID = '03' THEN '3RD SEMESTER' WHEN SEMID = '04' THEN '4TH SEMESTER' WHEN SEMID = '05' THEN
                          '5TH SEMESTER' WHEN SEMID = '06' THEN '6TH SEMESTER' WHEN SEMID = '07' THEN '7TH SEMESTER' WHEN SEMID = '08' THEN '8TH SEMESTER' END AS SEMID, COURSE_ASSIGN.ASSIGN, COURSE_ASSIGN.SL SLL,COURSE_ASSIGN.USERID
FROM            EIM_COURSE LEFT OUTER JOIN COURSE_ASSIGN ON EIM_COURSE.COURSEID = COURSE_ASSIGN.COURSEID WHERE EIM_COURSE.PROGRAMID='" + ddlProgram.Text + "') A", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Assign.DataSource = ds;
                gv_Assign.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Assign.DataSource = ds;
                gv_Assign.DataBind();
                int columncount = gv_Assign.Rows[0].Cells.Count;
                gv_Assign.Rows[0].Cells.Clear();
                gv_Assign.Rows[0].Cells.Add(new TableCell());
                gv_Assign.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Assign.Rows[0].Visible = false;
            }
        }
        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridShow();
        }

        protected void gv_Assign_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string COURSEID = DataBinder.Eval(e.Row.DataItem, "COURSEID").ToString();
                 string ui = ddlUser.Text;
                 string PROGRAMID = ddlProgram.Text;
                 string VAL = Global.GetData("SELECT COURSEID FROM COURSE_ASSIGN WHERE COURSEID='" + COURSEID + "' AND PROGRAMID='" + PROGRAMID + "' AND USERID='" + ui + "'");
                 if (VAL != "")
                    e.Row.BackColor = Color.LimeGreen;
                else
                    e.Row.BackColor = Color.White;
            }
        }

        protected void gv_Assign_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ddlUser.Text != "--SELECT--")
            {
                try
                {// Label lblPROGRAMID = (Label)gvCourse.Rows[e.RowIndex].FindControl("lblPROGRAMID");

                    string CHK = "";
                    Label sl = (Label)gv_Assign.Rows[e.RowIndex].FindControl("sl");
                    Label courseID = (Label)gv_Assign.Rows[e.RowIndex].FindControl("courseID");
                    string check = Global.GetData("SELECT ISNULL(USERID,'') FROM COURSE_ASSIGN WHERE USERID='" + ddlUser.Text + "' AND PROGRAMID='" + ddlProgram.Text + "' AND COURSEID='" + courseID.Text + "'");
                   if (check == "")
                    {
                        SqlConnection conn = new SqlConnection(Global.connection);
                        if (conn.State != ConnectionState.Open)conn.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO COURSE_ASSIGN (USERID,COURSEID,ASSIGN,PROGRAMID) VALUES ('" + ddlUser.Text + "','" + courseID.Text + "','T','" + ddlProgram.Text + "')", conn);
                        int result = cmd.ExecuteNonQuery();
                        if (conn.State != ConnectionState.Closed)conn.Close();
                        if (result == 1)
                        {
                            GridShow();
                        }
                    }
                    else
                    {
                        Global.Execute("DELETE FROM COURSE_ASSIGN WHERE  SL='" + sl.Text + "'");
                        GridShow();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }
            }
            else
                Response.Write("<script>alert('Select User Name')</script>");
        }

        protected void ddlUser_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ddlProgram_SelectedIndexChanged(sender,e);
        }
    }
}