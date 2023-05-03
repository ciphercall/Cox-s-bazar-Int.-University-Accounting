<%@ Page Title="Log on History" Language="C#" MasterPageFile="~/Web_2.Master" AutoEventWireup="true" CodeBehind="LogOnHistory.aspx.cs" Inherits="SellingSystem.Login.Reports.ReportUI.LogOnHistory" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" 
        ScrollBars="Both" Width="100%" Height="631px">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" Height="100%" ToolPanelView="None" 
            Width="100%" BestFitPage="False" />
    </asp:Panel>
</asp:Content>
