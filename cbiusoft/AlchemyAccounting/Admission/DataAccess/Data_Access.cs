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
using System.Data.SqlClient;
using AlchemyAccounting.Admission.DataModel;

namespace AlchemyAccounting.Admission.DataAccess
{
    public class Data_Access
    {
        SqlConnection con;
        SqlCommand cmd;
        public Data_Access()
        {
            con = new SqlConnection(Global.connection);
            cmd = new SqlCommand("", con);
        }
        public string InsertSemester(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["InsertSemester"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO EIM_SEMESTER(SEMESTERID,SEMESTERNM,STARTMM,REMARKS,USERID,USERPC,IPADDRESS,INTIME) Values" +
                    "(@SEMESTERID,@SEMESTERNM,@STARTMM,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@SEMESTERNM", SqlDbType.NVarChar).Value = ob.SemNM;
                cmd.Parameters.Add("@STARTMM", SqlDbType.NVarChar).Value = ob.StrtTime;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;

                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["InsertSemester"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }



        public string UpdateSemester(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["UpdateSemester"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE EIM_SEMESTER SET SEMESTERID=@SEMESTERID,SEMESTERNM=@SEMESTERNM,STARTMM=@STARTMM,
                             REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME where SEMESTERID=@SEMESTERID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@SEMESTERNM", SqlDbType.NVarChar).Value = ob.SemNM;
                cmd.Parameters.Add("@STARTMM", SqlDbType.NVarChar).Value = ob.StrtTime;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["UpdateSemester"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string InsertProgram(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["InsertProgram"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO EIM_PROGRAM(PROGRAMTP,PROGRAMID,PROGRAMNM,PROGRAMSID,TOTCREDIT,COSTPERCR,DURATION,TOTFEES,
                               REMARKS,USERID,USERPC,IPADDRESS,INTIME) Values
                    (@PROGRAMTP,@PROGRAMID,@PROGRAMNM,@PROGRAMSID,@TOTCREDIT,@COSTPERCR,@DURATION,@TOTFEES,
                               @REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PROGRAMTP", SqlDbType.NVarChar).Value = ob.ProgTP;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@PROGRAMNM", SqlDbType.NVarChar).Value = ob.ProgNM;

                cmd.Parameters.Add("@PROGRAMSID", SqlDbType.NVarChar).Value = ob.ProgSrtNM;
                cmd.Parameters.Add("@TOTCREDIT", SqlDbType.NVarChar).Value = ob.TotlCrdt;
                cmd.Parameters.Add("@COSTPERCR", SqlDbType.Decimal).Value = ob.CstPerCrdt;

                cmd.Parameters.Add("@DURATION", SqlDbType.NVarChar).Value = ob.Dura;
                cmd.Parameters.Add("@TOTFEES", SqlDbType.Decimal).Value = ob.TotlAmnt;

                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["InsertProgram"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string UpdateProgram(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["UpdateProgram"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE EIM_PROGRAM SET PROGRAMTP=@PROGRAMTP,PROGRAMID=@PROGRAMID,PROGRAMNM=@PROGRAMNM,PROGRAMSID=@PROGRAMSID,TOTCREDIT=@TOTCREDIT,
              COSTPERCR=@COSTPERCR,DURATION=@DURATION,TOTFEES=@TOTFEES,REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE PROGRAMID=@PROGRAMID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PROGRAMTP", SqlDbType.NVarChar).Value = ob.ProgTP;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@PROGRAMNM", SqlDbType.NVarChar).Value = ob.ProgNM;

                cmd.Parameters.Add("@PROGRAMSID", SqlDbType.NVarChar).Value = ob.ProgSrtNM;
                cmd.Parameters.Add("@TOTCREDIT", SqlDbType.NVarChar).Value = ob.TotlCrdt;
                cmd.Parameters.Add("@COSTPERCR", SqlDbType.Decimal).Value = ob.CstPerCrdt;

                cmd.Parameters.Add("@DURATION", SqlDbType.NVarChar).Value = ob.Dura;
                cmd.Parameters.Add("@TOTFEES", SqlDbType.Decimal).Value = ob.TotlAmnt;

                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["UpdateProgram"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string InsertAdmissionTest(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["InsertAdmissionTest"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO EIM_ADMTEST(TESTYY,SEMESTERID,PROGRAMTP,PROGRAMID,TESTDT,VENUE,TESTTM,REMARKS,
                               USERID,USERPC,IPADDRESS,INTIME) Values
                    (@TESTYY,@SEMESTERID,@PROGRAMTP,@PROGRAMID,@TESTDT,@VENUE,@TESTTM,@REMARKS,
                               @USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TESTYY", SqlDbType.NVarChar).Value = ob.ExamYr;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@PROGRAMTP", SqlDbType.NVarChar).Value = ob.ProgTP;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@TESTDT", SqlDbType.Date).Value = ob.ExamDT;
                cmd.Parameters.Add("@VENUE", SqlDbType.NVarChar).Value = ob.ExamVenu;

                cmd.Parameters.Add("@TESTTM", SqlDbType.NVarChar).Value = ob.ExamTM;


                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["InsertAdmissionTest"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string UpdateAdmissionTest(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["UpdateAdmissionTest"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE EIM_ADMTEST SET TESTYY=@TESTYY,SEMESTERID=@SEMESTERID,PROGRAMTP=@PROGRAMTP,PROGRAMID=@PROGRAMID,TESTDT=@TESTDT,
              VENUE=@VENUE,TESTTM=@TESTTM,REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE SEMESTERID=@SEMESTERID and TESTYY=@TESTYY and PROGRAMID=@PROGRAMID ";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TESTYY", SqlDbType.NVarChar).Value = ob.ExamYr;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@PROGRAMTP", SqlDbType.NVarChar).Value = ob.ProgTP;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@TESTDT", SqlDbType.Date).Value = ob.ExamDT;
                cmd.Parameters.Add("@VENUE", SqlDbType.NVarChar).Value = ob.ExamVenu;
                cmd.Parameters.Add("@TESTTM", SqlDbType.NVarChar).Value = ob.ExamTM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["UpdateAdmissionTest"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string InsertAdmission(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["InsertAdmission"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO EIM_ADMISSION(TESTYY,IMAGE,FORMNO,SEMESTERID,PROGRAMID,TESTDT,ROLLNO,STUDENTNM,FATHERNM,MOTHERNM,ADDRPRE,ADDRPER,
                                                        MOBNO,EMAIL,NATIONALITY,RELIGION,DOB,GENDER,GUARDIANNM,GRELATION,GPROFESSION,GADDRESS,GMOBNO,GEMAIL,MRDT,MRYY,MRNO,MRAMT,
                               USERID,USERPC,IPADDRESS,INTIME) Values
                    (@TESTYY,@IMAGE,@FORMNO,@SEMESTERID,@PROGRAMID,@TESTDT,@ROLLNO,@STUDENTNM,@FATHERNM,@MOTHERNM,@ADDRPRE,@ADDRPER,
                                                        @MOBNO,@EMAIL,@NATIONALITY,@RELIGION,@DOB,@GENDER,@GUARDIANNM,@GRELATION,@GPROFESSION,@GADDRESS,@GMOBNO,@GEMAIL,@MRDT,@MRYY,@MRNO,@MRAMT,
                               @USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TESTYY", SqlDbType.NVarChar).Value = ob.ExamYr;
                cmd.Parameters.Add("@IMAGE", SqlDbType.NVarChar).Value = ob.Img;
                cmd.Parameters.Add("@FORMNO", SqlDbType.Int).Value = ob.FormNO;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@TESTDT", SqlDbType.SmallDateTime).Value = ob.ExamDT;
                cmd.Parameters.Add("@ROLLNO", SqlDbType.Int).Value = ob.Roll;
                cmd.Parameters.Add("@STUDENTNM", SqlDbType.NVarChar).Value = ob.StuNM;
                cmd.Parameters.Add("@FATHERNM", SqlDbType.NVarChar).Value = ob.StuFNM;

                cmd.Parameters.Add("@MOTHERNM", SqlDbType.NVarChar).Value = ob.StuMNM;
                cmd.Parameters.Add("@ADDRPRE", SqlDbType.NVarChar).Value = ob.PreAdrs;
                cmd.Parameters.Add("@ADDRPER", SqlDbType.NVarChar).Value = ob.PerAdrs;
                cmd.Parameters.Add("@MOBNO", SqlDbType.NVarChar).Value = ob.MobNO;
                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@NATIONALITY", SqlDbType.NVarChar).Value = ob.Nation;
                cmd.Parameters.Add("@RELIGION", SqlDbType.NVarChar).Value = ob.Religion;
                cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = ob.DtOfBrt;

                cmd.Parameters.Add("@GENDER", SqlDbType.NVarChar).Value = ob.Gander;
                cmd.Parameters.Add("@GUARDIANNM", SqlDbType.NVarChar).Value = ob.GurNM;
                cmd.Parameters.Add("@GRELATION", SqlDbType.NVarChar).Value = ob.GRel;
                cmd.Parameters.Add("@GPROFESSION", SqlDbType.NVarChar).Value = ob.GProf;
                cmd.Parameters.Add("@GADDRESS", SqlDbType.NVarChar).Value = ob.GAdrs;
                cmd.Parameters.Add("@GMOBNO", SqlDbType.NVarChar).Value = ob.GMNo;
                cmd.Parameters.Add("@GEMAIL", SqlDbType.NVarChar).Value = ob.GEml;
                cmd.Parameters.Add("@MRDT", SqlDbType.NVarChar).Value = ob.MrDT;
                cmd.Parameters.Add("@MRYY", SqlDbType.NVarChar).Value = ob.MrYr;
                cmd.Parameters.Add("@MRNO", SqlDbType.BigInt).Value = ob.MrNo;
                cmd.Parameters.Add("@MRAMT", SqlDbType.Decimal).Value = ob.TotlAmnt;

                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["InsertAdmission"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string InsertApplicationReg(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["InsertApplicationReg"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO EIM_STUDENT(MIGRATEDT,MIGRATEPID,MIGRATESID,NEWMIGRATESID,ADMITYY,IMAGE,ADMITSL,ADMITDT,ADMITTP,SEMESTERID,PROGRAMID,SESSION,BATCH,STUDENTID,NEWSTUDENTID,STUDENTNM, FATHERNM,
                                                        FATHEROCP,FOCPDTL,MOTHERNM,MOTHEROCP,SPOUSENM,SPOUSEOCP,ADDRPRE,ADDRPER,TELNO,MOBNO,EMAIL,NATIONALITY,
                       RELIGION,DOB,GENDER,STUDENTTP,BIRTHP,NIDPNO,BLOODGR,PRESIDENCE,MSTATUS,RESHOSTEL,INCOMEYY,EXPENSEYY,GUARDIANNM,GRELATION,GADDRESS,
                 GTELNO,GMOBNO,GEMAIL,PREPROGTP,PREPROGNM,PPINSTITN,PPSESSION,FIRMNM,POSITION,REMARKS,
                               USERID,USERPC,IPADDRESS,INTIME) Values
                    (@MIGRATEDT,@MIGRATEPID,@MIGRATESID,@NEWMIGRATESID,@ADMITYY,@IMAGE,@ADMITSL,@ADMITDT,@ADMITTP,@SEMESTERID,@PROGRAMID,@SESSION,@BATCH,@STUDENTID,@NEWSTUDENTID,@STUDENTNM,@FATHERNM,
                                                        @FATHEROCP,@FOCPDTL,@MOTHERNM,@MOTHEROCP,@SPOUSENM,@SPOUSEOCP,@ADDRPRE,@ADDRPER,@TELNO,@MOBNO,@EMAIL,@NATIONALITY,
                       @RELIGION,@DOB,@GENDER,@STUDENTTP,@BIRTHP,@NIDPNO,@BLOODGR,@PRESIDENCE,@MSTATUS,@RESHOSTEL,@INCOMEYY,@EXPENSEYY,@GUARDIANNM,@GRELATION,@GADDRESS,
                @GTELNO,@GMOBNO,@GEMAIL,@PREPROGTP,@PREPROGNM,@PPINSTITN,@PPSESSION,@FIRMNM,@POSITION,@REMARKS,
                               @USERID,@USERPC,@IPADDRESS,@INTIME)";


                cmd.Parameters.Clear(); 
                cmd.Parameters.Add("@MIGRATEDT", SqlDbType.NVarChar).Value = ob.MigratDT;
                cmd.Parameters.Add("@MIGRATEPID", SqlDbType.NVarChar).Value = ob.ProgIDMigrate;
                cmd.Parameters.Add("@MIGRATESID", SqlDbType.NVarChar).Value = ob.StuIDMigrate;
                cmd.Parameters.Add("@NEWMIGRATESID", SqlDbType.NVarChar).Value = ob.StuIDMigrateNew;
                cmd.Parameters.Add("@ADMITYY", SqlDbType.NVarChar).Value = ob.AdmtYR;
                cmd.Parameters.Add("@IMAGE", SqlDbType.NVarChar).Value = ob.Img;
                cmd.Parameters.Add("@ADMITSL", SqlDbType.Int).Value = ob.AdmtSL;
                cmd.Parameters.Add("@ADMITDT", SqlDbType.Date).Value = ob.ADMITDT;
                cmd.Parameters.Add("@ADMITTP", SqlDbType.NVarChar).Value = ob.ADMITTP;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@SESSION", SqlDbType.NVarChar).Value = ob.SeSN;
                cmd.Parameters.Add("@BATCH", SqlDbType.NVarChar).Value = ob.Batch;
                cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                cmd.Parameters.Add("@NEWSTUDENTID", SqlDbType.NVarChar).Value = ob.StuIDNew;
                cmd.Parameters.Add("@STUDENTNM", SqlDbType.NVarChar).Value = ob.StuNM; 
                cmd.Parameters.Add("@FATHERNM", SqlDbType.NVarChar).Value = ob.StuFNM;
                cmd.Parameters.Add("@FATHEROCP", SqlDbType.NVarChar).Value = ob.FOcup;
                cmd.Parameters.Add("@FOCPDTL", SqlDbType.NVarChar).Value = ob.FOcupDTL;
                cmd.Parameters.Add("@MOTHERNM", SqlDbType.NVarChar).Value = ob.StuMNM;
                cmd.Parameters.Add("@MOTHEROCP", SqlDbType.NVarChar).Value = ob.MOcup;
                cmd.Parameters.Add("@SPOUSENM", SqlDbType.NVarChar).Value = ob.SPuseNM;
                cmd.Parameters.Add("@SPOUSEOCP", SqlDbType.NVarChar).Value = ob.SpuseOcup;
                cmd.Parameters.Add("@ADDRPRE", SqlDbType.NVarChar).Value = ob.PreAdrs;
                cmd.Parameters.Add("@ADDRPER", SqlDbType.NVarChar).Value = ob.PerAdrs;
                cmd.Parameters.Add("@TELNO", SqlDbType.NVarChar).Value = ob.TelePhn;
                cmd.Parameters.Add("@MOBNO", SqlDbType.NVarChar).Value = ob.MobNO;
                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@NATIONALITY", SqlDbType.NVarChar).Value = ob.Nation;
                cmd.Parameters.Add("@RELIGION", SqlDbType.NVarChar).Value = ob.Religion;
                cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = ob.DtOfBrt;
                cmd.Parameters.Add("@GENDER", SqlDbType.NVarChar).Value = ob.Gander;
                cmd.Parameters.Add("@STUDENTTP", SqlDbType.NVarChar).Value = ob.StuTP;
                cmd.Parameters.Add("@BIRTHP", SqlDbType.NVarChar).Value = ob.PofB;
                cmd.Parameters.Add("@NIDPNO ", SqlDbType.NVarChar).Value = ob.NIDPNO;
                cmd.Parameters.Add("@BLOODGR", SqlDbType.NVarChar).Value = ob.BldGRP;

                cmd.Parameters.Add("@PRESIDENCE", SqlDbType.NVarChar).Value = ob.PRecdnc;
                cmd.Parameters.Add("@MSTATUS", SqlDbType.NVarChar).Value = ob.MSTTS;
                cmd.Parameters.Add("@RESHOSTEL", SqlDbType.NVarChar).Value = ob.Hstl;
                cmd.Parameters.Add("@INCOMEYY", SqlDbType.NVarChar).Value = ob.Incm;
                cmd.Parameters.Add("@EXPENSEYY", SqlDbType.NVarChar).Value = ob.Expncy;
                cmd.Parameters.Add("@GUARDIANNM", SqlDbType.NVarChar).Value = ob.GNM;
                cmd.Parameters.Add("@GRELATION", SqlDbType.NVarChar).Value = ob.GRel;
                cmd.Parameters.Add("@GADDRESS", SqlDbType.NVarChar).Value = ob.GAdrs;
                cmd.Parameters.Add("@GTELNO", SqlDbType.NVarChar).Value = ob.GTelePhn;
                cmd.Parameters.Add("@GMOBNO", SqlDbType.NVarChar).Value = ob.GMNo;
                cmd.Parameters.Add("@GEMAIL", SqlDbType.NVarChar).Value = ob.GEml;
                cmd.Parameters.Add("@PREPROGTP", SqlDbType.NVarChar).Value = ob.PreProTP;
                cmd.Parameters.Add("@PREPROGNM", SqlDbType.NVarChar).Value = ob.PreProNM;
                cmd.Parameters.Add("@PPINSTITN", SqlDbType.NVarChar).Value = ob.PreInsNM;
                cmd.Parameters.Add("@PPSESSION", SqlDbType.NVarChar).Value = ob.PreSSN;
                cmd.Parameters.Add("@FIRMNM", SqlDbType.NVarChar).Value = ob.FIRMNM;
                cmd.Parameters.Add("@POSITION", SqlDbType.NVarChar).Value = ob.PosiSN;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.PosiSN;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["InsertApplicationReg"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string UpdateApplicationReg(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["UpdateApplicationReg"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE EIM_STUDENT SET MIGRATEDT=@MIGRATEDT,MIGRATEPID=@MIGRATEPID,MIGRATESID=@MIGRATESID,NEWMIGRATESID=@NEWMIGRATESID,IMAGE=@IMAGE,ADMITDT=@ADMITDT,ADMITTP=@ADMITTP,SEMESTERID=@SEMESTERID,SESSION=@SESSION, STUDENTNM=@STUDENTNM, FATHERNM=@FATHERNM,
                                                        FATHEROCP=@FATHEROCP,FOCPDTL=@FOCPDTL,MOTHERNM=@MOTHERNM,MOTHEROCP=@MOTHEROCP,SPOUSENM=@SPOUSENM,SPOUSEOCP=@SPOUSEOCP,ADDRPRE=@ADDRPRE,ADDRPER=@ADDRPER,TELNO=@TELNO,MOBNO=@MOBNO,EMAIL=@EMAIL,NATIONALITY=@NATIONALITY,
                       RELIGION=@RELIGION,DOB=@DOB,GENDER=@GENDER,STUDENTTP=@STUDENTTP,BIRTHP=@BIRTHP,NIDPNO=@NIDPNO,BLOODGR=@BLOODGR,PRESIDENCE=@PRESIDENCE,MSTATUS=@MSTATUS,RESHOSTEL=@RESHOSTEL,INCOMEYY=@INCOMEYY,EXPENSEYY=@EXPENSEYY,GUARDIANNM=@GUARDIANNM,GRELATION=@GRELATION,GADDRESS=@GADDRESS,
                 GTELNO=@GTELNO,GMOBNO=@GMOBNO,GEMAIL=@GEMAIL,PREPROGTP=@PREPROGTP,PREPROGNM=@PREPROGNM,PPINSTITN=@PPINSTITN,PPSESSION=@PPSESSION,FIRMNM=@FIRMNM,POSITION=@POSITION,REMARKS=@REMARKS,
                               UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME where NEWSTUDENTID=@NEWSTUDENTID";


                cmd.Parameters.Clear();
                cmd.Parameters.Add("@MIGRATEDT", SqlDbType.NVarChar).Value = ob.MigratDT;
                cmd.Parameters.Add("@MIGRATEPID", SqlDbType.NVarChar).Value = ob.ProgIDMigrate;
                cmd.Parameters.Add("@MIGRATESID", SqlDbType.NVarChar).Value = ob.StuIDMigrate;
                cmd.Parameters.Add("@NEWMIGRATESID", SqlDbType.NVarChar).Value = ob.StuIDMigrateNew;
                cmd.Parameters.Add("@ADMITYY", SqlDbType.NVarChar).Value = ob.AdmtYR;
                cmd.Parameters.Add("@IMAGE", SqlDbType.NVarChar).Value = ob.Img;
                //cmd.Parameters.Add("@ADMITSL", SqlDbType.Int).Value = ob.AdmtSL;
                cmd.Parameters.Add("@ADMITDT", SqlDbType.Date).Value = ob.ADMITDT;
                cmd.Parameters.Add("@ADMITTP", SqlDbType.NVarChar).Value = ob.ADMITTP;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                // cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@SESSION", SqlDbType.NVarChar).Value = ob.SeSN;
                cmd.Parameters.Add("@BATCH", SqlDbType.NVarChar).Value = ob.Batch;
                cmd.Parameters.Add("@NEWSTUDENTID", SqlDbType.NVarChar).Value = ob.StuIDNew;
                cmd.Parameters.Add("@STUDENTNM", SqlDbType.NVarChar).Value = ob.StuNM;
                cmd.Parameters.Add("@FATHERNM", SqlDbType.NVarChar).Value = ob.StuFNM;
                cmd.Parameters.Add("@FATHEROCP", SqlDbType.NVarChar).Value = ob.FOcup;
                cmd.Parameters.Add("@FOCPDTL", SqlDbType.NVarChar).Value = ob.FOcupDTL;
                cmd.Parameters.Add("@MOTHERNM", SqlDbType.NVarChar).Value = ob.StuMNM;
                cmd.Parameters.Add("@MOTHEROCP", SqlDbType.NVarChar).Value = ob.MOcup;
                cmd.Parameters.Add("@SPOUSENM", SqlDbType.NVarChar).Value = ob.SPuseNM; 
                cmd.Parameters.Add("@SPOUSEOCP", SqlDbType.NVarChar).Value = ob.SpuseOcup;
                cmd.Parameters.Add("@ADDRPRE", SqlDbType.NVarChar).Value = ob.PreAdrs;
                cmd.Parameters.Add("@ADDRPER", SqlDbType.NVarChar).Value = ob.PerAdrs;
                cmd.Parameters.Add("@TELNO", SqlDbType.NVarChar).Value = ob.TelePhn;
                cmd.Parameters.Add("@MOBNO", SqlDbType.NVarChar).Value = ob.MobNO;
                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@NATIONALITY", SqlDbType.NVarChar).Value = ob.Nation;
                cmd.Parameters.Add("@RELIGION", SqlDbType.NVarChar).Value = ob.Religion;
                cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = ob.DtOfBrt;
                cmd.Parameters.Add("@GENDER", SqlDbType.NVarChar).Value = ob.Gander;
                cmd.Parameters.Add("@STUDENTTP", SqlDbType.NVarChar).Value = ob.StuTP;
                cmd.Parameters.Add("@BIRTHP", SqlDbType.NVarChar).Value = ob.PofB;
                cmd.Parameters.Add("@NIDPNO ", SqlDbType.NVarChar).Value = ob.NIDPNO;
                cmd.Parameters.Add("@BLOODGR", SqlDbType.NVarChar).Value = ob.BldGRP;

                cmd.Parameters.Add("@PRESIDENCE", SqlDbType.NVarChar).Value = ob.PRecdnc;
                cmd.Parameters.Add("@MSTATUS", SqlDbType.NVarChar).Value = ob.MSTTS;
                cmd.Parameters.Add("@RESHOSTEL", SqlDbType.NVarChar).Value = ob.Hstl;
                cmd.Parameters.Add("@INCOMEYY", SqlDbType.NVarChar).Value = ob.Incm;
                cmd.Parameters.Add("@EXPENSEYY", SqlDbType.NVarChar).Value = ob.Expncy;
                cmd.Parameters.Add("@GUARDIANNM", SqlDbType.NVarChar).Value = ob.GNM;
                cmd.Parameters.Add("@GRELATION", SqlDbType.NVarChar).Value = ob.GRel;
                cmd.Parameters.Add("@GADDRESS", SqlDbType.NVarChar).Value = ob.GAdrs;
                cmd.Parameters.Add("@GTELNO", SqlDbType.NVarChar).Value = ob.GTelePhn;
                cmd.Parameters.Add("@GMOBNO", SqlDbType.NVarChar).Value = ob.GMNo;
                cmd.Parameters.Add("@GEMAIL", SqlDbType.NVarChar).Value = ob.GEml;
                cmd.Parameters.Add("@PREPROGTP", SqlDbType.NVarChar).Value = ob.PreProTP;
                cmd.Parameters.Add("@PREPROGNM", SqlDbType.NVarChar).Value = ob.PreProNM;
                cmd.Parameters.Add("@PPINSTITN", SqlDbType.NVarChar).Value = ob.PreInsNM;
                cmd.Parameters.Add("@PPSESSION", SqlDbType.NVarChar).Value = ob.PreSSN;
                cmd.Parameters.Add("@FIRMNM", SqlDbType.NVarChar).Value = ob.FIRMNM;
                cmd.Parameters.Add("@POSITION", SqlDbType.NVarChar).Value = ob.PosiSN;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.PosiSN;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["UpdateApplicationReg"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string InsertCourse(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["InsertCourse"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO EIM_COURSE(PROGRAMID,COURSEID,COURSECD,COURSENM,CREDITHH,SEMID,SEMSL,REMARKS,USERID,USERPC,IPADDRESS,INTIME) Values
                    (@PROGRAMID,@COURSEID,@COURSECD,@COURSENM,@CREDITHH,@SEMID,@SEMSL,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@COURSEID", SqlDbType.NVarChar).Value = ob.CrsID;
                cmd.Parameters.Add("@COURSECD", SqlDbType.NVarChar).Value = ob.CrsCD;
                cmd.Parameters.Add("@COURSENM", SqlDbType.NVarChar).Value = ob.CrsNM;
                cmd.Parameters.Add("@CREDITHH", SqlDbType.Decimal).Value = ob.TotlCrdt;
                cmd.Parameters.Add("@SEMID", SqlDbType.NVarChar).Value = ob.SemisterID;
                cmd.Parameters.Add("@SEMSL", SqlDbType.NVarChar).Value = ob.SemSL;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["InsertCourse"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string UpdateCourse(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["UpdateCourse"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE EIM_COURSE SET PROGRAMID=@PROGRAMID,COURSEID=@COURSEID,COURSECD=@COURSECD,COURSENM=@COURSENM,CREDITHH=@CREDITHH,
                             SEMID=@SEMID,SEMSL=@SEMSL,REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME where COURSEID=@COURSEID ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@COURSEID", SqlDbType.NVarChar).Value = ob.CrsID;
                cmd.Parameters.Add("@COURSECD", SqlDbType.NVarChar).Value = ob.CrsCD;
                cmd.Parameters.Add("@COURSENM", SqlDbType.NVarChar).Value = ob.CrsNM;
                cmd.Parameters.Add("@CREDITHH", SqlDbType.Decimal).Value = ob.TotlCrdt;
                cmd.Parameters.Add("@SEMID", SqlDbType.NVarChar).Value = ob.SemisterID;
                cmd.Parameters.Add("@SEMSL", SqlDbType.NVarChar).Value = ob.SemSL;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["UpdateCourse"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_EIM_CREGMST(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["Insert_EIM_CREGMST"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO EIM_CREGMST(REGYY,SEMESTERID,PROGRAMID,STUDENTID,ENRLDT,BATCH,SESSION,REMARKS,USERID,USERPC,IPADDRESS,INTIME) Values
                    (@REGYY,@SEMESTERID,@PROGRAMID,@STUDENTID,@ENRLDT,@BATCH,@SESSION,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.CrsYR;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                cmd.Parameters.Add("@ENRLDT", SqlDbType.Date).Value = ob.EnRLDT;
                cmd.Parameters.Add("@BATCH", SqlDbType.NVarChar).Value = ob.Batch;
                cmd.Parameters.Add("@SESSION", SqlDbType.NVarChar).Value = ob.SeSN;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["Insert_EIM_CREGMST"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string Update_EIM_CREGMST(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["Update_EIM_CREGMST"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE EIM_CREGMST SET ENRLDT=@ENRLDT,BATCH=@BATCH,
                        SESSION=@SESSION,REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE STUDENTID=@STUDENTID AND REGYY=@REGYY AND SEMESTERID=@SEMESTERID AND PROGRAMID=@PROGRAMID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.CrsYR;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                cmd.Parameters.Add("@ENRLDT", SqlDbType.Date).Value = ob.EnRLDT;
                cmd.Parameters.Add("@BATCH", SqlDbType.NVarChar).Value = ob.Batch;
                cmd.Parameters.Add("@SESSION", SqlDbType.NVarChar).Value = ob.SeSN;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["Update_EIM_CREGMST"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string Update_EIM_COURSEREG(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["Update_EIM_COURSEREG"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE EIM_COURSEREG SET ENRLDT=@ENRLDT WHERE STUDENTID=@STUDENTID AND REGYY=@REGYY AND SEMESTERID=@SEMESTERID AND PROGRAMID=@PROGRAMID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.CrsYR;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                cmd.Parameters.Add("@ENRLDT", SqlDbType.Date).Value = ob.EnRLDT;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["Update_EIM_COURSEREG"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string Insert_EIM_FEES(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["Insert_EIM_FEES"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO EIM_FEES(FEESID,FEESNM,FEESRT,REMARKS,USERID,USERPC,IPADDRESS,INTIME) Values
                    (@FEESID,@FEESNM,@FEESRT,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@FEESID", SqlDbType.NVarChar).Value = ob.FeesID;
                cmd.Parameters.Add("@FEESNM", SqlDbType.NVarChar).Value = ob.FeesNM;
                cmd.Parameters.Add("@FEESRT", SqlDbType.NVarChar).Value = ob.FeesRT;

                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["Insert_EIM_FEES"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_EIM_FEES(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["Update_EIM_FEES"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE EIM_FEES SET FEESID=@FEESID,FEESNM=@FEESNM,FEESRT=@FEESRT,REMARKS=@REMARKS,
                       UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME 
                       where FEESID=@FEESID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@FEESID", SqlDbType.NVarChar).Value = ob.FeesID;
                cmd.Parameters.Add("@FEESNM", SqlDbType.NVarChar).Value = ob.FeesNM;
                cmd.Parameters.Add("@FEESRT", SqlDbType.NVarChar).Value = ob.FeesRT;

                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["Update_EIM_FEES"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_EIM_TRANSMST(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["Insert_EIM_TRANSMST"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO EIM_TRANSMST(TRANSFOR,TRANSDT,TRANSTP,TRANSYY,TRANSNO,REGYY,SEMESTERID,PROGRAMID,STUDENTID,CNBCD,
                 PONO,PODT,POBANK,POBANKBR,REMARKS,USERID,USERPC,IPADDRESS,INTIME) Values
                    (@TRANSFOR,@TRANSDT,@TRANSTP,@TRANSYY,@TRANSNO,@REGYY,@SEMESTERID,@PROGRAMID,@STUDENTID,@CNBCD,
                 @PONO,@PODT,@POBANK,@POBANKBR,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value ="COLLECTION";
                cmd.Parameters.Add("@TRANSDT", SqlDbType.Date).Value = ob.TrnsDT;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYR;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.Int).Value = ob.TrnsNO;
                cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.RegYR;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                cmd.Parameters.Add("@CNBCD", SqlDbType.NVarChar).Value = ob.AccNo;
                cmd.Parameters.Add("@PONO", SqlDbType.NVarChar).Value = ob.PONO;
                cmd.Parameters.Add("@PODT", SqlDbType.Date).Value = ob.PODT;
                cmd.Parameters.Add("@POBANK", SqlDbType.NVarChar).Value = ob.POBNK;
                cmd.Parameters.Add("@POBANKBR", SqlDbType.NVarChar).Value = ob.POBRNC;

                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["Insert_EIM_TRANSMST"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_EIM_TRANS(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Insert_EIM_TRANS"] = "";
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO EIM_TRANS(TRANSDT,TRANSTP,TRANSYY,TRANSNO,REGYY,SEMESTERID,PROGRAMID,STUDENTID,CNBCD,
                 FEESID,AMOUNT, REMARKS,USERID,USERPC,IPADDRESS,INTIME) Values
                    (@TRANSDT,@TRANSTP,@TRANSYY,@TRANSNO,@REGYY,@SEMESTERID,@PROGRAMID,@STUDENTID,@CNBCD,
                 @FEESID,@AMOUNT ,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                    cmd.Parameters.Clear(); 
                    cmd.Parameters.Add("@TRANSDT", SqlDbType.Date).Value = ob.TrnsDT;
                    cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = ob.TransTP;
                    cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYR;
                    cmd.Parameters.Add("@TRANSNO", SqlDbType.Int).Value = ob.TrnsNO;
                    cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.RegYR;
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                    cmd.Parameters.Add("@CNBCD", SqlDbType.NVarChar).Value = ob.AccNo;
                    cmd.Parameters.Add("@FEESID", SqlDbType.NVarChar).Value = ob.FeesID;
                    cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amnt; 
                    cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksGRD;
                    cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                    cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                    cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                    cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Insert_EIM_TRANS"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;
            }
        }

        public string Update_EIM_TRANS(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Update_EIM_TRANS"] = "";
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    string FEESID = HttpContext.Current.Session["FEESID"].ToString();
                    cmd.CommandText = @"UPDATE EIM_TRANS SET SEMESTERID=@SEMESTERID,PROGRAMID=@PROGRAMID,STUDENTID=@STUDENTID,
                    FEESID=@FEESID,CNBCD=@CNBCD,AMOUNT=@AMOUNT, REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME 
                    WHERE TRANSYY=@TRANSYY AND TRANSNO=@TRANSNO AND TRANSTP='MREC' AND FEESID='" + FEESID + "'";

                    cmd.Parameters.Clear();
                     
                    cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYR;
                    cmd.Parameters.Add("@TRANSNO", SqlDbType.Int).Value = ob.TrnsNO;
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                    // cmd.Parameters.Add("@FEESSL", SqlDbType.NVarChar).Value = ob.FeesSL;
                    cmd.Parameters.Add("@FEESID", SqlDbType.NVarChar).Value = ob.FeesID;
                    cmd.Parameters.Add("@CNBCD", SqlDbType.NVarChar).Value = ob.AccNo;
                    cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amnt; 

                    cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksGRD;
                    cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                    cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                    cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                    cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Update_EIM_TRANS"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;
            }
        }

        public string Update_EIM_TRANSMST(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Update_EIM_TRANSMST"] = "";
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = @"UPDATE EIM_TRANSMST SET SEMESTERID=@SEMESTERID,PROGRAMID=@PROGRAMID,STUDENTID=@STUDENTID,CNBCD=@CNBCD,
                 PONO=@PONO,PODT=@PODT,POBANK=@POBANK,POBANKBR=@POBANKBR,REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE TRANSYY=@TRANSYY  AND TRANSTP='MREC AND TRANSNO=@TRANSNO";

                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYR;
                    cmd.Parameters.Add("@TRANSNO", SqlDbType.Int).Value = ob.TrnsNO;
                    cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.RegYR;
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                    cmd.Parameters.Add("@CNBCD", SqlDbType.NVarChar).Value = ob.AccNo;
                    cmd.Parameters.Add("@PONO", SqlDbType.NVarChar).Value = ob.PONO;
                    cmd.Parameters.Add("@PODT", SqlDbType.Date).Value = ob.PODT;
                    cmd.Parameters.Add("@POBANK", SqlDbType.NVarChar).Value = ob.POBNK;
                    cmd.Parameters.Add("@POBANKBR", SqlDbType.NVarChar).Value = ob.POBRNC;

                    cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                    cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                    cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                    cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                    cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Update_EIM_TRANSMST"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;
            }
        }

        public string Insert_EIM_ADMEDUQ(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Insert_EIM_ADMEDUQ"] = "";
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO EIM_ADMEDUQ (TESTYY,SEMESTERID,PROGRAMID,TESTDT,ROLLNO,
                                             EXAMSL,EXAMNM,SESSION,GROUPSUB,BOARDUNI,PASSYY,GPAMARKS,DIVGRADE,MRNO,USERID,USERPC,IPADDRESS,INTIME) VALUES 
                                                       (@TESTYY,@SEMESTERID,@PROGRAMID,@TESTDT,@ROLLNO,
                                             @EXAMSL,@EXAMNM,@SESSION,@GROUPSUB,@BOARDUNI,@PASSYY,@GPAMARKS,@DIVGRADE,@MRNO,@USERID,@USERPC,@IPADDRESS,@INTIME)";

                    cmd.Parameters.Add("@TESTYY", SqlDbType.Int).Value = ob.YR;
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@TESTDT", SqlDbType.Date).Value = ob.ExamDT;
                    cmd.Parameters.Add("@ROLLNO", SqlDbType.Int).Value = ob.Roll;
                    cmd.Parameters.Add("@EXAMSL", SqlDbType.Int).Value = ob.ExamSL;
                    cmd.Parameters.Add("@EXAMNM", SqlDbType.NVarChar).Value = ob.ExamNM;
                    cmd.Parameters.Add("@SESSION", SqlDbType.NVarChar).Value = ob.SeSN;
                    cmd.Parameters.Add("@GROUPSUB", SqlDbType.NVarChar).Value = ob.GRP;
                    cmd.Parameters.Add("@BOARDUNI", SqlDbType.NVarChar).Value = ob.BRD;

                    cmd.Parameters.Add("@PASSYY", SqlDbType.NVarChar).Value = ob.PassYR;
                    cmd.Parameters.Add("@GPAMARKS", SqlDbType.NVarChar).Value = ob.GPA;
                    cmd.Parameters.Add("@DIVGRADE", SqlDbType.NVarChar).Value = ob.LtrGRD;

                    cmd.Parameters.Add("@MRNO", SqlDbType.BigInt).Value = ob.MrNo;

                    cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                    cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                    cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                    cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Insert_EIM_ADMEDUQ"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;

            }
        }

        public string Update_EIM_ADMEDUQ(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Update_EIM_ADMEDUQ"] = "";
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"UPDATE EIM_ADMEDUQ SET EXAMNM=@EXAMNM,SESSION=@SESSION,GROUPSUB=@GROUPSUB,BOARDUNI=@BOARDUNI,PASSYY=@PASSYY,GPAMARKS=@GPAMARKS,
                                        DIVGRADE=@DIVGRADE,MRNO=@MRNO,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE TESTYY=@TESTYY 
                                AND SEMESTERID=@SEMESTERID AND PROGRAMID=@PROGRAMID AND ROLLNO=@ROLLNO AND EXAMSL=@EXAMSL";

                    cmd.Parameters.Add("@TESTYY", SqlDbType.Int).Value = ob.YR;
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@TESTDT", SqlDbType.Date).Value = ob.ExamDT;
                    cmd.Parameters.Add("@ROLLNO", SqlDbType.Int).Value = ob.Roll;
                    cmd.Parameters.Add("@EXAMSL", SqlDbType.Int).Value = ob.ExamSL;
                    cmd.Parameters.Add("@EXAMNM", SqlDbType.NVarChar).Value = ob.ExamNM;
                    cmd.Parameters.Add("@SESSION", SqlDbType.NVarChar).Value = ob.SeSN;
                    cmd.Parameters.Add("@GROUPSUB", SqlDbType.NVarChar).Value = ob.GRP;
                    cmd.Parameters.Add("@BOARDUNI", SqlDbType.NVarChar).Value = ob.BRD;
                    cmd.Parameters.Add("@PASSYY", SqlDbType.NVarChar).Value = ob.PassYR;
                    cmd.Parameters.Add("@GPAMARKS", SqlDbType.NVarChar).Value = ob.GPA;
                    cmd.Parameters.Add("@DIVGRADE", SqlDbType.NVarChar).Value = ob.LtrGRD;
                    cmd.Parameters.Add("@MRNO", SqlDbType.BigInt).Value = ob.MrNo;
                    cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                    cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                    cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                    cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Update_EIM_ADMEDUQ"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;

            }
        }

        public string UpdateAdmission(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["UpdateAdmission"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE EIM_ADMISSION SET TESTYY=@TESTYY,IMAGE=@IMAGE,SEMESTERID=@SEMESTERID,PROGRAMID=@PROGRAMID,TESTDT=@TESTDT,STUDENTNM=@STUDENTNM,
                                        FATHERNM=@FATHERNM,MOTHERNM=@MOTHERNM,ADDRPRE=@ADDRPRE,ADDRPER=@ADDRPER,MOBNO=@MOBNO,EMAIL=@EMAIL,NATIONALITY=@NATIONALITY,RELIGION=@RELIGION,
                                    DOB=@DOB,GENDER=@GENDER,GUARDIANNM=@GUARDIANNM,GRELATION=@GRELATION,GPROFESSION=@GPROFESSION,
                                      GADDRESS=@GADDRESS,GMOBNO=@GMOBNO,GEMAIL=@GEMAIL,MRAMT=@MRAMT,
                               UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE MRNO=@MRNO";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TESTYY", SqlDbType.NVarChar).Value = ob.ExamYr;
                cmd.Parameters.Add("@IMAGE", SqlDbType.NVarChar).Value = ob.Img;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@TESTDT", SqlDbType.SmallDateTime).Value = ob.ExamDT;
                cmd.Parameters.Add("@STUDENTNM", SqlDbType.NVarChar).Value = ob.StuNM;
                cmd.Parameters.Add("@FATHERNM", SqlDbType.NVarChar).Value = ob.StuFNM;
                cmd.Parameters.Add("@MOTHERNM", SqlDbType.NVarChar).Value = ob.StuMNM;
                cmd.Parameters.Add("@ADDRPRE", SqlDbType.NVarChar).Value = ob.PreAdrs;
                cmd.Parameters.Add("@ADDRPER", SqlDbType.NVarChar).Value = ob.PerAdrs;
                cmd.Parameters.Add("@MOBNO", SqlDbType.NVarChar).Value = ob.MobNO;
                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@NATIONALITY", SqlDbType.NVarChar).Value = ob.Nation;
                cmd.Parameters.Add("@RELIGION", SqlDbType.NVarChar).Value = ob.Religion;
                cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = ob.DtOfBrt;
                cmd.Parameters.Add("@GENDER", SqlDbType.NVarChar).Value = ob.Gander;
                cmd.Parameters.Add("@GUARDIANNM", SqlDbType.NVarChar).Value = ob.GurNM;
                cmd.Parameters.Add("@GRELATION", SqlDbType.NVarChar).Value = ob.GRel;
                cmd.Parameters.Add("@GPROFESSION", SqlDbType.NVarChar).Value = ob.GProf;
                cmd.Parameters.Add("@GADDRESS", SqlDbType.NVarChar).Value = ob.GAdrs;
                cmd.Parameters.Add("@GMOBNO", SqlDbType.NVarChar).Value = ob.GMNo;
                cmd.Parameters.Add("@GEMAIL", SqlDbType.NVarChar).Value = ob.GEml;
                cmd.Parameters.Add("@MRAMT", SqlDbType.Decimal).Value = ob.TotlAmnt;
                cmd.Parameters.Add("@MRNO", SqlDbType.BigInt).Value = ob.MrNo;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["UpdateAdmission"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }



        public string Insert_EIM_RESULT(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Insert_EIM_RESULT"] = "";
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO EIM_RESULT (REGYY,SEMESTERID,PROGRAMID,SEMID,STUDENTID,COURSEID,M40,M60,REMARKS,USERID,USERPC,IPADDRESS,INTIME) VALUES 
                                                       (@REGYY,@SEMESTERID,@PROGRAMID,@SEMID,@STUDENTID,@COURSEID,@M40,@M60,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";

                    cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.YR;
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@SEMID", SqlDbType.NVarChar).Value = ob.SemisterID;
                    cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                    cmd.Parameters.Add("@COURSEID", SqlDbType.NVarChar).Value = ob.CrsID;
                    cmd.Parameters.Add("@M40", SqlDbType.Decimal).Value = ob.M40;
                    cmd.Parameters.Add("@M60", SqlDbType.Decimal).Value = ob.M60;
                    cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                    cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                    cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                    cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                    cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Insert_EIM_RESULT"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;

            }
        }
        public string Insert_EIM_RESULT_Auto(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Insert_EIM_RESULT"] = "";
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO EIM_RESULT (REGYY,SEMESTERID,PROGRAMID,SEMID,STUDENTID,COURSEID,M40,M60,REMARKS,USERID,USERPC,IPADDRESS,INTIME) VALUES 
                                                       (@REGYY,@SEMESTERID,@PROGRAMID,@SEMID,@STUDENTID,@COURSEID,@M40,@M60,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";

                    cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.YR;
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@SEMID", SqlDbType.NVarChar).Value = ob.SemisterID;
                    cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                    cmd.Parameters.Add("@COURSEID", SqlDbType.NVarChar).Value = ob.CrsID;
                    cmd.Parameters.Add("@M40", SqlDbType.NVarChar).Value = ob.M40;
                    cmd.Parameters.Add("@M60", SqlDbType.NVarChar).Value = ob.M60;
                    cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                    cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                    cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                    cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                    cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Insert_EIM_RESULT"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;

            }
        }
        public string Update_EIM_RESULT(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Update_EIM_RESULT"] = "";
                    //string STUDENTID = HttpContext.Current.Session["STUDENTID"].ToString();

                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"UPDATE EIM_RESULT SET M60=@M60,M40=@M40,REMARKS=@REMARKS,
                                            UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME 
                                        WHERE REGYY=@REGYY AND SEMESTERID=@SEMESTERID AND SEMID=@SEMID AND PROGRAMID=@PROGRAMID AND STUDENTID=@STUDENTID AND COURSEID=@COURSEID";

                    cmd.Parameters.Add("@SEMID", SqlDbType.NVarChar).Value = ob.SemisterID;
                    cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.YR;
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                    cmd.Parameters.Add("@COURSEID", SqlDbType.NVarChar).Value = ob.CrsID;
                    cmd.Parameters.Add("@M60", SqlDbType.Decimal).Value = ob.M60;
                    cmd.Parameters.Add("@M40", SqlDbType.Decimal).Value = ob.M40;
                    cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                    cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                    cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                    cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                    cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Update_EIM_RESULT"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;

            }
        }

        public string Insert_EIM_STUEDUQ(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Insert_EIM_STUEDUQ"] = "";
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO EIM_STUEDUQ (EXAMSL,EXAMNM,PASSYY,EXAMROLL,GROUPSUB,DIVGRADE,INSTITUTE,BOARDUNI,STUDENTID,USERID,USERPC,IPADDRESS,INTIME) VALUES 
                                                       (@EXAMSL,@EXAMNM,@PASSYY,@EXAMROLL,@GROUPSUB,@DIVGRADE,@INSTITUTE,@BOARDUNI,@STUDENTID,@USERID,@USERPC,@IPADDRESS,@INTIME)";

                    cmd.Parameters.Add("@EXAMSL", SqlDbType.Int).Value = ob.ExamSL;
                    cmd.Parameters.Add("@EXAMNM", SqlDbType.NVarChar).Value = ob.ExamNM;
                    cmd.Parameters.Add("@PASSYY", SqlDbType.NVarChar).Value = ob.PassYR;
                    cmd.Parameters.Add("@EXAMROLL", SqlDbType.NVarChar).Value = ob.ExamRoll;
                    cmd.Parameters.Add("@GROUPSUB", SqlDbType.NVarChar).Value = ob.GRP;
                    cmd.Parameters.Add("@DIVGRADE", SqlDbType.NVarChar).Value = ob.GRAD;
                    cmd.Parameters.Add("@INSTITUTE", SqlDbType.NVarChar).Value = ob.insNM;
                    cmd.Parameters.Add("@BOARDUNI", SqlDbType.NVarChar).Value = ob.BRD;
                    cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                    cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                    cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                    cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                    cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Insert_EIM_STUEDUQ"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;

            }
        }

        public string Update_EIM_STUEDUQ(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    //string STUDENTID = HttpContext.Current.Session["COURSEID"].ToString();
                    //string COURSEID = HttpContext.Current.Session["COURSEID"].ToString();
                    HttpContext.Current.Session["Update_EIM_STUEDUQ"] = "";
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"UPDATE EIM_STUEDUQ SET EXAMNM=@EXAMNM,PASSYY=@PASSYY,EXAMROLL=@EXAMROLL,GROUPSUB=@GROUPSUB,DIVGRADE=@DIVGRADE,INSTITUTE=@INSTITUTE,BOARDUNI=@BOARDUNI,
                                            UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME 
                                        WHERE EXAMSL=@EXAMSL AND STUDENTID=@STUDENTID";

                    cmd.Parameters.Add("@EXAMSL", SqlDbType.Int).Value = ob.ExamSL;
                    cmd.Parameters.Add("@EXAMNM", SqlDbType.NVarChar).Value = ob.ExamNM;
                    cmd.Parameters.Add("@PASSYY", SqlDbType.NVarChar).Value = ob.PassYR;
                    cmd.Parameters.Add("@EXAMROLL", SqlDbType.NVarChar).Value = ob.ExamRoll;
                    cmd.Parameters.Add("@GROUPSUB", SqlDbType.NVarChar).Value = ob.GRP;
                    cmd.Parameters.Add("@DIVGRADE", SqlDbType.NVarChar).Value = ob.GRAD;
                    cmd.Parameters.Add("@INSTITUTE", SqlDbType.NVarChar).Value = ob.insNM;
                    cmd.Parameters.Add("@BOARDUNI", SqlDbType.NVarChar).Value = ob.BRD;
                    cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                    cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                    cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                    cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                    cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Update_EIM_STUEDUQ"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;
            }
        }

        public string Insert_HR_EMP(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_EMP(COMPID,EMPID,EMPNM,GUARDIANNM,MOTHERNM,ADDRESS_PRE,ADDRESS_PER,CONTACTNO,EMAILID,DOB,GENDER,VOTERIDNO,BLOODGR,REFNM1,REFDESIG1,REFADD1,REFCNO1,REFNM2,REFDESIG2,REFADD2,REFCNO2,JOININGDT,BANKACNO,DEPTID,POSTID,BASICSAL,HRENT,MALLWNC,ECONVEY,OTHER,STATUS,USERID,USERPC,IPADDRESS,INTIME)
 				Values 
				(@COMPID,@EMPID,@EMPNM,@GUARDIANNM,@MOTHERNM,@ADDRESS_PRE,@ADDRESS_PER,@CONTACTNO,@EMAILID,@DOB,@GENDER,@VOTERIDNO,@BLOODGR,@REFNM1,@REFDESIG1,@REFADD1,@REFCNO1,@REFNM2,@REFDESIG2,@REFADD2,@REFCNO2,@JOININGDT,@BANKACNO,@DEPTID,@POSTID,@BASICSAL,@HRENT,@MALLWNC,@ECONVEY,@OTHER,@STATUS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                cmd.Parameters.Add("@EMPNM", SqlDbType.NVarChar).Value = ob.EmpNM;
                cmd.Parameters.Add("@GUARDIANNM", SqlDbType.NVarChar).Value = ob.EmpGNM;
                cmd.Parameters.Add("@MOTHERNM", SqlDbType.NVarChar).Value = ob.EmpMNM;
                cmd.Parameters.Add("@ADDRESS_PRE", SqlDbType.NVarChar).Value = ob.PreAdrs;
                cmd.Parameters.Add("@ADDRESS_PER", SqlDbType.NVarChar).Value = ob.PerAdrs;
                cmd.Parameters.Add("@CONTACTNO", SqlDbType.NVarChar).Value = ob.EmpCNO;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = ob.DOB;
                cmd.Parameters.Add("@GENDER", SqlDbType.NVarChar).Value = ob.Gander;
                cmd.Parameters.Add("@VOTERIDNO", SqlDbType.NVarChar).Value = ob.NIDPNO;
                cmd.Parameters.Add("@BLOODGR", SqlDbType.NVarChar).Value = ob.BldGRP;
                cmd.Parameters.Add("@REFNM1", SqlDbType.NVarChar).Value = ob.Ref1NM;
                cmd.Parameters.Add("@REFDESIG1", SqlDbType.NVarChar).Value = ob.Ref1Desig;
                cmd.Parameters.Add("@REFADD1", SqlDbType.NVarChar).Value = ob.Ref1Adrs;
                cmd.Parameters.Add("@REFCNO1", SqlDbType.NVarChar).Value = ob.Ref1CNO;
                cmd.Parameters.Add("@REFNM2", SqlDbType.NVarChar).Value = ob.Ref2NM;
                cmd.Parameters.Add("@REFDESIG2", SqlDbType.NVarChar).Value = ob.Ref2Desig;
                cmd.Parameters.Add("@REFADD2", SqlDbType.NVarChar).Value = ob.Ref2Adrs;
                cmd.Parameters.Add("@REFCNO2", SqlDbType.NVarChar).Value = ob.Ref2CNO;
                cmd.Parameters.Add("@JOININGDT", SqlDbType.Date).Value = ob.JoinDT;
                cmd.Parameters.Add("@BANKACNO", SqlDbType.NVarChar).Value = ob.BankAcNO;
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = ob.DPTID;
                cmd.Parameters.Add("@POSTID", SqlDbType.Int).Value = ob.PostID;
                cmd.Parameters.Add("@BASICSAL", SqlDbType.Decimal).Value = ob.BasicSalary;
                cmd.Parameters.Add("@HRENT", SqlDbType.Decimal).Value = ob.HRent;
                cmd.Parameters.Add("@MALLWNC", SqlDbType.Decimal).Value = ob.MAllownc;
                cmd.Parameters.Add("@ECONVEY", SqlDbType.Decimal).Value = ob.EConvey;
                cmd.Parameters.Add("@OTHER", SqlDbType.Decimal).Value = ob.Other;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.status;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                s = "true";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_EMP(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE HR_EMP SET  DEPTID=@DEPTID, EMPID=@EMPID,GUARDIANNM=@GUARDIANNM,MOTHERNM=@MOTHERNM,ADDRESS_PRE=@ADDRESS_PRE,ADDRESS_PER=@ADDRESS_PER,CONTACTNO=@CONTACTNO,EMAILID=@EMAILID,DOB=@DOB,GENDER=@GENDER,VOTERIDNO=@VOTERIDNO,BLOODGR=@BLOODGR,REFNM1=@REFNM1,REFDESIG1=@REFDESIG1,REFADD1=@REFADD1,REFCNO1=@REFCNO1,REFNM2=@REFNM2,REFDESIG2=@REFDESIG2,REFADD2=@REFADD2,REFCNO2=@REFCNO2,JOININGDT=@JOININGDT,BANKACNO=@BANKACNO,POSTID=@POSTID,BASICSAL=@BASICSAL,HRENT=@HRENT,MALLWNC=@MALLWNC,ECONVEY=@ECONVEY,OTHER=@OTHER,STATUS=@STATUS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE SL=@SL ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@SL", SqlDbType.BigInt).Value = ob.SL;
                cmd.Parameters.Add("@DEPTID", SqlDbType.BigInt).Value = ob.DPTID;
                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                //cmd.Parameters.Add("@EMPNM", SqlDbType.NVarChar).Value = ob.EmpNM;
                cmd.Parameters.Add("@GUARDIANNM", SqlDbType.NVarChar).Value = ob.EmpGNM;
                cmd.Parameters.Add("@MOTHERNM", SqlDbType.NVarChar).Value = ob.EmpMNM;
                cmd.Parameters.Add("@ADDRESS_PRE", SqlDbType.NVarChar).Value = ob.PreAdrs;
                cmd.Parameters.Add("@ADDRESS_PER", SqlDbType.NVarChar).Value = ob.PerAdrs;
                cmd.Parameters.Add("@CONTACTNO", SqlDbType.NVarChar).Value = ob.EmpCNO;
                cmd.Parameters.Add("@EMAILID", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = ob.DOB;
                cmd.Parameters.Add("@GENDER", SqlDbType.NVarChar).Value = ob.Gander;
                cmd.Parameters.Add("@VOTERIDNO", SqlDbType.NVarChar).Value = ob.NIDPNO;
                cmd.Parameters.Add("@BLOODGR", SqlDbType.NVarChar).Value = ob.BldGRP;
                cmd.Parameters.Add("@REFNM1", SqlDbType.NVarChar).Value = ob.Ref1NM;
                cmd.Parameters.Add("@REFDESIG1", SqlDbType.NVarChar).Value = ob.Ref1Desig;
                cmd.Parameters.Add("@REFADD1", SqlDbType.NVarChar).Value = ob.Ref1Adrs;
                cmd.Parameters.Add("@REFCNO1", SqlDbType.NVarChar).Value = ob.Ref1CNO;
                cmd.Parameters.Add("@REFNM2", SqlDbType.NVarChar).Value = ob.Ref2NM;
                cmd.Parameters.Add("@REFDESIG2", SqlDbType.NVarChar).Value = ob.Ref2Desig;
                cmd.Parameters.Add("@REFADD2", SqlDbType.NVarChar).Value = ob.Ref2Adrs;
                cmd.Parameters.Add("@REFCNO2", SqlDbType.NVarChar).Value = ob.Ref2CNO;
                cmd.Parameters.Add("@JOININGDT", SqlDbType.Date).Value = ob.JoinDT;
                cmd.Parameters.Add("@BANKACNO", SqlDbType.NVarChar).Value = ob.BankAcNO; 
                cmd.Parameters.Add("@POSTID", SqlDbType.BigInt).Value = ob.PostID;
                cmd.Parameters.Add("@BASICSAL", SqlDbType.Decimal).Value = ob.BasicSalary;
                cmd.Parameters.Add("@HRENT", SqlDbType.Decimal).Value = ob.HRent;
                cmd.Parameters.Add("@MALLWNC", SqlDbType.Decimal).Value = ob.MAllownc;
                cmd.Parameters.Add("@ECONVEY", SqlDbType.Decimal).Value = ob.EConvey;
                cmd.Parameters.Add("@OTHER", SqlDbType.Decimal).Value = ob.Other;
                cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = ob.status;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                s = "true";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string Insert_HR_POST(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_POST(COMPID,POSTID,POSTNM,REMARKS,USERID,USERPC,IPADDRESS,INTIME ) Values

                    (@COMPID,@POSTID,@POSTNM,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@POSTID", SqlDbType.Int).Value = ob.PostID;
                cmd.Parameters.Add("@POSTNM", SqlDbType.NVarChar).Value = ob.PostNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string Update_HR_POST(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE  HR_POST SET COMPID=@COMPID,POSTNM=@POSTNM,REMARKS=@REMARKS,
                            UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE POSTID=@POSTID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@POSTID", SqlDbType.Int).Value = ob.PostID;
                cmd.Parameters.Add("@POSTNM", SqlDbType.NVarChar).Value = ob.PostNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string Insert_HR_DEPT(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_DEPT(COMPID,DEPTID,DEPTNM,DEPTSNM,REMARKS,USERID,USERPC,IPADDRESS,INTIME) Values
                                 (@COMPID,@DEPTID,@DEPTNM,@DEPTSNM,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = ob.DeptID;
                cmd.Parameters.Add("@DEPTNM", SqlDbType.NVarChar).Value = ob.DeptNM;
                cmd.Parameters.Add("@DEPTSNM", SqlDbType.NVarChar).Value = ob.DeptSNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Update_HR_DEPT(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE  HR_DEPT SET COMPID=@COMPID,DEPTNM=@DEPTNM,DEPTSNM=@DEPTSNM,REMARKS=@REMARKS,
                            UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE DEPTID=@DEPTID";

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@COMPID", SqlDbType.Int).Value = ob.CmpID;
                cmd.Parameters.Add("@DEPTID", SqlDbType.NVarChar).Value = ob.DeptID;
                cmd.Parameters.Add("@DEPTNM", SqlDbType.NVarChar).Value = ob.DeptNM;
                cmd.Parameters.Add("@DEPTSNM", SqlDbType.NVarChar).Value = ob.DeptSNM;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                //cmd.Parameters.Add("@UPDLTUDE", SqlDbType.NVarChar).Value = ob.UPDLtude;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string INSERT_HR_SALDRCR(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO HR_SALDRCR(TRANSMY,EMPID,MMDAY,HDAY,PREDAY,ABSDAY,LDAY,ALLOWANCE,ADVANCE,USERID,USERPC,IPADDRESS,INTIME)
 				Values 
				(@TRANSMY,@EMPID,@MMDAY,@HDAY,@PREDAY,@ABSDAY,@LDAY,@ALLOWANCE,@ADVANCE,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TRANSMY;
                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                cmd.Parameters.Add("@MMDAY", SqlDbType.Int).Value = ob.MMDay;
                cmd.Parameters.Add("@HDAY", SqlDbType.Int).Value = ob.HDAY;
                cmd.Parameters.Add("@PREDAY", SqlDbType.Int).Value = ob.PreDay;
                cmd.Parameters.Add("@ABSDAY", SqlDbType.Int).Value = ob.AbsentDay;
                cmd.Parameters.Add("@LDAY", SqlDbType.Int).Value = ob.LDay;
                cmd.Parameters.Add("@ALLOWANCE", SqlDbType.Decimal).Value = ob.ALLOWANCE;
                cmd.Parameters.Add("@ADVANCE", SqlDbType.Decimal).Value = ob.ADVANCE;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string UPDATE_HR_SALDRCR(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE HR_SALDRCR SET MMDAY=@MMDAY,HDAY=@HDAY,PREDAY=@PREDAY,ABSDAY=@ABSDAY,LDAY=@LDAY,ALLOWANCE=@ALLOWANCE,ADVANCE=@ADVANCE,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME WHERE TRANSMY=@TRANSMY AND EMPID=@EMPID ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TRANSMY;
                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                cmd.Parameters.Add("@MMDAY", SqlDbType.Int).Value = ob.MMDay;
                cmd.Parameters.Add("@HDAY", SqlDbType.Int).Value = ob.HDAY;
                cmd.Parameters.Add("@PREDAY", SqlDbType.Int).Value = ob.PreDay;
                cmd.Parameters.Add("@ABSDAY", SqlDbType.Int).Value = ob.AbsentDay;
                cmd.Parameters.Add("@LDAY", SqlDbType.Int).Value = ob.LDay;
                cmd.Parameters.Add("@ALLOWANCE", SqlDbType.Decimal).Value = ob.ALLOWANCE;
                cmd.Parameters.Add("@ADVANCE", SqlDbType.Decimal).Value = ob.ADVANCE;
                cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                cmd.Parameters.Add("@UPDTIME", SqlDbType.NVarChar).Value = ob.UPDTime;
                cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;

                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }
        public string DELETE_HR_SALDRCR(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"DELETE FROM HR_SALDRCR WHERE TRANSMY=@TRANSMY AND EMPID=@EMPID ";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@TRANSMY", SqlDbType.NVarChar).Value = ob.TRANSMY;
                cmd.Parameters.Add("@EMPID", SqlDbType.NVarChar).Value = ob.EmpID;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_Due_EIM_TRANSMST(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["Insert_Due_EIM_TRANSMST"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO EIM_TRANSMST(STUDENTID,TRANSFOR,TRANSDT,TRANSTP,TRANSYY,TRANSNO,REGYY,SEMESTERID,PROGRAMID, 
                    REMARKS,USERID,USERPC,IPADDRESS,INTIME) Values
                    (@STUDENTID,@TRANSFOR,@TRANSDT,@TRANSTP,@TRANSYY,@TRANSNO,@REGYY,@SEMESTERID,@PROGRAMID,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.TransFor;
                cmd.Parameters.Add("@TRANSDT", SqlDbType.Date).Value = ob.TrnsDT;
                cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = "JOUR";
                cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYR;
                cmd.Parameters.Add("@TRANSNO", SqlDbType.Int).Value = ob.TrnsNO;
                cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.RegYR;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["Insert_EIM_TRANSMST"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
            return s;
        }

        public string Insert_Due_EIM_TRANS(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Insert_Due_EIM_TRANS"] = "";
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO EIM_TRANS(TRANSFOR,TRANSDT,TRANSTP,TRANSYY,TRANSNO,REGYY,SEMESTERID,PROGRAMID,STUDENTID, 
                 FEESID,AMOUNT,REMARKS,USERID,USERPC,IPADDRESS,INTIME) Values
                    (@TRANSFOR,@TRANSDT,@TRANSTP,@TRANSYY,@TRANSNO,@REGYY,@SEMESTERID,@PROGRAMID,@STUDENTID, 
                 @FEESID,@AMOUNT,@REMARKS,@USERID,@USERPC,@IPADDRESS,@INTIME)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.TransFor;
                    cmd.Parameters.Add("@TRANSDT", SqlDbType.Date).Value = ob.TrnsDT;
                    cmd.Parameters.Add("@TRANSTP", SqlDbType.NVarChar).Value = "JOUR";
                    cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYR;
                    cmd.Parameters.Add("@TRANSNO", SqlDbType.Int).Value = ob.TrnsNO;
                    cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.RegYR;
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                    cmd.Parameters.Add("@FEESID", SqlDbType.NVarChar).Value = ob.FeesID;
                    cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amnt;
                    cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksGRD;
                    cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                    cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                    cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                    cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Insert_EIM_TRANS"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;
            }
        }

        public string Update_Due_EIM_TRANS(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Update_Due_EIM_TRANS"] = "";
                    string StuID = HttpContext.Current.Session["STUDENTID"].ToString();
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"UPDATE EIM_TRANS SET STUDENTID=@STUDENTID,SEMESTERID=@SEMESTERID,PROGRAMID=@PROGRAMID,
                    FEESID=@FEESID, AMOUNT=@AMOUNT,REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,UPDTIME=@UPDTIME 
                    WHERE TRANSYY=@TRANSYY AND TRANSFOR=@TRANSFOR AND TRANSNO=@TRANSNO AND TRANSTP='JOUR' AND FEESID=@FEESID AND STUDENTID='" + StuID + "'";

                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYR;
                    cmd.Parameters.Add("@TRANSNO", SqlDbType.Int).Value = ob.TrnsNO;
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                    cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.TransFor;
                    cmd.Parameters.Add("@FEESID", SqlDbType.NVarChar).Value = ob.FeesID;
                    cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = ob.Amnt; 
                    cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.RemarksGRD;
                    cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                    cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                    cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                    cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Update_EIM_TRANS"] = "True";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;
            }
        }

        public string Update_Due_EIM_TRANSMST(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    HttpContext.Current.Session["Update_Due_EIM_TRANSMST"] = "";
                    string StuID = HttpContext.Current.Session["STUDENTID"].ToString();
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = @"UPDATE EIM_TRANSMST SET STUDENTID=@STUDENTID,SEMESTERID=@SEMESTERID,PROGRAMID=@PROGRAMID,  
                        REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,
                        UPDTIME=@UPDTIME WHERE TRANSYY=@TRANSYY  AND TRANSTP='JOUR' AND TRANSNO=@TRANSNO AND STUDENTID='" + StuID + "' AND TRANSFOR=@TRANSFOR";

                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                    cmd.Parameters.Add("@TRANSFOR", SqlDbType.Int).Value = ob.TransFor;
                    cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYR;
                    cmd.Parameters.Add("@TRANSNO", SqlDbType.Int).Value = ob.TrnsNO;
                    cmd.Parameters.Add("@REGYY", SqlDbType.Int).Value = ob.RegYR;
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID; 
                    cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                    cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                    cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                    cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                    cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Update_EIM_TRANSMST"] = "True";
                    s = "true";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;
            }
        }
        public string Update_Due_EIM_TRANSMST_TOP(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = @"UPDATE EIM_TRANSMST SET SEMESTERID=@SEMESTERID,PROGRAMID=@PROGRAMID, 
                        REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,
                        UPDTIME=@UPDTIME WHERE TRANSYY=@TRANSYY  AND TRANSTP='JOUR' AND TRANSNO=@TRANSNO AND 
TRANSFOR=@TRANSFOR";

                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.TransFor;
                    cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYR;
                    cmd.Parameters.Add("@TRANSNO", SqlDbType.Int).Value = ob.TrnsNO; 
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                    cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                    cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                    cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                    cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    HttpContext.Current.Session["Update_EIM_TRANSMST"] = "True";
                    s = "true";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;
            }
        }
        public string Update_Due_EIM_TRANSMST_DETAILS(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = @"UPDATE EIM_TRANS SET FEESID=@FEESID,SEMESTERID=@SEMESTERID,PROGRAMID=@PROGRAMID,  
                        REMARKS=@REMARKS,UPDUSERID=@UPDUSERID,UPDUSERPC=@UPDUSERPC,UPDIPADDRESS=@UPDIPADDRESS,
                        UPDTIME=@UPDTIME WHERE TRANSYY=@TRANSYY  AND TRANSTP='JOUR' AND TRANSNO=@TRANSNO AND  TRANSFOR=@TRANSFOR";

                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@FEESID", SqlDbType.Int).Value = ob.FeesID;
                    cmd.Parameters.Add("@TRANSFOR", SqlDbType.NVarChar).Value = ob.TransFor;
                    cmd.Parameters.Add("@TRANSYY", SqlDbType.Int).Value = ob.TransYR;
                    cmd.Parameters.Add("@TRANSNO", SqlDbType.Int).Value = ob.TrnsNO; 
                    cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                    cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                    cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.Remarks;
                    cmd.Parameters.Add("@UPDUSERID", SqlDbType.NVarChar).Value = ob.UPDUserID;
                    cmd.Parameters.Add("@UPDUSERPC", SqlDbType.NVarChar).Value = ob.UPDPcName;
                    cmd.Parameters.Add("@UPDIPADDRESS", SqlDbType.NVarChar).Value = ob.UPDIpaddress;
                    cmd.Parameters.Add("@UPDTIME", SqlDbType.SmallDateTime).Value = ob.UPDTime;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery(); 
                    s = "true";
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;
            }
        }

        public string INSERT_LOG(Data_Model ob)
        {
            {
                string s = "";
                SqlTransaction tran = null;
                try
                {
                    if (con.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open)con.Open();
                    tran = con.BeginTransaction();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO LOG (LOGDATA,TYPE,TABLEID) VALUES (@LOGDATA,@TYPE,@TABLEID)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@LOGDATA", SqlDbType.NVarChar).Value = ob.Descrip;
                    cmd.Parameters.Add("@TYPE", SqlDbType.NVarChar).Value = ob.Type;
                    cmd.Parameters.Add("@TABLEID", SqlDbType.NVarChar).Value = ob.TableID;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    if (con.State != ConnectionState.Closed)
                        if (con.State != ConnectionState.Closed)con.Close();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
                return s;
            }
        }
        public string INSERT_MIGRATE_STK_STUDENT(Data_Model ob)
        {
            string s = "";
            SqlTransaction tran = null;
            try
            {
                HttpContext.Current.Session["InsertApplicationReg"] = "";
                if (con.State != ConnectionState.Open)
                    if (con.State != ConnectionState.Open)con.Open();
                tran = con.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO EIM_STUDENT(MIGRATED,ADMITYY,IMAGE,ADMITSL,ADMITDT,ADMITTP,SEMESTERID,PROGRAMID,SESSION,BATCH,STUDENTID,STUDENTNM,FATHERNM,
                                                        FATHEROCP,FOCPDTL,MOTHERNM,MOTHEROCP,SPOUSENM,SPOUSEOCP,ADDRPRE,ADDRPER,TELNO,MOBNO,EMAIL,NATIONALITY,
                       RELIGION,DOB,GENDER,STUDENTTP,BIRTHP,NIDPNO,BLOODGR,PRESIDENCE,MSTATUS,RESHOSTEL,INCOMEYY,EXPENSEYY,GUARDIANNM,GRELATION,GADDRESS,
                 GTELNO,GMOBNO,GEMAIL,PREPROGTP,PREPROGNM,PPINSTITN,PPSESSION,FIRMNM,POSITION,REMARKS,
                               USERID,USERPC,IPADDRESS,INTIME) Values
                    (@MIGRATED,@ADMITYY,@IMAGE,@ADMITSL,@ADMITDT,@ADMITTP,@SEMESTERID,@PROGRAMID,@SESSION,@BATCH,@STUDENTID,@STUDENTNM,@FATHERNM,
                                                        @FATHEROCP,@FOCPDTL,@MOTHERNM,@MOTHEROCP,@SPOUSENM,@SPOUSEOCP,@ADDRPRE,@ADDRPER,@TELNO,@MOBNO,@EMAIL,@NATIONALITY,
                       @RELIGION,@DOB,@GENDER,@STUDENTTP,@BIRTHP,@NIDPNO,@BLOODGR,@PRESIDENCE,@MSTATUS,@RESHOSTEL,@INCOMEYY,@EXPENSEYY,@GUARDIANNM,@GRELATION,@GADDRESS,
                @GTELNO,@GMOBNO,@GEMAIL,@PREPROGTP,@PREPROGNM,@PPINSTITN,@PPSESSION,@FIRMNM,@POSITION,@REMARKS,
                               @USERID,@USERPC,@IPADDRESS,@INTIME)";


                cmd.Parameters.Clear();
                cmd.Parameters.Add("@MIGRATED", SqlDbType.NVarChar).Value = "YES";
                cmd.Parameters.Add("@ADMITYY", SqlDbType.NVarChar).Value = ob.AdmtYR;
                cmd.Parameters.Add("@IMAGE", SqlDbType.NVarChar).Value = ob.Img;
                cmd.Parameters.Add("@ADMITSL", SqlDbType.Int).Value = ob.AdmtSL;
                cmd.Parameters.Add("@ADMITDT", SqlDbType.Date).Value = ob.ADMITDT;
                cmd.Parameters.Add("@ADMITTP", SqlDbType.NVarChar).Value = ob.ADMITTP;
                cmd.Parameters.Add("@SEMESTERID", SqlDbType.Int).Value = ob.SemID;
                cmd.Parameters.Add("@PROGRAMID", SqlDbType.NVarChar).Value = ob.ProgID;
                cmd.Parameters.Add("@SESSION", SqlDbType.NVarChar).Value = ob.SeSN;
                cmd.Parameters.Add("@BATCH", SqlDbType.NVarChar).Value = ob.Batch;
                cmd.Parameters.Add("@STUDENTID", SqlDbType.NVarChar).Value = ob.StuID;
                cmd.Parameters.Add("@STUDENTNM", SqlDbType.NVarChar).Value = ob.StuNM; 
                cmd.Parameters.Add("@FATHERNM", SqlDbType.NVarChar).Value = ob.StuFNM;
                cmd.Parameters.Add("@FATHEROCP", SqlDbType.NVarChar).Value = ob.FOcup;
                cmd.Parameters.Add("@FOCPDTL", SqlDbType.NVarChar).Value = ob.FOcupDTL;
                cmd.Parameters.Add("@MOTHERNM", SqlDbType.NVarChar).Value = ob.StuMNM;
                cmd.Parameters.Add("@MOTHEROCP", SqlDbType.NVarChar).Value = ob.MOcup;
                cmd.Parameters.Add("@SPOUSENM", SqlDbType.NVarChar).Value = ob.SPuseNM;
                cmd.Parameters.Add("@SPOUSEOCP", SqlDbType.NVarChar).Value = ob.SpuseOcup;
                cmd.Parameters.Add("@ADDRPRE", SqlDbType.NVarChar).Value = ob.PreAdrs;
                cmd.Parameters.Add("@ADDRPER", SqlDbType.NVarChar).Value = ob.PerAdrs;
                cmd.Parameters.Add("@TELNO", SqlDbType.NVarChar).Value = ob.TelePhn;
                cmd.Parameters.Add("@MOBNO", SqlDbType.NVarChar).Value = ob.MobNO;
                cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = ob.Email;
                cmd.Parameters.Add("@NATIONALITY", SqlDbType.NVarChar).Value = ob.Nation;
                cmd.Parameters.Add("@RELIGION", SqlDbType.NVarChar).Value = ob.Religion;
                cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = ob.DtOfBrt;
                cmd.Parameters.Add("@GENDER", SqlDbType.NVarChar).Value = ob.Gander;
                cmd.Parameters.Add("@STUDENTTP", SqlDbType.NVarChar).Value = ob.StuTP;
                cmd.Parameters.Add("@BIRTHP", SqlDbType.NVarChar).Value = ob.PofB;
                cmd.Parameters.Add("@NIDPNO ", SqlDbType.NVarChar).Value = ob.NIDPNO;
                cmd.Parameters.Add("@BLOODGR", SqlDbType.NVarChar).Value = ob.BldGRP;

                cmd.Parameters.Add("@PRESIDENCE", SqlDbType.NVarChar).Value = ob.PRecdnc;
                cmd.Parameters.Add("@MSTATUS", SqlDbType.NVarChar).Value = ob.MSTTS;
                cmd.Parameters.Add("@RESHOSTEL", SqlDbType.NVarChar).Value = ob.Hstl;
                cmd.Parameters.Add("@INCOMEYY", SqlDbType.NVarChar).Value = ob.Incm;
                cmd.Parameters.Add("@EXPENSEYY", SqlDbType.NVarChar).Value = ob.Expncy;
                cmd.Parameters.Add("@GUARDIANNM", SqlDbType.NVarChar).Value = ob.GNM;
                cmd.Parameters.Add("@GRELATION", SqlDbType.NVarChar).Value = ob.GRel;
                cmd.Parameters.Add("@GADDRESS", SqlDbType.NVarChar).Value = ob.GAdrs;
                cmd.Parameters.Add("@GTELNO", SqlDbType.NVarChar).Value = ob.GTelePhn;
                cmd.Parameters.Add("@GMOBNO", SqlDbType.NVarChar).Value = ob.GMNo;
                cmd.Parameters.Add("@GEMAIL", SqlDbType.NVarChar).Value = ob.GEml;
                cmd.Parameters.Add("@PREPROGTP", SqlDbType.NVarChar).Value = ob.PreProTP;
                cmd.Parameters.Add("@PREPROGNM", SqlDbType.NVarChar).Value = ob.PreProNM;
                cmd.Parameters.Add("@PPINSTITN", SqlDbType.NVarChar).Value = ob.PreInsNM;
                cmd.Parameters.Add("@PPSESSION", SqlDbType.NVarChar).Value = ob.PreSSN;
                cmd.Parameters.Add("@FIRMNM", SqlDbType.NVarChar).Value = ob.FIRMNM;
                cmd.Parameters.Add("@POSITION", SqlDbType.NVarChar).Value = ob.PosiSN;
                cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = ob.PosiSN;
                cmd.Parameters.Add("@USERID", SqlDbType.NVarChar).Value = ob.UserID;
                cmd.Parameters.Add("@USERPC", SqlDbType.NVarChar).Value = ob.PcName;
                cmd.Parameters.Add("@IPADDRESS", SqlDbType.NVarChar).Value = ob.Ipaddress;
                cmd.Parameters.Add("@INTIME", SqlDbType.SmallDateTime).Value = ob.InTime;
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                HttpContext.Current.Session["InsertApplicationReg"] = "True";
                tran.Commit();
                if (con.State != ConnectionState.Closed)
                    if (con.State != ConnectionState.Closed)con.Close();

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