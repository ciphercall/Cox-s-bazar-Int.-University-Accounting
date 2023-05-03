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
    public partial class FessCollectionStudentWiseaspxPrint : System.Web.UI.Page
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
                lblFeesNM.Text = Session["FEESNM"].ToString();
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
            string FEESID = Session["FEESID"].ToString();
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@" SELECT     EIM_TRANS.STUDENTID, EIM_STUDENT.STUDENTNM, EIM_STUDENT.NEWSTUDENTID,EIM_PROGRAM.PROGRAMNM, EIM_TRANS.AMOUNT
                                                FROM   EIM_STUDENT INNER JOIN
                      EIM_TRANS ON EIM_STUDENT.STUDENTID = EIM_TRANS.STUDENTID INNER JOIN
                      EIM_PROGRAM ON EIM_TRANS.PROGRAMID = EIM_PROGRAM.PROGRAMID
                      WHERE EIM_TRANS.FEESID = '" + FEESID + "' AND EIM_TRANS.TRANSDT >= '" + FRDT + "' AND EIM_TRANS.TRANSDT <= '" + TODT + "' ORDER BY EIM_TRANS.STUDENTID", conn);

            Global.lblAdd("SELECT SUM(AMOUNT) FROM EIM_TRANS WHERE FEESID = '" + FEESID + "' AND TRANSDT >= '" + FRDT + "' AND TRANSDT <= '" + TODT + "' ", lblAmount);
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
                string STUDENTID = DataBinder.Eval(e.Row.DataItem, "STUDENTID").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + STUDENTID;

                string STUDENTNM = DataBinder.Eval(e.Row.DataItem, "STUDENTNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + STUDENTNM;

                string PROGRAMNM = DataBinder.Eval(e.Row.DataItem, "PROGRAMNM").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + PROGRAMNM;

                string AMOUNT = DataBinder.Eval(e.Row.DataItem, "AMOUNT").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + AMOUNT;


            }
        }
    }
}