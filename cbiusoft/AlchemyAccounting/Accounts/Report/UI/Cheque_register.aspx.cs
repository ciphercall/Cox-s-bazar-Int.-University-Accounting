using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Accounts.UI
{
    public partial class Cheque_register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFrom.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                txtTo.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                ddlMode.Focus();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text == "") 
                txtFrom.Focus();
            else if (txtTo.Text == "")
                txtTo.Focus();
            else
            {
                Session["TYPENM"] = "";
                Session["TYPE"] = "";
                Session["FRDT"] = "";
                Session["TODT"] = "";
                Session["TYPENM"] = ddlMode.SelectedItem;
                Session["TYPE"] = ddlMode.SelectedValue;
                Session["FRDT"] = txtFrom.Text;
                Session["TODT"] = txtTo.Text;
                Page.ClientScript.RegisterStartupScript(
                        this.GetType(), "OpenWindow", "window.open('../Report/rptCheckRegister.aspx','_newtab');", true);
            }
        }

        protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFrom.Focus();
        }

        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {
            txtTo.Focus();
        }

        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }
    }
}