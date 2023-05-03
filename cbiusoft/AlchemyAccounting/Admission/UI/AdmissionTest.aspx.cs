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
    public partial class AdmissionTest : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection conn = new SqlConnection(Global.connection);
        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();

       
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
                    Global.dropDownAdd(ddlSemis, "SELECT SEMESTERNM FROM EIM_SEMESTER");
                    ddlSemis.SelectedIndex = -1;
                    string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    int i, m;
                    int a = int.Parse(yr);
                    m = a + 5;
                    for (i = a - 5; i <= m; i++)
                    {
                        ddlYear.Items.Add(i.ToString());
                    }
                    ddlYear.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    ddlSemis.Focus();
                }
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
        private void gridShow()
        {

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT EIM_ADMTEST.SEMESTERID, EIM_ADMTEST.PROGRAMTP, EIM_ADMTEST.PROGRAMID,convert(nvarchar(10),EIM_ADMTEST.TESTDT,103) as TESTDT,
                                                                 EIM_ADMTEST.VENUE, EIM_ADMTEST.TESTTM, EIM_ADMTEST.REMARKS, EIM_PROGRAM.PROGRAMNM
                                           FROM   EIM_ADMTEST 
                                           INNER JOIN
                                           EIM_PROGRAM ON EIM_ADMTEST.PROGRAMID = EIM_PROGRAM.PROGRAMID 
                                           WHERE SEMESTERID='" + lblSemID.Text + "' and TESTYY='" + ddlYear.Text + "'", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvInfo.DataSource = ds;
                gvInfo.DataBind();

                TextBox txtPROGRAMNMfooter = (TextBox)gvInfo.FooterRow.FindControl("txtPROGRAMNMfooter");
                txtPROGRAMNMfooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvInfo.DataSource = ds;
                gvInfo.DataBind();
                int columncount = gvInfo.Rows[0].Cells.Count;
                gvInfo.Rows[0].Cells.Clear();
                gvInfo.Rows[0].Cells.Add(new TableCell());
                gvInfo.Rows[0].Cells[0].ColumnSpan = columncount;
                gvInfo.Rows[0].Visible = false;
                TextBox txtPROGRAMNMfooter = (TextBox)gvInfo.FooterRow.FindControl("txtPROGRAMNMfooter");
                txtPROGRAMNMfooter.Focus();
            }
        }

        protected void ddlSemis_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMSG1.Visible = false;
            if (ddlSemis.Text == "Select")
            {
                gvInfo.Visible = false;
                ddlSemis.Focus();
            }
            else
            {

                Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemis.Text + "'", lblSemID);
                gvInfo.Visible = true;
                gridShow();

            }

        }

        protected void gvInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            gvInfo.EditIndex = -1;
            gridShow();

        }

        protected void gvInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblPROGRAMID = (Label)gvInfo.Rows[e.RowIndex].FindControl("lblPROGRAMID");
                if (conn.State != ConnectionState.Open)conn.Open();
                SqlCommand cmd = new SqlCommand("Delete from EIM_ADMTEST where PROGRAMID= '" + lblPROGRAMID.Text + "'", conn);
                cmd.ExecuteNonQuery();
                if (conn.State != ConnectionState.Closed)conn.Close();
                gridShow();
                TextBox txtPROGRAMNMfooter = (TextBox)gvInfo.FooterRow.FindControl("txtPROGRAMNMfooter");
                txtPROGRAMNMfooter.Focus();
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void gvInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblMSG1.Text = "";
            try
            {
                if (Session["UserName"]==null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    TextBox txtPROGRAMTPedit = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtPROGRAMTPedit");
                    Label lblPROGRAMID = (Label)gvInfo.Rows[e.RowIndex].FindControl("lblPROGRAMID");
                    Label lblPROGRAMTP = (Label)gvInfo.Rows[e.RowIndex].FindControl("lblPROGRAMTP");
                    TextBox txtTESTDTEdit = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtTESTDTEdit");
                    TextBox txtTESTTMedit = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtTESTTMedit");
                    TextBox txtVENUEedit = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtVENUEedit");
                    TextBox txtREMARKSedit = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtREMARKSedit");
                    TextBox txtPROGRAMNMedit = (TextBox)gvInfo.Rows[e.RowIndex].FindControl("txtPROGRAMNMedit");
                    if (lblPROGRAMID.Text == "")
                    {
                        txtPROGRAMNMedit.Focus();
                    }
                    else if (txtTESTDTEdit.Text == "")
                    {
                        txtTESTDTEdit.Focus();
                    }
                    else if (txtTESTTMedit.Text == "")
                    {
                        txtTESTTMedit.Focus();
                    }
                    else if (txtVENUEedit.Text == "")
                    {
                        txtVENUEedit.Focus();
                    }
                    else
                    {
                        DateTime Date = DateTime.Parse(txtTESTDTEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.UPDUserID = Session["UserName"].ToString();
                        iob.UPDIpaddress = Session["IpAddress"].ToString();
                        iob.UPDPcName = Session["PCName"].ToString();
                        iob.UPDTime = Global.Dayformat1(DateTime.Now);
                        if (conn.State != ConnectionState.Open)conn.Open();
                        iob.ExamYr = ddlYear.Text;
                        iob.SemID = int.Parse(lblSemID.Text);
                        iob.ProgID = lblPROGRAMID.Text;
                        iob.ProgTP = lblPROGRAMTP.Text;
                        iob.ExamDT = Date;
                        iob.ExamTM = txtTESTTMedit.Text;
                        iob.ExamVenu = txtVENUEedit.Text;
                        iob.Remarks = txtREMARKSedit.Text;
                        dob.UpdateAdmissionTest(iob);
                        if (conn.State != ConnectionState.Closed)conn.Close();
                        gvInfo.EditIndex = -1;
                        gridShow();
                        lblMSG1.Visible = true;
                        lblMSG1.Text = "Updated !";
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }

        }


        protected void gvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                TextBox txtPROGRAMNMfooter = (TextBox)gvInfo.FooterRow.FindControl("txtPROGRAMNMfooter");
                Label lblPROGRAMIDfooter = (Label)gvInfo.FooterRow.FindControl("lblPROGRAMIDfooter");
                Label lblPROGRAMTPfooter = (Label)gvInfo.FooterRow.FindControl("lblPROGRAMTPfooter");
                TextBox txtTESTDTfooter = (TextBox)gvInfo.FooterRow.FindControl("txtTESTDTfooter");
                TextBox txtTESTTMfooter = (TextBox)gvInfo.FooterRow.FindControl("txtTESTTMfooter");
                TextBox txtVENUEfooter = (TextBox)gvInfo.FooterRow.FindControl("txtVENUEfooter");
                TextBox txtREMARKSfooter = (TextBox)gvInfo.FooterRow.FindControl("txtREMARKSfooter");
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

                    if (e.CommandName.Equals("SaveCon"))
                    {
                        if (ddlSemis.Text == "Select")
                        {
                            ddlSemis.Focus();
                        }
                        else if (txtPROGRAMNMfooter.Text == "")
                        {
                            txtPROGRAMNMfooter.Focus();
                        }
                        else if (txtTESTDTfooter.Text == "")
                        {
                            txtTESTDTfooter.Focus();
                        }
                        else if (txtTESTTMfooter.Text == "")
                        {
                            txtTESTTMfooter.Focus();
                        }
                        else if (txtVENUEfooter.Text == "")
                        {
                            txtVENUEfooter.Focus();
                        }
                        else
                        {
                            if (conn.State != ConnectionState.Open)conn.Open();
                            string Search = "SELECT * FROM EIM_ADMTEST WHERE PROGRAMID='" + lblPROGRAMIDfooter.Text + "' and TESTYY='" + ddlYear.Text + "' and SEMESTERID='" + lblSemID.Text + "'";
                            SqlCommand cmd = new SqlCommand(Search, conn);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (conn.State != ConnectionState.Closed)conn.Close();


                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblMSG.Visible = true;
                                lblMSG.Text = "Already Exist !";
                                txtPROGRAMNMfooter.Focus();
                            }
                            else
                            {

                                DateTime Date = DateTime.Parse(txtTESTDTfooter.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                                lblMSG.Visible = false;

                                iob.ExamYr = ddlYear.Text;
                                iob.SemID = int.Parse(lblSemID.Text);

                                iob.ProgID = lblPROGRAMIDfooter.Text;
                                iob.ProgTP = lblPROGRAMTPfooter.Text;
                                iob.ExamDT = Date;
                                iob.ExamTM = txtTESTTMfooter.Text;
                                iob.ExamVenu = txtVENUEfooter.Text;
                                iob.Remarks = txtREMARKSfooter.Text;
                                dob.InsertAdmissionTest(iob);
                                gridShow();

                            }

                        }
                    }
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void txtPROGRAMNMfooter_TextChanged(object sender, EventArgs e)
        {
            Label lblPROGRAMTPfooter = (Label)gvInfo.FooterRow.FindControl("lblPROGRAMTPfooter");
            Label lblPROGRAMIDfooter = (Label)gvInfo.FooterRow.FindControl("lblPROGRAMIDfooter");
            TextBox txtPROGRAMNMfooter = (TextBox)gvInfo.FooterRow.FindControl("txtPROGRAMNMfooter");
            Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtPROGRAMNMfooter.Text + "'", lblPROGRAMIDfooter);
            Global.lblAdd("SELECT PROGRAMTP FROM EIM_PROGRAM WHERE PROGRAMID='" + lblPROGRAMIDfooter.Text + "'", lblPROGRAMTPfooter);
            TextBox txtTESTDTfooter = (TextBox)gvInfo.FooterRow.FindControl("txtTESTDTfooter");
            txtTESTDTfooter.Focus();
        }

        protected void gvInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvInfo.EditIndex = e.NewEditIndex;
            gridShow();

            TextBox txtPROGRAMNMedit = (TextBox)gvInfo.Rows[e.NewEditIndex].FindControl("txtPROGRAMNMedit");
            txtPROGRAMNMedit.Focus();
        }

        protected void txtPROGRAMNMedit_TextChanged(object sender, EventArgs e)
        {

            GridViewRow Row = ((GridViewRow)((TextBox)sender).NamingContainer);
            Label lblPROGRAMTP = (Label)Row.FindControl("lblPROGRAMTP");

            Label lblPROGRAMID = (Label)Row.FindControl("lblPROGRAMID");
            TextBox txtPROGRAMNMedit = (TextBox)Row.FindControl("txtPROGRAMNMedit");
            Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + txtPROGRAMNMedit.Text + "'", lblPROGRAMID);
            Global.lblAdd("SELECT PROGRAMTP FROM EIM_PROGRAM WHERE PROGRAMID='" + lblPROGRAMID.Text + "'", lblPROGRAMTP);
            TextBox txtTESTDTEdit = (TextBox)Row.FindControl("txtTESTDTEdit");
            txtTESTDTEdit.Focus();

        }

        protected void txtTESTDTfooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtTESTTMfooter = (TextBox)gvInfo.FooterRow.FindControl("txtTESTTMfooter");
            txtTESTTMfooter.Focus();

        }

        protected void txtTESTTMfooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtVENUEfooter = (TextBox)gvInfo.FooterRow.FindControl("txtVENUEfooter");
            TextBox txtTESTTMfooter = (TextBox)gvInfo.FooterRow.FindControl("txtTESTTMfooter");

            if (txtTESTTMfooter.Text != "")
            {
                txtVENUEfooter.Focus();
            }
            else
            {
                txtTESTTMfooter.Focus();
            }
        }

    }
}