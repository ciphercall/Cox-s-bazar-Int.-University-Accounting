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
    public partial class hr_sal : System.Web.UI.Page
    {
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
                    DateTime td = DateTime.Now;
                    string month = td.ToString("MMM").ToUpper();
                    string year = td.ToString("yy");
                    txtMY.Text = month + "-" + year;
                    GridShow();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListMY(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT TRANSMY FROM HR_SALDRCR WHERE TRANSMY LIKE '" + prefixText + "%' ", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["TRANSMY"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtMY_TextChanged(object sender, EventArgs e)
        {
            if (txtMY.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Type month.";
                txtMY.Focus();
            }
            else
            {
                lblError.Visible = false;
                GridShow();
            }
        }

        private void GridShow()
        {
            SqlConnection conn = new SqlConnection(Global.connection);

            conn.Open();
            SqlCommand cmdd = new SqlCommand("SELECT HR_EMP.EMPNM, HR_SALDRCR.EMPID, HR_SALDRCR.TRANSMY, HR_SALDRCR.BONUS, HR_SALDRCR.OTCADD, HR_SALDRCR.ADVANCE, " +
                      " HR_SALDRCR.PENALTY, HR_SALDRCR.OTCDED FROM HR_SALDRCR INNER JOIN HR_EMP ON HR_SALDRCR.EMPID = HR_EMP.EMPID WHERE HR_SALDRCR.TRANSMY=@TRANSMY " +
                      " ORDER BY HR_SALDRCR.EMPID", conn);
            cmdd.Parameters.Clear();
            cmdd.Parameters.AddWithValue("@TRANSMY", txtMY.Text);
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

                TextBox txtEmpNM = (TextBox)gvEmphour.FooterRow.FindControl("txtEmpNM");
                txtEmpNM.Focus();
            }
        }
        
        protected void gvEmphour_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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
            TextBox txtBonus = (TextBox)row.FindControl("txtBonus");

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
                    txtBonus.Focus();
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

        protected void gvEmphour_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TextBox txtEmpNM = (TextBox)gvEmphour.FooterRow.FindControl("txtEmpNM");
            TextBox txtEmpID = (TextBox)gvEmphour.FooterRow.FindControl("txtEmpID");
            TextBox txtBonus = (TextBox)gvEmphour.FooterRow.FindControl("txtBonus");
            TextBox txtOthAdd = (TextBox)gvEmphour.FooterRow.FindControl("txtOthAdd");
            TextBox txtAdvance = (TextBox)gvEmphour.FooterRow.FindControl("txtAdvance");
            TextBox txtPenalty = (TextBox)gvEmphour.FooterRow.FindControl("txtPenalty");
            TextBox txtOthDed = (TextBox)gvEmphour.FooterRow.FindControl("txtOthDed");

            if (e.CommandName.Equals("SaveCon"))
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Login/UI/Login.aspx");
                }
                else
                {
                    if (txtMY.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Type month.";
                        txtMY.Focus();
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

                    else
                    {
                        lblError.Visible = false;

                        iob.TransMY = txtMY.Text;
                        iob.EmpID = txtEmpID.Text;
                        iob.TransMY = txtMY.Text;
                        if (txtBonus.Text == "")
                            txtBonus.Text = "0";
                        iob.Bouns = Convert.ToDecimal(txtBonus.Text);
                        if (txtOthAdd.Text == "")
                            txtOthAdd.Text = "0";
                        iob.OthAdd = Convert.ToDecimal(txtOthAdd.Text);
                        if (txtAdvance.Text == "")
                            txtAdvance.Text = "0";
                        iob.Advance = Convert.ToDecimal(txtAdvance.Text);
                        if (txtPenalty.Text == "")
                            txtPenalty.Text = "0";
                        iob.Penalty = Convert.ToDecimal(txtPenalty.Text);
                        if (txtOthDed.Text=="")
                            txtOthDed.Text="0";
                        iob.OthDed = Convert.ToDecimal(txtOthDed.Text);


                        iob.InTm = DateTime.Now;
                        iob.UserNm = HttpContext.Current.Session["UserName"].ToString();
                        iob.Ip = HttpContext.Current.Session["IpAddress"].ToString();
                        iob.UserPc = HttpContext.Current.Session["PCName"].ToString();

                        dob.payroll_Salary_Info_HR_SALDRCR(iob);
                        GridShow();
                        txtEmpNM.Focus();
                    }
                }
            }
        }

        protected void gvEmphour_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmphour.EditIndex = e.NewEditIndex;
            GridShow();

            TextBox txtBonusEdit = (TextBox)gvEmphour.Rows[e.NewEditIndex].FindControl("txtBonusEdit");
            txtBonusEdit.Focus();
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
                Label lblEmpNmEdit = (Label)gvEmphour.Rows[e.RowIndex].FindControl("lblEmpNmEdit");
                Label lblEmpIDEdit = (Label)gvEmphour.Rows[e.RowIndex].FindControl("lblEmpIDEdit");
                TextBox txtBonusEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtBonusEdit");
                TextBox txtOthAddEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtOthAddEdit");
                TextBox txtAdvanceEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtAdvanceEdit");
                TextBox txtPenaltyEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtPenaltyEdit");
                TextBox txtOthDedEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtOthDedEdit");

                if (txtMY.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type month.";
                    txtMY.Focus();
                }
                else if (lblEmpNmEdit.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select an employee";
                    lblEmpNmEdit.Focus();
                }
                else if (lblEmpIDEdit.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select an employee";
                    lblEmpNmEdit.Focus();
                }
                else
                {
                    lblError.Visible = false;
                    iob.TransMY = txtMY.Text;
                    iob.EmpID = lblEmpIDEdit.Text;
                    if (txtBonusEdit.Text == "")
                        txtBonusEdit.Text = "0";
                    iob.Bouns = Convert.ToDecimal(txtBonusEdit.Text);
                    if (txtOthAddEdit.Text == "")
                        txtOthAddEdit.Text = "0";
                    iob.OthAdd = Convert.ToDecimal(txtOthAddEdit.Text);
                    if (txtAdvanceEdit.Text == "")
                        txtAdvanceEdit.Text = "0";
                    iob.Advance = Convert.ToDecimal(txtAdvanceEdit.Text);
                    if (txtPenaltyEdit.Text == "")
                        txtPenaltyEdit.Text = "0";
                    iob.Penalty = Convert.ToDecimal(txtPenaltyEdit.Text);
                    if (txtOthDedEdit.Text == "")
                        txtOthDedEdit.Text = "0";
                    iob.OthDed = Convert.ToDecimal(txtOthDedEdit.Text);
                    

                    iob.InTm = DateTime.Now;
                    iob.UserNm = HttpContext.Current.Session["UserName"].ToString();
                    iob.Ip = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.UserPc = HttpContext.Current.Session["PCName"].ToString();

                    dob.update_payroll_Salary_Info_HR_SALDRCR(iob);

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
                if (txtMY.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type month.";
                    txtMY.Focus();
                }
                else
                {
                    Label lblEmpID = (Label)gvEmphour.Rows[e.RowIndex].FindControl("lblEmpID");
                    lblError.Visible = false;
                    iob.TransMY = txtMY.Text;
                    iob.EmpID = lblEmpID.Text;

                    dob.delete_payroll_Salary_Info_HR_SALDRCR(iob);

                    GridShow();
                }
            }
        }
    }
}