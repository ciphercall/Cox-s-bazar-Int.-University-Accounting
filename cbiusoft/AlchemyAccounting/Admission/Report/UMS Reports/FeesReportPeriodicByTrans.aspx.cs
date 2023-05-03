using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.Report.UMS_Reports
{
    public partial class FeesReportPeriodicByTrans : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFrDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                txtToDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
            }
        }

        protected void txtFrDT_TextChanged(object sender, EventArgs e)
        {
            if (txtFrDT.Text == "")
            {
                txtFrDT.Focus();
            }
            else
            {
                txtToDT.Focus();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DateTime dateFR = DateTime.Parse(txtFrDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime dateTO = DateTime.Parse(txtToDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            if (dateFR > dateTO)
            {
                lblMSG.Visible = true;
                lblMSG.Text = "From Date Must Be Small to To Date !";
                txtFrDT.Focus();
            }
            else
            {
                lblMSG.Visible = false;
                if (txtFrDT.Text == "")
                    txtFrDT.Focus();
                else if (txtToDT.Text == "")
                    txtToDT.Focus();

                else
                {

                    Session["FrDT"] = "";
                    Session["ToDT"] = "";

                    Session["FrDT"] = txtFrDT.Text;
                    Session["ToDT"] = txtToDT.Text;

                    ScriptManager.RegisterStartupScript(this,
                              this.GetType(), "OpenWindow", "window.open('/Admission/Report/FeesCollectionReportPrint.aspx','_newtab');", true);
                }
            }
        }

        protected void txtToDT_TextChanged(object sender, EventArgs e)
        {
            if (txtToDT.Text == "")
                txtToDT.Focus();
            else
                Button1.Focus();
        }
    }
}