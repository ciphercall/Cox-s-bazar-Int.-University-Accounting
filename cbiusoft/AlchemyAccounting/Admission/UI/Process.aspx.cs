using System;
using System.IO;
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

namespace AlchemyAccounting.Admission.UI
{
    public partial class Process : System.Web.UI.Page
    {

        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection con = new SqlConnection(Global.connection);
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
       
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
                    Global.dropDownAdd(ddlMonth_Year, "SELECT TRANSMY FROM HR_SALDRCR");
                }
            }
        }
        private void gridShow()
        {

            if (con.State != ConnectionState.Open)con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT     HR_SALDRCR.TRANSMY, HR_SALDRCR.EMPID, HR_EMP.POSTID, HR_SALDRCR.MMDAY, HR_SALDRCR.HDAY, HR_SALDRCR.PREDAY, HR_SALDRCR.ABSDAY, HR_SALDRCR.LDAY, 
                      HR_EMP.BASICSAL, HR_SALDRCR.ALLOWANCE, (HR_EMP.BASICSAL + HR_SALDRCR.ALLOWANCE) as TOTPAID, HR_SALDRCR.ADVANCE, 
                      (HR_EMP.BASICSAL + HR_SALDRCR.ALLOWANCE - HR_SALDRCR.ADVANCE) as NETPAID
                      FROM HR_SALDRCR RIGHT OUTER JOIN
                      HR_EMP ON HR_SALDRCR.EMPID = HR_EMP.EMPID
                      WHERE HR_SALDRCR.TRANSMY='" + ddlMonth_Year.Text + "' ORDER BY HR_SALDRCR.EMPID", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State != ConnectionState.Closed)con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

            }
            else
            {


            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlMonth_Year.Text == "Select")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Select Month !";
            }
            else if (txtDate.Text == "")
            {
                lblMSG.Visible = true;
                lblMSG.Text = "Date is required !";
            }
            else
            {
                if (con.State != ConnectionState.Open)con.Open();
                string DeleteScript = "DELETE FROM HR_SALGRANT WHERE TRANSMY='" + ddlMonth_Year.Text + "'";
                SqlCommand command = new SqlCommand(DeleteScript, con);
                command.ExecuteNonQuery();
                if (con.State != ConnectionState.Closed)con.Close();
                /////////////////////******************************************/////////////////////////////////

                string PcName = Session["PCName"].ToString();
                string UserID = Session["UserName"].ToString();
                string Ipaddress = Session["IpAddress"].ToString();
                DateTime InTime = Global.Dayformat1(DateTime.Now);
                DateTime EffectDate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                int Result = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    int Count = GridView1.Rows.Count;
                    try
                    {
                        if (con.State != ConnectionState.Open)
                            if (con.State != ConnectionState.Open)con.Open();
                        string InsertScript = @"INSERT INTO HR_SALGRANT(TRANSMY,EMPID,POSTID,MMDAY,HDAY,PREDAY,ABSDAY,LDAY,BASICSAL,ALLOWANCE,TOTPAID,
                        ADVANCE,NETPAID,EFFECTDT,USERID,USERPC,IPADDRESS,INTIME) Values 
				        (@TRANSMY,@EMPID,@POSTID,@MMDAY,@HDAY,@PREDAY,@ABSDAY,@LDAY,@BASICSAL,@ALLOWANCE,@TOTPAID,
                        @ADVANCE,@NETPAID,@EFFECTDT,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                        SqlCommand cmd = new SqlCommand(InsertScript, con);
                        cmd.Parameters.AddWithValue("@TRANSMY", row.Cells[0].Text);
                        cmd.Parameters.AddWithValue("@EMPID", int.Parse(row.Cells[1].Text));
                        cmd.Parameters.AddWithValue("@POSTID", int.Parse(row.Cells[2].Text));
                        cmd.Parameters.AddWithValue("@MMDAY", int.Parse(row.Cells[3].Text));
                        cmd.Parameters.AddWithValue("@HDAY", int.Parse(row.Cells[4].Text));
                        cmd.Parameters.AddWithValue("@PREDAY", int.Parse(row.Cells[5].Text));
                        cmd.Parameters.AddWithValue("@ABSDAY", int.Parse(row.Cells[6].Text));
                        cmd.Parameters.AddWithValue("@LDAY", int.Parse(row.Cells[7].Text));
                        cmd.Parameters.AddWithValue("@BASICSAL", Decimal.Parse(row.Cells[8].Text));
                        cmd.Parameters.AddWithValue("@ALLOWANCE", Decimal.Parse(row.Cells[9].Text));
                        cmd.Parameters.AddWithValue("@TOTPAID", Decimal.Parse(row.Cells[10].Text));
                        cmd.Parameters.AddWithValue("@ADVANCE", Decimal.Parse(row.Cells[11].Text));
                        cmd.Parameters.AddWithValue("@NETPAID", Decimal.Parse(row.Cells[12].Text));
                        cmd.Parameters.AddWithValue("@EFFECTDT", EffectDate);
                        cmd.Parameters.AddWithValue("@USERID", UserID);
                        cmd.Parameters.AddWithValue("@USERPC", PcName);
                        cmd.Parameters.AddWithValue("@IPADDRESS", Ipaddress);
                        cmd.Parameters.AddWithValue("@INTIME", InTime);
                        Result = cmd.ExecuteNonQuery();
                        if (con.State != ConnectionState.Closed)
                            if (con.State != ConnectionState.Closed)con.Close();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex);
                    }
                    if (Result > 0)
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "Processing Complete !";
                    }
                }
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //SELECT     HR_SALDRCR.TRANSMY, HR_SALDRCR.EMPID, HR_EMP.POSTID, HR_SALDRCR.MMDAY, HR_SALDRCR.HDAY, HR_SALDRCR.PREDAY, 
            //HR_SALDRCR.ABSDAY, HR_SALDRCR.LDAY, 
            //          HR_EMP.BASICSAL, HR_SALDRCR.ALLOWANCE, (HR_EMP.BASICSAL + HR_SALDRCR.ALLOWANCE) as TOTPAID, 
            //HR_SALDRCR.ADVANCE, 
            //          (HR_EMP.BASICSAL + HR_SALDRCR.ALLOWANCE - HR_SALDRCR.ADVANCE) as NETPAID
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string TRANSMY = DataBinder.Eval(e.Row.DataItem, "TRANSMY").ToString();
                e.Row.Cells[0].Text = TRANSMY;
                string EMPID = DataBinder.Eval(e.Row.DataItem, "EMPID").ToString();
                e.Row.Cells[1].Text = EMPID;
                string POSTID = DataBinder.Eval(e.Row.DataItem, "POSTID").ToString();
                e.Row.Cells[2].Text = POSTID;
                string MMDAY = DataBinder.Eval(e.Row.DataItem, "MMDAY").ToString();
                e.Row.Cells[3].Text = MMDAY;
                string HDAY = DataBinder.Eval(e.Row.DataItem, "HDAY").ToString();
                e.Row.Cells[4].Text = HDAY;
                string PREDAY = DataBinder.Eval(e.Row.DataItem, "PREDAY").ToString();
                e.Row.Cells[5].Text = PREDAY;
                string ABSDAY = DataBinder.Eval(e.Row.DataItem, "ABSDAY").ToString();
                e.Row.Cells[6].Text = ABSDAY;
                string LDAY = DataBinder.Eval(e.Row.DataItem, "LDAY").ToString();
                e.Row.Cells[7].Text = LDAY;
                string BASICSAL = DataBinder.Eval(e.Row.DataItem, "BASICSAL").ToString();
                e.Row.Cells[8].Text = BASICSAL;
                string ALLOWANCE = DataBinder.Eval(e.Row.DataItem, "ALLOWANCE").ToString();
                e.Row.Cells[9].Text = ALLOWANCE;
                string TOTPAID = DataBinder.Eval(e.Row.DataItem, "TOTPAID").ToString();
                e.Row.Cells[10].Text = TOTPAID;
                string ADVANCE = DataBinder.Eval(e.Row.DataItem, "ADVANCE").ToString();
                e.Row.Cells[11].Text = ADVANCE;
                string NETPAID = DataBinder.Eval(e.Row.DataItem, "NETPAID").ToString();
                e.Row.Cells[12].Text = NETPAID;
            }
        }
        protected void ddlMonth_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMSG.Text = "";
            gridShow();
        }
    }
}