<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CommissionCreate.aspx.cs" Inherits="AlchemyAccounting.payroll.commission.CommissionCreate" %>

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
        <h1 align="center">
            Invoice Information</h1>
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
                            Text="Edit" Width="80px" OnClick="btnEdit_Click" />
                        <asp:Button ID="btnPrint" runat="server" Font-Bold="True" Font-Names="Calibri" Enabled="false"
                            Visible="false" Font-Size="15px" Text="Print" Width="80px" OnClick="btnPrint_Click" />
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
                        Date
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold">
                        <asp:TextBox ID="txtBillDate" runat="server" AutoPostBack="True" CssClass="txtColor"
                            OnTextChanged="txtBillDate_TextChanged" TabIndex="1" Width="30%"></asp:TextBox>
                        <asp:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="txtEntryDate" TargetControlID="txtBillDate">
                        </asp:CalendarExtender>
                        &nbsp;&nbsp; Month :&nbsp;
                        <asp:TextBox ID="txtMonth" runat="server" AutoPostBack="True" CssClass="txtColor"
                            Enabled="false" Width="20%"></asp:TextBox>
                        &nbsp;&nbsp;<asp:TextBox ID="txtMonthedit" runat="server" AutoPostBack="True" CssClass="txtColor"
                            Visible="false" Width="20%" OnTextChanged="txtMonthedit_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtMonthedit" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListMonth" CompletionListItemCssClass="listItem"
                            CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                        &nbsp;&nbsp;Invoice No :&nbsp;
                        <asp:TextBox ID="txtInvoice" runat="server" AutoPostBack="True" CssClass="txtColor"
                            Width="12%"></asp:TextBox>
                        <asp:DropDownList ID="ddlInvoiceno" runat="server" AutoPostBack="true" CssClass="txtColor"
                            Visible="false" OnSelectedIndexChanged="ddlInvoiceno_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Receivable Head</td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtPrtyNM" runat="server" CssClass="txtColor" TabIndex="2" Width="70%"
                            AutoPostBack="True" OnTextChanged="txtPrtyNM_TextChanged"></asp:TextBox>
                        &nbsp;<asp:TextBox ID="txtPrtyID" runat="server" CssClass="txtColor" Width="20%"
                            Visible="False"></asp:TextBox>
                        <asp:Label runat="server" ID="lblBillNO" Visible="False" ForeColor="Red"></asp:Label>
                        &nbsp;<asp:Label runat="server" ID="lblerrmsgp" Visible="False" ForeColor="Red"></asp:Label>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtPrtyNM" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListParty">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Payable Head</td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :</td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtPayable" runat="server" CssClass="txtColor" TabIndex="3" Width="70%"
                            AutoPostBack="True" OnTextChanged="txtPayable_TextChanged"></asp:TextBox>
                        &nbsp;<asp:TextBox ID="txtPayableID" runat="server" CssClass="txtColor" Width="20%"
                            Visible="False"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtPayable_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtPayable" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListPayable">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Site
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox runat="server" ID="txtCostPid" CssClass="txtColor" Width="70%" TabIndex="4"
                            AutoPostBack="True" OnTextChanged="txtCostPid_TextChanged"></asp:TextBox>
                        &nbsp;<asp:TextBox runat="server" ID="txtSiteID" CssClass="txtColor" Width="20%"
                            Visible="false"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtexpensebNo_AutoCompleteExtender" runat="server"
                            DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="txtCostPid"
                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                            UseContextKey="True" ServiceMethod="GetCompletionListSiteID" CompletionListItemCssClass="listItem"
                            CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                        <asp:Label runat="server" ID="lblerrmsgS" Visible="False" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Bill Amount
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtbillamt" runat="server" AutoPostBack="True" CssClass="txtColor"
                            TabIndex="5" Width="40%" OnTextChanged="txtbillamt_TextChanged"></asp:TextBox>
                        &nbsp; Percentage :
                        <asp:TextBox ID="txtpercentage" runat="server" AutoPostBack="True" CssClass="txtColor"
                            Width="12%" OnTextChanged="txtpercentage_TextChanged" TabIndex="6"></asp:TextBox>
                        %
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Commission Amount
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtcomAmt" runat="server" AutoPostBack="True" CssClass="txtColor"
                            TabIndex="7" Width="40%" Enabled="false" 
                            OnTextChanged="txtcomAmt_TextChanged"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblerrmsg" Visible="False" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Car Rent
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtcarrent" runat="server" AutoPostBack="True" CssClass="txtColor"
                            TabIndex="8" Width="40%" OnTextChanged="txtcarrent_TextChanged"></asp:TextBox>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Advance Amount
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 120px;">
                        <asp:TextBox ID="txtAdvanceAmount" runat="server" AutoPostBack="True" CssClass="txtColor"
                            TabIndex="9" Width="40%" OnTextChanged="txtAdvanceAmount_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right; font-weight: bold">
                        Total Amount
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 74%; text-align: left; font-weight: bold; margin-left: 240px;">
                        <asp:TextBox ID="txtTotalAmount" runat="server" AutoPostBack="True" CssClass="txtColor"
                            TabIndex="10" Width="40%" OnTextChanged="txtTotalAmount_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Advance Amount Company
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 120px;">
                        <asp:TextBox ID="txtAdvanceAmountComp" runat="server" AutoPostBack="True" CssClass="txtColor"
                            TabIndex="11" Width="40%" OnTextChanged="txtAdvanceAmountComp_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Net Amount
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 120px;">
                        <asp:TextBox ID="txtNetAmount" runat="server" AutoPostBack="True" CssClass="txtColor"
                            TabIndex="12" Width="40%" Enabled="False" 
                            OnTextChanged="txtNetAmount_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Remarks
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 120px;">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtColor" TabIndex="13" 
                            Width="70%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 120px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 120px;">
                        <asp:Button ID="btnSave" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                            Text="Save" Width="80px" OnClick="btnSave_Click" TabIndex="14" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnPrintsave" runat="server" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="15px" Text="Print & Save" Width="100px" OnClick="btnPrintsave_Click"
                            TabIndex="15" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                            Text="Delete" Width="80px" OnClick="btnDelete_Click" OnClientClick="confMSG()"
                            TabIndex="20" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
