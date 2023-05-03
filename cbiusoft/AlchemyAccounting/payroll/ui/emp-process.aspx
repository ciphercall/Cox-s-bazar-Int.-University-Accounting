<%@ Page Title="Employee Process" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="emp-process.aspx.cs" Inherits="AlchemyAccounting.payroll.ui.emp_process" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <script type="text/javascript">
        function closepopup() {
            $find('ModalPopupExtender1').hide();
        }
    </script>
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
            border-radius: 10px;
            margin-top: 10px;
            margin-bottom: 30px;
        }
        #main_cont
        {
            float: left;
            width: 50%;
            height: 200px;
            background: #f2f2f2;
            border: 2px solid #cccccc;
            border-radius: 10px;
            margin: 0% 25% 0% 25%;
        }
        
        .style1
        {
            width: 127px;
            text-align: right;
        }
        .style2
        {
            width: 166px;
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
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Employee Process</h1>
    </div>
    <div id="main_cont">
        <table style="width: 100%;">
            <tr>
                <td class="style2">
                    &nbsp;
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
                <td class="style1">
                    <asp:Label ID="lblBonus" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblOTCAdd" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:Label ID="lblAdvance" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblPenalty" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblOTCDed" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblMMDays" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style1">
                    <asp:Label ID="lblNMDays" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style1">
                    <asp:Label ID="lblOTDays" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <strong>Month :</strong>
                </td>
                <td>
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
                    <span style="color: Green; font-weight: bold">e.g. JAN-14</span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <strong>Effect Date :</strong>
                </td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" Width="150px" AutoPostBack="True" ClientIDMode="Static"
                        TabIndex="2" CssClass="txtColor" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                        PopupButtonID="txtDate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td>
                    &nbsp;
                    <asp:Label ID="lblOTHour" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2" style="text-align: right">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    <asp:Button ID="btnProcess" runat="server" CssClass="txtColor" Font-Bold="True" Font-Italic="False"
                        Text="Process" TabIndex="2" OnClick="btnProcess_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2" style="text-align: right">
                    &nbsp;
                    <asp:Label ID="lblError" runat="server" ForeColor="#CC0000" Visible="False" Style="font-weight: 700"></asp:Label>
                </td>
                <td style="text-align: center">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
            PopupControlID="pnlpopup" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="100px" Width="400px"
            Style="display: none">
            <table width="100%" style="border: Solid 2px #D46900; width: 100%; height: 100%"
                cellpadding="0" cellspacing="0">
                <tr style="background-image: url(../../Images/header.gif)">
                    <td style="height: 10%; color: White; font-weight: bold; padding: 3px; font-size: larger;
                        font-family: Calibri" align="Left">
                        Confirm Box
                    </td>
                    <td style="color: White; font-weight: bold; padding: 3px; font-size: larger" align="Right">
                        <a href="javascript:void(0)" onclick="closepopup()">
                            <img src="../../Images/Close.gif" style="border: 0px" align="right" /></a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" style="padding: 5px; font-family: Calibri">
                        <asp:Label ID="lblUser" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right" style="padding-right: 15px">
                        <asp:ImageButton ID="btnYes" runat="server" ImageUrl="~/Images/btnyes.jpg" OnClick="btnYes_Click" />
                        <asp:ImageButton ID="btnNo" runat="server" ImageUrl="~/Images/btnNo.jpg" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:GridView ID="gvShowEmp" runat="server">
        </asp:GridView>
    </div>
</asp:Content>
