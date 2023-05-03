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

namespace AlchemyAccounting.Accounts.UI
{
    public partial class CostPool : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

       
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
                        txtCategoryNM.Focus();
                        lblCatID.Text = "";
                    }
                }
                else
                    Response.Redirect("~/Permission_form.aspx");
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            
            SqlConnection conn = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10202', '20202') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P'", conn);
            SqlDataReader oReader;
            if (conn.State != ConnectionState.Open)conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            if (conn.State != ConnectionState.Closed)conn.Close();
            return CompletionSet.ToArray();
        }

        protected void txtCategoryNM_TextChanged(object sender, EventArgs e)
        {
            lblCatID.Text = "";
            lblMaxCatID.Text = "";
            lblPSID.Text = "";
            Global.lblAdd("select ACCOUNTCD from GL_ACCHART where ACCOUNTNM='" + txtCategoryNM.Text + "'", lblPSID);
            if (lblPSID.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Select Category.";
                txtCategoryNM.Text = "";
                txtCategoryNM.Focus();
            }
            else
            {
                lblErrMsg.Visible = false;
                Global.lblAdd(@"select CATID from GL_COSTPMST where PSID='" + lblPSID.Text + "'", lblCatID);
                Global.lblAdd(@"select max(CATID) from GL_COSTPMST", lblMaxCatID);
                if (lblCatID.Text == "")
                {
                    if (lblMaxCatID.Text == "")
                    {
                        lblCatID.Text = "C01";
                    }
                    else
                    {
                        string MaxCatId = lblMaxCatID.Text;
                        string CatId = MaxCatId.Substring(1, 2);
                        string mid, C_ID;
                        int ID = int.Parse(CatId);
                        int CID = ID + 1;
                        if (CID < 10)
                        {
                            mid = CID.ToString();
                            C_ID = "0" + mid;
                        }
                        else
                            C_ID = CID.ToString();
                        string FID = "C" + C_ID.ToString();
                        lblCatID.Text = FID;
                    }
                }
                else
                {

                }
                Search.Focus();
            }
            //BindEmployeeDetails();
        }

        protected void ShowCostPoolDesc()
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                
                SqlConnection conn = new SqlConnection(Global.connection);

                if (conn.State != ConnectionState.Open)conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT CATID, COSTPID, COSTPNM, COSTPSID, (CASE WHEN EFECTFR='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),EFECTFR,103) END)AS EFECTFR, (CASE WHEN EFECTTO='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),EFECTTO,103) END) AS EFECTTO, CPCNT, REMARKS, USERID, USERPC, INTIME, IPADDRSS FROM GL_COSTP WHERE CATID='" + lblCatID.Text + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (conn.State != ConnectionState.Closed)conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                    TextBox txtCOSTPNM = (TextBox)gvDetails.FooterRow.FindControl("txtCOSTPNM");
                    txtCOSTPNM.Focus();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                    //int columncount = gvDetails.Rows[0].Cells.Count;
                    //gvDetails.Rows[0].Cells.Clear();
                    //gvDetails.Rows[0].Cells.Add(new TableCell());
                    //gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                    //gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                    gvDetails.Rows[0].Visible = false;
                    TextBox txtCOSTPNM = (TextBox)gvDetails.FooterRow.FindControl("txtCOSTPNM");
                    txtCOSTPNM.Focus();
                }
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                
                SqlConnection conn = new SqlConnection(Global.connection);

                if (conn.State != ConnectionState.Open)conn.Open();
                SqlCommand cmd = new SqlCommand("Select CATID from GL_COSTPMST  where CATID='" + lblCatID.Text + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (conn.State != ConnectionState.Closed)conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowCostPoolDesc();
                }
                else
                {
                    string userName = Session["UserName"].ToString();

                    string query = "";
                    SqlCommand comm = new SqlCommand(query, conn);

                    query = ("insert into GL_COSTPMST (CATID, CATNM, PSID, USERPC, USERID,IPADDRESS) " +
                                   "values(@CATID, @CATNM, @PSID,'',@USERID,'')");

                    comm = new SqlCommand(query, conn);
                    comm.Parameters.AddWithValue("@USERID", userName);
                    comm.Parameters.AddWithValue("@CATID", lblCatID.Text);
                    comm.Parameters.AddWithValue("@CATNM", txtCategoryNM.Text);
                    comm.Parameters.AddWithValue("@PSID", lblPSID.Text);

                    if (conn.State != ConnectionState.Open)conn.Open();
                    int result = comm.ExecuteNonQuery();
                    if (conn.State != ConnectionState.Closed)conn.Close();
                    ShowCostPoolDesc();
                }
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Global.lblAdd(@"select MAX(COSTPID) from GL_COSTP where CATID = '" + lblCatID.Text + "'", lblIMaxItemID);
                string ItemCD;
                string mxCD, OItemCD, mid, subItemCD;
                int subCD, incrItCD;
                if (lblIMaxItemID.Text == "")
                {
                    ItemCD = lblCatID.Text + "0001";
                }
                else
                {
                    mxCD = lblIMaxItemID.Text;
                    OItemCD = mxCD.Substring(3, 4);
                    subCD = int.Parse(OItemCD);
                    incrItCD = subCD + 1;
                    if (incrItCD < 10)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "000" + mid;
                    }
                    else if (incrItCD < 100)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "00" + mid;
                    }
                    else if (incrItCD < 1000)
                    {
                        mid = incrItCD.ToString();
                        subItemCD = "0" + mid;
                    }
                    else
                        subItemCD = incrItCD.ToString();

                    ItemCD = lblCatID.Text + subItemCD;
                }
                e.Row.Cells[0].Text = lblCatID.Text;
                e.Row.Cells[1].Text = ItemCD;

                string year = Global.Dayformat1(DateTime.Now).ToString("yyyy");
                //TextBox txtProjectYr = (TextBox)e.Row.Cells[4].FindControl("txtProjectYr");
                //txtProjectYr.Text = year;
            }
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string userName = Session["UserName"].ToString();
            string ipAddress = Session["IpAddress"].ToString();
            string PCName = Session["PCName"].ToString();
            
            SqlConnection conn = new SqlConnection(Global.connection);

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);



            if (e.CommandName.Equals("AddNew"))
            {
                if (Session["UserName"]==null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    string CatID, CostPID;
                    CatID = gvDetails.FooterRow.Cells[0].Text;
                    CostPID = gvDetails.FooterRow.Cells[1].Text;
                    TextBox txtCOSTPNM = (TextBox)gvDetails.FooterRow.FindControl("txtCOSTPNM");
                    TextBox txtCOSTPSID = (TextBox)gvDetails.FooterRow.FindControl("txtCOSTPSID");
                    TextBox txtPercent = (TextBox)gvDetails.FooterRow.FindControl("txtPercent");
                    TextBox txtEFECTFR = (TextBox)gvDetails.FooterRow.FindControl("txtEFECTFR");
                    DateTime FRdt = new DateTime();
                    string fromDate = "";
                    if (txtEFECTFR.Text == "")
                    {
                        FRdt = DateTime.Parse("01/01/1900 00:00:00", dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        fromDate = FRdt.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        FRdt = DateTime.Parse(txtEFECTFR.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        fromDate = FRdt.ToString("yyyy-MM-dd");
                    }
                    TextBox txtEFECTTO = (TextBox)gvDetails.FooterRow.FindControl("txtEFECTTO");
                    DateTime Todt = new DateTime();
                    string toDate = "";
                    if (txtEFECTTO.Text == "")
                    {
                        Todt = DateTime.Parse("01/01/1900 00:00:00", dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        toDate = Todt.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        Todt = DateTime.Parse(txtEFECTTO.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        toDate = Todt.ToString("yyyy-MM-dd");
                    }
                    TextBox txtREMARKS = (TextBox)gvDetails.FooterRow.FindControl("txtREMARKS");

                    query = ("insert into GL_COSTP ( CATID, COSTPID, COSTPNM, COSTPSID, PSID, CPCNT, EFECTFR, EFECTTO, REMARKS, USERID, USERPC, IPADDRSS) " +
                             "values(@CATID, @COSTPID, @COSTPNM, @COSTPSID, @PSID, @CPCNT, @EFECTFR, @EFECTTO, @REMARKS, @USERID, @USERPC, @IPADDRSS)");

                    comm = new SqlCommand(query, conn);
                    comm.Parameters.Clear();
                    comm.Parameters.AddWithValue("@CatID", CatID);
                    comm.Parameters.AddWithValue("@CostPID", CostPID);
                    comm.Parameters.AddWithValue("@COSTPNM", txtCOSTPNM.Text);
                    comm.Parameters.AddWithValue("@COSTPSID", txtCOSTPSID.Text);
                    comm.Parameters.AddWithValue("@PSID", lblPSID.Text);
                    comm.Parameters.AddWithValue("@CPCNT", txtPercent.Text);
                    comm.Parameters.AddWithValue("@EFECTFR", fromDate);
                    comm.Parameters.AddWithValue("@EFECTTO", toDate);
                    comm.Parameters.AddWithValue("@REMARKS", txtREMARKS.Text);
                    comm.Parameters.AddWithValue("@USERID", userName);
                    comm.Parameters.AddWithValue("@USERPC", PCName);
                    comm.Parameters.AddWithValue("@IPADDRSS", ipAddress);

                    if (conn.State != ConnectionState.Open)conn.Open();
                    int result = comm.ExecuteNonQuery();
                    if (conn.State != ConnectionState.Closed)conn.Close();
                    ShowCostPoolDesc();
                }
            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            ShowCostPoolDesc();
            TextBox txtCOSTPNMEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtCOSTPNMEdit");
            txtCOSTPNMEdit.Focus();
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
                string ipAddress = Session["IpAddress"].ToString();
                string PCName = Session["PCName"].ToString();

                Label lblCatGIDEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblCatGIDEdit");
                Label lblCOSTPIDEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblCOSTPIDEdit");
                TextBox txtCOSTPNMEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCOSTPNMEdit");
                TextBox txtCOSTPSIDEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCOSTPSIDEdit");
                TextBox txtPercentEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtPercentEdit");
                TextBox txtEFECTFREdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtEFECTFREdit");
                DateTime FRdt = new DateTime();
                string fromDate = "";
                if (txtEFECTFREdit.Text == "")
                {
                    FRdt = DateTime.Parse("01/01/1900 00:00:00", dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    fromDate = FRdt.ToString("yyyy-MM-dd");
                }
                else
                {
                    FRdt = DateTime.Parse(txtEFECTFREdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    fromDate = FRdt.ToString("yyyy-MM-dd");
                }
                TextBox txtEFECTTOEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtEFECTTOEdit");
                DateTime Todt = new DateTime();
                string toDate = "";
                if (txtEFECTTOEdit.Text == "")
                {
                    Todt = DateTime.Parse("01/01/1900 00:00:00", dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    toDate = Todt.ToString("yyyy-MM-dd");
                }
                else
                {
                    Todt = DateTime.Parse(txtEFECTTOEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    toDate = Todt.ToString("yyyy-MM-dd");
                }
                TextBox txtREMARKSEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtREMARKSEdit");

                if (conn.State != ConnectionState.Open)conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE GL_COSTP SET COSTPNM =@COSTPNM, COSTPSID =@COSTPSID, PSID =@PSID, CPCNT =@CPCNT, EFECTFR =@EFECTFR, EFECTTO =@EFECTTO, REMARKS =@REMARKS, USERID =@USERID, USERPC =@USERPC, IPADDRSS =@IPADDRSS WHERE CATID = @CATID AND COSTPID =@COSTPID", conn);

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CatID", lblCatGIDEdit.Text);
                cmd.Parameters.AddWithValue("@CostPID", lblCOSTPIDEdit.Text);
                cmd.Parameters.AddWithValue("@COSTPNM", txtCOSTPNMEdit.Text);
                cmd.Parameters.AddWithValue("@COSTPSID", txtCOSTPSIDEdit.Text);
                cmd.Parameters.AddWithValue("@PSID", lblPSID.Text);
                cmd.Parameters.AddWithValue("@CPCNT", txtPercentEdit.Text);
                cmd.Parameters.AddWithValue("@EFECTFR", fromDate);
                cmd.Parameters.AddWithValue("@EFECTTO", toDate);
                cmd.Parameters.AddWithValue("@REMARKS", txtREMARKSEdit.Text);
                cmd.Parameters.AddWithValue("@USERID", userName);
                cmd.Parameters.AddWithValue("@USERPC", PCName);
                cmd.Parameters.AddWithValue("@IPADDRSS", ipAddress);

                cmd.ExecuteNonQuery();
                if (conn.State != ConnectionState.Closed)conn.Close();
                gvDetails.EditIndex = -1;
                ShowCostPoolDesc();
            }
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                
                SqlConnection conn = new SqlConnection(Global.connection);

                Label lblCatGID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblCatGID");
                Label lblCOSTPID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblCOSTPID");

                Global.lblAdd(@"select COSTPID from GL_MTRANS where COSTPID = '" + lblCOSTPID.Text + "'", lblChkItemID);

                int result = 0;

                if (lblChkItemID.Text == "")
                {
                    if (conn.State != ConnectionState.Open)conn.Open();
                    SqlCommand cmd = new SqlCommand("delete FROM GL_COSTP where CATID = '" + lblCatGID.Text + "' and COSTPID = '" + lblCOSTPID.Text + "'", conn);
                    result = cmd.ExecuteNonQuery();
                    if (conn.State != ConnectionState.Closed)conn.Close();
                }

                else
                {
                    Response.Write("<script>alert('This Item has a Transaction.');</script>");
                }

                if (result == 1)
                {
                    ShowCostPoolDesc();
                }
            }
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            ShowCostPoolDesc();
        }

        protected void txtEFECTFR_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtEFECTTO = (TextBox)row.FindControl("txtEFECTTO");
            txtEFECTTO.Focus();
        }

        protected void txtEFECTFREdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtEFECTTOEdit = (TextBox)row.FindControl("txtEFECTTOEdit");
            txtEFECTTOEdit.Focus();
        }

        //[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        //public static string[] GetCompletionListProject(string prefixText, int count, string contextKey)
        //{
        //    
        //    SqlConnection conn = new SqlConnection(Global.connection);
        //    SqlCommand cmd = new SqlCommand("SELECT DISTINCT PROJECTTP FROM GL_COSTP WHERE PROJECTTP like '" + prefixText + "%'", conn);
        //    SqlDataReader oReader;
        //    if (conn.State != ConnectionState.Open)conn.Open();
        //    List<String> CompletionSet = new List<string>();
        //    oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    while (oReader.Read())
        //        CompletionSet.Add(oReader["PROJECTTP"].ToString());
        //    return CompletionSet.ToArray();
        //}

        //[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        //public static string[] GetCompletionListProjectEdit(string prefixText, int count, string contextKey)
        //{
        //    
        //    SqlConnection conn = new SqlConnection(Global.connection);
        //    SqlCommand cmd = new SqlCommand("SELECT DISTINCT PROJECTTP FROM GL_COSTP WHERE PROJECTTP like '" + prefixText + "%'", conn);
        //    SqlDataReader oReader;
        //    if (conn.State != ConnectionState.Open)conn.Open();
        //    List<String> CompletionSet = new List<string>();
        //    oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    while (oReader.Read())
        //        CompletionSet.Add(oReader["PROJECTTP"].ToString());
        //    return CompletionSet.ToArray();
        //}

        protected void txtEFECTTO_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtREMARKS = (TextBox)row.FindControl("txtREMARKS");
            txtREMARKS.Focus();
        }

        protected void txtEFECTTOEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtREMARKSEdit = (TextBox)row.FindControl("txtREMARKSEdit");
            txtREMARKSEdit.Focus();
        }

    }
}