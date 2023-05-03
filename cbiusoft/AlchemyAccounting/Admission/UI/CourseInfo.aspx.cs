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
    public partial class CourseInfo : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        Data_Access dob = new Data_Access();
        Data_Model iob = new Data_Model();
        SqlConnection conn = new SqlConnection(Global.connection);
       
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
                    ddlProNM.SelectedIndex = -1;
                    Global.dropDownAdd(ddlProNM, @" SELECT PROGRAMNM FROM EIM_PROGRAM");

                    ddlProNM.Focus();
                }
            }
        }
        private void gridShow()
        {

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT COURSENM,COURSEID,COURSECD,CREDITHH,SEMID,SEMSL,REMARKS FROM  EIM_COURSE WHERE PROGRAMID='" + lblProID.Text + "' ORDER BY COURSEID", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCourse.DataSource = ds;
                gvCourse.DataBind();

                TextBox txtCOURSENMFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSENMFooter");
                txtCOURSENMFooter.Focus();
            }

            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvCourse.DataSource = ds;
                gvCourse.DataBind();
                int columncount = gvCourse.Rows[0].Cells.Count;
                gvCourse.Rows[0].Cells.Clear();
                gvCourse.Rows[0].Cells.Add(new TableCell());
                gvCourse.Rows[0].Cells[0].ColumnSpan = columncount;
                gvCourse.Rows[0].Visible = false;
                TextBox txtCOURSENMFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSENMFooter");
                txtCOURSENMFooter.Focus();
            }
        }
        private void CrsIDCD()
        {
            TextBox txtCOURSENMFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSENMFooter");
            TextBox txtCOURSEIDFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSEIDFooter");
            TextBox txtCOURSECDFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSECDFooter");
            TextBox txtCREDITHHFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCREDITHHFooter");
            TextBox txtREMARKSFooter = (TextBox)gvCourse.FooterRow.FindControl("txtREMARKSFooter");

            lblCrsID.Text = "";
            lblCrsCD.Text = "";

            Global.lblAdd(@"SELECT PROGRAMSID FROM  EIM_PROGRAM  WHERE PROGRAMNM='" + ddlProNM.Text + "'", lblProSrtNM);
            Global.lblAdd(@"SELECT MAX(substring(COURSEID,3,4)) FROM EIM_COURSE WHERE PROGRAMID='" + lblProID.Text + "'", lblCrsID);
            int CrsID = 0;
            if (lblCrsID.Text == "")
                lblCrsID.Text = "0";
            CrsID = int.Parse(lblCrsID.Text) + 1;
            if (lblCrsID.Text == "0")
            {
                iob.CrsID = lblProID.Text + "01";
                iob.CrsCD = lblProSrtNM.Text + "-" + iob.CrsID;
                txtCOURSECDFooter.Text = iob.CrsCD;
                txtCOURSEIDFooter.Text = iob.CrsID;

            }
            else
            {
                if (CrsID < 10)
                {
                    iob.CrsID = lblProID.Text + "0" + CrsID;
                    iob.CrsCD = lblProSrtNM.Text + "-" + iob.CrsID;
                    txtCOURSECDFooter.Text = iob.CrsCD;
                    txtCOURSEIDFooter.Text = iob.CrsID;
                }
                else if (CrsID < 90)
                {
                    iob.CrsID = lblProID.Text + CrsID;
                    iob.CrsCD = lblProSrtNM.Text + "-" + iob.CrsID;
                    txtCOURSECDFooter.Text = iob.CrsCD;
                    txtCOURSEIDFooter.Text = iob.CrsID;
                }
            }
        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {

            //if(ddlProNM.Text=="Select")
            //{
            //    ddlProNM.Focus();
            //}
            //else
            //{
            //    CrsIDCD();
            //    iob.CrsNM = txtCrsNM.Text;
            //    iob.Remarks = txtCrsRMK.Text;
            //}
        }

        protected void ddlProNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMSG.Text = "";
            if (ddlProNM.Text == "Select")
            {
                lblProID.Text = "";
                ddlProNM.Focus();
                gvCourse.Visible = false;
            }
            else
            {
                lblProID.Text = "";
                Global.lblAdd(@"SELECT PROGRAMID FROM  EIM_PROGRAM WHERE PROGRAMNM='" + ddlProNM.Text + "'", lblProID);
                gvCourse.Visible = true;
                gridShow();
                ////CrsIDCD();
                TextBox txtCOURSENMFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSENMFooter");
                txtCOURSENMFooter.Focus();
            }
        }

        private void ClearFooter()
        {
            TextBox txtCOURSENMFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSENMFooter");
            TextBox txtCOURSEIDFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSEIDFooter");
            TextBox txtCOURSECDFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSECDFooter");
            TextBox txtCREDITHHFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCREDITHHFooter");
            TextBox txtREMARKSFooter = (TextBox)gvCourse.FooterRow.FindControl("txtREMARKSFooter");
            txtCOURSEIDFooter.Text = "";
            txtCOURSECDFooter.Text = "";
            txtCREDITHHFooter.Text = "";
            txtREMARKSFooter.Text = "";
            txtCOURSENMFooter.Focus();
        }
        protected void txtCOURSENMFooter_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCOURSENMFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSENMFooter");
            TextBox txtCOURSEIDFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSEIDFooter");
            TextBox txtCOURSECDFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSECDFooter");
            TextBox txtCREDITHHFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCREDITHHFooter");
            TextBox txtREMARKSFooter = (TextBox)gvCourse.FooterRow.FindControl("txtREMARKSFooter");
            if (txtCOURSENMFooter.Text == "")
            {
                ClearFooter();
            }
            else
            {
                CrsIDCD();
                txtCOURSECDFooter.Focus();
            }
        }

        protected void gvCourse_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (Session["UserName"]==null)
                {
                    Response.Redirect("~/cbiu/signin.aspx");
                }
                else
                {
                    TextBox txtCOURSENMFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSENMFooter");
                    TextBox txtCOURSEIDFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSEIDFooter");
                    TextBox txtCOURSECDFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCOURSECDFooter");
                    TextBox txtCREDITHHFooter = (TextBox)gvCourse.FooterRow.FindControl("txtCREDITHHFooter");
                    TextBox txtREMARKSFooter = (TextBox)gvCourse.FooterRow.FindControl("txtREMARKSFooter");
                    DropDownList ddlSemisterNMFooter = (DropDownList)gvCourse.FooterRow.FindControl("ddlSemisterNMFooter");
                    Label lblSEMSLFooter = (Label)gvCourse.FooterRow.FindControl("lblSEMSLFooter");
                    if (e.CommandName.Equals("Add"))
                    {
                        if (conn.State != ConnectionState.Open)conn.Open();
                        string Search = "SELECT * FROM EIM_COURSE WHERE COURSENM='" + txtCOURSENMFooter.Text + "' AND PROGRAMID='" + lblProID.Text + "'";
                        SqlCommand cmd = new SqlCommand(Search, conn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (conn.State != ConnectionState.Closed)conn.Close();
                        iob.UserID = Session["UserName"].ToString();
                        iob.Ipaddress = Session["IpAddress"].ToString();
                        iob.PcName = Session["PCName"].ToString();
                        iob.InTime = Global.Dayformat1(DateTime.Now);

                        if (lblProID.Text == "")
                        {
                            ddlProNM.Focus();
                        }
                        else if (txtCOURSEIDFooter.Text == "")
                        {
                            txtCOURSENMFooter.Focus();
                        }
                        else if (txtCOURSECDFooter.Text == "")
                        {
                            txtCOURSECDFooter.Focus();
                        }
                        else if (txtCREDITHHFooter.Text == "")
                        {
                            txtCREDITHHFooter.Focus();
                        }
                        else if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblMSG.Visible = true;
                            lblMSG.Text = "Already Exist This Course Name !";
                        }

                        else
                        {
                            lblMSG.Visible = false;
                            iob.SemisterID = ddlSemisterNMFooter.Text;
                            iob.SemSL = lblSEMSLFooter.Text;
                            iob.ProgID = lblProID.Text;
                            iob.CrsID = txtCOURSEIDFooter.Text;
                            iob.CrsCD = txtCOURSECDFooter.Text;
                            iob.TotlCrdt = Convert.ToDouble(txtCREDITHHFooter.Text);
                            iob.CrsNM = txtCOURSENMFooter.Text;
                            iob.Remarks = txtREMARKSFooter.Text;
                            dob.InsertCourse(iob);
                            gridShow();

                        }
                    }
                }
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }
        }

        protected void gvCourse_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {// Label lblPROGRAMID = (Label)gvCourse.Rows[e.RowIndex].FindControl("lblPROGRAMID");
                Label lblCOURSEID = (Label)gvCourse.Rows[e.RowIndex].FindControl("lblCOURSEID");
                Label lblCOURSECD = (Label)gvCourse.Rows[e.RowIndex].FindControl("lblCOURSECD");
                if (conn.State != ConnectionState.Open)conn.Open();
                SqlCommand cmd = new SqlCommand("Delete from EIM_COURSE where COURSEID= '" + lblCOURSEID.Text + "' and COURSECD='" + lblCOURSECD.Text + "'", conn);
                int result = cmd.ExecuteNonQuery();
                if (conn.State != ConnectionState.Closed)conn.Close();
                if (result == 1)
                {
                    gridShow();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

        protected void gvCourse_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtCOURSENMEdit = (TextBox)gvCourse.Rows[e.RowIndex].FindControl("txtCOURSENMEdit");
                TextBox txtCOURSEIDEdit = (TextBox)gvCourse.Rows[e.RowIndex].FindControl("txtCOURSEIDEdit");
                TextBox txtCOURSECDEdit = (TextBox)gvCourse.Rows[e.RowIndex].FindControl("txtCOURSECDEdit");
                TextBox txtCREDITHHEdit = (TextBox)gvCourse.Rows[e.RowIndex].FindControl("txtCREDITHHEdit");
                TextBox txtREMARKSEdit = (TextBox)gvCourse.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                DropDownList ddlSemisterNMEdit = (DropDownList)gvCourse.Rows[e.RowIndex].FindControl("ddlSemisterNMEdit");
                Label lblSEMSLEdit = (Label)gvCourse.Rows[e.RowIndex].FindControl("lblSEMSLEdit");
                iob.UPDUserID = Session["UserName"].ToString();
                iob.UPDIpaddress = Session["IpAddress"].ToString();
                iob.UPDPcName = Session["PCName"].ToString();
                iob.UPDTime = Global.Dayformat1(DateTime.Now);


                if (ddlProNM.Text == "Select")
                {
                    ddlProNM.Focus();
                }
                else if (txtCOURSENMEdit.Text == "")
                {
                    txtCOURSENMEdit.Focus();
                }
                else if (txtCOURSEIDEdit.Text == "")
                {
                    txtCOURSENMEdit.Focus();
                }
                else if (txtCOURSECDEdit.Text == "")
                {
                    txtCOURSECDEdit.Focus();
                }
                else if (txtCREDITHHEdit.Text == "")
                {
                    txtCREDITHHEdit.Focus();
                }
                else
                {
                    lblMSG.Visible = false;
                    //CrsIDCD();
                    iob.ProgID = lblProID.Text;
                    iob.CrsID = txtCOURSEIDEdit.Text;
                    iob.CrsCD = txtCOURSECDEdit.Text;
                    iob.TotlCrdt = Convert.ToDouble(txtCREDITHHEdit.Text);
                    iob.CrsNM = txtCOURSENMEdit.Text;
                    iob.SemisterID = ddlSemisterNMEdit.Text;
                    iob.SemSL = lblSEMSLEdit.Text;
                    iob.Remarks = txtREMARKSEdit.Text;
                    dob.UpdateCourse(iob);

                    gvCourse.EditIndex = -1;
                    gridShow();

                }
            }
            catch
            {
                //lblMSG1.Visible = true;
                //lblMSG1.Text = "Input Type is not a Correct formate";
            }
        }

        protected void gvCourse_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCourse.EditIndex = e.NewEditIndex;
            gridShow();

            TextBox txtCOURSENMEdit = (TextBox)gvCourse.Rows[e.NewEditIndex].FindControl("txtCOURSENMEdit");
            DropDownList ddlSemisterNMEdit = (DropDownList)gvCourse.Rows[e.NewEditIndex].FindControl("ddlSemisterNMEdit");
            Label lblSem = (Label)gvCourse.Rows[e.NewEditIndex].FindControl("lblSem");
            ddlSemisterNMEdit.Text = lblSem.Text;
            txtCOURSENMEdit.Focus();
        }

        protected void txtCOURSENMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow Row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCREDITHHEdit = (TextBox)Row.FindControl("txtCREDITHHEdit");
            TextBox txtCOURSECDEdit = (TextBox)Row.FindControl("txtCOURSECDEdit");
            TextBox txtCOURSENMEdit = (TextBox)Row.FindControl("txtCOURSENMEdit");
            if (txtCOURSENMEdit.Text == "")
                txtCOURSENMEdit.Focus();
            else
                txtCOURSECDEdit.Focus();

        }

        protected void gvCourse_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCourse.EditIndex = -1;
            gridShow();
        }

        protected void ddlSemisterNMFooter_SelectedIndexChanged(object sender, EventArgs e)
        {

            //GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            DropDownList ddlSemisterNMFooter = (DropDownList)gvCourse.FooterRow.FindControl("ddlSemisterNMFooter");
            Label lblSEMSLFooter = (Label)gvCourse.FooterRow.FindControl("lblSEMSLFooter");
            TextBox txtREMARKSFooter = (TextBox)gvCourse.FooterRow.FindControl("txtREMARKSFooter");
            string ID = ddlSemisterNMFooter.SelectedValue;
            if (ddlSemisterNMFooter.Text != "")
            {
                string SLID = lblProID.Text + ID;
                Label lblSL = new Label();
                Global.lblAdd("SELECT MAX(SEMSL) FROM EIM_COURSE WHERE SEMID='" + ddlSemisterNMFooter.SelectedValue + "' AND PROGRAMID='" + lblProID.Text + "'", lblSL);
                if (lblSL.Text == "")
                {
                    lblSEMSLFooter.Text = SLID + "1";
                }
                else
                {
                    int SEMSLID = int.Parse(lblSL.Text) + 1; ;
                    lblSEMSLFooter.Text = SEMSLID.ToString();
                }
                txtREMARKSFooter.Focus();
            }
        }

        protected void ddlSemisterNMEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            DropDownList ddlSemisterNMEdit = (DropDownList)row.FindControl("ddlSemisterNMEdit");
            Label lblSEMSLEdit = (Label)row.FindControl("lblSEMSLEdit");
            TextBox txtREMARKSEdit = (TextBox)row.FindControl("txtREMARKSEdit");
            string ID = ddlSemisterNMEdit.SelectedValue;
            if (ddlSemisterNMEdit.Text != "")
            {
                string SLID = lblProID.Text + ID;
                Label lblSL = new Label();
                Global.lblAdd("SELECT MAX(SEMSL) FROM EIM_COURSE WHERE SEMID='" + ddlSemisterNMEdit.Text + "' PROGRAMID='" + lblProID.Text + "'", lblSL);
                if (lblSL.Text == "")
                {
                    lblSEMSLEdit.Text = SLID + "1";
                }
                else
                {
                    int SEMSLID = int.Parse(lblSL.Text) + 1; ;
                    lblSEMSLEdit.Text = SEMSLID.ToString();
                }
                txtREMARKSEdit.Focus();
            }
        }


    }
}