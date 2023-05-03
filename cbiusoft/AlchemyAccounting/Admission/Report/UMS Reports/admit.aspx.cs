using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Admission.Report.UMS_Reports
{
    public partial class admit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Global.BindDropDown(ddlProNM, @"SELECT    DISTINCT    EIM_PROGRAM.PROGRAMNM NM ,EIM_PROGRAM.PROGRAMID ID
                FROM EIM_PROGRAM INNER JOIN EIM_COURSEREG ON EIM_PROGRAM.PROGRAMID = EIM_COURSEREG.PROGRAMID 
                ORDER BY EIM_PROGRAM.PROGRAMNM");
                Global.BindDropDown(ddlProNM2, @"SELECT    DISTINCT    EIM_PROGRAM.PROGRAMNM NM ,EIM_PROGRAM.PROGRAMID ID
                FROM EIM_PROGRAM INNER JOIN EIM_COURSEREG ON EIM_PROGRAM.PROGRAMID = EIM_COURSEREG.PROGRAMID 
                ORDER BY EIM_PROGRAM.PROGRAMNM");
//                Global.BindDropDownNM(ddlStudent, @"SELECT DISTINCT EIM_STUDENT.NEWSTUDENTID NM
//                FROM EIM_COURSEREG INNER JOIN EIM_STUDENT ON EIM_COURSEREG.PROGRAMID = EIM_STUDENT.PROGRAMID AND
//                EIM_COURSEREG.STUDENTID = EIM_STUDENT.STUDENTID ORDER BY EIM_STUDENT.NEWSTUDENTID");
                Global.BindDropDownNM(ddlSemNM, @"SELECT SEMESTERNM NM FROM EIM_SEMESTER ORDER BY SEMESTERNM");
                Global.BindDropDownNM(ddlSemNM2, @"SELECT SEMESTERNM NM FROM EIM_SEMESTER ORDER BY SEMESTERNM");
            }
        }

        protected void btnSUBMIT_Click(object sender, EventArgs e)
        {

        }

       
        protected void btnAdmit_Click(object sender, EventArgs e)
        {
            Session["TP"] = "";
            Session["PROGRAMNM"] = "";
            Session["SEMESTERNM"] = "";
            Session["PROGRAMID"] = "";
            Session["SEMESTERID"] = ""; 
            Session["YEAR"] = "";
            Session["SEMNM"] = ddlSemNM.Text;
            Session["TP"] = "1";
            Session["PROGRAMID"] = ddlProNM.Text;
            Session["SEMESTERID"] = ddlSemester.Text;
            Session["PROGRAMNM"] = ddlProNM.SelectedItem.ToString();
            Session["SEMESTERNM"] = ddlSemester.SelectedItem.ToString(); 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('../admitCardPrint.aspx','_newtab');", true);
        }

        protected void btnStuAdmit_Click(object sender, EventArgs e)
        {
            Session["TP"] = "";
            Session["SEMESTERNM"] = ""; 
            Session["SEMESTERID"] = ""; 
            Session["PROGRAMNM"] = "";
            Session["PROGRAMID"] = "";
            Session["YEAR"] = "";
            Session["TP"] = "2";
            Session["SEMNM"] = ddlSemNM2.Text;
            Session["PROGRAMID"] = ddlProNM2.Text;
            Session["PROGRAMNM"] = ddlProNM2.SelectedItem.ToString();
            Session["SEMESTERID"] = ddlSemester2.Text;
            Session["SEMESTERNM"] = ddlSemester2.SelectedItem.ToString(); 
            Session["STUDENTID"] = ddlStudent.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", "window.open('../admitCardPrint.aspx','_newtab');", true);
        }

      
        protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemester.Text != "--SELECT--")
            {
                Global.BindDropDown(ddlProNM, @"SELECT DISTINCT  EIM_PROGRAM.PROGRAMNM NM, EIM_PROGRAM.PROGRAMID ID
FROM            EIM_PROGRAM INNER JOIN EIM_COURSEREG ON EIM_PROGRAM.PROGRAMID = EIM_COURSEREG.PROGRAMID WHERE EIM_COURSEREG.SEMID='" + ddlSemester.Text + "' ");
                ddlProNM.Focus();
            }
        }

        protected void ddlProNM2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProNM2.Text != "--SELECT--")
            {
                ddlSemNM2.Focus();
            }
        }

        protected void ddlSemester2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSemNM2.Text != "00")
            {
                Global.BindDropDownNM(ddlStudent, @" SELECT    DISTINCT    EIM_STUDENT.NEWSTUDENTID NM
FROM            EIM_COURSEREG INNER JOIN EIM_STUDENT ON EIM_COURSEREG.PROGRAMID = EIM_STUDENT.PROGRAMID AND 
EIM_COURSEREG.STUDENTID = EIM_STUDENT.STUDENTID WHERE EIM_COURSEREG.SEMID='" + ddlSemester2.Text + "' AND  EIM_COURSEREG.PROGRAMID='"+ddlProNM2.Text+"'");
                btnStuAdmit.Focus();
            }
        }  
         
    }
}