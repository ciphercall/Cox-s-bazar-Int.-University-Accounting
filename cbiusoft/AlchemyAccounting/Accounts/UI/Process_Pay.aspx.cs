using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Accounts.UI
{
    public partial class Process_Pay : System.Web.UI.Page
    {

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserTp"].ToString()  == "ADMIN")
            {
                if (!IsPostBack)
                {
                    DateTime today = DateTime.Today.Date;
                    string td = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                    txtDate.Text = td;
                    btnProcess.Focus();
                }
            }
            else
                Response.Redirect("~/Permission_form.aspx");

        }

        public void ShowGrid_Transaction()
        {
            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            
            SqlConnection conn = new SqlConnection(Global.connection);

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@" SELECT 'JOUR' AS TRANSTP, EIM_TRANS.TRANSYY, EIM_TRANS.TRANSNO, EIM_TRANS.CNBCD, SUM(EIM_TRANS.AMOUNT) AS AMOUNT, EIM_TRANS.STUDENTID + '  ' + EIM_SEMESTER.SEMESTERNM + '  ' + EIM_PROGRAM.PROGRAMNM + '  ' + ISNULL(EIM_TRANS.REMARKS,'') + '  ' + ISNULL(PONO,'') +'  '+
            (CASE WHEN EIM_TRANSMST.PODT='1999-01-01' THEN '' ELSE CONVERT(NVARCHAR(20),EIM_TRANSMST.PODT,103) END) +'  ' + ISNULL(EIM_TRANSMST.POBANK,'') + '  ' + 
            ISNULL(EIM_TRANSMST.POBANKBR,'')  AS REMARKS, EIM_TRANS.FEESID 
            FROM         EIM_TRANS INNER JOIN
                                  EIM_TRANSMST ON EIM_TRANS.TRANSTP = EIM_TRANSMST.TRANSTP AND EIM_TRANS.TRANSNO = EIM_TRANSMST.TRANSNO AND EIM_TRANS.TRANSYY = EIM_TRANSMST.TRANSYY AND 
                                  EIM_TRANS.TRANSDT = EIM_TRANSMST.TRANSDT INNER JOIN
                                  EIM_PROGRAM ON EIM_TRANS.PROGRAMID = EIM_PROGRAM.PROGRAMID INNER JOIN
                                  EIM_SEMESTER ON EIM_TRANS.SEMESTERID = EIM_SEMESTER.SEMESTERID
            WHERE     (EIM_TRANS.TRANSDT = @TRANSDT AND EIM_TRANS.TRANSTP = 'JOUR' AND TRANSFOR='PAYABLE') 
            GROUP BY  EIM_TRANS.TRANSYY, EIM_TRANS.TRANSNO, EIM_TRANS.CNBCD, EIM_TRANS.FEESID, (EIM_TRANS.STUDENTID + '  ' + EIM_SEMESTER.SEMESTERNM + '  ' + EIM_PROGRAM.PROGRAMNM + '  ' + ISNULL(EIM_TRANS.REMARKS,'') + '  ' + ISNULL(PONO,'') +'  '+
            (CASE WHEN EIM_TRANSMST.PODT='1999-01-01' THEN '' ELSE CONVERT(NVARCHAR(20),EIM_TRANSMST.PODT,103) END) +'  ' + ISNULL(EIM_TRANSMST.POBANK,'') + '  ' + 
            ISNULL(EIM_TRANSMST.POBANKBR,''))
            ORDER BY TRANSNO", conn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TRANSDT", p_Date);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Receivable.DataSource = ds;
                gv_Receivable.DataBind();
                gv_Receivable.Visible = false;
            }
            else
            {
                gv_Receivable.DataSource = ds;
                gv_Receivable.DataBind();
                gv_Receivable.Visible = false;
                //Response.Write("<script>alert('No Data Found');</script>");
                //GridView1.Visible = false;
            }
        }
        public void Transaction_process()
        {
            string userName = Session["UserName"].ToString();
            AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
            AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string mon = Transdt.ToString("MMM").ToUpper();
            string year = Transdt.ToString("yy");
            iob.Monyear = mon + '-' + year;
            string trans_DT = Transdt.ToString("yyyy/MM/dd");
            iob.Transdt = Transdt;

            foreach (GridViewRow grid in gv_Receivable.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "JOUR")
                    {
                        lblSerial_JOUR.Text = "";
                        Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' AND TABLEID ='EIM_TRANS' and SERIALNO like '31%' ", lblSerial_JOUR);
                        if (lblSerial_JOUR.Text == "")
                        {
                            serialNo = "31000";
                            iob.SerialNo_JOUR = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_JOUR.Text);
                            serial = sl + 1;

                            iob.SerialNo_JOUR = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        //iob.Monyear = grid.Cells[1].Text;
                        iob.TransNo = grid.Cells[2].Text;

                        iob.Creditcd = grid.Cells[3].Text;
                        if (grid.Cells[7].Text == "113")
                            iob.Debitcd = "401010900001";
                        else if (grid.Cells[7].Text == "114")
                            iob.Debitcd = "401010900002";
                        else if (grid.Cells[7].Text == "115")
                            iob.Debitcd = "401010900003";
                        else if (grid.Cells[7].Text == "116")
                            iob.Debitcd = "401010900004";
                        else if (grid.Cells[7].Text == "117")
                            iob.Debitcd = "401010900005";
                        else if (grid.Cells[7].Text == "118")
                            iob.Debitcd = "401010900006";
                        iob.Amount = Convert.ToDecimal(grid.Cells[4].Text);

                        string Remarks = grid.Cells[6].Text;
                        if (Remarks == "&nbsp;")
                            iob.Remarks = "";
                        else
                            iob.Remarks = Remarks;



                        dob.doProcess_Transaction_Pay(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

        }
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            // ShowGrid();
            // ShowGrid_Multiple();
            ShowGrid_Transaction();
            //btnProcess.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["UserName"]==null)
            {
                Response.Redirect("~/cbiu/signin.aspx");
            }
            else
            {
                if (txtDate.Text == "")
                {
                    Response.Write("<script>alert('Select a Date want to process?');</script>");
                }
                else
                {
                    ShowGrid_Transaction();
                    Transaction_process();
                }
            }
        }
    }
}