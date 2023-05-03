using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.Report
{
    public partial class dept_info : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection conn = new SqlConnection(Global.connection);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gridShow(); 
            }
            catch (Exception eX)
            {
                Response.Write(eX);
            }

        }
        private void gridShow()
        {
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT        DEPTNM, REMARKS
FROM            HR_DEPT order by DEPTNM", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_post.DataSource = ds;
                gv_post.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gv_post.DataSource = ds;
                gv_post.DataBind();
                int columncount = gv_post.Rows[0].Cells.Count;
                gv_post.Rows[0].Cells.Clear();
                gv_post.Rows[0].Cells.Add(new TableCell());
                gv_post.Rows[0].Cells[0].ColumnSpan = columncount;
                gv_post.Rows[0].Cells[0].Text = "No Records Found";

            }
        }
    }
}