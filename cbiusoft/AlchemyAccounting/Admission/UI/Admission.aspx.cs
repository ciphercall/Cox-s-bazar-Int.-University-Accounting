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
    public partial class Admission : System.Web.UI.Page
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
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Global.dropDownAdd(ddlProgramNM, @" SELECT PROGRAMNM FROM EIM_PROGRAM ");
                    Global.dropDownAdd(ddlSemisNM, @" SELECT SEMESTERNM FROM  EIM_SEMESTER");
                    Global.dropDownAdd(ddlExmYr, "SELECT distinct(TESTYY) FROM EIM_ADMTEST");
                    lblCOUNT.Text = "0";
                    lblGRDCount.Text = "0";
                    txtMRDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                    //txtMRDT.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtMRYR.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    int i, m;
                    int a = int.Parse(yr);
                    m = a + 5;
                    for (i = a - 5; i <= m; i++)
                    {
                        ddlExmYr.Items.Add(i.ToString());
                    }
                    ddlExmYr.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");

                    MR_NO();
                    ddlExmYr.Focus();
                    int Count = GridViewEQ.Rows.Count;
                    if (Count > 0)
                        dt.Clear();
                    Session["datatable"] = "";
                    txtExmDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                    // gridShow();
                    //dt.Clear();
                    //for raf
                    ///  Global.dropDownAdd(DropDownList1, "SELECT ROLLNO FRIM EIM_STUDENT");
                }

            }

        }
        private void gridShow()
        {

            if (con.State != ConnectionState.Open)
                if (con.State != ConnectionState.Open)con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT EXAMSL,EXAMNM,SESSION,GROUPSUB,BOARDUNI,PASSYY,GPAMARKS,DIVGRADE FROM dbo.EIM_ADMEDUQ WHERE MRNO='" + ddlMRNO.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State != ConnectionState.Closed)
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
        private void Clear()
        {

            ddlSemisNM.SelectedIndex = -1;
            ddlProgramNM.SelectedIndex = -1;
            txtStuNM.Text = "";
            txtStuFNM.Text = "";
            txtStuMNM.Text = "";
            txtPerAdrs1.Text = "";
            txtPerAdrs2.Text = "";
            txtPreAdrs1.Text = "";
            txtPreAdrs2.Text = "";
            txtExmDT.Text = "";
            txtMblNO.Text = "";
            txtEML.Text = "";
            // txtNation.Text = "";
            txtDtOfBth.Text = "";
            ddlReli.SelectedIndex = -1;
            ddlGndr.SelectedIndex = -1;
            txtGurNM.Text = "";
            txtGurStuRel.Text = "";
            txtGurProf.Text = "";
            txtGurAdrs.Text = "";
            txtGurPhnNO.Text = "";
            txtGurEML.Text = "";
            txtNOXM.Text = "";
            txtSSN.Text = "";
            txtGNM.Text = "";
            txtYOPASS.Text = "";
            txtBRD.Text = "";
            txtGPA.Text = "";
            txtLTRGRD.Text = "";
            txtMRDT.Text = "";
            txtMRAMNT.Text = "";
            txtMRNO.Text = "";
            txtMRYR.Text = "";
            FileUpload1 = null;
            lblImagePath.Text = "";
            GridEdit.Visible = false;



        }
        private void RollFormNO()
        {

            lblFrmNo.Text = "";
            Global.lblAdd("select MAX(ROLLNO) from EIM_ADMISSION where SEMESTERID='" + lblSemID.Text + "' and PROGRAMID='" + lblProID.Text + "' and TESTYY='" + ddlExmYr.Text + "'", lblRoll);
            if (lblRoll.Text == "")
            {
                iob.Roll = 1;
            }
            else
            {
                iob.Roll = int.Parse(lblRoll.Text) + 1;

            }
            Global.lblAdd("select MAX(FORMNO) from EIM_ADMISSION", lblFrmNo);
            if (lblRoll.Text == "")
            {
                iob.FormNO = 1;
            }
            else
            {
                iob.FormNO = int.Parse(lblFrmNo.Text) + 1;

            }
        }
        private void NullSender()
        {
            if (txtPerAdrs1.Text == "")
                iob.PerAdrs = "";
            if (txtStuFNM.Text == "")
                iob.StuFNM = "";
            if (txtStuMNM.Text == "")
                iob.StuMNM = "";
            if (txtPreAdrs1.Text == "")
                iob.PreAdrs = "";
            if (txtEML.Text == "")
                iob.Email = "";
            if (txtNation.Text == "")
                iob.Nation = "";
            if (ddlGndr.Text == "Select")
                iob.Gander = "";
            if (ddlReli.Text == "Select")
                iob.Religion = "";
           
            if (txtGurNM.Text == "")
                iob.GurNM = "";
            if (txtGurStuRel.Text == "")
                iob.GRel = "";
            if (txtGurProf.Text == "")
                iob.GProf = "";
            if (txtGurAdrs.Text == "")
                iob.GAdrs = "";
            if (txtGurPhnNO.Text == "")
                iob.GMNo = "";
            if (txtGurEML.Text == "")
                iob.GEml = "";

        }
        protected void btnInsertAdm_Click(object sender, EventArgs e)
        {
            try
            {
                lblMSG1.Visible = false;
                Session["SL"] = "";
                Session["a"] = "01";
                if (con.State != ConnectionState.Open)con.Open();
                if (Session["UserName"]==null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {

                    iob.UserID = Session["UserName"].ToString();
                    iob.Ipaddress = Session["IpAddress"].ToString();
                    iob.PcName = Session["PCName"].ToString();
                    iob.InTime = Global.Dayformat1(DateTime.Now);

                    iob.UPDUserID = Session["UserName"].ToString();
                    iob.UPDIpaddress = Session["IpAddress"].ToString();
                    iob.UPDPcName = Session["PCName"].ToString();
                    iob.UPDTime = Global.Dayformat1(DateTime.Now);
                    if (ddlExmYr.Text == "Select")
                    {
                        ddlExmYr.Focus();
                    }
                    else if (ddlSemisNM.Text == "Select")
                    {
                        ddlSemisNM.Focus();
                    }
                    else if (ddlProgramNM.Text == "Select")
                    {
                        ddlProgramNM.Focus();
                    }
                    else if (txtStuNM.Text == "")
                    {
                        txtStuNM.Focus();
                    }
                    else if (txtMblNO.Text == "")
                    {
                        txtMblNO.Focus();
                    }

                    //else if (txtStuFNM.Text == "")
                    //{
                    //    txtStuFNM.Focus();
                    //}
                    //else if (txtStuMNM.Text == "")
                    //{
                    //    txtStuMNM.Focus();
                    //}
                    //else if (txtPreAdrs1.Text == "")
                    //{
                    //    txtPreAdrs1.Focus();
                    //}
                    //else if (txtNation.Text == "")
                    //{
                    //    txtNation.Focus();
                    //}
                    //else if (ddlReli.Text == "Select")
                    //{
                    //    ddlReli.Focus();
                    //}
                    //else if (txtDtOfBth.Text == "")
                    //{
                    //    txtDtOfBth.Focus();
                    //}

                    //else if (ddlGndr.Text == "Select")
                    //{
                    //    ddlGndr.Focus();
                    //}
                    //else if (txtMRYR.Text == "")
                    //{
                    //    txtMRDT.Focus();
                    //}
                    //else if (txtMRAMNT.Text == "")
                    //{
                    //    txtMRAMNT.Focus();
                    //}
                    //else if (!FileUpload1.HasFile)
                    //{
                    //    FileUpload1.Focus();
                    //}
                    else
                    {
                        if (btnInsertAdm.Text == "Submit")
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
                                    lblMSG.Text = "Upload Image is not a permitted file type!";
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



                            lblMSG.Visible = false;
                            DateTime MRDT = DateTime.Parse(txtMRDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            DateTime EXAMDT = DateTime.Parse(txtExmDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            // DateTime Date1 = DateTime.Parse(txtExmDT.Text);
                            Global.lblAdd("select SEMESTERID from EIM_SEMESTER where SEMESTERNM='" + ddlSemisNM.Text + "'", lblSemID);
                            Global.lblAdd("select PROGRAMID from dbo.EIM_PROGRAM where PROGRAMNM='" + ddlProgramNM.Text + "'", lblProID);
                            RollFormNO();
                            // lblMSG.Visible = false;
                            iob.ExamYr = ddlExmYr.Text;

                            iob.SemID = int.Parse(lblSemID.Text);
                            iob.ProgID = lblProID.Text;
                            iob.ExamDT = EXAMDT;

                            iob.StuNM = txtStuNM.Text;
                            iob.StuFNM = txtExmDT.Text;

                            iob.StuMNM = txtStuMNM.Text;
                            iob.PreAdrs = txtPreAdrs1.Text + " " + txtPreAdrs2.Text;
                            iob.PerAdrs = txtPerAdrs1.Text + " " + txtPerAdrs2.Text;
                            iob.MobNO = txtMblNO.Text;
                            iob.Email = txtEML.Text;
                            iob.Nation = txtNation.Text;
                            iob.Religion = ddlReli.Text;
                            iob.DtOfBrt = txtDtOfBth.Text;
                            iob.Gander = ddlGndr.Text;
                            iob.GurNM = txtGurNM.Text;
                            iob.GRel = txtGurStuRel.Text;
                            iob.GProf = txtGurProf.Text;
                            iob.GAdrs = txtGurAdrs.Text;
                            iob.GMNo = txtGurPhnNO.Text;
                            iob.GEml = txtGurEML.Text;
                            iob.MrDT = MRDT;
                            iob.MrYr = int.Parse(txtMRYR.Text);
                            MR_NO();
                            iob.TotlAmnt = Decimal.Parse(txtMRAMNT.Text);
                            NullSender();

                            dob.InsertAdmission(iob);
                            //Education Qualification
                            lblSLGRD.Text = "";
                            //int Sl;
                            //Global.lblAdd("SELECT MAX(EXAMSL) FROM EIM_ADMEDUQ", lblSLGRD);
                            //if (lblSLGRD.Text == "")
                            //{
                            //    Sl = 1;
                            //}
                            //else
                            //    Sl = int.Parse(lblSLGRD.Text) + 1;
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
                                    SqlCommand cmd1 = new SqlCommand(@"INSERT INTO EIM_ADMEDUQ (TESTYY,SEMESTERID,PROGRAMID,TESTDT,ROLLNO,
                                             EXAMSL,EXAMNM,SESSION,GROUPSUB,BOARDUNI,PASSYY,GPAMARKS,DIVGRADE,MRNO,USERID,USERPC,IPADDRESS,INTIME) VALUES 
                                                       (@TESTYY,@SEMESTERID,@PROGRAMID,@TESTDT,@ROLLNO,
                                             @EXAMSL,@EXAMNM,@SESSION,@GROUPSUB,@BOARDUNI,@PASSYY,@GPAMARKS,@DIVGRADE,@MRNO,@USERID,@USERPC,@IPADDRESS,@INTIME)", con);

                                    cmd1.Parameters.AddWithValue("@TESTYY", iob.ExamYr);
                                    cmd1.Parameters.AddWithValue("@SEMESTERID", iob.SemID);
                                    cmd1.Parameters.AddWithValue("@PROGRAMID", iob.ProgID);
                                    cmd1.Parameters.AddWithValue("@TESTDT", iob.ExamDT);
                                    cmd1.Parameters.AddWithValue("@ROLLNO", iob.Roll);
                                    cmd1.Parameters.AddWithValue("@EXAMSL", Sl);
                                    cmd1.Parameters.AddWithValue("@EXAMNM", row.Cells[1].Text);
                                    cmd1.Parameters.AddWithValue("@SESSION", row.Cells[2].Text);
                                    cmd1.Parameters.AddWithValue("@GROUPSUB", row.Cells[3].Text);
                                    cmd1.Parameters.AddWithValue("@BOARDUNI", row.Cells[4].Text);
                                    cmd1.Parameters.AddWithValue("@PASSYY", row.Cells[5].Text);
                                    cmd1.Parameters.AddWithValue("@GPAMARKS", row.Cells[6].Text);
                                    cmd1.Parameters.AddWithValue("@DIVGRADE", row.Cells[7].Text);
                                    cmd1.Parameters.AddWithValue("@MRNO", iob.MrNo);
                                    cmd1.Parameters.AddWithValue("@USERID", iob.UserID);
                                    cmd1.Parameters.AddWithValue("@USERPC", iob.PcName);
                                    cmd1.Parameters.AddWithValue("@IPADDRESS", iob.Ipaddress);
                                    cmd1.Parameters.AddWithValue("@INTIME", iob.InTime);
                                    cmd1.ExecuteNonQuery();
                                    Sl++;
                                    if (con.State != ConnectionState.Closed)
                                        if (con.State != ConnectionState.Closed)con.Close();
                                }
                                catch (Exception ex)
                                {
                                    Response.Write(ex);
                                }
                            }
                            //Data Pass For Create Receipt
                            //Start
                            Session["MRNO"] = iob.MrNo;
                            Session["MRDT"] = iob.MrDT;
                            Session["FRNO"] = iob.FormNO;
                            Global.lblAdd("SELECT PROGRAMSID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgramNM.Text + "'", lblProSNM);
                            Session["PROGRAMSNM"] = lblProSNM.Text;
                            Session["SEMESTERNM"] = ddlSemisNM.Text;
                            Session["STUDENTNM"] = txtStuNM.Text;
                            Session["AMOUNT"] = txtMRAMNT.Text;

                            //End
                            Clear();
                            //Session["datatable"] = null;
                            int Count1 = GridViewEQ.Rows.Count;
                            if (Count1 > 0)
                                dt.Clear();
                            //Session["datatable"] = "";
                            GridViewEQ.Visible = false;
                            if (chkPrint.Checked == true)
                            {


                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                        "OpenWindow", "window.open('/Admission/Report/rptMoneyReceipt.aspx','_newtab');", true);
                            }
                            MR_NO();
                            txtMRDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                            txtMRYR.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                            ddlSemisNM.Focus();
                            if (Session["UpdateAdmission"] != "")
                            {
                                lblMSG1.Visible = true;
                                lblMSG1.Text = "Inserted !";
                            }
                           
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
                                    lblMSG.Text = "";
                                    lblMSG.Visible = true;
                                    lblMSG.Text = "Upload Image is not a permitted file type!";
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
                            {
                                iob.Img = lblImagePath.Text;
                            }
                            if (lblMSG.Text != "")
                            {
                                FileUpload1.Focus();
                            }
                            else
                            {
                                if (FileUpload1.HasFile || !FileUpload1.HasFile)
                                {
                                    string Len = iob.Img.Length.ToString();
                                    int lenth = int.Parse(Len);
                                    if (lenth > 40)
                                    {
                                        lblMSG.Visible = true;
                                        lblMSG.Text = "large Image name.";
                                        FileUpload1.Focus();
                                    }
                                    else
                                    {
                                        lblMSG.Visible = false;
                                        DateTime MRDT = DateTime.Parse(txtMRDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                        DateTime EXAMDT = DateTime.Parse(txtExmDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                        // DateTime Date1 = DateTime.Parse(txtExmDT.Text);
                                        Global.lblAdd("select SEMESTERID from EIM_SEMESTER where SEMESTERNM='" + ddlSemisNM.Text + "'", lblSemID);
                                        Global.lblAdd("select PROGRAMID from dbo.EIM_PROGRAM where PROGRAMNM='" + ddlProgramNM.Text + "'", lblProID);
                                        iob.Roll = int.Parse(lblRoll.Text);
                                        iob.FormNO = int.Parse(txtFormNO.Text);
                                        iob.MrNo = int.Parse(txtMRNO.Text);
                                        // lblMSG.Visible = false;
                                        iob.ExamYr = ddlExmYr.Text;

                                        iob.SemID = int.Parse(lblSemID.Text);
                                        iob.ProgID = lblProID.Text;
                                        iob.ExamDT = EXAMDT;

                                        iob.StuNM = txtStuNM.Text;
                                        iob.StuFNM = txtStuFNM.Text;

                                        iob.StuMNM = txtStuMNM.Text;
                                        iob.PreAdrs = txtPreAdrs1.Text + " " + txtPreAdrs2.Text;
                                        iob.PerAdrs = txtPerAdrs1.Text + " " + txtPerAdrs2.Text;
                                        iob.MobNO = txtMblNO.Text;
                                        iob.Email = txtEML.Text;
                                        iob.Nation = txtNation.Text;
                                        iob.Religion = ddlReli.Text;
                                        iob.DtOfBrt = txtDtOfBth.Text;
                                        iob.Gander = ddlGndr.Text;
                                        iob.GurNM = txtGurNM.Text;
                                        iob.GRel = txtGurStuRel.Text;
                                        iob.GProf = txtGurProf.Text;
                                        iob.GAdrs = txtGurAdrs.Text;
                                        iob.GMNo = txtGurPhnNO.Text;
                                        iob.GEml = txtGurEML.Text;
                                        iob.MrDT = MRDT;
                                        iob.MrYr = int.Parse(txtMRYR.Text);
                                        // MR_NO();
                                        iob.TotlAmnt = Decimal.Parse(txtMRAMNT.Text);

                                        NullSender();
                                        dob.UpdateAdmission(iob);
                                        //Education Qualification
                                        lblSLGRD.Text = "";
                                        //int Sl;

                                        //Start
                                        Session["MRNO"] = iob.MrNo;
                                        Session["MRDT"] = iob.MrDT;
                                        Session["FRNO"] = iob.FormNO;
                                        Global.lblAdd("SELECT PROGRAMSID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgramNM.Text + "'", lblProSNM);
                                        Session["PROGRAMSNM"] = lblProSNM.Text;
                                        Session["SEMESTERNM"] = ddlSemisNM.Text;
                                        Session["STUDENTNM"] = txtStuNM.Text;
                                        Session["AMOUNT"] = txtMRAMNT.Text;

                                        //End
                                        Clear();
                                        //Session["datatable"] = null;
                                        int Count = GridViewEQ.Rows.Count;
                                        if (Count > 0)
                                            dt.Clear();
                                        Session["datatable"] = "";
                                        ddlMRNO.SelectedIndex = -1;
                                        ddlMRNO.Focus();
                                        GridViewEQ.Visible = false;
                                        if (chkPrint.Checked == true)
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                                    "OpenWindow", "window.open('/Admission/Report/rptMoneyReceipt.aspx','_newtab');", true);
                                        }
                                        if (Session["UpdateAdmission"] != "")
                                        {
                                            lblMSG1.Visible = true;
                                            lblMSG1.Text = "Updated !";
                                        }
                                    }
                                }
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
        protected void btnKeep_Click(object sender, EventArgs e)
        {


            if (txtNOXM.Text == "")
            {
                txtNOXM.Focus();
            }
            else if (txtSSN.Text == "")
            {
                txtSSN.Focus();
            }
            else if (txtGNM.Text == "")
            {
                txtGNM.Focus();
            }
            else if (txtBRD.Text == "")
            {
                txtBRD.Focus();
            }
            else if (txtYOPASS.Text == "")
            {
                txtYOPASS.Focus();
            }
            else if (txtGPA.Text == "")
            {
                txtGPA.Focus();
            }
            else if (txtLTRGRD.Text == "")
            {
                txtLTRGRD.Focus();
            }
            else
            {
                //if (con.State != ConnectionState.Open)
                //{
                //    if (con.State != ConnectionState.Open)con.Open();

                //        for (int i = 0; i < CountRows; i++)
                //        {
                //            GridViewEQ.Rows[i].Cells.Clear();

                //        }

                //}
                if (Convert.ToString(ViewState["Row"]) != "")
                {
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["1"] = txtNOXM.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["2"] = txtSSN.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["3"] = txtGNM.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["4"] = txtBRD.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["5"] = txtYOPASS.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["6"] = txtGPA.Text;
                    dt.Rows[Convert.ToInt32(ViewState["Row"])]["7"] = txtLTRGRD.Text;

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
                        dr["1"] = txtNOXM.Text;
                        dr["2"] = txtSSN.Text;
                        dr["3"] = txtGNM.Text;
                        dr["4"] = txtBRD.Text;
                        dr["5"] = txtYOPASS.Text;
                        dr["6"] = txtGPA.Text;
                        dr["7"] = txtLTRGRD.Text;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dt.NewRow();
                        // dr["RowNumber"] = i;
                        dr["1"] = txtNOXM.Text;
                        dr["2"] = txtSSN.Text;
                        dr["3"] = txtGNM.Text;
                        dr["4"] = txtBRD.Text;
                        dr["5"] = txtYOPASS.Text;
                        dr["6"] = txtGPA.Text;
                        dr["7"] = txtLTRGRD.Text;
                        dt.Rows.Add(dr);
                    }
                    GridViewEQ.Visible = true;
                    GridViewEQ.DataSource = dt;
                    GridViewEQ.DataBind();
                    i++;
                    //int a = int.Parse(lblCOUNT.Text);
                    //int CountRows = GridViewEQ.Rows.Count;
                    //int Row = CountRows - 1;
                    //int SL = int.Parse(lblGRDCount.Text);
                    //if (CountRows > 0 && a > 0 && Session["SL"] != Session["a"])
                    //{
                    //    lblGRDCount.Text = CountRows.ToString();

                    //    for (int x = 0; x < Row; x++)
                    //    {
                    //        GridViewEQ.Rows[x].Cells.Clear();

                    //    }
                    //    Session["SL"] = SL;
                    //    Session["a"] = a;
                    //    Session["SL"] = Session["a"];
                    //    Session["a"] = Session["SL"];
                    //}
                    txtNOXM.Text = "";
                    txtSSN.Text = "";
                    txtGNM.Text = "";
                    txtYOPASS.Text = "";
                    txtBRD.Text = "";
                    txtGPA.Text = "";
                    txtLTRGRD.Text = "";
                    txtNOXM.Focus();
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
                    txtNOXM.Focus();
                }

            }
            else if (e.CommandName == "Change")
            {
                dt = (DataTable)Session["datatable"];
                //  TxtName.Text = (string)dt.Rows[grdView.SelectedIndex]["Name1"];
                txtNOXM.Text = GridViewEQ.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                txtSSN.Text = GridViewEQ.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                ViewState["Row"] = e.CommandArgument;

                //dt.Rows[grdView.SelectedIndex]["Name1"] = TxtName.Text;
                //dt.Rows[grdView.SelectedIndex]["Name2"] = txtAge.Text;
                GridViewEQ.DataSource = dt;
                GridViewEQ.DataBind();


            }
        }

        protected void GridViewEQ_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewEQ.DataSource = dt;
            GridViewEQ.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //  GridViewEQ.Rows.ToString() = null;
        }

        protected void ddlSemisNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.lblAdd("select SEMESTERID from EIM_SEMESTER where SEMESTERNM='" + ddlSemisNM.Text + "'", lblSemID);
            ddlProgramNM.Focus();
        }

        protected void ddlProgramNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.lblAdd("select PROGRAMID from dbo.EIM_PROGRAM where PROGRAMNM='" + ddlProgramNM.Text + "'", lblProID);
            if (ddlProgramNM.Text == "Select")
            {
                txtExmDT.Text = "";
            }
            else
            {
                if (ddlExmYr.Text == "Select")
                {
                    ddlProgramNM.SelectedIndex = -1;
                    ddlExmYr.Focus();
                }
                else if (ddlSemisNM.Text == "Select")
                {
                    ddlProgramNM.SelectedIndex = -1;
                    ddlSemisNM.Focus();
                }
                else
                {
                    //Global.txtAdd("select convert(nvarchar(10),EIM_ADMTEST.TESTDT,103) from EIM_ADMTEST where TESTYY='" + ddlExmYr.Text + "' and  SEMESTERID='" + lblSemID.Text + "' and PROGRAMID ='" + lblProID.Text + "'", txtExmDT);
                    txtStuNM.Focus();
                }
            }

        }

        protected void txtDtOfBth_TextChanged(object sender, EventArgs e)
        {
            txtEML.Focus();
        }


        private void MR_NO()
        {
            Global.lblAdd("SELECT MAX(MRNO) FROM EIM_ADMISSION", lblMRNO);
            if (lblMRNO.Text == "")
            {
                iob.MrNo = 1;
                txtMRNO.Text = iob.MrNo.ToString();
            }
            else
            {
                iob.MrNo = Convert.ToInt64(lblMRNO.Text) + 1;
                txtMRNO.Text = iob.MrNo.ToString();
            }

        }
        protected void txtMRDT_TextChanged(object sender, EventArgs e)
        {
            string Date = txtMRDT.Text;
            if (txtMRNO.Text == "")
                txtMRNO.Focus();
            else
                txtMRYR.Text = Date.Substring(6, 4);
            MR_NO();
            txtMRAMNT.Focus();
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "EDIT")
            {
                btnEdit.Text = "NEW ENTRY";
                btnInsertAdm.Text = "UPDATE";
                ddlMRNO.Visible = true;
                lblMRNAME.Visible = true;
                GridEdit.Visible = true;
                Global.dropDownAddTrans(ddlMRNO, "SELECT MRNO FROM EIM_ADMISSION ORDER BY MRNO");
                btnPrint.Visible = true;
                chkPrint.Visible = false;
                btnDLT.Visible = true;
                btnKeep.Visible = false;
                txtNOXM.Visible = false;
                txtSSN.Visible = false;
                txtGNM.Visible = false;
                txtBRD.Visible = false;
                txtYOPASS.Visible = false;
                txtGPA.Visible = false;
                txtLTRGRD.Visible = false;
                ddlMRNO.Focus();
                lblMSG1.Visible = false;
                ddlExmYr.Enabled = false;
                ddlSemisNM.Enabled = false;
                ddlProgramNM.Enabled = false;
                ddlExmYr.SelectedIndex = -1;
                txtExmDT.Text = "";
            }
            else
            {
                ddlExmYr.Enabled = true;
                ddlSemisNM.Enabled = true;
                ddlProgramNM.Enabled = true;
                btnEdit.Text = "EDIT";
                btnInsertAdm.Text = "SUBMIT";
                ddlMRNO.Visible = false;
                lblMRNAME.Visible = false;
                btnPrint.Visible = false;
                chkPrint.Visible = true;
                GridEdit.Visible = false;
                btnDLT.Visible = false;
                btnKeep.Visible = true;
                txtNOXM.Visible = true;
                txtSSN.Visible = true;
                txtGNM.Visible = true;
                txtBRD.Visible = true;
                txtYOPASS.Visible = true;
                txtGPA.Visible = true;
                txtLTRGRD.Visible = true;
                lblMSG1.Visible = false;
                Clear();
                ddlSemisNM.Focus();
                txtMRDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                txtExmDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                txtMRYR.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                MR_NO();
            }
        }

        private Unit Size(int p)
        {
            throw new NotImplementedException();
        }

        protected void txtStuNM_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMRNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMRNO.Text == "")
            {
                Clear();
                ddlMRNO.Focus();
            }
            else
            {
                if (con.State != ConnectionState.Open)con.Open();
                string Script = @"SELECT     EIM_ADMISSION.TESTYY, EIM_ADMISSION.IMAGE, EIM_ADMISSION.FORMNO, EIM_SEMESTER.SEMESTERNM, EIM_ADMISSION.SEMESTERID, EIM_PROGRAM.PROGRAMNM, 
                      EIM_ADMISSION.PROGRAMID, EIM_ADMISSION.TESTDT, EIM_ADMISSION.ROLLNO, EIM_ADMISSION.STUDENTNM, EIM_ADMISSION.FATHERNM, EIM_ADMISSION.MOTHERNM, 
                      EIM_ADMISSION.ADDRPRE, EIM_ADMISSION.ADDRPER, EIM_ADMISSION.MOBNO, EIM_ADMISSION.EMAIL, EIM_ADMISSION.NATIONALITY, EIM_ADMISSION.RELIGION, EIM_ADMISSION.DOB, 
                      EIM_ADMISSION.GENDER, EIM_ADMISSION.GUARDIANNM, EIM_ADMISSION.GRELATION, EIM_ADMISSION.GPROFESSION, EIM_ADMISSION.GADDRESS, EIM_ADMISSION.GMOBNO, 
                      EIM_ADMISSION.GEMAIL, EIM_ADMISSION.MRDT, EIM_ADMISSION.MRYY, EIM_ADMISSION.MRNO, EIM_ADMISSION.MRAMT
                      FROM  EIM_ADMISSION INNER JOIN
                      EIM_SEMESTER ON EIM_ADMISSION.SEMESTERID = EIM_SEMESTER.SEMESTERID INNER JOIN
                      EIM_PROGRAM ON EIM_ADMISSION.PROGRAMID = EIM_PROGRAM.PROGRAMID WHERE MRNO='"+ddlMRNO.Text+"'";
                SqlCommand cmd = new SqlCommand(Script, con);
                SqlDataReader DR = cmd.ExecuteReader();
                GridEdit.Visible = true;
                while (DR.Read())
                {

                    ddlExmYr.Text = DR["TESTYY"].ToString();
                    lblImagePath.Text = DR["IMAGE"].ToString();
                    txtFormNO.Text = DR["FORMNO"].ToString();
                    lblSemID.Text = DR["SEMESTERID"].ToString();
                    ddlSemisNM.Text = DR["SEMESTERNM"].ToString();
                    lblProID.Text = DR["PROGRAMID"].ToString();
                    ddlProgramNM.Text = DR["PROGRAMNM"].ToString();
                    txtExmDT.Text = DR["TESTDT"].ToString();
                    lblRoll.Text = DR["ROLLNO"].ToString();
                    txtStuNM.Text = DR["STUDENTNM"].ToString();
                    txtStuFNM.Text = DR["FATHERNM"].ToString();
                    txtStuMNM.Text = DR["MOTHERNM"].ToString();
                    txtPreAdrs1.Text = DR["ADDRPRE"].ToString();
                    txtPerAdrs1.Text = DR["ADDRPER"].ToString();
                    txtMblNO.Text = DR["MOBNO"].ToString();
                    txtEML.Text = DR["EMAIL"].ToString();
                    txtNation.Text = DR["NATIONALITY"].ToString();
                    ddlReli.Text = DR["RELIGION"].ToString();
                    txtDtOfBth.Text = DR["DOB"].ToString();
                    ddlGndr.Text = DR["GENDER"].ToString();
                    txtGNM.Text = DR["GUARDIANNM"].ToString();
                    txtGurStuRel.Text = DR["GRELATION"].ToString();
                    txtGurProf.Text = DR["GPROFESSION"].ToString();
                    txtGurAdrs.Text = DR["GADDRESS"].ToString();
                    txtGurPhnNO.Text = DR["GMOBNO"].ToString();
                    txtGurEML.Text = DR["GEMAIL"].ToString();
                    txtMRDT.Text = DR["MRDT"].ToString();
                    txtMRYR.Text = DR["MRYY"].ToString();
                    txtMRNO.Text = DR["MRNO"].ToString();
                    txtMRAMNT.Text = DR["MRAMT"].ToString();
                    gridShow();

                }
                DR.Close();
                if (con.State != ConnectionState.Closed)con.Close();
                //GridEdit.Visible = true;
                //Global.lblAdd("SELECT TESTYY FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", lblYR);
                //ddlExmYr.Text = lblYR.Text;
                //Global.lblAdd("SELECT SEMESTERID FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", lblSemID);
                //Global.lblAdd("SELECT SEMESTERNM FROM EIM_SEMESTER WHERE SEMESTERID='" + lblSemID.Text + "'", lblSemNM);
                //ddlSemisNM.Text = lblSemNM.Text;
                //Global.lblAdd("SELECT PROGRAMID FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", lblProID);
                //Global.lblAdd("SELECT PROGRAMNM FROM EIM_PROGRAM WHERE PROGRAMID='" + lblProID.Text + "'", lblProNM);
                //ddlProgramNM.Text = lblProNM.Text;
                //Global.txtAdd("SELECT convert(nvarchar(10),EIM_ADMISSION.TESTDT,103) FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtExmDT);
                //Global.txtAdd("SELECT STUDENTNM FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtStuNM);
                //Global.txtAdd("SELECT FATHERNM FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtStuFNM);
                //Global.txtAdd("SELECT MOTHERNM FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtStuMNM);
                //Global.txtAdd("SELECT ADDRPRE FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtPreAdrs1);
                //Global.txtAdd("SELECT ADDRPER FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtPerAdrs1);
                //Global.txtAdd("SELECT MOBNO FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtMblNO);
                //Global.txtAdd("SELECT EMAIL FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtEML);
                //Global.txtAdd("SELECT NATIONALITY FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtNation);
                //Global.lblAdd("SELECT RELIGION FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", lblrel);
                //ddlReli.Text = lblrel.Text;
                //Global.txtAdd("SELECT DOB FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtDtOfBth);
                //Global.lblAdd("SELECT GENDER FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", lblGNDR);
                //ddlGndr.Text = lblGNDR.Text;
                //gridShow();
                //Global.txtAdd("SELECT GUARDIANNM FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtGNM);
                //Global.txtAdd("SELECT GRELATION FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtGurStuRel);
                //Global.txtAdd("SELECT GPROFESSION FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtGurProf);
                //Global.txtAdd("SELECT GADDRESS FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtGurAdrs);
                //Global.txtAdd("SELECT GMOBNO FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtGurPhnNO);
                //Global.txtAdd("SELECT GEMAIL FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtGurEML);
                //Global.txtAdd("SELECT MRNO FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtMRNO);
                //Global.txtAdd("SELECT MRYY FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtMRYR);
                //Global.txtAdd("SELECT convert(nvarchar(10),EIM_ADMISSION.MRDT,103) FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtMRDT);
                //Global.txtAdd("SELECT MRAMT FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtMRAMNT);
                //Global.lblAdd("SELECT IMAGE FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", lblImagePath);
                //Global.lblAdd("SELECT ROLLNO FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", lblRoll);
                //Global.txtAdd("SELECT FORMNO FROM EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", txtFormNO);

                // FileUpload1.PostedFile.InputStream.Read(lblImagePath.Text);
            }

        }

        protected void txtLTRGRD_TextChanged(object sender, EventArgs e)
        {
            if (txtLTRGRD.Text == "")
                txtLTRGRD.Focus();
            else
                btnKeep.Focus();
        }

        protected void GridEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TextBox txtEXAMNMFooter = (TextBox)GridEdit.FooterRow.FindControl("txtEXAMNMFooter");
            TextBox txtSESSIONFooter = (TextBox)GridEdit.FooterRow.FindControl("txtSESSIONFooter");
            TextBox txtGROUPSUBFooter = (TextBox)GridEdit.FooterRow.FindControl("txtGROUPSUBFooter");
            TextBox txtBOARDUNIFooter = (TextBox)GridEdit.FooterRow.FindControl("txtBOARDUNIFooter");
            TextBox txtPASSYYFooter = (TextBox)GridEdit.FooterRow.FindControl("txtPASSYYFooter");
            TextBox txtGPAMARKSFooter = (TextBox)GridEdit.FooterRow.FindControl("txtGPAMARKSFooter");
            TextBox txtDIVGRADEFooter = (TextBox)GridEdit.FooterRow.FindControl("txtDIVGRADEFooter");
            iob.UserID = Session["UserName"].ToString();
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.PcName = Session["PCName"].ToString();
            iob.InTime = Global.DateTimeZoon(DateTime.Now);

            if (e.CommandName.Equals("Add"))
            {
                if (txtEXAMNMFooter.Text == "")
                    txtEXAMNMFooter.Focus();
                else if (txtSESSIONFooter.Text == "")
                    txtSESSIONFooter.Focus();
                else if (txtGROUPSUBFooter.Text == "")
                    txtGROUPSUBFooter.Focus();
                else if (txtBOARDUNIFooter.Text == "")
                    txtBOARDUNIFooter.Focus();
                else if (txtPASSYYFooter.Text == "")
                    txtPASSYYFooter.Focus();
                else if (txtGPAMARKSFooter.Text == "")
                    txtGPAMARKSFooter.Focus();
                else if (txtDIVGRADEFooter.Text == "")
                    txtDIVGRADEFooter.Focus();
                else
                {
                    int SL = 0;
                    Global.lblAdd("SELECT MAX(EXAMSL) FROM EIM_ADMEDUQ WHERE SEMESTERID='" + lblSemID.Text + "' AND PROGRAMID='" + lblProID.Text + "' AND TESTYY='" + ddlExmYr.Text + "' AND MRNO='" + ddlMRNO.Text + "'", lblSL);
                    int SLNO = int.Parse(lblSL.Text);
                    if (lblSL.Text == "")
                    {
                        SL = 1;
                    }
                    else
                    {
                        SL = SLNO + 1;
                    }
                    string Date1 = DateTime.Parse(txtExmDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal).ToString("yyyy-MM-dd");

                    iob.YR = int.Parse(ddlExmYr.Text);
                    iob.ExamDT = Convert.ToDateTime(Date1);
                    iob.SemID = int.Parse(lblSemID.Text);
                    iob.ProgID = lblProID.Text;
                    iob.Roll = int.Parse(lblRoll.Text);
                    iob.ExamSL = SL;
                    iob.ExamNM = txtEXAMNMFooter.Text;
                    iob.SeSN = txtSESSIONFooter.Text;
                    iob.GRP = txtGROUPSUBFooter.Text;
                    iob.BRD = txtBOARDUNIFooter.Text;
                    iob.PassYR = txtPASSYYFooter.Text;
                    iob.GPA = txtGPAMARKSFooter.Text;
                    iob.LtrGRD = txtDIVGRADEFooter.Text;
                    iob.MrNo = Convert.ToInt64(ddlMRNO.Text);
                    dob.Insert_EIM_ADMEDUQ(iob);

                    gridShow();
                }
            }
        }

        protected void GridEdit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblEXAMSL = (Label)GridEdit.Rows[e.RowIndex].FindControl("lblEXAMSL");
            if (con.State != ConnectionState.Open)con.Open();

            SqlCommand cmd = new SqlCommand(@"Delete from EIM_ADMEDUQ " +
                       " where PROGRAMID= '" + lblProID.Text + "' AND SEMESTERID='" + lblSemID.Text + "' AND ROLLNO='" + lblRoll.Text + "' AND TESTYY='" + ddlExmYr.Text + "' AND" +
                       " EXAMSL='" + lblEXAMSL.Text + "'", con);
            cmd.ExecuteNonQuery();
            if (con.State != ConnectionState.Closed)con.Close();
            gridShow();

            TextBox txtEXAMNMFooter = (TextBox)GridEdit.FooterRow.FindControl("txtEXAMNMFooter");
            txtEXAMNMFooter.Focus();
            // gridShow();
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
                if (Session["UserName"]==null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    lblMSG1.Text = "";
                    TextBox txtEXAMNMEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtEXAMNMEdit");
                    Label lblEXAMSLEdit = (Label)GridEdit.Rows[e.RowIndex].FindControl("lblEXAMSLEdit");
                    TextBox txtSESSIONEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtSESSIONEdit");
                    TextBox txtGROUPSUBEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtGROUPSUBEdit");
                    TextBox txtBOARDUNIEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtBOARDUNIEdit");
                    TextBox txtPASSYYEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtPASSYYEdit");
                    TextBox txtGPAMARKSEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtGPAMARKSEdit");
                    TextBox txtDIVGRADEEdit = (TextBox)GridEdit.Rows[e.RowIndex].FindControl("txtDIVGRADEEdit");
                    if (txtEXAMNMEdit.Text == "")
                    {
                        txtEXAMNMEdit.Focus();
                    }
                    else if (txtSESSIONEdit.Text == "")
                    {
                        txtSESSIONEdit.Focus();
                    }
                    else if (txtGROUPSUBEdit.Text == "")
                    {
                        txtGROUPSUBEdit.Focus();
                    }
                    else if (txtBOARDUNIEdit.Text == "")
                    {
                        txtBOARDUNIEdit.Focus();
                    }
                    else if (txtPASSYYEdit.Text == "")
                    {
                        txtPASSYYEdit.Focus();
                    }
                    else if (txtGPAMARKSEdit.Text == "")
                    {
                        txtGPAMARKSEdit.Focus();
                    }
                    else if (txtDIVGRADEEdit.Text == "")
                    {
                        txtDIVGRADEEdit.Focus();
                    }
                    else
                    {
                        //DateTime Date = DateTime.Parse(txtTESTDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.UPDUserID = Session["UserName"].ToString();
                        iob.UPDIpaddress = Session["IpAddress"].ToString();
                        iob.UPDPcName = Session["PCName"].ToString();
                        iob.UPDTime = DateTime.Now;

                        string Date1 = DateTime.Parse(txtExmDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal).ToString("yyyy-MM-dd");

                        iob.YR = int.Parse(ddlExmYr.Text);
                        iob.ExamDT = Convert.ToDateTime(Date1);
                        iob.SemID = int.Parse(lblSemID.Text);
                        iob.ProgID = lblProID.Text;
                        iob.Roll = int.Parse(lblRoll.Text);
                        iob.ExamSL = int.Parse(lblEXAMSLEdit.Text);
                        iob.ExamNM = txtEXAMNMEdit.Text;
                        iob.SeSN = txtSESSIONEdit.Text;
                        iob.GRP = txtGROUPSUBEdit.Text;
                        iob.BRD = txtBOARDUNIEdit.Text;
                        iob.PassYR = txtPASSYYEdit.Text;
                        iob.GPA = txtGPAMARKSEdit.Text;
                        iob.LtrGRD = txtDIVGRADEEdit.Text;
                        iob.MrNo = Convert.ToInt64(ddlMRNO.Text);
                        dob.Update_EIM_ADMEDUQ(iob);
                        GridEdit.EditIndex = -1;
                        gridShow();
                        lblMSG1.Visible = true;
                        lblMSG1.Text = "Updated !";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

        protected void btnDLT_Click(object sender, EventArgs e)
        {
            try
            {
                lblMSG1.Text = "";
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand(@"Delete from EIM_ADMEDUQ WHERE MRNO='" + ddlMRNO.Text + "'", con);
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand(@"Delete from EIM_ADMISSION WHERE MRNO='" + ddlMRNO.Text + "'", con);
                cmd1.ExecuteNonQuery();
                if (con.State != ConnectionState.Closed)con.Close();
                Clear();
                Global.dropDownAddTrans(ddlMRNO, "SELECT MRNO FROM EIM_ADMISSION ORDER BY MRNO");
                ddlMRNO.Focus();
                lblMSG1.Visible = true;
                lblMSG1.Text = "Deleted !";
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (ddlExmYr.Text == "Select")
            {
                ddlExmYr.Focus();
            }

            else if (ddlSemisNM.Text == "Select")
            {
                ddlSemisNM.Focus();
            }
            else if (ddlProgramNM.Text == "Select")
            {
                ddlProgramNM.Focus();
            }
            else if (txtStuNM.Text == "")
            {
                txtStuNM.Focus();
            }
            //else if (txtStuFNM.Text == "")
            //{
            //    txtStuFNM.Focus();
            //}
            //else if (txtStuMNM.Text == "")
            //{
            //    txtStuMNM.Focus();
            //}

            //else if (txtMRYR.Text == "")
            //{
            //    txtMRDT.Focus();
            //}
            //else if (txtMRAMNT.Text == "")
            //{
            //    txtMRAMNT.Focus();
            //}

            else
            {
                Session["MRNO"] = txtMRNO.Text;
                Session["MRDT"] = txtMRDT.Text;
                Session["FRNO"] = txtFormNO.Text;
                Global.lblAdd("SELECT PROGRAMSID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgramNM.Text + "'", lblProSNM);
                Session["PROGRAMSNM"] = lblProSNM.Text;
                Session["SEMESTERNM"] = ddlSemisNM.Text;
                Session["STUDENTNM"] = txtStuNM.Text;
                Session["AMOUNT"] = txtMRAMNT.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                        "OpenWindow", "window.open('/Admission/Report/rptMoneyReceipt.aspx','_newtab');", true);
            }
        }




    }
}