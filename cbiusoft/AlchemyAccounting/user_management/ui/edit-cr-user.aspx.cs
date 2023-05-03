using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;

namespace AlchemyAccounting.cr_user.ui
{
    public partial class edit_cr_user : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    txtSearch.Focus();

                    
                    SqlConnection conn = new SqlConnection(Global.connection);

                    if (conn.State != ConnectionState.Open)conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT LoginID, UserID, Name, Password, Email, OpenUser, USERTP, PerEd, PerDel FROM User_Registration", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (conn.State != ConnectionState.Closed)conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvDetails.DataSource = ds;
                        gvDetails.DataBind();
                        gvDetails.Visible = true;
                    }
                    else
                    {
                        Response.Write("<script>alert('No Data Found');</script>");
                        gvDetails.Visible = false;
                    }
                }
            }
        }

        public void ShowGrid()
        {
            
            SqlConnection conn = new SqlConnection(Global.connection);

            string src = txtSearch.Text;

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT LoginID, UserID, Name, Password, Email, OpenUser, USERTP, PerEd, PerDel FROM User_Registration WHERE User_Registration.Name like '" + txtSearch.Text + "%' or User_Registration.UserID like '" + txtSearch.Text + "%' or User_Registration.Email like '" + txtSearch.Text + "%' ", conn);

            cmd.Parameters.AddWithValue("@src", src);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;
            }
            else
            {
                Response.Write("<script>alert('No Data Found');</script>");
                gvDetails.Visible = false;
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ShowGrid();
            txtSearch.Focus();
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            ShowGrid();
            txtSearch.Focus();
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            ShowGrid();

            TextBox txtuseridEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtuseridEdit");
            CheckBox chkEdit = (CheckBox)gvDetails.Rows[e.NewEditIndex].FindControl("chkEdit");
            CheckBox chkDelete = (CheckBox)gvDetails.Rows[e.NewEditIndex].FindControl("chkDelete");
            DropDownList ddlUserTypeEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlUserTypeEdit");

            lblchkEdit.Text = "";
            Global.lblAdd("SELECT PerEd FROM User_Registration WHERE UserID ='" + txtuseridEdit.Text + "'", lblchkEdit);
            if (lblchkEdit.Text == "")
                chkEdit.Checked = false;
            else
                chkEdit.Checked = true;

            lblchkDel.Text = "";
            Global.lblAdd("SELECT PerDel FROM User_Registration WHERE UserID ='" + txtuseridEdit.Text + "'", lblchkDel);
            if (lblchkDel.Text == "")
                chkDelete.Checked = false;
            else
                chkDelete.Checked = true;

            lblUserTp.Text = "";
            Global.lblAdd("SELECT USERTP FROM User_Registration WHERE UserID ='" + txtuseridEdit.Text + "'", lblUserTp);
            ddlUserTypeEdit.Text = lblUserTp.Text;

            TextBox txtnameEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtnameEdit");
            txtnameEdit.Focus();
            Label lblLoginIDEdit = (Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblLoginIDEdit");
        }

        protected void txtEmailEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtEmailEdit = (TextBox)row.FindControl("txtEmailEdit");
            TextBox txtuseridEdit = (TextBox)row.FindControl("txtuseridEdit");
            DropDownList ddlBranchEdit = (DropDownList)row.FindControl("ddlBranchEdit");
            txtuseridEdit.Text = txtEmailEdit.Text;
            ddlBranchEdit.Focus();
        }

        //protected void ddlBranchEdit_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
        //    DropDownList ddlBranchEdit = (DropDownList)row.FindControl("ddlBranchEdit");
        //    DropDownList ddlUserTypeEdit = (DropDownList)row.FindControl("ddlUserTypeEdit");
        //    lblBranchCD.Text = "";
        //    Functions.lblAdd("SELECT CATID FROM GL_COSTP WHERE COSTPNM ='" + ddlBranchEdit.Text + "'", lblBranchCD);
        //    ddlUserTypeEdit.Focus();
        //}

        protected void ddlUserTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            TextBox txtPassEdit = (TextBox)row.FindControl("txtPassEdit");
            txtPassEdit.Focus();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            SqlConnection conn = new SqlConnection(Global.connection);
            string OpenUserName = Session["UserName"].ToString();
            string PcName = Session["PCName"].ToString();
            string ip = Session["IpAddress"].ToString();

            TextBox txtnameEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtnameEdit");
            TextBox txtEmailEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtEmailEdit");
            TextBox txtuseridEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtuseridEdit");
            DropDownList ddlUserTypeEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlUserTypeEdit");
            TextBox txtPassEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtPassEdit");
            Label lblLoginIDEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblLoginIDEdit");
            CheckBox chkEdit = (CheckBox)gvDetails.Rows[e.RowIndex].FindControl("chkEdit");
            CheckBox chkDelete = (CheckBox)gvDetails.Rows[e.RowIndex].FindControl("chkDelete");

            string Edit, Del = "";
            if (chkEdit.Checked == true)
                Edit = "Edit";
            else
                Edit = "";
            if (chkDelete.Checked == true)
                Del = "Delete";
            else
                Del = "";

            //DateTime lst_UpDT = DateTime.Now;
            //string lastUp = lst_UpDT.ToString("yyyy-MM-dd");

            //if (conn.State != ConnectionState.Open)conn.Open();
            //SqlCommand cmd = new SqlCommand(" INSERT INTO logData(userSID, tableID, description, userPc, userID, ipAddress) " +
            //                                " VALUES (" + lblUserSidEdit.Text + ",'userRegistration',(SELECT (name + ' ' + phone + ' ' + company + ' ' + address + ' ' + email + ' ' + webID + ' ' + userName + ' ' + password + ' ' + maskName + ' ' + convert(nvarchar(20),creditLimit,103) + ' ' + status + ' ' + userPc + ' ' + userID + ' ' + ipAddress) FROM userRegistration where userSID= " + lblUserSidEdit.Text + "), " +
            //                                " '" + PcName + "','" + userName + "','" + ip + "')", conn);
            //cmd.ExecuteNonQuery();
            //if (conn.State != ConnectionState.Closed)conn.Close();

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd1 = new SqlCommand(" update User_Registration set Name = '" + txtnameEdit.Text + "', Email = '" + txtEmailEdit.Text + "', UserID = '" + txtuseridEdit.Text + "', " +
                                            " Password = '" + txtPassEdit.Text + "',  USERTP ='" + ddlUserTypeEdit.Text + "',userPc = '" + PcName + "', OpenUser = '" + OpenUserName + "', ipAddress = '" + ip + "',  PerEd = '" + Edit + "', PerDel ='" + Del + "' WHERE LoginID= " + lblLoginIDEdit.Text + " ", conn);
            cmd1.ExecuteNonQuery();
            if (conn.State != ConnectionState.Closed)conn.Close();
            //Response.Write("<script>alert('Successfully Updated');</script>");
            gvDetails.EditIndex = -1;
            ShowGrid();
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            SqlConnection conn = new SqlConnection(Global.connection);
            string OpenUserName = Session["UserName"].ToString();
            string PcName = Session["PCName"].ToString();
            string ip = Session["IpAddress"].ToString();

            Label lblLoginID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblLoginID");

            //DateTime lst_UpDT = DateTime.Now;
            //string lastUp = lst_UpDT.ToString("yyyy-MM-dd");

            //if (conn.State != ConnectionState.Open)conn.Open();
            //SqlCommand cmd = new SqlCommand(" INSERT INTO logData(userSID, tableID, description, userPc, userID, ipAddress) " +
            //                                " VALUES (" + lblUserSid.Text + ",'userRegistration',(SELECT (name + ' ' + phone + ' ' + company + ' ' + address + ' ' + email + ' ' + webID + ' ' + userName + ' ' + password + ' ' + maskName + ' ' + convert(nvarchar(20),creditLimit,103) + ' ' + status + ' ' + userPc + ' ' + userID + ' ' + ipAddress) FROM userRegistration where userSID= " + lblUserSid.Text + "), " +
            //                                " '" + PcName + "','" + userName + "','" + ip + "')", conn);
            //cmd.ExecuteNonQuery();
            //if (conn.State != ConnectionState.Closed)conn.Close();

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd1 = new SqlCommand("delete from User_Registration where LoginID = '" + lblLoginID.Text + "' ", conn);
            cmd1.ExecuteNonQuery();
            if (conn.State != ConnectionState.Closed)conn.Close();
            ShowGrid();
        }
    }
}