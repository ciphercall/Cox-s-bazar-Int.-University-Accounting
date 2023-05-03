using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.Admission.Report
{
    public partial class StudentLedgerReport : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        int count = 0;
        decimal TotalBlnc = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GridShow();
        }
        protected void GridShow()
        {
            Label oldID = new Label();
            lblStuID.Text = Session["STUID"].ToString();
            string Date = Session["LEDGERDT"].ToString();
            DateTime FRDT = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string DT = FRDT.ToString("yyyy/MM/dd");
            SqlConnection conn = new SqlConnection(Global.connection);
            if (conn.State != ConnectionState.Open)conn.Open();
            string script = "";
            script = @"SELECT        dbo.EIM_STUDENT.BATCH,dbo.EIM_STUDENT.STUDENTNM,dbo.EIM_STUDENT.STUDENTID, dbo.EIM_PROGRAM.PROGRAMNM, dbo.EIM_STUDENT.ADMITYY
FROM            dbo.EIM_STUDENT INNER JOIN dbo.EIM_PROGRAM ON dbo.EIM_STUDENT.PROGRAMID = dbo.EIM_PROGRAM.PROGRAMID " +
"WHERE        (dbo.EIM_STUDENT.NEWSTUDENTID = '" + lblStuID.Text + "') OR  (dbo.EIM_STUDENT.NEWMIGRATESID = '" + lblStuID.Text + "')";
            SqlCommand cmd;
            cmd = new SqlCommand(script, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblStuNM.Text = dr["STUDENTNM"].ToString();
                lblProgram.Text = dr["PROGRAMNM"].ToString();
                lblYR.Text = dr["ADMITYY"].ToString();
                oldID.Text = dr["STUDENTID"].ToString();
                lblBatch1.Text = dr["BATCH"].ToString();
            }
            dr.Close();
            script = "";
            script = @"SELECT convert(nvarchar(10),TRANSDT,103) as TRANSDT, TRANSNO,SEMESTERNM, TRANSYY, FEESNM, REMARKS, SUM(DRAMT) DRAMT, SUM(CRAMT) CRAMT
            FROM
            (SELECT EIM_TRANS.TRANSDT, EIM_TRANS.TRANSNO,EIM_SEMESTER.SEMESTERNM,  EIM_TRANS.TRANSYY, EIM_TRANS.FEESID, FEESNM, EIM_TRANS.REMARKS, ISNULL(AMOUNT,0) DRAMT, 0 CRAMT
            FROM EIM_TRANS
			INNER JOIN EIM_STUDENT AS A ON EIM_TRANS.STUDENTID = A.STUDENTID OR EIM_TRANS.STUDENTID = A.MIGRATESID 
            INNER JOIN EIM_FEES ON EIM_FEES.FEESID = EIM_TRANS.FEESID 
			INNER JOIN EIM_SEMESTER ON  EIM_TRANS.SEMESTERID=EIM_SEMESTER.SEMESTERID  " +
            "WHERE A.STUDENTID = '" + oldID.Text + "' AND EIM_TRANS.TRANSDT <= '" + DT + "' AND EIM_TRANS.TRANSTP = 'JOUR' AND TRANSFOR='RECEIVABLE'  " +
            "UNION   " +
            "SELECT EIM_TRANS.TRANSDT, EIM_TRANS.TRANSNO,EIM_SEMESTER.SEMESTERNM,  EIM_TRANS.TRANSYY, EIM_TRANS.FEESID, FEESNM, EIM_TRANS.REMARKS,  0  DRAMT, ISNULL(AMOUNT,0)CRAMT   " +
            "FROM EIM_TRANS   " +
            "INNER JOIN EIM_STUDENT AS A ON EIM_TRANS.STUDENTID = A.STUDENTID OR EIM_TRANS.STUDENTID = A.MIGRATESID  " +
            "INNER JOIN EIM_FEES ON EIM_FEES.FEESID = EIM_TRANS.FEESID INNER JOIN EIM_SEMESTER  " +
            "ON  EIM_TRANS.SEMESTERID=EIM_SEMESTER.SEMESTERID  " +
            "WHERE A.STUDENTID = '" + oldID.Text + "'  AND EIM_TRANS.TRANSDT <= '" + DT + "' AND EIM_TRANS.TRANSTP = 'JOUR' AND TRANSFOR='PAYABLE'   " +
            "UNION   " +
            "SELECT EIM_TRANS.TRANSDT, EIM_TRANS.TRANSNO,EIM_SEMESTER.SEMESTERNM, EIM_TRANS.TRANSYY, EIM_TRANS.FEESID, FEESNM, EIM_TRANSMST.REMARKS, 0 DRAMT, ISNULL(AMOUNT,0) CRAMT   " +
            "FROM EIM_TRANS  " +
            "INNER JOIN EIM_STUDENT AS A ON EIM_TRANS.STUDENTID = A.STUDENTID OR EIM_TRANS.STUDENTID = A.MIGRATESID  " +
            "INNER JOIN EIM_TRANSMST ON EIM_TRANSMST.TRANSDT = EIM_TRANS.TRANSDT AND EIM_TRANSMST.TRANSNO = EIM_TRANS.TRANSNO AND EIM_TRANSMST.TRANSTP = EIM_TRANS.TRANSTP   " +
            "INNER JOIN EIM_FEES ON EIM_FEES.FEESID = EIM_TRANS.FEESID INNER JOIN EIM_SEMESTER ON  EIM_TRANS.SEMESTERID=EIM_SEMESTER.SEMESTERID    " +
            "WHERE A.STUDENTID = '" + oldID.Text + "' AND EIM_TRANS.TRANSDT <= '" + DT + "' AND EIM_TRANS.TRANSTP = 'MREC'  ) A  " +
            "GROUP BY TRANSDT, TRANSNO, SEMESTERNM, TRANSYY, FEESNM, REMARKS";
            cmd = new SqlCommand(script, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gv_StuLedger.DataSource = ds;
                Gv_StuLedger.DataBind();
            }
            else
            {

                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                Gv_StuLedger.DataSource = ds;
                Gv_StuLedger.DataBind();
                int columncount = Gv_StuLedger.Rows[0].Cells.Count;
                Gv_StuLedger.Rows[0].Cells.Clear();
                Gv_StuLedger.Rows[0].Cells.Add(new TableCell());
                Gv_StuLedger.Rows[0].Cells[0].ColumnSpan = columncount;
                Gv_StuLedger.Rows[0].Cells[0].Text = "No Records Found"; 

            }
        }
        protected void Gv_StuLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    string TRANSDT = DataBinder.Eval(e.Row.DataItem, "TRANSDT").ToString();
                    e.Row.Cells[0].Text = "&nbsp;" + TRANSDT;
                    string TRANSNO = DataBinder.Eval(e.Row.DataItem, "TRANSNO").ToString();
                    e.Row.Cells[1].Text = "&nbsp;" + TRANSNO;
                    string SEMESTERNM = DataBinder.Eval(e.Row.DataItem, "SEMESTERNM").ToString();
                    e.Row.Cells[2].Text = "&nbsp;" + SEMESTERNM;
                    string TRANSYY = DataBinder.Eval(e.Row.DataItem, "TRANSYY").ToString();
                    e.Row.Cells[3].Text = "&nbsp;" + TRANSYY;
                    string FEESNM = DataBinder.Eval(e.Row.DataItem, "FEESNM").ToString();
                    e.Row.Cells[4].Text = "&nbsp;" + FEESNM;
                    string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                    e.Row.Cells[5].Text = "&nbsp;" + REMARKS;
                    decimal DRAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DRAMT").ToString());
                    e.Row.Cells[6].Text = "&nbsp;" + DRAMT;
                    decimal CRAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CRAMT").ToString());
                    e.Row.Cells[7].Text = "&nbsp;" + CRAMT;
                    if (count == 0)
                        TotalBlnc = DRAMT - CRAMT;
                    else
                        TotalBlnc = (TotalBlnc + DRAMT) - CRAMT;
                    e.Row.Cells[8].Text = TotalBlnc.ToString();
                    count++;
                }
            }
            catch { }
        }
    }
}