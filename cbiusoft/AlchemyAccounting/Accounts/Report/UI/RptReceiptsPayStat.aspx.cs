﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Accounts.Report.UI
{
    public partial class RptReceiptsPayStat : System.Web.UI.Page
    {
       
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
                    DateTime today = DateTime.Today.Date;
                    string td = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                    txtFrom.Text = td;
                    txtTo.Text = td;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text == "" || txtTo.Text == "")
            {
                Response.Write("<script>alert('Fill Required Data');</script>");
            }
            else
            {
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptReceiptPaymentState.aspx','_newtab');", true);
                //Response.Redirect("~/Accounts/Report/Report/ReportLedgerBook.aspx");
            }
        }

        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }
    }
}