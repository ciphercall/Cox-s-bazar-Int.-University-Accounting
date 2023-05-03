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
    public partial class Semester : System.Web.UI.Page
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
                txtSemester.Focus();
            }

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionSemester(string prefixText, int count, string contextKey)
        {

            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT SEMESTERNM FROM EIM_SEMESTER WHERE SEMESTERNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["SEMESTERNM"].ToString()); 
            }
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();


        }

        protected void btnIncld_Click(object sender, EventArgs e)
        {
            try
            {
                iob.UserID = Session["UserName"].ToString();
                iob.Ipaddress = Session["IpAddress"].ToString();
                iob.PcName = Session["PCName"].ToString();
                iob.InTime = Global.Dayformat1(DateTime.Now);
                if (Session["UserName"]==null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    if (con.State != ConnectionState.Open)con.Open();
                    string Search = "SELECT * FROM EIM_SEMESTER WHERE SEMESTERNM='" + txtSemester.Text + "'";
                    SqlCommand cmd = new SqlCommand(Search, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (con.State != ConnectionState.Closed)con.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "Already Exist !";
                    }
                    else
                    {
                        lblMSG.Visible = false;
                        lblSemisID.Text = "";
                        Global.lblAdd("SELECT MAX(SEMESTERID) FROM EIM_SEMESTER", lblSemisID);
                        string semID;
                        if (lblSemisID.Text == "")
                        {
                            iob.SemID = 1;
                        }
                        else
                        {
                            iob.SemID = int.Parse(lblSemisID.Text) + 1;
                        }


                        iob.SemNM = txtSemester.Text;
                        iob.StrtTime = txtStartTime.Text;
                        iob.Remarks = txtRemarks.Text;
                        dob.InsertSemester(iob);
                        txtSemester.Text = "";
                        txtRemarks.Text = "";
                        txtStartTime.Text = "";
                        txtSemiID.Text = "";
                        lblMSG.Visible = true;
                        lblMSG.Text = "Included !";
                        // Response.Write("<script>alert('Successed !, either Debit Amount or Credit Amount');</script>");
                    }

                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void txtSemester_TextChanged(object sender, System.EventArgs e)
        {
            lblMSG.Text = "";
            if (txtSemester.Text == "")
            {
                txtStartTime.Text = "";
                txtRemarks.Text = "";
                txtSemiID.Text = "";
                txtSemester.Focus();
            }
            else
            {
                Global.txtAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + txtSemester.Text + "'", txtSemiID);
                Global.txtAdd("SELECT STARTMM FROM EIM_SEMESTER WHERE SEMESTERID='" + txtSemiID.Text + "'", txtStartTime);
                Global.txtAdd("SELECT REMARKS FROM EIM_SEMESTER WHERE SEMESTERID='" + txtSemiID.Text + "'", txtRemarks);
                txtStartTime.Focus();
            }
            if (txtSemiID.Text != "")
            {
                btnDlt.Visible = true;
                btnUpdt.Visible = true;
            }
            else
            {
                btnDlt.Visible = false;
                btnUpdt.Visible = false;
            }

        }

        protected void btnUpdt_Click(object sender, System.EventArgs e)
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
                    if (lblSemisID.Text == "")
                    {
                        lblMSG.Visible = true;
                        lblMSG.Text = "Select Semester Name !";
                    }
                    else
                    {
                        lblMSG.Visible = false;
                        iob.SemID = int.Parse(txtSemiID.Text);
                        iob.SemNM = txtSemester.Text;
                        iob.StrtTime = txtStartTime.Text;
                        iob.Remarks = txtRemarks.Text;
                        dob.UpdateSemester(iob);
                        txtSemester.Text = "";
                        txtRemarks.Text = "";
                        txtStartTime.Text = "";
                        txtSemiID.Text = "";
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

        protected void btnDlt_Click(object sender, System.EventArgs e)
        {
            try
            {

                if (con.State != ConnectionState.Open)con.Open();
                SqlCommand cmd = new SqlCommand("delete from EIM_SEMESTER where SEMESTERID = '" + txtSemiID.Text + "'", con);
                cmd.ExecuteNonQuery();
                if (con.State != ConnectionState.Closed)con.Close();
                //lblMSG.Visible = true;
                txtSemester.Text = "";
                txtRemarks.Text = "";
                txtStartTime.Text = "";
                txtSemiID.Text = "";
                //Response.Write("<script>alert('Successed !, either Debit Amount or Credit Amount');</script>");
                lblMSG.Visible = true;
                lblMSG.Text = "Deleted !";
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }
    }
}