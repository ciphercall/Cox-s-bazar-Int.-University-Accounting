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
using System.Drawing;

namespace AlchemyAccounting.Admission.UI
{
    public partial class Course_reg : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        public static DataTable dt;
        public static int i = 1;
        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection con = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Global.dropDownAdd(ddlSemNM, @"SELECT SEMESTERNM FROM EIM_SEMESTER");
                    Global.dropDownAdd(ddlProgNM, @"SELECT PROGRAMNM FROM EIM_PROGRAM");
                    string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    int i, m;
                    int a = int.Parse(yr);
                    m = a + 5;
                    ddlYr.Items.Add("Select");
                    for (i = a - 5; i <= m; i++)
                    { 
                        ddlYr.Items.Add(i.ToString());
                    }
                    ddlYr.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    txtStuIDNew.Focus();
                    txtDate.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                }
                Session["YR"] = ddlYr.Text;
            }

        }
        private void GridShow()
        {
            try
            {
                string StuID = "";
                if (btnEdit.Text == "Edit")
                    StuID = txtStuID.Text;
                else
                    StuID = ddlStudentEdit.SelectedValue.ToString();
                if (con.State != ConnectionState.Open)con.Open();
                string ui=Session["UserName"].ToString();
//                SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT (CASE WHEN EIM_COURSEREG.SEMID = '00' THEN '' WHEN EIM_COURSEREG.SEMID = '01' THEN '1st' WHEN EIM_COURSEREG.SEMID = '02' THEN '2nd' WHEN EIM_COURSEREG.SEMID = '03' THEN '3rd' WHEN
//                          EIM_COURSEREG.SEMID = '04' THEN '4th' WHEN EIM_COURSEREG.SEMID = '05' THEN '5th' WHEN EIM_COURSEREG.SEMID = '06' THEN '6th' WHEN EIM_COURSEREG.SEMID = '07' THEN '7th' WHEN EIM_COURSEREG.SEMID
//                          = '08' THEN '8th' ELSE '' END) AS SEM, EIM_COURSE.COURSECD, EIM_COURSE.COURSENM, EIM_COURSEREG.COURSEID, EIM_COURSEREG.CREDITHH, 
//                         CASE WHEN EIM_COURSEREG.REMARKS = '&nbsp;' THEN '' ELSE EIM_COURSEREG.REMARKS END AS REMARKS, EIM_COURSEREG.CRCOST
//FROM            EIM_COURSEREG INNER JOIN EIM_COURSE ON EIM_COURSEREG.COURSEID = EIM_COURSE.COURSEID INNER JOIN
//COURSE_ASSIGN ON EIM_COURSE.COURSEID = COURSE_ASSIGN.COURSEID AND EIM_COURSE.PROGRAMID = COURSE_ASSIGN.PROGRAMID WHERE EIM_COURSEREG.STUDENTID='" + StuID + "' AND EIM_COURSEREG.PROGRAMID='" + lblProgID.Text + "' AND COURSE_ASSIGN.ASSIGN='T' AND COURSE_ASSIGN.USERID='"+ui+"'", con);
                SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT 
                         (CASE WHEN EIM_COURSEREG.SEMID = '00' THEN '' WHEN EIM_COURSEREG.SEMID = '01' THEN '1st' WHEN EIM_COURSEREG.SEMID = '02' THEN '2nd' WHEN EIM_COURSEREG.SEMID = '03' THEN '3rd' WHEN EIM_COURSEREG.SEMID
                          = '04' THEN '4th' WHEN EIM_COURSEREG.SEMID = '05' THEN '5th' WHEN EIM_COURSEREG.SEMID = '06' THEN '6th' WHEN EIM_COURSEREG.SEMID = '07' THEN '7th' WHEN EIM_COURSEREG.SEMID = '08' THEN '8th' ELSE
                          '' END) AS SEM, dbo.EIM_COURSE.COURSECD, dbo.EIM_COURSE.COURSENM, dbo.EIM_COURSEREG.COURSEID, dbo.EIM_COURSEREG.CREDITHH, 
                         CASE WHEN EIM_COURSEREG.REMARKS = '&nbsp;' THEN '' ELSE EIM_COURSEREG.REMARKS END AS REMARKS, dbo.EIM_COURSEREG.CRCOST
FROM            dbo.EIM_COURSEREG INNER JOIN
                         dbo.EIM_COURSE ON dbo.EIM_COURSEREG.COURSEID = dbo.EIM_COURSE.COURSEID
WHERE        (dbo.EIM_COURSEREG.STUDENTID = '" + StuID + "') AND (dbo.EIM_COURSEREG.PROGRAMID = '" + lblProgID.Text + "')", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (con.State != ConnectionState.Closed)con.Close();
                string UI=Session["UserName"].ToString();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_CourseReg.Visible = true;
                    gv_CourseReg.DataSource = ds;
                    gv_CourseReg.DataBind();
                    btnPrintEdit.Visible = true;
                    DropDownList ddlSemIDFooter = (DropDownList)gv_CourseReg.FooterRow.FindControl("ddlSemIDFooter");
                    DropDownList ddlCourseFooter = (DropDownList)gv_CourseReg.FooterRow.FindControl("ddlCourseFooter");
                    Global.BindDropDownNM(ddlCourseFooter, @"SELECT COURSECD NM
FROM  EIM_COURSE WHERE  PROGRAMID='" + lblProgID.Text + "'");
                    ddlSemIDFooter.Focus();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    gv_CourseReg.DataSource = ds;
                    gv_CourseReg.DataBind();
                    int columncount = gv_CourseReg.Rows[0].Cells.Count;
                    gv_CourseReg.Rows[0].Cells.Clear();
                    gv_CourseReg.Rows[0].Cells.Add(new TableCell());
                    gv_CourseReg.Rows[0].Cells[0].ColumnSpan = columncount;
                    gv_CourseReg.Rows[0].Visible = false;
                    btnPrintEdit.Visible = false;
                    DropDownList ddlSemIDFooter = (DropDownList)gv_CourseReg.FooterRow.FindControl("ddlSemIDFooter");
                    DropDownList ddlCourseFooter = (DropDownList)gv_CourseReg.FooterRow.FindControl("ddlCourseFooter");
                    Global.BindDropDownNM(ddlCourseFooter, @"SELECT COURSECD NM
FROM  EIM_COURSE WHERE  PROGRAMID='" + lblProgID.Text + "'");
                    ddlSemIDFooter.Focus();
                }
            }
            catch  
            {

            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionCourseID(string prefixText, int count, string contextKey)
        {

            SqlConnection conn = new SqlConnection(Global.connection);
            string ProgID = HttpContext.Current.Session["ProgID"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT COURSECD FROM EIM_COURSE WHERE COURSECD LIKE '" + prefixText + "%' AND PROGRAMID='" + ProgID + "'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["COURSECD"].ToString());
            }
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionStudentIDEdit(string prefixText, int count, string contextKey)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT EIM_STUDENT.NEWSTUDENTID FROM  dbo.EIM_STUDENT INNER JOIN
dbo.EIM_CREGMST ON dbo.EIM_STUDENT.STUDENTID = dbo.EIM_CREGMST.STUDENTID WHERE EIM_STUDENT.NEWSTUDENTID LIKE '" + prefixText + "%' ", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["NEWSTUDENTID"].ToString());
            }
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionStudentID(string prefixText, int count, string contextKey)
        {

            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT NEWSTUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID LIKE '" + prefixText + "%' ", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
            {
                CompletionSet.Add(oReader["NEWSTUDENTID"].ToString());
            }
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }

        protected void ddlProgNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgNM.Text != "Select")
            {
                Session["ProgID"] = "";
                lblMSG.Text = "";
                lblWrng.Text = "";
                lblProgID.Text = "";
                Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProgID);
                Session["ProgID"] = lblProgID.Text;
                Global.dropDownAdd(ddlStudentEdit, @"SELECT EIM_STUDENT.STUDENTID ID,EIM_STUDENT.NEWSTUDENTID NM FROM  dbo.EIM_STUDENT INNER JOIN
dbo.EIM_CREGMST ON dbo.EIM_STUDENT.STUDENTID = dbo.EIM_CREGMST.STUDENTID WHERE EIM_CREGMST.PROGRAMID='" + lblProgID.Text + "' AND EIM_CREGMST.REGYY='" + ddlYr.Text + "' AND EIM_CREGMST.SEMESTERID='" + lblSemID.Text + "' ORDER BY STUDENTID");
                if (btnEdit.Text == "Edit")
                    txtStuIDNew.Focus();
                else
                    ddlStudentEdit.Focus();
            }
            else
            {
                ddlProgNM.Focus();

            }
        }
        protected void ddlYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYr.Text != "Select")
            {
                Session["YR"] = ""; 
                Session["YR"] = ddlYr.Text;
                lblMSG.Text = "";
                ddlSemNM.Focus();
            }
            else
                ddlYr.Focus();
        }

        protected void ddlSemNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemNM.Text != "Select")
            {
                Session["SemID"] = "";
                Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
                Session["SemID"] = lblSemID.Text;
                ddlProgNM.Focus();
            }
            else
            {
                ddlSemNM.Focus();
            }
        }

        protected void txtStuID_TextChanged(object sender, EventArgs e)
        {
            ddlSemNM.SelectedIndex = -1;
            //ddlYr.SelectedIndex = -1;
            ddlProgNM.SelectedIndex = -1;
            lblSemID.Text = "";
            lblProgID.Text = "";
            txtSSN.Text = "";
            txtBtch.Text = "";
            if (txtStuID.Text == "")
            {
                txtStuIDNew.Focus();
            }
            else
            {

                lblStuNM.Text = "";
                SqlConnection conn = new SqlConnection(Global.connection);
                if (conn.State != ConnectionState.Open)conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT EIM_STUDENT.PROGRAMID, EIM_STUDENT.BATCH, EIM_PROGRAM.PROGRAMNM, EIM_STUDENT.SEMESTERID, EIM_SEMESTER.SEMESTERNM
                FROM EIM_STUDENT INNER JOIN EIM_PROGRAM ON EIM_STUDENT.PROGRAMID = EIM_PROGRAM.PROGRAMID INNER JOIN
                         EIM_SEMESTER ON EIM_STUDENT.SEMESTERID = EIM_SEMESTER.SEMESTERID WHERE EIM_STUDENT.STUDENTID='" + txtStuID.Text + "'", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                   // ddlYr.Text = txtStuID.Text.Substring(0, 4);
                    lblProgID.Text = dr["PROGRAMID"].ToString();
                    ddlProgNM.Text = dr["PROGRAMNM"].ToString();
                    lblSemID.Text = dr["SEMESTERID"].ToString();
                    if (lblSemID.Text == "1")
                    {
                        txtSSN.Text = "Jan'" + ddlYr.Text.Substring(2, 2) + ""; 
                    }
                    else
                    {
                        txtSSN.Text = "July'" + ddlYr.Text.Substring(2, 2) + "";
                       
                    }
                    txtBtch.Text = dr["BATCH"].ToString();
                    //ddlSemNM.Text = dr["SEMESTERNM"].ToString();
                    Global.lblAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE STUDENTID='" + txtStuID.Text + "'", lblStuNM);
                }
                dr.Close();
                if (conn.State != ConnectionState.Closed)conn.Close();
                GridShow();
                ddlYr.Focus();
            }
        }
        protected void txtStuIDNew_TextChanged(object sender, EventArgs e)
        {
            if (txtStuIDNew.Text == "") 
                txtStuIDNew.Focus(); 
            else
            {
                txtStuID.Text = Global.GetData("SELECT STUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtStuIDNew.Text + "'");
                txtStuID_TextChanged(sender, e); 
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Session["REGYR"] = ddlYr.Text;
                Session["DATE"] = txtDate.Text;
                Session["SEMESTERNM"] = ddlSemNM.Text;
                Session["PROHRAMNM"] = ddlProgNM.Text;
                if (btnEdit.Text == "Edit")
                    Session["STUDENTID"] = txtStuID.Text;
                else
                    Session["STUDENTID"] = ddlStudentEdit.SelectedValue.ToString();
                Session["BATCH"] = txtBtch.Text;
                Session["SESSION"] = txtSSN.Text;
                Session["SEMESTERID"] = lblSemID.Text;
                Session["PROHRAMID"] = lblProgID.Text;
                if (ddlYr.Text == "Select") 
                    ddlYr.Focus(); 
                else if (ddlSemNM.Text == "Select") 
                    ddlSemNM.Focus(); 
                else if (ddlProgNM.Text == "Select") 
                    ddlProgNM.Focus(); 
                else if (txtStuID.Text == "") 
                    txtStuIDNew.Focus(); 
                else if (txtStuIDNew.Text == "") 
                    txtStuIDNew.Focus(); 
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                                "OpenWindow", "window.open('/Admission/Report/CourseRegPrint.aspx','_newtab');", true);
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }

        }
        private void DataClear()
        {
            ddlSemNM.SelectedIndex = -1;
            //ddlYr.SelectedIndex = -1;
            ddlProgNM.SelectedIndex = -1;
            ddlStudentEdit.SelectedIndex = -1;
            lblStuNM.Text = "";
            lblSemID.Text = "";
            lblProgID.Text = "";
            txtStuID.Text = ""; txtStuIDNew.Text = "";
            txtSSN.Text = "";
            txtBtch.Text = "";
            GridShow();
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            lblStuNM.Text = "";
            if (btnEdit.Text == "Edit")
            {
                btnEdit.Text = "New";
                txtStuIDNew.Visible = false;
                ddlStudentEdit.Visible = true;
                DataClear();
                Global.BindDropDown(ddlStudentEdit, @"SELECT DISTINCT EIM_STUDENT.STUDENTID ID,EIM_STUDENT.NEWSTUDENTID NM FROM  dbo.EIM_STUDENT INNER JOIN
dbo.EIM_CREGMST ON dbo.EIM_STUDENT.STUDENTID = dbo.EIM_CREGMST.STUDENTID ORDER BY EIM_STUDENT.NEWSTUDENTID");
                ddlYr.Focus();
            }
            else
            {
                btnEdit.Text = "Edit";
                txtStuIDNew.Visible = true;
                ddlStudentEdit.Visible = false;
                DataClear();
                txtStuIDNew.Focus();
            }

        }


        protected void btnPrintEdit_Click(object sender, EventArgs e)
        {
            if (ddlYr.Text == "Select") 
                ddlYr.Focus(); 
            else if (ddlSemNM.Text == "Select") 
                ddlSemNM.Focus(); 
            else if (ddlProgNM.Text == "Select") 
                ddlProgNM.Focus(); 
            else if (txtStuID.Text == "") 
                txtStuIDNew.Focus(); 
            else if (txtStuIDNew.Text == "") 
                txtStuIDNew.Focus(); 
            else
            {
                Session["REGYR"] = "";
                Session["DATE"] = "";
                Session["SEMESTERNM"] = "";
                Session["PROHRAMNM"] = "";
                Session["STUDENTID"] = "";
                Session["BATCH"] = "";
                Session["SESSION"] = "";
                Session["SEMESTERID"] = "";
                Session["PROHRAMID"] = "";

                Session["REGYR"] = ddlYr.Text;
                Session["DATE"] = txtDate.Text;
                Session["SEMESTERNM"] = ddlSemNM.Text;
                Session["PROHRAMNM"] = ddlProgNM.Text;
                if (btnEdit.Text == "Edit")
                    Session["STUDENTID"] = txtStuID.Text;
                else
                    Session["STUDENTID"] = ddlStudentEdit.SelectedValue.ToString();
                Session["BATCH"] = txtBtch.Text;
                Session["SESSION"] = txtSSN.Text;
                Session["SEMESTERID"] = lblSemID.Text;
                Session["PROHRAMID"] = lblProgID.Text;


                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                            "OpenWindow", "window.open('/Admission/Report/CourseRegPrint.aspx','_newtab');", true);
            }


        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_CourseReg.EditIndex = -1;
            GridShow();
        }
        protected void gv_CourseReg_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_CourseReg.EditIndex = -1;
            GridShow();
        }
        private String SemID_8(string Value)
        {
            string SemID = "";
            if (Value == "1st")
                SemID = "01";
            else if (Value == "2nd")
                SemID = "02";
            else if (Value == "3rd")
                SemID = "03";
            else if (Value == "4th")
                SemID = "04";
            else if (Value == "5th")
                SemID = "05";
            else if (Value == "6th")
                SemID = "06";
            else if (Value == "7th")
                SemID = "07";
            else if (Value == "8th")
                SemID = "08";
            return SemID;
        }
        protected void gv_CourseReg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                iob.UserID = Session["UserName"].ToString();
                iob.Ipaddress = Session["IpAddress"].ToString();
                iob.PcName = Session["PCName"].ToString();
                iob.InTime = Global.Dayformat1(DateTime.Now);
                DropDownList ddlSemIDFooter = (DropDownList)gv_CourseReg.FooterRow.FindControl("ddlSemIDFooter");
                DropDownList ddlCourseFooter = (DropDownList)gv_CourseReg.FooterRow.FindControl("ddlCourseFooter");
                Label lblCourseIDFooter = (Label)gv_CourseReg.FooterRow.FindControl("lblCourseIDFooter");
                //TextBox txtVatFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtVatFooter");
                TextBox txtCourseHrFooter = (TextBox)gv_CourseReg.FooterRow.FindControl("txtCourseHrFooter");
                TextBox txtCrCostFooter = (TextBox)gv_CourseReg.FooterRow.FindControl("txtCrCostFooter");
                TextBox txtRemarksFooter = (TextBox)gv_CourseReg.FooterRow.FindControl("txtRemarksFooter");
                lblMSG.Text = "";
                lblMSGCourse.Text = "";
                if (ddlYr.Text == "Select") 
                    ddlYr.Focus(); 
                else if (lblSemID.Text == "") 
                    ddlSemNM.Focus(); 
                else if (ddlProgNM.Text == "Select") 
                    ddlProgNM.Focus(); 
                else if (txtStuID.Text == "") 
                    txtStuIDNew.Focus();  
                else if (txtStuIDNew.Text == "") 
                    txtStuIDNew.Focus();
                else if (ddlSemIDFooter.Text == "00" || ddlSemIDFooter.Text == "" || ddlSemIDFooter.SelectedValue == "00") 
                    ddlSemIDFooter.Focus(); 
                else if (ddlCourseFooter.Text == "Select") 
                    ddlCourseFooter.Focus(); 
                else
                {
                    string Date = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal).ToString("yyyy-MM-dd");
                    iob.EnRLDT = Convert.ToDateTime(Date);
                    iob.CrsYR = int.Parse(ddlYr.Text);
                    iob.SemID = int.Parse(lblSemID.Text);
                    iob.ProgID = lblProgID.Text;
                    iob.SeSN = txtSSN.Text;
                    iob.StuID = txtStuID.Text;
                    iob.Batch = txtBtch.Text;
                    string StuID = "";
                    if (btnEdit.Text == "Edit")
                        StuID = txtStuID.Text;
                    else
                        StuID = ddlStudentEdit.SelectedValue.ToString();
                    iob.Remarks = txtRmrk.Text;
                    Label lblCheck = new Label();
                    Global.lblAdd("SELECT PROGRAMID FROM EIM_CREGMST WHERE STUDENTID='" + txtStuID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND SEMESTERID='" + lblSemID.Text + "' AND REGYY='" + ddlYr.Text + "'", lblCheck);
                    if (lblCheck.Text == "")
                        dob.Insert_EIM_CREGMST(iob);
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    Label lblCorsID = new Label();
                    //string SemID = SemID_8(ddlSemIDFooter.Text);

                    SqlCommand cmd1 = new SqlCommand(@"INSERT INTO EIM_COURSEREG (REGYY,SEMID,SEMESTERID,PROGRAMID,STUDENTID,ENRLDT,COURSEID,
                                             CREDITHH,CRCOST,REMARKS,USERID,USERPC,IPADDRESS,INTIME) VALUES 
                                                       (@REGYY,@SEMID,@SEMESTERID,@PROGRAMID,@STUDENTID,@ENRLDT,@COURSEID,
                                             @CREDITHH,@CRCOST,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)", con);

                    cmd1.Parameters.AddWithValue("@REGYY", int.Parse(ddlYr.Text));
                    cmd1.Parameters.AddWithValue("@SEMID", ddlSemIDFooter.SelectedValue);
                    cmd1.Parameters.AddWithValue("@SEMESTERID", int.Parse(lblSemID.Text));
                    cmd1.Parameters.AddWithValue("@PROGRAMID", lblProgID.Text);
                    cmd1.Parameters.AddWithValue("@STUDENTID", StuID);
                    cmd1.Parameters.AddWithValue("@ENRLDT", Convert.ToDateTime(Date));
                    cmd1.Parameters.AddWithValue("@COURSEID", lblCourseIDFooter.Text);
                    cmd1.Parameters.AddWithValue("@CREDITHH", decimal.Parse(txtCourseHrFooter.Text));
                    cmd1.Parameters.AddWithValue("@CRCOST", Decimal.Parse(txtCrCostFooter.Text));
                    cmd1.Parameters.AddWithValue("@REMARKS", txtRemarksFooter.Text);
                    cmd1.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = iob.UserID;
                    cmd1.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = iob.PcName;
                    cmd1.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = iob.Ipaddress;
                    cmd1.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = iob.InTime;
                    cmd1.ExecuteNonQuery();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();
                    GridShow();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gv_CourseReg_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblSem_ID = (Label)gv_CourseReg.Rows[e.RowIndex].FindControl("lblSemID");
                Label lblCourse = (Label)gv_CourseReg.Rows[e.RowIndex].FindControl("lblCourse");
                Label lblCourseID = (Label)gv_CourseReg.Rows[e.RowIndex].FindControl("lblCourseID");
                string SemID = SemID_8(lblSem_ID.Text);
                Label lblCheck = new Label();
                Global.lblAdd("SELECT COURSEID FROM EIM_RESULT  WHERE REGYY ='" + ddlYr.Text + "' AND COURSEID='" + lblCourseID.Text + "' AND SEMESTERID = '" + lblSemID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND STUDENTID='" + txtStuID.Text + "' AND SEMID='" + SemID + "'", lblCheck);
                if (lblCheck.Text != "")
                {
                    lblMSGCourse.Visible = true;
                    lblMSGCourse.Text = "You can't delete this information,Beacause,it have already created result by this course id!";
                }
                else
                {
                    lblMSGCourse.Visible = false;
                    Label lblDescript = new Label();
                    string ID = "";
                    if (btnEdit.Text == "Edit")
                        ID = txtStuID.Text;
                    else
                        ID = ddlStudentEdit.SelectedValue.ToString();
                    Global.lblAdd(@"SELECT CONVERT(NVARCHAR(50),REGYY,103)+'  '+CONVERT(NVARCHAR(50),SEMESTERID,103)+'  '+PROGRAMID+'  '+STUDENTID+'  '
                                    +COURSEID+'  '+SEMID+'  '+ISNULL(CONVERT(NVARCHAR(50),ENRLDT,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),CREDITHH,103),'(NULL)')+'  '
                                    +ISNULL(CONVERT(NVARCHAR(50),CRCOST,103),'(NULL)')+'  '+ISNULL(REMARKS,'(NULL)')+'  '+ISNULL(USERID,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '
                                    +ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+ISNULL(UPDUSERID,'(NULL)')+'  '+
                                    ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)') FROM EIM_COURSEREG
                                     WHERE COURSEID='" + lblCourseID.Text + "' AND STUDENTID='" + ID + "' AND PROGRAMID='" + lblProgID.Text + "' AND SEMID='" + SemID + "'", lblDescript);
                    iob.TableID = "EIM_COURSEREG";
                    iob.Type = "DELETE";
                    iob.Descrip = lblDescript.Text;
                    dob.INSERT_LOG(iob);
                    //LogInsert End
                    //LogInsert Start   
                    lblDescript.Text = "";
                    Global.lblAdd(@"SELECT CONVERT(NVARCHAR(50),REGYY,103)+'  '+CONVERT(NVARCHAR(50),SEMESTERID,103)+'  '+PROGRAMID+'  '+STUDENTID+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),ENRLDT,103),'(NULL)')+'  '+ISNULL(BATCH,'(NULL)')+'  '+ISNULL(SESSION,'(NULL)')+'  '+ISNULL(REMARKS,'(NULL)')+'  '+
                    ISNULL(USERID,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                    ISNULL(UPDUSERID,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(UPDIPADDRESS,'(NULL)')+'  '
                    +ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)') FROM EIM_CREGMST
                    WHERE REGYY ='" + ddlYr.Text + "' AND   SEMESTERID = '" + lblSemID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND STUDENTID='" + ID + "'", lblDescript);
                    iob.TableID = "EIM_CREGMST";
                    iob.Type = "DELETE";
                    iob.Descrip = lblDescript.Text;
                    dob.INSERT_LOG(iob);
                    //LogInsert End
                    SqlConnection conn = new SqlConnection(Global.connection);
                    SqlCommand cmd = new SqlCommand("SELECT PROGRAMID FROM EIM_COURSEREG  WHERE PROGRAMID='" + lblProgID.Text + "' AND STUDENTID='" + ID + "' AND SEMESTERID='" + lblSemID.Text + "' AND REGYY='" + ddlYr.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (conn.State != ConnectionState.Closed)conn.Close();
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        lblMSG.Visible = false;
                        if (conn.State != ConnectionState.Open)conn.Open();
                        SqlCommand cmd1 = new SqlCommand("DELETE FROM EIM_COURSEREG WHERE COURSEID='" + lblCourseID.Text + "' AND STUDENTID='" + ID + "' AND PROGRAMID='" + lblProgID.Text + "' AND SEMID='" + SemID + "'", conn);
                        cmd1.ExecuteNonQuery();
                        if (conn.State != ConnectionState.Closed)conn.Close();
                        GridShow();
                    }
                    else
                    {
                        if (conn.State != ConnectionState.Open)conn.Open();
                        SqlCommand cmd2 = new SqlCommand("DELETE FROM EIM_CREGMST WHERE PROGRAMID='" + lblProgID.Text + "' AND STUDENTID='" + ID + "' AND SEMESTERID='" + lblSemID.Text + "' AND REGYY='" + ddlYr.Text + "'", conn);
                        cmd2.ExecuteNonQuery();
                        if (conn.State != ConnectionState.Closed)conn.Close();
                        if (conn.State != ConnectionState.Open)conn.Open();
                        SqlCommand cmd21 = new SqlCommand("DELETE FROM EIM_COURSEREG WHERE COURSEID='" + lblCourseID.Text + "' AND STUDENTID='" + ID + "' AND PROGRAMID='" + lblProgID.Text + "' AND SEMID='" + SemID + "'", conn);
                        cmd21.ExecuteNonQuery();
                        if (conn.State != ConnectionState.Closed)conn.Close();
                        GridShow();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void gv_CourseReg_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                gv_CourseReg.EditIndex = e.NewEditIndex;
                GridShow();
                Label lblCourseEdit = (Label)gv_CourseReg.Rows[e.NewEditIndex].FindControl("lblCourseEdit");
                Label lblSemIDEdit = (Label)gv_CourseReg.Rows[e.NewEditIndex].FindControl("lblSemIDEdit");
                ImageButton imgbtnPEdit = (ImageButton)gv_CourseReg.Rows[e.NewEditIndex].FindControl("imgbtnPEdit");
                Label lblCourseIDEdit = (Label)gv_CourseReg.Rows[e.NewEditIndex].FindControl("lblCourseIDEdit");
                DropDownList ddlCourseEdit = (DropDownList)gv_CourseReg.Rows[e.NewEditIndex].FindControl("ddlCourseEdit");
                DropDownList ddlSemIDEdit = (DropDownList)gv_CourseReg.Rows[e.NewEditIndex].FindControl("ddlSemIDEdit");
                 
                string Index = SemID_8(lblSemIDEdit.Text);
                string UI=Session["UserName"].ToString();
                if (Index == "")
                    Index = "00";
                Global.BindDropDownNM(ddlCourseEdit, @"SELECT COURSECD NM
FROM  EIM_COURSE WHERE  PROGRAMID='" + lblProgID.Text + "'");
                //Global.dropDownAdd_GridEditMode(ddlCourseEdit, "SELECT COURSECD FROM EIM_COURSE WHERE PROGRAMID='" + lblProgID.Text + "'", lblCourseEdit.Text);
                ddlSemIDEdit.SelectedIndex = Convert.ToInt16(Index) - 1;
                ddlCourseEdit.Text = lblCourseEdit.Text;
                Session["SEMID"] = "";
                Session["COURSEID"] = "";
                Session["SEMID"] = Index;
                Session["COURSEID"] = lblCourseIDEdit.Text;
                ddlSemIDEdit.Focus();
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

        protected void gv_CourseReg_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                DropDownList ddlSemIDEdit = (DropDownList)gv_CourseReg.Rows[e.RowIndex].FindControl("ddlSemIDEdit");
                DropDownList ddlCourseEdit = (DropDownList)gv_CourseReg.Rows[e.RowIndex].FindControl("ddlCourseEdit");
                Label lblCourseIDEdit = (Label)gv_CourseReg.Rows[e.RowIndex].FindControl("lblCourseIDEdit");
                TextBox txtCourseHrEdit = (TextBox)gv_CourseReg.Rows[e.RowIndex].FindControl("txtCourseHrEdit");
                TextBox txtCrCostEdit = (TextBox)gv_CourseReg.Rows[e.RowIndex].FindControl("txtCrCostEdit");
                TextBox txtRemarksEdit = (TextBox)gv_CourseReg.Rows[e.RowIndex].FindControl("txtRemarksEdit");

                if (ddlYr.Text == "Select")
                {
                    ddlYr.Focus();
                }
                else if (lblSemID.Text == "")
                {
                    ddlSemNM.Focus();
                }
                else if (ddlProgNM.Text == "Select")
                {
                    ddlProgNM.Focus();
                }
                else if (txtStuID.Text == "")
                {
                    txtStuIDNew.Focus();
                }
                else if (txtStuIDNew.Text == "")
                {
                    txtStuIDNew.Focus();
                }
                else if (ddlSemIDEdit.Text == "Select")
                {
                    ddlSemIDEdit.Focus();
                }
                else if (ddlCourseEdit.Text == "Select")
                {
                    ddlCourseEdit.Focus();
                }
                else
                {
                    Label lblCheck = new Label();
                    Global.lblAdd("SELECT COURSEID FROM EIM_RESULT  WHERE REGYY ='" + ddlYr.Text + "' AND COURSEID='" + Session["COURSEID"].ToString() + "' AND SEMESTERID = '" + lblSemID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND STUDENTID='" + txtStuID.Text + "' AND SEMID='" + Session["SEMID"].ToString() + "'", lblCheck);
                    if (lblCheck.Text != "")
                    {
                        lblMSGCourse.Visible = true;
                        lblMSGCourse.Text = "You can't Edit Whole information.Because, already have created result by this course id,<br/> You can edit only remarks!";
                        String Sem = ddlSemIDEdit.SelectedValue;
                        iob.UPDUserID = Session["UserName"].ToString();
                        iob.UPDIpaddress = Session["IpAddress"].ToString();
                        iob.UPDPcName = Session["PCName"].ToString();
                        iob.UPDTime = Global.Dayformat1(DateTime.Now);
                        if (con.State != ConnectionState.Open)con.Open();
                        //    Session["SEMID"] = Index;
                        //Session["COURSEID"] 
                        //Update EIM_COURSEREG Start
                        SqlCommand cmd = new SqlCommand("UPDATE EIM_COURSEREG set  REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE COURSEID='" + Session["COURSEID"].ToString() + "' AND STUDENTID='" + txtStuID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND SEMID='" + Session["SEMID"].ToString() + "'", con);
                        cmd.Parameters.Clear();
                         
                        cmd.Parameters.AddWithValue("REMARKS", txtRemarksEdit.Text);
                        cmd.Parameters.AddWithValue("UPDUSERID", iob.UPDUserID);
                        cmd.Parameters.AddWithValue("UPDUSERPC", iob.UPDPcName);
                        cmd.Parameters.AddWithValue("UPDIPADDRESS", iob.UPDIpaddress);
                        cmd.Parameters.AddWithValue("UPDTIME", iob.UPDTime);
                        cmd.ExecuteNonQuery();
                        if (con.State != ConnectionState.Closed)con.Close();
                        //Update EIM_COURSEREG End
                        //Update EIM_CREGMST Start
                        if (con.State != ConnectionState.Open)con.Open();
                        SqlCommand cmd1 = new SqlCommand("UPDATE EIM_CREGMST SET REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE STUDENTID='" + txtStuID.Text + "' AND SEMESTERID='" + lblSemID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND REGYY='" + ddlYr.Text + "' ", con);
                        cmd1.Parameters.Clear();
                        
                        cmd1.Parameters.AddWithValue("REMARKS", txtRemarksEdit.Text);
                        cmd1.Parameters.AddWithValue("UPDUSERID", iob.UPDUserID);
                        cmd1.Parameters.AddWithValue("UPDUSERPC", iob.UPDPcName);
                        cmd1.Parameters.AddWithValue("UPDIPADDRESS", iob.UPDIpaddress);
                        cmd1.Parameters.AddWithValue("UPDTIME", iob.UPDTime);
                        cmd1.ExecuteNonQuery();
                        if (con.State != ConnectionState.Closed)con.Close();
                        //Update EIM_CREGMST END
                        //LogInsert Start   
                        // string SemID = SemID_8(ddlSemIDEdit.SelectedValue);
                        Label lblDescript = new Label();
                        Global.lblAdd(@"SELECT CONVERT(NVARCHAR(50),REGYY,103)+'  '+CONVERT(NVARCHAR(50),SEMESTERID,103)+'  '+PROGRAMID+'  '+STUDENTID+'  '
                                    +COURSEID+'  '+SEMID+'  '+ISNULL(CONVERT(NVARCHAR(50),ENRLDT,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),CREDITHH,103),'(NULL)')+'  '
                                    +ISNULL(CONVERT(NVARCHAR(50),CRCOST,103),'(NULL)')+'  '+ISNULL(REMARKS,'(NULL)')+'  '+ISNULL(USERID,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '
                                    +ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+ISNULL(UPDUSERID,'(NULL)')+'  '+
                                    ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)') FROM EIM_COURSEREG
                                     WHERE COURSEID='" + lblCourseIDEdit.Text + "' AND STUDENTID='" + txtStuID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND SEMID='" + ddlSemIDEdit.SelectedValue + "'", lblDescript);
                        iob.TableID = "EIM_COURSEREG";
                        iob.Type = "UPDATE";
                        iob.Descrip = lblDescript.Text;
                        dob.INSERT_LOG(iob);
                        //LogInsert End
                        //LogInsert Start   
                        // SemID = SemID_8(lblSemID.Text); 
                        Global.lblAdd(@"SELECT CONVERT(NVARCHAR(50),REGYY,103)+'  '+CONVERT(NVARCHAR(50),SEMESTERID,103)+'  '+PROGRAMID+'  '+STUDENTID+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),ENRLDT,103),'(NULL)')+'  '+ISNULL(BATCH,'(NULL)')+'  '+ISNULL(SESSION,'(NULL)')+'  '+ISNULL(REMARKS,'(NULL)')+'  '+
                    ISNULL(USERID,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                    ISNULL(UPDUSERID,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(UPDIPADDRESS,'(NULL)')+'  '
                    +ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)') FROM EIM_CREGMST
                    WHERE REGYY ='" + ddlYr.Text + "' AND   SEMESTERID = '" + lblSemID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND STUDENTID='" + txtStuID.Text + "'", lblDescript);
                        iob.TableID = "EIM_CREGMST";
                        iob.Type = "UPDATE";
                        iob.Descrip = lblDescript.Text;
                        dob.INSERT_LOG(iob);
                        //LogInsert End
                        gv_CourseReg.EditIndex = -1;
                        GridShow();
                    }
                    else
                    {
                        String Sem = ddlSemIDEdit.SelectedValue;
                        iob.UPDUserID = Session["UserName"].ToString();
                        iob.UPDIpaddress = Session["IpAddress"].ToString();
                        iob.UPDPcName = Session["PCName"].ToString();
                        iob.UPDTime = Global.Dayformat1(DateTime.Now);
                        if (con.State != ConnectionState.Open)con.Open();
                        //    Session["SEMID"] = Index;
                        //Session["COURSEID"] 
                        //Update EIM_COURSEREG Start
                        SqlCommand cmd = new SqlCommand("UPDATE EIM_COURSEREG SET SEMID=@SEMID,COURSEID=@COURSEID,CREDITHH=@CREDITHH,CRCOST=@CRCOST,REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE COURSEID='" + Session["COURSEID"].ToString() + "' AND STUDENTID='" + txtStuID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND SEMID='" + Session["SEMID"].ToString() + "'", con);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("SEMID", Sem);
                        cmd.Parameters.AddWithValue("COURSEID", lblCourseIDEdit.Text);
                        cmd.Parameters.AddWithValue("CREDITHH", txtCourseHrEdit.Text);
                        cmd.Parameters.AddWithValue("CRCOST", txtCrCostEdit.Text);
                        cmd.Parameters.AddWithValue("REMARKS", txtRemarksEdit.Text);
                        cmd.Parameters.AddWithValue("UPDUSERID", iob.UPDUserID);
                        cmd.Parameters.AddWithValue("UPDUSERPC", iob.UPDPcName);
                        cmd.Parameters.AddWithValue("UPDIPADDRESS", iob.UPDIpaddress);
                        cmd.Parameters.AddWithValue("UPDTIME", iob.UPDTime);
                        cmd.ExecuteNonQuery();
                        if (con.State != ConnectionState.Closed)con.Close();
                        //Update EIM_COURSEREG End
                        //Update EIM_CREGMST Start
                        if (con.State != ConnectionState.Open)con.Open();
                        SqlCommand cmd1 = new SqlCommand("UPDATE EIM_CREGMST SET BATCH=@BATCH,SESSION=@SESSION,REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE STUDENTID='" + txtStuID.Text + "' AND SEMESTERID='" + lblSemID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND REGYY='" + ddlYr.Text + "' ", con);
                        cmd1.Parameters.Clear();
                        cmd1.Parameters.AddWithValue("BATCH", lblCourseIDEdit.Text);
                        cmd1.Parameters.AddWithValue("SESSION", txtCourseHrEdit.Text);
                        cmd1.Parameters.AddWithValue("REMARKS", txtRemarksEdit.Text);
                        cmd1.Parameters.AddWithValue("UPDUSERID", iob.UPDUserID);
                        cmd1.Parameters.AddWithValue("UPDUSERPC", iob.UPDPcName);
                        cmd1.Parameters.AddWithValue("UPDIPADDRESS", iob.UPDIpaddress);
                        cmd1.Parameters.AddWithValue("UPDTIME", iob.UPDTime);
                        cmd1.ExecuteNonQuery();
                        if (con.State != ConnectionState.Closed)con.Close();
                        //Update EIM_CREGMST END
                        //LogInsert Start   
                        // string SemID = SemID_8(ddlSemIDEdit.SelectedValue);
                        Label lblDescript = new Label();
                        Global.lblAdd(@"SELECT CONVERT(NVARCHAR(50),REGYY,103)+'  '+CONVERT(NVARCHAR(50),SEMESTERID,103)+'  '+PROGRAMID+'  '+STUDENTID+'  '
                                    +COURSEID+'  '+SEMID+'  '+ISNULL(CONVERT(NVARCHAR(50),ENRLDT,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),CREDITHH,103),'(NULL)')+'  '
                                    +ISNULL(CONVERT(NVARCHAR(50),CRCOST,103),'(NULL)')+'  '+ISNULL(REMARKS,'(NULL)')+'  '+ISNULL(USERID,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '
                                    +ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+ISNULL(UPDUSERID,'(NULL)')+'  '+
                                    ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)') FROM EIM_COURSEREG
                                     WHERE COURSEID='" + lblCourseIDEdit.Text + "' AND STUDENTID='" + txtStuID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND SEMID='" + ddlSemIDEdit.SelectedValue + "'", lblDescript);
                        iob.TableID = "EIM_COURSEREG";
                        iob.Type = "UPDATE";
                        iob.Descrip = lblDescript.Text;
                        dob.INSERT_LOG(iob);
                        //LogInsert End
                        //LogInsert Start   
                        // SemID = SemID_8(lblSemID.Text); 
                        Global.lblAdd(@"SELECT CONVERT(NVARCHAR(50),REGYY,103)+'  '+CONVERT(NVARCHAR(50),SEMESTERID,103)+'  '+PROGRAMID+'  '+STUDENTID+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),ENRLDT,103),'(NULL)')+'  '+ISNULL(BATCH,'(NULL)')+'  '+ISNULL(SESSION,'(NULL)')+'  '+ISNULL(REMARKS,'(NULL)')+'  '+
                    ISNULL(USERID,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                    ISNULL(UPDUSERID,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(UPDIPADDRESS,'(NULL)')+'  '
                    +ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)') FROM EIM_CREGMST
                    WHERE REGYY ='" + ddlYr.Text + "' AND   SEMESTERID = '" + lblSemID.Text + "' AND PROGRAMID='" + lblProgID.Text + "' AND STUDENTID='" + txtStuID.Text + "'", lblDescript);
                        iob.TableID = "EIM_CREGMST";
                        iob.Type = "UPDATE";
                        iob.Descrip = lblDescript.Text;
                        dob.INSERT_LOG(iob);
                        //LogInsert End
                        gv_CourseReg.EditIndex = -1;
                        GridShow();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(""+ex);
            }
        }

        protected void ddlSemIDFooter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCourseFooter = (DropDownList)gv_CourseReg.FooterRow.FindControl("ddlCourseFooter");
            DropDownList ddlSemIDFooter = (DropDownList)gv_CourseReg.FooterRow.FindControl("ddlSemIDFooter");
            if (ddlSemIDFooter.Text == "Select")
                ddlSemIDFooter.Focus();
            else
                ddlCourseFooter.Focus();
        }

        protected void ddlSemIDEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlSemIDEdit = (DropDownList)row.FindControl("ddlSemIDEdit");
            DropDownList ddlCourseEdit = (DropDownList)row.FindControl("ddlCourseEdit");
            if (ddlSemIDEdit.Text == "Select")
                ddlSemIDEdit.Focus();
            else
                ddlCourseEdit.Focus();
        }

        protected void ddlCourseFooter_SelectedIndexChanged(object sender, EventArgs e)
        {

            Label lblCourseIDFooter = (Label)gv_CourseReg.FooterRow.FindControl("lblCourseIDFooter");
            DropDownList ddlCourseFooter = (DropDownList)gv_CourseReg.FooterRow.FindControl("ddlCourseFooter");
            TextBox txtRemarksFooter = (TextBox)gv_CourseReg.FooterRow.FindControl("txtRemarksFooter");
            TextBox txtCourseNMFooter = (TextBox)gv_CourseReg.FooterRow.FindControl("txtCourseNMFooter");

            TextBox txtCourseHrFooter = (TextBox)gv_CourseReg.FooterRow.FindControl("txtCourseHrFooter");
            TextBox txtCrCostFooter = (TextBox)gv_CourseReg.FooterRow.FindControl("txtCrCostFooter");
            if (ddlCourseFooter.Text == "Select")
                ddlCourseFooter.Focus();
            else
            {
                Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE PROGRAMID='" + lblProgID.Text + "' AND COURSECD='" + ddlCourseFooter.Text + "'", lblCourseIDFooter);
                if (ddlCourseFooter.Text == "Select")
                    ddlCourseFooter.Focus();
                else
                {
                    Global.txtAdd("SELECT COURSENM FROM EIM_COURSE WHERE COURSECD='" + ddlCourseFooter.Text + "' AND PROGRAMID='" + lblProgID.Text + "'", txtCourseNMFooter);
                    Global.txtAdd("SELECT CREDITHH FROM EIM_COURSE WHERE PROGRAMID='" + lblProgID.Text + "' and COURSEID='" + lblCourseIDFooter.Text + "'", txtCourseHrFooter);
                    decimal CR = decimal.Parse(txtCourseHrFooter.Text);
                    Global.txtAdd("SELECT (COSTPERCR*'" + CR + "') FROM EIM_PROGRAM WHERE PROGRAMID='" + lblProgID.Text + "'", txtCrCostFooter);
                    txtRemarksFooter.Focus();
                }
            }
        }

        protected void ddlCourseEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            Label lblCourseIDEdit = (Label)row.FindControl("lblCourseIDEdit");
            DropDownList ddlCourseEdit = (DropDownList)row.FindControl("ddlCourseEdit");
            TextBox txtRemarksEdit = (TextBox)row.FindControl("txtRemarksEdit");
            TextBox txtCourseNMEdit = (TextBox)row.FindControl("txtCourseNMEdit");
            TextBox txtCourseHrEdit = (TextBox)row.FindControl("txtCourseHrEdit");
            TextBox txtCrCostEdit = (TextBox)row.FindControl("txtCrCostEdit");
            if (ddlCourseEdit.Text == "Select")
                ddlCourseEdit.Focus();
            else
            {
                Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE PROGRAMID='" + lblProgID.Text + "' AND COURSECD='" + ddlCourseEdit.Text + "'", lblCourseIDEdit);
                if (ddlCourseEdit.Text == "Select")
                    ddlCourseEdit.Focus();
                else
                {
                    Global.txtAdd("SELECT COURSENM FROM EIM_COURSE WHERE COURSECD='" + ddlCourseEdit.Text + "' AND PROGRAMID='" + lblProgID.Text + "'", txtCourseNMEdit);
                    Global.txtAdd("SELECT CREDITHH FROM EIM_COURSE WHERE PROGRAMID='" + lblProgID.Text + "' and COURSEID='" + lblCourseIDEdit.Text + "'", txtCourseHrEdit);
                    decimal CR = decimal.Parse(txtCourseHrEdit.Text);
                    Global.txtAdd("SELECT (COSTPERCR*'" + CR + "') FROM EIM_PROGRAM WHERE PROGRAMID='" + lblProgID.Text + "'", txtCrCostEdit);
                    txtRemarksEdit.Focus();
                }
            }
        }

        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            DataClear();
            ddlSemNM.Focus();
        }

        protected void ddlStudentEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSSN.Text = "";
            txtBtch.Text = "";
            lblStuNM.Text = "";
            Global.lblAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE STUDENTID='" + ddlStudentEdit.SelectedValue.ToString() + "'", lblStuNM);
            Global.txtAdd("SELECT SESSION FROM EIM_CREGMST WHERE (STUDENTID='" + ddlStudentEdit.SelectedValue.ToString() + "') AND PROGRAMID='" + lblProgID.Text + "' AND SEMESTERID='" + lblSemID.Text + "'", txtSSN);
            Global.txtAdd("SELECT BATCH FROM EIM_CREGMST WHERE (STUDENTID='" + ddlStudentEdit.SelectedValue.ToString() + "')  AND PROGRAMID='" + lblProgID.Text + "' AND SEMESTERID='" + lblSemID.Text + "'", txtBtch);
            Global.txtAdd("SELECT REMARKS FROM EIM_CREGMST WHERE (STUDENTID='" + ddlStudentEdit.SelectedValue.ToString() + "' OR MIGRATESID='" + ddlStudentEdit.SelectedValue.ToString() + "')  AND PROGRAMID='" + lblProgID.Text + "' AND SEMESTERID='" + lblSemID.Text + "'", txtRmrk);
            txtStuID.Text = ddlStudentEdit.SelectedValue.ToString();
            GridShow();
        }
    }
}
