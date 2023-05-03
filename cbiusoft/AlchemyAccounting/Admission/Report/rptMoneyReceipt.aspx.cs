using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.Report
{
    public partial class rptMoneyReceipt : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string MRNO = Session["MRNO"].ToString();
            string MRDT = Session["MRDT"].ToString();
            string FRNO = Session["FRNO"].ToString();
            string PROGRAMSNM = Session["PROGRAMSNM"].ToString();
            string SEMESTERNM = Session["SEMESTERNM"].ToString();
            string STUDENTNM = Session["STUDENTNM"].ToString();
            string AMOUNT = Session["AMOUNT"].ToString();
            //string SubString = MRDT.Substring(9,0);
            lblMRNO.Text = MRNO;
            lblMRDT.Text = MRDT;
            lblFRNO.Text = FRNO;
            lblProSNM.Text = PROGRAMSNM;
            lblSemNM.Text = SEMESTERNM;
            lblStuNM.Text = STUDENTNM;
            lblTamnt.Text = AMOUNT;
            lblAmnt.Text = AMOUNT;
            if (lblAmnt.Text == "")
            {
                lblAmnt.ForeColor = Color.Red;
                lblAmnt.Text = "0.00 TK";
            }

            Decimal amount = Decimal.Parse(lblAmnt.Text);
            lblAmnt.Text = SpellAmount.comma(amount);
            string AmtConv = SpellAmount.MoneyConvFn(lblAmnt.Text.ToString().Trim());
            lblInWrdAmnt.Text = AmtConv.Trim();
        }
    }
}