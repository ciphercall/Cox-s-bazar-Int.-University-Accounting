using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace SellingSystem.Login.Reports.ReportUI
{
    public partial class LogOnHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Global.connection);
            SqlCommand cmd = new SqlCommand("", con);
            DataTable table = new DataTable();
            try
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * From UserLogOnHistory";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                con.Close();
                //DataTable tbllogin = new DataTable();
                SellingSystem.Login.Reports.CrystalReport.LogOnHistory login = new CrystalReport.LogOnHistory();
                login.SetDataSource(table);

                CrystalReportViewer1.ReportSource = login;
                CrystalReportViewer1.RefreshReport();
            }
            catch (Exception re)
            {
                Page.Response.Write(re.Message);
            }
        }
    }
}