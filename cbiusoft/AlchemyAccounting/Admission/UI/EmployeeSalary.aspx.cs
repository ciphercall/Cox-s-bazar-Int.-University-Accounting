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

namespace AlchemyAccounting.Info.UI
{
    public partial class EmployeeSalary : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection conn = new SqlConnection(Global.connection);
        string UserID = int.Parse("10101").ToString();
        string CMPID = int.Parse("101").ToString();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"]==null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                if (!IsPostBack)
                {
                    Global.dropDownAddWithSelect(ddlEmp, "SELECT EMPNM FROM HR_EMP");
                    ddlEmp.Focus();

                    Label lblHeading = (Label)Master.FindControl("lblHeading");
                    lblHeading.Text = "Salary Information";
                }
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionPostNM(string prefixText, int count, string contextKey)
        {

            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand("SELECT POSTNM FROM HR_POST WHERE POSTNM like '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["POSTNM"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }
        private void gridShow()
        {
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT HR_POST.POSTNM, HR_EMPSALARY.POSTID, HR_EMPSALARY.SALSTATUS, HR_EMPSALARY.BASICSAL, HR_EMPSALARY.HOUSERENT, HR_EMPSALARY.MEDICAL, 
                      HR_EMPSALARY.TRANSPORT, HR_EMPSALARY.RSTAMP, HR_EMPSALARY.PFRATE, CONVERT(NVARCHAR(10),HR_EMPSALARY.PFEFDT,103) AS PFEFDT, CONVERT(NVARCHAR(10),HR_EMPSALARY.PFETDT,103) AS PFETDT, CONVERT(NVARCHAR(10),HR_EMPSALARY.JOBEFDT,103) AS JOBEFDT, 
                      CONVERT(NVARCHAR(10),HR_EMPSALARY.JOBETDT,103) AS JOBETDT
                      FROM HR_EMPSALARY INNER JOIN
                      HR_POST ON HR_EMPSALARY.POSTID = HR_POST.POSTID WHERE HR_EMPSALARY.EMPID='" + lblEmpID.Text + "' ORDER BY HR_EMPSALARY.POSTID", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Salary.DataSource = ds;
                gv_Salary.DataBind();
                //TextBox txtPOSTNMFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPOSTNMFooter");
                //txtPOSTNMFooter.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Salary.DataSource = ds;
                gv_Salary.DataBind();
                int columncount = gv_Salary.Rows[0].Cells.Count;
                gv_Salary.Rows[0].Cells.Clear();
                gv_Salary.Rows[0].Cells.Add(new TableCell());
                gv_Salary.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Salary.Rows[0].Visible = false;
                //TextBox txtPOSTNMFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPOSTNMFooter");
                // txtPOSTNMFooter.Focus();
            }
        }

        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.lblAdd("SELECT EMPID FROM HR_EMP WHERE EMPNM='" + ddlEmp.Text + "'", lblEmpID);
            gridShow();
        }

