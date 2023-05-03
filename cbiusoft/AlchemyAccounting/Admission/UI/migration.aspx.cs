using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting.Admission.DataAccess;
using AlchemyAccounting.Admission.DataModel;

namespace AlchemyAccounting.Admission.UI
{
    public partial class migration : System.Web.UI.Page
    {
        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection conn = new SqlConnection(Global.connection);
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true); 
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.dropDownAddWithValue(ddlProgNMFrom, @"SELECT PROGRAMNM,PROGRAMID FROM EIM_PROGRAM");
                Global.dropDownAddWithValue(ddlSemNMFrom, @"SELECT SEMESTERNM,SEMESTERID FROM EIM_SEMESTER");
                Global.dropDownAddWithValue(ddlProgNMTo, @"SELECT PROGRAMNM,PROGRAMID FROM EIM_PROGRAM");
                Global.dropDownAddWithValue(ddlSemNMTo, @"SELECT SEMESTERNM,SEMESTERID FROM EIM_SEMESTER");
                string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                int i, m;
                int a = int.Parse(yr);
                m = a + 5;
                for (i = a - 5; i <= m; i++)
                {
                    ddlRegYR.Items.Add(i.ToString());
                }
                ddlRegYR.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                txtdate.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                ddlRegYR.Focus();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionStudentID(string prefixText, int count, string contextKey)
        {

            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection 
            string PROGRAMID = HttpContext.Current.Session["PROGRAMID"].ToString();
            SqlCommand cmd = new SqlCommand("SELECT STUDENTID FROM EIM_STUDENT WHERE STUDENTID LIKE '" + prefixText + "%' AND PROGRAMID='" + PROGRAMID + "'", conn);
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

        private string StuID()
        {
            string YrSemPro = ddlRegYR.Text + ddlSemNMTo.SelectedValue + ddlProgNMTo.SelectedValue;
            //string StuIDs;
            Label lblStuID = new Label();
            Global.lblAdd("SELECT MAX(STUDENTID) from EIM_STUDENT where SEMESTERID='" + ddlSemNMTo.SelectedValue + "' and PROGRAMID='" + ddlProgNMTo.SelectedValue + "' and ADMITYY='" + ddlRegYR.Text + "'", lblStuID);
            int ID = 0;
            string StuID = "";
            if (lblStuID.Text == "")
                StuID = YrSemPro + "0001";
            else
            {
                StuID = lblStuID.Text.Substring(7, 4);
                ID = int.Parse(StuID) + 1;
                if (ID < 10)
                    StuID = YrSemPro + "000" + ID;
                else if (ID < 100)
                    StuID = YrSemPro + "00" + ID;
                else if (ID < 1000)
                    StuID = YrSemPro + "0" + ID;
                else if (ID < 10000)
                    StuID = YrSemPro + ID;
            }
            return StuID;
        }

        protected void ddlSemNMFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemNMFrom.Text == "Select")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Semester !";
            }
            else
            {
                lblMSG.Visible = false;
                Session["SEMESTERFR"] = "";
                Session["SEMESTERFR"] = ddlSemNMFrom.SelectedValue;
                ddlProgNMFrom.Focus();
            }
        }

        protected void ddlProgNMFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgNMFrom.Text == "Select")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Program !";
            }
            else
            {
                lblMSG.Visible = false;
                Session["PROGRAMIDFR"] = "";
                Session["PROGRAMIDFR"] = ddlProgNMFrom.SelectedValue;
                txtStudentIDFrom.Focus();
            }
        }

        protected void ddlSemNMTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemNMTo.Text == "Select")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Semester !";
            }
            else
            {
                lblMSG.Visible = false;
                Session["SEMESTERFR"] = "";
                Session["SEMESTERFR"] = ddlSemNMTo.SelectedValue;
                ddlProgNMTo.Focus();
            }
        }

        protected void ddlProgNMTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgNMTo.Text == "Select")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Program !";
            }
            else
            {
                lblMSG.Visible = false;
                Session["PROGRAMIDTO"] = "";
                Session["PROGRAMIDTO"] = ddlProgNMTo.SelectedValue;
                btnMigrat.Focus();
            }
        }

        protected void txtStudentIDFrom_TextChanged(object sender, EventArgs e)
        {
            if (txtStudentIDFrom.Text == "")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Student ID !";
                txtStudentIDFrom.Focus();
            }
            else
            {
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE STUDENTID='" + txtStudentIDFrom.Text + "'", txtStudentNMFrom);
                ddlSemNMTo.Focus();
            }
        }

        protected void btnMigrat_Click(object sender, EventArgs e)
        {
            if (ddlRegYR.Text == "Select")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Year !";
                ddlRegYR.Focus();
            }
            else if (ddlSemNMFrom.Text == "Select")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Semester !";
                ddlSemNMFrom.Focus();
            }
            else if (ddlProgNMFrom.Text == "Select")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Program !";
                ddlProgNMFrom.Focus();
            }
            else if (ddlSemNMTo.Text == "Select")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Semester !";
                ddlSemNMTo.Focus();
            }
            else if (ddlProgNMTo.Text == "Select")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Program !";
                ddlProgNMTo.Focus();
            }
            else
            {
                Image1.Visible = true;
                lblMSG.Visible = false;
                //                //LogInsert Start   
                //                Label lblDescript = new Label();
                //                GridView gv = new GridView();
                //                Global.gridViewAdd(gv, @"SELECT ISNULL(ADMITYY,'(NULL)')+'  '+ISNULL(IMAGE,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),ADMITSL,103),'(NULL)')+'  '+
                //                ISNULL(CONVERT(NVARCHAR(50),ADMITDT,103),'(NULL)')+'  '+ISNULL(ADMITTP,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),SEMESTERID,103),'(NULL)')+'  '+
                //                ISNULL(PROGRAMID,'(NULL)')+'  '+ISNULL(SESSION,'(NULL)')+'  '+ISNULL(BATCH,'(NULL)')+'  '+STUDENTID+'  '+ISNULL(STUDENTNM,'(NULL)')+'  '+
                //                ISNULL(FATHERNM,'(NULL)')+'  '+ISNULL(FATHEROCP,'(NULL)')+'  '+ISNULL(FOCPDTL,'(NULL)')+'  '+ISNULL(MOTHERNM,'(NULL)')+'  '+ISNULL(MOTHEROCP,'(NULL)')+'  '+
                //                ISNULL(SPOUSENM,'(NULL)')+'  '+ISNULL(SPOUSEOCP,'(NULL)')+'  '+ISNULL(ADDRPRE,'(NULL)')+'  '+ISNULL(ADDRPER,'(NULL)')+'  '+ISNULL(TELNO,'(NULL)')+'  '+
                //                ISNULL(MOBNO,'(NULL)')+'  '+ISNULL(EMAIL,'(NULL)')+'  '+ISNULL(NATIONALITY,'(NULL)')+'  '+ISNULL(RELIGION,'(NULL)')+'  '+ISNULL(DOB,'(NULL)')+'  '+
                //                ISNULL(GENDER,'(NULL)')+'  '+ISNULL(STUDENTTP,'(NULL)')+'  '+ISNULL(BIRTHP,'(NULL)')+'  '+ISNULL(NIDPNO,'(NULL)')+'  '+ISNULL(BLOODGR,'(NULL)')+'  '+
                //                ISNULL(PRESIDENCE,'(NULL)')+'  '+ISNULL(MSTATUS,'(NULL)')+'  '+ISNULL(RESHOSTEL,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INCOMEYY,103),'(NULL)')+'  '+
                //                ISNULL(CONVERT(NVARCHAR(50),EXPENSEYY,103),'(NULL)')+'  '+ISNULL(GUARDIANNM,'(NULL)')+'  '+ISNULL(GRELATION,'(NULL)')+'  '+ISNULL(GADDRESS,'(NULL)')+'  '+
                //                ISNULL(GTELNO,'(NULL)')+'  '+ISNULL(GMOBNO,'(NULL)')+'  '+ISNULL(GEMAIL,'(NULL)')+'  '+ISNULL(PREPROGTP,'(NULL)')+'  '+ISNULL(PREPROGNM,'(NULL)')+'  '+
                //                ISNULL(PPINSTITN,'(NULL)')+'  '+ISNULL(PPSESSION,'(NULL)')+'  '+ISNULL(FIRMNM,'(NULL)')+'  '+ISNULL(POSITION,'(NULL)')+'  '+ISNULL(REMARKS,'(NULL)')+'  '+
                //                ISNULL(USERID,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                //                ISNULL(UPDUSERID,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(UPDIPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)')+'  ' 
                //                FROM EIM_STUDENT WHERE STUDENTID='" + txtStudentIDFrom.Text + "''");
                //                iob.TableID = "EIM_STUDENT";
                //                iob.Type = "UPDATE";
                //                foreach (GridViewRow row in gv.Rows)
                //                {
                //                    iob.DescrIP = row.Cells[0].Text;
                //                    dob.INSERT_LOG(iob);
                //                } 
                //                //LogInsert End 

                DataSet ds = new DataSet();
                String ScriptEim_Student = @"SELECT  ADMITYY, IMAGE, ADMITSL, ADMITDT, ADMITTP, SEMESTERID, PROGRAMID, SESSION, BATCH, STUDENTID, STUDENTNM, WAIVER, FATHERNM, FATHEROCP, FOCPDTL, MOTHERNM, MOTHEROCP, 
                SPOUSENM, SPOUSEOCP, ADDRPRE, ADDRPER, TELNO, MOBNO, EMAIL, NATIONALITY, RELIGION, DOB, GENDER, STUDENTTP, BIRTHP, NIDPNO, BLOODGR, PRESIDENCE, MSTATUS, RESHOSTEL, 
                INCOMEYY, EXPENSEYY, GUARDIANNM, GRELATION, GADDRESS, GTELNO, GMOBNO, GEMAIL, PREPROGTP, PREPROGNM, PPINSTITN, PPSESSION, FIRMNM, POSITION, REMARKS,SCHOLAR
                FROM EIM_STUDENT WHERE STUDENTID='" + txtStudentIDFrom.Text + "'";

                String ScriptEim_StuEDU = @" SELECT        STUDENTID, EXAMSL, EXAMNM, PASSYY, EXAMROLL, GROUPSUB, DIVGRADE, INSTITUTE, BOARDUNI
                FROM  IM_STUEDUQ WHERE STUDENTID='" + txtStudentIDFrom.Text + "'";

                String ScriptEim_Trans = @"SELECT        TRANSDT, TRANSTP, TRANSYY, TRANSNO, REGYY, SEMESTERID, CNBCD, PROGRAMID, STUDENTID, FEESID, WAIVER, SCHOLAR, AMOUNT, VATAMOUNT, REMARKS
                FROM  EIM_TRANS  STUDENTID='" + txtStudentIDFrom.Text + "'";
                String ScriptEim_TransMST = @"SELECT        TRANSDT, TRANSTP, TRANSYY, TRANSNO, REGYY, SEMESTERID, PROGRAMID, STUDENTID, CNBCD, PONO, PODT, POBANK, POBANKBR, REMARKS
                FROM    EIM_TRANSMST  STUDENTID='" + txtStudentIDFrom.Text + "'";
                // Update Eim_Student Start
                Global.gridViewAdd(gv_Student, ScriptEim_Student);
                Global.gridViewAdd(gv_StuEdu, ScriptEim_StuEDU);
                Global.gridViewAdd(gv_Trans, ScriptEim_Trans);

                string StudentID = StuID();
                if (conn.State != ConnectionState.Open)conn.Open();
                string Script = "UPDATE EIM_STUDENT SET STUDENTID='" + StudentID + "',PROGRAMID='" + ddlProgNMTo.SelectedValue + "'  WHERE STUDENTID='" + txtStudentIDFrom.Text + "'";
                SqlCommand cmd = new SqlCommand(Script, conn);
                cmd.ExecuteNonQuery();
                // Update Eim_Student End

                // Update Eim_Student Start 
                if (conn.State != ConnectionState.Open)conn.Open();
                Script = "UPDATE EIM_TRANS SET STUDENTID='" + StudentID + "',PROGRAMID='" + ddlProgNMTo.SelectedValue + "'  WHERE STUDENTID='" + txtStudentIDFrom.Text + "'";
                SqlCommand cmd1 = new SqlCommand(Script, conn);
                cmd1.ExecuteNonQuery();
                // Update Eim_Student End 

                Image1.Visible = false;
            }
        }
        private void StudentInfoInsert()
        {
            iob.UserID = Session["UserName"].ToString();
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.PcName = Session["PCName"].ToString();
            iob.InTime = Global.Dayformat1(DateTime.Now);
            SqlConnection Conn = new SqlConnection(Global.connection);
            Conn.Open();
            foreach (GridViewRow ROW in gv_Student.Rows)
            {
                // Gross = decimal.Parse(ROW.Cells[1].Text); 
                DateTime Date = DateTime.Parse(txtdate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                iob.AdmtYR = ddlRegYR.Text; // New Data
                Label lblAdmtSL = new Label();
                Global.lblAdd("SELECT MAX(ADMITSL) FROM EIM_STUDENT", lblAdmtSL);
                if (lblAdmtSL.Text == "")
                {
                    iob.AdmtSL = 1;
                }
                else
                {
                    iob.AdmtSL = int.Parse(lblAdmtSL.Text) + 1;
                }
                iob.ADMITDT = Convert.ToDateTime(ROW.Cells[2].Text);
                iob.ADMITTP = ROW.Cells[3].Text;
                iob.SemID = int.Parse(ddlSemNMTo.SelectedValue); // New Data
                iob.ProgID = ddlProgNMTo.SelectedValue; // New Data
                iob.SeSN = ROW.Cells[6].Text;
                iob.Batch = ROW.Cells[7].Text;
                iob.StuID = txtStudentIDNew.Text;   // New Data
                iob.StuNM = ROW.Cells[9].Text;
                iob.Waiver = Convert.ToDecimal(ROW.Cells[10].Text);
                iob.StuFNM = ROW.Cells[11].Text;
                iob.FOcup = ROW.Cells[12].Text;
                iob.FOcupDTL = ROW.Cells[13].Text;
                iob.StuMNM = ROW.Cells[14].Text;
                iob.MOcup = ROW.Cells[15].Text;
                iob.SPuseNM = ROW.Cells[16].Text;
                iob.SpuseOcup = ROW.Cells[17].Text;
                iob.PreAdrs = ROW.Cells[18].Text;
                iob.PerAdrs = ROW.Cells[19].Text;
                iob.TelePhn = ROW.Cells[20].Text;
                iob.MobNO = ROW.Cells[21].Text;
                iob.Email = ROW.Cells[22].Text;
                iob.Nation = ROW.Cells[23].Text;
                iob.Religion = ROW.Cells[24].Text;
                iob.DtOfBrt = ROW.Cells[25].Text;
                iob.Gander = ROW.Cells[26].Text;
                iob.StuTP = ROW.Cells[27].Text;
                iob.PofB = ROW.Cells[28].Text;
                iob.NIDPNO = ROW.Cells[29].Text;
                iob.BldGRP = ROW.Cells[30].Text;
                iob.PRecdnc = ROW.Cells[31].Text;
                iob.MSTTS = ROW.Cells[32].Text;
                iob.Hstl = ROW.Cells[33].Text;
                if (ROW.Cells[34].Text == "")
                    ROW.Cells[34].Text = "0";
                iob.Incm = Decimal.Parse(ROW.Cells[34].Text);
                if (ROW.Cells[35].Text == "")
                    ROW.Cells[35].Text = "0";
                iob.Expncy = Decimal.Parse(ROW.Cells[35].Text);
                iob.GNM = ROW.Cells[36].Text;
                iob.GRel = ROW.Cells[37].Text;
                iob.GAdrs = ROW.Cells[38].Text;
                iob.GTelePhn = ROW.Cells[39].Text;
                iob.GMNo = ROW.Cells[40].Text;
                iob.GEml = ROW.Cells[41].Text;
                iob.PreProTP = "";
                iob.PreProNM = ROW.Cells[43].Text;
                iob.PreInsNM = ROW.Cells[44].Text;
                iob.PreSSN = ROW.Cells[45].Text;
                iob.FIRMNM = ROW.Cells[46].Text;
                iob.PosiSN = ROW.Cells[47].Text;
                iob.Remarks = ROW.Cells[48].Text;
                iob.Scholar = Convert.ToDecimal(ROW.Cells[49].Text);
                //dob.INSERT_MIGRATE_STK_STUDENT(iob);
                dob.InsertApplicationReg(iob);

            }
        }
        private void StudentEduInfoInsert()
        {
            iob.UserID = Session["UserName"].ToString();
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.PcName = Session["PCName"].ToString();
            iob.InTime = Global.Dayformat1(DateTime.Now);
            SqlConnection Conn = new SqlConnection(Global.connection);
            Conn.Open();
            int Sl = 1;
            int Count = gv_StuEdu.Rows.Count;
            //for (int i = 1; i <= Count; i++)
            //{
            try
            {
                foreach (GridViewRow ROW in gv_Student.Rows)
                {

                    if (Conn.State != ConnectionState.Open)
                        Conn.Open();
                    SqlCommand cmd1 = new SqlCommand(@"INSERT INTO EIM_STUEDUQ (STUDENTID,EXAMSL,EXAMNM,PASSYY,EXAMROLL,
                                             GROUPSUB,DIVGRADE,INSTITUTE,BOARDUNI) VALUES 
                                                       (@STUDENTID,@EXAMSL,@EXAMNM,@PASSYY,@EXAMROLL,
                                             @GROUPSUB,@DIVGRADE,@INSTITUTE,@BOARDUNI)", Conn);

                    cmd1.Parameters.AddWithValue("@STUDENTID", txtStudentIDNew.Text); // New Data
                    cmd1.Parameters.AddWithValue("@EXAMSL", Sl);
                    cmd1.Parameters.AddWithValue("@EXAMNM", ROW.Cells[1].Text);
                    cmd1.Parameters.AddWithValue("@PASSYY", ROW.Cells[2].Text);
                    cmd1.Parameters.AddWithValue("@EXAMROLL", ROW.Cells[3].Text);

                    cmd1.Parameters.AddWithValue("@GROUPSUB", ROW.Cells[4].Text);
                    cmd1.Parameters.AddWithValue("@DIVGRADE", ROW.Cells[5].Text);
                    cmd1.Parameters.AddWithValue("@INSTITUTE", ROW.Cells[6].Text);
                    cmd1.Parameters.AddWithValue("@BOARDUNI", ROW.Cells[7].Text);
                    cmd1.ExecuteNonQuery();
                    Sl++;
                    if (Conn.State != ConnectionState.Closed)
                        Conn.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
        private void TransInfoInsert()
        {
            iob.UserID = Session["UserName"].ToString();
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.PcName = Session["PCName"].ToString();
            iob.InTime = Global.Dayformat1(DateTime.Now);
            SqlConnection Conn = new SqlConnection(Global.connection);
            Conn.Open();
            string TransNO = "";
            foreach (GridViewRow ROW in gv_Student.Rows)
            {
                iob.TrnsDT = Convert.ToDateTime(ROW.Cells[0].Text);
                iob.TransTP = ROW.Cells[1].Text;
                iob.TransYR = int.Parse(ROW.Cells[2].Text);
                iob.TrnsNO = int.Parse(TransNO);
                iob.RegYR = int.Parse(ddlRegYR.Text);
                iob.SemID = int.Parse(ddlSemNMTo.SelectedValue);
                iob.AccNo = ROW.Cells[8].Text;
                iob.ProgID = ddlProgNMTo.SelectedValue;
                iob.StuID = txtStudentIDNew.Text;
                iob.FeesID = ROW.Cells[9].Text;
                iob.Waiver = Convert.ToDecimal(ROW.Cells[10].Text);
                iob.Scholar = Convert.ToDecimal(ROW.Cells[11].Text);
                iob.Amnt = Convert.ToDecimal(ROW.Cells[12].Text);
                iob.Remarks = ROW.Cells[13].Text;
                dob.Insert_EIM_TRANS(iob);
            }
        }
        private void TransMstInfoInsert()
        {
            iob.UserID = Session["UserName"].ToString();
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.PcName = Session["PCName"].ToString();
            iob.InTime = Global.Dayformat1(DateTime.Now);
            SqlConnection Conn = new SqlConnection(Global.connection);
            Conn.Open();
            string TransNO = "";
            foreach (GridViewRow ROW in gv_Student.Rows)
            {
                iob.TrnsDT = Convert.ToDateTime(ROW.Cells[0].Text);
                iob.TransTP = ROW.Cells[1].Text;
                iob.TransYR = int.Parse(ROW.Cells[2].Text);
                iob.TrnsNO = int.Parse(TransNO);
                iob.RegYR = int.Parse(ddlRegYR.Text);
                iob.SemID = int.Parse(ddlSemNMTo.SelectedValue);
                iob.ProgID = ddlProgNMTo.SelectedValue;
                iob.StuID = txtStudentIDNew.Text;
                iob.AccNo = ROW.Cells[8].Text;
                iob.PONO = ROW.Cells[9].Text;
                if (ROW.Cells[10].Text == "")
                    iob.PODT = DateTime.Parse("1999-01-01");
                else
                {
                    DateTime PoDate = DateTime.Parse(ROW.Cells[9].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.PODT = PoDate;
                }
                iob.POBNK = ROW.Cells[11].Text;
                iob.POBRNC = ROW.Cells[12].Text;
                iob.Remarks = ROW.Cells[13].Text;
                dob.Insert_EIM_TRANSMST(iob);
            }
        }
    }
}