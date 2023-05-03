<%@ Page Title="Commisssion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="commission.aspx.cs" Inherits="AlchemyAccounting.payroll.report.ui.commission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #header
        {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }
        #s_top
        {
            float: left;
            width: 60%;
            margin: 2% 20% 0% 20%;
            border: 1px solid #cccccc;
            border-radius: 10px;
        }
        .txtColor:focus
        {
            border: solid 4px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
        }
        .txtalign
        {
            text-align: center;
        }
        .completionList
        {
            width: 300px !important;
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 200px;
            overflow: auto;
            background-color: #FFFFFF;
        }
        
        .listItem
        {
            color: #1C1C1C;
        }
        
        .itemHighlighted
        {
            background-color: orange;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center" style="font-weight: bold;">
            Commission</h1>
    </div>
    <table id="s_top">
        <tr>
            <td style="text-align: right; width: 30%; font-weight: bold;">
                Site Name</td>
            <td style="text-align: center; width: 1%; font-weight: bold;">
                :
            </td>
            <td style="text-align: left; width: 40%; font-weight: bold;">
                <asp:TextBox ID="txtSite" runat="server" AutoPostBack="True" TabIndex="1" Width="100%"
                    CssClass="txtColor" OnTextChanged="txtSite_TextChanged"></asp:TextBox>
                <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                    Enabled="True" ServicePath="" TargetControlID="txtSite" MinimumPrefixLength="1"
                    CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                    ServiceMethod="GetCompletionListSite" CompletionListCssClass="completionList"
                    CompletionListItemCssClass="listItem" 
                    CompletionListHighlightedItemCssClass="itemHighlighted">
                </asp:AutoCompleteExtender>
            </td>
            <td style="text-align: left; width: 29%; font-weight: bold;">
                <asp:Label ID="lblSiteID" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 30%; font-weight: bold;">
                &nbsp;
            </td>
            <td style="text-align: center; width: 1%; font-weight: bold;">
                &nbsp;
            </td>
            <td style="text-align: left; width: 40%; font-weight: bold;">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Bold="True" Font-Italic="False"
                    Width="150px" OnClick="btnSearch_Click" TabIndex="4" CssClass="txtColor txtalign" />
            </td>
            <td style="text-align: left; width: 29%; font-weight: bold;">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 30%; font-weight: bold;">
                &nbsp;
            </td>
            <td style="text-align: center; width: 1%; font-weight: bold;">
                &nbsp;
            </td>
            <td style="text-align: left; width: 40%; font-weight: bold;">
                <asp:Label ID="lblErrMsg" runat="server" ForeColor="#990000" Visible="False" Font-Bold="True"></asp:Label>
            </td>
            <td style="text-align: left; width: 29%; font-weight: bold;">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
