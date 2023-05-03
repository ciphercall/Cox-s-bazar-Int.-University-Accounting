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
    public partial class Department : System.Web.UI.Page
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
                    gridShow();
                    Session["CMPID"] = CMPID;
                    Session["USERID"] = UserID;

                }
            }
        }
        private void gridShow()
        {

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM HR_DEPT", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_DEPT.DataSource = ds;
                gv_DEPT.DataBind();
                TextBox txtDEPTNMFooter = (TextBox)gv_DEPT.FooterRow.FindControl("txtDEPTNMFooter");
                txtDEPTNMFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_DEPT.DataSource = ds;
                gv_DEPT.DataBind();
                int columncount = gv_DEPT.Rows[0].Cells.Count;
                gv_DEPT.Rows[0].Cells.Clear();
                gv_DEPT.Rows[0].Cells.Add(new TableCell());
                gv_DEPT.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_DEPT.Rows[0].Visible = false;
                TextBox txtDEPTNMFooter = (TextBox)gv_DEPT.FooterRow.FindControl("txtDEPTNMFooter");
                txtDEPTNMFooter.Focus();
            }
        }
        private void DEPTID()
        {
            string CMPID = int.Parse("101").ToString();
            string DEPTID = "";
            Global.lblAdd("SELECT MAX(DEPTID) FROM HR_DEPT WHERE COMPID='" + CMPID + "'", lblDEPTID);
            if (lblDEPTID.Text == "")
            {
                DEPTID = CMPID + "01";
            }
            else
            {
                string Substr = lblDEPTID.Text.Substring(3, 2);
                int subint = int.Parse(Substr) + 1;
                if (subint < 10)
                {
                    DEPTID = CMPID + "0" + subint;
                }
                else if (subint < 100)
                {
                    DEPTID = CMPID + subint;
                }
            }
            iob.DeptID = int.Parse(DEPTID);
        }
        protected void gv_DEPT_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            iob.PcName = Session["PCName"].ToString();
            iob.UserID = Session["UserName"].ToString();
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.InTime = Global.Dayformat1(DateTime.Now);
            try
            {
                if (Session["UserName"]==null)
                    Response.Redirect("~/cbiu/signin.aspx");
                else
                {
                    Label lblDEPTID = (Label)gv_DEPT.FooterRow.FindControl("lblDEPTID");
                    TextBox txtDEPTNMFooter = (TextBox)gv_DEPT.FooterRow.FindControl("txtDEPTNMFooter");
                    TextBox txtDEPTSNMFooter = (TextBox)gv_DEPT.FooterRow.FindControl("txtDEPTSNMFooter");
                    TextBox txtREMARKSFooter = (TextBox)gv_DEPT.FooterRow.FindControl("txtREMARKSFooter");
                    if (e.CommandName.Equals("Add"))
                    {

                        if (txtDEPTNMFooter.Text == "")
                        {
                            txtDEPTNMFooter.Focus();
                        }
                        else if (txtDEPTSNMFooter.Text == "")
                        {
                            txtDEPTSNMFooter.Focus();
                        }
                        else
                        {
                            iob.CmpID = int.Parse(CMPID); 
                            DEPTID();
                            iob.DeptNM = txtDEPTNMFooter.Text;
                            iob.DeptSNM = txtDEPTSNMFooter.Text; 
                            iob.Remarks = txtREMARKSFooter.Text;
                            dob.Insert_HR_DEPT(iob);
                            gridShow();
                        }
                    }
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void gv_DEPT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DEPTID();
                e.Row.Cells[0].Text = iob.DeptID.ToString();
            }
        }

        protected void gv_DEPT_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["UserName"]==null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                gv_DEPT.EditIndex = -1;
                gridShow();
            }
        }

        protected void gv_DEPT_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["UserName"]==null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                try
                {
                    Label lblDEPTID = (Label)gv_DEPT.Rows[e.RowIndex].FindControl("lblDEPTID");
                    if (conn.State != ConnectionState.Open)conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM HR_DEPT WHERE DEPTID = '" + lblDEPTID.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    if (conn.State != ConnectionState.Closed)conn.Close();
                    gridShow();
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }

            }
        }

        protected void gv_DEPT_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["UserName"]==null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                gv_DEPT.EditIndex = e.NewEditIndex;
                gridShow();
                TextBox txtDEPTNMEdit = (TextBox)gv_DEPT.Rows[e.NewEditIndex].FindControl("txtDEPTNMEdit");
                TextBox txtDEPTSNMEdit = (TextBox)gv_DEPT.Rows[e.NewEditIndex].FindControl("txtDEPTSNMEdit");
                if (txtDEPTSNMEdit.Text.Trim() == "")
                    txtDEPTSNMEdit.ReadOnly = false;
                else
                    txtDEPTSNMEdit.ReadOnly = true;
                txtDEPTNMEdit.Focus();
            }
        }

        protected void gv_DEPT_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            else
            {
                iob.UPDPcName = Session["PCName"].ToString();
                iob.UPDUserID = Session["UserName"].ToString();
                iob.UPDIpaddress = Session["IpAddress"].ToString();
                iob.UPDTime = Global.Dayformat1(DateTime.Now);

                Label lblDEPTID = (Label)gv_DEPT.Rows[e.RowIndex].FindControl("lblDEPTID");
                TextBox txtDEPTNMEdit = (TextBox)gv_DEPT.Rows[e.RowIndex].FindControl("txtDEPTNMEdit");
                TextBox txtDEPTSNMEdit = (TextBox)gv_DEPT.Rows[e.RowIndex].FindControl("txtDEPTSNMEdit");
                TextBox txtREMARKSEdit = (TextBox)gv_DEPT.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                if (txtDEPTNMEdit.Text == "")
                    txtDEPTNMEdit.Focus();
                else if (txtDEPTSNMEdit.Text == "")
                    txtDEPTSNMEdit.Focus();
                else
                {
                    iob.DeptID = int.Parse(lblDEPTID.Text);
                    iob.CmpID = int.Parse(CMPID);
                    //iob.UserID = int.Parse(UserID);
                    iob.DeptNM = txtDEPTNMEdit.Text;
                    iob.DeptSNM = txtDEPTSNMEdit.Text;
                    // iob.UPDLtude = txtLngTude.Text + "," + txtLtude.Text;
                    iob.Remarks = txtREMARKSEdit.Text;
                    dob.Update_HR_DEPT(iob);
                    gv_DEPT.EditIndex = -1;
                    gridShow();
                }



            }
        }
    }
}