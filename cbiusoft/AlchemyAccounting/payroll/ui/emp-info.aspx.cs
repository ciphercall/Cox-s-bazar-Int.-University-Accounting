using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using AlchemyAccounting.payroll.model;
using AlchemyAccounting.payroll.dataAccess;

namespace AlchemyAccounting.payroll.ui
{
    public partial class emp_info : System.Web.UI.Page
    {
        payroll_model iob = new payroll_model();
        payroll_data dob = new payroll_data();

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    string user = HttpContext.Current.Session["UserName"].ToString();
                    Global.lblAdd("SELECT PerEd FROM User_Registration WHERE UserID ='" + user + "'", lblEdit);
                    if (lblEdit.Text == "Edit")
                        btnEdit.Visible = true;
                    else
                        btnEdit.Visible = false;

                    Global.lblAdd("SELECT PerDel FROM User_Registration WHERE UserID ='" + user + "'", lblDelete);

                    ChkmemID();
                    txtMemberInfo.Focus();
                }
            }
        }

        private void ChkmemID()
        {
            Global.lblAdd("SELECT MAX(EMPID) FROM HR_EMP", lblChkMemID);

            string MxCd = "";
            string MemID = "";
            int SubCD, IncrFwCD;

            if (lblChkMemID.Text == "")
            {
                txtMemberId.Text = "00001";
            }
            else
            {
                MxCd = lblChkMemID.Text;
                SubCD = int.Parse(MxCd);
                IncrFwCD = SubCD + 1;

                if (IncrFwCD < 10)
                    MemID = "0000" + IncrFwCD;
                else if (IncrFwCD < 100)
                    MemID = "000" + IncrFwCD;
                else if (IncrFwCD < 1000)
                    MemID = "00" + IncrFwCD;
                else if (IncrFwCD < 10000)
                    MemID = "0" + IncrFwCD;

                txtMemberId.Text = MemID;
            }
        }

        protected void txtIDExpDate_TextChanged(object sender, EventArgs e)
        {
            txtPPNo.Focus();
        }

        protected void txtPPExpDate_TextChanged(object sender, EventArgs e)
        {
            txtNationality.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (txtQID.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Type Qatar ID.";
                    txtQID.Focus();
                }
                else if (txtIDExpDate.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Select ID Expire Date.";
                    txtIDExpDate.Focus();
                }
                else if (txtNationality.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Type Nationality.";
                    txtNationality.Focus();
                }
                else if (txtPPNo.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Type Passport No.";
                    txtPPNo.Focus();
                }
                else if (txtPPExpDate.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Select Passport Expire Date.";
                    txtPPExpDate.Focus();
                }
                else if (txtOccup.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Type Occupation.";
                    txtOccup.Focus();
                }
                else if (txtBasicSal.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Type Basic Salary.";
                    txtBasicSal.Focus();
                }
                else if (txtFoods.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Type Foods.";
                    txtFoods.Focus();
                }
                else
                {
                    if (btnEdit.Text == "Edit")
                    {
                        if (txtMemberInfo.Text == "")
                        {
                            lblErrMsg.Visible = true;
                            lblErrMsg.Text = "Type Employee Name.";
                            txtMemberInfo.Focus();
                        }
                        else
                        {
                            lblErrMsg.Visible = false;

                            iob.EmpNM = txtMemberInfo.Text;

                            Global.lblAdd("SELECT MAX(EMPID) AS EMPID FROM HR_EMP", lblChkID);

                            string MxCd = "";
                            string MemID = "";
                            int SubCD, IncrFwCD;

                            if (lblChkID.Text == "")
                            {
                                txtMemberId.Text = "00001";
                            }
                            else
                            {
                                MxCd = lblChkID.Text;
                                SubCD = int.Parse(MxCd);
                                IncrFwCD = SubCD + 1;

                                if (IncrFwCD < 10)
                                    MemID = "0000" + IncrFwCD;
                                else if (IncrFwCD < 100)
                                    MemID = "000" + IncrFwCD;
                                else if (IncrFwCD < 1000)
                                    MemID = "00" + IncrFwCD;
                                else if (IncrFwCD < 10000)
                                    MemID = "0" + IncrFwCD;

                                txtMemberId.Text = MemID;
                            }

                            iob.EmpID = txtMemberId.Text;
                            iob.FatherNM = txtFatherNm.Text;
                            string varENdt = "";
                            if (txtEntryDate.Text == "")
                                varENdt = "1900-01-01";
                            else
                                varENdt = txtEntryDate.Text;
                            iob.EntryDT = DateTime.Parse(varENdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.QaterID = txtQID.Text;
                            string varIdExpdt = "";
                            if (txtIDExpDate.Text == "")
                                varIdExpdt = "1900-01-01";
                            else
                                varIdExpdt = txtIDExpDate.Text;
                            iob.IdExpDt = DateTime.Parse(varIdExpdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.PpNo = txtPPNo.Text;
                            string varPPExpdt = "";
                            if (txtPPExpDate.Text == "")
                                varPPExpdt = "1900-01-01";
                            else
                                varPPExpdt = txtPPExpDate.Text;
                            iob.PpExpDt = DateTime.Parse(varPPExpdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Nationality = txtNationality.Text;
                            iob.Occupation = txtOccup.Text;
                            iob.FileNo = txtFileNo.Text;
                            iob.ComNM = ddlCompNM.Text;
                            iob.Reference = txtRef.Text;
                            string varVacFrdt = "";
                            if (txtVacFr.Text == "")
                                varVacFrdt = "1900-01-01";
                            else
                                varVacFrdt = txtVacFr.Text;
                            iob.VacFr = DateTime.Parse(varVacFrdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            string varVacTodt = "";
                            if (txtVacTo.Text == "")
                                varVacTodt = "1900-01-01";
                            else
                                varVacTodt = txtVacTo.Text;
                            iob.VacTo = DateTime.Parse(varVacTodt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Status = txtStatus.Text;
                            iob.Note = txtNote.Text;
                            iob.BasicSal = Convert.ToDecimal(txtBasicSal.Text);
                            iob.Foods = Convert.ToDecimal(txtFoods.Text);
                            iob.Address = txtAddress.Text;
                            iob.ContactNo = txtContact.Text;

                            iob.InTm = DateTime.Now;
                            iob.UserNm = HttpContext.Current.Session["UserName"].ToString();
                            iob.Ip = HttpContext.Current.Session["IpAddress"].ToString();
                            iob.UserPc = HttpContext.Current.Session["PCName"].ToString();

                            dob.payroll_Employee_Information_HR_EMP(iob);

                            Refresh();

                            lblErrMsg.Visible = true;
                            lblErrMsg.Text = "Successfully Saved.";
                            txtMemberInfo.Focus();
                        }
                    }
                    else
                    {
                        if (txtMemberInfoEdit.Text == "")
                        {
                            lblErrMsg.Visible = true;
                            lblErrMsg.Text = "Type Employee Name.";
                            txtMemberInfoEdit.Focus();
                        }
                        else
                        {
                            lblErrMsg.Visible = false;

                            iob.EmpNM = txtMemberInfoEdit.Text;
                            iob.EmpID = ddlMemberId.Text;
                            iob.FatherNM = txtFatherNm.Text;
                            string varENdt = "";
                            if (txtEntryDate.Text == "")
                                varENdt = "1900-01-01";
                            else
                                varENdt = txtEntryDate.Text;
                            iob.EntryDT = DateTime.Parse(varENdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.QaterID = txtQID.Text;
                            string varIdExpdt = "";
                            if (txtIDExpDate.Text == "")
                                varIdExpdt = "1900-01-01";
                            else
                                varIdExpdt = txtIDExpDate.Text;
                            iob.IdExpDt = DateTime.Parse(varIdExpdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.PpNo = txtPPNo.Text;
                            string varPPExpdt = "";
                            if (txtPPExpDate.Text == "")
                                varPPExpdt = "1900-01-01";
                            else
                                varPPExpdt = txtPPExpDate.Text;
                            iob.PpExpDt = DateTime.Parse(varPPExpdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Nationality = txtNationality.Text;
                            iob.Occupation = txtOccup.Text;
                            iob.FileNo = txtFileNo.Text;
                            iob.ComNM = ddlCompNM.Text;
                            iob.Reference = txtRef.Text;
                            string varVacFrdt = "";
                            if (txtVacFr.Text == "")
                                varVacFrdt = "1900-01-01";
                            else
                                varVacFrdt = txtVacFr.Text;
                            iob.VacFr = DateTime.Parse(varVacFrdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            string varVacTodt = "";
                            if (txtVacTo.Text == "")
                                varVacTodt = "1900-01-01";
                            else
                                varVacTodt = txtVacTo.Text;
                            iob.VacTo = DateTime.Parse(varVacTodt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            iob.Status = txtStatus.Text;
                            iob.Note = txtNote.Text;
                            iob.BasicSal = Convert.ToDecimal(txtBasicSal.Text);
                            iob.Foods = Convert.ToDecimal(txtFoods.Text);
                            iob.Address = txtAddress.Text;
                            iob.ContactNo = txtContact.Text;

                            iob.InTm = DateTime.Now;
                            iob.UserNm = HttpContext.Current.Session["UserName"].ToString();
                            iob.Ip = HttpContext.Current.Session["IpAddress"].ToString();
                            iob.UserPc = HttpContext.Current.Session["PCName"].ToString();

                            dob.update_payroll_Employee_Information_HR_EMP(iob);

                            Refresh();

                            lblErrMsg.Visible = true;
                            lblErrMsg.Text = "Successfully Updated.";
                            ddlMemberId.Focus();
                        }
                    }
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                btnEdit.Text = "New";
                btnSave.Text = "Update";
                btnDelete.Visible = true;
                txtMemberInfoEdit.Visible = true;
                txtMemberInfo.Visible = false;
                txtMemberId.Visible = false;
                ddlMemberId.Visible = true;
                lblErrMsg.Visible = false;
                Refresh();
                if (lblDelete.Text == "")
                {
                    btnDelete.Visible = false;
                }
                else
                    btnDelete.Visible = true;
                Global.dropDownAddWithSelect(ddlMemberId, "SELECT EMPID FROM HR_EMP");
                ddlMemberId.Focus();
            }
            else
            {
                btnEdit.Text = "Edit";
                btnSave.Text = "Save";
                btnDelete.Visible = false;
                txtMemberInfoEdit.Visible = false;
                txtMemberInfo.Visible = true;
                txtMemberId.Visible = true;
                ddlMemberId.Visible = false;
                Refresh();
                ChkmemID();
                lblErrMsg.Visible = false;
                txtMemberInfo.Focus();
            }
        }

        protected void ddlMemberId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMemberId.Text == "Select")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Select Employee ID";
                ddlMemberId.Focus();
            }
            else
            {
                Global.txtAdd("SELECT EMPNM FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtMemberInfoEdit);
                Global.txtAdd("SELECT FATHERNM FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtFatherNm);
                Global.txtAdd("SELECT (CASE WHEN CONVERT(NVARCHAR,ENDATE,103)='01/01/1900' THEN NULL ELSE CONVERT(NVARCHAR,ENDATE,103) END) AS ENDATE FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtEntryDate);
                Global.txtAdd("SELECT QATARID FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtQID);
                Global.txtAdd("SELECT (CASE WHEN  CONVERT(NVARCHAR,IDEXPDT,103)='01/01/1900' THEN NULL ELSE CONVERT(NVARCHAR,IDEXPDT,103) END) AS IDEXPDT  FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtIDExpDate);
                Global.txtAdd("SELECT QATARID FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtQID);
                Global.txtAdd("SELECT NATIONALITY FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtNationality);
                Global.txtAdd("SELECT PPNO FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtPPNo);
                Global.txtAdd("SELECT (CASE WHEN CONVERT(NVARCHAR,PPEXPDT,103)='01/01/1900' THEN NULL ELSE CONVERT(NVARCHAR,PPEXPDT,103) END) AS PPEXPDT  FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtPPExpDate);
                Global.txtAdd("SELECT OCCUPATION FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtOccup);
                Global.txtAdd("SELECT FILENO FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtFileNo);
                Global.lblAdd("SELECT COMPANYNM FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", lblCompNm);
                ddlCompNM.Text = lblCompNm.Text;
                Global.txtAdd("SELECT REFERENCE FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtRef);
                Global.txtAdd("SELECT (CASE WHEN CONVERT(NVARCHAR,VACATIONFR,103)='01/01/1900' THEN NULL ELSE CONVERT(NVARCHAR,VACATIONFR,103) END) AS VACATIONFR FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtVacFr);
                Global.txtAdd("SELECT (CASE WHEN CONVERT(NVARCHAR,VACATIONTO,103)='01/01/1900' THEN NULL ELSE CONVERT(NVARCHAR,VACATIONTO,103) END) AS VACATIONTO FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtVacTo);
                Global.txtAdd("SELECT ADDRESS FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtAddress);
                Global.txtAdd("SELECT STATUS FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtStatus);
                Global.txtAdd("SELECT NOTE FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtNote);
                Global.txtAdd("SELECT CONTACTNO FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtContact);
                Global.txtAdd("SELECT BASICSAL FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtBasicSal);
                Global.txtAdd("SELECT FOODS FROM HR_EMP WHERE EMPID = '" + ddlMemberId.Text + "'", txtFoods);
                txtMemberInfoEdit.Focus();
            }
        }

        protected void Refresh()
        {
            txtMemberInfo.Text = "";
            txtMemberInfoEdit.Text = "";
            txtFatherNm.Text = "";
            txtEntryDate.Text = "";
            txtQID.Text = "";
            txtIDExpDate.Text = "";
            txtPPNo.Text = "";
            txtPPExpDate.Text = "";
            txtNationality.Text = "";
            txtOccup.Text = "";
            txtFileNo.Text = "";
            ddlCompNM.SelectedIndex = -1;
            txtRef.Text = "";
            txtVacFr.Text = "";
            txtVacTo.Text = "";
            txtBasicSal.Text = ".00";
            txtFoods.Text = ".00";
            txtAddress.Text = "";
            txtStatus.Text = "";
            txtNote.Text = "";
            txtContact.Text = "";
            lblErrMsg.Text = "";
            lblErrMsg.Visible = false;
            if (btnEdit.Text == "Edit")
            {
                ChkmemID();
                txtMemberId.Focus();
            }
            else
            {
                Global.dropDownAddWithSelect(ddlMemberId, "SELECT EMPID FROM HR_EMP");
                ddlMemberId.SelectedIndex = -1;
                txtMemberInfoEdit.Focus();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        protected void txtEntryDate_TextChanged(object sender, EventArgs e)
        {
            txtQID.Focus();
        }

        protected void txtVacFr_TextChanged(object sender, EventArgs e)
        {
            txtVacTo.Focus();
        }

        protected void txtVacTo_TextChanged(object sender, EventArgs e)
        {
            txtStatus.Focus();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (ddlMemberId.Text == "Select")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Select Member id.";
                    ddlMemberId.Focus();
                }

                else
                {
                    lblErrMsg.Visible = false;

                    iob.EmpID = ddlMemberId.Text;

                    dob.delete_payroll_Employee_Information_HR_EMP(iob);
                    Refresh();
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Employee deleted successfully.";
                    ddlMemberId.Focus();
                }
            }
        }
    }
}