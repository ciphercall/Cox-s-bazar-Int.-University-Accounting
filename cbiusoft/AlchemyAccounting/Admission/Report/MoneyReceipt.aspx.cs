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
using AlchemyAccounting.Admission.DataAccess;
using AlchemyAccounting.Admission.DataModel;
using System.Drawing;

namespace AlchemyAccounting.Admission.Report
{
    public partial class MoneyReceipt : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Global.connection);
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string DATE = Session["TRANSDT"].ToString();
            string TRANSNO = Session["TRANSNO"].ToString();
            string TRANSYY = Session["TRANSYY"].ToString();
            //string REGYR =Session["REGYR"].ToString();
            string SEMNM = Session["SEMESTERNM"].ToString();
            string PRONM = Session["PROGRAMNM"].ToString();
            string STUID = Session["STUDENTID"].ToString();
            string STUNM = Session["STUDENTNM"].ToString();
            string PODONO = Session["PODONO"].ToString();
            string PODT = Session["PODATE"].ToString();
            string POBNK = Session["POBANK"].ToString();
            string POBRNC = Session["POBRANCH"].ToString();
            string ACNM = Session["ACNM"].ToString();
            lblTransDate.Text = DATE;
            lblTransNO.Text = TRANSNO;
            lblSemNM.Text = SEMNM;
            lblProNM.Text = PRONM;
            lblStuID.Text = STUID;
            lblRcvFR.Text = STUNM;
            lblPODDNO.Text = PODONO;
            if (PODT == "01/01/1999")
                PODT = "";
            lblPODT.Text = PODT;
            lblPOBNK.Text = POBNK;
            lblPOBRNC.Text = POBRNC;
            lblAcNM.Text = ACNM;
            GridShow();
            Global.lblAdd("SELECT SUM(AMOUNT) FROM EIM_TRANS WHERE FEESID != '' AND TRANSYY='" + TRANSYY + "' AND TRANSNO='" + TRANSNO + "'", lblAmount);
            Global.lblAdd("SELECT SUM(VATAMOUNT) VAT FROM EIM_TRANS WHERE FEESID != '' AND TRANSYY='" + TRANSYY + "' AND TRANSNO='" + TRANSNO + "'", lblVat);
            if (lblAmount.Text == "")
            {
                lblAmount.Text = "0,00";
                lblAmount.ForeColor = Color.Red;
                lblVatAmount.Text = "0,00";
                lblVatAmount.ForeColor = Color.Red;
                lblVat.Text = "0,00";
                lblVat.ForeColor = Color.Red;
            }
            else
            {

                lblAmount.ForeColor = Color.Black;
                lblVatAmount.ForeColor = Color.Black;
                lblVat.ForeColor = Color.Black;
                Decimal amount = Decimal.Parse(lblAmount.Text);
                Decimal Vatamount = Decimal.Parse(lblVat.Text);
                Decimal Total = amount + Vatamount;
                lblAmount.Text = SpellAmount.comma(amount);
                lblVat.Text = SpellAmount.comma(Vatamount);
                lblVatAmount.Text = SpellAmount.comma(Total);
                string AmtConv = SpellAmount.MoneyConvFn(lblVatAmount.Text.ToString().Trim());
                lblTKinWRD.Text = AmtConv.Trim();

            }
        }
        private void GridShow()
        {

            string TRANSYY = Session["TRANSYY"].ToString();
            string TRANSNO = Session["TRANSNO"].ToString();
            //   string REGYR = Session["REGYR"].ToString();
            string SEMNM = Session["SEMESTERNM"].ToString();
            string PRONM = Session["PROGRAMNM"].ToString();
            string STUID = Session["STUDENTID"].ToString();
            string STUNM = Session["STUDENTNM"].ToString();
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT ROW_NUMBER( ) over(order by EIM_FEES.FEESID) AS SL, EIM_FEES.FEESNM, EIM_TRANS.AMOUNT,  EIM_TRANS.REMARKS
                                              FROM         EIM_FEES INNER JOIN
                                              EIM_TRANS ON EIM_FEES.FEESID = EIM_TRANS.FEESID WHERE TRANSNO='" + TRANSNO + "' AND TRANSYY='" + TRANSYY + "'", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_MR.DataSource = ds;
                gv_MR.DataBind();

            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_MR.DataSource = ds;
                gv_MR.DataBind();
                int columncount = gv_MR.Rows[0].Cells.Count;
                gv_MR.Rows[0].Cells.Clear();
                gv_MR.Rows[0].Cells.Add(new TableCell());
                gv_MR.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_MR.Rows[0].Visible = false;

            }

        }
    }
}