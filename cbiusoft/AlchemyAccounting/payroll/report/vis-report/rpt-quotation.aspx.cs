using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.payroll.report.vis_report
{
    public partial class rpt_quotation : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal grandtotal = 0;
        string grandtotalcomma = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                //Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompanyNM);
                //Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

                DateTime t = DateTime.Now;
                lblPrintDate.Text = t.ToString("dd/MM/yyy hh:mm:ss:tt");

                string date = Session["date"].ToString();
                string year = Session["year"].ToString();
                string trno = Session["trno"].ToString();
                string qtno = Session["qtno"].ToString();
                string compnm = Session["compnm"].ToString();
                string compadd = Session["compadd"].ToString();
                string compcont = Session["compcont"].ToString();
                string attpnm = Session["attpnm"].ToString();
                string attpdesig = Session["attpdesig"].ToString();
                string sub = Session["sub"].ToString();
                string prepby = Session["prepby"].ToString();
                string prepdesig = Session["prepdesig"].ToString();
                string prepcont = Session["prepcont"].ToString();
                string prepcompnm = Session["prepcompnm"].ToString();

                lblCompanyNM.Text = prepcompnm;

                lblDate.Text = date;
                lblComNm.Text = compnm;
                lblCompAdd.Text = compadd;
                lblCompCont.Text = compcont;
                lblQuotNo.Text = qtno;
                lblAttNm.Text = attpnm;
                lblAttDesig.Text = attpdesig;
                lblSubj.Text = sub;
                lblPrepNm.Text = prepby;
                lblPrepDesig.Text = prepdesig;
                lblPrepCont.Text = prepcont;
                lblPrepCompany.Text = prepcompnm;
                lblPrepCompanyNm.Text = prepcompnm;

                ShowGrid_Details();
                ShowGrid_Terms();
            }
        }

        private void ShowGrid_Details()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string year = Session["year"].ToString();
            string trno = Session["trno"].ToString();

            SqlCommand cmd = new SqlCommand(" SELECT ROW_NUMBER() OVER (ORDER BY QTDESC) AS SL, QTDESC, UNIT, QTRATE, QTQTY, QTQRS FROM HR_QUOTE WHERE TRANSYY =" + year + " AND TRANSNO =" + trno + " AND QTTP ='PRICE'", conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;

                if (grandtotalcomma == "0.00")
                {
                    GridView1.FooterRow.Visible = false;
                }
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = SL;

                string QTDESC = DataBinder.Eval(e.Row.DataItem, "QTDESC").ToString();
                e.Row.Cells[1].Text = QTDESC;

                string UNIT = DataBinder.Eval(e.Row.DataItem, "UNIT").ToString();
                if (UNIT == "")
                {
                    e.Row.Cells[2].Visible = false;
                    GridView1.Columns[2].Visible = false;
                }
                else
                    e.Row.Cells[2].Text = UNIT;

                decimal QTRATE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTRATE").ToString());
                if (QTRATE == Convert.ToDecimal(0.00))
                {
                    e.Row.Cells[3].Visible = false;
                    GridView1.Columns[3].Visible = false;
                }
                else
                {
                    string qrate = QTRATE.ToString("#,##0.00");
                    e.Row.Cells[3].Text = qrate;
                }

                decimal QTQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTQTY").ToString());
                if (QTQTY == Convert.ToDecimal(0.00))
                {
                    e.Row.Cells[4].Visible = false;
                    GridView1.Columns[4].Visible = false;
                }
                else
                {
                    string qqty = QTQTY.ToString("#,##0.00");
                    e.Row.Cells[4].Text = qqty;
                }

                decimal QTQRS = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "QTQRS").ToString());
                if (QTQTY == Convert.ToDecimal(0.00))
                {
                    e.Row.Cells[5].Visible = false;
                    GridView1.Columns[5].Visible = false;
                    
                }
                else
                {
                    string qtqrs = QTQRS.ToString("#,##0.00");
                    e.Row.Cells[5].Text = qtqrs;
                }

                grandtotal += QTQRS;
                grandtotalcomma = grandtotal.ToString("#,##0.00");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (grandtotalcomma == "0.00")
                {
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                }
                else
                {
                    e.Row.Cells[4].Text = "Total : ";
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].Text = grandtotalcomma;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;
                }
            }
        }

        private void ShowGrid_Terms()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string year = Session["year"].ToString();
            string trno = Session["trno"].ToString();

            SqlCommand cmd = new SqlCommand(" SELECT QTDESC FROM HR_QUOTE WHERE TRANSYY =" + year + " AND TRANSNO =" + trno + " AND QTTP ='TERMS'", conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                bltListOne.DataSource = ds;
                bltListOne.DataBind();
                bltListOne.Visible = true;
            }
            else
            {
                bltListOne.DataSource = ds;
                bltListOne.DataBind();
                bltListOne.Visible = true;
            }
        }
    }
}