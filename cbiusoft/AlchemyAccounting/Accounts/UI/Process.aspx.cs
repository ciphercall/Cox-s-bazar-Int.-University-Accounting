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
    public partial class Process : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserTp"].ToString()  == "ADMIN")
            {
                if (!IsPostBack)
                {
                    lblSerial_Mrec.Visible = false;
                    lblSerial_Mpay.Visible = false;
                    lblSerial_Jour.Visible = false;
                    lblSerial_Cont.Visible = false;
                    DateTime today = DateTime.Today.Date;
                    string td = Global.Dayformat1(DateTime.Now).ToString("dd/MM/yyyy");
                    txtDate.Text = td;
                    btnProcess.Focus();
                }
            }
            else
                Response.Redirect("~/Permission_form.aspx");

        }

        public void ShowGrid()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            
            SqlConnection conn = new SqlConnection(Global.connection);

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, CHEQUENO, convert(nvarchar(20),CHEQUEDT,103) as CHEQUEDT, AMOUNT, REMARKS, USERPC, USERID, ACTDTI, INTIME, IPADDRESS " +
                                            " FROM dbo.GL_STRANS where TRANSDT = '" + p_Date + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = false;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = false;
                //Response.Write("<script>alert('No Data Found');</script>");
                //GridView1.Visible = false;
            }
        }

        public void ShowGrid_Multiple()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            
            SqlConnection conn = new SqlConnection(Global.connection);

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, CHEQUENO, convert(nvarchar(20),CHEQUEDT,103) as CHEQUEDT, AMOUNT, REMARKS, USERPC, USERID, ACTDTI, INTIME, IPADDRESS " +
                                            " FROM GL_MTRANS where TRANSDT = '" + p_Date + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridMultiple.DataSource = ds;
                gridMultiple.DataBind();
                gridMultiple.Visible = false;
            }
            else
            {
                gridMultiple.DataSource = ds;
                gridMultiple.DataBind();
                gridMultiple.Visible = false;
                //Response.Write("<script>alert('No Data Found');</script>");
                //GridView1.Visible = false;
            }
        }

        public void ShowGrid_Commission()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            
            SqlConnection conn = new SqlConnection(Global.connection);

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSMY, TRANSNO, PSID, SPID, COSTPID, BILLAMT, COMMAMT, CARRENT, REMARKS " +
                                            " FROM dbo.GL_COMM where TRANSDT = '" + p_Date + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPayCommission.DataSource = ds;
                gvPayCommission.DataBind();
                gvPayCommission.Visible = false;
            }
            else
            {
                gvPayCommission.DataSource = ds;
                gvPayCommission.DataBind();
                gvPayCommission.Visible = false;
                //Response.Write("<script>alert('No Data Found');</script>");
                //GridView1.Visible = false;
            }
        }

        public void ShowGrid_Transaction()
        {
            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            
            SqlConnection conn = new SqlConnection(Global.connection);

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT        'MREC' AS TRANSTP, dbo.EIM_TRANS.TRANSYY, dbo.EIM_TRANS.TRANSNO, dbo.EIM_TRANS.CNBCD, SUM(dbo.EIM_TRANS.AMOUNT) AS AMOUNT, SUM(dbo.EIM_TRANS.VATAMOUNT) AS VATAMT, 
                         dbo.EIM_STUDENT.NEWSTUDENTID + '  ' + dbo.EIM_SEMESTER.SEMESTERNM + '  ' + dbo.EIM_PROGRAM.PROGRAMNM + '  ' + ISNULL(dbo.EIM_TRANS.REMARKS, N'') + '  ' + ISNULL(dbo.EIM_TRANSMST.PONO, N'') 
                         + '  ' + (CASE WHEN EIM_TRANSMST.PODT = '1999-01-01' THEN '' ELSE CONVERT(NVARCHAR(20), EIM_TRANSMST.PODT, 103) END) + '  ' + ISNULL(dbo.EIM_TRANSMST.POBANK, N'') 
                         + '  ' + ISNULL(dbo.EIM_TRANSMST.POBANKBR, N'') AS REMARKS 
FROM            dbo.EIM_TRANS INNER JOIN
                         dbo.EIM_TRANSMST ON dbo.EIM_TRANS.TRANSTP = dbo.EIM_TRANSMST.TRANSTP AND dbo.EIM_TRANS.TRANSNO = dbo.EIM_TRANSMST.TRANSNO AND dbo.EIM_TRANS.TRANSYY = dbo.EIM_TRANSMST.TRANSYY AND 
                         dbo.EIM_TRANS.TRANSDT = dbo.EIM_TRANSMST.TRANSDT INNER JOIN
                         dbo.EIM_PROGRAM ON dbo.EIM_TRANS.PROGRAMID = dbo.EIM_PROGRAM.PROGRAMID INNER JOIN
                         dbo.EIM_SEMESTER ON dbo.EIM_TRANS.SEMESTERID = dbo.EIM_SEMESTER.SEMESTERID INNER JOIN
                         dbo.EIM_STUDENT ON dbo.EIM_TRANS.STUDENTID = dbo.EIM_STUDENT.STUDENTID
WHERE        (dbo.EIM_TRANS.TRANSDT = @TRANSDT) AND (dbo.EIM_TRANS.TRANSTP = 'MREC')
GROUP BY dbo.EIM_TRANS.TRANSYY, dbo.EIM_TRANS.TRANSNO, dbo.EIM_TRANS.CNBCD, 
                         dbo.EIM_STUDENT.NEWSTUDENTID + '  ' + dbo.EIM_SEMESTER.SEMESTERNM + '  ' + dbo.EIM_PROGRAM.PROGRAMNM + '  ' + ISNULL(dbo.EIM_TRANS.REMARKS, N'') + '  ' + ISNULL(dbo.EIM_TRANSMST.PONO, N'') 
                         + '  ' + (CASE WHEN EIM_TRANSMST.PODT = '1999-01-01' THEN '' ELSE CONVERT(NVARCHAR(20), EIM_TRANSMST.PODT, 103) END) + '  ' + ISNULL(dbo.EIM_TRANSMST.POBANK, N'') 
                         + '  ' + ISNULL(dbo.EIM_TRANSMST.POBANKBR, N'') 
ORDER BY dbo.EIM_TRANS.TRANSNO", conn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TRANSDT", p_Date);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvTransaction.DataSource = ds;
                gvTransaction.DataBind();
                gvTransaction.Visible = false;
            }
            else
            {
                gvTransaction.DataSource = ds;
                gvTransaction.DataBind();
                gvTransaction.Visible = false;
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

            foreach (GridViewRow grid in gvTransaction.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "MREC")
                    {
                        lblSerial_Mrec.Text = "";
                        Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MREC' AND TABLEID ='EIM_TRANS' and SERIALNO like '11%' ", lblSerial_Mrec);
                        if (lblSerial_Mrec.Text == "")
                        {
                            serialNo = "11000";
                            iob.SerialNo_MREC = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_Mrec.Text);
                            serial = sl + 1;

                            iob.SerialNo_MREC = serial.ToString();
                        }

                        iob.Transtp = "MREC";
                        //iob.Monyear = grid.Cells[1].Text;
                        iob.TransNo = grid.Cells[2].Text;

                        iob.Debitcd = grid.Cells[3].Text;
                        iob.Creditcd = "301010100001";
                        iob.Amount = Convert.ToDecimal(grid.Cells[4].Text);
                        iob.VatAmount = Convert.ToDecimal(grid.Cells[5].Text);
                        string Remarks = grid.Cells[6].Text;
                        if (Remarks == "&nbsp;")
                            iob.Remarks = "";
                        else
                            iob.Remarks = Remarks;

                        dob.doProcess_Transaction(iob);
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
            ShowGrid();
            ShowGrid_Multiple();
            ShowGrid_Transaction();
            btnProcess.Focus();
        }

        protected void btnProcess_Click(object sender, EventArgs e)
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
                    string userName = Session["UserName"].ToString();
                    AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
                    AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
                    string serialNo = "";
                    int sl, serial;

                    ShowGrid();
                    ShowGrid_Multiple();
                    ShowGrid_Transaction();

                    iob.Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                    string trans_DT = iob.Transdt.ToString("yyyy/MM/dd");
                    iob.Username = userName;

                    
                    SqlConnection conn = new SqlConnection(Global.connection);
                    if (conn.State != ConnectionState.Open)conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'GL_STRANS' and TRANSTP <> 'OPEN'", conn);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd3 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'GL_MTRANS' and TRANSTP <> 'OPEN'", conn);
                    cmd3.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'STK_TRANS' and TRANSTP='JOUR'", conn);
                    cmd1.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'LC_EXPENSE' and TRANSTP='MPAY'", conn);
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd4 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'GL_COMM' and TRANSTP='JOUR'", conn);
                    cmd4.ExecuteNonQuery();

                    SqlCommand cmd5 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'EIM_TRANS' and TRANSTP='MREC'", conn);
                    cmd5.ExecuteNonQuery();

                    //SqlCommand cmd5 = new SqlCommand("Delete from MC_MLEDGER where TRANSDT='" + trans_DT + "' and TABLEID = 'MC_COLLECT' and TRANSTP='MREC'", conn);
                    //cmd5.ExecuteNonQuery(); 
                    if (conn.State != ConnectionState.Closed)conn.Close();
                    foreach (GridViewRow grid in GridView1.Rows)
                    {
                        try
                        {
                            if (grid.Cells[0].Text == "MREC")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MREC' ", lblSerial_Mrec);
                                if (lblSerial_Mrec.Text == "")
                                {
                                    serialNo = "1000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Mrec.Text);
                                    serial = sl + 1;

                                    iob.SerialNo_MREC = serial.ToString();
                                }

                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                //if (grid.Cells[10].Text == "&nbsp;")
                                //{
                                //    iob.Chequeno = null;
                                //}
                                //else
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;



                                dob.doProcess_MREC(iob);
                            }
                            else if (grid.Cells[0].Text == "MPAY")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MPAY' ", lblSerial_Mpay);
                                if (lblSerial_Mpay.Text == "")
                                {
                                    serialNo = "2000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Mpay.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;

                                dob.doProcess_MPAY(iob);
                            }
                            else if (grid.Cells[0].Text == "JOUR")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' ", lblSerial_Jour);
                                if (lblSerial_Jour.Text == "")
                                {
                                    serialNo = "3000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Jour.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;

                                dob.doProcess_JOUR(iob);
                            }
                            else if (grid.Cells[0].Text == "CONT")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'CONT' ", lblSerial_Cont);
                                if (lblSerial_Cont.Text == "")
                                {
                                    serialNo = "4000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Cont.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;


                                dob.doProcess_CONT(iob);
                            }


                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }

                    foreach (GridViewRow grid in gridMultiple.Rows)
                    {
                        try
                        {
                            if (grid.Cells[0].Text == "MREC")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MREC' ", lblSerial_Mrec);
                                if (lblSerial_Mrec.Text == "")
                                {
                                    serialNo = "1000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Mrec.Text);
                                    serial = sl + 1;

                                    iob.SerialNo_MREC = serial.ToString();
                                }

                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                //if (grid.Cells[10].Text == "&nbsp;")
                                //{
                                //    iob.Chequeno = null;
                                //}
                                //else
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;



                                dob.doProcess_MREC_Multiple(iob);
                            }
                            else if (grid.Cells[0].Text == "MPAY")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSMY = '" + grid.Cells[2].Text + "' and TRANSTP = 'MPAY' ", lblSerial_Mpay);
                                if (lblSerial_Mpay.Text == "")
                                {
                                    serialNo = "2000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Mpay.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;

                                dob.doProcess_MPAY_Multiple(iob);
                            }
                            else if (grid.Cells[0].Text == "JOUR")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' ", lblSerial_Jour);
                                if (lblSerial_Jour.Text == "")
                                {
                                    serialNo = "3000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Jour.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;

                                dob.doProcess_JOUR_Multiple(iob);
                            }
                            else if (grid.Cells[0].Text == "CONT")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'CONT' ", lblSerial_Cont);
                                if (lblSerial_Cont.Text == "")
                                {
                                    serialNo = "4000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Cont.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;

                                dob.doProcess_CONT_Multiple(iob);
                            }


                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }
                    Transaction_process();
                    //commission_Process();
                    //Buy_process();
                    //Buy_process_Ret();
                    //Sale_process();
                    //Sale_Discount_process();
                    //Sale_process_Ret();
                    //LC_Process();



                    //MicroCreditCollection_Process();
                    //MicroCreditCollectionMember_Process();

                    Response.Write("<script>alert('Process Completed.');</script>");
                }
            }
        }
    }
}