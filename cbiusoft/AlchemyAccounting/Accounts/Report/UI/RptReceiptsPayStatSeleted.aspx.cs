﻿using System;
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

namespace AlchemyAccounting.Accounts.Report.UI
{
    public partial class RptReceiptsPayStatSeleted : System.Web.UI.Page
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
                    txtHeadNM.Focus();
                    DateTime today = DateTime.Today.Date;
                    string td = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                    txtFrom.Text = td;
                    txtTo.Text = td;
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            
            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102') and STATUSCD='P' and ACCOUNTNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }

        protected void txtHeadNM_TextChanged(object sender, EventArgs e)
        {
            if (txtHeadNM.Text != "")
            {
                Global.lblAdd(@"Select ACCOUNTCD from GL_ACCHART where STATUSCD='P'and ACCOUNTNM = '" + txtHeadNM.Text + "'", lblAccHeadCD);
            }
            else
                txtHeadNM.Text = "";
            txtHeadNM.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text == "" || txtTo.Text == "")
            {
                Response.Write("<script>alert('Fill Required Data');</script>");
            }
            else
            {
                Session["HeadCD"] = lblAccHeadCD.Text;
                Session["HeadNM"] = txtHeadNM.Text;
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptReceiptPaymentStateSelected.aspx','_newtab');", true);
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