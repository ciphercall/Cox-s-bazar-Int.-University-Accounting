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
    public partial class Program : System.Web.UI.Page
    {
        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection con = new SqlConnection(Global.connection);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                ddlProgTP.Focus();
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionProgram(string prefixText, int count, string contextKey)
        {

            
            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ("SELECT PROGRAMNM FROM EIM_PROGRAM WHERE PROGRAMNM LIKE '" + prefixText + "%'");

            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["PROGRAMNM"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }
        private void Clear()
        {
            ddlProgTP.SelectedIndex = -1;
            txtProNM.Text = "";
            lblProgID.Text = "";
            txtProgSrtNM.Text = "";
            txtTtlCrdt.Text = "";
            txtCstPerCrdt.Text = "";
            txtDuration.Text = "";
            txtTtlAmnt.Text = "";
            txtRemarks.Text = "";
            txtProgID.Text = "";

        }
        protected void btnInclds_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"]==null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    if (con.State != ConnectionState.Open)con.Open();
                    string Search = "SELECT * FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtProNM.Text + "'";
                    SqlCommand cmd = new SqlCommand(Search, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (con.State != ConnectionState.Closed)con.Close();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "Already Exist !";
                        txtProNM.Focus();
                    }
                    else
                    {
                        iob.UserID = Session["UserName"].ToString();
                        iob.Ipaddress = Session["IpAddress"].ToString();
                        iob.PcName = Session["PCName"].ToString();
                        iob.InTime = Global.Dayformat1(DateTime.Now);
                        Global.lblAdd("SELECT MAX(PROGRAMID) FROM EIM_PROGRAM", lblProgID);
                        int PID = 0;
                        if (lblProgID.Text == "")
                            lblProgID.Text = "0";
                        PID = int.Parse(lblProgID.Text) + 1;
                        if (lblProgID.Text == "0")
                        {
                            iob.ProgID = "01";
                        }
                        else
                        {

                            if (PID < 10)
                            {
                                iob.ProgID = "0" + PID;
                            }
                            else if (PID < 100)
                            {
                                iob.ProgID = PID.ToString();
                            }
                        }
                        lblMSG.Visible = false;
                        iob.ProgTP = ddlProgTP.Text;
                        iob.ProgNM = txtProNM.Text;
                        iob.ProgSrtNM = txtProgSrtNM.Text;
                        iob.TotlCrdt = Convert.ToDouble(txtTtlCrdt.Text);
                        iob.CstPerCrdt = Decimal.Parse(txtCstPerCrdt.Text);
                        iob.Dura = txtDuration.Text;
                        iob.TotlAmnt = Decimal.Parse(txtTtlAmnt.Text);
                        iob.Remarks = txtRemarks.Text;
                        dob.InsertProgram(iob);
                        Clear();
                        ddlProgTP.Focus();
                        lblMSG.Visible = true;
                        lblMSG.Text = "Included !";
                    }
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }

        }

        protected void btnUpd_Click(object sender, EventArgs e)
        {
            try
            {
                iob.UPDUserID = Session["UserName"].ToString();
                iob.UPDIpaddress = Session["IpAddress"].ToString();
                iob.UPDPcName = Session["PCName"].ToString();
                iob.UPDTime = Global.Dayformat1(DateTime.Now);
                if (Session["UserName"]==null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    if (txtProNM.Text == "")
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "Select Program Name !";
                        txtProNM.Focus();
                    }
                    else
                    {
                        lblMSG.Visible = false;
                        iob.ProgID = txtProgID.Text;
                        iob.ProgTP = ddlProgTP.Text;
                        iob.ProgNM = txtProNM.Text;
                        iob.ProgSrtNM = txtProgSrtNM.Text;
                        iob.TotlCrdt = Convert.ToDouble(txtTtlCrdt.Text);
                        iob.CstPerCrdt = Decimal.Parse(txtCstPerCrdt.Text);
                        iob.Dura = txtDuration.Text;
                        iob.TotlAmnt = Decimal.Parse(txtTtlAmnt.Text);
                        iob.Remarks = txtRemarks.Text;
                        dob.UpdateProgram(iob);
                        Clear();
                        ddlProgTP.Focus();
                        lblMSG.Visible = true;
                        lblMSG.Text = "Updated !";

                    }
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void btnDlt_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProgID.Text == "")
                {
                    lblMSG.Visible = true;
                    lblMSG.Text = "Select Program Name !";
                    txtProNM.Focus();
                }
                lblMSG.Visible = false;
                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand("DELETE from EIM_PROGRAM where PROGRAMID = '" + txtProgID.Text + "'", con);
                cmd.ExecuteNonQuery();
                if (con.State != ConnectionState.Closed)con.Close();
                Clear();
                ddlProgTP.Focus();
                ddlProgTP.Focus();
                lblMSG.Visible = true;
                lblMSG.Text = "Deleted !";
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void txtProNM_TextChanged(object sender, EventArgs e)
        {
            if (txtProNM.Text != "")
            {
                Global.txtAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtProNM.Text + "'", txtProgID);
                Global.txtAdd("SELECT PROGRAMSID FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtProNM.Text + "'", txtProgSrtNM);
                Global.txtAdd("SELECT TOTCREDIT FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtProNM.Text + "'", txtTtlCrdt);
                Global.txtAdd("SELECT COSTPERCR FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtProNM.Text + "'", txtCstPerCrdt);
                Global.txtAdd("SELECT DURATION FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtProNM.Text + "'", txtDuration);
                Global.txtAdd("SELECT TOTFEES FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtProNM.Text + "'", txtTtlAmnt);
                Global.txtAdd("SELECT REMARKS FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtProNM.Text + "'", txtRemarks);
                txtProgSrtNM.Focus();
                lblMSG.Text = "";
                lblPTP.Text = "";
                Global.lblAdd("SELECT PROGRAMTP FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtProNM.Text + "'", lblPTP);
                if (lblPTP.Text == "Graduate")
                {
                    ddlProgTP.SelectedIndex = 1;
                }
                else if (lblPTP.Text == "Under Graduate")
                {
                    ddlProgTP.SelectedIndex = 2;
                }
                else if (lblPTP.Text == "Diploma & Others")
                {
                    ddlProgTP.SelectedIndex = 3;
                }


            }
            else
            {
                // txtProNM.Focus();
                Clear();
                ddlProgTP.Focus();

            }
            if (txtProgID.Text != "")
            {
                btnUpd.Visible = true;
                btnDlt.Visible = true;
            }
            else
            {
                btnUpd.Visible = false;
                btnDlt.Visible = false;
            }
        }

        protected void ddlProgTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProNM.Focus();
            lblMSG.Visible = false;
        }
    }
}