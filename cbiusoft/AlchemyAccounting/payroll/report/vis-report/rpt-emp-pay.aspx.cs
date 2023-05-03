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
    public partial class rpt_emp_pay : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal normalhour = 0;
        decimal normalot = 0;

        decimal norhr = 0;
        decimal norot = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompanyNM);
                    Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

                    DateTime t = DateTime.Now;
                    lblPrintDate.Text = t.ToString("dd/MM/yyy hh:mm:ss:tt");
                    string year = t.ToString("yyyy");

                    string month = Session["month"].ToString();
                    string my = Session["my"].ToString();
                    string empNm = Session["empNm"].ToString();
                    string empID = Session["empID"].ToString();
                    string qID = Session["qID"].ToString();

                    lblMonth.Text = month;
                    lblYear.Text = year;
                    lblName.Text = empNm;
                    lblQatarID.Text = qID;

                    Global.lblAdd("SELECT OCCUPATION FROM HR_EMP WHERE EMPID ='" + empID + "'", lblOccupation);
                    Global.lblAdd("SELECT PPNO FROM HR_EMP WHERE EMPID ='" + empID + "'", lblPassport);
                    Global.lblAdd("SELECT DISTINCT GL_COSTP.COSTPNM FROM HR_HOUR INNER JOIN HR_EMP ON HR_HOUR.EMPID = HR_EMP.EMPID INNER JOIN " +
                        " GL_COSTP ON HR_HOUR.COSTPID = GL_COSTP.COSTPID WHERE (HR_EMP.EMPID = '" + empID + "') AND (HR_HOUR.TRANSMY = '" + my + "')", lblSite);

                    Global.lblAdd("SELECT BASICSAL FROM HR_EMP WHERE EMPID ='" + empID + "'", lblBasic);
                    if (lblBasic.Text == "")
                        lblBasic.Text = "0";
                    Global.lblAdd("SELECT FOODS FROM HR_EMP WHERE EMPID ='" + empID + "'", lblFood);
                    if (lblFood.Text == "")
                        lblFood.Text = "0";
                    Global.lblAdd("SELECT SUM(FRIDAYOT)+SUM(HOLIDAYOT) AS OT FROM HR_HOUR WHERE EMPID ='" + empID + "' AND TRANSMY ='" + my + "'", lblFridayOT);
                    if (lblFridayOT.Text == "")
                        lblFridayOT.Text = "0";

                    ShowGrid_To_15();
                    ShowGrid_To_16();

                    if (lblNormalHR16.Text == "")
                        lblNormalHR16.Text = "0";

                    if (lblNormalHR15.Text == "")
                        lblNormalHR15.Text = "0";

                    lblNormalHR.Text = (Convert.ToInt64(lblNormalHR15.Text) + Convert.ToInt64(lblNormalHR16.Text)).ToString();
                    if (lblNormalHR.Text == "")
                        lblNormalHR.Text = "0";

                    if (lblNormalOT15.Text == "")
                        lblNormalOT15.Text = "0";

                    if (lblNormalOT16.Text == "")
                        lblNormalOT16.Text = "0";

                    lblNormalOT.Text = (Convert.ToInt64(lblNormalOT15.Text) + Convert.ToInt64(lblNormalOT16.Text)).ToString();
                    if (lblNormalOT.Text == "")
                        lblNormalOT.Text = "0";

                    Global.lblAdd("SELECT TOTHOUR FROM HR_SALGRANT WHERE EMPID ='" + empID + "' AND TRANSMY ='" + my + "'", lblTotHR);
                    if (lblTotHR.Text == "")
                        lblTotHR.Text = "0";
                    Global.lblAdd("SELECT TOTAMT FROM HR_SALGRANT WHERE EMPID ='" + empID + "' AND TRANSMY ='" + my + "'", lblTotAmt);
                    if (lblBonus.Text == "")
                        lblBonus.Text = "0";
                    Global.lblAdd("SELECT BONUS FROM HR_SALGRANT WHERE EMPID ='" + empID + "' AND TRANSMY ='" + my + "'", lblBonus);
                    if (lblBonus.Text == "")
                        lblBonus.Text = "0";
                    Global.lblAdd("SELECT OTCADD FROM HR_SALGRANT WHERE EMPID ='" + empID + "' AND TRANSMY ='" + my + "'", lblOtcAdd);
                    if (lblOtcAdd.Text == "")
                        lblOtcAdd.Text = "0";
                    Global.lblAdd("SELECT GROSSAMT FROM HR_SALGRANT WHERE EMPID ='" + empID + "' AND TRANSMY ='" + my + "'", lblGrossAmt);
                    if (lblGrossAmt.Text == "")
                        lblGrossAmt.Text = "0";
                    Global.lblAdd("SELECT ADVANCE FROM HR_SALGRANT WHERE EMPID ='" + empID + "' AND TRANSMY ='" + my + "'", lblAdv);
                    if (lblAdv.Text == "")
                        lblAdv.Text = "0";
                    Global.lblAdd("SELECT PENALTY FROM HR_SALGRANT WHERE EMPID ='" + empID + "' AND TRANSMY ='" + my + "'", lblPenalty);
                    if (lblPenalty.Text == "")
                        lblPenalty.Text = "0";
                    Global.lblAdd("SELECT OTCDED FROM HR_SALGRANT WHERE EMPID ='" + empID + "' AND TRANSMY ='" + my + "'", lblOtherDed);
                    if (lblOtherDed.Text == "")
                        lblOtherDed.Text = "0";
                    Global.lblAdd("SELECT NETAMT FROM HR_SALGRANT WHERE EMPID ='" + empID + "' AND TRANSMY ='" + my + "'", lblGSal);
                    if (lblGSal.Text == "")
                        lblGSal.Text = "0";
                }
            }
        }

        private void ShowGrid_To_15()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string my = Session["my"].ToString();
            string empNm = Session["empNm"].ToString();
            string empID = Session["empID"].ToString();

            SqlCommand cmd = new SqlCommand(" SELECT (CASE WHEN A.TRANSDT='9999-12-31' THEN NULL ELSE A.TRANSDT END)AS TRANSDT, A.NORMALHR, A.NORMALOT FROM (SELECT CONVERT(NVARCHAR(20), TRANSDT, 103) AS TRANSDT,NORMALHR,NORMALOT FROM HR_HOUR WHERE TRANSMY ='" + my + "' AND EMPID='" + empID + "' AND DAY(TRANSDT)<=  15  UNION " +
                    " SELECT '9999-12-31' AS TRANSDT, 0 AS NORMALHR, 0 NORMALOT)AS A ORDER BY A.TRANSDT", conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSal15.DataSource = ds;
                gvSal15.DataBind();
                gvSal15.Visible = true;
            }
            else
            {
                gvSal15.DataSource = ds;
                gvSal15.DataBind();
                gvSal15.Visible = true;
            }
        }

        protected void gvSal15_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    string TRANSDT = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                    e.Row.Cells[0].Text = TRANSDT;

                    decimal NORMALHR = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NORMALHR").ToString());
                    string nor = NORMALHR.ToString("#,##0.00");
                    e.Row.Cells[1].Text = nor;

                    decimal NORMALOT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NORMALOT").ToString());
                    string not = NORMALOT.ToString("#,##0.00");
                    e.Row.Cells[2].Text = not;

                    normalhour += NORMALHR;
                    lblNormalHR15.Text = normalhour.ToString();
                    if (lblNormalHR15.Text == "")
                        lblNormalHR15.Text = "0";
                    normalot += NORMALOT;
                    lblNormalOT15.Text = normalot.ToString();
                    if (lblNormalOT15.Text == "")
                        lblNormalOT15.Text = "0";
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total : ";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = normalhour.ToString();
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Text = normalot.ToString();
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Font.Bold = true;
            }
        }

        private void ShowGrid_To_16()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string my = Session["my"].ToString();
            string empNm = Session["empNm"].ToString();
            string empID = Session["empID"].ToString();

            SqlCommand cmd = new SqlCommand(" SELECT CONVERT(NVARCHAR(20), TRANSDT, 103) AS TRANSDT,NORMALHR,NORMALOT FROM HR_HOUR WHERE TRANSMY ='" + my + "' AND EMPID='" + empID + "' AND DAY(TRANSDT) >  15 ORDER BY TRANSDT ", conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSal16.DataSource = ds;
                gvSal16.DataBind();
                gvSal16.Visible = true;
            }
            else
            {
                gvSal16.DataSource = ds;
                gvSal16.DataBind();
                gvSal16.Visible = true;
            }
        }

        protected void gvSal16_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    string TRANSDT = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                    e.Row.Cells[0].Text = TRANSDT;

                    decimal NORMALHR = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NORMALHR").ToString());
                    string nor = NORMALHR.ToString("#,##0.00");
                    e.Row.Cells[1].Text = nor;

                    decimal NORMALOT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NORMALOT").ToString());
                    string not = NORMALOT.ToString("#,##0.00");
                    e.Row.Cells[2].Text = not;

                    norhr += NORMALHR;
                    lblNormalHR16.Text = norhr.ToString();
                    if (lblNormalHR16.Text == "")
                        lblNormalHR16.Text = "0";
                    norot += NORMALOT;
                    lblNormalOT16.Text = norot.ToString();
                    if (lblNormalOT16.Text == "")
                        lblNormalOT16.Text = "0";
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total : ";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = norhr.ToString();
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Text = norot.ToString();
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Font.Bold = true;
            }
        }
    }
}