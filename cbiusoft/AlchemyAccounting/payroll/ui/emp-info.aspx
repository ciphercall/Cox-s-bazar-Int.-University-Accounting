<%@ Page Title="Employee Information" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="emp-info.aspx.cs" Inherits="AlchemyAccounting.payroll.ui.emp_info" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }

            return true;
        }
        
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
                //                alert("Clicked Yes");
            }
            else {
                //                alert("Clicked No");
                return false;
            }

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
        .style1
        {
            width: 20%;
            height: 29px;
        }
        .style2
        {
            width: 1%;
            height: 29px;
        }
        .style3
        {
            width: 79%;
            height: 29px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
            Employee Information</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%; border: 1px solid #000;">
                <tr>
                    <td style="text-align: right; width: 50%">
                    <asp:Label ID="lblEdit" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblDelete" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblChkID" runat="server" Visible="False"></asp:Label>
                        <asp:Button ID="btnEdit" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                            Text="Edit" Width="80px" OnClick="btnEdit_Click" TabIndex="24" />
                    </td>
                    <td style="width: 50%">
                        <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="15px" Text="Refresh" Width="80px" OnClick="btnRefresh_Click" TabIndex="25" />
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
                        Employee Name
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold">
                        <asp:TextBox ID="txtMemberInfo" runat="server" CssClass="txtColor" TabIndex="1" Width="50%"></asp:TextBox>
                        <asp:TextBox ID="txtMemberInfoEdit" runat="server" CssClass="txtColor" TabIndex="1"
                            Width="50%" Visible="False"></asp:TextBox>
                        <%--<asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtMemberInfoEdit" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListMemInfo">
                        </asp:AutoCompleteExtender>--%>
                        &nbsp;<asp:TextBox ID="txtMemberId" runat="server" ReadOnly="True" Width="15%" Style="text-align: center"></asp:TextBox>
                        <asp:DropDownList ID="ddlMemberId" runat="server" CssClass="txtColor" Visible="False"
                            Width="15%" AutoPostBack="True" OnSelectedIndexChanged="ddlMemberId_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Father&#39;s Name
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 40px;">
                        <asp:TextBox ID="txtFatherNm" runat="server" CssClass="txtColor" TabIndex="2" Width="36%"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Entry Date :
                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="txtColor" TabIndex="3" Width="15%"
                            AutoPostBack="True" OnTextChanged="txtEntryDate_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" TargetControlID="txtEntryDate"
                            PopupButtonID="txtEntryDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; font-weight: bold" class="style1">
                        Qatar ID
                    </td>
                    <td style="text-align: center; font-weight: bold" class="style2">
                        :
                    </td>
                    <td style="text-align: left; font-weight: bold; margin-left: 80px;" class="style3">
                        <asp:TextBox ID="txtQID" runat="server" CssClass="txtColor" TabIndex="4" Width="36%"></asp:TextBox>
                        &nbsp; ID Expire Date :
                        <asp:TextBox ID="txtIDExpDate" runat="server" CssClass="txtColor" TabIndex="5" Width="15%"
                            AutoPostBack="True" OnTextChanged="txtIDExpDate_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIDExpDate"
                            PopupButtonID="txtIDExpDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Passport No
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtPPNo" runat="server" CssClass="txtColor" TabIndex="8" Width="36%"></asp:TextBox>
                        &nbsp;&nbsp;PP Expire Date :
                        <asp:TextBox ID="txtPPExpDate" runat="server" CssClass="txtColor" TabIndex="9" Width="15%"
                            AutoPostBack="True" OnTextChanged="txtPPExpDate_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="txtPPExpDate_CalendarExtender" runat="server" TargetControlID="txtPPExpDate"
                            PopupButtonID="txtPPExpDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Nationality
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtNationality" runat="server" CssClass="txtColor" TabIndex="10"
                            Width="36%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Occupation
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtOccup" runat="server" CssClass="txtColor" TabIndex="11" Width="36%"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; File No
                        :
                        <asp:TextBox ID="txtFileNo" runat="server" CssClass="txtColor" TabIndex="12" Width="30%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Basic Salary
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtBasicSal" runat="server" CssClass="txtColor" TabIndex="13" Style="text-align: right"
                            Width="36%" onkeypress="return isNumberKey(event)">.00</asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Foods :
                        <asp:TextBox ID="txtFoods" runat="server" CssClass="txtColor" TabIndex="14" Style="text-align: right"
                            Width="30%" onkeypress="return isNumberKey(event)">.00</asp:TextBox>
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
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="txtColor" TabIndex="15" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Contact No
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtContact" runat="server" CssClass="txtColor" TabIndex="16" onkeypress="return isNumberKey(event)"
                            Width="40%" AutoPostBack="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Company Name
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <%--<asp:TextBox ID="txtComNM" runat="server" CssClass="txtColor" TabIndex="17" Width="100%"></asp:TextBox>--%><asp:DropDownList
                            ID="ddlCompNM" runat="server" CssClass="txtColor" TabIndex="17" 
                            Width="100%">
                            <asp:ListItem>HELMI TRADING &amp; CONTRACTING W.L.L</asp:ListItem>
                            <asp:ListItem>ADI KARIYA QATAR W.L.L</asp:ListItem>
                            <asp:ListItem>NIZAM FURNITURE W.L.L</asp:ListItem>
                            <asp:ListItem>PANCACITRA QATAR CONTRACTING CO., W.L.L</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Reference
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtRef" runat="server" CssClass="txtColor" TabIndex="18" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Vacation From
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtVacFr" runat="server" CssClass="txtColor" TabIndex="19" Width="15%"
                            AutoPostBack="True" OnTextChanged="txtVacFr_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="txtVacFr_CalendarExtender" runat="server" TargetControlID="txtVacFr"
                            PopupButtonID="txtVacFr" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        &nbsp;&nbsp; Vacation To :
                        <asp:TextBox ID="txtVacTo" runat="server" CssClass="txtColor" TabIndex="20" Width="15%"
                            AutoPostBack="True" OnTextChanged="txtVacTo_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="txtVacTo_CalendarExtender" runat="server" TargetControlID="txtVacTo"
                            PopupButtonID="txtVacTo" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Status
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtStatus" runat="server" CssClass="txtColor" TabIndex="21" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Note
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtNote" runat="server" CssClass="txtColor" TabIndex="22" Width="100%"></asp:TextBox>
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
                        <asp:Button ID="btnSave" runat="server" TabIndex="23" Text="Save" Font-Bold="True"
                            Width="15%" OnClick="btnSave_Click" CssClass="txtColor txtalign" />
                        &nbsp;
                        <asp:Button ID="btnDelete" runat="server" TabIndex="24" Text="Delete" Font-Bold="True"
                            Width="15%" OnClick="btnDelete_Click" CssClass="txtColor txtalign" Visible="False" OnClientClick="return confMSG()"/>
                        <asp:Label ID="lblErrMsg" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                        <asp:Label ID="lblChkMemID" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblMaxSchSl" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblStatGv" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblCompNm" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="def" style="margin-bottom: 1%">
        </div>
    </div>
</asp:Content>
