using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AlchemyAccounting.payroll.invoice
{
    public partial class InvoiceCreate : System.Web.UI.Page
    {

        SqlConnection conn;
        SqlCommand cmdd;

        InvoiceCreateDataAccess icda = new InvoiceCreateDataAccess();
        InvoiceCreateModel icm = new InvoiceCreateModel();

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    DateTime td = DateTime.Now;
                    txtBillDate.Text = td.ToString("dd/MM/yyyy");

                    string mon = td.ToString("MMM").ToUpper();
                    string year = td.ToString("yyyy");
                    txtYear.Text = year;
                    txtBillMY.Text = mon + "-" + year;

                    string user = HttpContext.Current.Session["UserName"].ToString();
                    Global.lblAdd("SELECT PerEd FROM User_Registration WHERE UserID ='" + user + "'", lblEdit);
                    if (lblEdit.Text == "Edit")
                        btnEdit.Visible = true;
                    else
                        btnEdit.Visible = false;

                    Global.lblAdd("SELECT PerDel FROM User_Registration WHERE UserID ='" + user + "'", lblDelete);

                    Global.lblAdd("select max(BILLNO) AS BILLNO from HR_BILLMST where BILLYY = " + txtYear.Text + " ", lblBillNO);

                    if (lblBillNO.Text == "")
                    {
                        txtBillno.Text = "1";
                    }
                    else
                    {
                        Int64 id = Convert.ToInt64(lblBillNO.Text) + 1;
                        txtBillno.Text = id.ToString();
                    }

                    ddlCompanyNM.Focus();

                    ShowGrid();
                }
            }
        }


        private void ShowGrid()
        {
            conn = new SqlConnection(Global.connection);
            conn.Open();

            Int64 billno = 0;
            Int16 year = 0;

            if (btnEdit.Text == "Edit")
            {
                billno = Convert.ToInt64(txtBillno.Text);
                year = Convert.ToInt16(txtYear.Text);
            }
            else
            {
                if (ddlbillNo.Text == "Select")
                    billno = 0;
                else
                    billno = Convert.ToInt64(ddlbillNo.Text);
                if (ddlyear.Text == "Select")
                    year = 0;
                else
                    year = Convert.ToInt16(ddlyear.Text);
            }

            cmdd = new SqlCommand("SELECT HR_BILL.BILLSL, HR_BILL.BILLNM, HR_BILL.TWORKER, HR_BILL.RATEPTP, HR_BILL.TOTQPTP, HR_BILL.AMTPTP FROM HR_BILL INNER JOIN HR_BILLMST ON HR_BILL.BILLNO = HR_BILLMST.BILLNO  WHERE HR_BILL.BILLNO=" + billno + " AND HR_BILL.BILLYY=" + year + "", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();


            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;

                if (gvDetails.EditIndex == -1)
                {
                    Decimal totAmt = 0;
                    Decimal a = 0;
                    foreach (GridViewRow grid in gvDetails.Rows)
                    {
                        Label Per = (Label)grid.Cells[5].FindControl("lblAmount");
                        if (Per.Text == "")
                        {
                            Per.Text = "0";
                        }
                        else
                        {
                            Per.Text = Per.Text;
                        }
                        String Perf = Per.Text;
                        totAmt = Convert.ToDecimal(Perf);
                        a += totAmt;
                        txtTotAmount.Text = a.ToString();
                    }
                    a += totAmt;

                    txtTotInWords.Text = "";
                    decimal dec;
                    Boolean ValidInput = Decimal.TryParse(txtTotAmount.Text, out dec);
                    if (!ValidInput)
                    {
                        txtTotInWords.ForeColor = System.Drawing.Color.Red;
                        txtTotInWords.Text = "Enter the Proper Amount...";
                        return;
                    }
                    if (txtTotAmount.Text.ToString().Trim() == "")
                    {
                        txtTotInWords.ForeColor = System.Drawing.Color.Red;
                        txtTotInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtTotAmount.Text) == 0)
                        {
                            txtTotInWords.ForeColor = System.Drawing.Color.Red;
                            txtTotInWords.Text = "Amount Cannot Be Empty...";
                            return;
                        }
                    }

                    string x1 = "";
                    string x2 = "";

                    if (txtTotAmount.Text.Contains("."))
                    {
                        x1 = txtTotAmount.Text.ToString().Trim().Substring(0, txtTotAmount.Text.ToString().Trim().IndexOf("."));
                        x2 = txtTotAmount.Text.ToString().Trim().Substring(txtTotAmount.Text.ToString().Trim().IndexOf(".") + 1);
                    }
                    else
                    {
                        x1 = txtTotAmount.Text.ToString().Trim();
                        x2 = "00";
                    }

                    if (x1.ToString().Trim() != "")
                    {
                        x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
                    }
                    else
                    {
                        x1 = "0";
                    }

                    txtTotAmount.Text = x1 + "." + x2;

                    if (x2.Length > 2)
                    {
                        txtTotAmount.Text = Math.Round(Convert.ToDouble(txtTotAmount.Text), 2).ToString().Trim();
                    }

                    string AmtConv = SpellAmount.MoneyConvFn(txtTotAmount.Text.ToString().Trim());
                    //string amntComma = SpellAmount.comma(Convert.ToDecimal(txtAmount.Text));
                    
                    //Label3.Text = amntComma;

                    txtTotAmount.Text = a.ToString("#,##0.00");

                    txtTotInWords.Text = AmtConv.Trim();
                    txtTotInWords.ForeColor = System.Drawing.Color.Green;
                    txtTotInWords.Focus();
                }
                else
                {

                }




                TextBox txtTotWorker = (TextBox)gvDetails.FooterRow.FindControl("txtTotWorker");

                if (ddlBillType.Text == "Meter")
                {

                    txtTotWorker.Enabled = false;
                    txtTotWorker.Text = "";
                    //gvDetails.FooterRow.Cells[3].Text = "Total Metter";
                    gvDetails.HeaderRow.Cells[3].Text = "Total Meter";
                    gvDetails.HeaderRow.Cells[4].Text = "Per Meter Rate";
                }

                else if (ddlBillType.Text == "Hour")
                {
                    txtTotWorker.Enabled = true;
                    gvDetails.HeaderRow.Cells[3].Text = "Total Hour";
                    gvDetails.HeaderRow.Cells[4].Text = "Rate Per Hour";

                }


                TextBox txtCategory = (TextBox)gvDetails.FooterRow.FindControl("txtCategory");
                txtCategory.Focus();

            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                gvDetails.Rows[0].Visible = false;


            }
        }

        private bool Previousdata(string id)
        {
            bool bflag = false;
            DataTable table = new DataTable();

            try
            {

                table = icda.ShowInvoiceInfo(id);
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



        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListParty(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);

            cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10202') and ACCOUNTNM LIKE '" + prefixText + "%' ", conn);

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListSiteID(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = ("SELECT COSTPNM FROM GL_COSTP WHERE COSTPNM LIKE '" + prefixText + "%'");


            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["COSTPNM"].ToString());
            return CompletionSet.ToArray();

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListBillMonth(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = ("SELECT DISTINCT BILLMY FROM HR_BILLMST WHERE BILLMY LIKE '" + prefixText + "%'");


            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["BILLMY"].ToString());
            return CompletionSet.ToArray();

        }


        protected void ddlCompanyNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBillDate.Focus();
        }

        protected void txtBillDate_TextChanged(object sender, EventArgs e)
        {
            DateTime td = Convert.ToDateTime(txtBillDate.Text);
            string year = td.ToString("yyyy");
            txtYear.Text = year;

            txtPrtyNM.Focus();
        }

        protected void txtPrtyNM_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd("select ACCOUNTCD from GL_ACCHART where ACCOUNTNM='" + txtPrtyNM.Text + "' ", txtPrtyID);
            txtBillMY.Focus();
        }

        public void MstSave()
        {
            TextBox txtCategory = (TextBox)gvDetails.FooterRow.FindControl("txtCategory");
            TextBox txtTotWorker = (TextBox)gvDetails.FooterRow.FindControl("txtTotWorker");
            TextBox txttothr = (TextBox)gvDetails.FooterRow.FindControl("txttothr");
            TextBox txtPrice = (TextBox)gvDetails.FooterRow.FindControl("txtPrice");
            TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");



            icm.COMPANYNM = ddlCompanyNM.Text;

            icm.BILLDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            if (btnEdit.Text == "Edit")
            {
                icm.BILLYY = Convert.ToInt64(txtYear.Text);

                Global.lblAdd("select MAX(BILLNO) from HR_BILLMST where BILLYY='" + txtYear.Text + "' ", lblBillNO);

                if (lblBillNO.Text == "")
                {
                    txtBillno.Text = "1";
                    icm.BILLNO = Convert.ToInt64(txtBillno.Text);
                }
                else
                {
                    Int64 id = Convert.ToInt64(lblBillNO.Text) + 1;
                    txtBillno.Text = id.ToString();
                    icm.BILLNO = Convert.ToInt64(txtBillno.Text);
                }
            }
            else
            {
                icm.BILLYY = Convert.ToInt64(ddlyear.Text);
                icm.BILLNO = Convert.ToInt64(ddlbillNo.Text);
            }

            icm.SUBMITPNM = txtSUBMITPNM.Text;
            icm.SUBMITPCNO = txtSUBMITPCNO.Text;
            icm.BILLMY = txtBillMY.Text;
            icm.PSID = txtPrtyID.Text;
            icm.COSTPID = txtSiteID.Text;
            icm.BILLTP = ddlBillType.Text;
            icm.InTm = DateTime.Now;
            icm.UserPc = HttpContext.Current.Session["PCName"].ToString();
            icm.Ip = HttpContext.Current.Session["IpAddress"].ToString();

            icda.MstInput(icm);
        }

        public void Save()
        {
            TextBox txtCategory = (TextBox)gvDetails.FooterRow.FindControl("txtCategory");
            TextBox txtTotWorker = (TextBox)gvDetails.FooterRow.FindControl("txtTotWorker");
            TextBox txttothr = (TextBox)gvDetails.FooterRow.FindControl("txttothr");
            TextBox txtPrice = (TextBox)gvDetails.FooterRow.FindControl("txtPrice");
            TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");




            icm.BILLMY = txtBillMY.Text;
            icm.COMPANYNM = ddlCompanyNM.Text;
            icm.BILLDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            if (btnEdit.Text == "Edit")
            {
                icm.BILLYY = Convert.ToInt64(txtYear.Text);
                icm.BILLNO = Convert.ToInt64(txtBillno.Text);
            }
            else
            {
                icm.BILLYY = Convert.ToInt64(ddlyear.Text);
                icm.BILLNO = Convert.ToInt64(ddlbillNo.Text);
            }
            icm.PSID = txtPrtyID.Text;
            icm.COSTPID = txtSiteID.Text;
            icm.BILLTP = ddlBillType.Text;

            icm.BILLSL = Convert.ToInt64(gvDetails.FooterRow.Cells[0].Text);
            icm.BILLNM = txtCategory.Text;
            if (txtTotWorker.Text == "")
            {
                icm.TWORKER = 0;
            }
            else
            {
                icm.TWORKER = Convert.ToInt64(txtTotWorker.Text);
            }

            icm.TOTQPTP = Convert.ToDecimal(txttothr.Text);
            icm.RATEPTP = Convert.ToDecimal(txtPrice.Text);
            icm.AMTPTP = Convert.ToDecimal(txtAmount.Text);

            icm.InTm = DateTime.Now;
            icm.UserPc = HttpContext.Current.Session["PCName"].ToString();
            icm.Ip = HttpContext.Current.Session["IpAddress"].ToString();

            icda.SaveInvoiceInfo(icm);



        }


        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            TextBox txtCategory = (TextBox)gvDetails.FooterRow.FindControl("txtCategory");
            TextBox txtTotWorker = (TextBox)gvDetails.FooterRow.FindControl("txtTotWorker");
            TextBox txttothr = (TextBox)gvDetails.FooterRow.FindControl("txttothr");
            TextBox txtPrice = (TextBox)gvDetails.FooterRow.FindControl("txtPrice");
            TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");

            if (e.CommandName.Equals("SaveCon"))
            {
                if (btnEdit.Text == "Edit")
                {
                    if (Previousdata(txtBillno.Text) == false)
                    {
                        if (txtPrtyID.Text == "")
                        {
                            lblErrMsgExist.Text = "Party Name Required";
                        }
                        else if (txtCostPid.Text == "")
                        {
                            lblErrMsgExist.Text = "Site Name Required";
                        }
                        else
                        {
                            MstSave();
                        }

                    }

                    if (txtPrtyID.Text == "")
                    {
                        lblErrMsgExist.Text = "Party Name Required";
                    }
                    else if (txtCostPid.Text == "")
                    {
                        lblErrMsgExist.Text = "Site Name Required";
                    }
                    else
                    {

                        Save();

                    }
                }
                else
                {
                    Save();
                }


                ShowGrid();

            }

            else if (e.CommandName.Equals("Complete"))
            {
                if (btnEdit.Text == "Edit")
                {
                    if (Previousdata(txtBillno.Text) == false)
                    {
                        if (txtPrtyID.Text == "")
                        {
                            lblErrMsgExist.Text = "Party Name Required";
                        }
                        else if (txtCostPid.Text == "")
                        {
                            lblErrMsgExist.Text = "Site Name Required";
                        }
                        else
                        {

                            MstSave();
                        }
                    }

                    if (txtPrtyID.Text == "")
                    {
                        lblErrMsgExist.Text = "Party Name Required";
                    }
                    else if (txtCostPid.Text == "")
                    {
                        lblErrMsgExist.Text = "Site Name Required";
                    }
                    else
                    {

                        Save();
                    }
                }
                else
                {
                    Save();
                }

                Refresh();

            }

            else if (e.CommandName.Equals("SavePrint"))
            {
                Session["companyname"] = null;
                Session["partyNM"] = null;
                Session["partyID"] = null;
                Session["billdt"] = null;

                Session["YY"] = null;
                Session["billno"] = null;
                Session["site"] = null;
                Session["billtp"] = null;
                Session["name"] = null;
                Session["contact"] = null;

                Session["companyname"] = ddlCompanyNM.Text;
                Session["partyNM"] = txtPrtyNM.Text;
                Session["partyID"] = txtPrtyID.Text;
                Session["billdt"] = txtBillDate.Text;
                if (btnEdit.Text == "Edit")
                {
                    Session["YY"] = txtYear.Text;
                    Session["billno"] = txtBillno.Text;
                }
                else
                {
                    Session["YY"] = ddlyear.Text;
                    Session["billno"] = ddlbillNo.Text;
                }
                Session["site"] = txtCostPid.Text;
                Session["billtp"] = ddlBillType.Text;
                Session["name"] = txtSUBMITPNM.Text;
                Session["contact"] = txtSUBMITPCNO.Text;

                if (btnEdit.Text == "Edit")
                {
                    if (Previousdata(txtBillno.Text) == false)
                    {
                        if (txtPrtyID.Text == "")
                        {
                            lblErrMsgExist.Text = "Party Name Required";
                        }
                        else if (txtCostPid.Text == "")
                        {
                            lblErrMsgExist.Text = "Site Name Required";
                        }
                        else
                        {
                            MstSave();
                        }

                    }

                    if (txtPrtyID.Text == "")
                    {
                        lblErrMsgExist.Text = "Party Name Required";
                    }
                    else if (txtCostPid.Text == "")
                    {
                        lblErrMsgExist.Text = "Site Name Required";
                    }
                    else
                    {

                        Save();



                    }
                }
                else
                {
                    Save();
                }

                Page.ClientScript.RegisterStartupScript(
                       this.GetType(), "OpenWindow", "window.open('../report/vis-report/rpt-Invoice.aspx','_newtab');", true);

                Refresh();

            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.Footer)
            {

                if (btnEdit.Text == "Edit")
                {
                    Global.lblAdd("select MAX(BILLSL) from HR_BILL where BILLNO=" + txtBillno.Text + " and BILLYY=" + txtYear.Text + " ", lblChkInternalID);

                    if (lblChkInternalID.Text == "")
                    {

                        e.Row.Cells[0].Text = "1";

                    }

                    else
                    {
                        Int64 id = Convert.ToInt64(lblChkInternalID.Text) + 1;
                        e.Row.Cells[0].Text = id.ToString();
                    }
                }
                else
                {
                    Global.lblAdd("select MAX(BILLSL) from HR_BILL where BILLNO=" + ddlbillNo.Text + " and BILLYY=" + ddlyear.Text + " ", lblChkInternalID);

                    if (lblChkInternalID.Text == "")
                    {

                        e.Row.Cells[0].Text = "1";

                    }

                    else
                    {
                        Int64 id = Convert.ToInt64(lblChkInternalID.Text) + 1;
                        e.Row.Cells[0].Text = id.ToString();
                    }
                }

            }
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            ShowGrid();
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblSL = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblSL");
            Label lblCategory = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblCategory");
            Label lblTotWorker = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblTotWorker");
            Label lbltothr = (Label)gvDetails.Rows[e.RowIndex].FindControl("lbltothr");
            Label lblPrice = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblPrice");
            Label lblAmount = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblAmount");


            if (txtPrtyID.Text == "")
            {
                lblErrMsgExist.Visible = true;
                lblErrMsgExist.Text = "particular input missing";
                txtPrtyNM.Focus();
            }
            else
            {
                conn = new SqlConnection(Global.connection);
                conn.Open();

                Int64 billno = 0;
                Int16 year = 0;

                if (btnEdit.Text == "Edit")
                {
                    billno = Convert.ToInt64(txtBillno.Text);
                    year = Convert.ToInt16(txtYear.Text);
                }
                else
                {
                    if (ddlbillNo.Text == "Select")
                        billno = 0;
                    else
                        billno = Convert.ToInt64(ddlbillNo.Text);
                    if (ddlyear.Text == "Select")
                        year = 0;
                    else
                        year = Convert.ToInt16(ddlyear.Text);
                }

                SqlCommand cmd1 = new SqlCommand("SELECT * FROM HR_BILL where BILLNO =" + billno + " AND BILLYY=" + year + " ", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                conn.Close();
                if (ds1.Tables[0].Rows.Count > 1)
                {


                    if (btnEdit.Text == "Edit")
                    {
                        icm.BILLDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        icm.BILLYY = Convert.ToInt64(txtYear.Text);
                        icm.BILLNO = Convert.ToInt64(txtBillno.Text);
                        icm.BILLSL = Convert.ToInt64(lblSL.Text);

                        icda.DeleteInvoiceInfo(icm);
                        ShowGrid();
                    }
                    else
                    {
                        if (lblDelete.Text == "")
                        {
                            lblErrMsgExist.Visible = true;
                            lblErrMsgExist.Text = "You are not permited to continue this operation.";
                        }
                        else
                        {
                            lblErrMsgExist.Visible = false;
                            icm.BILLDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            icm.BILLYY = Convert.ToInt64(ddlyear.Text);
                            icm.BILLNO = Convert.ToInt64(ddlbillNo.Text);
                            icm.BILLSL = Convert.ToInt64(lblSL.Text);

                            icda.DeleteInvoiceInfo(icm);
                            ShowGrid();
                        }
                    }
                }
                else
                {
                    if (btnEdit.Text == "Edit")
                    {
                        icm.BILLDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        icm.BILLYY = Convert.ToInt64(txtYear.Text);
                        icm.BILLNO = Convert.ToInt64(txtBillno.Text);
                        icm.BILLSL = Convert.ToInt64(lblSL.Text);

                        icda.DeleteInvoiceInfo(icm);
                        icda.DeleteInvoiceInfo_master(icm);
                        ShowGrid();
                        Refresh();
                    }
                    else
                    {
                        if (lblDelete.Text == "")
                        {
                            lblErrMsgExist.Visible = true;
                            lblErrMsgExist.Text = "You are not permited to continue this operation.";
                        }
                        else
                        {
                            lblErrMsgExist.Visible = false;
                            icm.BILLDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                            icm.BILLYY = Convert.ToInt64(ddlyear.Text);
                            icm.BILLNO = Convert.ToInt64(ddlbillNo.Text);
                            icm.BILLSL = Convert.ToInt64(lblSL.Text);

                            icda.DeleteInvoiceInfo(icm);
                            icda.DeleteInvoiceInfo_master(icm);
                            Global.dropDownAddWithSelect(ddlbillNo, "SELECT BILLNO FROM HR_BILLMST WHERE BILLYY='" + ddlyear.Text + "'  ORDER BY BILLNO");
                            ddlbillNo.SelectedIndex = -1;
                            ShowGrid();
                            Refresh();
                        }
                    }



                }

            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            ShowGrid();

            TextBox txtCategoryEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtCategoryEdit");
            txtCategoryEdit.Focus();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblSLEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblSLEdit");
            TextBox txtCategoryEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCategoryEdit");
            TextBox txtTotWorkerEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtTotWorkerEdit");
            TextBox txttothrEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txttothrEdit");
            TextBox txtPriceEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtPriceEdit");
            TextBox txtAmountEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAmountEdit");




            icm.COMPANYNM = ddlCompanyNM.Text;
            icm.BILLDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            if (btnEdit.Text == "Edit")
            {
                icm.BILLYY = Convert.ToInt64(txtYear.Text);
                icm.BILLNO = Convert.ToInt64(txtBillno.Text);
            }
            else
            {
                icm.BILLYY = Convert.ToInt64(ddlyear.Text);
                icm.BILLNO = Convert.ToInt64(ddlbillNo.Text);
            }
            icm.PSID = txtPrtyID.Text;
            icm.COSTPID = txtCostPid.Text;
            icm.BILLTP = ddlBillType.Text;
            icm.BILLSL = Convert.ToInt64(lblSLEdit.Text);

            icm.BILLNM = txtCategoryEdit.Text;


            icm.TWORKER = Convert.ToInt64(txtTotWorkerEdit.Text);
            icm.TOTQPTP = Convert.ToDecimal(txttothrEdit.Text);
            icm.RATEPTP = Convert.ToDecimal(txtPriceEdit.Text);
            icm.AMTPTP = Convert.ToDecimal(txtAmountEdit.Text);

            icm.InTm = DateTime.Now;
            icm.UserPc = HttpContext.Current.Session["PCName"].ToString();
            icm.Ip = HttpContext.Current.Session["IpAddress"].ToString();

            icda.UpdateInvoiceInfo(icm);

            gvDetails.EditIndex = -1;
            ShowGrid();


        }

        protected void ddlBillType_SelectedIndexChanged(object sender, EventArgs e)
        {

            TextBox txtCategory = (TextBox)gvDetails.FooterRow.FindControl("txtCategory");
            TextBox txtTotWorker = (TextBox)gvDetails.FooterRow.FindControl("txtTotWorker");
            TextBox txttothr = (TextBox)gvDetails.FooterRow.FindControl("txttothr");
            TextBox txtPrice = (TextBox)gvDetails.FooterRow.FindControl("txtPrice");
            TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");

            if (ddlBillType.Text == "Meter")
            {

                txtTotWorker.Enabled = false;
                txtTotWorker.Text = "";
                //gvDetails.FooterRow.Cells[3].Text = "Total Metter";
                gvDetails.HeaderRow.Cells[3].Text = "Total Meter";
                gvDetails.HeaderRow.Cells[4].Text = "Per Meter Rate";
            }

            else if (ddlBillType.Text == "Hour")
            {
                txtTotWorker.Enabled = true;
                gvDetails.HeaderRow.Cells[3].Text = "Total Hour";
                gvDetails.HeaderRow.Cells[4].Text = "Rate Per Hour";

            }

            else
            {
                txtTotWorker.Enabled = true;
                gvDetails.HeaderRow.Cells[3].Text = "Total Hour";
                gvDetails.HeaderRow.Cells[4].Text = "Rate Per Hour";
            }

            txtSUBMITPNM.Focus();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            if (btnEdit.Text == "Edit")
            {
                ddlCompanyNM.SelectedIndex = 1;
                ddlBillType.SelectedIndex = -1;

                txtPrtyNM.Text = "";
                txtPrtyID.Text = "";
                txtCostPid.Text = "";

                Global.lblAdd("select max(BILLNO) AS BILLNO from HR_BILLMST where BILLYY = " + txtYear.Text + " ", lblBillNO);

                if (lblBillNO.Text == "")
                {
                    txtBillno.Text = "1";
                }
                else
                {
                    Int64 id = Convert.ToInt64(lblBillNO.Text) + 1;
                    txtBillno.Text = id.ToString();
                    txtSiteID.Text = "";

                }

                txtSUBMITPNM.Text = "";
                txtSUBMITPCNO.Text = "";


                ShowGrid();

                ddlCompanyNM.Focus();
            }
            else
            {
                ddlyear.SelectedIndex = -1;
                ddlbillNo.SelectedIndex = -1;
                txtPrtyNM.Text = "";
                txtPrtyID.Text = "";
                txtCostPid.Text = "";
                txtSUBMITPNM.Text = "";
                txtSUBMITPCNO.Text = "";
                txtcompany.Text = "";
                txtTotAmount.Text = "";
                txtTotInWords.Text = "";
                ddlyear.Focus();
                ShowGrid();
            }
        }

        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCategory = (TextBox)gvDetails.FooterRow.FindControl("txtCategory");
            TextBox txtTotWorker = (TextBox)gvDetails.FooterRow.FindControl("txtTotWorker");
            TextBox txttothr = (TextBox)gvDetails.FooterRow.FindControl("txttothr");
            TextBox txtPrice = (TextBox)gvDetails.FooterRow.FindControl("txtPrice");
            TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");
            ImageButton imgbtnAdd = (ImageButton)gvDetails.FooterRow.FindControl("imgbtnAdd");

            decimal hr = Convert.ToDecimal(txttothr.Text);
            decimal pr = Convert.ToDecimal(txtPrice.Text);

            decimal tot = hr * pr;

            txtAmount.Text = tot.ToString();
            imgbtnAdd.Focus();
        }

        protected void txttothr_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCategory = (TextBox)gvDetails.FooterRow.FindControl("txtCategory");
            TextBox txtTotWorker = (TextBox)gvDetails.FooterRow.FindControl("txtTotWorker");
            TextBox txttothr = (TextBox)gvDetails.FooterRow.FindControl("txttothr");
            TextBox txtPrice = (TextBox)gvDetails.FooterRow.FindControl("txtPrice");
            TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");


            decimal hr = Convert.ToDecimal(txttothr.Text);
            decimal pr = 0;

            if (txtPrice.Text == "")
            {
                pr = 0;
                decimal tot = hr * pr;
                txtAmount.Text = tot.ToString();
            }

            else
            {
                pr = Convert.ToDecimal(txtPrice.Text);
                decimal tot = hr * pr;
                txtAmount.Text = tot.ToString();
            }

            txtPrice.Focus();
        }

        protected void txtCategory_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCategory = (TextBox)gvDetails.FooterRow.FindControl("txtCategory");
            TextBox txtTotWorker = (TextBox)gvDetails.FooterRow.FindControl("txtTotWorker");
            TextBox txttothr = (TextBox)gvDetails.FooterRow.FindControl("txttothr");
            TextBox txtPrice = (TextBox)gvDetails.FooterRow.FindControl("txtPrice");
            TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");

            if (ddlBillType.Text == "Meter")
            {

                txtTotWorker.Enabled = false;
                txtTotWorker.Text = "";
                //gvDetails.FooterRow.Cells[3].Text = "Total Metter";
                gvDetails.HeaderRow.Cells[3].Text = "Total Meter";
                gvDetails.HeaderRow.Cells[4].Text = "Per Meter Rate";
                txttothr.Focus();
            }

            else if (ddlBillType.Text == "Hour")
            {
                txtTotWorker.Enabled = true;
                gvDetails.HeaderRow.Cells[3].Text = "Total Hour";
                gvDetails.HeaderRow.Cells[4].Text = "Rate Per Hour";
                txtTotWorker.Focus();
            }

            else
            {
                txtTotWorker.Enabled = true;
                gvDetails.HeaderRow.Cells[3].Text = "Total Hour";
                gvDetails.HeaderRow.Cells[4].Text = "Rate Per Hour";
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            if (btnEdit.Text == "Edit")
            {
                btnPrint.Visible = true;
                btnPrint.Enabled = true;


                txtBillno.Visible = false;
                txtBillno.Enabled = false;
                txtYear.Visible = false;
                txtYear.Enabled = false;

                ddlBillType.Visible = false;
                ddlCompanyNM.Visible = false;

                txtbillType.Visible = true;
                txtcompany.Visible = true;
                ddlbillNo.Visible = true;
                ddlbillNo.Enabled = true;
                ddlyear.Visible = true;
                ddlyear.Enabled = true;

                txtSUBMITPNM.Enabled = false;
                txtSUBMITPCNO.Enabled = false;

                txtBillno.Enabled = false;
                txtCostPid.Enabled = false;
                txtPrtyNM.Enabled = false;
                ddlCompanyNM.Enabled = false;
                txtTotAmount.Text = ".00";
                txtTotInWords.Text = "";
                txtcompany.Text = "";
                ddlyear.Focus();

                Global.dropDownAddWithSelect(ddlbillNo, "select distinct BILLNO from HR_BILL");

                Global.dropDownAddWithSelect(ddlyear, "select distinct BILLYY from HR_BILL");


                btnEdit.Text = "Cancel";
            }

            else if (btnEdit.Text == "Cancel")
            {
                btnEdit.Text = "Edit";
                btnPrint.Visible = false;

                txtBillno.Visible = true;
                txtBillno.Enabled = true;
                txtYear.Visible = true;
                txtYear.Enabled = true;

                ddlBillType.Visible = true;
                ddlCompanyNM.Visible = true;

                txtbillType.Visible = false;
                txtcompany.Visible = false;
                ddlbillNo.Visible = false;
                ddlbillNo.Enabled = false;
                ddlyear.Visible = false;
                ddlyear.Enabled = false;

                txtSUBMITPNM.Enabled = true;
                txtSUBMITPCNO.Enabled = true;

                txtBillno.Enabled = true;
                txtCostPid.Enabled = true;
                txtPrtyNM.Enabled = true;
                ddlCompanyNM.Enabled = true;
                txtTotAmount.Text = ".00";
                txtTotInWords.Text = "";
                ddlCompanyNM.Focus();
                Refresh();
            }
        }

        private void ShowGrid_print()
        {

            conn = new SqlConnection(Global.connection);
            conn.Open();

            cmdd = new SqlCommand("SELECT HR_BILL.BILLSL, HR_BILL.BILLNM, HR_BILL.TWORKER, HR_BILL.RATEPTP, HR_BILL.TOTQPTP, HR_BILL.AMTPTP FROM HR_BILL INNER JOIN HR_BILLMST ON HR_BILL.BILLNO = HR_BILLMST.BILLNO  WHERE HR_BILL.BILLNO='" + ddlbillNo.Text + "' AND HR_BILL.BILLYY='" + ddlyear.Text + "'", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();


            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;

                if (gvDetails.EditIndex == -1)
                {
                    Decimal totAmt = 0;
                    Decimal a = 0;
                    foreach (GridViewRow grid in gvDetails.Rows)
                    {
                        Label Per = (Label)grid.Cells[5].FindControl("lblAmount");
                        if (Per.Text == "")
                        {
                            Per.Text = "0";
                        }
                        else
                        {
                            Per.Text = Per.Text;
                        }
                        String Perf = Per.Text;
                        totAmt = Convert.ToDecimal(Perf);
                        a += totAmt;
                        txtTotAmount.Text = a.ToString();
                    }
                    a += totAmt;

                    txtTotInWords.Text = "";
                    decimal dec;
                    Boolean ValidInput = Decimal.TryParse(txtTotAmount.Text, out dec);
                    if (!ValidInput)
                    {
                        txtTotInWords.ForeColor = System.Drawing.Color.Red;
                        txtTotInWords.Text = "Enter the Proper Amount...";
                        return;
                    }
                    if (txtTotAmount.Text.ToString().Trim() == "")
                    {
                        txtTotInWords.ForeColor = System.Drawing.Color.Red;
                        txtTotInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtTotAmount.Text) == 0)
                        {
                            txtTotInWords.ForeColor = System.Drawing.Color.Red;
                            txtTotInWords.Text = "Amount Cannot Be Empty...";
                            return;
                        }
                    }

                    string x1 = "";
                    string x2 = "";

                    if (txtTotAmount.Text.Contains("."))
                    {
                        x1 = txtTotAmount.Text.ToString().Trim().Substring(0, txtTotAmount.Text.ToString().Trim().IndexOf("."));
                        x2 = txtTotAmount.Text.ToString().Trim().Substring(txtTotAmount.Text.ToString().Trim().IndexOf(".") + 1);
                    }
                    else
                    {
                        x1 = txtTotAmount.Text.ToString().Trim();
                        x2 = "00";
                    }

                    if (x1.ToString().Trim() != "")
                    {
                        x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
                    }
                    else
                    {
                        x1 = "0";
                    }

                    txtTotAmount.Text = x1 + "." + x2;

                    if (x2.Length > 2)
                    {
                        txtTotAmount.Text = Math.Round(Convert.ToDouble(txtTotAmount.Text), 2).ToString().Trim();
                    }

                    string AmtConv = SpellAmount.MoneyConvFn(txtTotAmount.Text.ToString().Trim());
                    //string amntComma = SpellAmount.comma(Convert.ToDecimal(txtAmount.Text));
                    //Label3.Text = amntComma;

                    txtTotInWords.Text = AmtConv.Trim();
                    txtTotInWords.ForeColor = System.Drawing.Color.Green;
                    txtTotInWords.Focus();
                }
                else
                {

                }

            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                gvDetails.Rows[0].Visible = false;


            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (ddlyear.Text == "Select")
            {
                lblErrMsgExist.Visible = true;
                lblErrMsgExist.Text = "Select year.";
                ddlyear.Focus();
            }
            else if (ddlbillNo.Text == "Select")
            {
                lblErrMsgExist.Visible = true;
                lblErrMsgExist.Text = "Select bill no.";
                ddlbillNo.Focus();
            }
            else
            {
                lblErrMsgExist.Visible = false;

                Session["companyname"] = "";
                Session["partyNM"] = "";
                Session["partyID"] = "";
                Session["billdt"] = "";
                Session["YY"] = "";
                Session["billno"] = "";
                Session["site"] = "";
                Session["billtp"] = "";
                Session["name"] = "";
                Session["contact"] = "";



                Session["companyname"] = txtcompany.Text;
                Session["partyNM"] = txtPrtyNM.Text;
                Session["partyID"] = txtPrtyID.Text;
                Session["billdt"] = txtBillDate.Text;
                Session["YY"] = ddlyear.Text;
                Session["billno"] = ddlbillNo.Text;
                Session["site"] = txtCostPid.Text;
                Session["billtp"] = txtbillType.Text;
                Session["name"] = txtSUBMITPNM.Text;
                Session["contact"] = txtSUBMITPCNO.Text;

                Page.ClientScript.RegisterStartupScript(
                       this.GetType(), "OpenWindow", "window.open('../report/vis-report/rpt-Invoice.aspx','_newtab');", true);
            }

            //Refresh();
        }

        protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlyear.Text == "Select")
            {
                lblErrMsgExist.Visible = true;
                lblErrMsgExist.Text = "Select year.";
                ddlyear.Focus();
            }
            else
                ddlbillNo.Focus();
        }

        protected void ddlbillNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbillNo.Text == "Select")
            {
                lblErrMsgExist.Visible = true;
                lblErrMsgExist.Text = "Select bill no.";
                ddlbillNo.Focus();
            }
            else if (ddlyear.Text == "Select")
            {
                lblErrMsgExist.Visible = true;
                lblErrMsgExist.Text = "Select year.";
                ddlyear.Focus();
            }
            else
            {
                Global.txtAdd("select convert(nvarchar(20),BILLDT, 103) as BILLDT from HR_BILL where BILLNO=" + ddlbillNo.Text + " and BILLYY=" + ddlyear.Text + "", txtBillDate);

                Global.txtAdd("select COSTPID from HR_BILL where BILLNO=" + ddlbillNo.Text + " and BILLYY=" + ddlyear.Text + "", txtSiteID);

                Global.txtAdd(" SELECT COSTPNM FROM GL_COSTP WHERE COSTPID ='" + txtSiteID.Text + "'", txtCostPid);

                Global.txtAdd("select PSID from HR_BILL where BILLNO=" + ddlbillNo.Text + " and BILLYY=" + ddlyear.Text + "", txtPrtyID);
                Global.txtAdd("select BILLTP from HR_BILL where BILLNO=" + ddlbillNo.Text + " and BILLYY=" + ddlyear.Text + "", txtbillType);
                Global.txtAdd("select COMPANYNM from HR_BILL where BILLNO=" + ddlbillNo.Text + " and BILLYY=" + ddlyear.Text + "", txtcompany);


                Global.txtAdd("select ACCOUNTNM from GL_ACCHART where ACCOUNTCD='" + txtPrtyID.Text + "'", txtPrtyNM);

                Global.txtAdd("select SUBMITPNM from HR_BILLMST where BILLNO=" + ddlbillNo.Text + " and BILLYY=" + ddlyear.Text + "", txtSUBMITPNM);
                Global.txtAdd("select SUBMITPCNO from HR_BILLMST where BILLNO=" + ddlbillNo.Text + " and BILLYY=" + ddlyear.Text + "", txtSUBMITPCNO);
                //Global.dropDownAddWithSelect(ddlCompanyNM, "select  COMPANYNM from HR_BILL where BILLNO=" + ddlbillNo.Text + " and BILLYY=" + ddlyear.Text + "");
                //Global.dropDownAddWithSelect(ddlBillType, "select  BILLTP from HR_BILL where BILLNO=" + ddlbillNo.Text + " and BILLYY=" + ddlyear.Text + "");

                ShowGrid_print();

                btnPrint.Focus();
            }
        }

        protected void txttothrEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txttothrEdit = (TextBox)row.FindControl("txttothrEdit");
            TextBox txtPriceEdit = (TextBox)row.FindControl("txtPriceEdit");
            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");

            decimal hr = Convert.ToDecimal(txttothrEdit.Text);
            decimal pr = 0;

            if (txtPriceEdit.Text == "")
            {
                pr = 0;
                decimal tot = hr * pr;
                txtAmountEdit.Text = tot.ToString();
            }

            else
            {
                pr = Convert.ToDecimal(txtPriceEdit.Text);
                decimal tot = hr * pr;
                txtAmountEdit.Text = tot.ToString();
            }

            txtPriceEdit.Focus();
        }

        protected void txtPriceEdit_TextChanged(object sender, EventArgs e)
        {

            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txttothrEdit = (TextBox)row.FindControl("txttothrEdit");
            TextBox txtPriceEdit = (TextBox)row.FindControl("txtPriceEdit");
            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");

            decimal hr = Convert.ToDecimal(txttothrEdit.Text);
            decimal pr = Convert.ToDecimal(txtPriceEdit.Text);

            decimal tot = hr * pr;

            txtAmountEdit.Text = tot.ToString();
        }

        protected void txtCostPid_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd("select COSTPID from GL_COSTP where COSTPNM='" + txtCostPid.Text + "'", txtSiteID);
            if (txtSiteID.Text == "")
            {
                lblErrMsgExist.Visible = true;
                lblErrMsgExist.Text = "Select site name.";
                txtSiteID.Text = "";
                txtCostPid.Focus();
            }
            else
                ddlBillType.Focus();
        }



        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListName_Contact(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = ("SELECT DISTINCT (SUBMITPNM + '-' + SUBMITPCNO) AS DETAILS FROM HR_BILLMST WHERE (SUBMITPNM + '-' + SUBMITPCNO) LIKE '" + prefixText + "%'");


            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["DETAILS"].ToString());
            return CompletionSet.ToArray();

        }




        protected void txtSUBMITPNM_TextChanged(object sender, EventArgs e)
        {
            if (txtSUBMITPNM.Text == "")
            {
                lblErrMsgExist.Visible = true;
                lblErrMsgExist.Text = "Select Name ";
                lblErrMsgExist.Focus();
            }
            else
            {
                lblErrMsgExist.Visible = false;

                string name = "";
                string contact = "";

                string searchPar = txtSUBMITPNM.Text;

                int splitter = searchPar.IndexOf("-");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('-');

                    name = lineSplit[0];
                    contact = lineSplit[1];


                    txtSUBMITPNM.Text = name.Trim();
                    txtSUBMITPCNO.Text = contact.Trim();

                }

            }

            txtSUBMITPCNO.Focus();
        }

        protected void txtBillMY_TextChanged(object sender, EventArgs e)
        {
            txtCostPid.Focus();
        }

        protected void txtSUBMITPCNO_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCategory = (TextBox)gvDetails.FooterRow.FindControl("txtCategory");
            txtCategory.Focus();
        }
    }
}