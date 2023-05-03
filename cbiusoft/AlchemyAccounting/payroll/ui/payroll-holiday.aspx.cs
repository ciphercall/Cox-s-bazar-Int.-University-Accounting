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
    public partial class payroll_holiday : System.Web.UI.Page
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
                    DateTime td = DateTime.Now;
                    string tdt = td.ToString("dd/MM/yyyy");
                    txtHolidayDate.Text = tdt;
                    txtHolidayDate.Focus();
                }
            }
        }

        protected void txtHolidayDate_TextChanged(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                lblErrMsg.Visible = false;
                ddlStatus.Focus();
            }
            else
            {
                lblErrMsg.Visible = false;
                DateTime holdt = DateTime.Parse(txtHolidayDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string hdt = holdt.ToString("yyyy-MM-dd");
                Global.lblAdd("SELECT STATUS FROM HR_HOLIDAYS WHERE HOLIDAYDT ='" + hdt + "'", lblStatus);
                ddlStatus.Text = lblStatus.Text;
                Global.txtAdd("SELECT REMARKS FROM HR_HOLIDAYS WHERE HOLIDAYDT ='" + hdt + "'", txtRemarks);
                ddlStatus.Focus();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtHolidayDate.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Select Holiday Date.";
                txtHolidayDate.Focus();
            }
            else
            {
                if (btnEdit.Text == "Edit")
                {
                    iob.HolDt = DateTime.Parse(txtHolidayDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.HolSt = ddlStatus.Text;
                    iob.Remarks = txtRemarks.Text;

                    iob.InTm = DateTime.Now;
                    iob.UserNm = HttpContext.Current.Session["UserName"].ToString();
                    iob.Ip = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.UserPc = HttpContext.Current.Session["PCName"].ToString();

                    dob.payroll_Holidays_HR_HOLIDAYS(iob);

                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Successfully Saved.";
                    ddlStatus.SelectedIndex = -1;
                    txtRemarks.Text = "";
                    txtHolidayDate.Focus();
                }
                else
                {
                    iob.HolDt = DateTime.Parse(txtHolidayDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.HolSt = ddlStatus.Text;
                    iob.Remarks = txtRemarks.Text;

                    iob.InTm = DateTime.Now;
                    iob.UserNm = HttpContext.Current.Session["UserName"].ToString();
                    iob.Ip = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.UserPc = HttpContext.Current.Session["PCName"].ToString();

                    dob.update_payroll_Holidays_HR_HOLIDAYS(iob);

                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Successfully Updated.";
                    ddlStatus.SelectedIndex = -1;
                    txtRemarks.Text = "";
                    txtHolidayDate.Focus();
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                btnEdit.Text = "New";
                btnSave.Text = "Update";
                lblErrMsg.Visible = false;
                txtHolidayDate.Focus();
            }
            else
            {
                btnEdit.Text = "Edit";
                btnSave.Text = "Save";
                lblErrMsg.Visible = false;
                txtHolidayDate.Focus();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        protected void Refresh()
        {
            DateTime td = DateTime.Now;
            string tdt = td.ToString("dd/MM/yyyy");
            txtHolidayDate.Text = tdt;
            ddlStatus.SelectedIndex = -1;
            lblErrMsg.Visible = false;
            txtRemarks.Text = "";
        }
    }
}