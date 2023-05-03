using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using AlchemyAccounting.payroll.model;
using AlchemyAccounting.payroll.dataAccess;

namespace AlchemyAccounting.payroll.ui
{
    public partial class quotation : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Global.connection);
        SqlCommand cmdd;

        payroll_model iob = new payroll_model();
        payroll_data dob = new payroll_data();

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
                    string user = HttpContext.Current.Session["UserName"].ToString();
                    Global.lblAdd("SELECT PerEd FROM User_Registration WHERE UserID ='" + user + "'", lblEdit);
                    if (lblEdit.Text == "Edit")
                        btnEdit.Visible = true;
                    else
                        btnEdit.Visible = false;

                    Global.lblAdd("SELECT PerDel FROM User_Registration WHERE UserID ='" + user + "'", lblDelete);

                    DateTime today = DateTime.Now;
                    string td = Global.Dayformat(today);
                    string todt = today.ToString("yyyy-MM-dd");
                    string year = today.ToString("yyyy");
                    txtDt.Text = td;
                    txtYear.Text = year;
                    generate_quotation_no();
                    GridShow();
                    txtCompanyName.Focus();
                }
            }
        }

        private void generate_quotation_no()
        {
            lblMxTransNo.Text = "";
            Global.lblAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM HR_QTMST WHERE TRANSYY =" + txtYear.Text + "", lblMxTransNo);

            if (lblMxTransNo.Text == "")
            {
                txtTransNo.Text = "1";
            }
            else
            {
                Int64 trno = Convert.ToInt64(lblMxTransNo.Text);
                txtTransNo.Text = (trno + 1).ToString();
            }

            DateTime txtdate = DateTime.Parse(txtDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string tdate = txtdate.ToString("dd.MM.yy");

            txtQuotation.Text = "HTC/" + txtTransNo.Text + "/" + tdate;

        }

        private void check_multiple_transno()
        {
            lblTransNo.Text = "";
            Global.lblAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM HR_QTMST WHERE TRANSYY =" + txtYear.Text + "", lblTransNo);

            if (lblTransNo.Text == "")
            {
                lblTransNo.Text = "0";
            }
            else
            {
                Int64 trno = Convert.ToInt64(lblTransNo.Text);

                if (trno > (Convert.ToDecimal(txtTransNo.Text)))
                {
                    txtTransNo.Text = (trno + 1).ToString();
                }
            }
        }

        protected void txtDt_TextChanged(object sender, EventArgs e)
        {
            if (txtDt.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select Date.";
                txtDt.Focus();
            }
            else
            {
                lblError.Visible = false;

                DateTime td = DateTime.Parse(txtDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string year = td.ToString("yyyy");
                txtYear.Text = year;
                generate_quotation_no();
                txtCompanyName.Focus();
            }
        }

        private void GridShow()
        {
            conn = new SqlConnection(Global.connection);

            DateTime trDate = DateTime.Parse(txtDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string tDt = trDate.ToString("yyyy-MM-dd");

            string year, no;
            if (btnEdit.Text == "Edit")
            {
                no = txtTransNo.Text;
                year = txtYear.Text;
            }
            else
            {
                if (ddlTransNo.Text == "" || ddlTransNo.Text == "Select")
                    no = "0";
                else
                    no = ddlTransNo.Text;
                if (ddlYear.Text == "" || ddlYear.Text == "Select")
                    year = "0";
                else
                    year = ddlYear.Text;
            }

            conn.Open();
            cmdd = new SqlCommand("SELECT HR_QUOTE.QTTP, HR_QUOTE.QTSL, HR_QUOTE.QTDESC, HR_QUOTE.UNIT, HR_QUOTE.QTRATE, HR_QUOTE.QTQTY, HR_QUOTE.QTQRS " +
                      " FROM HR_QTMST INNER JOIN HR_QUOTE ON HR_QTMST.TRANSYY = HR_QUOTE.TRANSYY AND HR_QTMST.TRANSNO = HR_QUOTE.TRANSNO " +
                      " WHERE HR_QUOTE.TRANSNO=@TRANSNO AND HR_QUOTE.TRANSYY =@TRANSYY", conn);
            cmdd.Parameters.Clear();
            cmdd.Parameters.AddWithValue("@TRANSNO", no);
            cmdd.Parameters.AddWithValue("@TRANSYY", year);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvEmphour.DataSource = ds;
                gvEmphour.DataBind();
                gvEmphour.Visible = true;

                DropDownList ddlQtTp = (DropDownList)gvEmphour.FooterRow.FindControl("ddlQtTp");
                ddlQtTp.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvEmphour.DataSource = ds;
                gvEmphour.DataBind();
                int columncount = gvEmphour.Rows[0].Cells.Count;
                gvEmphour.Rows[0].Cells.Clear();
                gvEmphour.Rows[0].Cells.Add(new TableCell());
                gvEmphour.Rows[0].Cells[0].ColumnSpan = columncount;
                gvEmphour.Rows[0].Cells[0].Text = "No Records Found";
                gvEmphour.Rows[0].Visible = false;
            }

        }

        protected void ddlQtTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlQtTp = (DropDownList)row.FindControl("ddlQtTp");
            TextBox txtDesc = (TextBox)row.FindControl("txtDesc");
            if (ddlQtTp.Text == "TERMS")
            {
                gvEmphour.FooterRow.Cells[3].Enabled = false;
                gvEmphour.FooterRow.Cells[4].Enabled = false;
                gvEmphour.FooterRow.Cells[5].Enabled = false;
                gvEmphour.FooterRow.Cells[6].Enabled = false;
            }
            else
            {
                gvEmphour.FooterRow.Cells[3].Enabled = true;
                gvEmphour.FooterRow.Cells[4].Enabled = true;
                gvEmphour.FooterRow.Cells[5].Enabled = true;
                gvEmphour.FooterRow.Cells[6].Enabled = true;
            }

            string year, no;
            if (btnEdit.Text == "Edit")
            {
                no = txtTransNo.Text;
                year = txtYear.Text;
            }
            else
            {
                if (ddlTransNo.Text == "" || ddlTransNo.Text == "Select")
                    no = "0";
                else
                    no = ddlTransNo.Text;
                if (ddlYear.Text == "" || ddlYear.Text == "Select")
                    year = "0";
                else
                    year = ddlYear.Text;
            }

            lblMxGridSL.Text = "";

            Global.lblAdd("SELECT MAX(QTSL) AS QTSL FROM HR_QUOTE WHERE TRANSYY = " + year + " AND TRANSNO =" + no + "", lblMxGridSL);

            if (lblMxGridSL.Text == "")
                gvEmphour.FooterRow.Cells[1].Text = "1";
            else
            {
                Int64 grsl = Convert.ToInt64(lblMxGridSL.Text) + 1;
                gvEmphour.FooterRow.Cells[1].Text = grsl.ToString();
            }

            txtDesc.Focus();
        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQTy = (TextBox)row.FindControl("txtQTy");
            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
            ImageButton imgbtnAdd = (ImageButton)row.FindControl("imgbtnAdd");

            if (txtQTy.Text == "")
                txtQTy.Text = "0";

            if (txtRate.Text == "")
                txtRate.Text = "0";

            txtTotal.Text = (Convert.ToDecimal(txtQTy.Text) * Convert.ToDecimal(txtRate.Text)).ToString();
            txtQTy.Text = "";
            txtQTy.Focus();
        }

        protected void txtQTy_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQTy = (TextBox)row.FindControl("txtQTy");
            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            TextBox txtTotal = (TextBox)row.FindControl("txtTotal");
            ImageButton imgbtnAdd = (ImageButton)row.FindControl("imgbtnAdd");

            if (txtQTy.Text == "")
                txtQTy.Text = "0";

            if (txtRate.Text == "")
                txtRate.Text = "0";

            txtTotal.Text = (Convert.ToDecimal(txtQTy.Text) * Convert.ToDecimal(txtRate.Text)).ToString();
            imgbtnAdd.Focus();

        }

        protected void gvEmphour_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblQTTP = (Label)e.Row.FindControl("lblQTTP");

                //if (lblQTTP.Text == "TERMS")
                //{
                //    e.Row.Cells[4].Enabled = false;
                //    e.Row.Cells[5].Enabled = false;
                //    e.Row.Cells[6].Enabled = false;
                //}
                //else
                //{
                //    e.Row.Cells[4].Enabled = true;
                //    e.Row.Cells[5].Enabled = true;
                //    e.Row.Cells[6].Enabled = true;
                //}
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlQtTp = (DropDownList)e.Row.FindControl("ddlQtTp");

                string year, no;
                if (btnEdit.Text == "Edit")
                {
                    no = txtTransNo.Text;
                    year = txtYear.Text;
                }
                else
                {
                    if (ddlTransNo.Text == "" || ddlTransNo.Text == "Select")
                        no = "0";
                    else
                        no = ddlTransNo.Text;
                    if (ddlYear.Text == "" || ddlYear.Text == "Select")
                        year = "0";
                    else
                        year = ddlYear.Text;
                }

                lblMxGridSL.Text = "";

                Global.lblAdd("SELECT MAX(QTSL) AS QTSL FROM HR_QUOTE WHERE TRANSYY = " + year + " AND TRANSNO =" + no + "", lblMxGridSL);

                if (lblMxGridSL.Text == "")
                    e.Row.Cells[1].Text = "1";
                else
                {
                    Int64 grsl = Convert.ToInt64(lblMxGridSL.Text) + 1;
                    e.Row.Cells[1].Text = grsl.ToString();
                }
            }
        }

        protected void gvEmphour_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DropDownList ddlQtTp = (DropDownList)gvEmphour.FooterRow.FindControl("ddlQtTp");
            TextBox txtDesc = (TextBox)gvEmphour.FooterRow.FindControl("txtDesc");
            TextBox txtUnit = (TextBox)gvEmphour.FooterRow.FindControl("txtUnit");
            TextBox txtRate = (TextBox)gvEmphour.FooterRow.FindControl("txtRate");
            TextBox txtQTy = (TextBox)gvEmphour.FooterRow.FindControl("txtQTy");
            TextBox txtTotal = (TextBox)gvEmphour.FooterRow.FindControl("txtTotal");
            string sl = gvEmphour.FooterRow.Cells[1].Text;

            if (e.CommandName.Equals("SaveCon"))
            {
                if (txtDesc.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type Description";
                    txtDesc.Focus();
                }
                else if (txtCompanyName.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type Company Name";
                    txtCompanyName.Focus();
                }
                else if (txtSub.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type Subject";
                    txtSub.Focus();
                }
                else
                {
                    if (Session["UserName"] == null)
                    {
                        Response.Redirect("~/Login/UI/Login.aspx");
                    }
                    else
                    {
                        iob.QDt = DateTime.Parse(txtDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                        if (btnEdit.Text == "Edit")
                        {
                            iob.Year = Convert.ToInt16(txtYear.Text);

                            check_multiple_transno();

                            iob.TrNo = Convert.ToInt64(txtTransNo.Text);
                        }
                        else
                        {
                            iob.Year = Convert.ToInt16(ddlYear.Text);
                            iob.TrNo = Convert.ToInt64(ddlTransNo.Text);
                        }
                        
                        iob.QuoteNo = txtQuotation.Text;
                        iob.CompNM = txtCompanyName.Text;
                        iob.CompADD = txtCompanyAddr.Text;
                        iob.CompContact = txtCompanyCont.Text;
                        iob.AttnPerNm = txtAttenPersonNm.Text;
                        iob.AttPerDesig = txtAttenPersonDesig.Text;
                        iob.Subject = txtSub.Text;
                        iob.PrepNM = txtPrepNM.Text;
                        iob.PrepDesig = txtPrepDesig.Text;
                        iob.PrepContact = txtPrepContact.Text;
                        iob.PrepCompNM = txtPrepCompanyNm.Text;
                        iob.QtTp = ddlQtTp.Text;
                        iob.QSL = Convert.ToInt64(sl);
                        iob.Desc = txtDesc.Text;
                        iob.Unit = txtUnit.Text;
                        if (txtRate.Text == "")
                            txtRate.Text = "0";
                        iob.QRate = Convert.ToDecimal(txtRate.Text);
                        if (txtQTy.Text == "")
                            txtQTy.Text = "0";
                        iob.QQty = Convert.ToDecimal(txtQTy.Text);
                        
                        txtTotal.Text = (iob.QQty * iob.QRate).ToString();

                        if (txtTotal.Text == "")
                            txtTotal.Text = "0";

                        iob.QTotal = Convert.ToDecimal(txtTotal.Text);

                        iob.InTm = DateTime.Now;
                        iob.UserNm = HttpContext.Current.Session["UserName"].ToString();
                        iob.Ip = HttpContext.Current.Session["IpAddress"].ToString();
                        iob.UserPc = HttpContext.Current.Session["PCName"].ToString();


                        conn.Open();
                        SqlCommand cmd = new SqlCommand();

                        cmd = new SqlCommand("Select TRANSNO from HR_QTMST where TRANSNO=@TRANSNO and TRANSYY=@TRANSYY ", conn);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@TRANSNO", iob.TrNo);
                        cmd.Parameters.AddWithValue("@TRANSYY", iob.Year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        conn.Close();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dob.payroll_quotation(iob);
                            ddlQtTp.Focus();
                        }
                        else
                        {
                            dob.payroll_quotation_master(iob);
                            dob.payroll_quotation(iob);
                            ddlQtTp.Focus();
                        }

                        GridShow();
                    }
                }
            }

        }

        protected void gvEmphour_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmphour.EditIndex = -1;
            GridShow();
        }

        protected void gvEmphour_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmphour.EditIndex = e.NewEditIndex;
            GridShow();

            Label lblSLEdit = (Label)gvEmphour.Rows[e.NewEditIndex].FindControl("lblSLEdit");

            string year, no;
            if (btnEdit.Text == "Edit")
            {
                no = txtTransNo.Text;
                year = txtYear.Text;
            }
            else
            {
                no = ddlTransNo.Text;
                year = ddlYear.Text;
            }

            Global.lblAdd("SELECT QTTP FROM HR_QUOTE WHERE TRANSYY =" + year + " AND TRANSNO = " + no + " AND QTSL =" + lblSLEdit.Text + "", lblQtTP);

            DropDownList ddlQtTpEdit = (DropDownList)gvEmphour.Rows[e.NewEditIndex].FindControl("ddlQtTpEdit");
            ddlQtTpEdit.Text = lblQtTP.Text;
            ddlQtTpEdit.Focus();
        }

        protected void ddlQtTpEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlQtTpEdit = (DropDownList)row.FindControl("ddlQtTpEdit");
            TextBox txtDescEdit = (TextBox)row.FindControl("txtDescEdit");
            TextBox txtUnitEdit = (TextBox)row.FindControl("txtUnitEdit");
            TextBox txtRateEdit = (TextBox)row.FindControl("txtRateEdit");
            TextBox txtQTYEdit = (TextBox)row.FindControl("txtQTYEdit");
            TextBox txtTotalEdit = (TextBox)row.FindControl("txtTotalEdit");

            if (ddlQtTpEdit.Text == "TERMS")
            {
                txtUnitEdit.Enabled = false;
                txtRateEdit.Enabled = false;
                txtQTYEdit.Enabled = false;
                txtTotalEdit.Enabled = false;
            }
            else
            {
                txtUnitEdit.Enabled = true;
                txtRateEdit.Enabled = true;
                txtQTYEdit.Enabled = true;
                txtTotalEdit.Enabled = true;
            }

            txtDescEdit.Focus();
        }

        protected void txtRateEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQTYEdit = (TextBox)row.FindControl("txtQTYEdit");
            TextBox txtRateEdit = (TextBox)row.FindControl("txtRateEdit");
            TextBox txtTotalEdit = (TextBox)row.FindControl("txtTotalEdit");
            ImageButton imgbtnUpdate = (ImageButton)row.FindControl("imgbtnUpdate");

            if (txtQTYEdit.Text == "")
                txtQTYEdit.Text = "0";

            if (txtRateEdit.Text == "")
                txtRateEdit.Text = "0";

            txtTotalEdit.Text = (Convert.ToDecimal(txtQTYEdit.Text) * Convert.ToDecimal(txtRateEdit.Text)).ToString();
            txtQTYEdit.Focus();
        }

        protected void txtQTYEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

            TextBox txtQTYEdit = (TextBox)row.FindControl("txtQTYEdit");
            TextBox txtRateEdit = (TextBox)row.FindControl("txtRateEdit");
            TextBox txtTotalEdit = (TextBox)row.FindControl("txtTotalEdit");
            ImageButton imgbtnUpdate = (ImageButton)row.FindControl("imgbtnUpdate");

            if (txtQTYEdit.Text == "")
                txtQTYEdit.Text = "0";

            if (txtRateEdit.Text == "")
                txtRateEdit.Text = "0";

            txtTotalEdit.Text = (Convert.ToDecimal(txtQTYEdit.Text) * Convert.ToDecimal(txtRateEdit.Text)).ToString();
            imgbtnUpdate.Focus();

        }

        protected void gvEmphour_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                DropDownList ddlQtTpEdit = (DropDownList)gvEmphour.Rows[e.RowIndex].FindControl("ddlQtTpEdit");
                Label lblSLEdit = (Label)gvEmphour.Rows[e.RowIndex].FindControl("lblSLEdit");
                TextBox txtDescEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtDescEdit");
                TextBox txtUnitEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtUnitEdit");
                TextBox txtRateEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtRateEdit");
                TextBox txtQTYEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtQTYEdit");
                TextBox txtTotalEdit = (TextBox)gvEmphour.Rows[e.RowIndex].FindControl("txtTotalEdit");

                if (txtDescEdit.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type Description";
                    txtDescEdit.Focus();
                }
                else if (txtCompanyName.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type Company Name";
                    txtCompanyName.Focus();
                }
                else if (txtSub.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type Subject";
                    txtSub.Focus();
                }
                else
                {
                    if (Session["UserName"] == null)
                    {
                        Response.Redirect("~/Login/UI/Login.aspx");
                    }
                    else
                    {
                        iob.QDt = DateTime.Parse(txtDt.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        
                        if (btnEdit.Text == "Edit")
                        {
                            iob.Year = Convert.ToInt16(txtYear.Text);
                            iob.TrNo = Convert.ToInt64(txtTransNo.Text);
                        }
                        else
                        {
                            iob.Year = Convert.ToInt16(ddlYear.Text);
                            iob.TrNo = Convert.ToInt64(ddlTransNo.Text);
                        }

                        iob.QuoteNo = txtQuotation.Text;
                        iob.CompNM = txtCompanyName.Text;
                        iob.CompADD = txtCompanyAddr.Text;
                        iob.CompContact = txtCompanyCont.Text;
                        iob.AttnPerNm = txtAttenPersonNm.Text;
                        iob.AttPerDesig = txtAttenPersonDesig.Text;
                        iob.Subject = txtSub.Text;
                        iob.PrepNM = txtPrepNM.Text;
                        iob.PrepDesig = txtPrepDesig.Text;
                        iob.PrepContact = txtPrepContact.Text;
                        iob.PrepCompNM = txtPrepCompanyNm.Text;
                        iob.QtTp = ddlQtTpEdit.Text;
                        iob.QSL = Convert.ToInt64(lblSLEdit.Text);
                        iob.Desc = txtDescEdit.Text;
                        iob.Unit = txtUnitEdit.Text;
                        if (txtRateEdit.Text == "")
                            txtRateEdit.Text = "0";
                        iob.QRate = Convert.ToDecimal(txtRateEdit.Text);
                        if (txtQTYEdit.Text == "")
                            txtQTYEdit.Text = "0";
                        iob.QQty = Convert.ToDecimal(txtQTYEdit.Text);

                        txtTotalEdit.Text = (iob.QQty * iob.QRate).ToString();

                        if (txtTotalEdit.Text == "")
                            txtTotalEdit.Text = "0";

                        iob.QTotal = Convert.ToDecimal(txtTotalEdit.Text);

                        iob.UserNm = HttpContext.Current.Session["UserName"].ToString();
                        iob.Ip = HttpContext.Current.Session["IpAddress"].ToString();
                        iob.UserPc = HttpContext.Current.Session["PCName"].ToString();


                        dob.update_payroll_quotation_master(iob);
                        dob.update_payroll_quotation(iob);

                        gvEmphour.EditIndex = -1;
                        GridShow();

                        DropDownList ddlQtTp = (DropDownList)gvEmphour.FooterRow.FindControl("ddlQtTp");
                        ddlQtTp.Focus();
                    }
                }
            }
        }

        protected void gvEmphour_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (txtDt.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Date";
                    txtDt.Focus();
                }
                else if (txtCompanyName.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type Company Name";
                    txtCompanyName.Focus();
                }
                else if (txtSub.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type Subject";
                    txtSub.Focus();
                }
                else
                {
                    Label lblQTTP = (Label)gvEmphour.Rows[e.RowIndex].FindControl("lblQTTP");
                    Label lblSL = (Label)gvEmphour.Rows[e.RowIndex].FindControl("lblSL");
                    lblError.Visible = false;

                    if (btnEdit.Text == "Edit")
                    {
                        iob.Year = Convert.ToInt16(txtYear.Text);
                        iob.TrNo = Convert.ToInt64(txtTransNo.Text);
                    }
                    else
                    {
                        iob.Year = Convert.ToInt16(ddlYear.Text);
                        iob.TrNo = Convert.ToInt64(ddlTransNo.Text);
                    }

                    iob.QtTp = lblQTTP.Text;
                    iob.QSL = Convert.ToInt64(lblSL.Text);


                    if (btnEdit.Text == "Edit")
                    {
                        dob.delete_payroll_quotation(iob);

                        GridShow();
                    }

                    else
                    {
                        if (lblDelete.Text == "")
                        {
                            lblError.Visible = true;
                            lblError.Text = "You are not permited to continue this operation.";
                        }
                        else
                        {
                            lblError.Visible = false;
                            dob.delete_payroll_quotation(iob);

                            GridShow();
                        }
                    }
                }
            }
        }

        protected void gvEmphour_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmphour.PageIndex = e.NewPageIndex;
            GridShow();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                btnEdit.Text = "New";
                txtDt.Enabled = false;
                txtYear.Visible = false;
                ddlYear.Visible = true;
                Global.dropDownAddWithSelect(ddlYear, "SELECT DISTINCT TRANSYY FROM HR_QTMST");
                txtTransNo.Visible = false;
                ddlTransNo.Visible = true;
                refresh();
                ddlYear.Focus();
            }
            else
            {
                btnEdit.Text = "Edit";
                txtDt.Enabled = true;
                txtYear.Visible = true;
                ddlYear.Visible = false;
                Global.dropDownAddWithSelect(ddlYear, "SELECT DISTINCT TRANSYY FROM HR_QTMST");
                txtTransNo.Visible = true;
                ddlTransNo.Visible = false;
                refresh();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            DateTime today = DateTime.Now;
            string td = Global.Dayformat(today);
            string todt = today.ToString("yyyy-MM-dd");
            string year = today.ToString("yyyy");
            txtDt.Text = td;
            if (btnEdit.Text == "Edit")
            {
                txtYear.Text = year;
                generate_quotation_no();
            }
            else
            {
                txtQuotation.Text = "";
                ddlTransNo.SelectedIndex = -1;
                Global.dropDownAddWithSelect(ddlTransNo, "SELECT TRANSNO FROM HR_QTMST WHERE TRANSYY =0");
                ddlYear.SelectedIndex = -1;
            }

            txtCompanyName.Text = "";
            txtCompanyAddr.Text = "";
            txtCompanyCont.Text = "";
            txtAttenPersonNm.Text = "";
            txtAttenPersonDesig.Text = "";
            txtSub.Text = "";
            txtPrepNM.Text = "";
            txtPrepDesig.Text = "";
            txtPrepContact.Text = "";
            txtPrepCompanyNm.Text = "";

            GridShow();
            if (btnEdit.Text == "Edit")
                txtCompanyName.Focus();
            else
                ddlYear.Focus();
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.Text == "Select")
            {
                lblError.Visible = true;
                lblError.Text = "Select an year.";
                ddlYear.Focus();
            }
            else
            {
                lblError.Visible=false;

                Global.dropDownAddWithSelect(ddlTransNo, "SELECT TRANSNO FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + "");
                ddlTransNo.Focus();
            }
        }

        protected void ddlTransNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.Text == "Select")
            {
                lblError.Visible = true;
                lblError.Text = "Select an year.";
                ddlYear.Focus();
            }
            else if (ddlTransNo.Text == "Select")
            {
                lblError.Visible = false;
                lblError.Text = "Select transaction no.";
                ddlTransNo.Focus();
            }
            else
            {
                Global.txtAdd("SELECT QTNO FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + " AND TRANSNO =" + ddlTransNo.Text + "", txtQuotation);
                Global.txtAdd("SELECT COMPNM FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + " AND TRANSNO =" + ddlTransNo.Text + "", txtCompanyName);
                Global.txtAdd("SELECT COMPADDR FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + " AND TRANSNO =" + ddlTransNo.Text + "", txtCompanyAddr);
                Global.txtAdd("SELECT COMPCNO FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + " AND TRANSNO =" + ddlTransNo.Text + "", txtCompanyCont);
                Global.txtAdd("SELECT ATNPNM FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + " AND TRANSNO =" + ddlTransNo.Text + "", txtAttenPersonNm);
                Global.txtAdd("SELECT ATNPDESIG FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + " AND TRANSNO =" + ddlTransNo.Text + "", txtAttenPersonDesig);
                Global.txtAdd("SELECT SUBJECT FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + " AND TRANSNO =" + ddlTransNo.Text + "", txtSub);
                Global.txtAdd("SELECT PREPBY FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + " AND TRANSNO =" + ddlTransNo.Text + "", txtPrepNM);
                Global.txtAdd("SELECT PREPDESIG FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + " AND TRANSNO =" + ddlTransNo.Text + "", txtPrepDesig);
                Global.txtAdd("SELECT PRECNO FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + " AND TRANSNO =" + ddlTransNo.Text + "", txtPrepContact);
                Global.txtAdd("SELECT PREPCOMPNM FROM HR_QTMST WHERE TRANSYY =" + ddlYear.Text + " AND TRANSNO =" + ddlTransNo.Text + "", txtPrepCompanyNm);
                GridShow();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["date"] = null;
            Session["year"] = null;
            Session["trno"] = null;
            Session["qtno"] = null;
            Session["compnm"] = null;
            Session["compadd"] = null;
            Session["compcont"] = null;
            Session["attpnm"] = null;
            Session["attpdesig"] = null;
            Session["sub"] = null;
            Session["prepby"] = null;
            Session["prepdesig"] = null;
            Session["prepcont"] = null;
            Session["prepcompnm"] = null;

            if (btnEdit.Text == "Edit")
            {
                if (txtDt.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select Date.";
                    txtDt.Focus();
                }
                else
                {
                    Session["year"] = txtYear.Text;
                    Session["trno"] = txtTransNo.Text;
                }
            }
            else
            {
                if (ddlYear.Text == "Select")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select an year.";
                    ddlYear.Focus();
                }
                else if (ddlTransNo.Text == "Select" || ddlTransNo.Text == "")
                {
                    lblError.Visible = false;
                    lblError.Text = "Select transaction no.";
                    ddlTransNo.Focus();
                }
                else
                {
                    Session["year"] = ddlYear.Text;
                    Session["trno"] = ddlTransNo.Text;
                }
            }

            Session["date"] = txtDt.Text;
            Session["qtno"] = txtQuotation.Text;
            Session["compnm"] = txtCompanyName.Text;
            Session["compadd"] = txtCompanyAddr.Text;
            Session["compcont"] = txtCompanyCont.Text;
            Session["attpnm"] = txtAttenPersonNm.Text;
            Session["attpdesig"] = txtAttenPersonDesig.Text;
            Session["sub"] = txtSub.Text;
            Session["prepby"] = txtPrepNM.Text;
            Session["prepdesig"] = txtPrepDesig.Text;
            Session["prepcont"] = txtPrepContact.Text;
            Session["prepcompnm"] = txtPrepCompanyNm.Text;

            Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../report/vis-report/rpt-quotation.aspx','_newtab');", true);
        }
    }
}