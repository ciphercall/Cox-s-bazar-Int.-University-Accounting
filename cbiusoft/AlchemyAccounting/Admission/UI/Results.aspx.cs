using System;
using System.IO;
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
using AlchemyAccounting.Admission.DataAccess;
using AlchemyAccounting.Admission.DataModel;

namespace AlchemyAccounting.Admission.UI
{
    public partial class Results : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        public static DataTable dt;
        public static int i = 1;
        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection conn = new SqlConnection(Global.connection); 
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
                    string UserID = Session["UserName"].ToString();
                    //Global.dropDownAdd(ddlYR, "SELECT DISTINCT(REGYY) FROM EIM_CREGMST");
                    Global.dropDownAdd(ddlSemNM, @"SELECT     DISTINCT(EIM_SEMESTER.SEMESTERNM) FROM  EIM_COURSEREG INNER JOIN
                      EIM_SEMESTER ON EIM_COURSEREG.SEMESTERID = EIM_SEMESTER.SEMESTERID  WHERE EIM_COURSEREG.USERID='" + UserID + "'");
                    Global.dropDownAdd(ddlProgNM, @"SELECT Distinct EIM_PROGRAM.PROGRAMNM FROM EIM_COURSEREG INNER JOIN
                      EIM_PROGRAM ON EIM_COURSEREG.PROGRAMID = EIM_PROGRAM.PROGRAMID WHERE EIM_COURSEREG.USERID='" + UserID + "'");
                    //Global.dropDownAdd(ddlSemNM, @"SELECT  SEMESTERNM  FROM  EIM_SEMESTER");
                    //Global.dropDownAdd(ddlProgNM, @"SELECT  PROGRAMNM FROM  EIM_PROGRAM"); 
                    string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    int i, m;
                    int a = int.Parse(yr);
                    m = a + 5;
                    ddlYR.Items.Add("Select");
                    for (i = a - 5; i <= m; i++)
                    {
                        ddlYR.Items.Add(i.ToString());
                    }
                    //ddlYR.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    ddlYR.Focus();
                    //gridShow();
                }
            }
        }
        private void gridShow()
        {
            try
            {
                string YR = Session["YR"].ToString();
                string SemesterID = Session["SemesterID"].ToString();
                string SemID = Session["SemID"].ToString();
                string ProgID = Session["ProgID"].ToString();
                string CourseID = Session["CourseID"].ToString();
                string UserID = Session["UserName"].ToString();
                if (conn.State != ConnectionState.Open)conn.Open();
                // (CASE WHEN EIM_RESULT.SEMID='01' then '1st' when EIM_RESULT.SEMID='02' then '2nd' when EIM_RESULT.SEMID='03' then '3rd' when EIM_RESULT.SEMID='04' then '4th' 
                //when EIM_RESULT.SEMID='05' then '5th' when EIM_RESULT.SEMID='06' then '6th' when EIM_RESULT.SEMID='07' then '7th' when EIM_RESULT.SEMID='08' then '8th'
                //else '' end) AS SEM,
                SqlCommand cmd = new SqlCommand(@"SELECT   DISTINCT     EIM_COURSEREG.STUDENTID, EIM_STUDENT.NEWSTUDENTID, EIM_STUDENT.STUDENTNM, EIM_RESULT.M40, EIM_RESULT.M60, EIM_RESULT.REMARKS
FROM            COURSE_ASSIGN INNER JOIN EIM_COURSEREG ON COURSE_ASSIGN.COURSEID = EIM_COURSEREG.COURSEID AND COURSE_ASSIGN.PROGRAMID = EIM_COURSEREG.PROGRAMID INNER JOIN
                         EIM_STUDENT ON EIM_COURSEREG.STUDENTID = EIM_STUDENT.STUDENTID INNER JOIN
                         EIM_RESULT ON EIM_COURSEREG.PROGRAMID = EIM_RESULT.PROGRAMID AND EIM_COURSEREG.SEMESTERID = EIM_RESULT.SEMESTERID AND EIM_COURSEREG.REGYY = EIM_RESULT.REGYY AND 
                         EIM_COURSEREG.SEMID = EIM_RESULT.SEMID  " +
                        "WHERE EIM_RESULT.SEMID='" + SemID + "' AND EIM_RESULT.PROGRAMID='" + ProgID + "' AND  EIM_RESULT.SEMESTERID='" + SemesterID + "' " +
                        "AND EIM_RESULT.REGYY='" + YR + "' AND EIM_RESULT.COURSEID='" + CourseID + "' AND COURSE_ASSIGN.USERID='" + UserID + "'", conn);
//SqlCommand cmd = new SqlCommand(@"SELECT dbo.EIM_RESULT.STUDENTID,dbo.EIM_STUDENT.NEWSTUDENTNM, dbo.EIM_STUDENT.STUDENTNM, M40 ,  M60, dbo.EIM_RESULT.REMARKS
//               FROM dbo.EIM_RESULT INNER JOIN dbo.EIM_STUDENT ON dbo.EIM_RESULT.STUDENTID = dbo.EIM_STUDENT.STUDENTID  " +
//                                              " WHERE EIM_RESULT.SEMID='" + SemID + "' AND EIM_RESULT.PROGRAMID='" + ProgID + "' AND  EIM_RESULT.SEMESTERID='" + SemesterID + "' " +
//                                              " AND EIM_RESULT.REGYY='" + YR + "' AND EIM_RESULT.COURSEID='" + CourseID + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (conn.State != ConnectionState.Closed)conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_Result.Visible = true;
                    gv_Result.DataSource = ds;
                    gv_Result.DataBind();
                    // btnInsertToResult.Visible = false; 
                }
                else
                {
                    gv_Result.Visible = true;
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    gv_Result.DataSource = ds;
                    gv_Result.DataBind();
                    int columncount = gv_Result.Rows[0].Cells.Count;
                    gv_Result.Rows[0].Cells.Clear();
                    gv_Result.Rows[0].Cells.Add(new TableCell());
                    gv_Result.Rows[0].Cells[0].ColumnSpan = columncount;
                    gv_Result.Rows[0].Visible = false;
                    // btnInsertToResult.Visible = true;
                    btnInsertToResult.Focus();
                }
            }
            catch { }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionStudentD(string prefixText, int count, string contextKey)
        {


            string YR = HttpContext.Current.Session["YR"].ToString();
            string SemID = HttpContext.Current.Session["SemID"].ToString();
            string ProgID = HttpContext.Current.Session["ProgID"].ToString();
            string CourseID = HttpContext.Current.Session["CourseID"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlConnection con = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT STUDENTID FROM EIM_COURSEREG WHERE STUDENTID LIKE '" + prefixText + "%' AND PROGRAMID='" + ProgID + "'  AND SEMESTERID='" + SemID + "'  AND REGYY='" + YR + "' AND COURSEID='" + CourseID + "'", con);
            SqlDataReader oReader;
            if (con.State != ConnectionState.Open)con.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["STUDENTID"].ToString());
            }
            if (con.State != ConnectionState.Closed)con.Close();
            return CompletionSet.ToArray();

        }
        //[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        //public static string[] GetCompletionCOURSEID(string prefixText, int count, string contextKey)
        //{


        //    string YR = HttpContext.Current.Session["YR"].ToString();
        //    string SemID = HttpContext.Current.Session["SemID"].ToString();
        //    string ProgID = HttpContext.Current.Session["ProgID"].ToString();
        //    // Try to use parameterized inline query/sp to protect sql injection
        //    SqlConnection con = new SqlConnection(Global.connection);
        //    SqlCommand cmd = new SqlCommand("SELECT COURSECD FROM EIM_COURSE WHERE COURSECD LIKE '" + prefixText + "%' AND PROGRAMID='" + ProgID + "'", con);
        //    SqlDataReader oReader;
        //    if (con.State != ConnectionState.Open)con.Open();
        //    List<string> CompletionSet = new List<string>();
        //    oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //    while (oReader.Read())
        //    {
        //        CompletionSet.Add(oReader["COURSECD"].ToString());
        //    }
        //    return CompletionSet.ToArray();

        //}
        private void Clear()
        {
            Session["YR"] = "";
            Session["SemID"] = "";
            Session["ProgID"] = "";

        }
        protected void ddlYR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYR.Text == "Select")
                ddlYR.Focus();
            else
            {

                gv_Result.Visible = false;
                ddlSemNM.Focus();
            }
        }

        protected void ddlSemNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMSG.Text = "";
            Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
            if (ddlYR.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlSemNM.SelectedIndex = -1;
                ddlProgNM.SelectedIndex = -1;
            }
            else
                ddlProgNM.Focus();
        }

        protected void ddlProgNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYR.Text == "Select")
            {
                ddlYR.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlSemNM.SelectedIndex = -1;
            }
            else if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlProgNM.SelectedIndex = -1;

            }
            else if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
            }
            else
            {
                ddlCourseNM.Items.Clear();
                string UI=Session["UserName"].ToString();
                Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProgID);
                Global.BindDropDownNM(ddlCourseNM, @"SELECT DISTINCT EIM_COURSE.COURSENM NM FROM EIM_COURSEREG INNER JOIN
EIM_COURSE ON EIM_COURSEREG.COURSEID = EIM_COURSE.COURSEID INNER JOIN
COURSE_ASSIGN ON EIM_COURSE.COURSEID = COURSE_ASSIGN.COURSEID AND EIM_COURSE.PROGRAMID = COURSE_ASSIGN.PROGRAMID 
WHERE EIM_COURSEREG.PROGRAMID='" + lblProgID.Text + "' AND COURSE_ASSIGN.USERID='" + UI + "' AND COURSE_ASSIGN.ASSIGN='T'");
                gv_Result.Visible = false;
                ddlCourseNM.Focus();
            }
        }
        protected void ddlCourseNM_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlYR.Text == "Select")
            {
                ddlYR.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlSemNM.SelectedIndex = -1;
                ddlCourseNM.SelectedIndex = -1;
            }
            else if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlCourseNM.SelectedIndex = -1;

            }
            else if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
                ddlCourseNM.SelectedIndex = -1;
            }
            else
            {
                Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE PROGRAMID='" + lblProgID.Text + "' AND COURSENM='" + ddlCourseNM.Text + "'", lblCourseID);
                gv_Result.Visible = false;
                ddlSemID.Focus();
                //gridShow();
            }
        }
        protected void gv_Result_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                TextBox txtSTUDENTIDFooter = (TextBox)gv_Result.FooterRow.FindControl("txtSTUDENTIDFooter");
                //TextBox txtCGPAFooter = (TextBox)gv_Result.FooterRow.FindControl("txtCGPAFooter");
                //TextBox txtGRADEFooter = (TextBox)gv_Result.FooterRow.FindControl("txtGRADEFooter");
                TextBox txtREMARKSFooter = (TextBox)gv_Result.FooterRow.FindControl("txtREMARKSFooter");
                TextBox txtM40Footer = (TextBox)gv_Result.FooterRow.FindControl("txtM40Footer");
                TextBox txtM60Footer = (TextBox)gv_Result.FooterRow.FindControl("txtM60Footer");
                DropDownList ddlSemisterNMFooter = (DropDownList)gv_Result.FooterRow.FindControl("ddlSemisterNMFooter");
                iob.UserID = Session["UserName"].ToString();
                iob.Ipaddress = Session["IpAddress"].ToString();
                iob.PcName = Session["PCName"].ToString();
                iob.InTime = Global.Dayformat1(DateTime.Now);
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    if (e.CommandName.Equals("SaveCon"))
                    {
                        if (ddlSemisterNMFooter.Text == "Select")
                            ddlSemisterNMFooter.Focus();
                        else if (txtSTUDENTIDFooter.Text == "")
                        {
                            txtSTUDENTIDFooter.Focus();
                        } 
                        else
                        {
                            if (conn.State != ConnectionState.Open)conn.Open();
                            string Search = "SELECT * FROM EIM_RESULT WHERE STUDENTID='" + txtSTUDENTIDFooter.Text + "' and REGYY='" + ddlYR.Text + "' and SEMESTERID='" + lblSemID.Text + "' and PROGRAMID='" + lblProgID.Text + "' AND COURSEID='" + lblCourseID.Text + "'";
                            SqlCommand cmd = new SqlCommand(Search, conn);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (conn.State != ConnectionState.Closed)conn.Close();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblMSG.Visible = true;
                                lblMSG.Text = "Already Exist !";
                            }
                            else
                            {
                                lblMSG.Visible = false;
                                iob.YR = int.Parse(ddlYR.Text);
                                iob.SemID = int.Parse(lblSemID.Text);
                                iob.ProgID = lblProgID.Text;
                                iob.StuID = txtSTUDENTIDFooter.Text;
                                iob.CrsID = lblCourseID.Text;
                                // iob.CGPA = Decimal.Parse(txtCGPAFooter.Text);
                                // iob.GRAD = txtGRADEFooter.Text;
                                iob.SemisterID = ddlSemisterNMFooter.SelectedValue;
                                if (txtM40Footer.Text == "")
                                    txtM40Footer.Text = "0";
                                if (txtM60Footer.Text == "")
                                    txtM60Footer.Text = "0";
                                iob.M40 = Convert.ToDecimal(txtM40Footer.Text);
                                iob.M60 = Convert.ToDecimal(txtM60Footer.Text);
                                iob.Remarks = txtREMARKSFooter.Text;
                                dob.Insert_EIM_RESULT(iob);
                                gridShow();
                                txtSTUDENTIDFooter.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void gv_Result_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_Result.EditIndex = -1;
            gridShow();
        }

        protected void gv_Result_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                Session["STUDENTID"] = "";
                gv_Result.EditIndex = e.NewEditIndex;
                gridShow();
                //Label lblSEMID = (Label)gv_Result.Rows[e.NewEditIndex].FindControl("lblSEMID");
                DropDownList ddlSemisterNMEdit = (DropDownList)gv_Result.Rows[e.NewEditIndex].FindControl("ddlSemisterNMEdit");

                //ddlSemisterNMEdit.SelectedItem.Text = lblSEMID.Text;
                TextBox txtM40Edit = (TextBox)gv_Result.Rows[e.NewEditIndex].FindControl("txtM40Edit");
                TextBox txtSTUDENTIDEdit = (TextBox)gv_Result.Rows[e.NewEditIndex].FindControl("txtSTUDENTIDEdit");
                Session["STUDENTID"] = txtSTUDENTIDEdit.Text;
                txtM40Edit.Focus();
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void gv_Result_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtSTUDENTIDEdit = (TextBox)gv_Result.Rows[e.RowIndex].FindControl("txtSTUDENTIDEdit");
                // TextBox txtCGPAEdit = (TextBox)gv_Result.Rows[e.RowIndex].FindControl("txtCGPAEdit");
                //TextBox txtGRADEEdit = (TextBox)gv_Result.Rows[e.RowIndex].FindControl("txtGRADEEdit");
                TextBox txtM40Edit = (TextBox)gv_Result.Rows[e.RowIndex].FindControl("txtM40Edit");
                DropDownList ddlSemisterNMEdit = (DropDownList)gv_Result.Rows[e.RowIndex].FindControl("ddlSemisterNMEdit");
                TextBox txtM60Edit = (TextBox)gv_Result.Rows[e.RowIndex].FindControl("txtM60Edit");
                TextBox txtREMARKSEdit = (TextBox)gv_Result.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                string YR = Session["YR"].ToString();
                string SemesterID = Session["SemesterID"].ToString();
                string ProgID = Session["ProgID"].ToString();
                iob.UPDUserID = Session["UserName"].ToString();
                iob.UPDIpaddress = Session["IpAddress"].ToString();
                iob.UPDPcName = Session["PCName"].ToString();
                iob.UPDTime = Global.Dayformat1(DateTime.Now);
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    //if (ddlSemID.Text == "Select")
                    //    ddlSemID.Focus();
                    //else if (txtSTUDENTIDEdit.Text == "")
                    //{
                    // txtSTUDENTIDEdit.Focus();
                    //}
                    //else if (txtCGPAEdit.Text == "")
                    //{
                    //    txtCGPAEdit.Focus();
                    //}
                    //else if (txtGRADEEdit.Text == "")
                    //{
                    //    txtGRADEEdit.Focus();
                    //}
                    //else
                    //{
                    lblMSG.Visible = false;
                    iob.YR = int.Parse(ddlYR.Text);
                    iob.SemID = int.Parse(SemesterID);
                    iob.ProgID = ProgID;
                    iob.StuID = txtSTUDENTIDEdit.Text;
                    iob.CrsID = lblCourseID.Text;
                    // iob.CGPA = Decimal.Parse(txtCGPAEdit.Text);
                    //iob.GRAD = txtGRADEEdit.Text;
                    iob.SemisterID = ddlSemID.SelectedValue;
                    if (txtM40Edit.Text == "")
                        txtM40Edit.Text = "0";
                    if (txtM60Edit.Text == "")
                        txtM60Edit.Text = "0";
                    iob.M40 = Convert.ToDecimal(txtM40Edit.Text);
                    iob.M60 = Convert.ToDecimal(txtM60Edit.Text);
                    iob.Remarks = txtREMARKSEdit.Text;
                    dob.Update_EIM_RESULT(iob);
                    gv_Result.EditIndex = -1;
                    gridShow();


                    //}
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void txtSTUDENTIDFooter_TextChanged(object sender, EventArgs e)
        {
            GridViewRow Row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtSTUDENTIDFooter = (TextBox)Row.FindControl("txtSTUDENTIDFooter");
            TextBox txtSTUDENTNMFooter = (TextBox)Row.FindControl("txtSTUDENTNMFooter");
            TextBox txtM40Footer = (TextBox)Row.FindControl("txtM40Footer");
            if (txtSTUDENTIDFooter.Text == "")
                txtSTUDENTIDFooter.Focus();
            else
            {
                txtSTUDENTNMFooter.Text = "";
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE STUDENTID='" + txtSTUDENTIDFooter.Text + "'", txtSTUDENTNMFooter);
                txtM40Footer.Focus();
            }

        }
        protected void txtSTUDENTIDEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow Row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtSTUDENTIDEdit = (TextBox)Row.FindControl("txtSTUDENTIDEdit");
            TextBox txtSTUDENTNMEdit = (TextBox)Row.FindControl("txtSTUDENTNMEdit");
            TextBox txtM40Edit = (TextBox)Row.FindControl("txtM40Edit");
            if (txtSTUDENTIDEdit.Text == "")
                txtSTUDENTIDEdit.Focus();
            else
            {
                txtSTUDENTIDEdit.Text = "";
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE STUDENTID='" + txtSTUDENTIDEdit.Text + "'", txtSTUDENTIDEdit);
                txtM40Edit.Focus();
            }
        }

        //protected void txtCOURSEIDFooter_TextChanged(object sender, EventArgs e)
        //{
        //    string SemID = Session["SemID"].ToString();
        //    string ProgID = Session["ProgID"].ToString();
        //    GridViewRow Row = ((GridViewRow)((TextBox)sender).NamingContainer);
        //    TextBox txtCOURSEIDFooter = (TextBox)Row.FindControl("txtCOURSEIDFooter");
        //    TextBox txtCREDITHHFooter = (TextBox)Row.FindControl("txtCREDITHHFooter");
        //    TextBox txtCGPAFooter = (TextBox)Row.FindControl("txtCGPAFooter");
        //    if (txtCOURSEIDFooter.Text == "")
        //        txtCOURSEIDFooter.Focus();
        //    else
        //    {
        //        Label lblCourseID = new Label();
        //        lblCourseID.Text = "";
        //        Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE COURSECD='" + txtCOURSEIDFooter.Text + "'", lblCourseID);
        //        Global.txtAdd("SELECT CREDITHH FROM EIM_COURSE WHERE PROGRAMID='" + ProgID + "' and COURSEID='" + lblCourseID.Text + "'", txtCREDITHHFooter);
        //        txtCGPAFooter.Focus();
        //    }
        //}

        //protected void txtCOURSEIDEdit_TextChanged(object sender, EventArgs e)
        //{
        //    string SemID = Session["SemID"].ToString();
        //    string ProgID = Session["ProgID"].ToString();
        //    GridViewRow Row = ((GridViewRow)((TextBox)sender).NamingContainer);
        //    TextBox txtCOURSEIDEdit = (TextBox)Row.FindControl("txtCOURSEIDEdit");
        //    TextBox txtCREDITHHEdit = (TextBox)Row.FindControl("txtCREDITHHEdit");
        //    TextBox txtCGPAEdit = (TextBox)Row.FindControl("txtCGPAEdit");
        //    if (txtCOURSEIDEdit.Text == "")
        //        txtCOURSEIDEdit.Focus();
        //    else
        //    {
        //        Label lblCourseID = new Label();
        //        lblCourseID.Text = "";
        //        Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE COURSECD='" + txtCOURSEIDEdit.Text + "'", lblCourseID);
        //        Global.txtAdd("SELECT CREDITHH FROM EIM_COURSE WHERE PROGRAMID='" + ProgID + "' and COURSEID='" + lblCourseID.Text + "'", txtCREDITHHEdit);
        //        txtCGPAEdit.Focus();
        //    }
        //}

        protected void gv_Result_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                //Label lblCOURSEID = (Label)gv_Result.Rows[e.RowIndex].FindControl("lblCOURSEID");
                Label lblSTUDENTID = (Label)gv_Result.Rows[e.RowIndex].FindControl("lblSTUDENTID");
                //Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE COURSECD='" + lblCOURSEID.Text + "'", lblCourseID);
                if (conn.State != ConnectionState.Open)conn.Open();
                SqlCommand cmd = new SqlCommand(" DELETE FROM EIM_RESULT WHERE STUDENTID='" + lblSTUDENTID.Text + "' AND REGYY='" + ddlYR.Text + "' AND SEMESTERID='" + lblSemID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND COURSEID='" + lblCourseID.Text + "'", conn);
                cmd.ExecuteNonQuery();
                if (conn.State != ConnectionState.Closed)conn.Close();
                gridShow();
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["YR"] = ddlYR.Text;
            Session["ProgID"] = lblProgID.Text;
            Session["SemesterID"] = lblSemID.Text;
            Session["SemID"] = ddlSemID.SelectedValue;
            Session["CourseID"] = lblCourseID.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                     "OpenWindow", "window.open('/Admission/Report/UMS Reports/ResultCreate.aspx','_newtab');", true);
        }

        protected void btnInsertToResult_Click(object sender, EventArgs e)
        {
            if (ddlYR.Text == "Select")
            {
                ddlYR.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlSemNM.SelectedIndex = -1;
                ddlCourseNM.SelectedIndex = -1;
            }
            else if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlCourseNM.SelectedIndex = -1;

            }
            else if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
                ddlCourseNM.SelectedIndex = -1;
            }
            else if (ddlSemID.Text == "Select")
            {
                ddlSemID.Focus();
            }
            else
            {
                iob.UserID = Session["UserName"].ToString();
                iob.Ipaddress = Session["IpAddress"].ToString();
                iob.PcName = Session["PCName"].ToString();
                iob.InTime = Global.Dayformat1(DateTime.Now);
                SqlConnection con = new SqlConnection(Global.connection);
                //if (con.State != ConnectionState.Open)con.Open();
                //string DeleteOldData = "Delete From EIM_RESULT WHERE SEMID='" + ddlSemID.SelectedValue + "' AND REGYY='" + ddlYR.Text + "' AND SEMESTERID='" + lblSemID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND COURSEID='" + lblCourseID.Text + "' AND M40='' AND M60=''";
                //SqlCommand cmd = new SqlCommand(DeleteOldData,con);
                //cmd.ExecuteNonQuery();
                //if (con.State != ConnectionState.Closed)con.Close();
                Global.gridViewAdd(GridView1, "SELECT DISTINCT STUDENTID FROM EIM_COURSEREG WHERE SEMID='" + ddlSemID.SelectedValue + "' AND REGYY='" + ddlYR.Text + "' AND SEMESTERID='" + lblSemID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND COURSEID='" + lblCourseID.Text + "'");
                try
                {
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        Label lblCheck = new Label();
                        Global.lblAdd(@"SELECT * FROM EIM_RESULT WHERE STUDENTID='" + row.Cells[0].Text + "' AND  " +
                        "REGYY='" + ddlYR.Text + "' AND SEMESTERID='" + lblSemID.Text + "' AND    " +
                        "PROGRAMID='" + lblProgID.Text + "' AND COURSEID='" + lblCourseID.Text + "' AND SEMID='" + ddlSemID.SelectedValue + "'", lblCheck);
                        if (lblCheck.Text == "")
                        {
                            if (con.State != ConnectionState.Open)
                                if (con.State != ConnectionState.Open)con.Open();
                            SqlCommand cmd1 = new SqlCommand(@"INSERT INTO EIM_RESULT (REGYY, SEMESTERID, PROGRAMID, STUDENTID, COURSEID, SEMID,USERID,USERPC,IPADDRESS,INTIME) VALUES 
                                                       (@REGYY, @SEMESTERID, @PROGRAMID, @STUDENTID, @COURSEID,@SEMID,@USERID,@USERPC,@IPADDRESS,@INTIME)", con);

                            cmd1.Parameters.Clear();
                            cmd1.Parameters.Add("@REGYY", SqlDbType.Int).Value = Convert.ToInt16(ddlYR.Text);
                            cmd1.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = Convert.ToInt16(lblSemID.Text);
                            cmd1.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = lblProgID.Text;
                            cmd1.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = row.Cells[0].Text;
                            cmd1.Parameters.Add("@COURSEID", SqlDbType.NVarChar).Value = lblCourseID.Text;
                            cmd1.Parameters.Add("@SEMID", SqlDbType.NVarChar).Value = ddlSemID.SelectedValue;
                            cmd1.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = iob.UserID;
                            cmd1.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = iob.PcName;
                            cmd1.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = iob.Ipaddress;
                            cmd1.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = iob.InTime;
                            cmd1.ExecuteNonQuery();
                            if (con.State != ConnectionState.Closed)
                                if (con.State != ConnectionState.Closed)con.Close();
                        }
                    }
                }
                catch { }
                gridShow();
            }
        }

        protected void ddlSemID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYR.Text == "Select")
            {
                ddlYR.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlSemNM.SelectedIndex = -1;
                ddlCourseNM.SelectedIndex = -1;
            }
            else if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlCourseNM.SelectedIndex = -1;

            }
            else if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
                ddlCourseNM.SelectedIndex = -1;
            }
            else if (ddlSemID.Text == "Select")
            {
                ddlSemID.Focus();
            }
            else
            {
                Session["SemID"] = "";
                Session["SemID"] = ddlSemID.SelectedValue.ToString();
                gv_Result.Visible = false;
                btnSearch.Focus();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlYR.Text == "Select")
            {
                ddlYR.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlSemNM.SelectedIndex = -1;
                ddlCourseNM.SelectedIndex = -1;
            }
            else if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
                ddlProgNM.SelectedIndex = -1;
                ddlCourseNM.SelectedIndex = -1;

            }
            else if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
                ddlCourseNM.SelectedIndex = -1;
            }
            else if (ddlSemID.Text == "Select")
            {
                ddlSemID.Focus();
            }
            else
            {
                Session["YR"] = "";
                Session["ProgID"] = "";
                Session["SemesterID"] = "";
                Session["SemID"] = "";
                Session["CourseID"] = "";

                Session["YR"] = ddlYR.Text;
                Session["ProgID"] = lblProgID.Text;
                Session["SemesterID"] = lblSemID.Text;
                Session["SemID"] = ddlSemID.SelectedValue;
                Session["CourseID"] = lblCourseID.Text;
                gridShow();
            }
        }
    }
}
