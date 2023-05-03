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
    public partial class emp_process : System.Web.UI.Page
    {
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
                    DateTime td = DateTime.Now;
                    string mon = td.ToString("MMM").ToUpper();
                    string year = td.ToString("yy");
                    txtMonth.Text = mon + "-" + year;
                    txtDate.Text = td.ToString("dd/MM/yyyy");
                    txtMonth.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListMonth(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT TRANSMY FROM HR_SALGRANT WHERE TRANSMY LIKE '" + prefixText + "%' ", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["TRANSMY"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtMonth_TextChanged(object sender, EventArgs e)
        {
            if (txtMonth.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Month can't empty.";
                txtMonth.Focus();
            }
            else
            {
                txtDate.Text = "";
                Show_Grid_Employee();
                txtDate.Focus();
            }
        }

        public void Show_Grid_Employee()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT EMPID, BASICSAL, FOODS FROM HR_EMP", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvShowEmp.DataSource = ds;
                gvShowEmp.DataBind();
                gvShowEmp.Visible = false;
            }
            else
            {
                gvShowEmp.DataSource = ds;
                gvShowEmp.DataBind();
                gvShowEmp.Visible = false;
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (txtDate.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Date can't empty.";
                txtDate.Focus();
            }
            else
            {
                btnProcess.Focus();
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (txtMonth.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Month can't empty.";
                    txtMonth.Focus();
                }
                else if (txtDate.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Date can't empty.";
                    txtDate.Focus();
                }
                else
                {
                    lblError.Visible = false;

                    Show_Grid_Employee();

                    SqlConnection conn = new SqlConnection(Global.connection);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(" SELECT * FROM HR_SALGRANT WHERE TRANSMY ='" + txtMonth.Text + "'", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblUser.Text = "This process is previously done. Are you sure to repeat?";
                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        conn.Open();
                        SqlCommand cmdd = new SqlCommand("Delete from HR_SALGRANT where TRANSMY='" + txtMonth.Text + "'", conn);
                        cmdd.ExecuteNonQuery();
                        conn.Close();

                        Employee_Salary_Process();

                        lblError.Visible = true;
                        lblError.Text = "Process Completed.";
                        txtMonth.Focus();
                    }
                }
            }
        }

        protected void btnYes_Click(object sender, ImageClickEventArgs e)
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            conn.Open();
            SqlCommand cmdd = new SqlCommand("Delete from HR_SALGRANT where TRANSMY='" + txtMonth.Text + "'", conn);
            cmdd.ExecuteNonQuery();
            conn.Close();

            Employee_Salary_Process();

            lblError.Visible = true;
            lblError.Text = "Process Completed.";
            txtMonth.Focus();
        }

        public void Employee_Salary_Process()
        {
            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gvShowEmp.Rows)
            {
                try
                {
                    iob.TransMY = txtMonth.Text;
                    iob.TransDT = Transdt;   //// effect date
                    iob.EmpID = grid.Cells[0].Text;
                    lblMMDays.Text = "";
                    Global.lblAdd("SELECT DAYS AS MMDAYS FROM HR_MONTH WHERE MONTH='" + iob.TransMY + "'", lblMMDays);
                    if (lblMMDays.Text == "")
                        lblMMDays.Text = "0";
                    iob.MmDays = Convert.ToInt16(lblMMDays.Text);
                    lblNMDays.Text = "";
                    Global.lblAdd("SELECT COUNT(*) AS NMDAYS FROM HR_HOUR WHERE EMPID ='" + iob.EmpID + "' AND TRANSMY='" + iob.TransMY + "' AND NORMALHR <>0", lblNMDays);
                    if (lblNMDays.Text == "")
                        lblNMDays.Text = "0";
                    iob.NmDays = Convert.ToInt16(lblNMDays.Text);
                    lblOTDays.Text = "";
                    Global.lblAdd("SELECT COUNT(*) AS OTDAYS FROM HR_HOUR WHERE EMPID ='" + iob.EmpID + "' AND TRANSMY='" + iob.TransMY + "' AND FRIDAYOT <>0 OR HOLIDAYOT<>0", lblOTDays);
                    if (lblOTDays.Text == "")
                        lblOTDays.Text = "0";
                    iob.OtDays = Convert.ToInt16(lblOTDays.Text);
                    iob.BasicSal = Convert.ToDecimal(grid.Cells[1].Text);
                    iob.RatePD = iob.BasicSal / iob.MmDays;
                    iob.RatePH = iob.RatePD / 8;
                    lblOTHour.Text = "";
                    Global.lblAdd("SELECT (SUM(NORMALHR) + SUM(NORMALOT)+SUM(FRIDAYOT)+SUM(HOLIDAYOT))AS TOTHOUR FROM HR_HOUR WHERE EMPID ='" + iob.EmpID + "' AND TRANSMY='" + iob.TransMY + "'", lblOTHour);
                    if (lblOTHour.Text == "")
                        lblOTHour.Text = "0";
                    iob.OtHour = Convert.ToInt16(lblOTHour.Text);
                    iob.OtAmt = iob.OtHour * iob.RatePH;
                    iob.Foods = Convert.ToDecimal(grid.Cells[2].Text);
                    lblBonus.Text = "";
                    Global.lblAdd("SELECT BONUS FROM HR_SALDRCR WHERE EMPID ='" + iob.EmpID + "' AND TRANSMY='" + iob.TransMY + "'", lblBonus);
                    if (lblBonus.Text == "")
                        lblBonus.Text = "0";
                    iob.Bouns = Convert.ToDecimal(lblBonus.Text);
                    lblOTCAdd.Text = "";
                    Global.lblAdd("SELECT OTCADD FROM HR_SALDRCR WHERE EMPID ='" + iob.EmpID + "' AND TRANSMY='" + iob.TransMY + "'", lblOTCAdd);
                    if (lblOTCAdd.Text == "")
                        lblOTCAdd.Text = "0";
                    iob.OtcAdd = Convert.ToDecimal(lblOTCAdd.Text);
                    iob.GrossAmt = iob.OtAmt + iob.Foods + iob.Bouns + iob.OtcAdd;
                    lblAdvance.Text = "";
                    Global.lblAdd("SELECT ADVANCE FROM HR_SALDRCR WHERE EMPID ='" + iob.EmpID + "' AND TRANSMY='" + iob.TransMY + "'", lblAdvance);
                    if (lblAdvance.Text == "")
                        lblAdvance.Text = "0";
                    iob.Advance = Convert.ToDecimal(lblAdvance.Text);
                    lblPenalty.Text = "";
                    Global.lblAdd("SELECT PENALTY FROM HR_SALDRCR WHERE EMPID ='" + iob.EmpID + "' AND TRANSMY='" + iob.TransMY + "'", lblPenalty);
                    if (lblPenalty.Text == "")
                        lblPenalty.Text = "0";
                    iob.Penalty = Convert.ToDecimal(lblPenalty.Text);
                    lblOTCDed.Text = "";
                    Global.lblAdd("SELECT OTCDED FROM HR_SALDRCR WHERE EMPID ='" + iob.EmpID + "' AND TRANSMY='" + iob.TransMY + "'", lblOTCDed);
                    if (lblOTCDed.Text == "")
                        lblOTCDed.Text = "0";
                    iob.OtcDED = Convert.ToDecimal(lblOTCDed.Text);
                    decimal grossDed = iob.Advance + iob.Penalty + iob.OtcDED;
                    iob.NetAmt = iob.GrossAmt - grossDed;

                    iob.InTm = DateTime.Now;
                    iob.UserNm = HttpContext.Current.Session["UserName"].ToString();
                    iob.Ip = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.UserPc = HttpContext.Current.Session["PCName"].ToString();

                    dob.payroll_Employee_Salary_Process(iob);

                    lblError.Visible = true;
                    lblError.Text = "Proccess Completed.";
                    txtMonth.Focus();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
    }
}