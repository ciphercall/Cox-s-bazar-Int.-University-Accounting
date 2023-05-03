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

namespace AlchemyAccounting.Admission.Report
{
    public partial class FeesCollectionReportPrint : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection conn = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string FRDT = Session["FrDT"].ToString();
                lblFrDT.Text = FRDT;
                string TODT = Session["ToDT"].ToString();
                lblToDT.Text = TODT;
                gridShow();

            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }

        }
        private void gridShow()
        {
            DateTime dateFR = DateTime.Parse(lblFrDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime dateTO = DateTime.Parse(lblToDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            string FRDT = dateFR.ToString("yyyy-MM-dd");
            string TODT = dateTO.ToString("yyyy-MM-dd");
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT EIM_TRANS.FEESID, EIM_FEES.FEESNM, EIM_TRANS.AMOUNT
                                        FROM   EIM_FEES INNER JOIN
                      EIM_TRANS ON EIM_FEES.FEESID = EIM_TRANS.FEESID
                      WHERE TRANSDT >= '" + FRDT + "' AND TRANSDT <= '" + TODT + "'   order by EIM_TRANS.TRANSDT ", conn);

            Global.lblAdd("SELECT SUM(AMOUNT) FROM EIM_TRANS WHERE TRANSDT >= '" + FRDT + "' AND TRANSDT <= '" + TODT + "' ", lblAmount);
            if (lblAmount.Text == "")
                lblAmount.Text = "0.00";
            Decimal Amnt = Convert.ToDecimal(lblAmount.Text);
            string lblAmount1 = Amnt.ToString();
            string AmtConv = SpellAmount.MoneyConvFn(lblAmount1.Trim());
            lblInWord.Text = AmtConv.Trim();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Fees.DataSource = ds;
                gv_Fees.DataBind();

            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Fees.DataSource = ds;
                gv_Fees.DataBind();
                int columncount = gv_Fees.Rows[0].Cells.Count;
                gv_Fees.Rows[0].Cells.Clear();
                gv_Fees.Rows[0].Cells.Add(new TableCell());
                gv_Fees.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Fees.Rows[0].Visible = false;

            }
        }

        protected void gv_Fees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string COURSEID = DataBinder.Eval(e.Row.DataItem, "FEESID").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + COURSEID;
                string CREDITHH = DataBinder.Eval(e.Row.DataItem, "FEESNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + CREDITHH;
                string CRCOST = DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + CRCOST;


            }
        }
    }
}