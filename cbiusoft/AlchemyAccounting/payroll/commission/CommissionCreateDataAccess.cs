using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.payroll.commission
{
    public class CommissionCreateDataAccess
    {
        SqlConnection con;
        SqlCommand cmd;

        public CommissionCreateDataAccess()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }


        internal string SaveCommissionInfo(CommissionCreateModel ccm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "insert into GL_COMM (TRANSDT, TRANSMY, TRANSNO, PSID, SPID, COSTPID, BILLAMT, COMPCNT,COMMAMT,CARRENT,ADVAMTP,TOTAMT,ADVAMTC,NETAMT,REMARKS, USERPC, INTIME, IPADDRSS)" +
                   "values(@TRANSDT, @TRANSMY,@TRANSNO, @PSID, @SPID, @COSTPID, @BILLAMT, @COMPCNT,@COMMAMT,@CARRENT,@ADVAMTP,@TOTAMT,@ADVAMTC,@NETAMT,@REMARKS, @USERPC, @INTIME ,@IPADDRSS  )";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ccm.TRANSDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ccm.TRANSMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ccm.TRANSNO;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ccm.PSID;
                cmd.Parameters.Add("@SPID", SqlDbType.NVarChar).Value = ccm.Payable;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ccm.COSTPID;
                cmd.Parameters.Add("@BILLAMT", SqlDbType.Decimal).Value = ccm.BILLAMT;
                cmd.Parameters.Add("@COMPCNT", SqlDbType.Decimal).Value = ccm.COMPCNT;
                cmd.Parameters.Add("@COMMAMT", SqlDbType.Decimal).Value = ccm.COMMAMT;


                cmd.Parameters.Add("@CARRENT", SqlDbType.Decimal).Value = ccm.CARRENT;
                cmd.Parameters.Add("@ADVAMTP", SqlDbType.Decimal).Value = ccm.ADVAMTP;
                cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = ccm.TOTAMT;
                cmd.Parameters.Add("@ADVAMTC", SqlDbType.Decimal).Value = ccm.ADVAMTC;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ccm.NETAMT;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ccm.REMARKS;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ccm.UserPc;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ccm.InTm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ccm.Ip;
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

        internal string UpdateCommissionInfo(CommissionCreateModel ccm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update GL_COMM set  TRANSDT=@TRANSDT,  PSID= @PSID, SPID =@SPID, COSTPID=@COSTPID, BILLAMT=@BILLAMT, COMPCNT=@COMPCNT,COMMAMT=@COMMAMT,CARRENT=@CARRENT,ADVAMTP=@ADVAMTP,TOTAMT=@TOTAMT,ADVAMTC=@ADVAMTC,NETAMT=@NETAMT,REMARKS=@REMARKS where TRANSMY=@TRANSMY and TRANSNO=@TRANSNO";
                  

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSDT", SqlDbType.SmallDateTime).Value = ccm.TRANSDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ccm.TRANSMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ccm.TRANSNO;
                cmd.Parameters.Add("@PSID", SqlDbType.NVarChar).Value = ccm.PSID;
                cmd.Parameters.Add("@SPID", SqlDbType.NVarChar).Value = ccm.Payable;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ccm.COSTPID;
                cmd.Parameters.Add("@BILLAMT", SqlDbType.Decimal).Value = ccm.BILLAMT;
                cmd.Parameters.Add("@COMPCNT", SqlDbType.Decimal).Value = ccm.COMPCNT;
                cmd.Parameters.Add("@COMMAMT", SqlDbType.Decimal).Value = ccm.COMMAMT;


                cmd.Parameters.Add("@CARRENT", SqlDbType.Decimal).Value = ccm.CARRENT;
                cmd.Parameters.Add("@ADVAMTP", SqlDbType.Decimal).Value = ccm.ADVAMTP;
                cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = ccm.TOTAMT;
                cmd.Parameters.Add("@ADVAMTC", SqlDbType.Decimal).Value = ccm.ADVAMTC;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ccm.NETAMT;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ccm.REMARKS;

                
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

        internal string deleteCommissionInfo(CommissionCreateModel ccm)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "DELETE FROM GL_COMM where TRANSMY=@TRANSMY and TRANSNO=@TRANSNO";


                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ccm.TRANSMY;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ccm.TRANSNO;

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