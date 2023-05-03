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
using AlchemyAccounting.Accounts.DataAccess;
using AlchemyAccounting.Accounts.Interface;

namespace AlchemyAccounting.Accounts.UI
{
    public partial class EditSingleVoucher : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        public string prefixText { get; set; }

        public int count { get; set; }

        public string contextKey { get; set; }

       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {

                if (Session["UserTp"].ToString() == "ADMIN")
                {
                    if (!Page.IsPostBack)
                    {
                        string user = Session["UserName"].ToString();
                        Global.lblAdd("SELECT PerEd FROM User_Registration WHERE UserID ='" + user + "'", lblEdit);

                        Global.lblAdd("SELECT PerDel FROM User_Registration WHERE UserID ='" + user + "'", lblDelete);

                        ddlEditTransType.AutoPostBack = true;
                        DateTime today = DateTime.Today.Date;
                        string td = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                        txtEdDate.Text = td;
                        ddlEditTransType.Focus();
                    }
                }
                else
                    Response.Redirect("~/Permission_form.aspx");
            }

        }

        protected void ddlEditTransType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                Session["Transtype"] = "";
                if (ddlEditTransType.Text == "MREC")
                {
                    Session["Transtype"] = ddlEditTransType.Text;
                    gvDetails.Visible = false;
                }
                else if (ddlEditTransType.Text == "MPAY")
                {
                    Session["Transtype"] = ddlEditTransType.Text;
                    gvDetails.Visible = false;
                }
                else if (ddlEditTransType.Text == "JOUR")
                {
                    Session["Transtype"] = ddlEditTransType.Text;
                    gvDetails.Visible = false;
                }
                else if (ddlEditTransType.Text == "CONT")
                {
                    Session["Transtype"] = ddlEditTransType.Text;
                    gvDetails.Visible = false;
                }
                else
                {
                    return;
                }
                btnSearch.Focus();
            }

        }
        public void ShowGrid()
        {
            lblDebitCD.Text = "";
            lblCreditCD.Text = "";

            DateTime eddate = DateTime.Parse(txtEdDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string edate = eddate.ToString("yyyy/MM/dd");


            
            SqlConnection conn = new SqlConnection(Global.connection);

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT GL_STRANS.TRANSNO, GL_ACCHART.ACCOUNTNM AS DEBITCD, GL_STRANS.DEBITCD AS DRCD, GL_ACCHART_1.ACCOUNTNM AS CREDITCD, " +
                      " GL_STRANS.CREDITCD AS CRCD, GL_STRANS.CHEQUENO, CASE WHEN CONVERT(DATE, CHEQUEDT) = '01-01-1900' THEN '' ELSE CONVERT(nvarchar(10), CHEQUEDT, 103) END AS CHEQUEDT_CON, GL_STRANS.AMOUNT, GL_STRANS.REMARKS, GL_STRANS.TRANSFOR, GL_COSTP.COSTPNM, GL_STRANS.TRANSMODE, GL_STRANS.COSTPID " +
                      " FROM GL_STRANS INNER JOIN GL_ACCHART ON GL_STRANS.DEBITCD = GL_ACCHART.ACCOUNTCD INNER JOIN GL_ACCHART AS GL_ACCHART_1 ON GL_STRANS.CREDITCD = GL_ACCHART_1.ACCOUNTCD LEFT OUTER JOIN GL_COSTP ON GL_STRANS.COSTPID = GL_COSTP.COSTPID " +
                      " WHERE (GL_STRANS.TRANSTP = '" + ddlEditTransType.Text + "') AND (GL_STRANS.TRANSDT = '" + edate + "')", conn);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                if (txtEdDate.Text == "")
                {
                    Response.Write("<script>alert('Select a Date?');</script>");
                }
                else if (ddlEditTransType.Text == "Select")
                {
                    Response.Write("<script>alert('Select Transaction Type?');</script>");
                }
                else
                {
                    ShowGrid();
                }
            }
        }

        /// <summary>
        /// AutoComplete
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListDebit(string prefixText, int count, string contextKey)
        {
            string Transtype = HttpContext.Current.Session["Transtype"].ToString();
            
            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            if (Transtype == "MREC")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103')  AND STATUSCD = 'P' and ACCOUNTNM LIKE '" + prefixText + "%'");
            }

            else if (Transtype == "MPAY")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE '" + prefixText + "%'");
            }
            else if (Transtype == "JOUR")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE '" + prefixText + "%'");
            }
            else if (Transtype == "CONT")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE '" + prefixText + "%'");
            }
            else
            {
                Transtype = "";
            }

            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListCredit(string prefixText, int count, string contextKey)
        {
            string Transtype = HttpContext.Current.Session["Transtype"].ToString();
            
            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            if (Transtype == "MREC")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE '" + prefixText + "%'");
            }

            else if (Transtype == "MPAY")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE  substring(ACCOUNTCD,1,7)  in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE '" + prefixText + "%'");
            }
            else if (Transtype == "JOUR")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) not in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE '" + prefixText + "%'");
            }
            else if (Transtype == "CONT")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD = 'P' and ACCOUNTNM LIKE '" + prefixText + "%'");
            }
            else
            {
                Transtype = "";
            }

            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();

        }


        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            ShowGrid();
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow oItem = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            index = oItem.RowIndex;

            if (e.CommandName.Equals("print"))
            {
                if (Session["UserName"]==null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    Session["TransType"] = ddlEditTransType.Text;
                    Session["TransDate"] = txtEdDate.Text;
                    Label Voucher = (Label)gvDetails.Rows[index].Cells[1].FindControl("lblTransNo");
                    Session["VouchNo"] = Voucher.Text;
                    Label DBCD = (Label)gvDetails.Rows[index].Cells[3].FindControl("lblDRCD");

                    Session["DebitCD"] = DBCD.Text;
                    Label CRCD = (Label)gvDetails.Rows[index].Cells[5].FindControl("lblCreditCD");
                    Session["CreditCD"] = CRCD.Text;

                    Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/Report/RptCreditVoucherEdit.aspx','_newtab');", true);

                    //Response.Redirect("~/Accounts/Report/RptCreditVoucherEdit.aspx");
                }
            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            ShowGrid();

            DateTime eddate = DateTime.Parse(txtEdDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string edate = eddate.ToString("yyyy/MM/dd");

            DropDownList ddlTransFor = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlTransFor");
            DropDownList ddlTransMode = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlTransMode");
            Label lblTransNo = (Label)gvDetails.Rows[e.NewEditIndex].FindControl("lblTransNo");
            Global.lblAdd("SELECT TRANSFOR FROM GL_STRANS WHERE TRANSDT ='" + edate + "' AND TRANSTP ='" + ddlEditTransType.Text + "' AND TRANSNO =" + lblTransNo.Text + "", lblTransFor);
            ddlTransFor.Text = lblTransFor.Text;
            Global.lblAdd("SELECT TRANSMODE FROM GL_STRANS WHERE TRANSDT ='" + edate + "' AND TRANSTP ='" + ddlEditTransType.Text + "' AND TRANSNO =" + lblTransNo.Text + "", lblTransMode);
            ddlTransMode.Text = lblTransMode.Text;
            ddlTransFor.Focus();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                
                SqlConnection conn = new SqlConnection(Global.connection);
                string userName = Session["UserName"].ToString();
                DateTime eddate = DateTime.Parse(txtEdDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string edate = eddate.ToString("yyyy/MM/dd");
                DropDownList ddlTransFor = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlTransFor");
                Label lblCostPoolIDEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblCostPoolIDEdit");
                DropDownList ddlTransMode = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlTransMode");

                TextBox DebitCode = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtDbCd");
                TextBox CreditCode = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCrCd");

                TextBox txtChq = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtChq");
                TextBox txtCqDt = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCqDt");
                DateTime CQDT = new DateTime();
                string cqdt;
                if (txtChq.Text == "")
                {
                    CQDT = DateTime.Parse("01/01/1900 00:00:00", dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    cqdt = CQDT.ToString("yyyy/MM/dd");
                }
                else
                {
                    CQDT = DateTime.Parse(txtCqDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    cqdt = CQDT.ToString("yyyy/MM/dd");
                }
                TextBox txtAmount = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAmount");
                TextBox txtRemarks = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarks");
                string Remarks = txtRemarks.Text;
                Label lblTransNo = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblTransNo");

                if (DebitCode.Text == "")
                {
                    Response.Write("<script>alert('Select Debit Head.');</script>");
                    DebitCode.Focus();
                }
                else if (CreditCode.Text == "")
                {
                    Response.Write("<script>alert('Select Credit Head.');</script>");
                    CreditCode.Focus();
                }
                else
                {

                    Global.lblAdd(@"Select ACCOUNTCD from GL_ACCHART where STATUSCD = 'P' and ACCOUNTNM = '" + DebitCode.Text + "'", lblDebitCD);

                    Global.lblAdd(@"Select ACCOUNTCD from GL_ACCHART where STATUSCD = 'P' and ACCOUNTNM = '" + CreditCode.Text + "'", lblCreditCD);

                    if (conn.State != ConnectionState.Open)conn.Open();
                    SqlCommand cmd = new SqlCommand("update GL_STRANS set TRANSFOR ='" + ddlTransFor.Text + "', COSTPID ='" + lblCostPoolIDEdit.Text + "', TRANSMODE ='" + ddlTransMode.Text + "', DEBITCD='" + lblDebitCD.Text + "', CREDITCD = '" + lblCreditCD.Text + "', CHEQUENO= '" + txtChq.Text + "', CHEQUEDT ='" + cqdt + "', AMOUNT = '" + txtAmount.Text + "' ,REMARKS = @Remarks where TRANSTP = '" + ddlEditTransType.Text + "' and TRANSDT = '" + edate + "' and TRANSNO = '" + lblTransNo.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@Remarks", Remarks);
                    cmd.ExecuteNonQuery();
                    if (conn.State != ConnectionState.Closed)conn.Close();
                    Response.Write("<script>alert('Successfully Updated');</script>");
                    gvDetails.EditIndex = -1;
                    ShowGrid();
                    lblErrMsg.Visible = false;
                }
            }
        }

        protected void txtDbCd_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox otherDB = (TextBox)row.FindControl("txtDbCd");

            Global.lblAdd(@"Select ACCOUNTCD from GL_ACCHART where STATUSCD = 'P' and ACCOUNTNM = '" + otherDB.Text + "'", lblDebitCD);
        }

        protected void txtCrCd_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox otherCR = (TextBox)row.FindControl("txtCrCd");

            Global.lblAdd(@"Select ACCOUNTCD from GL_ACCHART where STATUSCD = 'P' and ACCOUNTNM = '" + otherCR.Text + "'", lblCreditCD);
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else if (lblDelete.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "You are not permited to continue this operation.";
            }
            else
            {
                lblErrMsg.Visible = false;

                
                SqlConnection conn = new SqlConnection(Global.connection);
                string userName = Session["UserName"].ToString();
                DateTime eddate = DateTime.Parse(txtEdDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string edate = eddate.ToString("yyyy/MM/dd");

                Label lblTransNo = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblTransNo");

                if (conn.State != ConnectionState.Open)conn.Open();
                SqlCommand cmd = new SqlCommand("delete from GL_STRANS where TRANSTP = '" + ddlEditTransType.Text + "' and TRANSDT = '" + edate + "' and TRANSNO = '" + lblTransNo.Text + "'", conn);
                cmd.ExecuteNonQuery();
                if (conn.State != ConnectionState.Closed)conn.Close();
                Response.Write("<script>alert('Successfully Deleted');</script>");
                ShowGrid();
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        public int index { get; set; }

        protected void txtEdDate_TextChanged(object sender, EventArgs e)
        {
            ddlEditTransType.Focus();
        }

        protected void ddlTransFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlTransFor = (DropDownList)row.FindControl("ddlTransFor");
            TextBox txtCostPoolNM = (TextBox)row.FindControl("txtCostPoolNM");
            Label lblCostPoolIDEdit = (Label)row.FindControl("lblCostPoolIDEdit");
            DropDownList ddlTransMode = (DropDownList)row.FindControl("ddlTransMode");
            if (ddlTransFor.Text == "OFFICIAL")
            {
                ddlTransMode.Focus();
                txtCostPoolNM.Text = "";
                txtCostPoolNM.Enabled = false;
                lblCostpoolID.Text = "";
                lblCostPoolIDEdit.Text = "";
                //ddlCostPID.AutoPostBack = false;
            }
            else
            {
                txtCostPoolNM.Focus();
                txtCostPoolNM.Enabled = true;
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListCostPool(string prefixText, int count, string contextKey)
        {
            
            SqlConnection conn = new SqlConnection(Global.connection);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT (GL_COSTP.COSTPNM + '|' + GL_COSTPMST.CATNM) AS COSTPNM FROM GL_COSTP INNER JOIN GL_COSTPMST ON GL_COSTP.CATID = GL_COSTPMST.CATID WHERE (GL_COSTP.COSTPNM + ' - ' + GL_COSTPMST.CATNM) LIKE '" + prefixText + "%' ", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["COSTPNM"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }

        protected void txtCostPoolNM_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCostPoolNM = (TextBox)row.FindControl("txtCostPoolNM");
            Label lblCostPoolIDEdit = (Label)row.FindControl("lblCostPoolIDEdit");
            DropDownList ddlTransMode = (DropDownList)row.FindControl("ddlTransMode");

            if (txtCostPoolNM.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Select Costpool name.";
                lblCostPoolIDEdit.Text = "";
                txtCostPoolNM.Focus();
            }
            else
            {
                lblErrMsg.Visible = false;

                string costpnm = "";
                string catnm = "";

                string searchPar = txtCostPoolNM.Text;
                int splitter = searchPar.IndexOf("|");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    costpnm = lineSplit[0];
                    catnm = lineSplit[1];

                    txtCostPoolNM.Text = costpnm.Trim();
                    lblCatNM.Text = catnm.Trim();
                    lblCostPoolIDEdit.Text = "";
                    Global.lblAdd(@" SELECT GL_COSTP.COSTPID FROM GL_COSTP INNER JOIN GL_COSTPMST ON GL_COSTP.CATID = GL_COSTPMST.CATID WHERE GL_COSTP.COSTPNM ='" + txtCostPoolNM.Text + "' AND GL_COSTPMST.CATNM ='" + lblCatNM.Text + "'", lblCostPoolIDEdit);
                    //txtCostPool.Text = costpnm + '|' + catnm;
                    ddlTransMode.Focus();
                }
                else
                {
                    txtCostPoolNM.Text = "";
                    lblCatNM.Text = "";
                    lblCostPoolIDEdit.Text = "";
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Select Costpool name.";
                    txtCostPoolNM.Focus();
                }
            }
        }

        protected void ddlTransMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlTransMode = (DropDownList)row.FindControl("ddlTransMode");
            TextBox txtChq = (TextBox)row.FindControl("txtChq");
            TextBox txtCqDt = (TextBox)row.FindControl("txtCqDt");
            TextBox txtDbCd = (TextBox)row.FindControl("txtDbCd");
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                if (ddlTransMode.Text == "CASH CHEQUE")
                {
                    txtChq.Enabled = true;
                    txtCqDt.Enabled = true;
                    txtDbCd.Focus();
                }
                else if (ddlTransMode.Text == "CHEQUE TRANSACTION")
                {
                    txtChq.Enabled = true;
                    txtCqDt.Enabled = true;
                    txtDbCd.Focus();
                }
                else
                {
                    txtChq.Enabled = false;
                    txtCqDt.Enabled = false;
                    txtDbCd.Focus();
                }
            }
        }

        //[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        //public static string[] GetCompletionListHdSR(string prefixText, int count, string contextKey)
        //{
        //    //string Transtype = HttpContext.Current.Session["Transtype"].ToString();
        //    
        //    SqlConnection conn = new SqlConnection(Global.connection);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd = new SqlCommand("", conn);

        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE STATUSCD = 'P' and ACCOUNTNM LIKE '" + prefixText + "%'");

        //    SqlDataReader oReader;
        //    if (conn.State != ConnectionState.Open)conn.Open();
        //    List<String> CompletionSet = new List<string>();
        //    oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    while (oReader.Read())
        //        CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
        //    return CompletionSet.ToArray();
        //}

        //public void ShowGrid_Searched()
        //{
        //    lblDebitCD.Text = "";
        //    lblCreditCD.Text = "";

        //    DateTime eddate = DateTime.Parse(txtEdDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //    string edate = eddate.ToString("yyyy/MM/dd");


        //    
        //    SqlConnection conn = new SqlConnection(Global.connection);

        //    if (conn.State != ConnectionState.Open)conn.Open();
        //    SqlCommand cmd = new SqlCommand("SELECT dbo.GL_STRANS.TRANSNO, dbo.GL_ACCHART.ACCOUNTNM AS DEBITCD, GL_STRANS.DEBITCD as DRCD, GL_ACCHART_1.ACCOUNTNM AS CREDITCD,GL_STRANS.CREDITCD as CRCD, dbo.GL_STRANS.CHEQUENO, " +
        //                                   " CASE WHEN CONVERT(DATE, CHEQUEDT) = '01-01-1900' THEN '' ELSE convert(nvarchar(10),CHEQUEDT,103) end as CHEQUEDT_CON, dbo.GL_STRANS.AMOUNT, dbo.GL_STRANS.REMARKS FROM " +
        //                                   " dbo.GL_STRANS INNER JOIN dbo.GL_ACCHART ON dbo.GL_STRANS.DEBITCD = dbo.GL_ACCHART.ACCOUNTCD INNER JOIN " +
        //                                   " dbo.GL_ACCHART AS GL_ACCHART_1 ON dbo.GL_STRANS.CREDITCD = GL_ACCHART_1.ACCOUNTCD  " +
        //                                   " where dbo.GL_STRANS.TRANSTP = '" + ddlEditTransType.Text + "' and dbo.GL_STRANS.TRANSDT = '" + edate + "'", conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    if (conn.State != ConnectionState.Closed)conn.Close();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvDetails.DataSource = ds;
        //        gvDetails.DataBind();
        //        gvDetails.Visible = true;
        //    }
        //    else
        //    {
        //        Response.Write("<script>alert('No Data Found');</script>");
        //        gvDetails.Visible = false;
        //    }
        //}

        //protected void txtAcHdNm_TextChanged(object sender, EventArgs e)
        //{
        //    Global.lblAdd(@"Select ACCOUNTCD from GL_ACCHART where STATUSCD = 'P' and ACCOUNTNM = '" + txtAcHdNm.Text + "'", lblSearchedCD);
        //}
    }
}