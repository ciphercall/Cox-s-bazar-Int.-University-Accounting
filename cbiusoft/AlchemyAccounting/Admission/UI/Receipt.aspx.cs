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
using System.Collections.Specialized;
using AlchemyAccounting.Admission.DataAccess;
using AlchemyAccounting.Admission.DataModel;

namespace AlchemyAccounting.Admission.UI
{
    public partial class Receipt : System.Web.UI.Page
    {
        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection conn = new SqlConnection(Global.connection);
       
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
                    gridShow();
                    TextBox txtFEESNMFooter = (TextBox)GridView1.FooterRow.FindControl("txtFEESNMFooter");
                    txtFEESNMFooter.Focus();
                }
            }

        }
        private void gridShow()
        {

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM EIM_FEES", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

                TextBox txtFEESNMFooter = (TextBox)GridView1.FooterRow.FindControl("txtFEESNMFooter");
                txtFEESNMFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Visible = false;
                TextBox txtFEESNMFooter = (TextBox)GridView1.FooterRow.FindControl("txtFEESNMFooter");
                txtFEESNMFooter.Focus();
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (Session["UserName"]==null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    TextBox txtFEESIDFooter = (TextBox)GridView1.FooterRow.FindControl("txtFEESIDFooter");
                    TextBox txtFEESNMFooter = (TextBox)GridView1.FooterRow.FindControl("txtFEESNMFooter");
                    TextBox txtFEESRTFooter = (TextBox)GridView1.FooterRow.FindControl("txtFEESRTFooter");
                    TextBox txtREMARKSFooter = (TextBox)GridView1.FooterRow.FindControl("txtREMARKSFooter");

                    if (e.CommandName.Equals("Add"))
                    {
                        Global.lblAdd("SELECT MAX(FEESID) FROM EIM_FEES", lblFeesID);
                        if (lblFeesID.Text == "")
                        {
                            iob.FeesID = "101";
                        }
                        else
                        {
                            int FeesID = int.Parse(lblFeesID.Text) + 1;
                            iob.FeesID = FeesID.ToString();
                        }

                        iob.UserID = Session["UserName"].ToString();
                        iob.Ipaddress = Session["IpAddress"].ToString();
                        iob.PcName = Session["PCName"].ToString();
                        iob.InTime = Global.Dayformat1(DateTime.Now);

                        if (txtFEESNMFooter.Text == "")
                        {
                            txtFEESNMFooter.Focus();
                        }
                        else if (txtFEESRTFooter.Text == "")
                        {
                            txtFEESRTFooter.Focus();
                        }
                        else
                        {

                            iob.FeesNM = txtFEESNMFooter.Text;
                            iob.FeesRT = txtFEESRTFooter.Text;
                            iob.Remarks = txtREMARKSFooter.Text;
                            dob.Insert_EIM_FEES(iob);
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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView1.EditIndex = e.NewEditIndex;
            gridShow();
            TextBox txtFEESNMEdit = (TextBox)GridView1.Rows[e.NewEditIndex].FindControl("txtFEESNMEdit");
            txtFEESNMEdit.Focus();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblFEESID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblFEESID");
                if (conn.State != ConnectionState.Open)conn.Open();
                SqlCommand cmd = new SqlCommand("Delete from EIM_FEES where FEESID= '" + lblFEESID.Text + "'", conn);
                cmd.ExecuteNonQuery();
                if (conn.State != ConnectionState.Closed)conn.Close();
                gridShow();
                TextBox txtFEESNMFooter = (TextBox)GridView1.FooterRow.FindControl("txtFEESNMFooter");
                txtFEESNMFooter.Focus();
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (Session["UserName"]==null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    TextBox txtFEESNMEdit = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtFEESNMEdit");
                    Label lblFEESID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblFEESID");

                    TextBox txtFEESRTEdit = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtFEESRTEdit");
                    TextBox txtREMARKSEdit = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtREMARKSEdit");

                    if (txtFEESNMEdit.Text == "")
                    {
                        txtFEESNMEdit.Focus();
                    }
                    else if (txtFEESRTEdit.Text == "")
                    {
                        txtFEESRTEdit.Focus();
                    }
                    else
                    {

                        iob.UPDUserID = Session["UserName"].ToString();
                        iob.UPDIpaddress = Session["IpAddress"].ToString();
                        iob.UPDPcName = Session["PCName"].ToString();
                        iob.UPDTime = Global.Dayformat1(DateTime.Now);
                        //if (conn.State != ConnectionState.Open)conn.Open();
                        iob.FeesID = lblFEESID.Text;
                        iob.FeesNM = txtFEESNMEdit.Text;
                        iob.FeesRT = txtFEESRTEdit.Text;
                        iob.Remarks = txtREMARKSEdit.Text;
                        dob.Update_EIM_FEES(iob);

                        GridView1.EditIndex = -1;
                        gridShow();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            gridShow();
        }
    }
}