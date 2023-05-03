<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="employee-payment.aspx.cs" Inherits="AlchemyAccounting.payroll.report.ui.employee_payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="shortcut icon" href="../../../Images/favicon.ico" />
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
            Employee Payment</h1>
    </div>
    <table id="s_top">
        <tr>
            <td style="text-align: right; width: 30%; font-weight: bold;">
                Month
            </td>
            <td style="text-align: center; width: 1%; font-weight: bold;">
                :
            </td>
            <td style="text-align: left; width: 40%; font-weight: bold;">
                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" CssClass="txtColor"
                    TabIndex="1" Width="82%" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                    <asp:ListItem>JANUARY</asp:ListItem>
                    <asp:ListItem>FEBRUARY</asp:ListItem>
                    <asp:ListItem>MARCH</asp:ListItem>
                    <asp:ListItem>APRIL</asp:ListItem>
                    <asp:ListItem>MAY</asp:ListItem>
                    <asp:ListItem>JUNE</asp:ListItem>
                    <asp:ListItem>JULY</asp:ListItem>
                    <asp:ListItem>AUGUST</asp:ListItem>
                    <asp:ListItem>SEPTEMBER</asp:ListItem>
                    <asp:ListItem>OCTOBER</asp:ListItem>
                    <asp:ListItem>NOVEMBER</asp:ListItem>
                    <asp:ListItem>DECEMBER</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="text-align: left; width: 29%; font-weight: bold;">
                <asp:Label ID="lblMy" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 30%; font-weight: bold;">
                Emloyee Name
            </td>
            <td style="text-align: center; width: 1%; font-weight: bold;">
                :
            </td>
            <td style="text-align: left; width: 40%; font-weight: bold;">
                <asp:TextBox ID="txtEmp" runat="server" AutoPostBack="True" TabIndex="2" Width="80%"
                    CssClass="txtColor" OnTextChanged="txtEmp_TextChanged"></asp:TextBox>
                <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                    Enabled="True" ServicePath="" TargetControlID="txtEmp" MinimumPrefixLength="1"
                    CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                    ServiceMethod="GetCompletionListEmployeeInfo" CompletionListCssClass="completionList"
                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                </asp:AutoCompleteExtender>
            </td>
            <td style="text-align: left; width: 29%; font-weight: bold;">
                <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblQID" runat="server" Visible="False"></asp:Label>
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
