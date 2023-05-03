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
    public partial class admitCardPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admit();
        }
        private void Admit()
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            string id = Session["PROGRAMID"].ToString();
            string tp = Session["TP"].ToString(); 
             string SemID = Session["SEMESTERID"].ToString();
            string Script = "";
            if (tp == "2")
            {
                string STid = Session["STUDENTID"].ToString();
                Script = " AND EIM_STUDENT.NEWSTUDENTID='" + STid + "'";
            }

            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT    distinct    EIM_STUDENT.STUDENTID, EIM_STUDENT.STUDENTNM, EIM_STUDENT.NEWSTUDENTID, EIM_STUDENT.BATCH, EIM_STUDENT.SESSION, EIM_PROGRAM.PROGRAMNM, EIM_STUDENT.IMAGE
FROM            EIM_STUDENT INNER JOIN
                         EIM_PROGRAM ON EIM_STUDENT.PROGRAMID = EIM_PROGRAM.PROGRAMID INNER JOIN
                         EIM_COURSEREG ON EIM_STUDENT.PROGRAMID = EIM_COURSEREG.PROGRAMID AND EIM_STUDENT.STUDENTID = EIM_COURSEREG.STUDENTID
WHERE EIM_PROGRAM.PROGRAMID='" + id + "' AND   EIM_COURSEREG.SEMID='" + SemID + "'" + Script + "", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (conn.State != ConnectionState.Closed)conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater1.Visible = true;
            }
            else
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater1.Visible = true;
            }
        }

        protected void RepeatR_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater repeater = (Repeater)(e.Item.FindControl("repeater"));
            Label lblStuID = (Label)e.Item.FindControl("lblStuID");
            GridView gv = (GridView)(e.Item.FindControl("gv_Course"));
            DataTable dt = loadData(lblStuID.Text);
            gv.DataSource = dt;
            gv.DataBind();


        }
        private DataTable loadData(string stuID)
        {
           
            string SemID = Session["SEMESTERID"].ToString();
            string id = Session["PROGRAMID"].ToString();
            string tp = Session["TP"].ToString();  
            SqlConnection conn = new SqlConnection(Global.connection);
            //ds = (DataSet)ViewState["currentOrders"];
            DataTable dtGridTable = new DataTable();
             
            if (conn.State != ConnectionState.Open)conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT  distinct EIM_COURSE.COURSECD, EIM_COURSE.COURSENM, EIM_COURSE.COURSEID, EIM_COURSEREG.REMARKS
FROM            EIM_COURSEREG INNER JOIN EIM_COURSE ON EIM_COURSEREG.COURSEID = EIM_COURSE.COURSEID INNER JOIN
                         EIM_STUDENT ON EIM_COURSEREG.STUDENTID = EIM_STUDENT.STUDENTID WHERE EIM_COURSEREG.STUDENTID='" + stuID + "' AND " +
            "EIM_COURSEREG.PROGRAMID='" + id + "' AND EIM_COURSEREG.SEMID='" + SemID + "'", conn);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtGridTable);
            if (conn.State != ConnectionState.Closed)conn.Close();
            return dtGridTable;
        }
    }
}