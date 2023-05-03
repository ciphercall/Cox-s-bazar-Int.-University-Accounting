using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using AlchemyAccounting.Admission.DataAccess;
using AlchemyAccounting.Admission.DataModel;

namespace AlchemyAccounting.Info.UI
{
    public partial class Member : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection conn = new SqlConnection(Global.connection);
        string UserID = int.Parse("10101").ToString();
        string CMPID = int.Parse("101").ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                if (!IsPostBack)
                {
                    Global.dropDownAddWithSelect(ddlDPT, "SELECT DEPTNM FROM HR_DEPT DEPTNM");
                    Global.dropDownAddWithSelect(ddlPostNM, "SELECT POSTNM FROM HR_POST ORDER BY POSTNM");
                    ddlDPT.Focus();
                }
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionMemberNM(string prefixText, int count, string contextKey)
        {

            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand("SELECT EMPNM FROM HR_EMP WHERE EMPNM like '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["EMPNM"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }
        private void Clear()
        {
            Global.dropDownAddWithSelect(ddlDPT, "SELECT DEPTNM FROM HR_DEPT DEPTNM");
            ddlEmpEdit.SelectedIndex = -1; 
            lblSL.Text = "";
            txtCRDISUDT.Text = "";
            txtCRDNO.Text = "";
            txtEmpBld.Text = "";
            txtEmpCNO.Text = "";
            txtEmpDOB.Text = "";
            txtEMPEmail.Text = "";
            txtEmpGNM.Text = "";
            // txtEMPIDM.Text = "";
            txtEmpMNM.Text = "";
            txtEmpNM.Text = "";
            txtEmpPerAddress.Text = "";
            txtEmpPreAddress.Text = "";
            txtEmpVoterID.Text = "";
            txtJoinDT.Text = "";
            txtRef1Address.Text = "";
            txtRef1CNO.Text = "";
            txtRef1Desig.Text = "";
            txtRef1NM.Text = "";
            txtRef2Address.Text = "";
            txtRef2CNO.Text = "";
            txtRef2Desig.Text = "";
            txtRef2NM.Text = "";
            ddlDPT.SelectedIndex = -1;
            ddlEmpGen.SelectedIndex = -1;
            //ddlEMPTP.SelectedIndex = -1;
            txtBasicSalary.Text = "";
            txtBankAcNO.Text = "";
            ddlPostNM.SelectedIndex = -1;
            ddlSTATS.SelectedIndex = -1;
            lblDeptID.Text = "";
            txtID.Text = "";

        }
        private void NullChack()
        {
            DateTime DefaultDtae = Convert.ToDateTime("01-01-1900");
            if (txtCRDISUDT.Text == "")
                iob.CRDISUDT = DefaultDtae;
            if (txtCRDNO.Text == "")
                iob.CRDNO = "";
            if (txtEmpBld.Text == "")
                iob.BldGRP = "";
            if (txtEMPEmail.Text == "")
                iob.Email = "";
            if (txtEmpGNM.Text == "")
                iob.EmpGNM = "";
            if (txtEmpMNM.Text == "")
                iob.EmpMNM = "";
            if (txtEmpPerAddress.Text == "")
                iob.PerAdrs = "";
            if (txtEmpPreAddress.Text == "")
                iob.PreAdrs = "";
            if (txtEmpVoterID.Text == "")
                iob.NIDPNO = "";
            if (txtJoinDT.Text == "")
                iob.JoinDT = DefaultDtae;
            if (txtRef1Address.Text == "")
                iob.Ref1Adrs = "";
            if (txtRef1CNO.Text == "")
                iob.Ref1CNO = "";
            if (txtRef1Desig.Text == "")
                iob.Ref1Desig = "";
            if (txtRef1NM.Text == "")
                iob.Ref1NM = "";
            if (txtRef2Address.Text == "")
                iob.Ref2Adrs = "";
            if (txtRef2CNO.Text == "")
                iob.Ref2CNO = "";
            if (txtRef2Desig.Text == "")
                iob.Ref2Desig = "";
            if (txtRef2NM.Text == "")
                iob.Ref2NM = "";
            if (ddlEmpGen.Text == "Select")
                iob.Gander = "";
            if (txtBankAcNO.Text == "")
                iob.BankAcNO = "";
            if (txtBasicSalary.Text == "")
                iob.BasicSalary = 0;
            if (ddlSTATS.Text == "Select")
                iob.status = "";
        }
        //private void EMPID()
        //{
        //    string CMPID = int.Parse("101").ToString();
        //    string EMPID = "";
        //    Global.lblAdd("SELECT MAX(EMPID) FROM HR_EMP  WHERE COMPID='" + CMPID + "'", lblEmpID);
        //    if (lblEmpID.Text == "")
        //    {
        //        EMPID = CMPID + "00001";
        //    }
        //    else
        //    {
        //        string Substr = lblEmpID.Text.Substring(3, 5);
        //        int subint = int.Parse(Substr) + 1;
        //        if (subint < 10)
        //        {
        //            EMPID = CMPID + "0000" + subint;
        //        }
        //        else if (subint < 100)
        //        {
        //            EMPID = CMPID + "000" + subint;
        //        }
        //        else if (subint < 1000)
        //        {
        //            EMPID = CMPID + "00" + subint;
        //        }
        //        else if (subint < 10000)
        //        {
        //            EMPID = CMPID + "0" + subint;
        //        }
        //        else if (subint < 100000)
        //        {
        //            EMPID = CMPID + subint;
        //        }

        //    }
        //    iob.EmpID = int.Parse(EMPID);
        //}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/Login.aspx");
            else
            {
                try
                {
                    lblMSG.Text = "";
                    iob.PcName = Session["PCName"].ToString();
                    iob.UserID = Session["UserName"].ToString();
                    iob.Ipaddress = Session["IpAddress"].ToString();
                    iob.InTime = Global.Dayformat1(DateTime.Now);
                    if (txtEmpNM.Text == "")
                        txtEmpNM.Focus();
                    else if (txtEmpCNO.Text == "")
                        txtEmpCNO.Focus();
                    else if (ddlEmpGen.Text == "Select")
                        ddlEmpGen.Focus();
                    else if (txtJoinDT.Text == "")
                        txtJoinDT.Focus();
                    else if (ddlDPT.Text == "Select")
                        ddlDPT.Focus();
                    else if (ddlPostNM.Text == "Select")
                        ddlPostNM.Focus();
                    else if (ddlSTATS.Text == "")
                        ddlSTATS.Focus();
                    else
                    {

                        if (txtEmpDOB.Text == "")
                            txtEmpDOB.Text = "01/01/1900";
                        DateTime DOB = DateTime.Parse(txtEmpDOB.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        DateTime JoinDT = DateTime.Parse(txtJoinDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        if (txtCRDISUDT.Text == "")
                            txtCRDISUDT.Text = "01/01/1900";
                        DateTime CRDISUDT = DateTime.Parse(txtCRDISUDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        //EMPID(); 
                        txtID.Text = EmpIDGenerate(lblDeptSID.Text, lblDeptID.Text);
                        iob.EmpID = txtID.Text;
                        iob.CmpID = int.Parse(CMPID);
                        iob.EmpNM = txtEmpNM.Text;
                        iob.EmpGNM = txtEmpGNM.Text;
                        iob.EmpMNM = txtEmpMNM.Text;
                        iob.PreAdrs = txtEmpPreAddress.Text;
                        iob.PerAdrs = txtEmpPerAddress.Text;
                        iob.EmpCNO = txtEmpCNO.Text;
                        iob.Email = txtEMPEmail.Text;
                        iob.DOB = DOB;
                        iob.Gander = ddlEmpGen.Text;
                        iob.NIDPNO = txtEmpVoterID.Text;
                        iob.BldGRP = txtEmpBld.Text;
                        iob.Ref1NM = txtRef1NM.Text;
                        iob.Ref1Desig = txtRef1Desig.Text;
                        iob.Ref1Adrs = txtRef1Address.Text;
                        iob.Ref1CNO = txtRef1CNO.Text;
                        iob.Ref2NM = txtRef2NM.Text;
                        iob.Ref2Desig = txtRef2Desig.Text;
                        iob.Ref2Adrs = txtRef2Address.Text;
                        iob.Ref2CNO = txtRef2CNO.Text;
                        iob.JoinDT = JoinDT;
                        iob.BankAcNO = txtBankAcNO.Text;
                        iob.DPTID = Convert.ToInt64(lblDeptID.Text);
                        iob.BasicSalary = Decimal.Parse(txtBasicSalary.Text);
                        iob.HRent = Decimal.Parse(txtHRent.Text);
                        iob.MAllownc = Decimal.Parse(txtMAllwnce.Text);
                        iob.EConvey = Decimal.Parse(txtEnterConvey.Text);
                        iob.Other = Decimal.Parse(txtOther.Text);
                        iob.PostID = Convert.ToInt64(lblPostID.Text);

                        iob.status = ddlSTATS.Text;
                        NullChack();
                        string s=dob.Insert_HR_EMP(iob);
                        if (s == "true")
                        {
                            Clear();
                            lblMSG.Visible = true;
                            lblMSG.Text = "Success: One Execute Effected !";
                            ddlDPT.Focus();
                        }
                        else
                        {
                            lblMSG.Visible = true;
                            lblMSG.Text = "Error: Insertion failed !";
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/Login.aspx");
            else
            {
                try
                {
                    lblMSG.Text = "";
                    iob.UPDPcName = Session["PCName"].ToString();
                    iob.UPDUserID = Session["UserName"].ToString();
                    iob.UPDIpaddress = Session["IpAddress"].ToString();
                    iob.UPDTime = Global.Dayformat1(DateTime.Now);
                    if (txtEmpNM.Text == "" && btnEdit.Text == "EDIT")
                        txtEmpNM.Focus();
                    else if (ddlDPT.Text == "Select" && btnEdit.Text != "EDIT")
                        ddlDPT.Focus();
                    else if (txtEmpCNO.Text == "")
                        txtEmpCNO.Focus();
                    else if (ddlEmpGen.Text == "Select")
                        ddlEmpGen.Focus();
                    else if (txtJoinDT.Text == "")
                        txtJoinDT.Focus();
                    else if (ddlDPT.Text == "Select")
                        ddlDPT.Focus();
                    else if (ddlPostNM.Text == "Select")
                        ddlPostNM.Focus();
                    else if (ddlSTATS.Text == "")
                        ddlSTATS.Focus();
                    else
                    {
                        if (txtEmpDOB.Text == "")
                            txtEmpDOB.Text = "01/01/1900";
                        // DateTime DOB = DateTime.Parse(txtEmpDOB.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        DateTime JoinDT = DateTime.Parse(txtJoinDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        if (txtCRDISUDT.Text == "")
                            txtCRDISUDT.Text = "01/01/1900";
                        DateTime CRDISUDT = DateTime.Parse(txtCRDISUDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.EmpID = txtID.Text;
                        iob.SL = Convert.ToInt64(lblSL.Text);
                        iob.CmpID = int.Parse(CMPID); 
                        iob.EmpGNM = txtEmpGNM.Text;
                        iob.EmpMNM = txtEmpMNM.Text;
                        iob.PreAdrs = txtEmpPreAddress.Text;
                        iob.PerAdrs = txtEmpPerAddress.Text;
                        iob.EmpCNO = txtEmpCNO.Text;
                        iob.Email = txtEMPEmail.Text;
                        iob.DtOfBrt = txtEmpDOB.Text;
                        iob.Gander = ddlEmpGen.Text;
                        iob.NIDPNO = txtEmpVoterID.Text;
                        iob.BldGRP = txtEmpBld.Text;
                        iob.Ref1NM = txtRef1NM.Text;
                        iob.Ref1Desig = txtRef1Desig.Text;
                        iob.Ref1Adrs = txtRef1Address.Text;
                        iob.Ref1CNO = txtRef1CNO.Text;
                        iob.Ref2NM = txtRef2NM.Text;
                        iob.Ref2Desig = txtRef2Desig.Text;
                        iob.Ref2Adrs = txtRef2Address.Text;
                        iob.Ref2CNO = txtRef2CNO.Text;
                        iob.JoinDT = JoinDT;
                        iob.Gander = ddlEmpGen.Text;
                        iob.BankAcNO = txtBankAcNO.Text;
                        iob.DPTID = Convert.ToInt64(lblDeptID.Text);
                        iob.BasicSalary = Decimal.Parse(txtBasicSalary.Text);
                        iob.HRent = Decimal.Parse(txtHRent.Text);
                        iob.MAllownc = Decimal.Parse(txtMAllwnce.Text);
                        iob.EConvey = Decimal.Parse(txtEnterConvey.Text);
                        iob.Other = Decimal.Parse(txtOther.Text);
                        iob.PostID = Convert.ToInt64(lblPostID.Text);
                        iob.status = ddlSTATS.Text;
                        NullChack();
                        string s = dob.Update_HR_EMP(iob);
                        if (s == "true")
                        {
                            Clear();
                            ddlDPT.Focus();
                            lblMSG.Visible = true;
                            lblMSG.Text = "Success:  Updated !";
                        }
                        else
                        {
                            lblMSG.Visible = true;
                            lblMSG.Text = "Error:  Failed !";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }
            }
        }

        protected void ddlDPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.lblAdd("SELECT DEPTID FROM HR_DEPT WHERE DEPTNM='" + ddlDPT.Text + "'", lblDeptID);
            Global.lblAdd("SELECT DEPTSNM FROM HR_DEPT WHERE DEPTNM='" + ddlDPT.Text + "'", lblDeptSID);
            txtID.Text = EmpIDGenerate(lblDeptSID.Text, lblDeptID.Text);
            lblMSG.Visible = false;
        }
        private string EmpIDGenerate(string SID, string DeptID)
        {
            string GeneratedID="";
            string ID = Global.GetData("SELECT MAX(SUBSTRING(EMPID,LEN(EMPID)-3,LEN(EMPID))) FROM HR_EMP WHERE DEPTID='" + DeptID + "'");
            if (ID == "")
                GeneratedID = SID + "-" + "0001";
            else
            {
                ID=(int.Parse(ID)+1).ToString();
                if (int.Parse(ID) < 10)
                    GeneratedID = SID + "-" + "000" + ID;
                else if (int.Parse(ID) < 100)
                    GeneratedID = SID + "-" + "00" + ID;
                else if (int.Parse(ID) < 1000)
                    GeneratedID = SID + "-" + "0" + ID;
                else if (int.Parse(ID) < 10000)
                    GeneratedID = SID + "-" + ID;
            }
            return GeneratedID;
        }
        protected void txtEmpNM_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlEmpEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpEdit.Text != "Select")
            {
                if (conn.State != ConnectionState.Open)conn.Open();
                string Script = @"SELECT     HR_EMP.SL,HR_EMP.EMPID,HR_DEPT.DEPTNM,HR_DEPT.DEPTID,HR_DEPT.DEPTSNM, HR_EMP.EMPNM, HR_EMP.GUARDIANNM, HR_EMP.MOTHERNM, HR_EMP.ADDRESS_PRE, HR_EMP.ADDRESS_PER, HR_EMP.CONTACTNO, HR_EMP.EMAILID,HR_EMP.GENDER, CONVERT(NVARCHAR(10),HR_EMP.DOB,103) AS DOB, 
                        HR_EMP.VOTERIDNO, HR_EMP.BLOODGR, HR_EMP.REFNM1, HR_EMP.REFDESIG1, HR_EMP.REFADD1, HR_EMP.REFCNO1, HR_EMP.REFNM2, HR_EMP.REFDESIG2, HR_EMP.REFADD2, 
                        HR_EMP.REFCNO2, CONVERT(NVARCHAR(10),HR_EMP.JOININGDT,103) AS JOININGDT, HR_EMP.BANKACNO, HR_EMP.DEPTID, HR_DEPT.DEPTNM, HR_EMP.POSTID, HR_POST.POSTNM, HR_EMP.BASICSAL, HR_EMP.HRENT, HR_EMP.MALLWNC, HR_EMP.ECONVEY, HR_EMP.OTHER, HR_EMP.STATUS
                        FROM HR_EMP INNER JOIN
                        HR_DEPT ON HR_EMP.DEPTID = HR_DEPT.DEPTID INNER JOIN
                        HR_POST ON HR_EMP.POSTID = HR_POST.POSTID WHERE  HR_EMP.EMPID+'-'+HR_EMP.EMPNM='" + ddlEmpEdit.Text + "' ";
                SqlCommand cmd = new SqlCommand(Script, conn);
                SqlDataReader DR = cmd.ExecuteReader();
                //GridEdit.Visible = true;
                if (DR.Read())
                {
                    lblSL.Text = DR["SL"].ToString();
                    lblDeptID.Text = DR["DEPTID"].ToString();
                    lblDeptSID.Text = DR["DEPTSNM"].ToString();
                    Global.dropDownAdd_GridEditMode(ddlDPT, "SELECT DEPTNM FROM HR_DEPT ORDER BY DEPTNM", DR["DEPTNM"].ToString());
                    txtID.Text = DR["EMPID"].ToString();
                    // lblEmpID.Text = DR["EMPNM"].ToString();
                    txtEmpGNM.Text = DR["GUARDIANNM"].ToString();
                    txtEmpMNM.Text = DR["MOTHERNM"].ToString();
                    txtEmpPreAddress.Text = DR["ADDRESS_PRE"].ToString();
                    txtEmpPerAddress.Text = DR["ADDRESS_PER"].ToString();
                    txtEmpCNO.Text = DR["CONTACTNO"].ToString();
                    txtEMPEmail.Text = DR["EMAILID"].ToString();
                    if (DR["GENDER"].ToString() == "MALE")
                        ddlEmpGen.SelectedIndex = 1;
                    else
                        ddlEmpGen.SelectedIndex = 2;
                    txtEmpDOB.Text = DR["DOB"].ToString();
                    txtEmpVoterID.Text = DR["VOTERIDNO"].ToString();
                    txtEmpBld.Text = DR["BLOODGR"].ToString();
                    txtRef1NM.Text = DR["REFNM1"].ToString();
                    txtRef1Desig.Text = DR["REFDESIG1"].ToString();
                    txtRef1Address.Text = DR["REFADD1"].ToString();
                    txtRef1CNO.Text = DR["REFCNO1"].ToString();
                    txtRef2NM.Text = DR["REFNM2"].ToString();
                    txtRef2Desig.Text = DR["REFDESIG2"].ToString();
                    txtRef2Address.Text = DR["REFADD2"].ToString();
                    txtRef2CNO.Text = DR["REFCNO2"].ToString();
                    txtJoinDT.Text = DR["JOININGDT"].ToString();
                    txtBankAcNO.Text = DR["BANKACNO"].ToString();
                    lblDeptID.Text = DR["DEPTID"].ToString();
                    ddlDPT.Text = DR["DEPTNM"].ToString();
                    lblPostID.Text = DR["POSTID"].ToString();
                    ddlPostNM.Text = DR["POSTNM"].ToString();
                    ddlSTATS.Text = DR["STATUS"].ToString();
                    if (txtEmpDOB.Text == "1900-01-01")
                        txtEmpDOB.Text = "";
                    txtBasicSalary.Text = DR["BASICSAL"].ToString();
                    txtHRent.Text = DR["HRENT"].ToString();
                    txtMAllwnce.Text = DR["MALLWNC"].ToString();
                    txtEnterConvey.Text = DR["ECONVEY"].ToString();
                    txtOther.Text = DR["OTHER"].ToString();
                }
            }
            else
                Clear();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                Clear();
                btnEdit.Text = "New";
                btnDelete.Visible = true;
                btnUpdate.Visible = true;
                btnSubmit.Visible = false;
                txtEmpNM.Focus(); 
                ddlEmpEdit.Visible = true;
                txtEmpNM.Visible = false;
                Global.dropDownAddWithSelect(ddlEmpEdit, "SELECT EMPID+'-'+EMPNM FROM HR_EMP ORDER BY EMPID,EMPNM");
                ddlEmpEdit.Focus();
            }
            else
            {
                Clear();
                btnEdit.Text = "Edit"; 
                btnSubmit.Visible = true;
                txtEmpNM.Focus();
                btnDelete.Visible = false;
                btnUpdate.Visible = false;
                ddlEmpEdit.Visible = false;
                txtEmpNM.Visible = true;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State != ConnectionState.Open)conn.Open(); 
                SqlCommand cmd = new SqlCommand("DELETE FROM HR_EMP WHERE EMPID = '" + lblSL.Text + "'", conn);
                int i=cmd.ExecuteNonQuery(); 
                if (1 > 0)
                {
                    lblMSG.Visible = true;
                    lblMSG.Text = "Success:  Deleted !";
                    Clear();
                    if (conn.State != ConnectionState.Closed)conn.Close();
                    ddlDPT.Focus();
                }
                else
                {
                    lblMSG.Visible = true;
                    lblMSG.Text = "Error:  failed for delete !";
                }
                
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

        protected void ddlPostNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.lblAdd("SELECT POSTID FROM HR_POST WHERE POSTNM='" + ddlPostNM.Text + "'", lblPostID);
        }
    }
}