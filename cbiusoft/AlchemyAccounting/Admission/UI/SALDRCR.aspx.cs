using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using AlchemyAccounting.Admission.DataAccess;
using AlchemyAccounting.Admission.DataModel;

namespace AlchemyAccounting.Admission.UI
{
    public partial class SALDRCR : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection conn = new SqlConnection(Global.connection);
        string UserID = int.Parse("10101").ToString();
        string CMPID = int.Parse("101").ToString(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                if (!IsPostBack)
                {
                    // GridShow();
                    Session["CMPID"] = CMPID;
                    Session["USERID"] = UserID;
                    string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    int i, m;
                    int a = int.Parse(yr);
                    m = a + 5;
                    for (i = a - 5; i <= m; i++)
                    {
                        ddlTransMY_Year.Items.Add(i.ToString());
                    }
                    ddlTransMY_Year.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                }
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionMemberNM(string prefixText, int count, string contextKey)
        {

            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand("SELECT EMPNM+' | '+EMPID EMPNM FROM HR_EMP WHERE EMPNM+' | '+EMPID like '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["EMPNM"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }
        private void GridShow()
        {

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT     HR_SALDRCR.EMPID, HR_EMP.EMPNM, HR_SALDRCR.MMDAY, HR_SALDRCR.HDAY, HR_SALDRCR.PREDAY, HR_SALDRCR.ABSDAY, HR_SALDRCR.LDAY, HR_SALDRCR.ALLOWANCE, 
                      HR_SALDRCR.ADVANCE FROM HR_EMP INNER JOIN
                      HR_SALDRCR ON HR_EMP.EMPID = HR_SALDRCR.EMPID WHERE TRANSMY='" + lblTransMY.Text + "'", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gv_HR_SALDRCR.DataSource = ds;
                Gv_HR_SALDRCR.DataBind();
                TextBox txtEMPNMFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtEMPNMFooter");
                txtEMPNMFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                Gv_HR_SALDRCR.DataSource = ds;
                Gv_HR_SALDRCR.DataBind();
                int columncount = Gv_HR_SALDRCR.Rows[0].Cells.Count;
                Gv_HR_SALDRCR.Rows[0].Cells.Clear();
                Gv_HR_SALDRCR.Rows[0].Cells.Add(new TableCell());
                Gv_HR_SALDRCR.Rows[0].Cells[0].ColumnSpan = columncount;
                Gv_HR_SALDRCR.Rows[0].Visible = false;
                TextBox txtEMPNMFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtEMPNMFooter");
                txtEMPNMFooter.Focus();
            }
        }
        private void POstID()
        {

            Global.lblAdd("SELECT MAX(POSTID) FROM HR_POST", lblPostID);
            string CMPID = int.Parse("101").ToString();
            string postID = "";
            if (lblPostID.Text == "")
            {
                postID = CMPID + "01";
            }
            else
            {
                string Substr = lblPostID.Text.Substring(3, 2);
                int subint = int.Parse(Substr) + 1;
                if (subint < 10)
                {
                    postID = CMPID + "0" + subint;
                }
                else if (subint < 100)
                {
                    postID = CMPID + subint;
                }
            }
            iob.PostID = int.Parse(postID);
        }

        protected void Gv_HR_SALDRCR_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                Gv_HR_SALDRCR.EditIndex = -1;
                GridShow();
            }
        }

