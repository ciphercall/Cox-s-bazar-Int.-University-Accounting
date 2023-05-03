using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using AlchemyAccounting.payroll.model;
using AlchemyAccounting.payroll.dataAccess;

namespace AlchemyAccounting.payroll.ui
{
    public partial class payroll_hour : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmdd;

        payroll_model iob = new payroll_model();
        payroll_data dob = new payroll_data();

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    DateTime today = DateTime.Now;
                    string td = Global.Dayformat(today);
                    string todt = today.ToString("yyyy-MM-dd");
                    txtDt.Text = td;

                    string mon = DateTime.Now.ToString("MMM").ToUpper();
                    string year = today.ToString("yy");
                    lblMy.Text = mon + "-" + year;
                    lblDayTp.Text = "";
                    Global.lblAdd("SELECT STATUS FROM HR_HOLIDAYS WHERE HOLIDAYDT ='" + todt + "'", lblDayTp);
                    if (lblDayTp.Text == "")
                        lblDayTp.Text = "NORMAL";
                    GridShow();
                    txtSiteNM.Focus();
                }
            }
        }

        protected void txtDt_TextChanged(object sender, EventArgs e)
        {
            if (txtDt.Text == "")
            {
                lblError.Visible = false;
                lblError.Text = "Date cant' be empty.";
                txtDt.Focus();
            }
            else
            {
                lblMy.Text = "";
                DateTime txdt = DateTime.Parse(txtDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string todt = txdt.ToString("yyyy-MM-dd");
                string mon = txdt.ToString("MMM").ToUpper();
                string year = txdt.ToString("yy");
                lblMy.Text = mon + "-" + year;
                string dt = txdt.ToString("yyyy-MMM-dd");
                lblDayTp.Text = "";
                Global.lblAdd("SELECT STATUS FROM HR_HOLIDAYS WHERE HOLIDAYDT ='" + todt + "'", lblDayTp);
                if (lblDayTp.Text == "")
                    lblDayTp.Text = "NORMAL";
                GridShow();
                txtSiteNM.Focus();
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListSite(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT COSTPNM FROM GL_COSTP WHERE COSTPNM LIKE '" + prefixText + "%' ", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["COSTPNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtSiteNM_TextChanged(object sender, EventArgs e)
        {
            if (txtSiteNM.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select site name.";
                txtSiteNM.Focus();
            }
            else
            {
                txtSiteID.Text = "";
                Global.txtAdd("SELECT COSTPID FROM GL_COSTP WHERE COSTPNM ='" + txtSiteNM.Text + "'", txtSiteID);

                if (txtSiteID.Text == "")
                {
                    txtSiteNM.Text = "";
                    lblError.Visible = true;
                    lblError.Text = "Select site name.";
                    txtSiteNM.Focus();
                }
                else
                {
                    lblError.Visible = false;
                    GridShow();
                    TextBox txtEmpNM = (TextBox)gvEmphour.FooterRow.FindControl("txtEmpNM");
                    txtEmpNM.Focus();
                }
            }
        }

        private void GridShow()
        {
            conn = new SqlConnection(Global.connection);

            DateTime trDate = DateTime.Parse(txtDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string tDt = trDate.ToString("yyyy-MM-dd");

            conn.Open();
            cmdd = new SqlCommand("SELECT HR_EMP.EMPNM, HR_HOUR.EMPID, HR_HOUR.TRADE, (CASE WHEN HR_HOUR.NORMALHR=0 THEN NULL ELSE HR_HOUR.NORMALHR END) AS NORMALHR, " +
                      " (CASE WHEN HR_HOUR.NORMALOT=0 THEN NULL ELSE HR_HOUR.NORMALOT END) AS NORMALOT, (CASE WHEN HR_HOUR.FRIDAYOT=0 THEN NULL ELSE HR_HOUR.FRIDAYOT END) AS FRIDAYOT, " +
                      " (CASE WHEN HR_HOUR.HOLIDAYOT=0 THEN NULL ELSE HR_HOUR.HOLIDAYOT END) AS HOLIDAYOT, HR_HOUR.SL " +
                      " FROM HR_HOUR INNER JOIN HR_EMP ON HR_HOUR.EMPID = HR_EMP.EMPID " +
                      " WHERE HR_HOUR.TRANSDT =@TRANSDT AND HR_HOUR.TRANSMY =@TRANSMY AND HR_HOUR.COSTPID =@COSTPID", conn);
            cmdd.Parameters.Clear();
            cmdd.Parameters.AddWithValue("@TRANSDT", tDt);
            cmdd.Parameters.AddWithValue("@TRANSMY", lblMy.Text);
            cmdd.Parameters.AddWithValue("@COSTPID", txtSiteID.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvEmphour.DataSource = ds;
                gvEmphour.DataBind();
                gvEmphour.Visible = true;

                TextBox txtEmpNM = (TextBox)gvEmphour.FooterRow.FindControl("txtEmpNM");
                txtEmpNM.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvEmphour.DataSource = ds;
                gvEmphour.DataBind();
                int columncount = gvEmphour.Rows[0].Cells.Count;
                gvEmphour.Rows[0].Cells.Clear();
                gvEmphour.Rows[0].Cells.Add(new TableCell());
                gvEmphour.Rows[0].Cells[0].ColumnSpan = columncount;
                gvEmphour.Rows[0].Cells[0].Text = "No Records Found";
                gvEmphour.Rows[0].Visible = false;
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListEmployeeInfo(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT (EMPNM  +  '|' +  EMPID) AS EMP FROM HR_EMP WHERE (EMPNM  +  '|' +  EMPID) LIKE '" + prefixText + "%' ORDER BY EMPID", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["EMP"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtEmpNM_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtEmpNM = (TextBox)row.FindControl("txtEmpNM");
            TextBox txtEmpID = (TextBox)row.FindControl("txtEmpID");
            TextBox txtNHr = (TextBox)row.FindControl("txtNHr");
            TextBox txtFOT = (TextBox)row.FindControl("txtNHr");
            TextBox txtHOT = (TextBox)row.FindControl("txtNHr");
            TextBox txtTrade = (TextBox)row.FindControl("txtTrade");

            if (txtEmpNM.Text == "")
            {
                txtEmpNM.Text = "";
                txtEmpID.Text = "";
                lblError.Visible = true;
                lblError.Text = "Select Employee Name.";
                txtEmpNM.Focus();
            }
            else
            {
                lblError.Visible = false;

                string empNM = "";
                string empID = "";

                string searchPar = txtEmpNM.Text;
                int splitter = searchPar.IndexOf("|");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    empNM = lineSplit[0];
                    empID = lineSplit[1];

                    txtEmpNM.Text = empNM.Trim();
                    txtEmpID.Text = empID.Trim();
                    txtEmpID.ReadOnly = true;
                    //if (lblDayTp.Text == "NORMAL")
                    //    txtNHr.Focus();
                    //else if (lblDayTp.Text == "FRIDAY")
                    //    txtFOT.Focus();
                    //else if (lblDayTp.Text == "HOLIDAY")
                    //    txtHOT.Focus();
                    txtTrade.Focus();
                }
                else
                {
                    txtEmpNM.Text = "";
                    txtEmpID.Text = "";
                    lblError.Visible = true;
                    lblError.Text = "Select Employee Name.";
                    txtEmpNM.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListTrade(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT TRADE FROM HR_HOUR WHERE TRADE LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["TRADE"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtTrade_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtTrade = (TextBox)row.FindControl("txtTrade");

            txtTrade.Focus();
        }

        protected void gvEmphour_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (lblDayTp.Text == "NORMAL")
                {
                    e.Row.Cells[5].Enabled = false;
                    e.Row.Cells[6].Enabled = false;
                }
                else if (lblDayTp.Text == "FRIDAY")
                {
                    e.Row.Cells[3].Enabled = false;
                    e.Row.Cells[4].Enabled = false;
                    e.Row.Cells[6].Enabled = false;
                }
                else if (lblDayTp.Text == "HOLIDAY")
                {
                    e.Row.Cells[3].Enabled = false;
                    e.Row.Cells[4].Enabled = false;
                    e.Row.Cells[5].Enabled = false;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (lblDayTp.Text == "NORMAL")
                {
                    e.Row.Cells[5].Enabled = false;
                    e.Row.Cells[6].Enabled = false;
                }
                else if (lblDayTp.Text == "FRIDAY")
                {
                    e.Row.Cells[3].Enabled = false;
                    e.Row.Cells[4].Enabled = false;
                    e.Row.Cells[6].Enabled = false;
                }
                else if (lblDayTp.Text == "HOLIDAY")
                {
                    e.Row.Cells[3].Enabled = false;
                    e.Row.Cells[4].Enabled = false;
                    e.Row.Cells[5].Enabled = false;
                }
            }
        }

        protected void gvEmphour_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TextBox txtEmpNM = (TextBox)gvEmphour.FooterRow.FindControl("txtEmpNM");
            TextBox txtEmpID = (TextBox)gvEmphour.FooterRow.FindControl("txtEmpID");
            TextBox txtTrade = (TextBox)gvEmphour.FooterRow.FindControl("txtTrade");
            TextBox txtNHr = (TextBox)gvEmphour.FooterRow.FindControl("txtNHr");
            TextBox txtNOT = (TextBox)gvEmphour.FooterRow.FindControl("txtNOT");
            TextBox txtFOT = (TextBox)gvEmphour.FooterRow.FindControl("txtFOT");
            TextBox txtHOT = (TextBox)gvEmphour.FooterRow.FindControl("txtHOT");

            if (e.CommandName.Equals("SaveCon"))
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Login/UI/Login.aspx");
                }
                else
                {
                    if (txtDt.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select a date";
                        txtDt.Focus();
                    }
                    else if (txtSiteNM.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select site";
                        txtSiteNM.Focus();
                    }
                    else if (txtSiteID.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select site";
                        txtSiteNM.Focus();
                    }
                    else if (txtEmpNM.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select an employee";
                        txtEmpNM.Focus();
                    }
                    else if (txtEmpID.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select an employee";
                        txtEmpNM.Focus();
                    }
                    else if (txtTrade.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select trade";
                        txtTrade.Focus();
                    }
                    else
                    {
                        lblError.Visible = false;
                        iob.TransDT = DateTime.Parse(txtDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.TransMY = lblMy.Text;
                        iob.SiteID = txtSiteID.Text;
                        iob.EmpID = txtEmpID.Text;
                        iob.Trade = txtTrade.Text;
                        if (lblDayTp.Text == "NORMAL")
                        {
                            if (txtNHr.Text == "")
                            {
                                lblError.Visible = true;
                                lblError.Text = "Type normal hour.";
                                txtNHr.Focus();
                            }
                            else
                            {
                                lblError.Visible = false;
                                iob.NorHR = Convert.ToDecimal(txtNHr.Text);
                                if (txtNOT.Text == "")
                                    iob.NorOT = 0;
                                else
                                    iob.NorOT = Convert.ToDecimal(txtNOT.Text);
                            }
                        }
                        else if (lblDayTp.Text == "FRIDAY")
                        {
                            if (txtFOT.Text == "")
                            {
                                lblError.Visible = true;
                                lblError.Text = "Type friday ot.";
                                txtFOT.Focus();
                            }
                            else
                            {
                                lblError.Visible = false;
                                iob.FOT = Convert.ToDecimal(txtFOT.Text);
                            }
                        }
                        else if (lblDayTp.Text == "HOLIDAY")
                        {
                            if (txtHOT.Text == "")
                            {
                                lblError.Visible = true;
                                lblError.Text = "Type holiday ot.";
                                txtHOT.Focus();
                            }
                            else
                            {
                                lblError.Visible = false;
                                iob.HOT = Convert.ToDecimal(txtHOT.Text);
                            }
                        }

                        iob.InTm = DateTime.Now;
                        iob.UserNm = HttpContext.Current.Session["UserName"].ToString();
                        iob.Ip = HttpContext.Current.Session["IpAddress"].ToString();
                        iob.UserPc = HttpContext.Current.Session["PCName"].ToString();

                        dob.payroll_Employee_Work_Hour_HR_HOUR(iob);
                        GridShow();
                        txtEmpNM.Focus();
                    }
                }
            }
        }

        protected void txtEmpNMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtEmpNMEdit = (TextBox)row.FindControl("txtEmpNMEdit");
            Label lblEmpIDEdit = (Label)row.FindControl("lblEmpIDEdit");
            TextBox txtNHrEdit = (TextBox)row.FindControl("txtNHrEdit");
            TextBox txtFOTEdit = (TextBox)row.FindControl("txtFOTEdit");
            TextBox txtHOTEdit = (TextBox)row.FindControl("txtHOTEdit");

            if (txtEmpNMEdit.Text == "")
            {
                txtEmpNMEdit.Text = "";
                lblEmpIDEdit.Text = "";
                lblError.Visible = true;
                lblError.Text = "Select Employee Name.";
                txtEmpNMEdit.Focus();
            }
            else
            {
                lblError.Visible = false;

                string empNM = "";
                string empID = "";

                string searchPar = txtEmpNMEdit.Text;
                int splitter = searchPar.IndexOf("|");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    empNM = lineSplit[0];
                    empID = lineSplit[1];

                    txtEmpNMEdit.Text = empNM.Trim();
                    lblEmpIDEdit.Text = empID.Trim();
                    if (lblDayTp.Text == "NORMAL")
                        txtNHrEdit.Focus();
                    else if (lblDayTp.Text == "FRIDAY")
                        txtFOTEdit.Focus();
                    else if (lblDayTp.Text == "HOLIDAY")
                        txtHOTEdit.Focus();
                }
                else
                {
                    txtEmpNMEdit.Text = "";
                    lblEmpIDEdit.Text = "";
                    lblError.Visible = true;
                    lblError.Text = "Select Employee Name.";
                    txtEmpNMEdit.Focus();
                }
            }
        }

        protected void txtTradeEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtTradeEdit = (TextBox)row.FindControl("txtTradeEdit");

            txtTradeEdit.Focus();
        }

        protected void gvEmphour_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmphour.EditIndex = e.NewEditIndex;
            GridShow();

            TextBox txtEmpNMEdit = (TextBox)gvEmphour.Rows[e.NewEditIndex].FindControl("txtEmpNMEdit");
            txtEmpNMEdit.Focus();
        }

        protected void gvEmphour_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmphour.EditIndex = -1;
            GridShow();
        }

        protected void gvEmphour_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                TextBox txtEmpNMEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtEmpNMEdit");
                Label lblEmpIDEdit = (Label)gvEmphour.Rows[e.RowIndex].FindControl("lblEmpIDEdit");
                TextBox txtTradeEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtTradeEdit");
                TextBox txtNHrEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtNHrEdit");
                TextBox txtNOTEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtNOTEdit");
                TextBox txtFOTEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtFOTEdit");
                TextBox txtHOTEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtHOTEdit");
                Label lblSLEdit = (Label)gvEmphour.Rows[e.RowIndex].FindControl("lblSLEdit");

                if (txtDt.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select a date";
                    txtDt.Focus();
                }
                else if (txtSiteNM.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select site";
                    txtSiteNM.Focus();
                }
                else if (txtSiteID.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select site";
                    txtSiteNM.Focus();
                }
                else if (txtEmpNMEdit.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select an employee";
                    txtEmpNMEdit.Focus();
                }
                else if (lblEmpIDEdit.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select an employee";
                    txtEmpNMEdit.Focus();
                }
                else if (txtTradeEdit.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select trade";
                    txtTradeEdit.Focus();
                }
                else
                {
                    lblError.Visible = false;
                    iob.TransDT = DateTime.Parse(txtDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.TransMY = lblMy.Text;
                    iob.SiteID = txtSiteID.Text;
                    iob.EmpID = lblEmpIDEdit.Text;
                    iob.Trade = txtTradeEdit.Text;
                    if (lblDayTp.Text == "NORMAL")
                    {
                        if (txtNHrEdit.Text == "")
                        {
                            lblError.Visible = true;
                            lblError.Text = "Type normal hour.";
                            txtNHrEdit.Focus();
                        }
                        else
                        {
                            lblError.Visible = false;
                            iob.NorHR = Convert.ToDecimal(txtNHrEdit.Text);
                            if (txtNOTEdit.Text == "")
                                iob.NorOT = 0;
                            else
                                iob.NorOT = Convert.ToDecimal(txtNOTEdit.Text);
                        }
                    }
                    else if (lblDayTp.Text == "FRIDAY")
                    {
                        if (txtFOTEdit.Text == "")
                        {
                            lblError.Visible = true;
                            lblError.Text = "Type friday ot.";
                            txtFOTEdit.Focus();
                        }
                        else
                        {
                            lblError.Visible = false;
                            iob.FOT = Convert.ToDecimal(txtFOTEdit.Text);
                        }
                    }
                    else if (lblDayTp.Text == "HOLIDAY")
                    {
                        if (txtHOTEdit.Text == "")
                        {
                            lblError.Visible = true;
                            lblError.Text = "Type holiday ot.";
                            txtHOTEdit.Focus();
                        }
                        else
                        {
                            lblError.Visible = false;
                            iob.HOT = Convert.ToDecimal(txtHOTEdit.Text);
                        }
                    }
                    iob.Sl = Convert.ToInt64(lblSLEdit.Text);

                    iob.InTm = DateTime.Now;
                    iob.UserNm = HttpContext.Current.Session["UserName"].ToString();
                    iob.Ip = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.UserPc = HttpContext.Current.Session["PCName"].ToString();

                    dob.update_payroll_Employee_Work_Hour_HR_HOUR(iob);

                    gvEmphour.EditIndex = -1;
                    GridShow();

                    TextBox txtEmpNM = (TextBox)gvEmphour.FooterRow.FindControl("txtEmpNM");
                    txtEmpNM.Focus();
                }
            }
        }

        protected void gvEmphour_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (txtDt.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select a date";
                    txtDt.Focus();
                }
                else if (txtSiteNM.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select site";
                    txtSiteNM.Focus();
                }
                else if (txtSiteID.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select site";
                    txtSiteNM.Focus();
                }
                else
                {
                    Label lblSL = (Label)gvEmphour.Rows[e.RowIndex].FindControl("lblSL");
                    lblError.Visible = false;
                    iob.TransDT = DateTime.Parse(txtDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.TransMY = lblMy.Text;
                    iob.SiteID = txtSiteID.Text;
                    iob.Sl = Convert.ToInt64(lblSL.Text);

                    dob.delete_payroll_Employee_Work_Hour_HR_HOUR(iob);

                    GridShow();
                }
            }
        }

        protected void gvEmphour_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmphour.PageIndex = e.NewPageIndex;
            GridShow();
        }

        protected void Refresh()
        {
            txtSiteNM.Text = "";
            txtSiteID.Text = "";
            GridShow();
            txtSiteNM.Focus();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}