using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting.Admission.DataAccess;
using AlchemyAccounting.Admission.DataModel;

namespace AlchemyAccounting.Admission.UI
{
    public partial class receivable : System.Web.UI.Page
    {
        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection conn = new SqlConnection(Global.connection);
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {

                    txtDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                    txtYR.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    string yr = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    int i, m;
                    int a = int.Parse(yr);
                    m = a + 5;
                    for (i = a - 5; i <= m; i++)
                    {
                        ddlRegYR.Items.Add(i.ToString());
                    }
                    ddlRegYR.Text = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                    Session["YEAR"] = ddlRegYR.Text;
                    Global.DropDownAddAllTextWithValue(ddlSemNM, "SELECT SEMESTERNM,SEMESTERID FROM EIM_SEMESTER");
                    Global.DropDownAddAllTextWithValue(ddlProgNM, "SELECT PROGRAMNM,PROGRAMID FROM EIM_PROGRAM");
                    Global.dropDownAdd(ddlBatch, "SELECT DISTINCT BATCH FROM EIM_STUDENT ORDER BY BATCH");
                    Global.dropDownAddWithSelect(ddlFeesForRec, "SELECT DISTINCT FEESNM FROM EIM_FEES WHERE FEESID BETWEEN 101 AND 112  ORDER BY FEESNM");
                    Global.dropDownAddWithSelect(ddlFeesForPay, "SELECT DISTINCT FEESNM FROM EIM_FEES WHERE FEESID BETWEEN 113 AND 118  ORDER BY FEESNM");
                    gridShowIDCreate();
                    ddlTransFor.Focus();

                }
            }
        }
        private void gridShowIDCreate()
        {
            Global.txtAdd("SELECT MAX(TRANSNO) FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='JOUR' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'", txtTransNO);
            if (txtTransNO.Text == "")
            {
                txtTransNO.Text = "1";

            }
            else
            {
                int TRNO = int.Parse(txtTransNO.Text) + 1;
                txtTransNO.Text = TRNO.ToString();
            }
            gridShow();
            ddlSemNM.Focus();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionStudentID(string prefixText, int count, string contextKey)
        {

            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection
            string PROGRAMID = HttpContext.Current.Session["PROGRAMID"].ToString();
            string SemID = HttpContext.Current.Session["SEMESTERID"].ToString();
            string YEAR = HttpContext.Current.Session["YEAR"].ToString();
            SqlCommand cmd = new SqlCommand("SELECT NEWSTUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID LIKE '" + prefixText + "%' AND PROGRAMID='" + PROGRAMID + "' AND SEMESTERID='" + SemID + "' AND ADMITYY='" + YEAR + "'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["NEWSTUDENTID"].ToString());


            }
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();


        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionFEESNM(string prefixText, int count, string contextKey)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT FEESNM FROM EIM_FEES WHERE FEESNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["FEESNM"].ToString());
            }
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }
        private void gridShow()
        {
            string TransNO = "";
            if (btnEdit.Text == "EDIT")
                TransNO = txtTransNO.Text;
            else
                TransNO = ddlTransNO.Text;
            SqlConnection conn = new SqlConnection(Global.connection);
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT    EIM_TRANS.STUDENTID,EIM_STUDENT.NEWSTUDENTID, EIM_STUDENT.STUDENTNM, EIM_TRANS.AMOUNT, EIM_TRANS.REMARKS
                                              FROM         EIM_TRANS INNER JOIN
                                              EIM_STUDENT ON EIM_TRANS.STUDENTID = EIM_STUDENT.STUDENTID WHERE TRANSNO='" + TransNO + "' AND TRANSTP='JOUR' AND TRANSYY='" + txtYR.Text + "'AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "' ", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Trans.Visible = true;
                gv_Trans.DataSource = ds;
                gv_Trans.DataBind();
                TextBox txtSTUDENTIDFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtSTUDENTIDFooter");
                txtSTUDENTIDFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_Trans.DataSource = ds;
                gv_Trans.DataBind();
                int columncount = gv_Trans.Rows[0].Cells.Count;
                gv_Trans.Rows[0].Cells.Clear();
                gv_Trans.Rows[0].Cells.Add(new TableCell());
                gv_Trans.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_Trans.Rows[0].Visible = false;
                TextBox txtSTUDENTIDFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtSTUDENTIDFooter");
                txtSTUDENTIDFooter.Focus();
            }
        }
        protected void txtDT_TextChanged(object sender, EventArgs e)
        {
            string Date = txtDT.Text;
            txtYR.Text = Date.Substring(6, 4);
            if (btnEdit.Text == "EDIT")
            {
                gridShowIDCreate();
                ddlRegYR.Focus();
            }
            else
            {
                Global.dropDownAddTrans(ddlTransNO, "SELECT TRANSNO FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='JOUR' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'");

                ddlTransNO.Focus();
            }
        }
        private void clear()
        {
            ddlProgNM.SelectedIndex = -1;
            ddlSemNM.SelectedIndex = -1;
            txtRemarks.Text = ""; 
            ddlFeesForRec.SelectedIndex = -1;
            ddlFeesForPay.SelectedIndex = -1;
            lblFeesID.Text = "";
        }

        protected void gv_Trans_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                TextBox txtSTUDENTNMFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtSTUDENTNMFooter");
                TextBox txtAMOUNTFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtAMOUNTFooter");
                TextBox txtREMARKSFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtREMARKSFooter");
                TextBox txtSTUDENTIDFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtSTUDENTIDFooter");

                iob.UserID = Session["UserName"].ToString();
                iob.Ipaddress = Session["IpAddress"].ToString();
                iob.PcName = Session["PCName"].ToString();
                iob.InTime = Global.Dayformat1(DateTime.Now);
                iob.TransTP = "JOUR";
                if (txtDT.Text == "")
                {
                    txtDT.Focus();
                }
                else if (ddlRegYR.Text == "")
                {
                    ddlRegYR.Focus();
                }
                else if (ddlSemNM.Text == "Select")
                {
                    ddlSemNM.Focus();
                }
                else if (ddlProgNM.Text == "Select")
                {
                    ddlProgNM.Focus();
                }
                else if (ddlFeesForRec.Text == "Select" && ddlFeesForPay.Text == "Select")
                {
                    if (ddlTransFor.SelectedItem.ToString() == "RECEIVABLE")
                        ddlFeesForRec.Focus();
                    else
                        ddlFeesForPay.Focus();
                }
                else if (txtSTUDENTIDFooter.Text == "")
                {
                    txtSTUDENTIDFooter.Focus();
                }
                else
                {
                    if (conn.State != ConnectionState.Open)conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    if (btnEdit.Text == "EDIT")
                    {
                        //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'", txtInNo);
                        cmd = new SqlCommand("Select TRANSNO from EIM_TRANSMST where TRANSNO='" + txtTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='JOUR'  AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("Select TRANSNO from EIM_TRANSMST where TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='JOUR' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'", conn);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (conn.State != ConnectionState.Closed)conn.Close();
                    DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.TrnsDT = Date;
                    iob.TransYR = int.Parse(txtYR.Text);
                    iob.TransFor = ddlTransFor.SelectedItem.ToString();
                    iob.RegYR = int.Parse(ddlRegYR.Text);
                    iob.SemID = int.Parse(ddlSemNM.Text);
                    iob.ProgID = ddlProgNM.Text;
                    iob.FeesID = lblFeesID.Text;
                    iob.Remarks = txtRemarks.Text;
                    if (txtRemarks.Text == "")
                        iob.Remarks = "";
                    iob.StuID = txtSTUDENTIDFooter.Text;
                    if (txtAMOUNTFooter.Text == "")
                        txtAMOUNTFooter.Text = "0.00";
                    iob.Amnt = Decimal.Parse(txtAMOUNTFooter.Text);
                    iob.RemarksGRD = txtREMARKSFooter.Text;
                    if (txtREMARKSFooter.Text == "")
                        txtREMARKSFooter.Text = "";
                    if (e.CommandName.Equals("Add"))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (btnEdit.Text == "EDIT")
                            {
                                lblMSG.Visible = false;
                                iob.TrnsNO = int.Parse(txtTransNO.Text);
                                dob.Insert_Due_EIM_TRANS(iob);
                                gridShow();
                            }
                            else
                            {
                                iob.TrnsNO = int.Parse(ddlTransNO.Text);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    dob.Insert_Due_EIM_TRANS(iob);
                                    gridShow();
                                }
                                else
                                {
                                    lblMSG.Visible = true;
                                    lblMSG.Text = "Must be in New Mode.";
                                }

                            }
                        }
                        else
                        {
                            lblMSG.Visible = false;
                            Global.lblAdd("SELECT MAX(TRANSNO) FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='JOUR' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'", lblTransNO);
                            if (btnEdit.Text == "EDIT")
                            {
                                if (lblTransNO.Text == "")
                                    iob.TrnsNO = 1;
                                else
                                    iob.TrnsNO = int.Parse(lblTransNO.Text) + 1;
                            }
                            else
                                iob.TrnsNO = int.Parse(ddlTransNO.Text);
                            dob.Insert_Due_EIM_TRANSMST(iob);
                            dob.Insert_Due_EIM_TRANS(iob);
                            gridShow();
                        }
                    }
                    else if (e.CommandName.Equals("Complete"))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (btnEdit.Text == "EDIT")
                            {
                                lblMSG.Visible = false;
                                iob.TrnsNO = int.Parse(txtTransNO.Text);
                                dob.Insert_Due_EIM_TRANS(iob);
                                clear();
                                gridShowIDCreate();
                                ddlSemNM.Focus();
                            }
                            else
                            {
                                iob.TrnsNO = int.Parse(ddlTransNO.Text);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    dob.Insert_Due_EIM_TRANS(iob);
                                    clear();
                                    gridShow();
                                    ddlTransNO.Focus();
                                }
                                else
                                {
                                    lblMSG.Visible = true;
                                    lblMSG.Text = "Must be in New Mode.";
                                }

                            }
                        }
                        else
                        {
                            lblMSG.Visible = false;
                            Global.lblAdd("SELECT MAX(TRANSNO) FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='JOUR' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'", lblTransNO);
                            if (btnEdit.Text == "EDIT")
                            {
                                if (lblTransNO.Text == "")
                                    iob.TrnsNO = 1;
                                else
                                    iob.TrnsNO = int.Parse(lblTransNO.Text) + 1;
                            }
                            else
                                iob.TrnsNO = int.Parse(ddlTransNO.Text);
                            dob.Insert_Due_EIM_TRANSMST(iob);
                            dob.Insert_Due_EIM_TRANS(iob);
                            clear();
                            gridShowIDCreate();
                            ddlSemNM.Focus();
                        }
                    }
                    else if (e.CommandName.Equals("Print"))
                    {
                        Session["TRANSNO"] = "";
                        Session["TRANSYY"] = "";
                        Session["TRANSDT"] = "";
                        Session["SEMESTERNM"] = "";
                        Session["PROGRAMNM"] = "";
                        Session["FEESID"] = "";
                        Session["FEESNM"] = "";
                        Session["STUDENTID"] = "";
                        Session["STUDENTNM"] = "";
                        Session["REMARKS"] = "";

                        if (btnEdit.Text == "EDIT")
                            Session["TRANSNO"] = txtTransNO.Text;
                        else
                            Session["TRANSNO"] = ddlTransNO.Text;
                        Session["TRANSYY"] = txtYR.Text;
                        Session["TRANSDT"] = txtDT.Text;
                        Session["SEMESTERNM"] = ddlSemNM.Text;
                        Session["PROGRAMNM"] = ddlProgNM.Text;
                        Session["FEESID"] = lblFeesID.Text;
                        if (ddlTransFor.SelectedItem.ToString() == "RECEIVABLE")
                            Session["FEESNM"] = ddlFeesForRec.Text;
                        else
                            Session["FEESNM"] = ddlFeesForPay.Text;
                        Session["STUDENTID"] = txtSTUDENTIDFooter.Text;
                        Session["STUDENTNM"] = txtSTUDENTNMFooter.Text;
                        Session["REMARKS"] = txtRemarks.Text;
                        if (conn.State != ConnectionState.Open)conn.Open();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (btnEdit.Text == "EDIT")
                            {
                                Global.lblAdd("SELECT MAX(TRANSNO) FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='JOUR' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'", lblTransNO);
                                if (btnEdit.Text == "EDIT")
                                {
                                    if (lblTransNO.Text == "")
                                        iob.TrnsNO = 1;
                                    else
                                        iob.TrnsNO = int.Parse(lblTransNO.Text) + 1;
                                }
                                else
                                    iob.TrnsNO = int.Parse(ddlTransNO.Text);
                                dob.Insert_Due_EIM_TRANS(iob);
                                gridShowIDCreate();
                                clear();
                                ddlSemNM.Focus();
                            }
                            else
                            {
                                iob.TrnsNO = int.Parse(ddlTransNO.Text);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    dob.Insert_Due_EIM_TRANS(iob);
                                    clear();
                                    gridShow();
                                    ddlTransNO.Focus();
                                }
                                else
                                {
                                    lblMSG.Visible = true;
                                    lblMSG.Text = "Must be in New Mode.";
                                }

                            }
                        }
                        else
                        {
                            lblMSG.Visible = false;
                            Global.lblAdd("SELECT MAX(TRANSNO) FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "'  AND TRANSTP='JOUR' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'", lblTransNO);
                            if (btnEdit.Text == "EDIT")
                            {
                                if (lblTransNO.Text == "")
                                    iob.TrnsNO = 1;
                                else
                                    iob.TrnsNO = int.Parse(lblTransNO.Text) + 1;
                            }
                            else
                                iob.TrnsNO = int.Parse(ddlTransNO.Text);
                            dob.Insert_Due_EIM_TRANSMST(iob);
                            dob.Insert_Due_EIM_TRANS(iob);
                            gridShowIDCreate();
                            clear();
                            ddlSemNM.Focus();
                        }
                        if (btnEdit.Text == "EDIT")
                        {

                            gridShowIDCreate();
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "OpenWindow", "window.open('../Report/StudentLedger.aspx','_newtab');", true);
                        }
                        else
                        {
                            gridShow();
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                 "OpenWindow", "window.open('../Report/StudentLedger.aspx','_newtab');", true);
                        }
                    }
                }

            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }
        protected void ddlSemNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMSG.Text = "";
            if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
            }
            else
            { 
                Session["SEMESTERID"] = ddlSemNM.Text;
                ddlProgNM.Focus();
            }
        }

        protected void ddlProgNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
            }
            else
            {
                Session["PROGRAMID"] = ""; 
                Session["PROGRAMID"] = ddlProgNM.Text;
                Session["BATCH"] = ddlBatch.Text;
                string semid = Global.GetData("SELECT SEMESTERID FROM EIM_STUDENT WHERE BATCH='" + ddlBatch.Text + "' AND PROGRAMID='" + ddlProgNM.Text + "'");
                if (semid == "")
                    semid = "--SELECT--";
                ddlSemNM.Text = semid;
                Session["SEMESTERID"] = semid;
                if (ddlTransFor.SelectedItem.ToString() == "RECEIVABLE")
                    ddlFeesForRec.Focus();
                else
                    ddlFeesForPay.Focus();
            }
        }

        protected void ddlRegYR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegYR.Text != "")
            {
                ddlSemNM.Focus();
                Session["YEAR"] = ddlRegYR.Text;
            }

        }
        protected void gv_Trans_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string TransNO = "";
                if (btnEdit.Text == "EDIT")
                    TransNO = txtTransNO.Text;
                else
                    TransNO = ddlTransNO.Text;
                SqlCommand cmd = new SqlCommand("SELECT * FROM EIM_TRANS WHERE FEESID='" + lblFeesID.Text + "' AND TRANSYY= '" + txtYR.Text + "' AND TRANSTP='JOUR' AND TRANSNO = '" + TransNO + "' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (conn.State != ConnectionState.Closed)conn.Close();
                lblMSG.Visible = false;
                Label lblSTUDENTID = (Label)gv_Trans.Rows[e.RowIndex].FindControl("lblSTUDENTID");
                DateTime transdate = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                if (btnEdit.Text == "EDIT")
                {
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        string TrDate = transdate.ToString("yyyy/MM/dd");
                        if (conn.State != ConnectionState.Open)conn.Open();
                        SqlCommand cmd1 = new SqlCommand("DELETE FROM EIM_TRANS WHERE FEESID ='" + lblFeesID.Text + "' AND   TRANSYY = '" + txtYR.Text + "' AND TRANSNO='" + TransNO + "' AND TRANSTP='JOUR' AND  TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "' AND STUDENTID='" + lblSTUDENTID.Text + "'", conn);
                        cmd1.ExecuteNonQuery();
                        if (conn.State != ConnectionState.Closed)conn.Close();
                        gridShow();
                    }
                    else
                    {
                        if (conn.State != ConnectionState.Open)conn.Open();
                        SqlCommand cmd1 = new SqlCommand("DELETE FROM EIM_TRANS WHERE TRANSNO ='" + TransNO + "' AND FEESID ='" + lblFeesID.Text + "' AND TRANSTP='JOUR' AND TRANSYY = '" + txtYR.Text + "' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "' AND STUDENTID='" + lblSTUDENTID.Text + "'", conn);
                        cmd1.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("DELETE FROM EIM_TRANSMST WHERE TRANSNO ='" + TransNO + "'  AND TRANSTP='JOUR' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "' AND  TRANSYY='" + txtYR.Text + "'", conn);
                        cmd2.ExecuteNonQuery();
                        if (conn.State != ConnectionState.Closed)conn.Close();
                        gridShow();
                    }
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        lblMSG.Visible = false;
                        if (conn.State != ConnectionState.Open)conn.Open();
                        SqlCommand cmd1 = new SqlCommand("DELETE FROM EIM_TRANS WHERE FEESID ='" + lblFeesID.Text + "' AND TRANSTP='JOUR' AND TRANSYY = '" + txtYR.Text + "' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "' AND TRANSNO='" + TransNO + "' AND STUDENTID='" + lblSTUDENTID.Text + "'", conn);
                        cmd1.ExecuteNonQuery();
                        if (conn.State != ConnectionState.Closed)conn.Close();
                        gridShow();
                        ddlSemNM.Focus();
                    }
                    else
                    {
                        lblMSG.Visible = false;
                        if (conn.State != ConnectionState.Open)conn.Open();
                        SqlCommand cmd1 = new SqlCommand("DELETE FROM EIM_TRANS WHERE TRANSNO ='" + TransNO + "' AND FEESID ='" + lblFeesID.Text + "' AND TRANSTP='JOUR' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "' AND TRANSYY = '" + txtYR.Text + "' AND STUDENTID='" + lblSTUDENTID.Text + "'", conn);
                        cmd1.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("DELETE FROM EIM_TRANSMST WHERE TRANSNO ='" + TransNO + "'  AND TRANSTP='JOUR' AND TRANSYY='" + txtYR.Text + "' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'", conn);
                        cmd2.ExecuteNonQuery();
                        if (conn.State != ConnectionState.Closed)conn.Close();
                        ddlTransNO.SelectedIndex = -1;
                        ddlTransNO.Focus();
                        clear();
                        gridShow();
                        Global.dropDownAddTrans(ddlTransNO, "SELECT TRANSNO FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='JOUR'");
                        ddlTransNO.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            lblMSG.Text = "";
            if (btnEdit.Text == "EDIT")
            {
               btnComplete0.Visible = true;
                btnEdit.Text = "NEW";
                txtTransNO.Visible = false;
                ddlTransNO.Visible = true;
                // btnComplete.Visible = false;
                string Date = txtDT.Text;
                txtYR.Text = Date.Substring(6, 4);
                Global.dropDownAddTrans(ddlTransNO, "SELECT DISTINCT TRANSNO FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='JOUR' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'");
                clear();
                gridShow();
                ddlTransFor.Focus();
                ddlRegYR.Enabled = false;


            }
            else
            {
                btnComplete0.Visible = false;
                gv_Trans.Visible = true;
                ddlRegYR.Enabled = true;
                btnEdit.Text = "EDIT";
                txtTransNO.Visible = true;
                ddlTransNO.Visible = false;
                clear();
                Global.dropDownAddWithSelect(ddlFeesForRec, "SELECT DISTINCT FEESNM FROM EIM_FEES WHERE FEESID BETWEEN 101 AND 112  ORDER BY FEESID");
                Global.dropDownAddWithSelect(ddlFeesForPay, "SELECT DISTINCT FEESNM FROM EIM_FEES WHERE FEESID BETWEEN 113 AND 118  ORDER BY FEESID");
                gridShowIDCreate();
                string Date = txtDT.Text;
                txtYR.Text = Date.Substring(6, 4);
                ddlTransFor.Focus();
            }
        }

        protected void ddlTransNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransNO.Text == "")
            {
                clear();
                gv_Trans.Visible = false;
                ddlTransNO.Focus();
            }
            else
            {
                Session["PROGRAMID"] = "";
                clear();
                gv_Trans.Visible = true;
                if (conn.State != ConnectionState.Open)conn.Open();
                string Script = @"SELECT DISTINCT CONVERT(NVARCHAR(10), EIM_TRANSMST.TRANSDT, 103) AS TRANSDT, EIM_TRANSMST.TRANSFOR, EIM_TRANSMST.REGYY, EIM_TRANSMST.SEMESTERID, EIM_FEES.FEESID, EIM_TRANSMST.PROGRAMID, 
                         EIM_FEES.FEESNM, EIM_TRANSMST.REMARKS, EIM_STUDENT.BATCH
FROM            EIM_TRANSMST INNER JOIN
                         EIM_TRANS ON EIM_TRANSMST.TRANSNO = EIM_TRANS.TRANSNO AND EIM_TRANSMST.TRANSTP = EIM_TRANS.TRANSTP AND EIM_TRANSMST.TRANSDT = EIM_TRANS.TRANSDT INNER JOIN
                         EIM_STUDENT ON EIM_TRANS.STUDENTID = EIM_STUDENT.STUDENTID LEFT OUTER JOIN
                         EIM_FEES ON EIM_TRANS.FEESID = EIM_FEES.FEESID WHERE EIM_TRANS.TRANSNO='" + ddlTransNO.Text + "' AND EIM_TRANS.TRANSTP='JOUR' and EIM_TRANS.TRANSYY='" + txtYR.Text + "' AND EIM_TRANSMST.TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'";
                SqlCommand cmd = new SqlCommand(Script, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDT.Text = dr["TRANSDT"].ToString();
                    //ddlSemNM.Text = dr["SEMESTERNM"].ToString();
                    ddlRegYR.Text = dr["REGYY"].ToString();
                    ddlSemNM.Text = dr["SEMESTERID"].ToString();
                     ddlBatch.Text = dr["BATCH"].ToString();
                    ddlProgNM.Text = dr["PROGRAMID"].ToString();
                    if (ddlTransFor.SelectedItem.ToString() == "RECEIVABLE")
                        Global.dropDownAdd_GridEditMode(ddlFeesForRec, "SELECT DISTINCT FEESNM FROM EIM_FEES WHERE FEESID BETWEEN 101 AND 112 ORDER BY FEESNM", dr["FEESNM"].ToString());
                    else
                        Global.dropDownAdd_GridEditMode(ddlFeesForPay, "SELECT DISTINCT FEESNM FROM EIM_FEES WHERE FEESID BETWEEN 113 AND 118  ORDER BY FEESNM", dr["FEESNM"].ToString());
                    lblFeesID.Text = dr["FEESID"].ToString();
                    txtRemarks.Text = dr["REMARKS"].ToString();
                    Session["PROGRAMID"] = ddlProgNM.Text;

                    Session["SEMESTERID"] = ddlSemNM.Text;
               
                }
                if (conn.State != ConnectionState.Closed)conn.Close();
                dr.Close();
                gridShow();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

            int Count = gv_Trans.Rows.Count;

            if (txtDT.Text == "")
            {
                txtDT.Focus();
            }
            else if (ddlRegYR.Text == "Select")
            {
                ddlRegYR.Focus();
            }
            else if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
            }
            else if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
            }
            else if (lblFeesID.Text == "")
            {
                if (ddlTransFor.SelectedItem.ToString() == "RECEIVABLE")
                    ddlFeesForRec.Focus();
                else
                    ddlFeesForPay.Focus();
            }
            else
            {
                Session["TRANSNO"] = "";
                Session["TRANSFOR"] = "";
                Session["TRANSYY"] = "";
                Session["TRANSDT"] = "";
                Session["SEMESTERNM"] = "";
                Session["PROGRAMNM"] = "";
                Session["REMARKS"] = "";
                Session["FEESNM"] = "";
                Session["FEESID"] = "";


                if (btnEdit.Text == "EDIT")
                    Session["TRANSNO"] = txtTransNO.Text;
                else
                    Session["TRANSNO"] = ddlTransNO.Text;
                Session["TRANSFOR"] = ddlTransFor.SelectedItem.ToString();
                Session["TRANSYY"] = txtYR.Text;
                Session["TRANSDT"] = txtDT.Text;
                Session["SEMESTERNM"] = ddlSemNM.Text;
                Session["PROGRAMNM"] = ddlProgNM.Text;
                Session["REMARKS"] = txtRemarks.Text;
                if (ddlTransFor.SelectedItem.ToString() == "RECEIVABLE")
                    Session["FEESNM"] = ddlFeesForRec.Text;
                else
                    Session["FEESNM"] = ddlFeesForPay.Text;
                Session["FEESID"] = lblFeesID.Text;
                clear();
                if (btnEdit.Text == "EDIT")
                {
                    gridShowIDCreate();
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "OpenWindow", "window.open('../Report/DueReport.aspx','_newtab');", true);
                }
                else
                {
                    gridShow();
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                        "OpenWindow", "window.open('../Report/DueReport.aspx','_newtab');", true);
                }
            }

        }
        protected void btnComplete_Click(object sender, EventArgs e)
        {
            if (txtDT.Text == "")
            {
                txtDT.Focus();
            }
            else if (ddlRegYR.Text == "")
            {
                ddlRegYR.Focus();
            }
            else if (ddlSemNM.Text == "Select")
            {
                ddlSemNM.Focus();
            }
            else if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
            }
            else if (lblFeesID.Text == "")
            {
                if (ddlTransFor.SelectedItem.ToString() == "RECEIVABLE")
                    ddlFeesForRec.Focus();
                else
                    ddlFeesForPay.Focus();
            }
            else
            {
                try
                {
                    iob.UPDUserID = Session["UserName"].ToString();
                    iob.UPDIpaddress = Session["IpAddress"].ToString();
                    iob.UPDPcName = Session["PCName"].ToString();
                    iob.UPDTime = Global.Dayformat1(DateTime.Now);
                    DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    // iob.FeesID = lblFEESIDEdit.Text; 

                    iob.TransFor = ddlTransFor.SelectedItem.ToString();
                    iob.TrnsDT = Date;
                    iob.TransYR = int.Parse(txtYR.Text);
                    iob.RegYR = int.Parse(ddlRegYR.Text);
                    iob.SemID = int.Parse(ddlSemNM.Text);
                    iob.ProgID = ddlProgNM.Text;
                    iob.Remarks = txtRemarks.Text;
                    if (txtRemarks.Text == "")
                        iob.Remarks = "";
                    iob.FeesID = lblFeesID.Text;
                    if (btnEdit.Text == "EDIT")
                        iob.TrnsNO = int.Parse(txtTransNO.Text);
                    else
                        iob.TrnsNO = int.Parse(ddlTransNO.Text);
                    string s = dob.Update_Due_EIM_TRANSMST_TOP(iob);
                    string x = dob.Update_Due_EIM_TRANSMST_DETAILS(iob);
                    if (s == "true")
                    {
                        lblError.Visible = false;
                        lblError.Text = "Successfully Updated !";
                    }
                    else
                    {
                        lblError.Visible = false;
                        lblError.Text = "Failed. Try Again !";
                    }
                }
                catch { }
            }

        }

        protected void gv_Trans_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (btnEdit.Text == "EDIT")
            {
                gv_Trans.EditIndex = -1;
                gridShow();
            }
            else
            {
                gv_Trans.EditIndex = -1;
                gridShow();
            }
        }
        protected void gv_Trans_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                Session["STUDENTID"] = "";
                Label lblSTUDENTID = (Label)gv_Trans.Rows[e.NewEditIndex].FindControl("lblSTUDENTID");
                Session["STUDENTID"] = lblSTUDENTID.Text;
                if (btnEdit.Text == "EDIT")
                {
                    gv_Trans.EditIndex = e.NewEditIndex;
                    gridShow();
                }
                else
                {
                    gv_Trans.EditIndex = e.NewEditIndex;
                    gridShow();
                }

                TextBox txtAMOUNTEdit = (TextBox)gv_Trans.Rows[e.NewEditIndex].FindControl("txtAMOUNTEdit");
                txtAMOUNTEdit.Focus();
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

        protected void gv_Trans_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                // TextBox txtItemNMEdit_P = (TextBox)gvPurchase.Rows[e.RowIndex].FindControl("txtItemNMEdit_P");
                TextBox txtSTUDENTNMEdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtSTUDENTNMEdit");
                TextBox txtAMOUNTEdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtAMOUNTEdit");
                TextBox txtREMARKSEdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                TextBox txtSTUDENTIDEdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtSTUDENTIDEdit");
                //Label lblFEESID = (Label)gv_Trans.Rows[e.RowIndex].FindControl("lblFEESID");
                iob.UPDUserID = Session["UserName"].ToString();
                iob.UPDPcName = Session["PCName"].ToString();
                iob.UPDIpaddress = Session["IpAddress"].ToString();
                iob.UPDTime = Global.Dayformat1(DateTime.Now);
                if (txtDT.Text == "")
                {
                    txtDT.Focus();
                }
                else if (ddlRegYR.Text == "")
                {
                    ddlRegYR.Focus();
                }
                else if (ddlSemNM.Text == "Select")
                {
                    ddlSemNM.Focus();
                }
                else if (ddlProgNM.Text == "Select")
                {
                    ddlProgNM.Focus();
                }
                else if (lblFeesID.Text == "")
                {
                    if (ddlTransFor.SelectedItem.ToString() == "RECEIVABLE")
                        ddlFeesForRec.Focus();
                    else
                        ddlFeesForPay.Focus();
                }
                else if (txtSTUDENTIDEdit.Text == "")
                {
                    txtSTUDENTNMEdit.Focus();
                }
                else
                {
                    DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                    // iob.FeesID = lblFEESIDEdit.Text; 
                    iob.TransFor = ddlTransFor.SelectedItem.ToString();
                    iob.TrnsDT = Date;
                    iob.TransYR = int.Parse(txtYR.Text);
                    iob.RegYR = int.Parse(ddlRegYR.Text);
                    iob.SemID = int.Parse(ddlSemNM.Text);
                    iob.ProgID = ddlProgNM.Text;
                    iob.StuID = txtSTUDENTIDEdit.Text;
                    iob.Remarks = txtRemarks.Text;
                    if (txtRemarks.Text == "")
                        iob.Remarks = "";
                    iob.FeesID = lblFeesID.Text;
                    if (txtAMOUNTEdit.Text == "")
                        txtAMOUNTEdit.Text = "0.00";
                    iob.Amnt = Decimal.Parse(txtAMOUNTEdit.Text);
                    iob.RemarksGRD = txtREMARKSEdit.Text;
                    if (txtREMARKSEdit.Text == "")
                        txtREMARKSEdit.Text = "";
                    if (btnEdit.Text == "EDIT")
                    {
                        iob.TrnsNO = int.Parse(txtTransNO.Text);
                        dob.Update_Due_EIM_TRANS(iob);
                        dob.Update_Due_EIM_TRANSMST(iob);
                        gv_Trans.EditIndex = -1;
                        gridShow();
                    }
                    else
                    {
                        iob.TrnsNO = int.Parse(ddlTransNO.Text);
                        dob.Update_Due_EIM_TRANS(iob);
                        dob.Update_Due_EIM_TRANSMST(iob);
                        gv_Trans.EditIndex = -1;
                        gridShow();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
        protected void txtSTUDENTIDNewFooter_TextChanged(object sender, EventArgs e)
        {

            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtSTUDENTNMFooter = (TextBox)row.FindControl("txtSTUDENTNMFooter");
            TextBox txtSTUDENTIDFooter = (TextBox)row.FindControl("txtSTUDENTIDFooter");
            TextBox txtSTUDENTIDNewFooter = (TextBox)row.FindControl("txtSTUDENTIDNewFooter");
            txtSTUDENTIDFooter.Text = Global.GetData("SELECT STUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtSTUDENTIDNewFooter.Text + "'");
            TextBox txtAMOUNTFooter = (TextBox)row.FindControl("txtAMOUNTFooter");
            if (txtSTUDENTIDFooter.Text != "")
            {
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE STUDENTID='" + txtSTUDENTIDFooter.Text + "'", txtSTUDENTNMFooter);
                txtAMOUNTFooter.Focus();
            }
            else
                txtSTUDENTIDFooter.Focus();
        }

        protected void txtSTUDENTIDNewEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtSTUDENTNMEdit = (TextBox)row.FindControl("txtSTUDENTNMEdit");
            TextBox txtSTUDENTIDEdit = (TextBox)row.FindControl("txtSTUDENTIDEdit");
            TextBox txtSTUDENTIDNewEdit = (TextBox)row.FindControl("txtSTUDENTIDNewEdit");
            txtSTUDENTIDEdit.Text = Global.GetData("SELECT STUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtSTUDENTIDNewEdit.Text + "'");
            TextBox txtAMOUNTEdit = (TextBox)row.FindControl("txtAMOUNTEdit");
            if (txtSTUDENTIDEdit.Text != "")
            {
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE STUDENTID='" + txtSTUDENTIDEdit.Text + "'", txtSTUDENTNMEdit);
                txtAMOUNTEdit.Focus();
            }
            else
                txtSTUDENTIDEdit.Focus();
        }

        protected void ddlTransFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransFor.SelectedItem.ToString() == "RECEIVABLE")
            {
                ddlFeesForRec.Visible = true;
                ddlFeesForPay.Visible = false;
                gridShowIDCreate();
                if (btnEdit.Text == "Edit")
                    ddlRegYR.Focus();
                else
                {
                    Global.dropDownAddTrans(ddlTransNO, "SELECT DISTINCT TRANSNO FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='JOUR'  AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'");
                    ddlTransNO.Focus();
                }
            }
            else
            {
                ddlFeesForRec.Visible = false;
                ddlFeesForPay.Visible = true;
                gridShowIDCreate();
                if (btnEdit.Text == "Edit")
                    ddlRegYR.Focus();
                else
                {
                    Global.dropDownAddTrans(ddlTransNO, "SELECT TRANSNO FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='JOUR' AND TRANSFOR='" + ddlTransFor.SelectedItem.ToString() + "'");
                    ddlTransNO.Focus();
                }
            }
        }

        protected void ddlFeesForRec_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridShow();
            Global.lblAdd("SELECT FEESID FROM EIM_FEES WHERE FEESNM='" + ddlFeesForRec.Text + "'", lblFeesID);
            txtRemarks.Focus();
        }

        protected void ddlFeesForPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridShow();
            Global.lblAdd("SELECT FEESID FROM EIM_FEES WHERE FEESNM='" + ddlFeesForPay.Text + "'", lblFeesID);
            txtRemarks.Focus();
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProgNM.SelectedIndex = - 1;
        }
    }
}