        protected void Gv_HR_SALDRCR_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["UserName"] == null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                try
                {
                    Label lblEMPID = (Label)Gv_HR_SALDRCR.Rows[e.RowIndex].FindControl("lblEMPID");
                    if (conn.State != ConnectionState.Open)conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM HR_SALDRCR WHERE EMPID = '" + lblEMPID.Text + "' and TRANSMY='" + lblTransMY.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    if (conn.State != ConnectionState.Closed)conn.Close();
                    GridShow();
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }

            }
        }

        protected void Gv_HR_SALDRCR_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                Gv_HR_SALDRCR.EditIndex = e.NewEditIndex;
                GridShow();
                TextBox txtEMPNMEdit = (TextBox)Gv_HR_SALDRCR.Rows[e.NewEditIndex].FindControl("txtEMPNMEdit");
                txtEMPNMEdit.Focus();
            }
        }

        protected void Gv_HR_SALDRCR_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            iob.PcName = Session["PCName"].ToString();
            iob.UserID = Session["UserName"].ToString();
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.InTime = Global.Dayformat1(DateTime.Now);
            TextBox txtEMPNMFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtEMPNMFooter");
            TextBox txtEMPIDFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtEMPIDFooter");
            TextBox txtMMDAYFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtMMDAYFooter");
            TextBox txtHDAYFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtHDAYFooter");
            TextBox txtPREDAYFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtPREDAYFooter");
            TextBox txtABSDAYFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtABSDAYFooter");
            TextBox txtLDAYFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtLDAYFooter");
            TextBox txtALLOWANCEFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtALLOWANCEFooter");
            TextBox txtADVANCEFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtADVANCEFooter");
            if (e.CommandName.Equals("Add"))
            {
                if (txtEMPNMFooter.Text == "")
                    txtEMPNMFooter.Focus();
                else
                {
                    iob.TRANSMY = lblTransMY.Text;
                    iob.EmpID = txtEMPIDFooter.Text;
                    iob.MMDay = int.Parse(txtMMDAYFooter.Text);
                    iob.HDay = int.Parse(txtHDAYFooter.Text);
                    iob.PreDay = int.Parse(txtPREDAYFooter.Text);
                    iob.AbsentDay = int.Parse(txtABSDAYFooter.Text);
                    iob.LDay = int.Parse(txtLDAYFooter.Text);
                    iob.ALLOWANCE = Decimal.Parse(txtALLOWANCEFooter.Text);
                    iob.ADVANCE = Decimal.Parse(txtADVANCEFooter.Text);
                    dob.INSERT_HR_SALDRCR(iob);
                    GridShow();
                }
            }
        }

        protected void Gv_HR_SALDRCR_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            iob.UPDPcName = Session["PCName"].ToString();
            iob.UPDUserID = Session["UserName"].ToString();
            iob.UPDIpaddress = Session["IpAddress"].ToString();
            iob.UPDTime = Global.Dayformat1(DateTime.Now);
            TextBox txtEMPNMEdit = (TextBox)Gv_HR_SALDRCR.Rows[e.RowIndex].FindControl("txtEMPNMEdit");
            TextBox txtEMPIDEdit = (TextBox)Gv_HR_SALDRCR.Rows[e.RowIndex].FindControl("txtEMPIDEdit");
            TextBox txtMMDAYEdit = (TextBox)Gv_HR_SALDRCR.Rows[e.RowIndex].FindControl("txtMMDAYEdit");
            TextBox txtHDAYEdit = (TextBox)Gv_HR_SALDRCR.Rows[e.RowIndex].FindControl("txtHDAYEdit");
            TextBox txtPREDAYEdit = (TextBox)Gv_HR_SALDRCR.Rows[e.RowIndex].FindControl("txtPREDAYEdit");
            TextBox txtABSDAYEdit = (TextBox)Gv_HR_SALDRCR.Rows[e.RowIndex].FindControl("txtABSDAYEdit");
            TextBox txtLDAYEdit = (TextBox)Gv_HR_SALDRCR.Rows[e.RowIndex].FindControl("txtLDAYEdit");
            TextBox txtALLOWANCEEdit = (TextBox)Gv_HR_SALDRCR.Rows[e.RowIndex].FindControl("txtALLOWANCEEdit");
            TextBox txtADVANCEEdit = (TextBox)Gv_HR_SALDRCR.Rows[e.RowIndex].FindControl("txtADVANCEEdit");
            if (txtEMPNMEdit.Text == "")
                txtEMPNMEdit.Focus();
            else
            {
                iob.TRANSMY = lblTransMY.Text;
                iob.EmpID = txtEMPIDEdit.Text;
                iob.MMDay = int.Parse(txtMMDAYEdit.Text);
                iob.HDay = int.Parse(txtHDAYEdit.Text);
                iob.PreDay = int.Parse(txtPREDAYEdit.Text);
                iob.AbsentDay = int.Parse(txtABSDAYEdit.Text);
                iob.LDay = int.Parse(txtLDAYEdit.Text);
                iob.ALLOWANCE = Decimal.Parse(txtALLOWANCEEdit.Text);
                iob.ADVANCE = Decimal.Parse(txtADVANCEEdit.Text);
                dob.UPDATE_HR_SALDRCR(iob);
                Gv_HR_SALDRCR.EditIndex = -1;
                GridShow();
            }
        }

        protected void Gv_HR_SALDRCR_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void ddlTransMY_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransMY_Month.Text == "Select")
                ddlTransMY_Month.Focus();
            else if (ddlTransMY_Year.Text == "Select")
                ddlTransMY_Year.Focus();
            else
            {
                lblTransMY.Text = ddlTransMY_Month.Text + "-" + ddlTransMY_Year.Text.Substring(2, 2);
                GridShow();
            }
        }

        protected void ddlTransMY_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransMY_Year.Text == "Select")
                ddlTransMY_Year.Focus();
            else if (ddlTransMY_Month.Text == "Select")
                ddlTransMY_Month.Focus();
            else
            {
                lblTransMY.Text = ddlTransMY_Month.Text + "-" + ddlTransMY_Year.Text.Substring(0, 2);
                GridShow();
            }
        }

        protected void txtEMPNMFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtEMPNMFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtEMPNMFooter");
            TextBox txtEMPIDFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtEMPIDFooter");
            TextBox txtMMDAYFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtMMDAYFooter");
            string EmpNM = Global.Slipt(txtEMPNMFooter.Text.Trim(),0,'|');
            string EmpID = Global.Slipt(txtEMPNMFooter.Text.Trim(), 1, '|');
            txtEMPNMFooter.Text = EmpNM;
            txtEMPIDFooter.Text = EmpID;
            txtMMDAYFooter.Focus();
        }

        protected void txtEMPNMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow Row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtEMPNMEdit = (TextBox)Row.FindControl("txtEMPNMEdit");
            TextBox txtEMPIDEdit = (TextBox)Row.FindControl("txtEMPIDEdit");
            TextBox txtMMDAYEdit = (TextBox)Row.FindControl("txtMMDAYEdit");
            string EmpNM = Global.Slipt(txtEMPNMEdit.Text.Trim(), 0, '|'); 
            string EmpID = Global.Slipt(txtEMPNMEdit.Text.Trim(), 1, '|');
            txtEMPNMEdit.Text = EmpNM;
            txtEMPIDEdit.Text = EmpID;
            txtMMDAYEdit.Focus();
        }

        protected void txtLDAYFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtMMDAYFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtMMDAYFooter");
            TextBox txtHDAYFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtHDAYFooter");
            TextBox txtPREDAYFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtPREDAYFooter");
            TextBox txtABSDAYFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtABSDAYFooter");
            TextBox txtLDAYFooter = (TextBox)Gv_HR_SALDRCR.FooterRow.FindControl("txtLDAYFooter");
            int TotalMonth = int.Parse(txtMMDAYFooter.Text) - int.Parse(txtHDAYFooter.Text);
            int TotalDay = TotalMonth - int.Parse(txtLDAYFooter.Text);
            TotalDay = TotalDay - int.Parse(txtPREDAYFooter.Text);
            txtABSDAYFooter.Text = TotalDay.ToString();
        }

        protected void txtLDAYEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow Row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtMMDAYEdit = (TextBox)Row.FindControl("txtMMDAYEdit");
            TextBox txtHDAYEdit = (TextBox)Row.FindControl("txtHDAYEdit");
            TextBox txtPREDAYEdit = (TextBox)Row.FindControl("txtPREDAYEdit");
            TextBox txtABSDAYEdit = (TextBox)Row.FindControl("txtABSDAYEdit");
            TextBox txtLDAYEdit = (TextBox)Row.FindControl("txtLDAYEdit");
            TextBox txtALLOWANCEEdit = (TextBox)Row.FindControl("txtALLOWANCEEdit");
            int TotalMonth = int.Parse(txtMMDAYEdit.Text) - int.Parse(txtHDAYEdit.Text);
            int TotalDay = TotalMonth - int.Parse(txtLDAYEdit.Text);
            TotalDay = TotalDay - int.Parse(txtPREDAYEdit.Text);
            txtABSDAYEdit.Text = TotalDay.ToString();
        }
    }
}