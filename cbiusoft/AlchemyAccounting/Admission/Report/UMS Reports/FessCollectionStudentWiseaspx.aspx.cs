using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.Report.UMS_Reports
{
    public partial class FessCollectionStudentWiseaspx : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFrDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                txtToDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                Global.dropDownAdd(DropDownList1, "SELECT DISTINCT FEESNM FROM EIM_FEES");
            }
        }

        protected void txtFrDT_TextChanged(object sender, EventArgs e)
        {
            if (DropDownList1.Text == "Select")
                DropDownList1.Focus();
            else if (txtFrDT.Text == "")
                txtFrDT.Focus();
            else
                txtToDT.Focus();

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
                if (DropDownList1.Text == "Select")
                    DropDownList1.Focus();
                else if (txtFrDT.Text == "")
                    txtFrDT.Focus();
                else if (txtToDT.Text == "")
                    txtToDT.Focus();

                else
                {
                    Session["FrDT"] = "";
                    Session["ToDT"] = "";
                    Session["FEESNM"] = "";

                    Session["FrDT"] = txtFrDT.Text;
                    Session["ToDT"] = txtToDT.Text;
                    Session["FEESNM"] = DropDownList1.Text;
                    ScriptManager.RegisterStartupScript(this,
                              this.GetType(), "OpenWindow", "window.open('/Admission/Report/FessCollectionStudentWiseaspxPrint.aspx','_newtab');", true);
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label lblFeesID = new Label();
            if (DropDownList1.Text == "Select")
                DropDownList1.Focus();
            else
            {
                Global.lblAdd("SELECT FEESID FROM EIM_FEES WHERE FEESNM ='" + DropDownList1.Text + "'", lblFeesID);
                Session["FEESID"] = lblFeesID.Text;
                txtFrDT.Focus();
            }
        }

        protected void txtToDT_TextChanged(object sender, EventArgs e)
        {
            if (DropDownList1.Text == "Select")
                DropDownList1.Focus();
            else if (txtFrDT.Text == "")
                txtFrDT.Focus();
            else if (txtToDT.Text == "")
                txtToDT.Focus();
            else
                Button1.Focus();
        }
    }
}