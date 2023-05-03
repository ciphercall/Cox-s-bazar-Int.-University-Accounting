<%@ Application Codebehind="Global1.asax.cs" Inherits="AlchemyAccounting.Global1" Language="C#" %>

<%@ Import Namespace="System.Web.Routing" %>
<script runat="server">
    void Application_Start(object sender, EventArgs e) 
    {
        RegisterRoutes(RouteTable.Routes);
    }
    
    static void RegisterRoutes(RouteCollection routes)
    {
        routes.MapPageRoute("<a href="Admission/UI/Course_reg.aspx">Admission/UI/Course_reg.aspx</a>");
        
    }
</script>
