<%@ Page Title="Salary Sheet" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="salary-sheet.aspx.cs" Inherits="AlchemyAccounting.payroll.report.ui.salary_sheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="shortcut icon" href="../../../Images/favicon.ico" />

<style type="text/css">
        #header
        {
            float: left;
            width:100%;
            background-color: transparent;
            height: 50px;
        }
        #s_top
        {
            float:left;
            width:60%;
            margin: 2% 20% 0% 20%;
            border: 1px solid #cccccc;
            border-radius: 10px;
        }
        .style1
        {
            width: 147px;
            text-align: right;
        }
        .style2
        {
            width: 2px;
            font-weight: bold;
        }
        .style3
        {
            width: 242px;
            text-align: left;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
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
        <h1 align="center" style="font-weight:bold;font-family:Century Gothic;">Balance 
            Sheet</h1>
    </div>

    <div>
        
        <table id="s_top">
            <tr>
                <td class="style1" style="text-align: right; font-weight: 700">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>Month</strong></td>
                <td class="style2">
                    :</td>
                <td class="style3">
                    <asp:TextBox ID="txtMonth" runat="server" Width="150px" AutoPostBack="True" TabIndex="1"
                        CssClass="txtColor" OnTextChanged="txtMonth_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtMonth_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                        Enabled="True" ServicePath="" TargetControlID="txtMonth" MinimumPrefixLength="1"
                        CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                        ServiceMethod="GetCompletionListMonth" CompletionListCssClass="completionList"
                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                    </asp:AutoCompleteExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td style="text-align: right">
                    <asp:Button ID="btnSearch" runat="server" Font-Bold="True" Text="Search" 
                        Width="130px" onclick="btnSearch_Click" CssClass="txtColor" />
                </td>
                <td>
                    <asp:Label ID="lblError" runat="server" ForeColor="#CC0000" Visible="False" Style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>
