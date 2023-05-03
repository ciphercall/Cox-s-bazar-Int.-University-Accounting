using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace AlchemyAccounting.payroll.invoice
{
    public class InvoiceCreateDataAccess
    {

        SqlConnection con;
        SqlCommand cmd;

        public InvoiceCreateDataAccess()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }


        internal DataTable ShowInvoiceInfo(string id)
        {
            DataTable table = new DataTable();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select convert(nvarchar(20),BILLDT,103) as BILLD, BILLYY, PSID,COSTPID,BILLTP,REMARKS from HR_BILLMST where BILLNO='" + id + "' ";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch { }
            return table;
        }

      

        internal string MstInput(InvoiceCreateModel icm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into HR_BILLMST (COMPANYNM, BILLDT, BILLYY,BILLMY,SUBMITPNM,SUBMITPCNO, BILLNO,PSID,COSTPID,BILLTP, USERPC, INTIME, IPADDRESS)" +
                    "values(@COMPANYNM, @BILLDT,@BILLYY,@BILLMY,@SUBMITPNM,@SUBMITPCNO, @BILLNO,@PSID,@COSTPID,@BILLTP, @USERPC, @INTIME , @IPADDRESS  )";

                cmd.Parameters.Clear();

                cmd.Parameters.Add("@COMPANYNM", SqlDbType.NVarChar).Value = icm.COMPANYNM;
                cmd.Parameters.Add("@BILLMY", SqlDbType.NVarChar).Value = icm.BILLMY;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = icm.BILLDT;
                cmd.Parameters.Add("@BILLYY", SqlDbType.BigInt).Value = icm.BILLYY;
                cmd.Parameters.Add("@BILLNO", SqlDbType.BigInt).Value = icm.BILLNO;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = icm.PSID;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = icm.COSTPID;
                cmd.Parameters.Add("@BILLTP", SqlDbType.NVarChar).Value = icm.BILLTP;
                cmd.Parameters.Add("@SUBMITPNM", SqlDbType.NVarChar).Value = icm.SUBMITPNM;
                cmd.Parameters.Add("@SUBMITPCNO", SqlDbType.NVarChar).Value = icm.SUBMITPCNO;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = icm.UserPc;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = icm.InTm;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = icm.Ip;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }

            return s;
        }
        internal string SaveInvoiceInfo(InvoiceCreateModel icm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into HR_BILL (COMPANYNM, BILLDT, BILLYY,BILLMY, BILLNO,PSID,COSTPID,BILLTP, BILLSL,BILLNM,TWORKER,RATEPTP,TOTQPTP,AMTPTP, USERPC, INTIME, IPADDRESS)" +
                   "values(@COMPANYNM, @BILLDT,@BILLYY, @BILLMY, @BILLNO,@PSID,@COSTPID,@BILLTP,@BILLSL,@BILLNM,@TWORKER,@RATEPTP,@TOTQPTP,@AMTPTP, @USERPC, @INTIME ,@IPADDRESS  )";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPANYNM", SqlDbType.NVarChar).Value = icm.COMPANYNM;
                cmd.Parameters.Add("@BILLMY", SqlDbType.NVarChar).Value = icm.BILLMY;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = icm.BILLDT;
                cmd.Parameters.Add("@BILLYY", SqlDbType.BigInt).Value = icm.BILLYY;
                cmd.Parameters.Add("@BILLNO", SqlDbType.BigInt).Value = icm.BILLNO;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = icm.PSID;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = icm.COSTPID;
                cmd.Parameters.Add("@BILLTP", SqlDbType.NVarChar).Value = icm.BILLTP;
               

                cmd.Parameters.Add("@BILLSL", SqlDbType.BigInt).Value = icm.BILLSL;
                cmd.Parameters.Add("@BILLNM", SqlDbType.NVarChar).Value = icm.BILLNM;
                cmd.Parameters.Add("@TWORKER", SqlDbType.BigInt).Value = icm.TWORKER;
                cmd.Parameters.Add("@RATEPTP", SqlDbType.Decimal).Value = icm.RATEPTP;
                cmd.Parameters.Add("@TOTQPTP", SqlDbType.Decimal).Value = icm.TOTQPTP;
                cmd.Parameters.Add("@AMTPTP", SqlDbType.Decimal).Value = icm.AMTPTP;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = icm.UserPc;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = icm.InTm;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = icm.Ip;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }

            return s;
        }
        internal string UpdateInvoiceInfo(InvoiceCreateModel icm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update HR_BILL set BILLNM=@BILLNM, TWORKER=@TWORKER , RATEPTP=@RATEPTP, TOTQPTP=@TOTQPTP, AMTPTP=@AMTPTP  where BILLNO=@BILLNO and BILLYY=@BILLYY and BILLSL =@BILLSL";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@BILLYY", SqlDbType.BigInt).Value = icm.BILLYY;
                cmd.Parameters.Add("@BILLNO", SqlDbType.BigInt).Value = icm.BILLNO;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = icm.BILLDT;

                cmd.Parameters.Add("@BILLSL", SqlDbType.BigInt).Value=icm.BILLSL;
                cmd.Parameters.Add("@BILLNM", SqlDbType.NVarChar).Value = icm.BILLNM;
                cmd.Parameters.Add("@TWORKER", SqlDbType.BigInt).Value = icm.TWORKER;
                cmd.Parameters.Add("@RATEPTP", SqlDbType.Decimal).Value = icm.RATEPTP;
                cmd.Parameters.Add("@TOTQPTP", SqlDbType.Decimal).Value = icm.TOTQPTP;
                cmd.Parameters.Add("@AMTPTP", SqlDbType.Decimal).Value = icm.AMTPTP;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = icm.UserPc;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = icm.InTm;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = icm.Ip;



                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        internal string DeleteInvoiceInfo(InvoiceCreateModel icm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM HR_BILL WHERE BILLNO=@BILLNO and BILLYY=@BILLYY AND BILLDT =@BILLDT AND BILLSL =@BILLSL";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@BILLYY", SqlDbType.BigInt).Value = icm.BILLYY;
                cmd.Parameters.Add("@BILLNO", SqlDbType.BigInt).Value = icm.BILLNO;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = icm.BILLDT;
                cmd.Parameters.Add("@BILLSL", SqlDbType.BigInt).Value = icm.BILLSL;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        internal string DeleteInvoiceInfo_master(InvoiceCreateModel icm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM HR_BILLMST WHERE BILLNO=@BILLNO and BILLYY=@BILLYY AND BILLDT =@BILLDT";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@BILLYY", SqlDbType.BigInt).Value = icm.BILLYY;
                cmd.Parameters.Add("@BILLNO", SqlDbType.BigInt).Value = icm.BILLNO;
                cmd.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = icm.BILLDT;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
    }
}