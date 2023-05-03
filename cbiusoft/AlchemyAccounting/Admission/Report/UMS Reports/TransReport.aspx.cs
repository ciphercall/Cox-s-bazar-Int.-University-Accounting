using System;
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

namespace AlchemyAccounting.Admission.Report.UMS_Reports
{
    public partial class TransReport : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection conn = new SqlConnection(Global.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimeZoneInfo timeZoneInfo;
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
                DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
                string td = PrintDate.ToString("dd-MM-yyyy");

                txtFromDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                txtToDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
            }
        }
        
        protected void txtFromDT_TextChanged(object sender, EventArgs e)
        {
            if (txtFromDT.Text != "")
                txtToDT.Focus();
            else
                txtFromDT.Focus();
        }

        protected void txtToDT_TextChanged(object sender, EventArgs e)
        {
            if (txtFromDT.Text == "")
                txtFromDT.Focus();
            else if (txtToDT.Text == "")
                txtToDT.Focus();
            else
                btnPrint.Focus();
           
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            DateTime FRDT = DateTime.Parse(txtFromDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime TODT = DateTime.Parse(txtToDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);  
            
            if (FRDT > TODT)
            {
                lblMSG.Visible = true;
                lblMSG.Text = "From Date Must Be Less From To Date";
            }
            else
            {
                lblMSG.Visible = false;
                Session["FORMDT"] = "";
                Session["TODT"] = "";
                Session["CNBCD"] = "102010100001";
                Session["FORMDT"] = txtFromDT.Text;
                Session["TODT"] = txtToDT.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                               "OpenWindow", "window.open('/Admission/Report/TransReportPrint.aspx','_newtab');", true);
            }
        }

        protected void btnPrintBNK_Click(object sender, EventArgs e)
        {
            DateTime FRDT = DateTime.Parse(txtFromDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime TODT = DateTime.Parse(txtToDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            if (FRDT > TODT)
            {
                lblMSGBNK.Visible = true;
                lblMSGBNK.Text = "From Date Must Be Less From To Date";
            }
            else
            {
                lblMSGBNK.Visible = false;
                Session["FORMDT"] = "";
                Session["TODT"] = "";

                Session["CNBCD"] = "";
                Session["FORMDT"] = txtFromDTBNK.Text;
                Session["TODT"] = txtToDTBNK.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                               "OpenWindow", "window.open('/Admission/Report/TransReportPrint.aspx','_newtab');", true);
            }
        }

        protected void txtFromDTBNK_TextChanged(object sender, EventArgs e)
        {
            if (txtFromDTBNK.Text != "")
                txtToDTBNK.Focus();
            else
                txtFromDTBNK.Focus();
        }

        protected void txtToDTBNK_TextChanged(object sender, EventArgs e)
        {
            if (txtFromDTBNK.Text == "")
                txtFromDTBNK.Focus();
            else if (txtToDTBNK.Text == "")
                txtToDTBNK.Focus();
            else
                btnPrintBNK.Focus();
        }
    }
}