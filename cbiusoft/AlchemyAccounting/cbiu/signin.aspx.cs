using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net;
using System.Data;

namespace AlchemyAccounting.cbiu
{
    public partial class signin : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                Session["UserName"] = null;
                 
            }
            //RegisterHyperLink.NavigateUrl = "~/Login/UI/Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //RegisterHyperLink.NavigateUrl = "~/Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            // txtUserName.Focus();
        }

        protected void btnLogIN_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Global.connection);



                string uname = txtUserName.Text.Trim(); //Get the username from the control
                string password = txtPassword.Text.Trim();//get the Password from the control
                bool flag = AuthenticateUser(uname, password);
                if (flag == true)
                {

                     Session["UserName"] = txtUserName.Text; 
                    //Session["UserPC"] = Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName;
                    //string a = Session["UserPC"].ToString();

                    string strHostName = System.Net.Dns.GetHostName();
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];

                     Session["PCName"] = strHostName;
                     Session["IpAddress"] = ipAddress;
                     
                     
                    string UserTP = "";

                    SqlCommand cmdd = new SqlCommand("SELECT USERTP FROM User_Registration WHERE UserID=@UserID AND Password =@Password", con);
                    if (con.State != ConnectionState.Open)con.Open();
                    cmdd.Parameters.Clear();
                    cmdd.Parameters.AddWithValue("@UserID", uname);
                    cmdd.Parameters.AddWithValue("@Password", password);
                    SqlDataReader tpRead = null;
                    tpRead = cmdd.ExecuteReader();

                    if (tpRead.Read())
                    {
                        UserTP = tpRead["USERTP"].ToString();
                        Session["UserTp"] = UserTP;  
                    }
                    tpRead.Close();
                    if (con.State != ConnectionState.Closed)con.Close();

                     Session["PCName"] = strHostName; 
                    
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Password or username mismatched.";
                    txtPassword.Text = "";
                    txtUserName.Focus();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
        private bool AuthenticateUser(string uname, string password)
        {
            bool bflag = false;
            DataTable table = new DataTable();
            try
            {
                Login.DataAccess.LoginDataAccess dob = new Login.DataAccess.LoginDataAccess();
                table = dob.showEmpLoginInfo(uname, password);
                DataSet userDS = new DataSet();
            }
            catch (Exception ex)
            {
                table = null;
                Response.Write(ex.Message);
            }
            if (table != null)
            {
                if (table.Rows.Count > 0)
                    bflag = true;
            }
            return bflag;
        }
        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                lblmsg.Visible = true;
                lblmsg.Text = "User Name Missing.";
                txtUserName.Focus();
            }
            else
            {
                lblmsg.Visible = false;
                txtPassword.Focus();
            }
        }

        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Password Missing.";
                txtPassword.Focus();
            }
            else
            {
                lblmsg.Visible = false;
                btnLogIN.Focus();
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        { 
        }
    }
}