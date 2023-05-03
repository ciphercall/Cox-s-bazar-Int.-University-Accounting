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
    public partial class Collection_Old : System.Web.UI.Page
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
                    Global.dropDownAdd(ddlSemNM, "SELECT SEMESTERNM FROM EIM_SEMESTER");
                    Global.dropDownAdd(ddlProgNM, "SELECT PROGRAMNM FROM EIM_PROGRAM");
                    gridShowIDCreate();

                }
            }
        }
        private void LogData(string Script, string Table, string type)
        {
            //LogInsert Start  
            //            Label lblItemIDEdit = (Label)row.FindControl("lblItemIDEdit");
            //            Label lblDescript = new Label();
            //            Global.lblAdd(@"SELECT MSTID+'  '+SUBID+'  '+ITEMID+'  '+ITEMNM+'  '+CONVERT(NVARCHAR(50),RATE,103)+'  '+CONVERT(NVARCHAR(50),QTYM,103)+'  '+PSID+'  '+
            //                    ISNULL(USERPC,'(NULL)')+'  '+ISNULL(USERID,'(NULL)')+'  '+CONVERT(NVARCHAR(50),INTIME,103)+'  '+
            //                    ISNULL(CONVERT(NVARCHAR(50),UPDATETIME,103),'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)') FROM STK_ITEM
            //                    WHERE ITEMID ='" + lblItemIDEdit.Text + "'", lblDescript);
            //            iob.TableID = "STK_ITEM";
            //            iob.Type = "UPDATE";
            //            iob.DescrIP = lblDescript.Text;
            //            dob.INSERT_EST_DELETE(iob);
            //LogInsert End 
        }
        private void gridShowIDCreate()
        {
            Global.txtAdd("SELECT MAX(TRANSNO) FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", txtTransNO);
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
            SqlCommand cmd = new SqlCommand("SELECT STUDENTID FROM EIM_STUDENT WHERE STUDENTID LIKE '" + prefixText + "%' AND PROGRAMID='" + PROGRAMID + "'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["STUDENTID"].ToString());


            }
            return CompletionSet.ToArray();


        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionAccNM(string prefixText, int count, string contextKey)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection

            //string PROGRAMID = HttpContext.Current.Session["PROGRAMID"].ToString();
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTNM LIKE '" + prefixText + "%' AND SUBSTRING(ACCOUNTCD,1,5)='10201' AND LEVELCD='5'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            }
            return CompletionSet.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionFEESNM(string prefixText, int count, string contextKey)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT FEESNM FROM EIM_FEES WHERE FEESNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["FEESNM"].ToString());
            }
            return CompletionSet.ToArray();
        }
        private void gridShow()
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  EIM_FEES.FEESNM,EIM_TRANS.FEESID,EIM_TRANS.WAIVER+EIM_TRANS.SCHOLAR+EIM_TRANS.AMOUNT PREAMOUNT, EIM_TRANS.WAIVER, EIM_TRANS.SCHOLAR, EIM_TRANS.AMOUNT, EIM_TRANS.REMARKS
            FROM  EIM_FEES INNER JOIN
            EIM_TRANS ON EIM_FEES.FEESID = EIM_TRANS.FEESID WHERE TRANSNO='" + txtTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Trans.DataSource = ds;
                gv_Trans.DataBind();

                TextBox txtFEESNMFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtFEESNMFooter");
                txtFEESNMFooter.Focus();
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
                TextBox txtFEESNMFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtFEESNMFooter");
                txtFEESNMFooter.Focus();
            }
        }
        private void gridShowEdit()
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  EIM_FEES.FEESNM,EIM_TRANS.FEESID,EIM_TRANS.WAIVER+EIM_TRANS.SCHOLAR+EIM_TRANS.AMOUNT PREAMOUNT, EIM_TRANS.WAIVER, EIM_TRANS.SCHOLAR, EIM_TRANS.AMOUNT, EIM_TRANS.REMARKS
            FROM  EIM_FEES INNER JOIN
            EIM_TRANS ON EIM_FEES.FEESID = EIM_TRANS.FEESID  WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Trans.DataSource = ds;
                gv_Trans.DataBind();

                TextBox txtFEESNMFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtFEESNMFooter");
                txtFEESNMFooter.Focus();
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
                TextBox txtFEESNMFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtFEESNMFooter");
                txtFEESNMFooter.Focus();
            }
        }

        protected void txtDT_TextChanged(object sender, EventArgs e)
        {
            string Date = txtDT.Text;
            txtYR.Text = Date.Substring(6, 4);
            if (btnEdit.Text == "EDIT")
            {

                //Global.dropDownAddTrans(ddlTransNO, "SELECT TRANSNO FROM EIM_TRANS WHERE TRANSYY='" + txtYR.Text + "'");
                gridShowIDCreate();
                ddlRegYR.Focus();
                txtYR.Text = txtDT.Text.Substring(6, 4);

            }
            else
            {
                Global.dropDownAddTrans(ddlTransNO, "SELECT TRANSNO FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'");
                ddlTransNO.Focus();
            }

        }
        private void clear()
        {
            ddlProgNM.SelectedIndex = -1;
            ddlSemNM.SelectedIndex = -1;
            txtStuID.Text = "";
            txtPODDNO.Text = "";
            txtPODT.Text = "";
            txtPOBNK.Text = "";
            txtPOBRNC.Text = "";
            txtRemarks.Text = "";
            lblProID.Text = "";
            lblSemID.Text = "";
            txtStuNM.Text = "";
            Session["PROGRAMID"] = "";
            Session["FEESID"] = "";
            txtAcNM.Text = "";
            lblAccNO.Text = "";
            lblWaiver.Text = "";
        }

        protected void gv_Trans_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add") || e.CommandName.Equals("Complete") || e.CommandName.Equals("Print"))
                {
                    TextBox txtFEESNMFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtFEESNMFooter");
                    TextBox txtAMOUNTFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtAMOUNTFooter");
                    TextBox txtVatFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtVatFooter");
                    TextBox txtREMARKSFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtREMARKSFooter");
                    Label lblFEESIDFooter = (Label)gv_Trans.FooterRow.FindControl("lblFEESIDFooter");

                    TextBox txtWaiverFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtWaiverFooter");
                    TextBox txtWaiverSCOLRFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtWaiverSCOLRFooter");
                    TextBox txtTotalAMOUNTFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtTotalAMOUNTFooter");

                    iob.UserID = HttpContext.Current.Session["UserName"].ToString();
                    iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.PcName = HttpContext.Current.Session["PCName"].ToString();
                    iob.InTime = Global.Dayformat1(DateTime.Now);
                    if (txtWaiverFooter.Text == "0.00")
                        txtWaiverFooter.Text = "0";
                    if (txtWaiverSCOLRFooter.Text == "0.00")
                        txtWaiverSCOLRFooter.Text = "0";
                    iob.Amnt = Convert.ToDecimal(txtTotalAMOUNTFooter.Text);
                    iob.Waiver = Convert.ToDecimal(txtWaiverFooter.Text);
                    iob.Scholar = Convert.ToDecimal(txtWaiverSCOLRFooter.Text);
                    iob.AccNo = lblAccNO.Text;
                    iob.TransTP = "MREC";
                    if (Session["UserName"] == null)
                    {
                        Response.Redirect("~/cbiu/signin.aspx");
                    }
                    else
                    {
                        if (e.CommandName.Equals("Add"))
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
                            else if (txtStuID.Text == "")
                            {
                                txtStuID.Focus();
                            }
                            else if (txtStuID.Text == "")
                            {
                                txtStuID.Focus();
                            }
                            else if (lblAccNO.Text == "")
                            {
                                txtAcNM.Focus();
                            }
                            else if (lblFEESIDFooter.Text == "")
                            {
                                txtFEESNMFooter.Focus();
                            }
                            else
                            {
                                conn.Open();
                                SqlCommand cmd = new SqlCommand();
                                if (btnEdit.Text == "EDIT")
                                {
                                    //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'", txtInNo);
                                    cmd = new SqlCommand("Select TRANSNO from EIM_TRANSMST where TRANSNO='" + txtTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "'  AND TRANSTP='MREC'", conn);
                                }
                                else
                                {
                                    cmd = new SqlCommand("Select TRANSNO from EIM_TRANSMST where TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", conn);
                                }
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                conn.Close();
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    if (btnEdit.Text == "EDIT")
                                    {
                                        lblMSG.Visible = false;
                                        DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                        if (txtPODT.Text == "")
                                        {
                                            iob.PODT = DateTime.Parse("1999-01-01");
                                        }
                                        else
                                        {
                                            DateTime PoDate = DateTime.Parse(txtPODT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                            iob.PODT = PoDate;
                                        }
                                        iob.TrnsDT = Date;
                                        iob.TransYR = int.Parse(txtYR.Text);
                                        iob.TrnsNO = int.Parse(txtTransNO.Text);
                                        iob.RegYR = int.Parse(ddlRegYR.Text);
                                        iob.SemID = int.Parse(lblSemID.Text);
                                        iob.ProgID = lblProID.Text;
                                        iob.StuID = txtStuID.Text;
                                        iob.PONO = txtPODDNO.Text;
                                        if (txtPODDNO.Text == "")
                                            iob.PONO = "";
                                        iob.POBNK = txtPOBNK.Text;
                                        if (txtPOBNK.Text == "")
                                            iob.POBNK = "";
                                        iob.POBRNC = txtPOBRNC.Text;
                                        if (txtPOBRNC.Text == "")
                                            iob.POBRNC = "";
                                        iob.Remarks = txtRemarks.Text;
                                        if (txtRemarks.Text == "")
                                            iob.Remarks = "";
                                        iob.FeesID = lblFEESIDFooter.Text;
                                        iob.RemarksGRD = txtREMARKSFooter.Text;
                                        if (txtREMARKSFooter.Text == "")
                                            txtREMARKSFooter.Text = "";
                                        dob.Insert_EIM_TRANS(iob);
                                        gridShow();

                                    }
                                    else
                                    {
                                        //Global.lblAdd("SELECT MAX(TRANSNO) FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "'", lblTransNO);
                                        iob.TrnsNO = int.Parse(ddlTransNO.Text);

                                        conn.Open();
                                        SqlCommand cmd1 = new SqlCommand("SELECT * FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' and TRANSNO ='" + ddlTransNO.Text + "' AND TRANSTP='MREC'", conn);
                                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                        DataSet ds1 = new DataSet();
                                        da1.Fill(ds1);
                                        conn.Close();
                                        if (ds1.Tables[0].Rows.Count > 0)
                                        {
                                            DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                            if (txtPODT.Text == "")
                                            {
                                                iob.PODT = DateTime.Parse("1999-01-01");
                                            }
                                            else
                                            {
                                                DateTime PoDate = DateTime.Parse(txtPODT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                                iob.PODT = PoDate;
                                            }
                                            iob.TrnsDT = Date;
                                            iob.TransYR = int.Parse(txtYR.Text);
                                            // iob.TrnsNO = int.Parse(ddlTransNO.Text);
                                            iob.RegYR = int.Parse(ddlRegYR.Text);
                                            iob.SemID = int.Parse(lblSemID.Text);
                                            iob.ProgID = lblProID.Text;
                                            iob.StuID = txtStuID.Text;
                                            iob.PONO = txtPODDNO.Text;
                                            if (txtPODDNO.Text == "")
                                                iob.PONO = "";
                                            iob.POBNK = txtPOBNK.Text;
                                            if (txtPOBNK.Text == "")
                                                iob.POBNK = "";
                                            iob.POBRNC = txtPOBRNC.Text;
                                            if (txtPOBRNC.Text == "")
                                                iob.POBRNC = "";
                                            iob.Remarks = txtRemarks.Text;
                                            if (txtRemarks.Text == "")
                                                iob.Remarks = "";
                                            iob.FeesID = lblFEESIDFooter.Text;
                                            iob.RemarksGRD = txtREMARKSFooter.Text;
                                            if (txtREMARKSFooter.Text == "")
                                                txtREMARKSFooter.Text = "";
                                            dob.Insert_EIM_TRANS(iob);

                                            gridShowEdit();
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
                                    Global.lblAdd("SELECT MAX(TRANSNO) FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", lblTransNO);
                                    if (btnEdit.Text == "EDIT")
                                    {
                                        if (lblTransNO.Text == "")
                                        {
                                            iob.TrnsNO = 1;
                                            // txtTransNO.Text = iob.TrnsNO.ToString();
                                        }
                                        else
                                        {
                                            iob.TrnsNO = int.Parse(lblTransNO.Text) + 1;
                                            // txtTransNO.Text = iob.TrnsNO.ToString();
                                        }
                                    }
                                    else
                                    {
                                        iob.TrnsNO = int.Parse(ddlTransNO.Text);
                                    }
                                    DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                    if (txtPODT.Text == "")
                                    {
                                        iob.PODT = DateTime.Parse("1999-01-01");
                                    }
                                    else
                                    {
                                        DateTime PoDate = DateTime.Parse(txtPODT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                        iob.PODT = PoDate;
                                    }
                                    iob.TrnsDT = Date;
                                    iob.TransYR = int.Parse(txtYR.Text);
                                    iob.RegYR = int.Parse(ddlRegYR.Text);
                                    iob.SemID = int.Parse(lblSemID.Text);
                                    iob.ProgID = lblProID.Text;
                                    iob.StuID = txtStuID.Text;
                                    iob.PONO = txtPODDNO.Text;
                                    if (txtPODDNO.Text == "")
                                        iob.PONO = "";
                                    iob.POBNK = txtPOBNK.Text;
                                    if (txtPOBNK.Text == "")
                                        iob.POBNK = "";
                                    iob.POBRNC = txtPOBRNC.Text;
                                    if (txtPOBRNC.Text == "")
                                        iob.POBRNC = "";
                                    iob.Remarks = txtRemarks.Text;
                                    if (txtRemarks.Text == "")
                                        iob.Remarks = "";
                                    iob.FeesID = lblFEESIDFooter.Text;
                                    iob.RemarksGRD = txtREMARKSFooter.Text;
                                    if (txtREMARKSFooter.Text == "")
                                        txtREMARKSFooter.Text = "";
                                    dob.Insert_EIM_TRANSMST(iob);
                                    dob.Insert_EIM_TRANS(iob);
                                    gridShow();
                                }


                            }
                        }
                        else if (e.CommandName.Equals("Complete"))
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
                            else if (txtStuID.Text == "")
                            {
                                txtStuID.Focus();
                            }
                            else if (txtStuID.Text == "")
                            {
                                txtStuID.Focus();
                            }
                            else if (lblFEESIDFooter.Text == "")
                            {
                                txtFEESNMFooter.Focus();
                            }
                            else
                            {
                                conn.Open();
                                SqlCommand cmd = new SqlCommand();
                                if (btnEdit.Text == "EDIT")
                                {
                                    //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'", txtInNo);
                                    cmd = new SqlCommand("Select TRANSNO from EIM_TRANSMST where TRANSNO='" + txtTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "'  AND TRANSTP='MREC'", conn);
                                }
                                else
                                {
                                    cmd = new SqlCommand("Select TRANSNO from EIM_TRANSMST where TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", conn);
                                }
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                conn.Close();
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    if (btnEdit.Text == "EDIT")
                                    {
                                        lblMSG.Visible = false;
                                        DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                        if (txtPODT.Text == "")
                                        {
                                            iob.PODT = DateTime.Parse("1999-01-01");
                                        }
                                        else
                                        {
                                            DateTime PoDate = DateTime.Parse(txtPODT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                            iob.PODT = PoDate;
                                        }
                                        iob.TrnsDT = Date;
                                        iob.TransYR = int.Parse(txtYR.Text);
                                        iob.TrnsNO = int.Parse(txtTransNO.Text);
                                        iob.RegYR = int.Parse(ddlRegYR.Text);
                                        iob.SemID = int.Parse(lblSemID.Text);
                                        iob.ProgID = lblProID.Text;
                                        iob.StuID = txtStuID.Text;
                                        iob.PONO = txtPODDNO.Text;
                                        if (txtPODDNO.Text == "")
                                            iob.PONO = "";
                                        iob.POBNK = txtPOBNK.Text;
                                        if (txtPOBNK.Text == "")
                                            iob.POBNK = "";
                                        iob.POBRNC = txtPOBRNC.Text;
                                        if (txtPOBRNC.Text == "")
                                            iob.POBRNC = "";
                                        iob.Remarks = txtRemarks.Text;
                                        if (txtRemarks.Text == "")
                                            iob.Remarks = "";
                                        iob.FeesID = lblFEESIDFooter.Text;
                                        iob.RemarksGRD = txtREMARKSFooter.Text;
                                        if (txtREMARKSFooter.Text == "")
                                            txtREMARKSFooter.Text = "";
                                        dob.Insert_EIM_TRANS(iob);
                                        gridShowIDCreate();
                                        txtPOBNK.Enabled = true;
                                        txtPOBRNC.Enabled = true;
                                        txtPODDNO.Enabled = true;
                                        txtPODT.Enabled = true;
                                        clear();
                                        gridShowIDCreate();
                                    }
                                    else
                                    {
                                        // Global.lblAdd("SELECT MAX(TRANSNO) FROM EIM_TRANS WHERE TRANSYY='" + txtYR.Text + "'", lblTransNO);
                                        iob.TrnsNO = int.Parse(ddlTransNO.Text);

                                        conn.Open();
                                        SqlCommand cmd1 = new SqlCommand("SELECT * FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' and TRANSNO ='" + ddlTransNO.Text + "' AND TRANSTP='MREC'", conn);
                                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                        DataSet ds1 = new DataSet();
                                        da.Fill(ds1);
                                        conn.Close();
                                        if (ds1.Tables[0].Rows.Count > 0)
                                        {
                                            DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                            if (txtPODT.Text == "")
                                            {
                                                iob.PODT = DateTime.Parse("1999-01-01");
                                            }
                                            else
                                            {
                                                DateTime PoDate = DateTime.Parse(txtPODT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                                iob.PODT = PoDate;
                                            }
                                            iob.TrnsDT = Date;
                                            iob.TransYR = int.Parse(txtYR.Text);
                                            // iob.TrnsNO = int.Parse(txtTransNO.Text);
                                            iob.RegYR = int.Parse(ddlRegYR.Text);
                                            iob.SemID = int.Parse(lblSemID.Text);
                                            iob.ProgID = lblProID.Text;
                                            iob.StuID = txtStuID.Text;
                                            iob.PONO = txtPODDNO.Text;
                                            if (txtPODDNO.Text == "")
                                                iob.PONO = "";
                                            iob.POBNK = txtPOBNK.Text;
                                            if (txtPOBNK.Text == "")
                                                iob.POBNK = "";
                                            iob.POBRNC = txtPOBRNC.Text;
                                            if (txtPOBRNC.Text == "")
                                                iob.POBRNC = "";
                                            iob.Remarks = txtRemarks.Text;
                                            if (txtRemarks.Text == "")
                                                iob.Remarks = "";
                                            iob.FeesID = lblFEESIDFooter.Text;

                                            iob.RemarksGRD = txtREMARKSFooter.Text;
                                            if (txtREMARKSFooter.Text == "")
                                                txtREMARKSFooter.Text = "";
                                            dob.Insert_EIM_TRANS(iob);
                                            txtPOBNK.Enabled = true;
                                            txtPOBRNC.Enabled = true;
                                            txtPODDNO.Enabled = true;
                                            txtPODT.Enabled = true;
                                            gridShowEdit();
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
                                    Global.lblAdd("SELECT MAX(TRANSNO) FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", lblTransNO);
                                    if (btnEdit.Text == "EDIT")
                                    {
                                        if (lblTransNO.Text == "")
                                        {
                                            iob.TrnsNO = 1;
                                            // txtTransNO.Text = iob.TrnsNO.ToString();
                                        }
                                        else
                                        {
                                            iob.TrnsNO = int.Parse(lblTransNO.Text) + 1;
                                            // txtTransNO.Text = iob.TrnsNO.ToString();
                                        }
                                    }
                                    else
                                    {
                                        iob.TrnsNO = int.Parse(ddlTransNO.Text);
                                    }
                                    DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                    if (txtPODT.Text == "")
                                    {
                                        iob.PODT = DateTime.Parse("1999-01-01");
                                    }
                                    else
                                    {
                                        DateTime PoDate = DateTime.Parse(txtPODT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                        iob.PODT = PoDate;
                                    }
                                    iob.TrnsDT = Date;
                                    iob.TransYR = int.Parse(txtYR.Text);
                                    iob.RegYR = int.Parse(ddlRegYR.Text);
                                    iob.SemID = int.Parse(lblSemID.Text);
                                    iob.ProgID = lblProID.Text;
                                    iob.StuID = txtStuID.Text;
                                    iob.PONO = txtPODDNO.Text;
                                    if (txtPODDNO.Text == "")
                                        iob.PONO = "";
                                    iob.POBNK = txtPOBNK.Text;
                                    if (txtPOBNK.Text == "")
                                        iob.POBNK = "";
                                    iob.POBRNC = txtPOBRNC.Text;
                                    if (txtPOBRNC.Text == "")
                                        iob.POBRNC = "";
                                    iob.Remarks = txtRemarks.Text;
                                    if (txtRemarks.Text == "")
                                        iob.Remarks = "";
                                    iob.FeesID = lblFEESIDFooter.Text;
                                    iob.RemarksGRD = txtREMARKSFooter.Text;
                                    if (txtREMARKSFooter.Text == "")
                                        txtREMARKSFooter.Text = "";
                                    dob.Insert_EIM_TRANSMST(iob);
                                    dob.Insert_EIM_TRANS(iob);
                                    gridShowIDCreate();
                                    txtPOBNK.Enabled = true;
                                    txtPOBRNC.Enabled = true;
                                    txtPODDNO.Enabled = true;
                                    txtPODT.Enabled = true;
                                    clear();
                                    gridShowIDCreate();
                                }

                            }
                        }
                        else if (e.CommandName.Equals("Print"))
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
                            else if (txtStuID.Text == "")
                            {
                                txtStuID.Focus();
                            }
                            else if (txtStuID.Text == "")
                            {
                                txtStuID.Focus();
                            }
                            else if (lblFEESIDFooter.Text == "")
                            {
                                txtFEESNMFooter.Focus();
                            }
                            else
                            {
                                if (btnEdit.Text == "EDIT")
                                    Session["TRANSNO"] = txtTransNO.Text;
                                else
                                    Session["TRANSNO"] = ddlTransNO.Text;

                                Session["TRANSYY"] = txtYR.Text;
                                Session["TRANSDT"] = txtDT.Text;
                                Session["SEMESTERNM"] = ddlSemNM.Text;
                                //Session["InvNo_ISU"] = txtInNo_ISU.Text;
                                Session["PROGRAMNM"] = ddlProgNM.Text;
                                Session["STUDENTID"] = txtStuID.Text;
                                Session["STUDENTNM"] = txtStuNM.Text;
                                Session["REMARKS"] = txtRemarks.Text;

                                Session["PODONO"] = txtPODDNO.Text;
                                Session["PODATE"] = txtPODT.Text;
                                Session["POBANK"] = txtPOBNK.Text;
                                Session["POBRANCH"] = txtPOBRNC.Text;
                                Session["ACNM"] = txtAcNM.Text;
                                conn.Open();
                                SqlCommand cmd = new SqlCommand();
                                if (btnEdit.Text == "EDIT")
                                {
                                    //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'", txtInNo);
                                    cmd = new SqlCommand("Select TRANSNO from EIM_TRANSMST where TRANSNO='" + txtTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "'  AND TRANSTP='MREC'", conn);
                                }
                                else
                                {
                                    cmd = new SqlCommand("Select TRANSNO from EIM_TRANSMST where TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", conn);
                                }
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                conn.Close();
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    if (btnEdit.Text == "EDIT")
                                    {
                                        lblMSG.Visible = false;
                                        DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                        if (txtPODT.Text == "")
                                        {
                                            iob.PODT = DateTime.Parse("1999-01-01");
                                        }
                                        else
                                        {
                                            DateTime PoDate = DateTime.Parse(txtPODT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                            iob.PODT = PoDate;
                                        }
                                        iob.TrnsDT = Date;
                                        iob.TransYR = int.Parse(txtYR.Text);
                                        iob.TrnsNO = int.Parse(txtTransNO.Text);
                                        iob.RegYR = int.Parse(ddlRegYR.Text);
                                        iob.SemID = int.Parse(lblSemID.Text);
                                        iob.ProgID = lblProID.Text;
                                        iob.StuID = txtStuID.Text;
                                        iob.PONO = txtPODDNO.Text;
                                        if (txtPODDNO.Text == "")
                                            iob.PONO = "";
                                        iob.POBNK = txtPOBNK.Text;
                                        if (txtPOBNK.Text == "")
                                            iob.POBNK = "";
                                        iob.POBRNC = txtPOBRNC.Text;
                                        if (txtPOBRNC.Text == "")
                                            iob.POBRNC = "";
                                        iob.Remarks = txtRemarks.Text;
                                        if (txtRemarks.Text == "")
                                            iob.Remarks = "";
                                        iob.FeesID = lblFEESIDFooter.Text;
                                        iob.RemarksGRD = txtREMARKSFooter.Text;
                                        if (txtREMARKSFooter.Text == "")
                                            txtREMARKSFooter.Text = "";
                                        dob.Insert_EIM_TRANS(iob);
                                        txtPOBNK.Enabled = true;
                                        txtPOBRNC.Enabled = true;
                                        txtPODDNO.Enabled = true;
                                        txtPODT.Enabled = true;

                                        clear();
                                        gridShowIDCreate();
                                    }
                                    else
                                    {
                                        // Global.lblAdd("SELECT MAX(TRANSNO) FROM EIM_TRANS WHERE TRANSYY='" + txtYR.Text + "'", lblTransNO);
                                        iob.TrnsNO = int.Parse(ddlTransNO.Text);

                                        conn.Open();
                                        SqlCommand cmd1 = new SqlCommand("SELECT * FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' and TRANSNO ='" + ddlTransNO.Text + "' AND TRANSTP='MREC'", conn);
                                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                        DataSet ds1 = new DataSet();
                                        da.Fill(ds1);
                                        conn.Close();
                                        if (ds1.Tables[0].Rows.Count > 0)
                                        {
                                            DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                            if (txtPODT.Text == "")
                                            {
                                                iob.PODT = DateTime.Parse("1999-01-01");
                                            }
                                            else
                                            {
                                                DateTime PoDate = DateTime.Parse(txtPODT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                                iob.PODT = PoDate;
                                            }
                                            iob.TrnsDT = Date;
                                            iob.TransYR = int.Parse(txtYR.Text);
                                            //iob.TrnsNO = int.Parse(txtTransNO.Text);
                                            iob.RegYR = int.Parse(ddlRegYR.Text);
                                            iob.SemID = int.Parse(lblSemID.Text);
                                            iob.ProgID = lblProID.Text;
                                            iob.StuID = txtStuID.Text;
                                            iob.PONO = txtPODDNO.Text;
                                            if (txtPODDNO.Text == "")
                                                iob.PONO = "";
                                            iob.POBNK = txtPOBNK.Text;
                                            if (txtPOBNK.Text == "")
                                                iob.POBNK = "";
                                            iob.POBRNC = txtPOBRNC.Text;
                                            if (txtPOBRNC.Text == "")
                                                iob.POBRNC = "";
                                            iob.Remarks = txtRemarks.Text;
                                            if (txtRemarks.Text == "")
                                                iob.Remarks = "";
                                            iob.FeesID = lblFEESIDFooter.Text;
                                            iob.RemarksGRD = txtREMARKSFooter.Text;
                                            if (txtREMARKSFooter.Text == "")
                                                txtREMARKSFooter.Text = "";
                                            dob.Insert_EIM_TRANS(iob);

                                            gridShowEdit();
                                            txtPOBNK.Enabled = true;
                                            txtPOBRNC.Enabled = true;
                                            txtPODDNO.Enabled = true;
                                            txtPODT.Enabled = true;
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
                                    Global.lblAdd("SELECT MAX(TRANSNO) FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", lblTransNO);
                                    if (btnEdit.Text == "EDIT")
                                    {
                                        if (lblTransNO.Text == "")
                                        {
                                            iob.TrnsNO = 1;
                                            // txtTransNO.Text = iob.TrnsNO.ToString();
                                        }
                                        else
                                        {
                                            iob.TrnsNO = int.Parse(lblTransNO.Text) + 1;
                                            // txtTransNO.Text = iob.TrnsNO.ToString();
                                        }
                                    }
                                    else
                                    {
                                        iob.TrnsNO = int.Parse(ddlTransNO.Text);
                                    }
                                    DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                    if (txtPODT.Text == "")
                                    {
                                        iob.PODT = DateTime.Parse("1999-01-01");
                                    }
                                    else
                                    {
                                        DateTime PoDate = DateTime.Parse(txtPODT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                        iob.PODT = PoDate;
                                    }
                                    iob.TrnsDT = Date;
                                    iob.TransYR = int.Parse(txtYR.Text);
                                    iob.RegYR = int.Parse(ddlRegYR.Text);
                                    iob.SemID = int.Parse(lblSemID.Text);
                                    iob.ProgID = lblProID.Text;
                                    iob.StuID = txtStuID.Text;
                                    iob.PONO = txtPODDNO.Text;
                                    if (txtPODDNO.Text == "")
                                        iob.PONO = "";
                                    iob.POBNK = txtPOBNK.Text;
                                    if (txtPOBNK.Text == "")
                                        iob.POBNK = "";
                                    iob.POBRNC = txtPOBRNC.Text;
                                    if (txtPOBRNC.Text == "")
                                        iob.POBRNC = "";
                                    iob.Remarks = txtRemarks.Text;
                                    if (txtRemarks.Text == "")
                                        iob.Remarks = "";
                                    iob.FeesID = lblFEESIDFooter.Text;
                                    iob.RemarksGRD = txtREMARKSFooter.Text;
                                    if (txtREMARKSFooter.Text == "")
                                        txtREMARKSFooter.Text = "";
                                    dob.Insert_EIM_TRANSMST(iob);
                                    dob.Insert_EIM_TRANS(iob);
                                    //lblMSG.Visible = true;
                                    //lblMSG.Text = "Inserted !";
                                    clear();
                                    txtPOBNK.Enabled = true;
                                    txtPOBRNC.Enabled = true;
                                    txtPODDNO.Enabled = true;
                                    txtPODT.Enabled = true;
                                }


                            }
                            if (btnEdit.Text == "EDIT")
                            {

                                gridShowIDCreate();
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "OpenWindow", "window.open('../Report/MoneyReceipt.aspx','_newtab');", true);
                            }
                            else
                            {
                                gridShowEdit();
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                     "OpenWindow", "window.open('../Report/MoneyReceipt.aspx','_newtab');", true);
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

        protected void txtStuID_TextChanged(object sender, EventArgs e)
        {
            lblMSG.Text = "";
            if (txtStuID.Text == "")
            {
                txtStuID.Focus();
            }
            else
            {
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE STUDENTID='" + txtStuID.Text + "'", txtStuNM);
                lblWaiver.Text = "";
                Global.lblAdd("SELECT 'WAIVER '+ CONVERT(NVARCHAR(10),WAIVER,103) +' % AND SCHOLARSHIP  '+CONVERT(NVARCHAR(10),SCHOLAR,103)+' %' AS AMOUNT FROM EIM_STUDENT WHERE STUDENTID='" + txtStuID.Text + "'", lblWaiver);
                txtAcNM.Focus();
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
                Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
                ddlProgNM.Focus();
            }
        }

        protected void ddlProgNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtStuID.Text = "";
            if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
            }
            else
            {
                Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProID);
                Session["PROGRAMID"] = lblProID.Text;
                txtStuID.Focus();
            }
        }

        protected void ddlRegYR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegYR.Text != "")
            {
                ddlSemNM.Focus();
            }

        }

        protected void txtFEESNMFooter_TextChanged(object sender, EventArgs e)
        {
            Label lblFEESIDFooter = (Label)gv_Trans.FooterRow.FindControl("lblFEESIDFooter");
            TextBox txtFEESNMFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtFEESNMFooter");
            TextBox txtAMOUNTFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtAMOUNTFooter");
            Global.lblAdd("SELECT FEESID FROM EIM_FEES WHERE FEESNM='" + txtFEESNMFooter.Text + "'", lblFEESIDFooter);
            txtAMOUNTFooter.Focus();
        }

        protected void gv_Trans_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string TransNo = "";
                if (btnEdit.Text == "EDIT")
                    TransNo = txtTransNO.Text;
                else
                    TransNo = ddlTransNO.Text;
                Label lblFEESID = (Label)gv_Trans.Rows[e.RowIndex].FindControl("lblFEESID");
                //LogInsert Start   
                Label lblDescript = new Label();
                Global.lblAdd(@"SELECT ISNULL(CONVERT(NVARCHAR(50),TRANSDT,103),'(NULL)')+'  '+TRANSTP+'  '+CONVERT(NVARCHAR(50),TRANSYY,103)+'  '+
                CONVERT(NVARCHAR(50),TRANSNO,103)+'  '+ISNULL(CONVERT(NVARCHAR(50),REGYY,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),SEMESTERID,103),'(NULL)')+'  '+
                ISNULL(CNBCD,'(NULL)')+'  '+ISNULL(PROGRAMID,'(NULL)')+'  '+STUDENTID+'  '+FEESID+'  '+ISNULL(CONVERT(NVARCHAR(50),AMOUNT,103),'(NULL)')+'  '+
                ISNULL(REMARKS,'(NULL)')+'  '+ISNULL(USERID,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+
                ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+ISNULL(UPDUSERID,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+
                ISNULL(UPDIPADDRESS,'(NULL)')+'  '+
                ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)') FROM EIM_TRANS 
                WHERE FEESID ='" + lblFEESID.Text + "' AND   TRANSYY = '" + txtYR.Text + "' AND TRANSNO='" + TransNo + "' AND TRANSTP='MREC'", lblDescript);
                iob.TableID = "STK_TRANS";
                iob.Type = "DELETE";
                iob.Descrip = lblDescript.Text;
                dob.INSERT_LOG(iob);
                //LogInsert End 

                if (btnEdit.Text == "EDIT")
                {
                    DateTime transdate = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    string TrDate = transdate.ToString("yyyy/MM/dd");

                    lblMSG.Visible = false;
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("DELETE FROM EIM_TRANS WHERE FEESID ='" + lblFEESID.Text + "' AND   TRANSYY = '" + txtYR.Text + "' AND TRANSNO='" + TransNo + "' AND TRANSTP='MREC'", conn);
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    gridShow();
                }
                else
                {
                    DateTime transdate = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM EIM_TRANS WHERE  TRANSYY= '" + txtYR.Text + "' AND TRANSTP='MREC' AND TRANSNO = '" + TransNo + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        lblMSG.Visible = false;
                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("DELETE FROM EIM_TRANS WHERE FEESID ='" + lblFEESID.Text + "' AND TRANSTP='MREC' AND TRANSYY = '" + txtYR.Text + "' AND TRANSNO='" + TransNo + "'", conn);
                        cmd1.ExecuteNonQuery();
                        conn.Close();
                        gridShowEdit();

                    }
                    else
                    {
                        //LogInsert Start    
                        Global.lblAdd(@"SELECT ISNULL(CONVERT(NVARCHAR(50),TRANSDT,103),'(NULL)')+'  '+TRANSTP+'  '+TRANSYY+'  '+CONVERT(NVARCHAR(50),TRANSNO,103)+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),REGYY,103),'(NULL)')+'  '+ISNULL(CNBCD,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),SEMESTERID,103),'(NULL)')+'  '+ISNULL(PROGRAMID,'(NULL)')+'  '+
                        ISNULL(STUDENTID,'(NULL)')+'  '+ISNULL(PONO,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),PODT,103),'(NULL)')+'  '+ISNULL(POBANK,'(NULL)')+'  '+ISNULL(POBANKBR,'(NULL)')+'  '+
                        ISNULL(REMARKS,'(NULL)')+'  '+ISNULL(USERID,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                        ISNULL(UPDUSERID,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(UPDIPADDRESS,'(NULL)')+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)') FROM EIM_TRANSMST 
                        WHERE TRANSNO ='" + TransNo + "'  AND TRANSTP='MREC' AND TRANSYY='" + txtYR.Text + "'", lblDescript);
                        iob.TableID = "STK_TRANSMST";
                        iob.Type = "DELETE";
                        iob.Descrip = lblDescript.Text;
                        dob.INSERT_LOG(iob);
                        //LogInsert End 
                        lblMSG.Visible = false;
                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand("DELETE FROM EIM_TRANS WHERE TRANSNO ='" + TransNo + "' AND FEESID ='" + lblFEESID.Text + "' AND TRANSTP='MREC' AND TRANSYY = '" + txtYR.Text + "'", conn);
                        cmd1.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("DELETE FROM EIM_TRANSMST WHERE TRANSNO ='" + TransNo + "'  AND TRANSTP='MREC' AND TRANSYY='" + txtYR.Text + "'", conn);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                        ddlTransNO.SelectedIndex = -1;
                        ddlTransNO.Focus();
                        clear();
                        gridShowEdit();
                        Global.dropDownAddTrans(ddlTransNO, "SELECT TRANSNO FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'");
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
                btnEdit.Text = "NEW";
                txtTransNO.Visible = false;
                ddlTransNO.Visible = true;
                // btnComplete.Visible = false;
                string Date = txtDT.Text;
                txtYR.Text = Date.Substring(6, 4);
                Global.dropDownAddTrans(ddlTransNO, "SELECT DISTINCT TRANSNO FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'");
                clear();
                gridShowEdit();
                ddlTransNO.Focus();
                txtPOBNK.Enabled = true;
                txtPOBRNC.Enabled = true;
                txtPODDNO.Enabled = true;
                txtPODT.Enabled = true;
                ddlRegYR.Enabled = false;
                // txtDT.Enabled = false;


            }
            else
            {
                ddlRegYR.Enabled = true;
                txtDT.Text = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                btnEdit.Text = "EDIT";
                txtTransNO.Visible = true;
                ddlTransNO.Visible = false;
                //btnComplete.Visible = true;
                //Global.dropDownAdd(ddlTransNO, "SELECT TRANSNO FROM EIM_TRANS WHERE TRANSYY='" + txtYR.Text + "'");
                clear();
                gridShowIDCreate();
                string Date = txtDT.Text;
                txtYR.Text = Date.Substring(6, 4);
                txtPOBNK.Enabled = true;
                txtPOBRNC.Enabled = true;
                txtPODDNO.Enabled = true;
                txtPODT.Enabled = true;
            }
        }

        protected void ddlTransNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTransNO.Text == "")
            {
                clear();
                gv_Trans.Visible = false;
                ddlTransNO.Focus();
                txtPOBNK.Enabled = true;
                txtPOBRNC.Enabled = true;
                txtPODDNO.Enabled = true;
                txtPODT.Enabled = true;
            }
            else
            {
                clear();
                gv_Trans.Visible = true;
                //                //Global.txtAdd("SELECT TRANSYY FROM EIM_TRANS WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "'", txtYR);
                //                Global.lblAdd("SELECT REGYY FROM EIM_TRANSMST WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", lblYR);
                //                ddlRegYR.Text = lblYR.Text;
                //                Global.lblAdd("SELECT SEMESTERID FROM EIM_TRANSMST WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", lblSemID);
                //                Global.lblAdd("SELECT SEMESTERNM FROM EIM_SEMESTER WHERE SEMESTERID='" + lblSemID.Text + "'", lblSEMNM);
                //                ddlSemNM.Text = lblSEMNM.Text;
                //                Global.lblAdd("SELECT PROGRAMID FROM EIM_TRANSMST WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", lblProID);
                //                Global.lblAdd("SELECT PROGRAMNM FROM EIM_PROGRAM WHERE PROGRAMID='" + lblProID.Text + "'", lblPRONM);
                //                ddlProgNM.Text = lblPRONM.Text;
                //                Global.txtAdd("SELECT STUDENTID FROM EIM_TRANS WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", txtStuID);
                //                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE STUDENTID='" + txtStuID.Text + "'", txtStuNM);
                //                Global.txtAdd("SELECT PONO FROM EIM_TRANSMST WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", txtPODDNO);
                //                Global.txtAdd("SELECT CONVERT(NVARCHAR(10),PODT,103) FROM EIM_TRANSMST WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", txtPODT);
                //                if (txtPODT.Text == "1/1/1999 12:00:00 AM")
                //                    txtPODT.Text = "";
                //                Global.txtAdd("SELECT POBANK FROM EIM_TRANSMST WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", txtPOBNK);
                //                Global.txtAdd("SELECT POBANKBR FROM EIM_TRANSMST WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", txtPOBRNC);
                //                Global.txtAdd("SELECT REMARKS FROM EIM_TRANSMST WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", txtRemarks);
                //                Global.txtAdd(@"SELECT     GL_ACCHART.ACCOUNTNM
                //                                    FROM  GL_ACCHART INNER JOIN
                //                      EIM_TRANSMST ON GL_ACCHART.ACCOUNTCD = EIM_TRANSMST.CNBCD WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "'", txtAcNM);

                //                if (txtPODT.Text == "01/01/1999 12:00:00 AM")
                //                    txtPODT.Text = "";
                //                Global.lblAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTNM='" + txtAcNM.Text + "' AND LEVELCD='5'", lblAccNO);

                string Script = @"SELECT     CONVERT(NVARCHAR(10),dbo.EIM_TRANSMST.TRANSDT,103) TRANSDT, dbo.EIM_TRANSMST.REGYY, dbo.EIM_TRANSMST.TRANSYY, dbo.EIM_SEMESTER.SEMESTERNM, dbo.EIM_TRANSMST.SEMESTERID, 
                      dbo.EIM_PROGRAM.PROGRAMNM, dbo.EIM_TRANSMST.PROGRAMID, dbo.EIM_STUDENT.STUDENTNM, dbo.EIM_TRANSMST.STUDENTID, 
                      dbo.EIM_TRANSMST.CNBCD, dbo.EIM_TRANSMST.PONO, dbo.EIM_TRANSMST.PODT, dbo.EIM_TRANSMST.POBANK, dbo.EIM_TRANSMST.POBANKBR, dbo.EIM_TRANSMST.REMARKS
                      FROM dbo.EIM_STUDENT INNER JOIN
                      dbo.EIM_TRANSMST INNER JOIN
                      dbo.EIM_SEMESTER ON dbo.EIM_TRANSMST.SEMESTERID = dbo.EIM_SEMESTER.SEMESTERID INNER JOIN
                      dbo.EIM_PROGRAM ON dbo.EIM_TRANSMST.PROGRAMID = dbo.EIM_PROGRAM.PROGRAMID ON dbo.EIM_STUDENT.STUDENTID = dbo.EIM_TRANSMST.STUDENTID 
                      WHERE EIM_TRANSMST.TRANSNO='" + ddlTransNO.Text + "' AND EIM_TRANSMST.TRANSYY='" + txtYR.Text + "' AND EIM_TRANSMST.TRANSTP='MREC'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(Script, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDT.Text = dr["TRANSDT"].ToString();
                    ddlRegYR.Text = dr["REGYY"].ToString();
                    lblYR.Text = dr["TRANSYY"].ToString();
                    lblSemID.Text = dr["SEMESTERID"].ToString();
                    ddlSemNM.Text = dr["SEMESTERNM"].ToString();
                    lblProID.Text = dr["PROGRAMID"].ToString();
                    ddlProgNM.Text = dr["PROGRAMNM"].ToString();
                    txtStuID.Text = dr["STUDENTID"].ToString();
                    txtStuNM.Text = dr["STUDENTNM"].ToString();
                    lblAccNO.Text = dr["CNBCD"].ToString();
                    Global.txtAdd(@"SELECT  ACCOUNTNM FROM  GL_ACCHART   WHERE ACCOUNTCD ='" + lblAccNO.Text + "'", txtAcNM);
                    Global.lblAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTNM='" + txtAcNM.Text + "' AND LEVELCD='5'", lblAccNO);
                    txtPODDNO.Text = dr["PONO"].ToString();
                    txtPODT.Text = dr["PODT"].ToString();
                    if (txtPODT.Text == "1/1/1999 12:00:00 AM")
                        txtPODT.Text = "";
                    txtPOBNK.Text = dr["POBANK"].ToString();
                    txtPOBRNC.Text = dr["POBANKBR"].ToString();
                    txtRemarks.Text = dr["REMARKS"].ToString();

                }
                dr.Close();
                conn.Close();
                 
                gridShowEdit();
                string Acc = lblAccNO.Text;
                string Sub = Acc.Substring(0, 7);
                if (Sub == "1020101")
                {
                    txtAcNM.Focus();
                    txtPOBNK.Enabled = false;
                    txtPOBRNC.Enabled = false;
                    txtPODDNO.Enabled = false;
                    txtPODT.Enabled = false;
                }
                else
                {
                    txtPOBNK.Enabled = true;
                    txtPOBRNC.Enabled = true;
                    txtPODDNO.Enabled = true;
                    txtPODT.Enabled = true;
                }

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
            else if (txtStuNM.Text == "")
            {
                txtStuID.Focus();
            }
            //else if (gv_Trans.Rows[0].DataItem ==null)
            //{
            //    TextBox txtFEESNMFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtFEESNMFooter");
            //    txtFEESNMFooter.Focus();
            //}
            else
            {
                Session["TRANSNO"] = "";

                Session["TRANSYY"] = "";
                Session["TRANSDT"] = "";
                Session["SEMESTERNM"] = "";
                //Session["InvNo_ISU"] = txtInNo_ISU.Text;
                Session["PROGRAMNM"] = "";
                Session["STUDENTID"] = "";
                Session["STUDENTNM"] = "";
                Session["REMARKS"] = "";

                Session["PODONO"] = "";
                Session["PODATE"] = "";
                Session["POBANK"] = "";
                Session["POBRANCH"] = "";
                Session["ACNM"] = "";

                if (btnEdit.Text == "EDIT")
                    Session["TRANSNO"] = txtTransNO.Text;
                else
                    Session["TRANSNO"] = ddlTransNO.Text;

                Session["TRANSYY"] = txtYR.Text;
                Session["TRANSDT"] = txtDT.Text;
                Session["SEMESTERNM"] = ddlSemNM.Text;
                //Session["InvNo_ISU"] = txtInNo_ISU.Text;
                Session["PROGRAMNM"] = ddlProgNM.Text;
                Session["STUDENTID"] = txtStuID.Text;
                Session["STUDENTNM"] = txtStuNM.Text;
                Session["REMARKS"] = txtRemarks.Text;

                Session["PODONO"] = txtPODDNO.Text;
                Session["PODATE"] = txtPODT.Text;
                Session["POBANK"] = txtPOBNK.Text;
                Session["POBRANCH"] = txtPOBRNC.Text;
                Session["ACNM"] = txtAcNM.Text;
                //clear();
                if (btnEdit.Text == "EDIT")
                {

                    gridShow();

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "OpenWindow", "window.open('../Report/MoneyReceipt.aspx','_newtab');", true);
                }
                else
                {
                    gridShowEdit();
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                        "OpenWindow", "window.open('../Report/MoneyReceipt.aspx','_newtab');", true);
                }
            }

        }

        protected void txtPODT_TextChanged(object sender, EventArgs e)
        {
            if (txtPODT.Text == "")
            {
                txtPODT.Focus();
            }
            else
                txtPOBNK.Focus();
        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            clear();
            gridShowIDCreate();

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
                gridShowEdit();
            }
        }

        protected void gv_Trans_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                Label lblFEESID = (Label)gv_Trans.Rows[e.NewEditIndex].FindControl("lblFEESID");
                Session["FEESID"] = lblFEESID.Text;
                if (btnEdit.Text == "EDIT")
                {
                    gv_Trans.EditIndex = e.NewEditIndex;
                    gridShow();
                }
                else
                {
                    gv_Trans.EditIndex = e.NewEditIndex;
                    gridShowEdit();
                }

                TextBox txtFEESNMEdit = (TextBox)gv_Trans.Rows[e.NewEditIndex].FindControl("txtFEESNMEdit");
                txtFEESNMEdit.Focus();
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
                TextBox txtFEESNMEdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtFEESNMEdit");
                TextBox txtAMOUNTEdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtAMOUNTEdit");
                TextBox txtREMARKSEdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                Label lblFEESIDEdit = (Label)gv_Trans.Rows[e.RowIndex].FindControl("lblFEESIDEdit");

                TextBox txtWaiverEdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtWaiverEdit");
                TextBox txtWaiverSCOLREdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtWaiverSCOLREdit");
                TextBox txtTotalAMOUNTEdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtTotalAMOUNTEdit");
                if (txtWaiverEdit.Text == "0.00")
                    txtWaiverEdit.Text = "0";
                if (txtWaiverSCOLREdit.Text == "0.00")
                    txtWaiverSCOLREdit.Text = "0";
                iob.Amnt = Convert.ToDecimal(txtTotalAMOUNTEdit.Text);
                iob.Waiver = Convert.ToDecimal(txtWaiverEdit.Text);
                iob.Scholar = Convert.ToDecimal(txtWaiverSCOLREdit.Text);

                //Label lblFEESID = (Label)gv_Trans.Rows[e.RowIndex].FindControl("lblFEESID");
                iob.UPDUserID = HttpContext.Current.Session["UserName"].ToString();
                iob.UPDPcName = HttpContext.Current.Session["PCName"].ToString();
                iob.UPDIpaddress = HttpContext.Current.Session["IpAddress"].ToString();

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
                else if (txtStuID.Text == "")
                {
                    txtStuID.Focus();
                }
                else if (txtStuID.Text == "")
                {
                    txtStuID.Focus();
                }
                else if (lblFEESIDEdit.Text == "")
                {
                    txtFEESNMEdit.Focus();
                }
                else if (txtAMOUNTEdit.Text == "")
                {
                    txtAMOUNTEdit.Focus();
                }
                else
                {
                    DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    if (txtPODT.Text == "")
                    {
                        iob.PODT = DateTime.Parse("1999-01-01");
                    }
                    else
                    {

                        DateTime PoDate = DateTime.Parse(txtPODT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.PODT = PoDate;
                    }
                    // iob.FeesID = lblFEESIDEdit.Text; 
                    iob.AccNo = lblAccNO.Text;
                    iob.TrnsDT = Date;
                    iob.TransYR = int.Parse(txtYR.Text);
                    iob.RegYR = int.Parse(ddlRegYR.Text);
                    iob.SemID = int.Parse(lblSemID.Text);
                    iob.ProgID = lblProID.Text;
                    iob.StuID = txtStuID.Text;
                    iob.PONO = txtPODDNO.Text;
                    if (lblAccNO.Text == "")
                        lblAccNO.Text = "";
                    if (txtPODDNO.Text == "")
                        iob.PONO = "";
                    iob.POBNK = txtPOBNK.Text;
                    if (txtPOBNK.Text == "")
                        iob.POBNK = "";
                    iob.POBRNC = txtPOBRNC.Text;
                    if (txtPOBRNC.Text == "")
                        iob.POBRNC = "";
                    iob.Remarks = txtRemarks.Text;
                    if (txtRemarks.Text == "")
                        iob.Remarks = "";
                    iob.FeesID = lblFEESIDEdit.Text;
                    iob.RemarksGRD = txtREMARKSEdit.Text;
                    if (txtREMARKSEdit.Text == "")
                        txtREMARKSEdit.Text = "";
                    int TransNO;
                    if (btnEdit.Text == "EDIT")
                        TransNO = int.Parse(txtTransNO.Text);
                    else
                        TransNO = int.Parse(ddlTransNO.Text);
                    //LogInsert Start   
                    Label lblDescript = new Label();
                    Global.lblAdd(@"SELECT ISNULL(CONVERT(NVARCHAR(50),TRANSDT,103),'(NULL)')+'  '+TRANSTP+'  '+CONVERT(NVARCHAR(50),TRANSYY,103)+'  '+
                    CONVERT(NVARCHAR(50),TRANSNO,103)+'  '+ISNULL(CONVERT(NVARCHAR(50),REGYY,103),'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),SEMESTERID,103),'(NULL)')+'  '+
                    ISNULL(CNBCD,'(NULL)')+'  '+ISNULL(PROGRAMID,'(NULL)')+'  '+STUDENTID+'  '+FEESID+'  '+ISNULL(CONVERT(NVARCHAR(50),AMOUNT,103),'(NULL)')+'  '+
                    ISNULL(REMARKS,'(NULL)')+'  '+ISNULL(USERID,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+ISNULL(UPDUSERID,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+
                    ISNULL(UPDIPADDRESS,'(NULL)')+'  '+
                    ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)') FROM EIM_TRANS 
                    WHERE FEESID ='" + lblFEESIDEdit.Text + "' AND   TRANSYY = '" + txtYR.Text + "' AND TRANSNO='" + TransNO + "' AND TRANSTP='MREC'", lblDescript);
                    iob.TableID = "STK_TRANS";
                    iob.Type = "UPDATE";
                    iob.Descrip = lblDescript.Text;
                    dob.INSERT_LOG(iob);
                    //LogInsert End 
                    //LogInsert Start    
                    Global.lblAdd(@"SELECT ISNULL(CONVERT(NVARCHAR(50),TRANSDT,103),'(NULL)')+'  '+TRANSTP+'  '+TRANSYY+'  '+CONVERT(NVARCHAR(50),TRANSNO,103)+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),REGYY,103),'(NULL)')+'  '+ISNULL(CNBCD,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),SEMESTERID,103),'(NULL)')+'  '+ISNULL(PROGRAMID,'(NULL)')+'  '+
                        ISNULL(STUDENTID,'(NULL)')+'  '+ISNULL(PONO,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),PODT,103),'(NULL)')+'  '+ISNULL(POBANK,'(NULL)')+'  '+ISNULL(POBANKBR,'(NULL)')+'  '+
                        ISNULL(REMARKS,'(NULL)')+'  '+ISNULL(USERID,'(NULL)')+'  '+ISNULL(USERPC,'(NULL)')+'  '+ISNULL(IPADDRESS,'(NULL)')+'  '+ISNULL(CONVERT(NVARCHAR(50),INTIME,103),'(NULL)')+'  '+
                        ISNULL(UPDUSERID,'(NULL)')+'  '+ISNULL(UPDUSERPC,'(NULL)')+'  '+ISNULL(UPDIPADDRESS,'(NULL)')+'  '+
                        ISNULL(CONVERT(NVARCHAR(50),UPDTIME,103),'(NULL)') FROM EIM_TRANSMST 
                        WHERE TRANSNO ='" + TransNO + "'  AND TRANSTP='MREC' AND TRANSYY='" + txtYR.Text + "'", lblDescript);
                    iob.TableID = "STK_TRANSMST";
                    iob.Type = "UPDATE";
                    iob.Descrip = lblDescript.Text;
                    dob.INSERT_LOG(iob);
                    //LogInsert End 
                    if (btnEdit.Text == "EDIT")
                    {
                        iob.TrnsNO = int.Parse(txtTransNO.Text);
                        dob.Update_EIM_TRANS(iob);
                        dob.Update_EIM_TRANSMST(iob);

                        gv_Trans.EditIndex = -1;
                        gridShow();
                    }
                    else
                    {
                        iob.TrnsNO = int.Parse(ddlTransNO.Text);
                        dob.Update_EIM_TRANS(iob);
                        dob.Update_EIM_TRANSMST(iob);

                        gv_Trans.EditIndex = -1;
                        gridShowEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
        protected void txtFEESNMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            Label lblFEESIDEdit = (Label)row.FindControl("lblFEESIDEdit");
            TextBox txtFEESNMEdit = (TextBox)row.FindControl("txtFEESNMEdit");
            TextBox txtAMOUNTEdit = (TextBox)row.FindControl("txtAMOUNTEdit");
            Global.lblAdd("SELECT FEESID FROM EIM_FEES WHERE FEESNM='" + txtFEESNMEdit.Text + "'", lblFEESIDEdit);
            txtAMOUNTEdit.Focus();
        }

        protected void txtAcNM_TextChanged(object sender, EventArgs e)
        {
            if (txtAcNM.Text == "")
            {
                txtPODDNO.Focus();
                txtPOBNK.Enabled = true;
                txtPOBRNC.Enabled = true;
                txtPODDNO.Enabled = true;
                txtPODT.Enabled = true;
            }
            else
            {
                Global.lblAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTNM='" + txtAcNM.Text + "' AND LEVELCD ='5'", lblAccNO);
                string Acc = lblAccNO.Text;
                string Sub = Acc.Substring(0, 7);

                if (Sub == "1020101")
                {
                    txtAcNM.Focus();
                    txtPOBNK.Enabled = false;
                    txtPOBRNC.Enabled = false;
                    txtPODDNO.Enabled = false;
                    txtPODT.Enabled = false;
                    txtRemarks.Focus();

                }
                else
                {
                    txtPOBNK.Enabled = true;
                    txtPOBRNC.Enabled = true;
                    txtPODDNO.Enabled = true;
                    txtPODT.Enabled = true;
                    txtPODDNO.Focus();

                    // txt.Enabled = true;
                }
            }
        }
        protected void txtAMOUNTFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtAMOUNTFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtAMOUNTFooter");
            TextBox txtVatFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtVatFooter");
            TextBox txtREMARKSFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtREMARKSFooter");
            TextBox txtTotalFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtTotalFooter");
            Label lblFEESIDFooter = (Label)gv_Trans.FooterRow.FindControl("lblFEESIDFooter");

            TextBox txtWaiverFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtWaiverFooter");
            TextBox txtWaiverSCOLRFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtWaiverSCOLRFooter");
            TextBox txtTotalAMOUNTFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtTotalAMOUNTFooter");
            if (lblFEESIDFooter.Text == "102")
            {
                TextBox txtWaiver = new TextBox();
                TextBox txtScholar = new TextBox();
                Global.txtAdd("SELECT WAIVER FROM EIM_STUDENT WHERE STUDENTID='" + txtStuID.Text + "'", txtWaiver);
                Global.txtAdd("SELECT SCHOLAR FROM EIM_STUDENT WHERE STUDENTID='" + txtStuID.Text + "'", txtScholar);
                double Waiver = Convert.ToDouble(txtWaiver.Text);
                double Scholar = Convert.ToDouble(txtScholar.Text);
                double Amount = Convert.ToDouble(txtAMOUNTFooter.Text);
                if (Waiver > 0)
                    txtWaiverFooter.Text = ((Waiver * Amount) / 100).ToString();
                else
                    txtWaiverFooter.Text = "0.00";

                if (Scholar > 0)
                    txtWaiverSCOLRFooter.Text = ((Scholar * Amount) / 100).ToString();
                else
                    txtWaiverSCOLRFooter.Text = "0.00";
                double WaiverAmount = Convert.ToDouble(txtWaiverFooter.Text);
                double ScholarAmount = Convert.ToDouble(txtWaiverSCOLRFooter.Text);

                txtTotalAMOUNTFooter.Text = (Amount - (WaiverAmount + ScholarAmount)).ToString();
                txtREMARKSFooter.Focus();
            }
            else
            {
                txtWaiverFooter.Text = "0.00";
                txtWaiverSCOLRFooter.Text = "0.00";
                txtTotalAMOUNTFooter.Text = txtAMOUNTFooter.Text;
                txtREMARKSFooter.Focus();
            }
            //if (txtAMOUNTFooter.Text == "")
            //{
            //    txtAMOUNTFooter.Focus();
            //}
            //else
            //{
            //    double amount = Convert.ToDouble(txtAMOUNTFooter.Text);
            //    double Vat = Convert.ToDouble(txtAMOUNTFooter.Text);
            //    double a = 7.5;
            //    amount = ((amount * a) / 100);
            //    txtVatFooter.Text = amount.ToString("F2");
            //    txtTotalFooter.Text = (Vat + amount).ToString("F2");
            //    txtREMARKSFooter.Focus();
            //}
        }

        protected void txtAMOUNTEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtAMOUNTEdit = (TextBox)row.FindControl("txtAMOUNTEdit");
            TextBox txtVatEdit = (TextBox)row.FindControl("txtVatEdit");
            TextBox txtREMARKSEdit = (TextBox)row.FindControl("txtREMARKSEdit");
            Label lblFEESIDEdit = (Label)row.FindControl("lblFEESIDEdit");
            TextBox txtTotalEdit = (TextBox)row.FindControl("txtTotalEdit");
            TextBox txtWaiverEdit = (TextBox)row.FindControl("txtWaiverEdit");
            TextBox txtWaiverSCOLREdit = (TextBox)row.FindControl("txtWaiverSCOLREdit");
            TextBox txtTotalAMOUNTEdit = (TextBox)row.FindControl("txtTotalAMOUNTEdit");
            if (lblFEESIDEdit.Text == "102")
            {
                TextBox txtWaiver = new TextBox();
                TextBox txtScholar = new TextBox();
                Global.txtAdd("SELECT WAIVER FROM EIM_STUDENT WHERE STUDENTID='" + txtStuID.Text + "'", txtWaiver);
                Global.txtAdd("SELECT SCHOLAR FROM EIM_STUDENT WHERE STUDENTID='" + txtStuID.Text + "'", txtScholar);
                double Waiver = Convert.ToDouble(txtWaiver.Text);
                double Scholar = Convert.ToDouble(txtScholar.Text);
                double Amount = Convert.ToDouble(txtAMOUNTEdit.Text);
                if (Waiver > 0)
                    txtWaiverEdit.Text = ((Waiver * Amount) / 100).ToString();
                else
                    txtWaiverEdit.Text = "0.00";

                if (Scholar > 0)
                    txtWaiverSCOLREdit.Text = ((Scholar * Amount) / 100).ToString();
                else
                    txtWaiverSCOLREdit.Text = "0.00";
                double WaiverAmount = Convert.ToDouble(txtWaiverEdit.Text);
                double ScholarAmount = Convert.ToDouble(txtWaiverSCOLREdit.Text);

                txtTotalAMOUNTEdit.Text = (Amount - (WaiverAmount + ScholarAmount)).ToString();
                txtREMARKSEdit.Focus();
            }
            else
            {
                txtWaiverEdit.Text = "0.00";
                txtWaiverSCOLREdit.Text = "0.00";
                txtTotalAMOUNTEdit.Text = txtAMOUNTEdit.Text;
                txtREMARKSEdit.Focus();
            }
            //if (txtAMOUNTEdit.Text == "")
            //{
            //    txtAMOUNTEdit.Focus();
            //}
            //else
            //{
            //    double amount = Convert.ToDouble(txtAMOUNTEdit.Text);
            //    double Vat = Convert.ToDouble(txtAMOUNTEdit.Text);
            //    double a = 7.5;
            //    amount = ((amount * a) / 100);
            //    txtVatEdit.Text = amount.ToString("F2");
            //    txtTotalEdit.Text = (Vat + amount).ToString("F2");
            //    txtREMARKSEdit.Focus();
            //}
        }
    }
}
