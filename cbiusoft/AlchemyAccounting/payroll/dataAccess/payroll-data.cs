using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AlchemyAccounting.payroll.model;

namespace AlchemyAccounting.payroll.dataAccess
{
    public class payroll_data
    {
        SqlConnection con;
        SqlCommand cmd;

        public payroll_data()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }

        public string payroll_Employee_Information_HR_EMP(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO HR_EMP (EMPID, EMPNM, FATHERNM, ENDATE, QATARID, IDEXPDT, NATIONALITY, PPNO, PPEXPDT, OCCUPATION, FILENO, COMPANYNM, REFERENCE, VACATIONFR, VACATIONTO, ADDRESS, STATUS, NOTE, CONTACTNO, BASICSAL, FOODS, USERID, USERPC, INTIME, IPADDRSS) " +
                    " VALUES (@EMPID, @EMPNM, @FATHERNM, @ENDATE, @QATARID, @IDEXPDT, @NATIONALITY, @PPNO, @PPEXPDT, @OCCUPATION, @FILENO, @COMPANYNM, @REFERENCE, @VACATIONFR, @VACATIONTO, @ADDRESS, @STATUS, @NOTE, @CONTACTNO, @BASICSAL, @FOODS, @USERID, @USERPC, @INTIME, @IPADDRSS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                cmd.Parameters.Add("@EMPNM", SqlDbType.NVarChar).Value = ob.EmpNM;
                cmd.Parameters.Add("@FATHERNM", SqlDbType.NVarChar).Value = ob.FatherNM;
                cmd.Parameters.Add("@ENDATE", SqlDbType.DateTime).Value = ob.EntryDT;
                cmd.Parameters.Add("@QATARID", SqlDbType.NVarChar).Value = ob.QaterID;
                cmd.Parameters.Add("@IDEXPDT", SqlDbType.DateTime).Value = ob.IdExpDt;
                cmd.Parameters.Add("@NATIONALITY", SqlDbType.NVarChar).Value = ob.Nationality;
                cmd.Parameters.Add("@PPNO", SqlDbType.NVarChar).Value = ob.PpNo;
                cmd.Parameters.Add("@PPEXPDT", SqlDbType.DateTime).Value = ob.PpExpDt;
                cmd.Parameters.Add("@OCCUPATION", SqlDbType.NVarChar).Value = ob.Occupation;
                cmd.Parameters.Add("@FILENO", SqlDbType.NVarChar).Value = ob.FileNo;
                cmd.Parameters.Add("@COMPANYNM", SqlDbType.NVarChar).Value = ob.ComNM;
                cmd.Parameters.Add("@REFERENCE", SqlDbType.NVarChar).Value = ob.Reference;
                cmd.Parameters.Add("@VACATIONFR", SqlDbType.DateTime).Value = ob.VacFr;
                cmd.Parameters.Add("@VACATIONTO", SqlDbType.DateTime).Value = ob.VacTo;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = ob.Address;
                cmd.Parameters.Add("@CONTACTNO", SqlDbType.NVarChar).Value = ob.ContactNo;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@NOTE", SqlDbType.NVarChar).Value = ob.Note;
                cmd.Parameters.Add("@BASICSAL", SqlDbType.Decimal).Value = ob.BasicSal;
                cmd.Parameters.Add("@FOODS", SqlDbType.Decimal).Value = ob.Foods;
                
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string update_payroll_Employee_Information_HR_EMP(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE HR_EMP SET EMPNM =@EMPNM, FATHERNM =@FATHERNM, ENDATE =@ENDATE, QATARID =@QATARID, IDEXPDT =@IDEXPDT, NATIONALITY =@NATIONALITY, PPNO =@PPNO, PPEXPDT =@PPEXPDT, OCCUPATION =@OCCUPATION, FILENO =@FILENO, COMPANYNM =@COMPANYNM, REFERENCE =@REFERENCE, VACATIONFR =@VACATIONFR, VACATIONTO =@VACATIONTO, ADDRESS =@ADDRESS, STATUS =@STATUS, NOTE =@NOTE, CONTACTNO =@CONTACTNO, BASICSAL =@BASICSAL, FOODS =@FOODS, " +
                      " USERID =@USERID, USERPC =@USERPC, IPADDRSS =@IPADDRSS WHERE EMPID =@EMPID";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                cmd.Parameters.Add("@EMPNM", SqlDbType.NVarChar).Value = ob.EmpNM;
                cmd.Parameters.Add("@FATHERNM", SqlDbType.NVarChar).Value = ob.FatherNM;
                cmd.Parameters.Add("@ENDATE", SqlDbType.DateTime).Value = ob.EntryDT;
                cmd.Parameters.Add("@QATARID", SqlDbType.NVarChar).Value = ob.QaterID;
                cmd.Parameters.Add("@IDEXPDT", SqlDbType.DateTime).Value = ob.IdExpDt;
                cmd.Parameters.Add("@NATIONALITY", SqlDbType.NVarChar).Value = ob.Nationality;
                cmd.Parameters.Add("@PPNO", SqlDbType.NVarChar).Value = ob.PpNo;
                cmd.Parameters.Add("@PPEXPDT", SqlDbType.DateTime).Value = ob.PpExpDt;
                cmd.Parameters.Add("@OCCUPATION", SqlDbType.NVarChar).Value = ob.Occupation;
                cmd.Parameters.Add("@FILENO", SqlDbType.NVarChar).Value = ob.FileNo;
                cmd.Parameters.Add("@COMPANYNM", SqlDbType.NVarChar).Value = ob.ComNM;
                cmd.Parameters.Add("@REFERENCE", SqlDbType.NVarChar).Value = ob.Reference;
                cmd.Parameters.Add("@VACATIONFR", SqlDbType.DateTime).Value = ob.VacFr;
                cmd.Parameters.Add("@VACATIONTO", SqlDbType.DateTime).Value = ob.VacTo;
                cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = ob.Address;
                cmd.Parameters.Add("@CONTACTNO", SqlDbType.NVarChar).Value = ob.ContactNo;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.Status;
                cmd.Parameters.Add("@NOTE", SqlDbType.NVarChar).Value = ob.Note;
                cmd.Parameters.Add("@BASICSAL", SqlDbType.Decimal).Value = ob.BasicSal;
                cmd.Parameters.Add("@FOODS", SqlDbType.Decimal).Value = ob.Foods;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string delete_payroll_Employee_Information_HR_EMP(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM HR_EMP WHERE EMPID =@EMPID";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                
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

        public string payroll_Holidays_HR_HOLIDAYS(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO HR_HOLIDAYS (HOLIDAYDT, STATUS, REMARKS, USERID, USERPC, INTIME, IPADDRSS) " +
                    " VALUES (@HOLIDAYDT, @STATUS, @REMARKS, @USERID, @USERPC, @INTIME, @IPADDRSS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@HOLIDAYDT", SqlDbType.DateTime).Value = ob.HolDt;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.HolSt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string update_payroll_Holidays_HR_HOLIDAYS(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE HR_HOLIDAYS SET STATUS =@STATUS, REMARKS =@REMARKS, USERID =@USERID, USERPC =@USERPC, IPADDRSS =@IPADDRSS WHERE HOLIDAYDT =@HOLIDAYDT";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@HOLIDAYDT", SqlDbType.DateTime).Value = ob.HolDt;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.HolSt;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string payroll_Employee_Work_Hour_HR_HOUR(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO HR_HOUR (TRANSDT, TRANSMY, COSTPID, EMPID, TRADE, NORMALHR, NORMALOT, FRIDAYOT, HOLIDAYOT, USERID, USERPC, INTIME, IPADDRSS) " +
                       " VALUES (@TRANSDT, @TRANSMY, @COSTPID, @EMPID, @TRADE, @NORMALHR, @NORMALOT, @FRIDAYOT, @HOLIDAYOT, @USERID, @USERPC, @INTIME, @IPADDRSS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TransDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMY;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.SiteID;
                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                cmd.Parameters.Add("@TRADE", SqlDbType.NVarChar).Value = ob.Trade;
                cmd.Parameters.Add("@NORMALHR", SqlDbType.Decimal).Value = ob.NorHR;
                cmd.Parameters.Add("@NORMALOT", SqlDbType.Decimal).Value = ob.NorOT;
                cmd.Parameters.Add("@FRIDAYOT", SqlDbType.Decimal).Value = ob.FOT;
                cmd.Parameters.Add("@HOLIDAYOT", SqlDbType.Decimal).Value = ob.HOT;
                
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string update_payroll_Employee_Work_Hour_HR_HOUR(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE HR_HOUR SET EMPID =@EMPID, TRADE =@TRADE, NORMALHR =@NORMALHR, NORMALOT =@NORMALOT, FRIDAYOT =@FRIDAYOT, HOLIDAYOT =@HOLIDAYOT, " +
                    " USERID =@USERID, USERPC =@USERPC, IPADDRSS =@IPADDRSS WHERE TRANSDT =@TRANSDT AND TRANSMY =@TRANSMY AND COSTPID =@COSTPID AND SL =@SL";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TransDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMY;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.SiteID;
                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                cmd.Parameters.Add("@TRADE", SqlDbType.NVarChar).Value = ob.Trade;
                cmd.Parameters.Add("@NORMALHR", SqlDbType.Decimal).Value = ob.NorHR;
                cmd.Parameters.Add("@NORMALOT", SqlDbType.Decimal).Value = ob.NorOT;
                cmd.Parameters.Add("@FRIDAYOT", SqlDbType.Decimal).Value = ob.FOT;
                cmd.Parameters.Add("@HOLIDAYOT", SqlDbType.Decimal).Value = ob.HOT;
                cmd.Parameters.Add("@SL", SqlDbType.BigInt).Value = ob.Sl;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string delete_payroll_Employee_Work_Hour_HR_HOUR(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM HR_HOUR WHERE TRANSDT =@TRANSDT AND TRANSMY =@TRANSMY AND COSTPID =@COSTPID AND SL =@SL";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.TransDT;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMY;
                cmd.Parameters.Add("@COSTPID", SqlDbType.NVarChar).Value = ob.SiteID;
                cmd.Parameters.Add("@SL", SqlDbType.BigInt).Value = ob.Sl;

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

        public string payroll_Salary_Info_HR_SALDRCR(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " INSERT INTO HR_SALDRCR (EMPID, TRANSMY, BONUS, OTCADD, ADVANCE, PENALTY, OTCDED, USERID, USERPC, INTIME, IPADDRSS) " +
                    " VALUES (@EMPID, @TRANSMY, @BONUS, @OTCADD, @ADVANCE, @PENALTY, @OTCDED, @USERID, @USERPC, @INTIME, @IPADDRSS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMY;
                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                cmd.Parameters.Add("@BONUS", SqlDbType.Decimal).Value = ob.Bouns;
                cmd.Parameters.Add("@OTCADD", SqlDbType.Decimal).Value = ob.OthAdd;
                cmd.Parameters.Add("@ADVANCE", SqlDbType.Decimal).Value = ob.Advance;
                cmd.Parameters.Add("@PENALTY", SqlDbType.Decimal).Value = ob.Penalty;
                cmd.Parameters.Add("@OTCDED", SqlDbType.Decimal).Value = ob.OthDed;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string update_payroll_Salary_Info_HR_SALDRCR(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " UPDATE HR_SALDRCR SET BONUS =@BONUS, OTCADD =@OTCADD, ADVANCE =@ADVANCE, PENALTY =@PENALTY, OTCDED =@OTCDED, USERID =@USERID, USERPC =@USERPC, IPADDRSS =@IPADDRSS " +
                    " WHERE EMPID =@EMPID AND TRANSMY =@TRANSMY";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMY;
                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                cmd.Parameters.Add("@BONUS", SqlDbType.Decimal).Value = ob.Bouns;
                cmd.Parameters.Add("@OTCADD", SqlDbType.Decimal).Value = ob.OthAdd;
                cmd.Parameters.Add("@ADVANCE", SqlDbType.Decimal).Value = ob.Advance;
                cmd.Parameters.Add("@PENALTY", SqlDbType.Decimal).Value = ob.Penalty;
                cmd.Parameters.Add("@OTCDED", SqlDbType.Decimal).Value = ob.OthDed;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string delete_payroll_Salary_Info_HR_SALDRCR(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " DELETE FROM HR_SALDRCR WHERE EMPID =@EMPID AND TRANSMY =@TRANSMY";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMY;
                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;

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

        public string payroll_Employee_Salary_Process(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO HR_SALGRANT (EMPID, TRANSMY, MMDAYS, NMDAYS, OTDAYS, RATEPD, RATEPH, TOTHOUR, TOTAMT, BASIC, BONUS, FOOD, OTCADD, GROSSAMT, ADVANCE, PENALTY, OTCDED, NETAMT, EFFECTDT, USERID, USERPC, INTIME, IPADDRSS) " +
                    " VALUES (@EMPID, @TRANSMY, @MMDAYS, @NMDAYS, @OTDAYS, @RATEPD, @RATEPH, @TOTHOUR, @TOTAMT, @BASIC, @BONUS, @FOOD, @OTCADD, @GROSSAMT, @ADVANCE, @PENALTY, @OTCDED, @NETAMT, @EFFECTDT, @USERID, @USERPC, @INTIME, @IPADDRSS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TransMY;
                cmd.Parameters.Add("@MMDAYS", SqlDbType.Int).Value = ob.MmDays;
                cmd.Parameters.Add("@NMDAYS", SqlDbType.Int).Value = ob.NmDays;
                cmd.Parameters.Add("@OTDAYS", SqlDbType.Int).Value = ob.OtDays;
                cmd.Parameters.Add("@RATEPD", SqlDbType.Decimal).Value = ob.RatePD;
                cmd.Parameters.Add("@RATEPH", SqlDbType.Decimal).Value = ob.RatePH;
                cmd.Parameters.Add("@TOTHOUR", SqlDbType.BigInt).Value = ob.OtHour;
                cmd.Parameters.Add("@TOTAMT", SqlDbType.Decimal).Value = ob.OtAmt;
                cmd.Parameters.Add("@BASIC", SqlDbType.Decimal).Value = ob.BasicSal;
                cmd.Parameters.Add("@BONUS", SqlDbType.Decimal).Value = ob.Bouns;
                cmd.Parameters.Add("@FOOD", SqlDbType.Decimal).Value = ob.Foods;
                cmd.Parameters.Add("@OTCADD", SqlDbType.Decimal).Value = ob.OtcAdd;
                cmd.Parameters.Add("@GROSSAMT", SqlDbType.Decimal).Value = ob.GrossAmt;
                cmd.Parameters.Add("@ADVANCE", SqlDbType.Decimal).Value = ob.Advance;
                cmd.Parameters.Add("@PENALTY", SqlDbType.Decimal).Value = ob.Penalty;
                cmd.Parameters.Add("@OTCDED", SqlDbType.Decimal).Value = ob.OtcDED;
                cmd.Parameters.Add("@NETAMT", SqlDbType.Decimal).Value = ob.NetAmt;
                cmd.Parameters.Add("@EFFECTDT", SqlDbType.DateTime).Value = ob.TransDT;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string payroll_quotation_master(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO HR_QTMST(TRANSDT, TRANSYY, TRANSNO, QTNO, COMPNM, COMPADDR, COMPCNO, ATNPNM, ATNPDESIG, SUBJECT, PREPBY, PREPDESIG, PRECNO, PREPCOMPNM, USERID, USERPC, INTIME, IPADDRSS) " +
                    " VALUES (@TRANSDT, @TRANSYY, @TRANSNO, @QTNO, @COMPNM, @COMPADDR, @COMPCNO, @ATNPNM, @ATNPDESIG, @SUBJECT, @PREPBY, @PREPDESIG, @PRECNO, @PREPCOMPNM, @USERID, @USERPC, @INTIME, @IPADDRSS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.QDt;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.Year;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TrNo;
                cmd.Parameters.Add("@QTNO", SqlDbType.NVarChar).Value = ob.QuoteNo;
                cmd.Parameters.Add("@COMPNM", SqlDbType.NVarChar).Value = ob.CompNM;
                cmd.Parameters.Add("@COMPADDR", SqlDbType.NVarChar).Value = ob.CompADD;
                cmd.Parameters.Add("@COMPCNO", SqlDbType.NVarChar).Value = ob.CompContact;
                cmd.Parameters.Add("@ATNPNM", SqlDbType.NVarChar).Value = ob.AttnPerNm;
                cmd.Parameters.Add("@ATNPDESIG", SqlDbType.NVarChar).Value = ob.AttPerDesig;
                cmd.Parameters.Add("@SUBJECT", SqlDbType.NVarChar).Value = ob.Subject;
                cmd.Parameters.Add("@PREPBY", SqlDbType.NVarChar).Value = ob.PrepNM;
                cmd.Parameters.Add("@PREPDESIG", SqlDbType.NVarChar).Value = ob.PrepDesig;
                cmd.Parameters.Add("@PRECNO", SqlDbType.NVarChar).Value = ob.PrepContact;
                cmd.Parameters.Add("@PREPCOMPNM", SqlDbType.NVarChar).Value = ob.PrepCompNM;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string payroll_quotation(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO HR_QUOTE(TRANSDT, TRANSYY, TRANSNO, QTTP, QTSL, QTDESC, UNIT, QTRATE, QTQTY, QTQRS, USERID, USERPC, INTIME, IPADDRSS) " +
                    " VALUES (@TRANSDT, @TRANSYY, @TRANSNO, @QTTP, @QTSL, @QTDESC, @UNIT, @QTRATE, @QTQTY, @QTQRS, @USERID, @USERPC, @INTIME, @IPADDRSS)";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.QDt;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.Year;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TrNo;
                cmd.Parameters.Add("@QTTP", SqlDbType.NVarChar).Value = ob.QtTp;
                cmd.Parameters.Add("@QTSL", SqlDbType.BigInt).Value = ob.QSL;
                cmd.Parameters.Add("@QTDESC", SqlDbType.NVarChar).Value = ob.Desc;
                cmd.Parameters.Add("@UNIT", SqlDbType.NVarChar).Value = ob.Unit;
                cmd.Parameters.Add("@QTRATE", SqlDbType.Decimal).Value = ob.QRate;
                cmd.Parameters.Add("@QTQTY", SqlDbType.Decimal).Value = ob.QQty;
                cmd.Parameters.Add("@QTQRS", SqlDbType.Decimal).Value = ob.QTotal;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@INTIME", SqlDbType.DateTime).Value = ob.InTm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string update_payroll_quotation_master(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE HR_QTMST SET TRANSDT =@TRANSDT, QTNO =@QTNO, COMPNM =@COMPNM, COMPADDR =@COMPADDR, COMPCNO =@COMPCNO, ATNPNM =@ATNPNM, ATNPDESIG =@ATNPDESIG, SUBJECT =@SUBJECT, PREPBY =@PREPBY, PREPDESIG =@PREPDESIG, PRECNO =@PRECNO, PREPCOMPNM=@PREPCOMPNM, USERID =@USERID, USERPC =@USERPC, IPADDRSS =@IPADDRSS WHERE TRANSYY =@TRANSYY AND TRANSNO =@TRANSNO";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.QDt;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.Year;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TrNo;
                cmd.Parameters.Add("@QTNO", SqlDbType.NVarChar).Value = ob.QuoteNo;
                cmd.Parameters.Add("@COMPNM", SqlDbType.NVarChar).Value = ob.CompNM;
                cmd.Parameters.Add("@COMPADDR", SqlDbType.NVarChar).Value = ob.CompADD;
                cmd.Parameters.Add("@COMPCNO", SqlDbType.NVarChar).Value = ob.CompContact;
                cmd.Parameters.Add("@ATNPNM", SqlDbType.NVarChar).Value = ob.AttnPerNm;
                cmd.Parameters.Add("@ATNPDESIG", SqlDbType.NVarChar).Value = ob.AttPerDesig;
                cmd.Parameters.Add("@SUBJECT", SqlDbType.NVarChar).Value = ob.Subject;
                cmd.Parameters.Add("@PREPBY", SqlDbType.NVarChar).Value = ob.PrepNM;
                cmd.Parameters.Add("@PREPDESIG", SqlDbType.NVarChar).Value = ob.PrepDesig;
                cmd.Parameters.Add("@PRECNO", SqlDbType.NVarChar).Value = ob.PrepContact;
                cmd.Parameters.Add("@PREPCOMPNM", SqlDbType.NVarChar).Value = ob.PrepCompNM;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string update_payroll_quotation(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE HR_QUOTE SET TRANSDT =@TRANSDT, QTDESC =@QTDESC, UNIT=@UNIT, QTRATE =@QTRATE, QTQTY =@QTQTY, QTQRS =@QTQRS, USERID =@USERID, USERPC =@USERPC, IPADDRSS =@IPADDRSS WHERE TRANSYY =@TRANSYY AND TRANSNO =@TRANSNO AND QTTP =@QTTP AND QTSL =@QTSL";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSDT", SqlDbType.DateTime).Value = ob.QDt;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.Year;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TrNo;
                cmd.Parameters.Add("@QTTP", SqlDbType.NVarChar).Value = ob.QtTp;
                cmd.Parameters.Add("@QTSL", SqlDbType.BigInt).Value = ob.QSL;
                cmd.Parameters.Add("@QTDESC", SqlDbType.NVarChar).Value = ob.Desc;
                cmd.Parameters.Add("@UNIT", SqlDbType.NVarChar).Value = ob.Unit;
                cmd.Parameters.Add("@QTRATE", SqlDbType.Decimal).Value = ob.QRate;
                cmd.Parameters.Add("@QTQTY", SqlDbType.Decimal).Value = ob.QQty;
                cmd.Parameters.Add("@QTQRS", SqlDbType.Decimal).Value = ob.QTotal;

                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.UserPc;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserNm;
                cmd.Parameters.Add("@IPADDRSS", SqlDbType.NVarChar).Value = ob.Ip;

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

        public string delete_payroll_quotation(payroll_model ob)
        {
            string s = "";
            SqlTransaction tran = null;


            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                tran = con.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM HR_QUOTE WHERE TRANSYY =@TRANSYY AND TRANSNO =@TRANSNO AND QTTP =@QTTP AND QTSL =@QTSL";
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.Year;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.BigInt).Value = ob.TrNo;
                cmd.Parameters.Add("@QTTP", SqlDbType.NVarChar).Value = ob.QtTp;
                cmd.Parameters.Add("@QTSL", SqlDbType.BigInt).Value = ob.QSL;

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