        protected void gv_Salary_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            iob.PcName = Session["PCName"].ToString();
            iob.UserID = Session["UserName"].ToString();
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.InTime = Global.Dayformat1(DateTime.Now);
            // iob.UserID = int.Parse(UserID);
            iob.CmpID = int.Parse(CMPID);
            if (e.CommandName.Equals("Add"))
            {
                TextBox txtPOSTNMFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPOSTNMFooter");
                Label lblPOSTIDFooter = (Label)gv_Salary.FooterRow.FindControl("lblPOSTIDFooter");
                DropDownList ddlStatsFooter = (DropDownList)gv_Salary.FooterRow.FindControl("ddlStatsFooter");
                TextBox txtBASICSALFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtBASICSALFooter");
                TextBox txtHOUSERENTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtHOUSERENTFooter");
                TextBox txtMEDICALFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtMEDICALFooter");
                TextBox txtTRANSPORTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtTRANSPORTFooter");
                TextBox txtRSTAMPFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtRSTAMPFooter");
                TextBox txtPFRATEFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPFRATEFooter");
                TextBox txtPFEFDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPFEFDTFooter");
                TextBox txtPFETDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPFETDTFooter");
                TextBox txtJOBEFDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtJOBEFDTFooter");
                TextBox txtJOBETDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtJOBETDTFooter");
                if (lblPOSTIDFooter.Text == "")
                    txtPOSTNMFooter.Focus();
                else if (ddlStatsFooter.Text == "Select")
                    ddlStatsFooter.Focus();
                else if (txtBASICSALFooter.Text == "")
                    txtBASICSALFooter.Focus();
                else if (txtHOUSERENTFooter.Text == "")
                    txtHOUSERENTFooter.Focus();
                else if (txtMEDICALFooter.Text == "")
                    txtMEDICALFooter.Focus();
                else if (txtJOBEFDTFooter.Text == "")
                    txtJOBEFDTFooter.Focus();
                else if (txtJOBETDTFooter.Text == "")
                    txtJOBETDTFooter.Focus();
                else
                {

                    //if (txtPFEFDTFooter.Text == "")
                    //{
                    //    iob.PFEffectFR = Convert.ToDateTime("01/01/1900");
                    //}
                    //else
                    //{
                    //    DateTime PFEFDT = DateTime.Parse(txtPFEFDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    //    iob.PFEffectFR = PFEFDT;
                    //}
                    //if (txtPFETDTFooter.Text == "")
                    //{
                    //    iob.PFEffectTO = Convert.ToDateTime("01/01/1900");
                    //}
                    //else
                    //{
                    //    DateTime PFETDT = DateTime.Parse(txtPFETDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    //    iob.PFEffectTO = PFETDT;
                    //}
                    //DateTime JOBEFDT = DateTime.Parse(txtJOBEFDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    //DateTime JOBETDT = DateTime.Parse(txtJOBETDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    //iob.PostID = int.Parse(lblPOSTIDFooter.Text);
                    //iob.EMPID = int.Parse(lblEmpID.Text);
                    //iob.Status = ddlStatsFooter.Text;
                    //iob.BasicSal = Decimal.Parse(txtBASICSALFooter.Text);
                    //iob.HouseRent = Decimal.Parse(txtHOUSERENTFooter.Text);
                    //iob.Medical = Decimal.Parse(txtMEDICALFooter.Text);
                    //iob.TrnsPort = Decimal.Parse(txtTRANSPORTFooter.Text);
                    //iob.Revenue = Decimal.Parse(txtRSTAMPFooter.Text);
                    //iob.PFRate = Decimal.Parse(txtPFRATEFooter.Text);
                    //iob.JOBEffectFR = JOBEFDT;
                    //iob.JOBEffectTO = JOBETDT;
                    //dob.INSERT_HR_EMPSALARY(iob);
                    //gridShow();
                }
            }
        }

        protected void txtPOSTNMFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtPOSTNMFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtPOSTNMFooter");
            Label lblPOSTIDFooter = (Label)gv_Salary.FooterRow.FindControl("lblPOSTIDFooter");
            DropDownList ddlStatsFooter = (DropDownList)gv_Salary.FooterRow.FindControl("ddlStatsFooter");
            if (txtPOSTNMFooter.Text != "")
            {
                Global.lblAdd("SELECT POSTID FROM HR_POST WHERE POSTNM='" + txtPOSTNMFooter.Text + "'", lblPOSTIDFooter);
                ddlStatsFooter.Focus();
            }
        }

        protected void txtPOSTNMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtPOSTNMEdit = (TextBox)row.FindControl("txtPOSTNMEdit");
            DropDownList ddlStatsEdit = (DropDownList)row.FindControl("ddlStatsEdit");
            Label lblPOSTIDEdit = (Label)row.FindControl("lblPOSTIDEdit");
            if (txtPOSTNMEdit.Text != "")
            {
                Global.lblAdd("SELECT POSTID FROM HR_POST WHERE POSTNM='" + txtPOSTNMEdit.Text + "'", lblPOSTIDEdit);
                ddlStatsEdit.Focus();

            }
        }

        protected void gv_Salary_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["UserName"]==null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                gv_Salary.EditIndex = -1;
                gridShow();
            }
        }

        protected void gv_Salary_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Label lblPOSTID = (Label)gv_Salary.Rows[e.RowIndex].FindControl("lblPOSTID");
            //Label lblStatus = (Label)gv_Salary.Rows[e.RowIndex].FindControl("lblStatus");

            //iob.PostID = int.Parse(lblPOSTID.Text);
            //iob.Status = lblStatus.Text;
            //iob.CmpID = int.Parse(CMPID);
            //iob.EMPID = int.Parse(lblEmpID.Text);
            //dob.DELETE_HR_EMPSALARY(iob);
            //gridShow();
        }

        protected void gv_Salary_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["UserName"]==null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                gv_Salary.EditIndex = e.NewEditIndex;
                gridShow();
                TextBox txtPOSTNMEdit = (TextBox)gv_Salary.Rows[e.NewEditIndex].FindControl("txtPOSTNMEdit");
                txtPOSTNMEdit.Focus();
            }
        }

        protected void gv_Salary_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //iob.UPDUserPC = Session["PCName"];
            //iob.UPDUsername = Session["UserName"];
            //iob.UPDIpaddress = Session["IpAddress"];
            //iob.UPDTime = Global.Timezone(DateTime.Now);
            //iob.UserID = int.Parse(UserID);
            //iob.CmpID = int.Parse(CMPID);

            //TextBox txtPOSTNMEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtPOSTNMEdit");
            //Label lblPOSTIDEdit = (Label)gv_Salary.Rows[e.RowIndex].FindControl("lblPOSTIDEdit");
            //DropDownList ddlStatsEdit = (DropDownList)gv_Salary.Rows[e.RowIndex].FindControl("ddlStatsEdit");
            //TextBox txtBASICSALEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtBASICSALEdit");
            //TextBox txtHOUSERENTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtHOUSERENTEdit");
            //TextBox txtMEDICALEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtMEDICALEdit");
            //TextBox txtTRANSPORTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtTRANSPORTEdit");
            //TextBox txtRSTAMPEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtRSTAMPEdit");
            //TextBox txtPFRATEEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtPFRATEEdit");
            //TextBox txtPFEFDTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtPFEFDTEdit");
            //TextBox txtPFETDTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtPFETDTEdit");
            //TextBox txtJOBEFDTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtJOBEFDTEdit");
            //TextBox txtJOBETDTEdit = (TextBox)gv_Salary.Rows[e.RowIndex].FindControl("txtJOBETDTEdit");
            //if (lblPOSTIDEdit.Text == "")
            //    txtPOSTNMEdit.Focus();
            //else if (ddlStatsEdit.Text == "Select")
            //    ddlStatsEdit.Focus();
            //else if (txtBASICSALEdit.Text == "")
            //    txtBASICSALEdit.Focus();
            //else if (txtHOUSERENTEdit.Text == "")
            //    txtHOUSERENTEdit.Focus();
            //else if (txtMEDICALEdit.Text == "")
            //    txtMEDICALEdit.Focus();        
            //else if (txtJOBEFDTEdit.Text == "")
            //    txtJOBEFDTEdit.Focus();
            //else if (txtJOBETDTEdit.Text == "")
            //    txtJOBETDTEdit.Focus();
            //else
            //{
            //    if (txtPFEFDTEdit.Text == "")
            //    {
            //        iob.PFEffectFR = Convert.ToDateTime("01/01/1900");
            //    }
            //    else
            //    {
            //        DateTime PFEFDT = DateTime.Parse(txtPFEFDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            //        iob.PFEffectFR = PFEFDT;
            //    }
            //    if (txtPFETDTEdit.Text == "")
            //    {
            //        iob.PFEffectTO = Convert.ToDateTime("01/01/1900");
            //    }
            //    else
            //    {
            //        DateTime PFETDT = DateTime.Parse(txtPFETDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            //        iob.PFEffectTO = PFETDT;
            //    }
            //    DateTime JOBEFDT = DateTime.Parse(txtJOBEFDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            //    DateTime JOBETDT = DateTime.Parse(txtJOBETDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            //    iob.PostID = int.Parse(lblPOSTIDEdit.Text);
            //    iob.EMPID = int.Parse(lblEmpID.Text);
            //    iob.Status = ddlStatsEdit.Text;
            //    iob.BasicSal = Decimal.Parse(txtBASICSALEdit.Text);
            //    iob.HouseRent = Decimal.Parse(txtHOUSERENTEdit.Text);
            //    iob.Medical = Decimal.Parse(txtMEDICALEdit.Text);
            //    iob.TrnsPort = Decimal.Parse(txtTRANSPORTEdit.Text);
            //    iob.Revenue = Decimal.Parse(txtRSTAMPEdit.Text);
            //    iob.PFRate = Decimal.Parse(txtPFRATEEdit.Text);
            //    iob.JOBEffectFR = JOBEFDT;
            //    iob.JOBEffectTO = JOBETDT;
            //    dob.UPDATE_HR_EMPSALARY(iob);
            //    gv_Salary.EditIndex = -1;
            //    gridShow();
            //}
        }

        protected void txtJOBETDTFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtJOBETDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtJOBETDTFooter");
            DateTime Date = DateTime.Parse(txtJOBETDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            txtJOBETDTFooter.Text = Date.ToString("dd/MM/yy");
        }

        protected void txtJOBETDTEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtJOBETDTEdit = (TextBox)row.FindControl("txtJOBETDTEdit");
            DateTime Date = DateTime.Parse(txtJOBETDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            txtJOBETDTEdit.Text = Date.ToString("dd/MM/yy");
        }

        protected void txtJOBEFDTFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtJOBEFDTFooter = (TextBox)gv_Salary.FooterRow.FindControl("txtJOBEFDTFooter");
            DateTime Date = DateTime.Parse(txtJOBEFDTFooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            txtJOBEFDTFooter.Text = Date.ToString("dd/MM/yy");
        }

        protected void txtJOBEFDTEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtJOBEFDTEdit = (TextBox)row.FindControl("txtJOBEFDTEdit");
            DateTime Date = DateTime.Parse(txtJOBEFDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            txtJOBEFDTEdit.Text = Date.ToString("dd/MM/yy");
        }

    }
}