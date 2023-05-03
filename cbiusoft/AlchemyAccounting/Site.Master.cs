using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    { 
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Default.aspx");
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("/cbiu/signin.aspx");
        }

        protected void btnsessionTime_Click(object sender, EventArgs e)
        {
            Session["UserName"] = Session["UserName"].ToString();
            Session["PCName"] = Session["PCName"].ToString();
            Session["IpAddress"] = Session["IpAddress"].ToString();
            Session["PCName"] = Session["PCName"].ToString();
            Session["UserTp"] = Session["UserTp"].ToString();  
        }
    }
}
