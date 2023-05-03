using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.UI
{
    public partial class add_batch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Update_Click(object sender, EventArgs e)
        {
            //if (TextBox3.Text == "" || Convert.ToInt16(TextBox3.Text.Length) < 3 || Convert.ToInt16(TextBox3.Text.Length) > 3)
            //    TextBox3.Focus();
            //else
            //{
                string s = "";
                //for (int i = 1; i < 27; i++)
                //{
                    try
                    {
                        string x = "";
                        //if (i < 10)
                        //    x = "0" + i;
                        //else
                        //    x = i.ToString();
                        SqlConnection CONN = new SqlConnection(Global.connection);
                        CONN.Open();
                        SqlCommand cmd = new SqlCommand(@"SELECT SL,STUDENTID,ID FROM (
SELECT ROW_NUMBER() OVER(ORDER BY(STUDENTID)) AS SL,STUDENTID,SUBSTRING(ADMITYY,3,2)+BATCH+PROGRAMID ID FROM EIM_STUDENT  WHERE PROGRAMID='" + TextBox3.Text + "' and  admityy not between 2010 and 2013 " +
    ") X  ORDER BY STUDENTID", CONN);
                        cmd.CommandTimeout = 0;
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            string nid = "";
                            Int64 h = Convert.ToInt64(dr["SL"].ToString());
                            if (h < 10)
                                nid = "0000" + h;
                            else if (h < 100)
                                nid = "000" + h;
                            else if (h < 1000)
                                nid = "00" + h;
                            else if (h < 10000)
                                nid = "0" + h;
                            else if (h < 100000)
                                nid = h.ToString();
                            nid = dr["ID"].ToString() + nid;
                            Global.Execute("UPDATE EIM_STUDENT SET NEWSTUDENTID='" + nid + "' WHERE STUDENTID='" + dr["STUDENTID"].ToString() + "'");
                        }
                        dr.Close();
                        CONN.Close();
                    }
                    catch { }

                //}
                gRIDsHOW();
           // }
        }
        private void NEWID(string YY, string BATCH, string PROGRAMID, int INCREMENT)
        {

            string x = "";
            if (INCREMENT < 10)
                x = "0000" + INCREMENT;
            else if (INCREMENT < 100)
                x = "000" + INCREMENT;
            else if (INCREMENT < 100)
                x = "00" + INCREMENT;
            else if (INCREMENT < 100)
                x = "0" + INCREMENT;
            else if (INCREMENT < 100)
                x = INCREMENT.ToString();
            else if (INCREMENT < 100)
                x = "0000" + INCREMENT;
        }
        private void gRIDsHOW()
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmdd = new SqlCommand(@"SELECT ROW_NUMBER() OVER(ORDER BY(STUDENTID)) AS SL,dbo.EIM_STUDENT.STUDENTNM, dbo.EIM_STUDENT.STUDENTID, dbo.EIM_STUDENT.NEWSTUDENTID, dbo.EIM_STUDENT.BATCH, dbo.EIM_PROGRAM.PROGRAMSID
FROM            dbo.EIM_STUDENT INNER JOIN dbo.EIM_PROGRAM ON dbo.EIM_STUDENT.PROGRAMID = dbo.EIM_PROGRAM.PROGRAMID WHERE EIM_STUDENT.programid='05'  order by EIM_STUDENT.programid", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }
        private void gRIDsHOWAll()
        {

            SqlConnection conn = new SqlConnection(Global.connection);
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmdd = new SqlCommand(@"SELECT ROW_NUMBER() OVER(ORDER BY(STUDENTID)) AS SL,dbo.EIM_STUDENT.STUDENTNM, dbo.EIM_STUDENT.STUDENTID, dbo.EIM_STUDENT.NEWSTUDENTID, dbo.EIM_STUDENT.BATCH, dbo.EIM_PROGRAM.PROGRAMSID
FROM            dbo.EIM_STUDENT INNER JOIN dbo.EIM_PROGRAM ON dbo.EIM_STUDENT.PROGRAMID = dbo.EIM_PROGRAM.PROGRAMID WHERE EIM_STUDENT.PROGRAMID='" + TextBox3.Text + "' AND EIM_STUDENT.ADMITYY='" + DropDownList1.Text + "' AND EIM_STUDENT.SEMESTERID='" + DropDownList2.SelectedValue.ToString() + "' order by EIM_STUDENT.programid", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }

        protected void all_Click(object sender, EventArgs e)
        {
            gRIDsHOWAll();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox3.Text = Global.GetData("SELECT BATCH FROM EIM_STUDENT WHERE ADMITYY='" + DropDownList1.Text + "' AND SEMESTERID='" + DropDownList2.SelectedValue.ToString() + "'");
        }
    }
}