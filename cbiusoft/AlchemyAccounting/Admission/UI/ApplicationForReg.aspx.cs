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
    public partial class ApplicationForReg : System.Web.UI.Page
    {
        int ClickCount = 1;
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
                    string Type = Session["UserTp"].ToString();
                    if (Type != "ADMIN")
                        txtStudentIDOld.Enabled = false;
                    else
                        txtStudentIDOld.Enabled = true;
                    Global.BindDropDown(ddlSemNM, @" SELECT  SEMESTERNM NM ,SEMESTERID ID FROM EIM_SEMESTER");
                    Global.BindDropDown(ddlProNM, @" SELECT  PROGRAMNM NM ,PROGRAMID ID FROM EIM_PROGRAM");
                    Global.BindDropDown(ddlStudentIDOld, @"SELECT        NEWSTUDENTID NM,dbo.EIM_STUDENT.STUDENTID+'|'+dbo.EIM_STUDENT.STUDENTNM+'|'+dbo.EIM_STUDENT.PROGRAMID+'|'+dbo.EIM_PROGRAM.PROGRAMNM ID
                    FROM dbo.EIM_STUDENT INNER JOIN dbo.EIM_PROGRAM ON dbo.EIM_STUDENT.PROGRAMID = dbo.EIM_PROGRAM.PROGRAMID ORDER BY NEWSTUDENTID");
                    // Global.dropDownAdd(ddlAdmYR, "SELECT DISTINCT(TESTYY) FROM EIM_ADMTEST");

                    string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    int i, m;
                    int a = int.Parse(yr);
                    m = a + 5;
                    for (i = a - 5; i <= m; i++)
                    {
                        ddlAdmYR.Items.Add(i.ToString());
                    }
                    string btc = "";
                    ddlBCH.Items.Clear();
                    ddlBCH.Items.Add("--SELECT--");
                    for (i = 1; i < 1000; i++)
                    {
                        if (i < 10)
                            btc = "00" + i;
                        else if(i<100)
                            btc = "0" + i;
                        else
                            btc = i.ToString();
                        ddlBCH.Items.Add(btc);
                    }
                    ddlAdmYR.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    lblCOUNT.Text = "0";
                    lblGRDCount.Text = "0";
                    ddlAdmYR.Focus();
                    int Count1 = GridViewEQ.Rows.Count;
                    if (Count1 > 0)
                        dt.Clear();
                    Session["datatable"] = "";
                }


            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionStudentID(string prefixText, int count, string contextKey)
        {


            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ("SELECT  TOP 20 NEWSTUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID LIKE '" + prefixText + "%'");

            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["NEWSTUDENTID"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        } 

        protected void ddlSemNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemNM.SelectedItem.ToString() == "--SELECT--")
                ddlSemNM.Focus();  
            if (ddlBCH.SelectedItem.ToString() == "--SELECT--")
                ddlBCH.Focus(); 
            else
            {
                //string batchNo = "";
                //int YY = Convert.ToInt16(ddlAdmYR.Text);
                //int sem = Convert.ToInt16(ddlSemNM.Text);
                //int x = (YY - 2014) + 1;
                //int batch = x * 2;
                //if (sem == 1)
                //    batch = batch - 1;
                //if (batch < 10)
                //    batchNo = "00" + batch;
                //else if (batch < 100)
                //    batchNo = "0" + batch;
                //else
                //    batchNo = batch.ToString();
                //txtBCH.Text = batchNo;
                //Global.lblAdd("select SEMESTERID from EIM_SEMESTER where SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
                StuID(); 
                txtSession.Focus();
            }
        }
        private void StuID()
        {
            string RL_ADD = ddlAdmYR.Text + ddlSemNM.Text + ddlProNM.Text;
            //string StuIDs;
            Global.lblAdd("SELECT MAX(STUDENTID) from EIM_STUDENT where SEMESTERID='" + ddlSemNM.Text + "' and PROGRAMID='" + ddlProNM.Text + "' and ADMITYY='" + ddlAdmYR.Text + "'", lblStuID);
            int ID = 0;
            string StuID = "";
            if (lblStuID.Text != "")
            {
                StuID = lblStuID.Text.Substring(7, 4);
                ID = int.Parse(StuID) + 1;
            }
            if (lblStuID.Text == "")
            {
                iob.StuID = RL_ADD + "0001";
                txtStuMaxID.Text = iob.StuID;
            }
            else
            {
                if (ID < 10)
                {
                    iob.StuID = RL_ADD + "000" + ID;
                    txtStuMaxID.Text = iob.StuID;
                }
                else if (ID < 100)
                {
                    iob.StuID = RL_ADD + "00" + ID;
                    txtStuMaxID.Text = iob.StuID;
                }
                else if (ID < 1000)
                {
                    iob.StuID = RL_ADD + "0" + ID;
                    txtStuMaxID.Text = iob.StuID;
                }
                else if (ID < 10000)
                {
                    iob.StuID = RL_ADD + ID;
                    txtStuMaxID.Text = iob.StuID;
                }
            }
        }
        private void StuIDNEW()
        {
            string RL_ADD = ddlAdmYR.Text.Substring(2, 2) + ddlBCH.Text + ddlProNM.Text;
            //string StuIDs; 
            Label lblStuIDNew = new Label();
            Global.lblAdd("SELECT MAX(SUBSTRING(NEWSTUDENTID,8,5)) from EIM_STUDENT WHERE PROGRAMID='" + ddlProNM.Text + "'", lblStuIDNew);
            Int64 ID = 0;
            string StuID = "";
            if (lblStuIDNew.Text == "")
                StuID = RL_ADD + "00001";
            else
            {
                ID = Convert.ToInt64(lblStuIDNew.Text) + 1;
                if (ID < 10)
                    StuID = RL_ADD + "0000" + ID;
                else if (ID < 100)
                    StuID = RL_ADD + "000" + ID;
                else if (ID < 1000)
                    StuID = RL_ADD + "00" + ID;
                else if (ID < 10000)
                    StuID = RL_ADD + "0" + ID;
                else
                    StuID = RL_ADD + ID;
            }
            txtStuIdNew.Text = StuID;
        }
        private void Clear()
        {
            txtStuIdNew.Text = "";
            txtStudentIDOld.Text = "";
            lblProgIDMigrate.Text = "";
            txtProgNMMigrate.Text = "";
            txtStudentNameOld.Text = "";
            txtMigrateDT.Text = "";
            ddlAdmtTP.SelectedIndex = -1;
            ddlGNDR.SelectedIndex = -1;
            ddlHSTL.SelectedIndex = -1;
            ddlMSTTS.SelectedIndex = -1;
            ddlPRcdnc.SelectedIndex = -1;
            ddlProNM.SelectedIndex = -1;
            ddlReli.SelectedIndex = -1;
            ddlSemNM.SelectedIndex = -1;
            txtAdmDT.Text = "";
            ddlBCH.SelectedIndex = -1;
            txtBG.Text = "";
            txtDOB.Text = "";
            txtEML.Text = "";
            txtEXP.Text = "";
            txtFOcup.Text = "";
            txtFOcupDTL.Text = "";
            txtGAdrs1.Text = "";
            txtGAdrs2.Text = "";
            txtGML.Text = "";
            txtGMoNo.Text = "";
            txtGNM.Text = "";
            txtGTelNO.Text = "";
            txtGurREL.Text = "";
            txtINCM.Text = "";
            txtMOcup.Text = "";
            txtMoNO.Text = "";
            //txtNatin.Text = "";
            txtNPidNO.Text = "";
            txtPerAdrs1.Text = "";
            txtPerAdrs2.Text = "";
            txtPOB.Text = "";
            txtPreAdrs1.Text = "";
            txtPreAdrs2.Text = "";
            txtPreIns.Text = "";
            txtPreProgNM.Text = "";
            txtPreSSN.Text = "";
            txtSession.Text = "";
            txtSPNM.Text = "";
            txtSPOcup.Text = "";
            txtStuFNM.Text = "";
            txtStuMNM.Text = "";
            txtStuNM.Text = "";
            txtStuTP.Text = "";
            txtTelNO.Text = "";
            txtXmNM.Text = "";
            txtPYr.Text = "";
            txtXmRl.Text = "";
            txtInsNM.Text = "";
            txtGPA.Text = "";
            txtGrop.Text = "";
            txtBrd.Text = "";
            txtStuMaxID.Text = "";
            lblImagePath.Text = "";
        }
        private void KeepClickCount()
        {
            lblGRDCount.Text = "0";
            if (ViewState["Clicks"] != null)
            {
                ClickCount = (int)ViewState["Clicks"] + 1;
            }
            lblCOUNT.Text = ClickCount.ToString();
            ViewState["Clicks"] = ClickCount;
        }
        private void KeepClickCountGrid()
        {

            if (ViewState["Clicks"] != null)
            {
                ClickCount = (int)ViewState["Clicks"] + 1;
            }
            lblGRDCount.Text = ClickCount.ToString();
            ViewState["Clicks"] = ClickCount;
        }
        private void NullChack()
        {
            if (txtFOcupDTL.Text == "")
                iob.FOcupDTL = "";
            if (txtSPOcup.Text == "")
                iob.SPuseNM = "";
            if (txtSPOcup.Text == "")
                iob.SpuseOcup = "";
            if (txtPerAdrs1.Text == "")
                iob.PerAdrs = "";
            if (txtTelNO.Text == "")
                iob.TelePhn = "";
            if (txtGNM.Text == "")
                iob.GNM = "";
            if (txtGurREL.Text == "")
                iob.GRel = "";
            if (txtGAdrs1.Text == "")
                iob.GAdrs = "";
            if (txtGMoNo.Text == "")
                iob.GMNo = "";
            if (txtGML.Text == "")
                iob.GEml = "";
            if (txtGTelNO.Text == "")
                iob.GTelePhn = "";
            if (txtPreProgNM.Text == "")
                iob.PreProNM = "";
            if (txtPreIns.Text == "")
                iob.PreInsNM = "";
            if (txtPreSSN.Text == "")
                iob.PreSSN = "";
            if (txtFirmNM.Text == "")
                iob.FIRMNM = "";
            if (txtPosisn.Text == "")
                iob.PosiSN = "";
            if (txtSession.Text == "")
                iob.SeSN = "";
            if (ddlBCH.Text == "--SELECT--")
                iob.Batch = "";
            if (ddlAdmtTP.Text == "Select")
                iob.ADMITTP = "";
            if (txtStuFNM.Text == "")
                iob.StuFNM = "";
            if (txtFOcup.Text == "")
                iob.FOcup = "";
            if (txtStuMNM.Text == "")
                iob.StuMNM = "";
            if (txtMOcup.Text == "")
                iob.MOcup = "";
            if (txtPreAdrs1.Text == "")
                iob.PreAdrs = "";
            if (txtEML.Text == "")
                iob.Email = "";
            if (txtPosisn.Text == "")
                iob.PosiSN = "";
            if (ddlReli.Text == "Select")
                iob.Religion = "";
            if (txtDOB.Text == "")
                iob.DtOfBrt = "";
            if (ddlGNDR.Text == "Select")
                iob.Gander = "";
            if (txtStuTP.Text == "")
                iob.StuTP = "";
            if (txtPOB.Text == "")
                iob.PofB = "";
            if (txtBG.Text == "")
                iob.BldGRP = "";
            if (txtNPidNO.Text == "")
                iob.NIDPNO = "";
            if (ddlPRcdnc.Text == "Select")
                iob.PRecdnc = "";
            if (ddlMSTTS.Text == "Select")
                iob.MSTTS = "";
            if (ddlHSTL.Text == "Select")
                iob.Hstl = "";
            //if (txtINCM.Text == "")
            //    iob.Incm = 0;
            //if (txtEXP.Text == "")
            //    iob.Expncy = 0;


        }
        private void INSERT()
        {
            StuID();
            Session["SL"] = null;
            Session["a"] = null;
            //KeepClickCount();
            iob.UserID = Session["UserName"].ToString();
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.PcName = Session["PCName"].ToString();
            iob.InTime = Global.Dayformat1(DateTime.Now);
            if (ddlAdmYR.Text == "Select")
                ddlAdmYR.Focus();
            else if (txtAdmDT.Text == "")
                txtAdmDT.Focus();
            else if (ddlSemNM.SelectedItem.ToString() == "--SELECT--")
                ddlSemNM.Focus();
            else if (ddlProNM.Text == "Select")
                ddlProNM.Focus();
            else if (txtStuMaxID.Text == "")
                ddlProNM.Focus();
            else if (txtStuIdNew.Text == "")
                ddlProNM.Focus();
            else if (txtStuNM.Text == "")
                txtStuNM.Focus();
            else if (txtMoNO.Text == "")
                txtMoNO.Focus();
            else if (Session["ALERT4DATE"].ToString() == "ALERT")
            {
                lblMSG1.Visible = true;
                lblMSG.Text = "Select Date !";
                txtMigrateDT.Focus();
            }
            else
            {
                if (FileUpload1.HasFile)
                {
                    string fileName = FileUpload1.FileName;
                    string exten = Path.GetExtension(fileName);
                    //here we have to restrict file type            
                    exten = exten.ToLower();
                    string[] acceptedFileTypes = new string[4];
                    acceptedFileTypes[0] = ".jpg";
                    acceptedFileTypes[1] = ".jpeg";
                    //acceptedFileTypes[2] = ".gif";
                    acceptedFileTypes[3] = ".png";
                    bool acceptFile = false;
                    for (int i = 0; i <= 3; i++)
                    {
                        if (exten == acceptedFileTypes[i])
                        {
                            acceptFile = true;
                        }
                    }
                    if (!acceptFile)
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "upload Image is not a permitted file type!";
                        FileUpload1.Focus();
                    }
                    else
                    {
                        //upload the file onto the server                   
                        FileUpload1.SaveAs(Server.MapPath("~/Admission/Images/" + fileName));
                        iob.Img = "~/Admission/Images/" + fileName;
                    }
                }
                else
                    iob.Img = "";

                lblAdmtSL.Text = "";
                Global.lblAdd("SELECT MAX(ADMITSL) FROM EIM_STUDENT", lblAdmtSL);
                if (lblAdmtSL.Text == "")
                    iob.AdmtSL = 1;
                else
                    iob.AdmtSL = int.Parse(lblAdmtSL.Text) + 1;
                DateTime Date = DateTime.Parse(txtAdmDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                iob.AdmtYR = ddlAdmYR.Text;
                //iob.Img = txtAdmDT.Text;
                if (txtStudentIDOld.Text == "")
                    txtMigrateDT.Text = "";
                if (txtMigrateDT.Text == "")
                    txtMigrateDT.Text = "1900/01/01";
                iob.MigratDT = DateTime.Parse(txtMigrateDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                iob.ADMITDT = Date;
                iob.ADMITTP = ddlAdmtTP.Text;
                iob.SemID = int.Parse(ddlSemNM.Text);
                iob.ProgID = ddlProNM.Text;
                iob.SeSN = txtSession.Text;
                iob.Batch = ddlBCH.Text;
                iob.StuID = txtStuMaxID.Text;
                iob.StuIDNew = txtStuIdNew.Text;
                iob.StuNM = txtStuNM.Text;
                iob.StuFNM = txtStuFNM.Text;
                iob.FOcup = txtFOcup.Text;
                iob.FOcupDTL = txtFOcupDTL.Text;
                iob.StuMNM = txtStuMNM.Text;
                iob.MOcup = txtMOcup.Text;
                iob.SPuseNM = txtSPNM.Text;
                iob.SpuseOcup = txtSPOcup.Text;
                iob.PreAdrs = txtPreAdrs1.Text + " " + txtPreAdrs2.Text;
                iob.PerAdrs = txtPerAdrs1.Text + " " + txtPerAdrs2.Text;
                iob.TelePhn = txtTelNO.Text;
                iob.MobNO = txtMoNO.Text;
                iob.Email = txtEML.Text;
                iob.Nation = txtNatin.Text;
                iob.Religion = ddlReli.Text;
                iob.DtOfBrt = txtDOB.Text;
                iob.Gander = ddlGNDR.Text;
                iob.StuTP = txtStuTP.Text;
                iob.PofB = txtPOB.Text;
                iob.NIDPNO = txtNPidNO.Text;
                iob.BldGRP = txtBG.Text;
                iob.PRecdnc = ddlPRcdnc.Text;
                iob.MSTTS = ddlMSTTS.Text;
                iob.Hstl = ddlHSTL.Text;
                if (txtINCM.Text == "")
                    txtINCM.Text = "0";
                iob.Incm = Decimal.Parse(txtINCM.Text);
                if (txtEXP.Text == "")
                    txtEXP.Text = "0";
                iob.Expncy = Decimal.Parse(txtEXP.Text);
                iob.GNM = txtGNM.Text;
                iob.GRel = txtGurREL.Text;
                iob.GAdrs = txtGAdrs1.Text + " " + txtGAdrs2.Text;
                iob.GTelePhn = txtGTelNO.Text;
                iob.GMNo = txtGMoNo.Text;
                iob.GEml = txtGML.Text;
                iob.PreProTP = "";
                iob.PreProNM = txtPreProgNM.Text;
                iob.PreInsNM = txtPreIns.Text;
                iob.PreSSN = txtPreSSN.Text;
                iob.FIRMNM = txtFirmNM.Text;
                iob.PosiSN = txtPosisn.Text;
                NullChack();
                dob.InsertApplicationReg(iob);
                lblSL.Text = "";
                int Sl = 1;

                foreach (GridViewRow row in GridViewEQ.Rows)
                {
                    int Count = GridViewEQ.Rows.Count;
                    //for (int i = 1; i <= Count; i++)
                    //{
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            if (con.State != ConnectionState.Open)con.Open();
                        SqlCommand cmd1 = new SqlCommand(@"INSERT INTO EIM_STUEDUQ (STUDENTID,NEWSTUDENTID,EXAMSL,EXAMNM,PASSYY,EXAMROLL,
                                             GROUPSUB,DIVGRADE,INSTITUTE,BOARDUNI) VALUES 
                                                       (@STUDENTID,@EXAMSL,@EXAMNM,@PASSYY,@EXAMROLL,
                                             @GROUPSUB,@DIVGRADE,@INSTITUTE,@BOARDUNI)", con);

                        cmd1.Parameters.AddWithValue("@STUDENTID", iob.StuID);
                        cmd1.Parameters.AddWithValue("@NEWSTUDENTID", iob.StuIDNew);
                        cmd1.Parameters.AddWithValue("@EXAMSL", Sl);
                        cmd1.Parameters.AddWithValue("@EXAMNM", row.Cells[1].Text);
                        cmd1.Parameters.AddWithValue("@PASSYY", row.Cells[2].Text);
                        cmd1.Parameters.AddWithValue("@EXAMROLL", row.Cells[3].Text);

                        cmd1.Parameters.AddWithValue("@GROUPSUB", row.Cells[4].Text);
                        cmd1.Parameters.AddWithValue("@DIVGRADE", row.Cells[5].Text);
                        cmd1.Parameters.AddWithValue("@INSTITUTE", row.Cells[6].Text);
                        cmd1.Parameters.AddWithValue("@BOARDUNI", row.Cells[7].Text);


                        cmd1.ExecuteNonQuery();
                        ddlAdmYR.Focus();
                        Sl++;
                        if (con.State != ConnectionState.Closed)
                            if (con.State != ConnectionState.Closed)con.Close();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex);
                    }
                }



                GridViewEQ.Visible = false;




            }
        }
        protected void btnSUBMIT_Click(object sender, EventArgs e)
        {
            lblMSG1.Visible = false;
            try
            {
                Session["ALERT4DATE"] = "";
                if (txtStudentIDOld.Text != "")
                    if (txtMigrateDT.Text == "")
                        Session["ALERT4DATE"] = "ALERT";
                    else
                        iob.MigratDT = DateTime.Parse(txtMigrateDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                iob.ProgIDMigrate = lblProgIDMigrate.Text;
                iob.StuIDMigrate = txtStudentIDOld.Text;
                iob.StuIDMigrateNew = ddlStudentIDOld.SelectedItem.ToString();
                if (btnSUBMIT.Text == "Submit")
                {
                    
                    Session["InsertApplicationReg"] = "";
                    INSERT();
                    if (Session["InsertApplicationReg"].ToString() != "")
                    {
                        Clear();
                        Session["datatable"] = null;
                        int Count1 = GridViewEQ.Rows.Count;
                        if (Count1 > 0)
                            dt.Clear();
                        Session["datatable"] = "";
                        ddlAdmYR.Focus();
                        lblMSG1.Visible = true;
                        lblMSG1.Text = "Inserted !";
                        Response.Write("<script>alert('Successfully Inserted !')</script>");
                    }
                }
                else
                {
                    lblMSG1.Visible = false;
                    // Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProNM.Text + "'", lblProID);
                    //Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
                    iob.UPDUserID = Session["UserName"].ToString();
                    iob.UPDIpaddress = Session["IpAddress"].ToString();
                    iob.UPDPcName = Session["PCName"].ToString();
                    iob.UPDTime = Global.Dayformat1(DateTime.Now);
                    if (ddlAdmYR.Text == "Select")
                        ddlAdmYR.Focus();
                    //else if (!FileUpload1.HasFile)
                    //{
                    //    FileUpload1.Focus();
                    //}
                    else if (txtAdmDT.Text == "")
                        txtAdmDT.Focus();
                    else if (ddlSemNM.SelectedItem.ToString() == "--SELECT--")
                        ddlSemNM.Focus();
                    else if (ddlProNM.Text == "Select")
                        ddlProNM.Focus();
                    else if (txtStuMaxID.Text == "")
                        txtStuMaxID.Focus();
                    else if (txtStuNM.Text == "")
                        txtStuNM.Focus();
                    else if (txtMoNO.Text == "")
                        txtMoNO.Focus();
                    else if (Session["ALERT4DATE"].ToString() == "ALERT")
                    {
                        lblMSG1.Visible = true;
                        lblMSG.Text = "Select Date !";
                        Response.Write("<script>alert('Select Date !')</script>");
                        txtMigrateDT.Focus();
                    }
                    else
                    {
                        if (FileUpload1.HasFile)
                        {
                            string fileName = FileUpload1.FileName;
                            string exten = Path.GetExtension(fileName);
                            //here we have to restrict file type            
                            exten = exten.ToLower();
                            string[] acceptedFileTypes = new string[4];
                            acceptedFileTypes[0] = ".jpg";
                            acceptedFileTypes[1] = ".jpeg";
                            //acceptedFileTypes[2] = ".gif";
                            acceptedFileTypes[3] = ".png";
                            bool acceptFile = false;
                            for (int i = 0; i <= 3; i++)
                            {
                                if (exten == acceptedFileTypes[i])
                                {
                                    acceptFile = true;
                                }
                            }
                            if (!acceptFile)
                            {
                                lblMSG.Visible = true;
                                lblMSG.Text = "upload Image is not a permitted file type!";
                                Response.Write("<script>alert('upload Image is not a permitted file type !')</script>");
                                FileUpload1.Focus();
                            }
                            else
                            {
                                //upload the file onto the server                   
                                FileUpload1.SaveAs(Server.MapPath("~/Admission/Images/" + fileName));
                                iob.Img = "~/Admission/Images/" + fileName;
                            }
                        }
                        else
                            iob.Img = lblImagePath.Text;

                        lblAdmtSL.Text = "";
                        Global.lblAdd("SELECT MAX(ADMITSL) FROM EIM_STUDENT", lblAdmtSL);
                        if (lblAdmtSL.Text == "")
                        {
                            iob.AdmtSL = 1;
                        }
                        else
                        {
                            iob.AdmtSL = int.Parse(lblAdmtSL.Text) + 1;
                        }
                        DateTime Date = DateTime.Parse(txtAdmDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.AdmtYR = ddlAdmYR.Text;
                        iob.ADMITDT = Date;
                        iob.ADMITTP = ddlAdmtTP.Text;
                        iob.SemID = int.Parse(ddlSemNM.Text);
                        iob.ProgID = ddlProNM.Text;
                        iob.SeSN = txtSession.Text;
                        iob.Batch = ddlBCH.Text;
                        //StuID();
                        iob.StuID = txtStuID.Text;
                        iob.StuIDNew = txtStuIdNew.Text;
                        iob.StuNM = txtStuNM.Text;
                        iob.StuFNM = txtStuFNM.Text;
                        iob.FOcup = txtFOcup.Text;
                        iob.FOcupDTL = txtFOcupDTL.Text;
                        iob.StuMNM = txtStuMNM.Text;
                        iob.MOcup = txtMOcup.Text;
                        iob.SPuseNM = txtSPNM.Text;
                        iob.SpuseOcup = txtSPOcup.Text;
                        iob.PreAdrs = txtPreAdrs1.Text + " " + txtPreAdrs2.Text;
                        iob.PerAdrs = txtPerAdrs1.Text + " " + txtPerAdrs2.Text;
                        iob.TelePhn = txtTelNO.Text;
                        iob.MobNO = txtMoNO.Text;
                        iob.Email = txtEML.Text;
                        iob.Nation = txtNatin.Text;
                        iob.Religion = ddlReli.Text;
                        iob.DtOfBrt = txtDOB.Text;
                        iob.Gander = ddlGNDR.Text;
                        iob.StuTP = txtStuTP.Text;
                        iob.PofB = txtPOB.Text;
                        iob.NIDPNO = txtNPidNO.Text;
                        iob.BldGRP = txtBG.Text;
                        iob.PRecdnc = ddlPRcdnc.Text;
                        iob.MSTTS = ddlMSTTS.Text;
                        iob.Hstl = ddlHSTL.Text;
                        if (txtINCM.Text == "")
                            txtINCM.Text = "0";
                        iob.Incm = Decimal.Parse(txtINCM.Text);
                        if (txtEXP.Text == "")
                            txtEXP.Text = "0";
                        iob.Expncy = Decimal.Parse(txtEXP.Text);
                        iob.GNM = txtGNM.Text;
                        iob.GRel = txtGurREL.Text;
                        iob.GAdrs = txtGAdrs1.Text + " " + txtGAdrs2.Text;
                        iob.GTelePhn = txtGTelNO.Text;
                        iob.GMNo = txtGMoNo.Text;
                        iob.GEml = txtGML.Text;
                        iob.PreProTP = "";
                        iob.PreProNM = txtPreProgNM.Text;
                        iob.PreInsNM = txtPreIns.Text;
                        iob.PreSSN = txtPreSSN.Text;
                        iob.FIRMNM = txtFirmNM.Text;
                        iob.PosiSN = txtPosisn.Text;
                        NullChack();
                        Clear();
                        txtStuID.Text = "";
                        txtStuID.Focus();
                        dob.UpdateApplicationReg(iob);
                        if (Session["UpdateApplicationReg"].ToString() != "")
                        {
                            lblMSG1.Visible = true;
                            lblMSG1.Text = "Updated !";
                            Response.Write("<script>alert('Successfully Updated !')</script>");
                        }
                    }
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //KeepClickCountGrid();

            if (txtXmNM.Text == "") 
                txtXmNM.Focus(); 
            else if (txtPYr.Text == "") 
                txtPYr.Focus(); 
            else if (txtXmRl.Text == "") 
                txtXmRl.Focus(); 
            else if (txtGrop.Text == "") 
                txtGrop.Focus(); 
            else if (txtGPA.Text == "") 
                txtGPA.Focus(); 
            else if (txtInsNM.Text == "") 
                txtInsNM.Focus(); 
            else if (txtBrd.Text == "") 
                txtBrd.Focus(); 
            else
            {
                if (Convert.ToString(ViewState["Row"]) != "")
                {
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["1"] = txtXmNM.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["2"] = txtPYr.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["3"] = txtXmRl.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["4"] = txtGrop.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["5"] = txtGPA.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["6"] = txtInsNM.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["7"] = txtBrd.Text;

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
                    dt.Columns.Add(new DataColumn("2", typeof(string)));
                    dt.Columns.Add(new DataColumn("3", typeof(string)));
                    dt.Columns.Add(new DataColumn("4", typeof(string)));
                    dt.Columns.Add(new DataColumn("5", typeof(string)));
                    dt.Columns.Add(new DataColumn("6", typeof(string)));
                    dt.Columns.Add(new DataColumn("7", typeof(string)));


                    if (Session["datatable"] != null)
                    {

                        dt = (DataTable)Session["datatable"];
                        dr = dt.NewRow();
                        // dr["RowNumber"] = i;
                        dr["1"] = txtXmNM.Text;
                        dr["2"] = txtPYr.Text;
                        dr["3"] = txtXmRl.Text;
                        dr["4"] = txtGrop.Text;
                        dr["5"] = txtGPA.Text;
                        dr["6"] = txtInsNM.Text;
                        dr["7"] = txtBrd.Text;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dt.NewRow();
                        // dr["RowNumber"] = i;
                        dr["1"] = txtXmNM.Text;
                        dr["2"] = txtPYr.Text;
                        dr["3"] = txtXmRl.Text;
                        dr["4"] = txtGrop.Text;
                        dr["5"] = txtGPA.Text;
                        dr["6"] = txtInsNM.Text;
                        dr["7"] = txtBrd.Text;
                        dt.Rows.Add(dr);
                        Session["datatable"] = dt;
                    }
                    GridViewEQ.Visible = true;
                    GridViewEQ.DataSource = dt;
                    GridViewEQ.DataBind();
                    i++;
                    txtXmNM.Focus(); 
                    txtXmNM.Text = "";
                    txtPYr.Text = "";
                    txtXmRl.Text = "";
                    txtInsNM.Text = "";
                    txtGPA.Text = "";
                    txtGrop.Text = "";
                    txtBrd.Text = "";
                }
            }
        }

        protected void GridViewEQ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                dt = (DataTable)Session["datatable"];
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



        protected void txtStuID_TextChanged(object sender, EventArgs e)
        {
            if (txtStuID.Text == "")
            {
                txtStuID.Focus();
            }
            else
            {
                if (con.State != ConnectionState.Open)con.Open();
                string Script = @"SELECT    EIM_STUDENT.NEWSTUDENTID, EIM_STUDENT.WAIVER,EIM_STUDENT.SCHOLAR, CONVERT(NVARCHAR(10),EIM_STUDENT.MIGRATEDT,103) MIGRATEDT,EIM_STUDENT.MIGRATEPID,EIM_STUDENT.MIGRATESID,EIM_STUDENT.ADMITYY, CONVERT(NVARCHAR(10),EIM_STUDENT.ADMITDT,103) AS ADMITDT, EIM_STUDENT.ADMITTP, EIM_SEMESTER.SEMESTERNM, EIM_STUDENT.SEMESTERID, EIM_PROGRAM.PROGRAMNM, 
                      EIM_STUDENT.PROGRAMID, EIM_STUDENT.SESSION, EIM_STUDENT.BATCH, EIM_STUDENT.STUDENTID, EIM_STUDENT.STUDENTNM,EIM_STUDENT.WAIVER, EIM_STUDENT.FATHERNM, EIM_STUDENT.FATHEROCP, 
                      EIM_STUDENT.FOCPDTL, EIM_STUDENT.MOTHERNM, EIM_STUDENT.MOTHEROCP, EIM_STUDENT.SPOUSENM, EIM_STUDENT.SPOUSEOCP, EIM_STUDENT.ADDRPRE, 
                      EIM_STUDENT.ADDRPER, EIM_STUDENT.TELNO, EIM_STUDENT.MOBNO, EIM_STUDENT.EMAIL, EIM_STUDENT.NATIONALITY, EIM_STUDENT.RELIGION, EIM_STUDENT.DOB, 
                      EIM_STUDENT.GENDER, EIM_STUDENT.STUDENTTP, EIM_STUDENT.BIRTHP, EIM_STUDENT.NIDPNO, EIM_STUDENT.BLOODGR, EIM_STUDENT.PRESIDENCE, EIM_STUDENT.MSTATUS, 
                      EIM_STUDENT.RESHOSTEL, EIM_STUDENT.INCOMEYY, EIM_STUDENT.EXPENSEYY, EIM_STUDENT.GUARDIANNM, EIM_STUDENT.GRELATION, EIM_STUDENT.GADDRESS, 
                      EIM_STUDENT.GTELNO, EIM_STUDENT.GMOBNO, EIM_STUDENT.GEMAIL, EIM_STUDENT.PREPROGNM, EIM_STUDENT.PPINSTITN, EIM_STUDENT.PPSESSION, 
                      EIM_STUDENT.FIRMNM, EIM_STUDENT.POSITION, EIM_STUDENT.REMARKS
                      FROM EIM_STUDENT INNER JOIN
                      EIM_SEMESTER ON EIM_STUDENT.SEMESTERID = EIM_SEMESTER.SEMESTERID INNER JOIN
                      EIM_PROGRAM ON EIM_STUDENT.PROGRAMID = EIM_PROGRAM.PROGRAMID WHERE NEWSTUDENTID='" + txtStuID.Text + "'";
                SqlCommand cmd = new SqlCommand(Script, con);
                SqlDataReader DR = cmd.ExecuteReader();
                Clear();
                while (DR.Read())
                {
                    txtMigrateDT.Text = DR["MIGRATEDT"].ToString();
                    txtStudentIDOld.Text = DR["MIGRATESID"].ToString();
                    txtStudentNameOld.Text = DR["STUDENTNM"].ToString();
                    lblProgIDMigrate.Text = DR["MIGRATEPID"].ToString();
                    ddlAdmYR.Text = DR["ADMITYY"].ToString();
                    txtAdmDT.Text = DR["ADMITDT"].ToString();
                    if (DR["ADMITTP"].ToString() == "Regular")
                        ddlAdmtTP.SelectedIndex = 0;
                    else if (DR["ADMITTP"].ToString() == "Credit Transfer")
                        ddlAdmtTP.SelectedIndex = 1;
                    else
                        ddlAdmtTP.SelectedIndex = -1;
                    //ddlAdmtTP.Text = DR["ADMITTP"].ToString();
                    ddlSemNM.Text = DR["SEMESTERID"].ToString();
                    ddlProNM.Text = DR["PROGRAMID"].ToString();
                    txtSession.Text = DR["SESSION"].ToString();
                    ddlBCH.Text = DR["BATCH"].ToString();
                    txtStuMaxID.Text = DR["STUDENTID"].ToString();
                    txtStuIdNew.Text = DR["NEWSTUDENTID"].ToString();
                    txtStuNM.Text = DR["STUDENTNM"].ToString();
                    txtStuFNM.Text = DR["FATHERNM"].ToString();
                    txtFOcup.Text = DR["FATHEROCP"].ToString();
                    txtFOcupDTL.Text = DR["FOCPDTL"].ToString();
                    txtStuMNM.Text = DR["MOTHERNM"].ToString();
                    txtMOcup.Text = DR["MOTHEROCP"].ToString();
                    txtSPNM.Text = DR["SPOUSENM"].ToString();
                    txtSPOcup.Text = DR["SPOUSEOCP"].ToString();
                    txtPreAdrs1.Text = DR["ADDRPRE"].ToString();
                    txtPerAdrs1.Text = DR["ADDRPER"].ToString();
                    txtTelNO.Text = DR["TELNO"].ToString();
                    txtMoNO.Text = DR["MOBNO"].ToString();
                    txtEML.Text = DR["EMAIL"].ToString();
                    txtNatin.Text = DR["NATIONALITY"].ToString();
                    ddlReli.Text = DR["RELIGION"].ToString();
                    txtDOB.Text = DR["DOB"].ToString();
                    ddlGNDR.Text = DR["GENDER"].ToString();
                    txtStuTP.Text = DR["STUDENTTP"].ToString();
                    txtPOB.Text = DR["BIRTHP"].ToString();
                    txtNPidNO.Text = DR["NIDPNO"].ToString();
                    txtBG.Text = DR["BLOODGR"].ToString();
                    ddlPRcdnc.Text = DR["PRESIDENCE"].ToString();
                    ddlMSTTS.Text = DR["MSTATUS"].ToString();
                    ddlHSTL.Text = DR["RESHOSTEL"].ToString();
                    txtINCM.Text = DR["INCOMEYY"].ToString();
                    txtEXP.Text = DR["EXPENSEYY"].ToString();
                    txtGNM.Text = DR["GUARDIANNM"].ToString();
                    txtGurREL.Text = DR["GRELATION"].ToString();
                    txtGAdrs1.Text = DR["GADDRESS"].ToString();
                    txtGTelNO.Text = DR["GTELNO"].ToString();
                    txtGMoNo.Text = DR["GMOBNO"].ToString();
                    txtGML.Text = DR["GEMAIL"].ToString();
                    txtPreProgNM.Text = DR["PREPROGNM"].ToString();
                    txtPreIns.Text = DR["PPINSTITN"].ToString();
                    txtPreSSN.Text = DR["PPSESSION"].ToString();
                    txtFirmNM.Text = DR["FIRMNM"].ToString();
                    txtPosisn.Text = DR["POSITION"].ToString();
                    lblImagePath.Text = DR["REMARKS"].ToString();
                    if (txtStudentIDOld.Text == "")
                        txtStudentNameOld.Text = "";
                }
                DR.Close();
                if (con.State != ConnectionState.Closed)con.Close();

                Global.txtAdd("SELECT PROGRAMNM FROM EIM_PROGRAM WHERE PROGRAMID='" + lblProgIDMigrate.Text + "'", txtProgNMMigrate);
                gridShow();
                lblMSG1.Visible = false;

            }
        }
        private void Garvez()
        {

        }
        protected void ddlAdmYR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdmYR.Text == "Select")
            {
                ddlAdmYR.Focus();
            }
            else
            {
                //string batchNo = "";
                //int YY = Convert.ToInt16(ddlAdmYR.Text);
                //int sem = Convert.ToInt16(ddlSemNM.Text);
                //int x = (YY - 2014) + 1;
                //int batch = x * 2;
                //if (sem == 1)
                //    batch = batch - 1;
                //if (batch < 10)
                //    batchNo = "00" + batch;
                //else if (batch < 100)
                //    batchNo = "0" + batch;
                //else
                //    batchNo = batch.ToString();
                //if (ddlSemNM.Text != "0")
                //    txtBCH.Text = batchNo;
                txtAdmDT.Focus();
            }
        }

         
        private void Hide()
        {
            btnAdd.Visible = false;
            txtXmNM.Visible = false;
            txtPYr.Visible = false;
            txtXmRl.Visible = false;
            txtGrop.Visible = false;
            txtGPA.Visible = false;
            txtInsNM.Visible = false;
            txtBrd.Visible = false;
        }
        private void Show()
        {
            btnAdd.Visible = true;
            txtXmNM.Visible = true;
            txtPYr.Visible = true;
            txtXmRl.Visible = true;
            txtGrop.Visible = true;
            txtGPA.Visible = true;
            txtInsNM.Visible = true;
            txtBrd.Visible = true;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                //GridEdit.Visible = true;
                btnEdit.Text = "New Entry";
                txtStuID.Visible = true;
                lblStuIDNO.Visible = true;
                btnSUBMIT.Text = "Update";
                Clear();
                ddlStudentIDOld.Enabled = false;
                txtStuID.Focus();
                lblMSG1.Visible = false;
                Hide();
            }
            else
            {
                GridEdit.Visible = false;
                btnEdit.Text = "Edit";
                txtStuID.Visible = false;
                lblStuIDNO.Visible = false;
                btnSUBMIT.Text = "Submit";
                txtStuID.Text = "";
                Clear();
                ddlStudentIDOld.Enabled = true;
                txtStuID.Focus();
                lblMSG1.Visible = false;
                gridShow();
                Show();
            }
        }

        protected void btnDLT_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtStuIdNew.Text + "'", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("DELETE FROM EIM_STUEDUQ WHERE NEWSTUDENTID='" + txtStuIdNew.Text + "'", con);
                cmd1.ExecuteNonQuery();
                if (con.State != ConnectionState.Closed)con.Close();
                Clear();
                txtStuID.Focus();
                txtStuID.Text = "";
                lblMSG1.Visible = true;
                lblMSG1.Text = "Deleted !";
                Response.Write("<script>alert('Successfully Deleted !')</script>");
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }
        private void gridShow()
        {
            if (con.State != ConnectionState.Open)con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT EXAMSL, EXAMNM, PASSYY, EXAMROLL, GROUPSUB, DIVGRADE, INSTITUTE, BOARDUNI
            FROM EIM_STUEDUQ WHERE STUDENTID='" + txtStuMaxID.Text + "' OR STUDENTID='" + ddlStudentIDOld.Text + "'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State != ConnectionState.Closed)con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridEdit.DataSource = ds;
                GridEdit.DataBind();


            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridEdit.DataSource = ds;
                GridEdit.DataBind();
                int columncount = GridEdit.Rows[0].Cells.Count;
                GridEdit.Rows[0].Cells.Clear();
                GridEdit.Rows[0].Cells.Add(new TableCell());
                GridEdit.Rows[0].Cells[0].ColumnSpan = columncount;
                GridEdit.Rows[0].Visible = false;

            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                TextBox txtEXAMNMFooter = (TextBox)GridEdit.FooterRow.FindControl("txtEXAMNMFooter");
                TextBox txtPASSYYFooter = (TextBox)GridEdit.FooterRow.FindControl("txtPASSYYFooter");
                TextBox txtEXAMROLLFooter = (TextBox)GridEdit.FooterRow.FindControl("txtEXAMROLLFooter");
                TextBox txtGROUPSUBFooter = (TextBox)GridEdit.FooterRow.FindControl("txtGROUPSUBFooter");
                TextBox txtDIVGRADEFooter = (TextBox)GridEdit.FooterRow.FindControl("txtDIVGRADEFooter");
                TextBox txtINSTITUTEFooter = (TextBox)GridEdit.FooterRow.FindControl("txtINSTITUTEFooter");
                TextBox txtBOARDUNIFooter = (TextBox)GridEdit.FooterRow.FindControl("txtBOARDUNIFooter");
                iob.UserID = Session["UserName"].ToString();
                iob.Ipaddress = Session["IpAddress"].ToString();
                iob.PcName = Session["PCName"].ToString();
                iob.InTime = Global.Dayformat1(DateTime.Now);

                if (e.CommandName.Equals("Add"))
                {
                    if (txtEXAMNMFooter.Text == "")
                        txtEXAMNMFooter.Focus();
                    else if (txtPASSYYFooter.Text == "")
                        txtPASSYYFooter.Focus();
                    else if (txtEXAMROLLFooter.Text == "")
                        txtEXAMROLLFooter.Focus();
                    else if (txtGROUPSUBFooter.Text == "")
                        txtGROUPSUBFooter.Focus();
                    else if (txtDIVGRADEFooter.Text == "")
                        txtDIVGRADEFooter.Focus();
                    else if (txtINSTITUTEFooter.Text == "")
                        txtINSTITUTEFooter.Focus();
                    else if (txtBOARDUNIFooter.Text == "")
                        txtBOARDUNIFooter.Focus();
                    else
                    {
                        int SL = 0;
                        Global.lblAdd("SELECT MAX(EXAMSL) FROM EIM_STUEDUQ WHERE STUDENTID='" + txtStuMaxID.Text + "'", lblSL);

                        if (lblSL.Text == "")
                        {
                            SL = 1;
                        }
                        else
                        {
                            int SLNO = int.Parse(lblSL.Text);
                            SL = SLNO + 1;
                        }

                        iob.StuID = txtStuMaxID.Text;
                        iob.ExamSL = SL;
                        iob.ExamNM = txtEXAMNMFooter.Text;
                        iob.PassYR = txtPASSYYFooter.Text;
                        iob.ExamRoll = txtEXAMROLLFooter.Text;
                        iob.GRP = txtGROUPSUBFooter.Text;
                        iob.GRAD = txtDIVGRADEFooter.Text;
                        iob.insNM = txtINSTITUTEFooter.Text;
                        iob.BRD = txtBOARDUNIFooter.Text;
                        dob.Insert_EIM_STUEDUQ(iob);
                        gridShow();
                    }
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void GridEdit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblEXAMSL = (Label)GridEdit.Rows[e.RowIndex].FindControl("lblEXAMSL");
                if (con.State != ConnectionState.Open)con.Open();

                SqlCommand cmd = new SqlCommand(@"Delete from EIM_STUEDUQ  where STUDENTID='" + txtStuMaxID.Text + "' AND EXAMSL='" + lblEXAMSL.Text + "'", con);
                cmd.ExecuteNonQuery();
                if (con.State != ConnectionState.Closed)con.Close();
                gridShow();

                TextBox txtEXAMNMFooter = (TextBox)GridEdit.FooterRow.FindControl("txtEXAMNMFooter");
                txtEXAMNMFooter.Focus();
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void GridEdit_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridEdit.EditIndex = e.NewEditIndex;
            gridShow();

            TextBox txtEXAMNMEdit = (TextBox)GridEdit.Rows[e.NewEditIndex].FindControl("txtEXAMNMEdit");
            txtEXAMNMEdit.Focus();
        }

        protected void GridEdit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridEdit.EditIndex = -1;
            gridShow();
        }

        protected void GridEdit_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                iob.UPDUserID = Session["UserName"].ToString();
                iob.UPDIpaddress = Session["IpAddress"].ToString();
                iob.UPDPcName = Session["PCName"].ToString();
                iob.UPDTime = Global.Dayformat1(DateTime.Now);
                Label lblEXAMSLEdit = (Label)GridEdit.Rows[e.RowIndex].FindControl("lblEXAMSLEdit");
                TextBox txtEXAMNMEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtEXAMNMEdit");
                TextBox txtPASSYYEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtPASSYYEdit");
                TextBox txtEXAMROLLEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtEXAMROLLEdit");
                TextBox txtGROUPSUBEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtGROUPSUBEdit");
                TextBox txtDIVGRADEEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtDIVGRADEEdit");
                TextBox txtINSTITUTEEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtINSTITUTEEdit");
                TextBox txtBOARDUNIEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtBOARDUNIEdit");
                //iob.UserID = Session["UserName"];
                //iob.Ipaddress = Session["IpAddress"];
                //iob.PcName = Session["PCName"];
                //iob.InTime = DateTime.Now;

                if (txtEXAMNMEdit.Text == "")
                    txtEXAMNMEdit.Focus();
                else if (txtPASSYYEdit.Text == "")
                    txtPASSYYEdit.Focus();
                else if (txtEXAMROLLEdit.Text == "")
                    txtEXAMROLLEdit.Focus();
                else if (txtGROUPSUBEdit.Text == "")
                    txtGROUPSUBEdit.Focus();
                else if (txtDIVGRADEEdit.Text == "")
                    txtDIVGRADEEdit.Focus();
                else if (txtINSTITUTEEdit.Text == "")
                    txtINSTITUTEEdit.Focus();
                else if (txtBOARDUNIEdit.Text == "")
                    txtBOARDUNIEdit.Focus();
                else
                {
                    iob.StuID = txtStuMaxID.Text;
                    iob.ExamSL = int.Parse(lblEXAMSLEdit.Text);
                    iob.ExamNM = txtEXAMNMEdit.Text;
                    iob.PassYR = txtPASSYYEdit.Text;
                    iob.ExamRoll = txtEXAMROLLEdit.Text;
                    iob.GRP = txtGROUPSUBEdit.Text;
                    iob.GRAD = txtDIVGRADEEdit.Text;
                    iob.insNM = txtINSTITUTEEdit.Text;
                    iob.BRD = txtBOARDUNIEdit.Text;

                    dob.Update_EIM_STUEDUQ(iob);
                    GridEdit.EditIndex = -1;
                    gridShow();
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionStudentOLDID(string prefixText, int count, string contextKey)
        {

            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand("SELECT TOP 20 NEWSTUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID LIKE '" + prefixText + "%'", conn);
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
        protected void txtFirmNM1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtStudentIDOld_TextChanged(object sender, EventArgs e)
        {
            if (txtStudentIDOld.Text == "")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Student ID !";
                Response.Write("<script>alert('Select Student ID !')</script>");
                txtStudentIDOld.Focus();
            }
            else
            {
                txtStudentNameOld.Text = "";
                lblProgIDMigrate.Text = "";
                txtProgNMMigrate.Text = "";
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtStudentIDOld.Text + "'", txtStudentNameOld);
                Global.lblAdd("SELECT PROGRAMID FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtStudentIDOld.Text + "'", lblProgIDMigrate);
                Global.txtAdd(@"SELECT        EIM_PROGRAM.PROGRAMNM FROM EIM_STUDENT INNER JOIN EIM_PROGRAM 
                ON EIM_STUDENT.PROGRAMID = EIM_PROGRAM.PROGRAMID WHERE EIM_STUDENT.NEWSTUDENTID='" + txtStudentIDOld.Text + "'", txtProgNMMigrate);
                OldDataLoad();
                btnSUBMIT.Focus();
            }
        }
        protected void ddlStudentIDOld_TextChanged(object sender, EventArgs e)
        {


            txtStudentNameOld.Text = "";
            lblProgIDMigrate.Text = "";
            txtProgNMMigrate.Text = "";
            //                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtStudentIDOld.Text + "'", txtStudentNameOld);
            //                Global.lblAdd("SELECT PROGRAMID FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtStudentIDOld.Text + "'", lblProgIDMigrate);
            //                Global.txtAdd(@"SELECT        EIM_PROGRAM.PROGRAMNM FROM EIM_STUDENT INNER JOIN EIM_PROGRAM 
            //                ON EIM_STUDENT.PROGRAMID = EIM_PROGRAM.PROGRAMID WHERE EIM_STUDENT.NEWSTUDENTID='" + txtStudentIDOld.Text + "'", txtProgNMMigrate);
            txtProgNMMigrate.Text = Global.Slipt(ddlStudentIDOld.Text, 3, '|');
            txtStudentNameOld.Text = Global.Slipt(ddlStudentIDOld.Text, 1, '|');
            lblProgIDMigrate.Text = Global.Slipt(ddlStudentIDOld.Text, 2, '|');
            txtStudentIDOld.Text = Global.Slipt(ddlStudentIDOld.Text, 0, '|');
            OldDataLoad();
            btnSUBMIT.Focus();
        }
        private void OldDataLoad()
        {

            if (con.State != ConnectionState.Open)con.Open();
            string Script = @"SELECT    EIM_STUDENT.WAIVER,EIM_STUDENT.SCHOLAR, CONVERT(NVARCHAR(10),EIM_STUDENT.MIGRATEDT,103) MIGRATEDT,EIM_STUDENT.MIGRATEPID,EIM_STUDENT.MIGRATESID,EIM_STUDENT.ADMITYY, CONVERT(NVARCHAR(10),EIM_STUDENT.ADMITDT,103) AS ADMITDT, EIM_STUDENT.ADMITTP, EIM_SEMESTER.SEMESTERNM, EIM_STUDENT.SEMESTERID, EIM_PROGRAM.PROGRAMNM, 
                      EIM_STUDENT.PROGRAMID, EIM_STUDENT.SESSION, EIM_STUDENT.BATCH, EIM_STUDENT.STUDENTID, EIM_STUDENT.STUDENTNM,EIM_STUDENT.WAIVER, EIM_STUDENT.FATHERNM, EIM_STUDENT.FATHEROCP, 
                      EIM_STUDENT.FOCPDTL, EIM_STUDENT.MOTHERNM, EIM_STUDENT.MOTHEROCP, EIM_STUDENT.SPOUSENM, EIM_STUDENT.SPOUSEOCP, EIM_STUDENT.ADDRPRE, 
                      EIM_STUDENT.ADDRPER, EIM_STUDENT.TELNO, EIM_STUDENT.MOBNO, EIM_STUDENT.EMAIL, EIM_STUDENT.NATIONALITY, EIM_STUDENT.RELIGION, EIM_STUDENT.DOB, 
                      EIM_STUDENT.GENDER, EIM_STUDENT.STUDENTTP, EIM_STUDENT.BIRTHP, EIM_STUDENT.NIDPNO, EIM_STUDENT.BLOODGR, EIM_STUDENT.PRESIDENCE, EIM_STUDENT.MSTATUS, 
                      EIM_STUDENT.RESHOSTEL, EIM_STUDENT.INCOMEYY, EIM_STUDENT.EXPENSEYY, EIM_STUDENT.GUARDIANNM, EIM_STUDENT.GRELATION, EIM_STUDENT.GADDRESS, 
                      EIM_STUDENT.GTELNO, EIM_STUDENT.GMOBNO, EIM_STUDENT.GEMAIL, EIM_STUDENT.PREPROGNM, EIM_STUDENT.PPINSTITN, EIM_STUDENT.PPSESSION, 
                      EIM_STUDENT.FIRMNM, EIM_STUDENT.POSITION, EIM_STUDENT.REMARKS
                      FROM EIM_STUDENT INNER JOIN
                      EIM_SEMESTER ON EIM_STUDENT.SEMESTERID = EIM_SEMESTER.SEMESTERID INNER JOIN
                      EIM_PROGRAM ON EIM_STUDENT.PROGRAMID = EIM_PROGRAM.PROGRAMID WHERE STUDENTID='" + txtStudentIDOld.Text + "'";
            SqlCommand cmd = new SqlCommand(Script, con);
            SqlDataReader DR = cmd.ExecuteReader();
            // Clear();
            while (DR.Read())
            {
                //txtMigrateDT.Text = DR["MIGRATEDT"].ToString();
                //txtStudentIDOld.Text = DR["MIGRATESID"].ToString();
                //txtStudentNameOld.Text = DR["STUDENTNM"].ToString();
                //lblProgIDMigrate.Text = DR["MIGRATEPID"].ToString();
                //ddlAdmYR.Text = DR["ADMITYY"].ToString();
                //txtAdmDT.Text = DR["ADMITDT"].ToString();
                //if (DR["ADMITTP"].ToString() == "Regular")
                //    ddlAdmtTP.SelectedIndex = 0;
                //else if (DR["ADMITTP"].ToString() == "Credit Transfer")
                //    ddlAdmtTP.SelectedIndex = 1;
                //else
                //    ddlAdmtTP.SelectedIndex = -1;
                ////ddlAdmtTP.Text = DR["ADMITTP"].ToString();
                //ddlSemNM.Text = DR["SEMESTERNM"].ToString();
                //lblSemID.Text = DR["SEMESTERID"].ToString();
                //ddlProNM.Text = DR["PROGRAMNM"].ToString();
                //lblProID.Text = DR["PROGRAMID"].ToString();
                //txtSession.Text = DR["SESSION"].ToString();
                //txtBCH.Text = DR["BATCH"].ToString();
                //txtStuMaxID.Text = DR["STUDENTID"].ToString();
                ddlProNM.SelectedIndex = -1;
                ddlSemNM.SelectedIndex = -1;
                ddlBCH.SelectedIndex = -1;
                txtStuNM.Text = DR["STUDENTNM"].ToString();
                txtStuFNM.Text = DR["FATHERNM"].ToString();
                txtFOcup.Text = DR["FATHEROCP"].ToString();
                txtFOcupDTL.Text = DR["FOCPDTL"].ToString();
                txtStuMNM.Text = DR["MOTHERNM"].ToString();
                txtMOcup.Text = DR["MOTHEROCP"].ToString();
                txtSPNM.Text = DR["SPOUSENM"].ToString();
                txtSPOcup.Text = DR["SPOUSEOCP"].ToString();
                txtPreAdrs1.Text = DR["ADDRPRE"].ToString();
                txtPerAdrs1.Text = DR["ADDRPER"].ToString();
                txtTelNO.Text = DR["TELNO"].ToString();
                txtMoNO.Text = DR["MOBNO"].ToString();
                txtEML.Text = DR["EMAIL"].ToString();
                txtNatin.Text = DR["NATIONALITY"].ToString();
                ddlReli.Text = DR["RELIGION"].ToString();
                txtDOB.Text = DR["DOB"].ToString();
                ddlGNDR.Text = DR["GENDER"].ToString();
                txtStuTP.Text = DR["STUDENTTP"].ToString();
                txtPOB.Text = DR["BIRTHP"].ToString();
                txtNPidNO.Text = DR["NIDPNO"].ToString();
                txtBG.Text = DR["BLOODGR"].ToString();
                ddlPRcdnc.Text = DR["PRESIDENCE"].ToString();
                ddlMSTTS.Text = DR["MSTATUS"].ToString();
                ddlHSTL.Text = DR["RESHOSTEL"].ToString();
                txtINCM.Text = DR["INCOMEYY"].ToString();
                txtEXP.Text = DR["EXPENSEYY"].ToString();
                txtGNM.Text = DR["GUARDIANNM"].ToString();
                txtGurREL.Text = DR["GRELATION"].ToString();
                txtGAdrs1.Text = DR["GADDRESS"].ToString();
                txtGTelNO.Text = DR["GTELNO"].ToString();
                txtGMoNo.Text = DR["GMOBNO"].ToString();
                txtGML.Text = DR["GEMAIL"].ToString();
                txtPreProgNM.Text = DR["PREPROGNM"].ToString();
                txtPreIns.Text = DR["PPINSTITN"].ToString();
                txtPreSSN.Text = DR["PPSESSION"].ToString();
                txtFirmNM.Text = DR["FIRMNM"].ToString();
                txtPosisn.Text = DR["POSITION"].ToString();
                lblImagePath.Text = DR["REMARKS"].ToString();
                if (txtStudentIDOld.Text == "")
                    txtStudentNameOld.Text = "";
            }
            DR.Close();
            if (con.State != ConnectionState.Closed)con.Close();

            //Global.txtAdd("SELECT PROGRAMNM FROM EIM_PROGRAM WHERE PROGRAMID='" + lblProgIDMigrate.Text + "'", txtProgNMMigrate);
            gridShow();
            lblMSG1.Visible = false;
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Clear();
            gridShow();
            if (btnEdit.Text == "Edit")
                txtAdmDT.Focus();
            else
                txtStuID.Focus();
        }

        protected void txtStuFNM_TextChanged(object sender, EventArgs e)
        {

        }
         

        protected void ddlBCH_SelectedIndexChanged(object sender, EventArgs e)
        {
            StuID();
            ddlSemNM_SelectedIndexChanged(sender, e);
            StuIDNEW();
            ddlSemNM.Focus();
        }
         

    }

}


