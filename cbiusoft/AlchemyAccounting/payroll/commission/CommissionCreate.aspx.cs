using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AlchemyAccounting.payroll.commission
{
    public partial class CommissionCreate : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmdd;

        CommissionCreateDataAccess ccd = new CommissionCreateDataAccess();
        CommissionCreateModel ccm = new CommissionCreateModel();

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
                    string year = td.ToString("yy");

                    txtMonth.Text = mon + "-" + year;

                    string user = HttpContext.Current.Session["UserName"].ToString();
                    Global.lblAdd("SELECT PerEd FROM User_Registration WHERE UserID ='" + user + "'", lblEdit);
                    if (lblEdit.Text == "Edit")
                        btnEdit.Visible = true;
                    else
                        btnEdit.Visible = false;

                    Global.lblAdd("SELECT PerDel FROM User_Registration WHERE UserID ='" + user + "'", lblDelete);
                    if (lblDelete.Text == "")
                    {
                        btnDelete.Visible = false;
                    }
                    else
                        btnDelete.Visible = true;

                    Global.lblAdd("select max(TRANSNO) AS TRANSNO from GL_COMM where TRANSMY = '" + txtMonth.Text + "' ", lblBillNO);

                    if (lblBillNO.Text == "")
                    {
                        txtInvoice.Text = "1";
                    }
                    else
                    {
                        Int64 id = Convert.ToInt64(lblBillNO.Text) + 1;
                        txtInvoice.Text = id.ToString();
                    }

                    txtPrtyNM.Focus();
                }
            }
        }

        protected void txtBillDate_TextChanged(object sender, EventArgs e)
        {

            DateTime td = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            string mon = td.ToString("MMM").ToUpper();
            string year = td.ToString("yy");

            txtMonth.Text = mon + "-" + year;

        }


        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            txtPrtyNM.Text = "";
            txtPrtyID.Text = "";
            txtPayable.Text = "";
            txtSiteID.Text = "";
            txtCostPid.Text = "";
            txtbillamt.Text = "";
            txtpercentage.Text = "";
            txtcomAmt.Text = "";
            txtcarrent.Text = "";
            txtAdvanceAmount.Text = "";
            txtAdvanceAmountComp.Text = "";
            txtTotalAmount.Text = "";
            txtNetAmount.Text = "";
            txtRemarks.Text = "";
            

            if (btnEdit.Text == "Edit")
            {
                Global.lblAdd("select max(TRANSNO) AS TRANSNO from GL_COMM where TRANSMY = '" + txtMonth.Text + "' ", lblBillNO);

                if (lblBillNO.Text == "")
                {
                    txtInvoice.Text = "1";
                }
                else
                {
                    Int64 id = Convert.ToInt64(lblBillNO.Text) + 1;
                    txtInvoice.Text = id.ToString();
                }

                txtPrtyNM.Focus();
            }
            else
            {
                Global.dropDownAddWithSelect(ddlInvoiceno, "SELECT TRANSNO FROM GL_COMM WHERE TRANSMY ='" + txtMonthedit.Text + "' ORDER BY TRANSNO");
                ddlInvoiceno.SelectedIndex = -1;
                txtMonthedit.Text = "";
                txtMonthedit.Focus();
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListParty(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);

            cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) IN ('1020201') and ACCOUNTNM LIKE '" + prefixText + "%' AND LEVELCD =5 ", conn);

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
        public static string[] GetCompletionListMonth(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = ("SELECT DISTINCT TRANSMY FROM GL_COMM WHERE TRANSMY LIKE '" + prefixText + "%'");


            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["TRANSMY"].ToString());
            return CompletionSet.ToArray();

        }

        protected void txtPrtyNM_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd("select ACCOUNTCD from GL_ACCHART where ACCOUNTNM='" + txtPrtyNM.Text + "' AND LEVELCD =5 ", txtPrtyID);
            txtPayable.Focus();
            lblerrmsgp.Visible = false;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListPayable(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);

            cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) IN ('2020102') and ACCOUNTNM LIKE '" + prefixText + "%' AND LEVELCD =5 ", conn);

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtPayable_TextChanged(object sender, EventArgs e)
        {
            if (txtPayable.Text == "")
            {
                lblerrmsgp.Visible = true;
                lblerrmsgp.Text = "Select payable head.";
                txtPayable.Focus();
            }
            else
            {
                lblerrmsgp.Visible = false;

                SqlConnection con = new SqlConnection(Global.connection);
                con.Open();
                SqlCommand cmd = new SqlCommand("select ACCOUNTCD from GL_ACCHART where ACCOUNTNM=@ACCOUNTNM AND LEVELCD =5", con);
                
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ACCOUNTNM", txtPayable.Text);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtPayableID.Text = reader[0].ToString();
                }
                con.Close();
                reader.Close();

                if (txtPayableID.Text == "")
                {
                    lblerrmsgp.Visible = true;
                    lblerrmsgp.Text = "Select payable head.";
                    txtPayable.Text = "";
                    txtPayable.Focus();
                }
                else
                    txtCostPid.Focus();
            }
        }

        protected void txtCostPid_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd("select COSTPID from GL_COSTP where COSTPNM='" + txtCostPid.Text + "'", txtSiteID);
            txtbillamt.Focus();
            lblerrmsgS.Visible = false;
        }

        protected void txtbillamt_TextChanged(object sender, EventArgs e)
        {
            decimal bill;
            decimal perc;
            decimal car;
            decimal adp;
            decimal adcompany;

            decimal comamt;
            decimal totamt;
            decimal netamt;


            if (txtbillamt.Text == "")
            {
                bill = 0;
            }
            else
            {
                bill = Convert.ToDecimal(txtbillamt.Text);
            }

            if (txtpercentage.Text == "")
            {
                perc = 0;

            }

            else
            {
                perc = Convert.ToDecimal(txtpercentage.Text);

            }

            if (txtcarrent.Text == "")
            {
                car = 0;
            }
            else
            {
                car = Convert.ToDecimal(txtcarrent.Text);
            }

            if (txtAdvanceAmount.Text == "")
            {
                adp = 0;
            }
            else
            {
                adp = Convert.ToDecimal(txtAdvanceAmount.Text);
            }


            if (txtAdvanceAmountComp.Text == "")
            {
                adcompany = 0;
            }
            else
            {
                adcompany = Convert.ToDecimal(txtAdvanceAmountComp.Text);
            }


            comamt = bill * perc / 100;
            txtcomAmt.Text = comamt.ToString();

            totamt = comamt + car + adp;
            txtTotalAmount.Text = totamt.ToString();

            netamt = bill - (totamt + adcompany);
            txtNetAmount.Text = netamt.ToString();



            lblerrmsg.Visible = false;
            txtpercentage.Focus();
        }

        protected void txtpercentage_TextChanged(object sender, EventArgs e)
        {


            decimal bill;
            decimal perc;
            decimal car;
            decimal adp;
            decimal adcompany;

            decimal comamt;
            decimal totamt;
            decimal netamt;


            if (txtbillamt.Text == "")
            {
                bill = 0;
            }
            else
            {
                bill = Convert.ToDecimal(txtbillamt.Text);
            }

            if (txtpercentage.Text == "")
            {
                perc = 0;

            }

            else
            {
                perc = Convert.ToDecimal(txtpercentage.Text);

            }

            if (txtcarrent.Text == "")
            {
                car = 0;
            }
            else
            {
                car = Convert.ToDecimal(txtcarrent.Text);
            }

            if (txtAdvanceAmount.Text == "")
            {
                adp = 0;
            }
            else
            {
                adp = Convert.ToDecimal(txtAdvanceAmount.Text);
            }


            if (txtAdvanceAmountComp.Text == "")
            {
                adcompany = 0;
            }
            else
            {
                adcompany = Convert.ToDecimal(txtAdvanceAmountComp.Text);
            }


            comamt = bill * perc / 100;
            txtcomAmt.Text = comamt.ToString();

            totamt = comamt + car + adp;
            txtTotalAmount.Text = totamt.ToString();

            netamt = bill - (totamt + adcompany);
            txtNetAmount.Text = netamt.ToString();

            txtcarrent.Focus();

        }

        protected void txtcarrent_TextChanged(object sender, EventArgs e)
        {



            decimal bill;
            decimal perc;
            decimal car;
            decimal adp;
            decimal adcompany;

            decimal comamt;
            decimal totamt;
            decimal netamt;


            if (txtbillamt.Text == "")
            {
                bill = 0;
            }
            else
            {
                bill = Convert.ToDecimal(txtbillamt.Text);
            }

            if (txtpercentage.Text == "")
            {
                perc = 0;

            }

            else
            {
                perc = Convert.ToDecimal(txtpercentage.Text);

            }

            if (txtcarrent.Text == "")
            {
                car = 0;
            }
            else
            {
                car = Convert.ToDecimal(txtcarrent.Text);
            }

            if (txtAdvanceAmount.Text == "")
            {
                adp = 0;
            }
            else
            {
                adp = Convert.ToDecimal(txtAdvanceAmount.Text);
            }


            if (txtAdvanceAmountComp.Text == "")
            {
                adcompany = 0;
            }
            else
            {
                adcompany = Convert.ToDecimal(txtAdvanceAmountComp.Text);
            }


            comamt = bill * perc / 100;
            txtcomAmt.Text = comamt.ToString();

            totamt = comamt + car + adp;
            txtTotalAmount.Text = totamt.ToString();

            netamt = bill - (totamt + adcompany);
            txtNetAmount.Text = netamt.ToString();

            txtAdvanceAmount.Focus();

        }

        protected void txtAdvanceAmount_TextChanged(object sender, EventArgs e)
        {


            decimal bill;
            decimal perc;
            decimal car;
            decimal adp;
            decimal adcompany;

            decimal comamt;
            decimal totamt;
            decimal netamt;


            if (txtbillamt.Text == "")
            {
                bill = 0;
            }
            else
            {
                bill = Convert.ToDecimal(txtbillamt.Text);
            }

            if (txtpercentage.Text == "")
            {
                perc = 0;

            }

            else
            {
                perc = Convert.ToDecimal(txtpercentage.Text);

            }

            if (txtcarrent.Text == "")
            {
                car = 0;
            }
            else
            {
                car = Convert.ToDecimal(txtcarrent.Text);
            }

            if (txtAdvanceAmount.Text == "")
            {
                adp = 0;
            }
            else
            {
                adp = Convert.ToDecimal(txtAdvanceAmount.Text);
            }


            if (txtAdvanceAmountComp.Text == "")
            {
                adcompany = 0;
            }
            else
            {
                adcompany = Convert.ToDecimal(txtAdvanceAmountComp.Text);
            }


            comamt = bill * perc / 100;
            txtcomAmt.Text = comamt.ToString();

            totamt = comamt + car + adp;
            txtTotalAmount.Text = totamt.ToString();

            netamt = bill - (totamt + adcompany);
            txtNetAmount.Text = netamt.ToString();


            txtAdvanceAmountComp.Focus();
        }

        protected void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {



            decimal bill;
            decimal perc;
            decimal car;
            decimal adp;
            decimal adcompany;

            decimal comamt;
            decimal totamt;
            decimal netamt;


            if (txtbillamt.Text == "")
            {
                bill = 0;
            }
            else
            {
                bill = Convert.ToDecimal(txtbillamt.Text);
            }

            if (txtpercentage.Text == "")
            {
                perc = 0;

            }

            else
            {
                perc = Convert.ToDecimal(txtpercentage.Text);

            }

            if (txtcarrent.Text == "")
            {
                car = 0;
            }
            else
            {
                car = Convert.ToDecimal(txtcarrent.Text);
            }

            if (txtAdvanceAmount.Text == "")
            {
                adp = 0;
            }
            else
            {
                adp = Convert.ToDecimal(txtAdvanceAmount.Text);
            }


            if (txtAdvanceAmountComp.Text == "")
            {
                adcompany = 0;
            }
            else
            {
                adcompany = Convert.ToDecimal(txtAdvanceAmountComp.Text);
            }


            comamt = bill * perc / 100;
            txtcomAmt.Text = comamt.ToString();

            totamt = comamt + car + adp;
            txtTotalAmount.Text = totamt.ToString();

            netamt = bill - (totamt + adcompany);
            txtNetAmount.Text = netamt.ToString();


            txtAdvanceAmountComp.Focus();
        }

        protected void txtAdvanceAmountComp_TextChanged(object sender, EventArgs e)
        {



            decimal bill;
            decimal perc;
            decimal car;
            decimal adp;
            decimal adcompany;

            decimal comamt;
            decimal totamt;
            decimal netamt;


            if (txtbillamt.Text == "")
            {
                bill = 0;
            }
            else
            {
                bill = Convert.ToDecimal(txtbillamt.Text);
            }

            if (txtpercentage.Text == "")
            {
                perc = 0;

            }

            else
            {
                perc = Convert.ToDecimal(txtpercentage.Text);

            }

            if (txtcarrent.Text == "")
            {
                car = 0;
            }
            else
            {
                car = Convert.ToDecimal(txtcarrent.Text);
            }

            if (txtAdvanceAmount.Text == "")
            {
                adp = 0;
            }
            else
            {
                adp = Convert.ToDecimal(txtAdvanceAmount.Text);
            }


            if (txtAdvanceAmountComp.Text == "")
            {
                adcompany = 0;
            }
            else
            {
                adcompany = Convert.ToDecimal(txtAdvanceAmountComp.Text);
            }


            comamt = bill * perc / 100;
            txtcomAmt.Text = comamt.ToString();

            totamt = comamt + car + adp;
            txtTotalAmount.Text = totamt.ToString();

            netamt = bill - (totamt + adcompany);
            txtNetAmount.Text = netamt.ToString();


            txtRemarks.Focus();
        }

        protected void txtcomAmt_TextChanged(object sender, EventArgs e)
        {

            decimal bill;
            decimal perc;
            decimal car;
            decimal adp;
            decimal adcompany;

            decimal comamt;
            decimal totamt;
            decimal netamt;


            if (txtbillamt.Text == "")
            {
                bill = 0;
            }
            else
            {
                bill = Convert.ToDecimal(txtbillamt.Text);
            }

            if (txtpercentage.Text == "")
            {
                perc = 0;

            }

            else
            {
                perc = Convert.ToDecimal(txtpercentage.Text);

            }

            if (txtcarrent.Text == "")
            {
                car = 0;
            }
            else
            {
                car = Convert.ToDecimal(txtcarrent.Text);
            }

            if (txtAdvanceAmount.Text == "")
            {
                adp = 0;
            }
            else
            {
                adp = Convert.ToDecimal(txtAdvanceAmount.Text);
            }


            if (txtAdvanceAmountComp.Text == "")
            {
                adcompany = 0;
            }
            else
            {
                adcompany = Convert.ToDecimal(txtAdvanceAmountComp.Text);
            }


            comamt = bill * perc / 100;
            txtcomAmt.Text = comamt.ToString();

            totamt = comamt + car + adp;
            txtTotalAmount.Text = totamt.ToString();


            netamt = bill - (totamt + adcompany);
            txtNetAmount.Text = netamt.ToString();


        }


        public void save()
        {
            if (txtPrtyID.Text == "")
            {
                lblerrmsgp.Visible = true;
                lblerrmsgp.Text = "Party Name Missing.";
                txtPrtyNM.Focus();
            }
            else if (txtPayableID.Text == "")
            {
                lblerrmsgp.Visible = true;
                lblerrmsgp.Text = "Payable Head Missing.";
                txtPayable.Focus();
            }
            else if (txtSiteID.Text == "")
            {
                lblerrmsgS.Visible = true;
                lblerrmsgS.Text = "Site Name Missing";
                txtCostPid.Focus();
            }
            else if (txtbillamt.Text == "")
            {
                lblerrmsg.Visible = true;
                lblerrmsg.Text = "Bill Amount Missing.";
            }

            else
            {
                ccm.TRANSDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                ccm.TRANSMY = txtMonth.Text;

                Global.lblAdd("select max(TRANSNO) AS TRANSNO from GL_COMM where TRANSMY = '" + txtMonth.Text + "' ", lblBillNO);

                if (lblBillNO.Text == "")
                {
                    txtInvoice.Text = "1";
                    ccm.TRANSNO = Convert.ToInt64(txtInvoice.Text);
                }
                else
                {
                    Int64 id = Convert.ToInt64(lblBillNO.Text) + 1;
                    txtInvoice.Text = id.ToString();
                    ccm.TRANSNO = Convert.ToInt64(txtInvoice.Text);
                }


                ccm.PSID = txtPrtyID.Text;
                ccm.Payable = txtPayableID.Text;
                ccm.COSTPID = txtSiteID.Text;


                if (txtbillamt.Text == "")
                {
                    ccm.BILLAMT = 0;
                }
                else
                {
                    ccm.BILLAMT = Convert.ToDecimal(txtbillamt.Text);
                }


                if (txtpercentage.Text == "")
                {
                    ccm.COMPCNT = 0;
                }
                else
                {
                    ccm.COMPCNT = Convert.ToDecimal(txtpercentage.Text);
                }

                if (txtcomAmt.Text == "")
                {
                    ccm.COMMAMT = 0;
                }
                else
                {
                    ccm.COMMAMT = Convert.ToDecimal(txtcomAmt.Text);
                }
                if (txtcarrent.Text == "")
                {
                    ccm.CARRENT = 0;
                }
                else
                {
                    ccm.CARRENT = Convert.ToDecimal(txtcarrent.Text);

                }


                if (txtAdvanceAmount.Text == "")
                {
                    ccm.ADVAMTP = 0;
                }
                else
                {
                    ccm.ADVAMTP = Convert.ToDecimal(txtAdvanceAmount.Text);
                }

                if (txtTotalAmount.Text == "")
                {
                    ccm.TOTAMT = 0;
                }
                else
                {
                    ccm.TOTAMT = Convert.ToDecimal(txtTotalAmount.Text);
                }

                if (txtAdvanceAmountComp.Text == "")
                {
                    ccm.ADVAMTC = 0;
                }
                else
                {
                    ccm.ADVAMTC = Convert.ToDecimal(txtAdvanceAmountComp.Text);
                }
                if (txtNetAmount.Text == "")
                {
                    ccm.NETAMT = 0;
                }

                else
                {
                    ccm.NETAMT = Convert.ToDecimal(txtNetAmount.Text);
                }
                ccm.REMARKS = txtRemarks.Text;

                ccm.InTm = DateTime.Now;
                ccm.UserPc = HttpContext.Current.Session["PCName"].ToString();
                ccm.Ip = HttpContext.Current.Session["IpAddress"].ToString();

                ccd.SaveCommissionInfo(ccm);


            }
        }


        public void Update()
        {
            lblerrmsgp.Text = "";
            lblerrmsgS.Text = "";
            lblerrmsg.Text = "";


            if (txtPrtyID.Text == "")
            {

                lblerrmsgp.Visible = true;
                lblerrmsgp.Text = "Party Name Missing.";
                txtPrtyNM.Focus();
            }
            else if (txtPayableID.Text == "")
            {
                lblerrmsgp.Visible = true;
                lblerrmsgp.Text = "Payable Head Missing.";
                txtPayable.Focus();
            }
            else if (txtSiteID.Text == "")
            {
                lblerrmsgS.Visible = true;
                lblerrmsgS.Text = "Site Name Missing";
                txtCostPid.Focus();
            }
            else if (txtbillamt.Text == "")
            {
                lblerrmsg.Visible = true;
                lblerrmsg.Text = "Bill Amount Missing.";
            }

            else
            {
                ccm.TRANSDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                ccm.TRANSMY = txtMonthedit.Text;
                ccm.TRANSNO = Convert.ToInt64(ddlInvoiceno.Text);

                ccm.PSID = txtPrtyID.Text;
                ccm.Payable = txtPayableID.Text;
                ccm.COSTPID = txtSiteID.Text;

                if (txtbillamt.Text == "")
                {
                    ccm.BILLAMT = 0;
                }
                else
                {
                    ccm.BILLAMT = Convert.ToDecimal(txtbillamt.Text);
                }


                if (txtpercentage.Text == "")
                {
                    ccm.COMPCNT = 0;
                }
                else
                {
                    ccm.COMPCNT = Convert.ToDecimal(txtpercentage.Text);
                }

                if (txtcomAmt.Text == "")
                {
                    ccm.COMMAMT = 0;
                }
                else
                {
                    ccm.COMMAMT = Convert.ToDecimal(txtcomAmt.Text);
                }
                if (txtcarrent.Text == "")
                {
                    ccm.CARRENT = 0;
                }
                else
                {
                    ccm.CARRENT = Convert.ToDecimal(txtcarrent.Text);

                }


                if (txtAdvanceAmount.Text == "")
                {
                    ccm.ADVAMTP = 0;
                }
                else
                {
                    ccm.ADVAMTP = Convert.ToDecimal(txtAdvanceAmount.Text);
                }

                if (txtTotalAmount.Text == "")
                {
                    ccm.TOTAMT = 0;
                }
                else
                {
                    ccm.TOTAMT = Convert.ToDecimal(txtTotalAmount.Text);
                }

                if (txtAdvanceAmountComp.Text == "")
                {
                    ccm.ADVAMTC = 0;
                }
                else
                {
                    ccm.ADVAMTC = Convert.ToDecimal(txtAdvanceAmountComp.Text);
                }
                if (txtNetAmount.Text == "")
                {
                    ccm.NETAMT = 0;
                }

                else
                {
                    ccm.NETAMT = Convert.ToDecimal(txtNetAmount.Text);
                }
                ccm.REMARKS = txtRemarks.Text;


                ccm.InTm = DateTime.Now;
                ccm.UserPc = HttpContext.Current.Session["PCName"].ToString();
                ccm.Ip = HttpContext.Current.Session["IpAddress"].ToString();

                ccd.UpdateCommissionInfo(ccm);


            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {

            lblerrmsgp.Text = "";
            lblerrmsgp.Visible = false;
            lblerrmsgS.Text = "";
            lblerrmsgS.Visible = false;

            lblerrmsg.Text = "";
            lblerrmsg.Visible = false;


            if (btnSave.Text == "Save")
            {

                save();

                Refresh();
            }

            else if (btnSave.Text == "Update")
            {
                Update();
                Refresh();
            }

        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {

            if (btnEdit.Text == "Edit")
            {
                btnPrintsave.Visible = false;
                btnPrint.Visible = true;
                btnPrint.Enabled = true;
                btnDelete.Visible = true;

                txtInvoice.Visible = false;
                txtMonth.Visible = false;

                ddlInvoiceno.Visible = true;
                txtMonthedit.Visible = true;


                txtBillDate.Enabled = false;

                btnSave.Text = "Update";

                btnEdit.Text = "Cancel";
                if (lblDelete.Text == "")
                {
                    btnDelete.Visible = false;
                }
                else
                    btnDelete.Visible = true;
                Refresh();

            }

            else if (btnEdit.Text == "Cancel")
            {

                btnPrintsave.Visible = true;
                btnPrint.Visible = false;
                btnPrint.Enabled = false;
                btnDelete.Visible = false;

                txtInvoice.Visible = true;
                txtMonth.Visible = true;

                ddlInvoiceno.Visible = false;
                txtMonthedit.Visible = false;


                txtBillDate.Enabled = true;

                btnSave.Text = "Save";

                btnEdit.Text = "Edit";
                txtMonthedit.Text = "";
                ddlInvoiceno.SelectedIndex = -1;
                Refresh();
            }
        }

        protected void txtMonthedit_TextChanged(object sender, EventArgs e)
        {
            Global.dropDownAddWithSelect(ddlInvoiceno, "Select distinct TRANSNO from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' ");

            txtPrtyNM.Text = "";
            txtPrtyID.Text = "";
            txtSiteID.Text = "";
            txtCostPid.Text = "";
            txtbillamt.Text = "";
            txtpercentage.Text = "";
            txtcarrent.Text = "";
            txtAdvanceAmount.Text = "";
            txtAdvanceAmountComp.Text = "";
            txtTotalAmount.Text = "";
            txtNetAmount.Text = "";
            txtRemarks.Text = "";
            ddlInvoiceno.Focus();
        }

        protected void ddlInvoiceno_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.txtAdd("select CONVERT(NVARCHAR(20),TRANSDT,103) AS TRANSD from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "' ", txtBillDate);

            Global.txtAdd("select PSID from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtPrtyID);
            Global.txtAdd("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD='" + txtPrtyID.Text + "'", txtPrtyNM);

            Global.txtAdd("select SPID from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtPayableID);
            Global.txtAdd("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD='" + txtPayableID.Text + "'", txtPayable);

            Global.txtAdd("select COSTPID from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtSiteID);
            Global.txtAdd("select COSTPNM from GL_COSTP where COSTPID ='" + txtSiteID.Text + "'", txtCostPid);

            Global.txtAdd("select BILLAMT from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtbillamt);
            Global.txtAdd("select COMPCNT from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtpercentage);
            Global.txtAdd("select COMMAMT from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtcomAmt);
            Global.txtAdd("select CARRENT from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtcarrent);
            Global.txtAdd("select ADVAMTP from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtAdvanceAmount);
            Global.txtAdd("select TOTAMT from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtTotalAmount);
            Global.txtAdd("select ADVAMTC from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtAdvanceAmountComp);
            Global.txtAdd("select NETAMT from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtNetAmount);
            Global.txtAdd("select REMARKS from GL_COMM where TRANSMY='" + txtMonthedit.Text + "' and TRANSNO='" + ddlInvoiceno.Text + "'", txtRemarks);


        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["date"] = "";
            Session["monthedit"] = "";
            Session["invoiceno"] = "";
            Session["partyID"] = "";
            Session["partyNM"] = "";
            Session["siteID"] = "";
            Session["siteNM"] = "";
            Session["billAmt"] = "";
            Session["percentage"] = "";
            Session["commission"] = "";
            Session["carRent"] = "";
            Session["advanceAmount"] = "";
            Session["total"] = "";
            Session["advanceAmtComp"] = "";
            Session["nettotal"] = "";
            Session["remarks"] = "";

            Session["date"] = txtBillDate.Text;
            Session["month"] = txtMonthedit.Text;
            Session["invoiceno"] = ddlInvoiceno.Text;
            Session["partyID"] = txtPrtyID.Text;
            Session["partyNM"] = txtPrtyNM.Text;
            Session["siteID"] = txtSiteID.Text;
            Session["siteNM"] = txtCostPid.Text;
            Session["billAmt"] = txtbillamt.Text;
            Session["percentage"] = txtpercentage.Text;
            Session["commission"] = txtcomAmt.Text;
            Session["carRent"] = txtcarrent.Text;
            Session["advanceAmount"] = txtAdvanceAmount.Text;
            Session["total"] = txtTotalAmount.Text;
            Session["advanceAmtComp"] = txtAdvanceAmountComp.Text;
            Session["nettotal"] = txtNetAmount.Text;
            Session["remarks"] = txtRemarks.Text;

            Update();
            Page.ClientScript.RegisterStartupScript(
                   this.GetType(), "OpenWindow", "window.open('../report/vis-report/CommissionCreateReport.aspx','_newtab');", true);

            Refresh();

        }

        protected void txtNetAmount_TextChanged(object sender, EventArgs e)
        {


            txtRemarks.Focus();
        }

        protected void btnPrintsave_Click(object sender, EventArgs e)
        {
            Session["date"] = "";
            Session["month"] = "";
            Session["invoiceno"] = "";
            Session["partyID"] = "";
            Session["partyNM"] = "";
            Session["siteID"] = "";
            Session["siteNM"] = "";
            Session["billAmt"] = "";
            Session["percentage"] = "";
            Session["commission"] = "";
            Session["carRent"] = "";
            Session["advanceAmount"] = "";
            Session["total"] = "";
            Session["advanceAmtComp"] = "";
            Session["nettotal"] = "";
            Session["remarks"] = "";

            Session["date"] = txtBillDate.Text;
            Session["month"] = txtMonth.Text;
            Session["invoiceno"] = txtInvoice.Text;
            Session["partyID"] = txtPrtyID.Text;
            Session["partyNM"] = txtPrtyNM.Text;
            Session["siteID"] = txtSiteID.Text;
            Session["siteNM"] = txtCostPid.Text;
            Session["billAmt"] = txtbillamt.Text;
            Session["percentage"] = txtpercentage.Text;
            Session["commission"] = txtcomAmt.Text;
            Session["carRent"] = txtcarrent.Text;
            Session["advanceAmount"] = txtAdvanceAmount.Text;
            Session["total"] = txtTotalAmount.Text;
            Session["advanceAmtComp"] = txtAdvanceAmountComp.Text;
            Session["nettotal"] = txtNetAmount.Text;
            Session["remarks"] = txtRemarks.Text;

            save();

            Page.ClientScript.RegisterStartupScript(
                   this.GetType(), "OpenWindow", "window.open('../report/vis-report/CommissionCreateReport.aspx','_newtab');", true);

            Refresh();

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblerrmsgp.Text = "";
            lblerrmsgS.Text = "";
            lblerrmsg.Text = "";


            if (txtPrtyID.Text == "")
            {

                lblerrmsgp.Visible = true;
                lblerrmsgp.Text = "Party Name Missing.";
                txtPrtyNM.Focus();
            }
            else if (txtSiteID.Text == "")
            {
                lblerrmsgS.Visible = true;
                lblerrmsgS.Text = "Site Name Missing";
                txtCostPid.Focus();
            }
            else if (txtbillamt.Text == "")
            {
                lblerrmsg.Visible = true;
                lblerrmsg.Text = "Bill Amount Missing.";
            }

            else if (ddlInvoiceno.Text == "Select")
            {
                lblBillNO.Visible = true;
                lblBillNO.Text = "Select invoice no.";
                ddlInvoiceno.Focus();
            }
            else if (txtMonthedit.Text == "")
            {
                lblBillNO.Visible = true;
                lblBillNO.Text = "Select month.";
                txtMonthedit.Focus();
            }

            else
            {
                ccm.TRANSDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                ccm.TRANSMY = txtMonthedit.Text;
                ccm.TRANSNO = Convert.ToInt64(ddlInvoiceno.Text);

                ccd.deleteCommissionInfo(ccm);

                Refresh();
            }
        }




    }
}