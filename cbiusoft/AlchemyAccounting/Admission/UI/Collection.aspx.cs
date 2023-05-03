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
using System.Net;

namespace AlchemyAccounting.Admission.UI
{
    public partial class Collection : System.Web.UI.Page
    {

        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection conn = new SqlConnection(Global.connection);
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        //public void UserServerInfo()
        //{
        //    string strHostName = System.Net.Dns.GetHostName();
        //    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        //    IPAddress ipAddress = ipHostInfo.AddressList[0];
        //    iob.Ipaddress = ipAddress.ToString();
        //    iob.PcName = strHostName.ToString();
        //    iob.InTime = Global.Dayformat1(DateTime.Now);
        //    iob.UserID = Session["UserName"];
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                if (Session["UserTp"].ToString() == "ADMIN")
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
                        Global.DropDownAddAllTextWithValue(ddlSemNM, "SELECT SEMESTERNM,SEMESTERID FROM EIM_SEMESTER");
                        Global.DropDownAddAllTextWithValue(ddlProgNM, "SELECT PROGRAMNM,PROGRAMID FROM EIM_PROGRAM");
                        Global.dropDownAdd(ddlBatch, "SELECT DISTINCT BATCH FROM EIM_STUDENT ORDER BY BATCH");
                        gridShowIDCreate();
                        Session["YEAR"] = ddlRegYR.Text;
                        txtStuIDNew.Focus();

                    }
                }
                else
                    Response.Redirect("~/Permission_form.aspx");
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
            SqlCommand cmd = new SqlCommand("SELECT top 10 NEWSTUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open) conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["NEWSTUDENTID"].ToString());
            }
            if (conn.State != ConnectionState.Closed) conn.Close();
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
            if (conn.State != ConnectionState.Open) conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            }
            if (conn.State != ConnectionState.Closed) conn.Close();
            return CompletionSet.ToArray();
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionFEESNM(string prefixText, int count, string contextKey)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT FEESNM FROM EIM_FEES WHERE FEESNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open) conn.Open();
            List<string> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (oReader.Read())
            {
                CompletionSet.Add(oReader["FEESNM"].ToString());
            }
            if (conn.State != ConnectionState.Closed) conn.Close();
            return CompletionSet.ToArray();
        }
        private void gridShow()
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            if (conn.State != ConnectionState.Open) conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT    EIM_TRANS.FEESID, EIM_FEES.FEESNM, EIM_TRANS.AMOUNT,  EIM_TRANS.REMARKS
                                              FROM         EIM_FEES INNER JOIN
                                              EIM_TRANS ON EIM_FEES.FEESID = EIM_TRANS.FEESID WHERE TRANSNO='" + txtTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed) conn.Close();
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

            if (conn.State != ConnectionState.Open) conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  EIM_TRANS.FEESID, EIM_FEES.FEESNM, EIM_TRANS.AMOUNT, EIM_TRANS.REMARKS
                                             FROM         EIM_FEES INNER JOIN
                                             EIM_TRANS ON EIM_FEES.FEESID = EIM_TRANS.FEESID WHERE TRANSNO='" + ddlTransNO.Text + "' AND TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed) conn.Close();
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
            txtStuID.Text = ""; txtStuIDNew.Text = "";
            txtPODDNO.Text = "";
            txtPODT.Text = "";
            txtPOBNK.Text = "";
            txtPOBRNC.Text = "";
            txtRemarks.Text = "";
            txtStuNM.Text = "";
            Session["PROGRAMID"] = "";
            Session["FEESID"] = "";
            txtAcNM.Text = "";
            lblAccNO.Text = "";

        }

        protected void gv_Trans_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                TextBox txtFEESNMFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtFEESNMFooter");
                TextBox txtAMOUNTFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtAMOUNTFooter");
                //TextBox txtVatFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtVatFooter");
                TextBox txtREMARKSFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtREMARKSFooter");
                Label lblFEESIDFooter = (Label)gv_Trans.FooterRow.FindControl("lblFEESIDFooter");

                iob.UserID = Session["UserName"].ToString();
                iob.Ipaddress = Session["IpAddress"].ToString();
                iob.PcName = Session["PCName"].ToString();
                iob.InTime = Global.Dayformat1(DateTime.Now);
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
                        else if (txtStuIDNew.Text == "")
                        {
                            txtStuIDNew.Focus();
                        }
                        else if (txtStuID.Text == "")
                        {
                            txtStuIDNew.Focus();
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
                            if (conn.State != ConnectionState.Open) conn.Open();
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
                            if (conn.State != ConnectionState.Closed) conn.Close();
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
                                    iob.TransFor = "COLLACTION";
                                    iob.TrnsNO = int.Parse(txtTransNO.Text);
                                    iob.RegYR = int.Parse(ddlRegYR.Text);
                                    iob.SemID = int.Parse(ddlSemNM.Text);
                                    iob.ProgID = ddlProgNM.Text;
                                    iob.StuID = txtStuID.Text;
                                    //iob.VatAmount = Decimal.Parse(txtVatFooter.Text);
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
                                    if (txtAMOUNTFooter.Text == "")
                                        txtAMOUNTFooter.Text = "0.00";
                                    iob.Amnt = Decimal.Parse(txtAMOUNTFooter.Text);
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

                                    if (conn.State != ConnectionState.Open) conn.Open();
                                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' and TRANSNO ='" + ddlTransNO.Text + "' AND TRANSTP='MREC'", conn);
                                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                    DataSet ds1 = new DataSet();
                                    da1.Fill(ds1);
                                    if (conn.State != ConnectionState.Closed) conn.Close();
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
                                        iob.SemID = int.Parse(ddlSemNM.Text);
                                        //iob.VatAmount = Decimal.Parse(txtVatFooter.Text);
                                        iob.ProgID = ddlProgNM.Text;
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
                                        if (txtAMOUNTFooter.Text == "")
                                            txtAMOUNTFooter.Text = "0.00";
                                        iob.Amnt = Decimal.Parse(txtAMOUNTFooter.Text);
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
                                iob.SemID = int.Parse(ddlSemNM.Text);
                                //iob.VatAmount = Decimal.Parse(txtVatFooter.Text);
                                iob.ProgID = ddlProgNM.Text;
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
                                if (txtAMOUNTFooter.Text == "")
                                    txtAMOUNTFooter.Text = "0.00";
                                iob.Amnt = Decimal.Parse(txtAMOUNTFooter.Text);
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
                        else if (txtStuIDNew.Text == "")
                        {
                            txtStuIDNew.Focus();
                        }
                        else if (txtStuID.Text == "")
                        {
                            txtStuIDNew.Focus();
                        }
                        else if (lblFEESIDFooter.Text == "")
                        {
                            txtFEESNMFooter.Focus();
                        }
                        else
                        {
                            if (conn.State != ConnectionState.Open) conn.Open();
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
                            if (conn.State != ConnectionState.Closed) conn.Close();
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
                                    iob.SemID = int.Parse(ddlSemNM.Text);
                                    // iob.VatAmount = Decimal.Parse(txtVatFooter.Text);
                                    iob.ProgID = ddlProgNM.Text;
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
                                    if (txtAMOUNTFooter.Text == "")
                                        txtAMOUNTFooter.Text = "0.00";
                                    iob.Amnt = Decimal.Parse(txtAMOUNTFooter.Text);
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

                                    if (conn.State != ConnectionState.Open) conn.Open();
                                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' and TRANSNO ='" + ddlTransNO.Text + "' AND TRANSTP='MREC'", conn);
                                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                    DataSet ds1 = new DataSet();
                                    da.Fill(ds1);
                                    if (conn.State != ConnectionState.Closed) conn.Close();
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
                                        iob.SemID = int.Parse(ddlSemNM.Text);
                                        // iob.VatAmount = Decimal.Parse(txtVatFooter.Text);
                                        iob.ProgID = ddlProgNM.Text;
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
                                        if (txtAMOUNTFooter.Text == "")
                                            txtAMOUNTFooter.Text = "0.00";
                                        iob.Amnt = Decimal.Parse(txtAMOUNTFooter.Text);

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
                                iob.SemID = int.Parse(ddlSemNM.Text);
                                //iob.VatAmount = Decimal.Parse(txtVatFooter.Text);
                                iob.ProgID = ddlProgNM.Text;
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
                                if (txtAMOUNTFooter.Text == "")
                                    txtAMOUNTFooter.Text = "0.00";
                                iob.Amnt = Decimal.Parse(txtAMOUNTFooter.Text);
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
                            txtStuIDNew.Focus();
                        }
                        else if (txtStuIDNew.Text == "")
                        {
                            txtStuIDNew.Focus();
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
                            if (conn.State != ConnectionState.Open) conn.Open();
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
                            if (conn.State != ConnectionState.Closed) conn.Close();
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
                                    iob.SemID = int.Parse(ddlSemNM.Text);
                                    //iob.VatAmount = Decimal.Parse(txtVatFooter.Text);
                                    iob.ProgID = ddlProgNM.Text;
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
                                    if (txtAMOUNTFooter.Text == "")
                                        txtAMOUNTFooter.Text = "0.00";
                                    iob.Amnt = Decimal.Parse(txtAMOUNTFooter.Text);
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

                                    if (conn.State != ConnectionState.Open) conn.Open();
                                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' and TRANSNO ='" + ddlTransNO.Text + "' AND TRANSTP='MREC'", conn);
                                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                    DataSet ds1 = new DataSet();
                                    da.Fill(ds1);
                                    if (conn.State != ConnectionState.Closed) conn.Close();
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
                                        iob.SemID = int.Parse(ddlSemNM.Text);
                                        //iob.VatAmount = Decimal.Parse(txtVatFooter.Text);
                                        iob.ProgID = ddlProgNM.Text;
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
                                        if (txtAMOUNTFooter.Text == "")
                                            txtAMOUNTFooter.Text = "0.00";
                                        iob.Amnt = Decimal.Parse(txtAMOUNTFooter.Text);
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
                                iob.SemID = int.Parse(ddlSemNM.Text);
                                iob.ProgID = ddlProgNM.Text;
                                iob.StuID = txtStuID.Text;
                                // iob.VatAmount = Decimal.Parse(txtVatFooter.Text);
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
                                if (txtAMOUNTFooter.Text == "")
                                    txtAMOUNTFooter.Text = "0.00";
                                iob.Amnt = Decimal.Parse(txtAMOUNTFooter.Text);
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
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void txtStuID_TextChanged(object sender, EventArgs e)
        {
            if (txtStuID.Text.Length == 11)
            {
                string progID = txtStuID.Text.Substring(5, 2);
                string SemIDs = txtStuID.Text.Substring(4, 1);
                string Year = txtStuID.Text.Substring(0, 4);
                ddlProgNM.Text = progID;
                ddlSemNM.Text = SemIDs;

                lblmsg1.Visible = false;
                Session["PROGRAMID"] = progID;
                Session["SEMESTERID"] = SemIDs;
                Session["YEAR"] = Year;
                ddlRegYR.Text = Year;
                //if (SemIDs == "1")
                //    ddlSemNM.SelectedIndex = 1;
                //else if (SemIDs == "2")
                //    ddlSemNM.SelectedIndex = 2;
                //else
                //    ddlSemNM.SelectedIndex = -1;
                //ddlProgNM.Text = Global.GetData("SELECT PROGRAMNM FROM EIM_PROGRAM WHERE PROGRAMID='" + progID + "'");
                ddlBatch.Text = Global.GetData("SELECT BATCH FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtStuIDNew.Text + "'");
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE STUDENTID='" + txtStuID.Text + "'", txtStuNM);
                // ddlRegYR.Text=
                //ddlRegYR.Text = txtStuID.Text.Substring(0, 5); 
                if (txtStuNM.Text == "")
                {
                    lblmsg1.Visible = true;
                    lblmsg1.Text = "Invalid Student ID For Selected Year Semester And Program";
                    txtStuID.Text = "";
                    txtStuIDNew.Text = "";
                    txtStuIDNew.Focus();
                }
                else
                    txtAcNM.Focus();
            }
            else
            {
                lblmsg1.Visible = true;
                lblmsg1.Text = "Invalid Student ID";
            }
        }
        protected void txtStuIDNew_TextChanged(object sender, EventArgs e)
        {
            if (txtStuIDNew.Text.Length == 12)
            {
                txtStuID.Text = Global.GetData("SELECT STUDENTID FROM EIM_STUDENT WHERE NEWSTUDENTID='" + txtStuIDNew.Text + "'");
                txtStuID_TextChanged(sender, e);

            }
            else
            {
                lblmsg1.Visible = true;
                lblmsg1.Text = "Invalid Student ID";
            }
        }
        protected void txtStuID1_TextChanged(object sender, EventArgs e)
        {
            lblmsg1.Visible = false;
            lblMSG.Text = "";
            if (txtStuID.Text == "")
            {
                txtStuIDNew.Focus();
            }
            else
            {

                string PROGRAMID = HttpContext.Current.Session["PROGRAMID"].ToString();
                string SemID = HttpContext.Current.Session["SEMESTERID"].ToString();
                string YEAR = HttpContext.Current.Session["YEAR"].ToString();
                txtStuNM.Text = "";
                Global.txtAdd("SELECT STUDENTNM FROM EIM_STUDENT WHERE STUDENTID='" + txtStuID.Text + "' AND PROGRAMID='" + PROGRAMID + "' AND SEMESTERID='" + SemID + "' AND ADMITYY='" + YEAR + "'", txtStuNM);
                // ddlRegYR.Text=
                //ddlRegYR.Text = txtStuID.Text.Substring(0, 5); 
                txtAcNM.Focus();
                if (txtStuNM.Text == "")
                {
                    lblmsg1.Visible = true;
                    lblmsg1.Text = "Invalid Student ID For Selected Year Semester And Program";
                    txtStuID.Text = "";
                    txtStuIDNew.Focus();
                }

            }

        }

        protected void ddlSemNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblMSG.Text = "";
            //if (ddlSemNM.Text == "Select")
            //{
            //    ddlSemNM.Focus();
            //}
            //else
            //{
            //    Global.lblAdd("SELECT SEMESTERID FROM EIM_SEMESTER WHERE SEMESTERNM='" + ddlSemNM.Text + "'", lblSemID);
            //    Session["SEMESTERID"] = lblSemID.Text;
            //    ddlProgNM.Focus();
            //}
        }

        protected void ddlProgNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtStuID.Text = ""; txtStuIDNew.Text = "";
            if (ddlProgNM.Text == "Select")
            {
                ddlProgNM.Focus();
            }
            else
            {
                ///Global.lblAdd("SELECT PROGRAMID FROM EIM_PROGRAM WHERE PROGRAMNM='" + ddlProgNM.Text + "'", lblProID);
                Session["PROGRAMID"] = ddlProgNM.Text;
                Session["BATCH"] = ddlBatch.Text;
                string semid = Global.GetData("SELECT SEMESTERID FROM EIM_STUDENT WHERE BATCH='" + ddlBatch.Text + "' AND PROGRAMID='" + ddlProgNM.Text + "'");
                if (semid == "")
                    semid = "--SELECT--";
                ddlSemNM.Text = semid;
                txtStuIDNew.Focus();
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
                DateTime transdate = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                SqlCommand cmd = new SqlCommand("SELECT * FROM EIM_TRANS WHERE  TRANSYY= '" + txtYR.Text + "' AND TRANSTP='MREC' AND TRANSNO = '" + TransNo + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (conn.State != ConnectionState.Closed) conn.Close();
                if (ds.Tables[0].Rows.Count > 1)
                {
                    lblMSG.Visible = false;
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlCommand cmd1 = new SqlCommand("DELETE FROM EIM_TRANS WHERE FEESID ='" + lblFEESID.Text + "' AND TRANSTP='MREC' AND TRANSYY = '" + txtYR.Text + "' AND TRANSNO='" + TransNo + "'", conn);
                    cmd1.ExecuteNonQuery();
                    if (conn.State != ConnectionState.Closed) conn.Close();
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
                    if (conn.State != ConnectionState.Open) conn.Open();
                    SqlCommand cmd1 = new SqlCommand("DELETE FROM EIM_TRANS WHERE TRANSNO ='" + TransNo + "' AND FEESID ='" + lblFEESID.Text + "' AND TRANSTP='MREC' AND TRANSYY = '" + txtYR.Text + "'", conn);
                    cmd1.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("DELETE FROM EIM_TRANSMST WHERE TRANSNO ='" + TransNo + "'  AND TRANSTP='MREC' AND TRANSYY='" + txtYR.Text + "'", conn);
                    cmd2.ExecuteNonQuery();
                    if (conn.State != ConnectionState.Closed) conn.Close();
                    ddlTransNO.SelectedIndex = -1;
                    ddlTransNO.Focus();
                    clear();
                    gridShowEdit();
                    Global.dropDownAddTrans(ddlTransNO, "SELECT TRANSNO FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC'");
                    ddlTransNO.Focus();
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
                Global.dropDownAddTrans(ddlTransNO, "SELECT DISTINCT TRANSNO FROM EIM_TRANSMST WHERE TRANSYY='" + txtYR.Text + "' AND TRANSTP='MREC' ORDER BY TRANSNO");
                clear();
                gridShowEdit();
                ddlTransNO.Focus();
                txtPOBNK.Enabled = true;
                txtPOBRNC.Enabled = true;
                txtPODDNO.Enabled = true;
                txtPODT.Enabled = true;
                ddlRegYR.Enabled = false;
                // txtDT.Enabled = false;
                ddlTransNO.Focus();
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
                txtStuIDNew.Focus();
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
                string Script = @"SELECT     CONVERT(NVARCHAR(10),dbo.EIM_TRANSMST.TRANSDT,103) TRANSDT, dbo.EIM_TRANSMST.REGYY, dbo.EIM_TRANSMST.TRANSYY, dbo.EIM_SEMESTER.SEMESTERNM, dbo.EIM_TRANSMST.SEMESTERID, 
                      dbo.EIM_PROGRAM.PROGRAMNM, dbo.EIM_TRANSMST.PROGRAMID, dbo.EIM_STUDENT.STUDENTNM, dbo.EIM_TRANSMST.STUDENTID,  dbo.EIM_STUDENT.NEWSTUDENTID,dbo.EIM_STUDENT.BATCH, 
                      dbo.EIM_TRANSMST.CNBCD, dbo.EIM_TRANSMST.PONO, dbo.EIM_TRANSMST.PODT, dbo.EIM_TRANSMST.POBANK, dbo.EIM_TRANSMST.POBANKBR, dbo.EIM_TRANSMST.REMARKS
                      FROM dbo.EIM_STUDENT INNER JOIN
                      dbo.EIM_TRANSMST INNER JOIN
                      dbo.EIM_SEMESTER ON dbo.EIM_TRANSMST.SEMESTERID = dbo.EIM_SEMESTER.SEMESTERID INNER JOIN
                      dbo.EIM_PROGRAM ON dbo.EIM_TRANSMST.PROGRAMID = dbo.EIM_PROGRAM.PROGRAMID ON dbo.EIM_STUDENT.STUDENTID = dbo.EIM_TRANSMST.STUDENTID 
                      WHERE EIM_TRANSMST.TRANSNO='" + ddlTransNO.Text + "' AND EIM_TRANSMST.TRANSYY='" + txtYR.Text + "' AND EIM_TRANSMST.TRANSTP='MREC'";
                if (conn.State != ConnectionState.Open) conn.Open();
                SqlCommand cmd = new SqlCommand(Script, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDT.Text = dr["TRANSDT"].ToString();
                    ddlRegYR.Text = dr["REGYY"].ToString();
                    lblYR.Text = dr["TRANSYY"].ToString();
                    ddlSemNM.Text = dr["SEMESTERID"].ToString();
                    //ddlSemNM.Text = dr["SEMESTERNM"].ToString();
                    ddlProgNM.Text = dr["PROGRAMID"].ToString();
                    ddlBatch.Text = dr["BATCH"].ToString();
                    txtStuID.Text = dr["STUDENTID"].ToString();
                    txtStuIDNew.Text = dr["NEWSTUDENTID"].ToString();
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
                if (conn.State != ConnectionState.Closed) conn.Close();
                gridShowEdit();
                string Acc = lblAccNO.Text;
                string Sub = "";
                if (Acc != "")
                    Sub = Acc.Substring(0, 7);
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
                txtStuIDNew.Focus();
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
            else
            {
                try
                {
                    lblError.Visible = false;
                    iob.UPDUserID = Session["UserName"].ToString();
                    iob.UPDIpaddress = Session["IpAddress"].ToString();
                    iob.UPDPcName = Session["PCName"].ToString();
                    iob.UPDTime = Global.Dayformat1(DateTime.Now);
                    DateTime Date = DateTime.Parse(txtDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    // iob.FeesID = lblFEESIDEdit.Text; 

                    iob.TransFor = "";
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
                        lblError.Visible = true;
                        lblError.Text = "Successfully Updated !";
                    }
                    else
                    {
                        lblError.Visible = true;
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
                //TextBox txtVatEdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtVatEdit");
                TextBox txtREMARKSEdit = (TextBox)gv_Trans.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                Label lblFEESIDEdit = (Label)gv_Trans.Rows[e.RowIndex].FindControl("lblFEESIDEdit");
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
                else if (txtStuID.Text == "")
                {
                    txtStuIDNew.Focus();
                }
                else if (txtStuIDNew.Text == "")
                {
                    txtStuIDNew.Focus();
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
                    iob.TrnsDT = Date;
                    iob.TransYR = int.Parse(txtYR.Text);
                    iob.RegYR = int.Parse(ddlRegYR.Text);
                    iob.SemID = int.Parse(ddlSemNM.Text);
                    iob.ProgID = ddlProgNM.Text;
                    iob.StuID = txtStuID.Text;
                    //iob.VatAmount = Decimal.Parse(txtVatEdit.Text);
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
                    if (txtAMOUNTEdit.Text == "")
                        txtAMOUNTEdit.Text = "0.00";
                    iob.Amnt = Decimal.Parse(txtAMOUNTEdit.Text);
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
                Global.lblAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTNM='" + txtAcNM.Text + "'", lblAccNO);
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
            //TextBox txtVatFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtVatFooter");
            TextBox txtREMARKSFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtREMARKSFooter");
            TextBox txtTotalFooter = (TextBox)gv_Trans.FooterRow.FindControl("txtTotalFooter");
            if (txtAMOUNTFooter.Text == "")
            {
                txtAMOUNTFooter.Focus();
            }
            else
            {
                //double amount = Convert.ToDouble(txtAMOUNTFooter.Text);
                //double Vat = Convert.ToDouble(txtAMOUNTFooter.Text);
                //double a = 7.5;
                //amount = ((amount * a) / 100);
                //txtVatFooter.Text = amount.ToString("F2");
                //txtTotalFooter.Text = (Vat + amount).ToString("F2");
                txtREMARKSFooter.Focus();
            }
        }

        protected void txtAMOUNTEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtAMOUNTEdit = (TextBox)row.FindControl("txtAMOUNTEdit");
            // TextBox txtVatEdit = (TextBox)row.FindControl("txtVatEdit");
            TextBox txtREMARKSEdit = (TextBox)row.FindControl("txtREMARKSEdit");
            TextBox txtTotalEdit = (TextBox)row.FindControl("txtTotalEdit");
            if (txtAMOUNTEdit.Text == "")
            {
                txtAMOUNTEdit.Focus();
            }
            else
            {
                //double amount = Convert.ToDouble(txtAMOUNTEdit.Text);
                //double Vat = Convert.ToDouble(txtAMOUNTEdit.Text);
                //double a = 7.5;
                //amount = ((amount * a) / 100);
                //txtVatEdit.Text = amount.ToString("F2");
                //txtTotalEdit.Text = (Vat + amount).ToString("F2");
                txtREMARKSEdit.Focus();
            }
        }

        protected void btnPrint0_Click(object sender, EventArgs e)
        {
            clear();
            if (btnEdit.Text == "EDIT")
            {
                gridShowIDCreate();
                ddlSemNM.Focus();
            }
            else
            {

                ddlTransNO.SelectedIndex = -1;

                gridShowEdit();
                ddlTransNO.Focus();
            }
        }

        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProgNM.SelectedIndex = -1;
        }
    }
}
