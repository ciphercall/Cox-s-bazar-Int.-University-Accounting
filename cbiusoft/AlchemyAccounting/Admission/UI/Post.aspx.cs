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
    public partial class Post : System.Web.UI.Page
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
                    gridShow();

                    Session["CMPID"] = CMPID;
                    Session["USERID"] = UserID;
                }
            }
        }
        private void gridShow()
        {

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM HR_POST", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_post.DataSource = ds;
                gv_post.DataBind();
                TextBox txtPOSTNMFooter = (TextBox)gv_post.FooterRow.FindControl("txtPOSTNMFooter");
                txtPOSTNMFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_post.DataSource = ds;
                gv_post.DataBind();
                int columncount = gv_post.Rows[0].Cells.Count;
                gv_post.Rows[0].Cells.Clear();
                gv_post.Rows[0].Cells.Add(new TableCell());
                gv_post.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_post.Rows[0].Visible = false;
                TextBox txtPOSTNMFooter = (TextBox)gv_post.FooterRow.FindControl("txtPOSTNMFooter");
                txtPOSTNMFooter.Focus();
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
        protected void gv_post_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            iob.PcName = Session["PCName"].ToString();
            iob.UserID = Session["UserName"].ToString();
            iob.Ipaddress = Session["IpAddress"].ToString();
            iob.InTime = Global.Dayformat1(DateTime.Now);
            try
            {
                if (Session["UserName"] == null)
                    Response.Redirect("~/cbiu/signin.aspx");
                else
                {
                    Label lblPOSTID = (Label)gv_post.FooterRow.FindControl("lblPOSTID");
                    TextBox txtPOSTNMFooter = (TextBox)gv_post.FooterRow.FindControl("txtPOSTNMFooter");
                    TextBox txtREMARKSFooter = (TextBox)gv_post.FooterRow.FindControl("txtREMARKSFooter");
                    if (e.CommandName.Equals("Add"))
                    {

                        if (txtPOSTNMFooter.Text == "")
                        {
                            txtPOSTNMFooter.Focus();
                        }
                        else
                        {
                            iob.CmpID = int.Parse(CMPID);
                            // iob.userID = int.Parse(UserID);
                            POstID();
                            iob.PostNM = txtPOSTNMFooter.Text;
                            // iob.Ltude = txtLngTude.Text + "," + txtLtude.Text;
                            iob.Remarks = txtREMARKSFooter.Text;
                            dob.Insert_HR_POST(iob);
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

        protected void gv_post_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                POstID();
                e.Row.Cells[0].Text = iob.PostID.ToString();
            }
        }

        protected void gv_post_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                gv_post.EditIndex = -1;
                gridShow();
            }
        }

        protected void gv_post_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Session["UserName"] == null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                try
                {
                    Label lblPOSTID = (Label)gv_post.Rows[e.RowIndex].FindControl("lblPOSTID");
                    if (conn.State != ConnectionState.Open)conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM HR_POST WHERE POSTID = '" + lblPOSTID.Text + "'", conn);
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

        protected void gv_post_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/cbiu/signin.aspx");
            else
            {
                gv_post.EditIndex = e.NewEditIndex;
                gridShow();
                TextBox txtPOSTNMEdit = (TextBox)gv_post.Rows[e.NewEditIndex].FindControl("txtPOSTNMEdit");
                txtPOSTNMEdit.Focus();
            }
        }

        protected void gv_post_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            else
            {
                iob.UPDPcName = Session["PCName"].ToString();
                iob.UPDUserID = Session["UserName"].ToString();
                iob.UPDIpaddress = Session["IpAddress"].ToString();
                iob.UPDTime = Global.Dayformat1(DateTime.Now);

                Label lblPOSTID = (Label)gv_post.Rows[e.RowIndex].FindControl("lblPOSTID");
                TextBox txtPOSTNMEdit = (TextBox)gv_post.Rows[e.RowIndex].FindControl("txtPOSTNMEdit");
                TextBox txtREMARKSEdit = (TextBox)gv_post.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                if (txtPOSTNMEdit.Text == "")
                    txtPOSTNMEdit.Focus();
                else
                {
                    iob.PostID = int.Parse(lblPOSTID.Text);
                    iob.CmpID = int.Parse(CMPID);
                    // iob.UserID = int.Parse(UserID);                 
                    iob.PostNM = txtPOSTNMEdit.Text;
                    //iob.UPDLtude = txtLngTude.Text + "," + txtLtude.Text;
                    iob.Remarks = txtREMARKSEdit.Text;
                    dob.Update_HR_POST(iob);
                    gv_post.EditIndex = -1;
                    gridShow();
                }
            }
        }
    }
}