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
    public partial class Course_reg1 : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        public static DataTable dt;
        public static int i = 1;
        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection con = new SqlConnection(Global.connection); 
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
                    //                    Global.dropDownAdd(ddlSemNM, @"SELECT     EIM_SEMESTER.SEMESTERNM FROM EIM_COURSEREG 
                    //                                                 INNER JOIN EIM_SEMESTER ON EIM_COURSEREG.SEMESTERID = EIM_SEMESTER.SEMESTERID");
                    //                    Global.dropDownAdd(ddlProgNM, @"SELECT EIM_PROGRAM.PROGRAMNM FROM EIM_COURSEREG INNER JOIN
                    //                                                 EIM_PROGRAM ON EIM_COURSEREG.PROGRAMID = EIM_PROGRAM.PROGRAMID");
                    Global.dropDownAdd(ddlSemNM, @"SELECT SEMESTERNM FROM EIM_SEMESTER");
                    Global.dropDownAdd(ddlProgNM, @"SELECT PROGRAMNM FROM EIM_PROGRAM");
                    Session["datatable"] = null;
                    //Global.dropDownAdd(ddlSSN, "SELECT PROGRAMNM FROM EIM_PROGRAM");

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
                    ddlSemNM.Focus();
                    txtDate.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                    int Count = GridViewEQ.Rows.Count;
                    if (Count > 0)
                        dt.Clear();
                    Session["datatable"] = "";
                }
                Session["YR"] = ddlYr.Text;
            }

        }
        private void gridShow()
        {

            if (con.State != ConnectionState.Open)con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT SEMID,COURSEID,CREDITHH,CRCOST,REMARKS FROM  EIM_COURSEREG WHERE STUDENTID='" + txtStuIDEdit.Text + "'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State != ConnectionState.Closed)con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }

            else
            {
                GridView1.Visible = false;

            }
        }
        private void gridShow1()
        {
            string StuID = "";
            if (btnEdit.Text == "Edit")
                StuID = txtStuID.Text;
            else
                StuID = txtStuIDEdit.Text;
            if (con.State != ConnectionState.Open)con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  (CASE WHEN EIM_COURSEREG.SEMID='00' then '' when EIM_COURSEREG.SEMID='01' then '1st' when EIM_COURSEREG.SEMID='02' then '2nd' when EIM_COURSEREG.SEMID='03' then '3rd' when EIM_COURSEREG.SEMID='04' then '4th' 
            when EIM_COURSEREG.SEMID='05' then '5th' when EIM_COURSEREG.SEMID='06' then '6th' when EIM_COURSEREG.SEMID='07' then '7th' when EIM_COURSEREG.SEMID='08' then '8th'
            else '' end) AS SEM,EIM_COURSE.COURSECD, EIM_COURSEREG.CREDITHH,  CASE WHEN EIM_COURSEREG.REMARKS='&nbsp;' THEN '' ELSE EIM_COURSEREG.REMARKS END as REMARKS, EIM_COURSEREG.CRCOST
                                              FROM         EIM_COURSEREG INNER JOIN
                                              EIM_COURSE ON EIM_COURSEREG.COURSEID = EIM_COURSE.COURSEID WHERE STUDENTID='" + StuID + "'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State != ConnectionState.Closed)con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.Visible = false;
            }
        }
        private void Preview()
        {
            string StuID = "";
            StuID = txtStuID.Text;
            SqlCommand cmd = new SqlCommand(@"SELECT  (CASE WHEN EIM_COURSEREG.SEMID='00' then '' when EIM_COURSEREG.SEMID='01' then '1st' when EIM_COURSEREG.SEMID='02' then '2nd' when EIM_COURSEREG.SEMID='03' then '3rd' when EIM_COURSEREG.SEMID='04' then '4th' 
            when EIM_COURSEREG.SEMID='05' then '5th' when EIM_COURSEREG.SEMID='06' then '6th' when EIM_COURSEREG.SEMID='07' then '7th' when EIM_COURSEREG.SEMID='08' then '8th'
            else '' end) AS SEM,EIM_COURSE.COURSECD,EIM_COURSE.COURSENM, EIM_COURSEREG.CREDITHH, CASE WHEN EIM_COURSEREG.REMARKS='&nbsp;' THEN '' ELSE EIM_COURSEREG.REMARKS END as REMARKS, EIM_COURSEREG.CRCOST
            FROM         EIM_COURSEREG INNER JOIN
            EIM_COURSE ON EIM_COURSEREG.COURSEID = EIM_COURSE.COURSEID WHERE STUDENTID='" + StuID + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State != ConnectionState.Closed)con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Preview.Visible = true;
                lblPreview.Visible = true;
                lblPreviewTXT.Visible = true;
                gv_Preview.DataSource = ds;
                gv_Preview.DataBind();
            }
            else
            {
                gv_Preview.Visible = false;
                lblPreview.Visible = false;
                lblPreviewTXT.Visible = false;
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
            string YR = HttpContext.Current.Session["YR"].ToString();
            string SemID = HttpContext.Current.Session["SemID"].ToString();
            //string ProID = HttpContext.Current.Session["ProgID"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT STUDENTID FROM EIM_CREGMST WHERE STUDENTID LIKE '" + prefixText + "%' AND SEMESTERID='" + SemID + "' AND REGYY='" + YR + "'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["STUDENTID"].ToString());
            }
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();


        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionStudentID(string prefixText, int count, string contextKey)
        {

            SqlConnection conn = new SqlConnection(Global.connection);

            string ProgID = HttpContext.Current.Session["ProgID"].ToString();
            string YR = HttpContext.Current.Session["YR"].ToString();
            string SemID = HttpContext.Current.Session["SemID"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT STUDENTID FROM EIM_STUDENT WHERE STUDENTID LIKE '" + prefixText + "%' AND PROGRAMID='" + ProgID + "' AND SEMESTERID='" + SemID + "' AND ADMITYY='" + YR + "'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
            {
                CompletionSet.Add(oReader["STUDENTID"].ToString());
            }
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();



        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgNM.Text != "Select")
            {
                Session["ProgID"] = "";
                lblMSG.Text = "";
                lblWrng.Text = "";
                lblProgID.Text = "";
                Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProgID);
                Session["ProgID"] = lblProgID.Text;
                txtStuID.Focus();
            }
            else
            {
                ddlProgNM.Focus();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (lblWrng.Text != "")
            {
                txtCrsID.Focus();
            }
            if (txtCrsID.Text == "")
            {
                txtCrsID.Focus();
            }
            else if (txtCrditHr.Text == "")
            {
                txtCrditHr.Focus();
            }
            else if (txtCrditCst.Text == "")
            {
                txtCrditCst.Focus();
            }
            else
            {
                if (Convert.ToString(ViewState["Row"]) != "")
                {
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["1"] = ddlSemisterNM.SelectedItem;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["SEMID"] = txtSemID.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["2"] = txtCrsID.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["COURSEID"] = txtCourseID.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["3"] = txtCrditHr.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["4"] = txtCrditCst.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["5"] = txtRemark.Text;


                    GridViewEQ.DataSource = dt;
                    GridViewEQ.DataBind();
                    ViewState["Row"] = "";

                }
                else if (Convert.ToString(ViewState["Row"]) == "")
                {
                    Session["datatable"] = dt;

                    dt = new DataTable();
                    DataRow dr = null;
                    //  dt.Columns.Add(new DataColumn("RowNumber", typeof(int)));
                    dt.Columns.Add(new DataColumn("1", typeof(string)));
                    dt.Columns.Add(new DataColumn("SEMID", typeof(string)));
                    dt.Columns.Add(new DataColumn("2", typeof(string)));
                    dt.Columns.Add(new DataColumn("COURSEID", typeof(string)));
                    dt.Columns.Add(new DataColumn("3", typeof(string)));
                    dt.Columns.Add(new DataColumn("4", typeof(string)));
                    dt.Columns.Add(new DataColumn("5", typeof(string)));

                    if (Session["datatable"] != null)
                    {

                        dt = (DataTable)Session["datatable"];
                        dr = dt.NewRow();
                        // dr["RowNumber"] = i;
                        dr["1"] = ddlSemisterNM.SelectedItem;
                        dr["SEMID"] = txtSemID.Text;
                        dr["2"] = txtCrsID.Text;
                        dr["COURSEID"] = txtCourseID.Text;
                        dr["3"] = txtCrditHr.Text;
                        dr["4"] = txtCrditCst.Text;
                        dr["5"] = txtRemark.Text;

                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dt.NewRow();
                        dr["1"] = ddlSemisterNM.SelectedItem;
                        dr["SEMID"] = txtSemID.Text;
                        dr["2"] = txtCrsID.Text;
                        dr["COURSEID"] = txtCourseID.Text;
                        dr["3"] = txtCrditHr.Text;
                        dr["4"] = txtCrditCst.Text;
                        dr["5"] = txtRemark.Text;
                        dt.Rows.Add(dr);
                    }
                    GridViewEQ.Visible = true;
                    GridViewEQ.DataSource = dt;
                    GridViewEQ.DataBind();
                    i++;
                    ddlSemisterNM.SelectedIndex = -1;
                    txtSemID.Text = "";
                    txtCourseID.Text = "";
                    txtCrsID.Text = "";
                    txtCrditHr.Text = "";
                    txtCrditCst.Text = "";
                    txtRemark.Text = "";
                    ddlSemisterNM.Focus();

                }
            }
        }

        protected void GridViewEQ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                // dt = (DataTable)Session["datatable"];
                if (dt.Rows.Count >= 0)
                {

                    dt.Rows.RemoveAt(Convert.ToInt16(e.CommandArgument));
                    GridViewEQ.DataSource = dt;
                    GridViewEQ.DataBind();

                }

            }
        }

        protected void GridViewEQ_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            GridViewEQ.DataSource = dt;
            GridViewEQ.DataBind();

        }
        private void clear()
        {
            txtStuID.Text = "";
            txtSSN.Text = "";
            txtRmrk.Text = "";
            txtBtch.Text = "";
            ddlProgNM.SelectedIndex = -1;
            Session["ProgID"] = "";
        }
        protected void ddlYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYr.Text != "Select")
            {
                Session["YR"] = "";
                ddlSemNM.Focus();
                Session["YR"] = ddlYr.Text;
                lblMSG.Text = "";
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
                if (btnEdit.Text == "Edit")
                    ddlProgNM.Focus();
                else
                    txtStuIDEdit.Focus();
                lblMSG.Text = "";


            }
            else
            {
                ddlSemNM.Focus();
            }
        }
        private void INSERT()
        {
            iob.UserID = Session["UserName"].ToString();
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.PcName = Session["PCName"].ToString();
            iob.InTime = Global.Dayformat1(DateTime.Now);
            lblMSG.Text = "";
            lblMSGCourse.Text = "";
            if (ddlYr.Text == "Select")
            {
                ddlYr.Focus();
            }
            else if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
            }
            else if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
            }
            else if (txtStuID.Text == "")
            {
                txtStuID.Focus();
            }
            else
            {
                lblProgID.Text = "";
                lblSemID.Text = "";
                string Date = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal).ToString("yyyy-MM-dd");
                Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProgID);
                Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
                iob.EnRLDT = Convert.ToDateTime(Date);
                iob.CrsYR = int.Parse(ddlYr.Text);
                iob.SemID = int.Parse(lblSemID.Text);
                iob.ProgID = lblProgID.Text;
                iob.SeSN = txtSSN.Text;
                iob.StuID = txtStuID.Text;
                iob.Batch = txtBtch.Text;
                iob.Remarks = txtRmrk.Text;
                dob.Insert_EIM_CREGMST(iob);
                Label lblCourseID = new Label();

                foreach (GridViewRow row in GridViewEQ.Rows)
                {
                    int Count = GridViewEQ.Rows.Count;
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            if (con.State != ConnectionState.Open)con.Open();
                        Label lblCorsID = new Label();
                        string SemID = "";
                        if (row.Cells[1].Text == "1st")
                            SemID = "01";
                        else if (row.Cells[1].Text == "2nd")
                            SemID = "02";
                        else if (row.Cells[1].Text == "3rd")
                            SemID = "03";
                        else if (row.Cells[1].Text == "4th")
                            SemID = "04";
                        else if (row.Cells[1].Text == "5th")
                            SemID = "05";
                        else if (row.Cells[1].Text == "6th")
                            SemID = "06";
                        else if (row.Cells[1].Text == "7th")
                            SemID = "07";
                        else if (row.Cells[1].Text == "8th")
                            SemID = "08";

                        Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE PROGRAMID='" + lblProgID.Text + "' AND COURSECD='" + row.Cells[2].Text + "'", lblCorsID);

                        SqlCommand cmd1 = new SqlCommand(@"INSERT INTO EIM_COURSEREG (REGYY,SEMID,SEMESTERID,PROGRAMID,STUDENTID,ENRLDT,COURSEID,
                                             CREDITHH,CRCOST,REMARKS,USERID,USERPC,IPADDRESS,INTIME) VALUES 
                                                       (@REGYY,@SEMID,@SEMESTERID,@PROGRAMID,@STUDENTID,@ENRLDT,@COURSEID,
                                             @CREDITHH,@CRCOST,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)", con);

                        cmd1.Parameters.AddWithValue("@REGYY", int.Parse(ddlYr.Text));
                        cmd1.Parameters.AddWithValue("@SEMID", SemID);
                        cmd1.Parameters.AddWithValue("@SEMESTERID", int.Parse(lblSemID.Text));
                        cmd1.Parameters.AddWithValue("@PROGRAMID", lblProgID.Text);
                        cmd1.Parameters.AddWithValue("@STUDENTID", txtStuID.Text);
                        cmd1.Parameters.AddWithValue("@ENRLDT", Convert.ToDateTime(Date));
                        cmd1.Parameters.AddWithValue("@COURSEID", lblCorsID.Text);
                        cmd1.Parameters.AddWithValue("@CREDITHH", int.Parse(row.Cells[3].Text));
                        cmd1.Parameters.AddWithValue("@CRCOST", Decimal.Parse(row.Cells[4].Text));
                        cmd1.Parameters.AddWithValue("@REMARKS", row.Cells[5].Text);
                        cmd1.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = iob.UserID;
                        cmd1.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = iob.PcName;
                        cmd1.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = iob.Ipaddress;
                        cmd1.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = iob.InTime;
                        cmd1.ExecuteNonQuery();
                        if (con.State != ConnectionState.Closed)
                            if (con.State != ConnectionState.Closed)con.Close();

                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex);
                    }


                }


                int Count1 = GridViewEQ.Rows.Count;
                if (Count1 > 0)
                    dt.Clear();
                Session["datatable"] = "";

                clear();
                gridShow1();
                ddlProgNM.Focus();
                GridViewEQ.Visible = false;
                lblMSG.Visible = true;
                lblMSG.Text = "Inserted !";
            }
        }
        private void UPDATE()
        {

            if (txtStuIDEdit.Text == "")
            {
                txtStuIDEdit.Focus();
            }
            else
            {

                iob.UPDUserID = Session["UserName"].ToString();
                iob.UPDIpaddress = Session["IpAddress"].ToString();
                iob.UPDPcName = Session["PCName"].ToString();
                iob.UPDTime = Global.Dayformat1(DateTime.Now);
                lblProgID.Text = "";
                lblSemID.Text = "";
                DateTime Date = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtProgramNM.Text + "'", lblProgID);
                Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
                iob.EnRLDT = Date;
                iob.CrsYR = int.Parse(ddlYr.Text);
                iob.SemID = int.Parse(lblSemID.Text);
                //iob.SemisterID=
                iob.ProgID = lblProgID.Text;
                iob.SeSN = txtSSN.Text;
                iob.StuID = txtStuIDEdit.Text;
                iob.Batch = txtBtch.Text;
                iob.Remarks = txtRmrk.Text;

                dob.Update_EIM_CREGMST(iob);
                dob.Update_EIM_COURSEREG(iob);
                lblMSG.Visible = true;
                lblMSG.Text = "Updated !";
                EditDataClear();
                GridViewEQ.Visible = false;
                // ddlYr.Enabled = false;
                txtDate.Enabled = false;
                ///ddlSemNM.Enabled = false;
                txtSSN.Enabled = false;
                txtBtch.Enabled = false;
                txtRmrk.Enabled = false;
                GridView1.Visible = false;
                lbl1.Visible = false;
                btnAdd.Visible = false;
                btnEditAdd.Visible = false;

                txtStuIDEdit.Focus();
                txtStuIDEdit.BorderStyle = BorderStyle.Double;
                txtStuIDEdit.BorderColor = Color.LightGreen;


            }
        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {

                if (btnInsert.Text == "Update")
                {
                    UPDATE();
                }
                else
                {
                    INSERT();

                }
                gv_Preview.Visible = false;
            }
            catch (Exception)
            {
                Response.Write("please check preview Data if this data already inserted ?");
            }
        }

        protected void txtCrditHr_TextChanged(object sender, EventArgs e)
        {
            //Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProgID);
            //if (lblProgID.Text == "")
            //{
            //    ddlProgNM.Focus();
            //}
            //else
            //{
            //    int CR = int.Parse(txtCrditHr.Text);
            //    Global.txtAdd("SELECT (COSTPERCR*'" + CR + "') FROM EIM_PROGRAM WHERE PROGRAMID='" + lblProgID.Text + "'", txtCrditCst);
            //    txtRemark.Focus();
            //}
        }

        protected void txtCrditCst_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtCrsID_TextChanged(object sender, EventArgs e)
        {
            Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProgID);
            Global.txtAdd("SELECT COURSEID FROM EIM_COURSE WHERE COURSECD='" + txtCrsID.Text + "' AND PROGRAMID='" + lblProgID.Text + "'", txtCourseID);
            string Match = "";
            lblWrng.Text = "";
            if (txtCourseID.Text != "")
                Match = txtCourseID.Text.Substring(0, 2);
            if (lblProgID.Text != Match)
            {
                lblWrng.Visible = true;
                lblWrng.Text = "Invalid Course CD For Selected Program!";
                txtCrsID.Focus();
            }
            else
            {
                if (lblProgID.Text == "")
                {
                    ddlProgNM.Focus();
                }
                else
                {
                    lblWrng.Visible = false;

                    Global.txtAdd("SELECT CREDITHH FROM EIM_COURSE WHERE PROGRAMID='" + lblProgID.Text + "' and COURSEID='" + txtCourseID.Text + "'", txtCrditHr);
                    int CR = int.Parse(txtCrditHr.Text);
                    Global.txtAdd("SELECT (COSTPERCR*'" + CR + "') FROM EIM_PROGRAM WHERE PROGRAMID='" + lblProgID.Text + "'", txtCrditCst);
                    txtRemark.Focus();
                }
            }
        }

        protected void txtStuID_TextChanged(object sender, EventArgs e)
        {
            if (txtStuID.Text == "")
            {
                txtStuID.Focus();
            }
            else
            {
                txtSSN.Focus();
                Preview();
            }
        }

        protected void txtRemark_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Focus();

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProgID);
                Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
                Session["REGYR"] = ddlYr.Text;
                Session["DATE"] = txtDate.Text;
                Session["SEMESTERNM"] = ddlSemNM.Text;
                Session["PROHRAMNM"] = ddlProgNM.Text;
                if (btnEdit.Text == "Edit")
                    Session["STUDENTID"] = txtStuID.Text;
                else
                    Session["STUDENTID"] = txtStuIDEdit.Text;
                Session["BATCH"] = txtBtch.Text;
                Session["SESSION"] = txtSSN.Text;
                Session["SEMESTERID"] = lblSemID.Text;
                Session["PROHRAMID"] = lblProgID.Text;
                if (ddlYr.Text == "Select")
                {
                    ddlYr.Focus();
                }
                else if (ddlSemNM.Text == "Select")
                {
                    ddlSemNM.Focus();
                }
                else if (ddlProgNM.Text == "Select")
                {
                    ddlProgNM.Focus();
                }
                else
                {
                    if (btnPrint.Text == "Save & Print")
                    {
                        if (txtStuID.Text == "")
                            txtStuID.Focus();
                        else
                        {

                            INSERT();
                            lblMSG.Visible = true;
                            lblMSG.Text = "Inserted !";
                        }
                    }
                    else
                        if (txtStuIDEdit.Text == "")
                            txtStuIDEdit.Focus();
                        else
                        {
                            UPDATE();
                            lblMSG.Visible = true;
                            lblMSG.Text = "Inserted !";
                        }
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                                "OpenWindow", "window.open('/Admission/Report/CourseRegPrint.aspx','_newtab');", true);
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }

        }
        private void EditDataClear()
        {

            txtDate.Text = "";
            ddlSemNM.SelectedIndex = -1;
            ddlYr.SelectedIndex = -1;
            ddlProgNM.SelectedIndex = -1;
            txtStuID.Text = "";
            txtSSN.Text = "";
            txtBtch.Text = "";
            txtProgramNM.Text = "";
            txtRemark.Text = "";
            txtCrsID.Text = "";
            txtCrditHr.Text = "";
            txtCrditCst.Text = "";
            txtRmrk.Text = "";
            txtStuIDEdit.Text = "";

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                btnInsert.Text = "Update";
                btnPrint.Text = "Update & Print";
                EditDataClear();
                btnEdit.Text = "New Entry";
                txtStuIDEdit.Visible = true;
                txtStuID.Visible = false;
                ///ddlYr.Enabled = false;
                txtDate.Enabled = false;
                //ddlSemNM.Enabled = false;
                ddlProgNM.Visible = false;
                txtProgramNM.Visible = true;
                txtSSN.Enabled = false;
                txtBtch.Enabled = false;
                txtRmrk.Enabled = false;
                gv_Preview.Visible = false;
                txtStuIDEdit.BorderStyle = BorderStyle.Double;
                txtStuIDEdit.BorderColor = Color.LightBlue;
                GridView1.Visible = false;
                lbl1.Visible = false;
                lbl2.Visible = false;
                btnAdd.Visible = false;
                btnPrintEdit.Visible = true;
                btnDLT.Visible = true;
                lblMSG.Text = "";
                txtStuIDEdit.Enabled = true;
                ddlYr.Focus();
            }
            else
            {
                btnInsert.Text = "Save";
                btnPrint.Text = "Save & Print";
                EditDataClear();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnEdit.Text = "Edit";
                txtStuIDEdit.Visible = false;
                txtStuID.Visible = true;
                ddlYr.Enabled = true;
                txtDate.Enabled = true;
                ddlSemNM.Enabled = true;
                ddlProgNM.Visible = true;
                lblPreview.Visible = false;
                lblPreviewTXT.Visible = false;
                txtProgramNM.Visible = false;
                txtSSN.Enabled = true;
                txtBtch.Enabled = true;
                txtRmrk.Enabled = true;
                ddlSemNM.Focus();
                GridView1.Visible = false;
                lbl1.Visible = false;
                lbl2.Visible = true;
                btnAdd.Visible = true;
                btnPrintEdit.Visible = true;
                btnEditAdd.Visible = false;
                btnDLT.Visible = false;
                btnPrintEdit.Visible = false;
                lblMSG.Text = "";
            }
        }

        protected void txtStuIDEdit_TextChanged(object sender, EventArgs e)
        {
            if (txtStuIDEdit.Text != "")
            {

                Label lblStuEditID = new Label();
                Label lblSemEditID = new Label();
                Label lblSemNMEditID = new Label();
                Label lblProgEditID = new Label();
                Label lblProgNMEditID = new Label();
                ddlYr.Enabled = true;
                txtDate.Enabled = true;
                ddlSemNM.Enabled = true;
                txtSSN.Enabled = true;
                txtBtch.Enabled = true;
                txtRmrk.Enabled = true;
                ddlSemNM.Enabled = false;
                ddlYr.Enabled = false;
                txtStuIDEdit.Enabled = false;

                // Global.lblAdd("SELECT REGYY FROM EIM_COURSEREG WHERE STUDENTID='" + txtStuIDEdit.Text + "'", lblStuEditID);
                Global.txtAdd("SELECT convert(nvarchar(10),ENRLDT,103) FROM EIM_CREGMST WHERE STUDENTID='" + txtStuIDEdit.Text + "'", txtDate);
                // ddlYr.Text = lblStuEditID.Text;
                Global.txtAdd("SELECT CONVERT(NVARCHAR(10),ENRLDT,103) AS DT FROM EIM_CREGMST WHERE STUDENTID='" + txtStuIDEdit.Text + "'", txtDate);
                // Global.lblAdd("SELECT SEMESTERNM FROM EIM_SEMESTER WHERE SEMESTERID='" + lblSemEditID.Text + "'", lblSemNMEditID);
                //ddlSemNM.Text = lblSemNMEditID.Text;
                Global.lblAdd("SELECT PROGRAMID FROM EIM_COURSEREG WHERE STUDENTID='" + txtStuIDEdit.Text + "'", lblProgID);
                Session["ProgID"] = lblProgID.Text;
                Global.lblAdd("SELECT PROGRAMNM FROM EIM_PROGRAM WHERE PROGRAMID='" + lblProgID.Text + "'", lblProgNMEditID);
                txtProgramNM.Text = lblProgNMEditID.Text;
                // ddlProgNM.Items.Add(lblProgNMEditID.Text);
                Global.txtAdd("SELECT SESSION FROM EIM_CREGMST WHERE STUDENTID='" + txtStuIDEdit.Text + "'", txtSSN);
                Global.txtAdd("SELECT BATCH FROM EIM_CREGMST WHERE STUDENTID='" + txtStuIDEdit.Text + "'", txtBtch);
                Global.txtAdd("SELECT REMARKS FROM EIM_CREGMST WHERE STUDENTID='" + txtStuIDEdit.Text + "'", txtRmrk);
                GridView1.Visible = true;
                lbl1.Visible = true;
                btnEditAdd.Visible = true;
                btnNext.Visible = true;
                GridView1.Enabled = true;
                gridShow1();
            }
            else
            {

                GridViewEQ.Visible = false;
                EditDataClear();
                ddlYr.Enabled = false;
                txtDate.Enabled = false;
                ddlSemNM.Enabled = false;
                txtSSN.Enabled = false;
                txtBtch.Enabled = false;
                txtRmrk.Enabled = false;
                GridView1.Visible = false;
                lbl1.Visible = false;
                btnEditAdd.Visible = false;

                txtStuIDEdit.Focus();
                txtStuIDEdit.BorderStyle = BorderStyle.Double;
                txtStuIDEdit.BorderColor = Color.LightBlue;
            }

        }



        protected void btnEditAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblWrng.Text != "")
                {
                    txtCrsID.Focus();
                }
                if (txtCrsID.Text == "")
                {
                    txtCrsID.Focus();
                }
                else if (txtCrditHr.Text == "")
                {
                    txtCrditHr.Focus();
                }
                else if (txtCrditCst.Text == "")
                {
                    txtCrditCst.Focus();
                }
                else
                {

                    string UserID = Session["UserName"].ToString();
                    string Ipaddress = Session["IpAddress"].ToString();
                    string PcName = Session["PCName"].ToString();
                    DateTime InTime = Global.Dayformat1(DateTime.Now);
                    Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProgID);
                    Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    string Date = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal).ToString("yyyy-MM-dd");
                    SqlCommand cmd1 = new SqlCommand(@"INSERT INTO EIM_COURSEREG (REGYY,SEMESTERID,SEMID,PROGRAMID,STUDENTID,ENRLDT,COURSEID,
                                             CREDITHH,CRCOST,REMARKS,USERID,USERPC,IPADDRESS,INTIME) VALUES 
                                                       (@REGYY,@SEMESTERID,@SEMID,@PROGRAMID,@STUDENTID,@ENRLDT,@COURSEID,
                                             @CREDITHH,@CRCOST,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)", con);

                    cmd1.Parameters.AddWithValue("@REGYY", int.Parse(ddlYr.Text));
                    cmd1.Parameters.AddWithValue("@SEMESTERID", int.Parse(lblSemID.Text));
                    cmd1.Parameters.AddWithValue("@SEMID", txtSemID.Text);
                    cmd1.Parameters.AddWithValue("@PROGRAMID", lblProgID.Text);
                    cmd1.Parameters.AddWithValue("@STUDENTID", txtStuIDEdit.Text);
                    cmd1.Parameters.AddWithValue("@ENRLDT", Convert.ToDateTime(Date));
                    cmd1.Parameters.AddWithValue("@COURSEID", txtCourseID.Text);
                    cmd1.Parameters.AddWithValue("@CREDITHH", txtCrditHr.Text);
                    cmd1.Parameters.AddWithValue("@CRCOST", txtCrditCst.Text);
                    cmd1.Parameters.AddWithValue("@REMARKS", txtRemark.Text);
                    cmd1.Parameters.AddWithValue("@USERID", UserID);
                    cmd1.Parameters.AddWithValue("@USERPC", PcName);
                    cmd1.Parameters.AddWithValue("@IPADDRESS", Ipaddress);
                    cmd1.Parameters.AddWithValue("@INTIME", InTime);

                    int Result = cmd1.ExecuteNonQuery();
                    if (Result == 1)
                    {
                        ddlYr.Enabled = false;
                        ddlSemNM.Enabled = false;
                        txtStuIDEdit.Enabled = false;
                    }
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();
                    gridShow1();
                    ddlSemisterNM.SelectedIndex = -1;
                    txtSemID.Text = "";
                    txtCourseID.Text = "";
                    txtCrsID.Text = "";
                    txtCrditHr.Text = "";
                    txtCrditCst.Text = "";
                    txtRemark.Text = "";
                    ddlSemisterNM.Focus();
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }
        private string SemID(string SemID)
        {
            string SemCD = "";

            if (SemID == "1st")
                SemID = "01";
            else if (SemID == "2nd")
                SemID = "02";
            else if (SemID == "3rd")
                SemID = "03";
            else if (SemID == "4th")
                SemID = "04";
            else if (SemID == "5th")
                SemID = "05";
            else if (SemID == "6th")
                SemID = "06";
            else if (SemID == "7th")
                SemID = "07";
            else if (SemID == "8th")
                SemID = "08";
            return SemID;
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblCourseID = new Label();
                Label lblCOURSEID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblCOURSEID");
                Label lblSEMID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblSEMID");
                string Semester = SemID(lblSEMID.Text);
                Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE COURSECD='" + lblCOURSEID.Text + "' AND PROGRAMID='" + lblProgID.Text + "'", lblCourseID);
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(" DELETE FROM EIM_COURSEREG WHERE STUDENTID='" + txtStuIDEdit.Text + "' AND COURSEID='" + lblCourseID.Text + "' AND SEMID='" + Semester + "'", con);
                cmd.ExecuteNonQuery();
                if (con.State != ConnectionState.Closed)con.Close();
                gridShow1();
                txtCrsID.Focus();
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            gridShow1();
            DropDownList ddlSemisterNMEdit = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlSemisterNMEdit");
            TextBox txtCOURSEIDEdit = (TextBox)GridView1.Rows[e.NewEditIndex].FindControl("txtCOURSEIDEdit");
            txtCOURSEIDEdit.Focus();
            Label lblCourseID = new Label();
            Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE COURSECD='" + txtCOURSEIDEdit.Text + "' AND PROGRAMID='" + lblProgID.Text + "'", lblCourseID);
            Session["COURSEID"] = lblCourseID.Text;
            ddlSemisterNMEdit.Focus();
        }

        protected void txtCOURSEIDEdit_TextChanged(object sender, EventArgs e)
        {
            Label lblCourseID = new Label();
            lblCourseID.Text = "";
            GridViewRow Row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCREDITHHEdit = (TextBox)Row.FindControl("txtCREDITHHEdit");
            TextBox txtCOURSEIDEdit = (TextBox)Row.FindControl("txtCOURSEIDEdit");
            TextBox txtCRCOSTEdit = (TextBox)Row.FindControl("txtCRCOSTEdit");
            TextBox txtREMARKSEdit = (TextBox)Row.FindControl("txtREMARKSEdit");
            Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProgID);
            Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE COURSECD='" + txtCOURSEIDEdit.Text + "'  AND PROGRAMID='" + lblProgID.Text + "'", lblCourseID);


            lblWrng.Text = "";
            string Match = lblCourseID.Text.Substring(0, 2);
            if (lblProgID.Text != Match)
            {
                lblMSGCourse.Visible = true;
                lblMSGCourse.Text = "Invalid Course CD For Selected Program!";
                txtCOURSEIDEdit.Focus();
            }
            else
            {
                lblMSGCourse.Visible = false;

                Global.txtAdd("SELECT CREDITHH FROM EIM_COURSE WHERE PROGRAMID='" + lblProgID.Text + "' and COURSEID='" + lblCourseID.Text + "'", txtCREDITHHEdit);
                int CR = int.Parse(txtCREDITHHEdit.Text);
                Global.txtAdd("SELECT (COSTPERCR*'" + CR + "') FROM EIM_PROGRAM WHERE PROGRAMID='" + lblProgID.Text + "'", txtCRCOSTEdit);
                txtREMARKSEdit.Focus();

            }
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    TextBox txtCREDITHHEdit = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCREDITHHEdit");
                    TextBox txtCOURSEIDEdit = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCOURSEIDEdit");
                    TextBox txtCRCOSTEdit = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCRCOSTEdit");
                    TextBox txtREMARKSEdit = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                    DropDownList ddlSemisterNMEdit = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlSemisterNMEdit");
                    if (txtCREDITHHEdit.Text == "")
                        txtCREDITHHEdit.Focus();
                    else if (txtCOURSEIDEdit.Text == "")
                        txtCOURSEIDEdit.Focus();
                    else
                    {
                        String Sem = ddlSemisterNMEdit.SelectedValue;
                        Label lblCourseID = new Label();
                        lblCourseID.Text = "";
                        string COURSEID = Session["COURSEID"].ToString();
                        Global.lblAdd("SELECT COURSEID FROM EIM_COURSE WHERE COURSECD='" + txtCOURSEIDEdit.Text + "' AND PROGRAMID='" + lblProgID.Text + "'", lblCourseID);
                        iob.UPDUserID = Session["UserName"].ToString();
                        iob.UPDIpaddress = Session["IpAddress"].ToString();
                        iob.UPDPcName = Session["PCName"].ToString();
                        iob.UPDTime = Global.Dayformat1(DateTime.Now);
                        if (con.State != ConnectionState.Open)con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE EIM_COURSEREG SET SEMID=@SEMID,COURSEID=@COURSEID,CREDITHH=@CREDITHH,CRCOST=@CRCOST,REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE COURSEID='" + COURSEID + "' AND STUDENTID='" + txtStuIDEdit.Text + "'", con);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("SEMID", Sem);
                        cmd.Parameters.AddWithValue("COURSEID", lblCourseID.Text);
                        cmd.Parameters.AddWithValue("CREDITHH", txtCREDITHHEdit.Text);
                        cmd.Parameters.AddWithValue("CRCOST", txtCRCOSTEdit.Text);
                        cmd.Parameters.AddWithValue("REMARKS", txtREMARKSEdit.Text);
                        cmd.Parameters.AddWithValue("UPDUSERID", iob.UPDUserID);
                        cmd.Parameters.AddWithValue("UPDUSERPC", iob.UPDPcName);
                        cmd.Parameters.AddWithValue("UPDIPADDRESS", iob.UPDIpaddress);
                        cmd.Parameters.AddWithValue("UPDTIME", iob.UPDTime);
                        cmd.ExecuteNonQuery();
                        ddlYr.Enabled = false;
                        ddlSemNM.Enabled = false;
                        txtStuIDEdit.Enabled = false;
                        if (con.State != ConnectionState.Closed)con.Close();
                        GridView1.EditIndex = -1;
                        gridShow1();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
        protected void btnPrintEdit_Click(object sender, EventArgs e)
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

            Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProgID);
            Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
            Session["REGYR"] = ddlYr.Text;
            Session["DATE"] = txtDate.Text;
            Session["SEMESTERNM"] = ddlSemNM.Text;
            Session["PROHRAMNM"] = ddlProgNM.Text;
            if (btnEdit.Text == "Edit")
                Session["STUDENTID"] = txtStuID.Text;
            else
                Session["STUDENTID"] = txtStuIDEdit.Text;
            Session["BATCH"] = txtBtch.Text;
            Session["SESSION"] = txtSSN.Text;
            Session["SEMESTERID"] = lblSemID.Text;
            Session["PROHRAMID"] = lblProgID.Text;
            if (txtStuIDEdit.Text == "")
                txtStuIDEdit.Focus();
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                            "OpenWindow", "window.open('/Admission/Report/CourseRegPrint.aspx','_newtab');", true);
            }

        }
        protected void btnDLT_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(" DELETE FROM EIM_COURSEREG WHERE STUDENTID='" + txtStuIDEdit.Text + "'", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand(" DELETE FROM EIM_CREGMST WHERE STUDENTID='" + txtStuIDEdit.Text + "'", con);
                cmd1.ExecuteNonQuery();
                if (con.State != ConnectionState.Closed)con.Close();
                EditDataClear();
                txtStuIDEdit.Visible = true;
                txtStuID.Visible = false;
                ddlYr.Enabled = false;
                txtDate.Enabled = false;
                ddlSemNM.Enabled = false;
                ddlProgNM.Visible = false;
                txtProgramNM.Visible = true;
                txtSSN.Enabled = false;
                txtBtch.Enabled = false;
                txtRmrk.Enabled = false;
                txtStuIDEdit.Focus();
                txtStuIDEdit.BorderStyle = BorderStyle.Double;
                txtStuIDEdit.BorderColor = Color.LightBlue;
                GridView1.Visible = false;
                lbl1.Visible = false;
                lbl2.Visible = false;
                btnAdd.Visible = false;
                btnEditAdd.Visible = false;
                lblMSG.Visible = true;
                lblMSG.Text = "Deleted !";
                gridShow1();
                txtStuIDEdit.Focus();
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            EditDataClear();
            txtStuIDEdit.Visible = true;
            txtStuID.Visible = false;
            ddlYr.Enabled = true;
            txtDate.Enabled = false;
            ddlSemNM.Enabled = true;
            ddlProgNM.Visible = false;
            txtProgramNM.Visible = true;
            txtSSN.Enabled = false;
            txtBtch.Enabled = false;
            txtRmrk.Enabled = false;
            ddlYr.Focus();
            txtStuIDEdit.BorderStyle = BorderStyle.Double;
            txtStuIDEdit.BorderColor = Color.LightBlue;
            GridView1.Visible = false;
            lbl1.Visible = false;
            lbl2.Visible = false;
            btnAdd.Visible = false;
            btnPrintEdit.Visible = true;
            btnDLT.Visible = true;
            lblMSG.Text = "";
            txtStuIDEdit.Enabled = true;
            btnNext.Visible = false;
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            gridShow1();
        }
        protected void ddlSemisterNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemisterNM.Text != "Select")
            {
                txtSemID.Text = ddlSemisterNM.SelectedValue;
                txtCrsID.Focus();
            }

        }
    }
}
