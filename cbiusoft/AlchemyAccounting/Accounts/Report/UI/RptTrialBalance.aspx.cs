﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Accounts.Report.UI
{
    public partial class RptTrialBalance : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            } 
            else
            {
                if (!Page.IsPostBack)
                {
                    DateTime today = DateTime.Today.Date;
                    string td = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                    txtDate.Text = td;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtDate.Text == "")
            {
                Response.Write("<script>alert('Select Date.');</script>");
            }
            else
            {
                Session["Date"] = txtDate.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptTrialBalance.aspx','_newtab');", true);
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

    }
}