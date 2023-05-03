<%@ Page Title="Holiday" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="payroll-holiday.aspx.cs" Inherits="AlchemyAccounting.payroll.ui.payroll_holiday" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link rel="shortcut icon" href="../../Images/favicon.ico" />

    <style type="text/css">
        #header
        {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }
        #header h1
        {
            font-family: Century Gothic;
            font-weight: bold;
        }
        #entry
        {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            margin-top: 10px;
            margin-bottom: 30px;
            border-radius: 10px;
            text-align: left;
        }
        .Gridview
        {
            font-family: Verdana;
            font-size: 10pt;
            font-weight: normal;
            color: black;
            margin-right: 0px;
            text-align: left;
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
        .def
        {
            float: left;
            width: 100%;
        }
        #toolbar
        {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        .ui-accordion
        {
            text-align: left;
        }
        .txtalign
        {
            text-align: center;
        }
        .passport
        {
            float: left;
            width: 100%;
            height: 250px;
        }
        .sign
        {
            float: left;
            width: 100%;
            height: 150px;
            margin-top: 10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Holiday</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%; border: 1px solid #000;">
                <tr>
                    <td style="text-align: right; width: 50%">
                        <asp:Button ID="btnEdit" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                            Text="Edit" Width="80px" OnClick="btnEdit_Click" />
                    </td>
                    <td style="width: 50%">
                        <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="15px" Text="Refresh" Width="80px" OnClick="btnRefresh_Click" />
                        <asp:Label ID="lblMemID" runat="server" Visible="False"></asp:Label>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 79%; margin: 1% 1% 1% 0">
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Holiday Date</td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtHolidayDate" runat="server" CssClass="txtColor" 
                            TabIndex="1" Width="15%"
                            AutoPostBack="True" OnTextChanged="txtHolidayDate_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtHolidayDate"
                            PopupButtonID="txtIDExpDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        <asp:Label ID="lblStatus" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Status</td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txtColor" 
                            TabIndex="2" Width="16%">
                            <asp:ListItem>FRIDAY</asp:ListItem>
                            <asp:ListItem>HOLIDAY</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Address
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtColor" TabIndex="3" 
                            Width="60%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:Button ID="btnSave" runat="server" TabIndex="4" Text="Save" Font-Bold="True"
                            Width="15%" OnClick="btnSave_Click" CssClass="txtColor txtalign" />
                        &nbsp;
                        <asp:Label ID="lblErrMsg" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="def" style="margin-bottom: 1%">
        </div>
    </div>
</asp:Content>
