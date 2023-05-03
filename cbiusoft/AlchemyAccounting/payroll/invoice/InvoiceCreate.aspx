<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InvoiceCreate.aspx.cs" Inherits="AlchemyAccounting.payroll.invoice.InvoiceCreate" %>

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
                        Company Name
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold">
                        <asp:DropDownList runat="server" ID="ddlCompanyNM" CssClass="txtColor" Width="89%"
                            TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlCompanyNM_SelectedIndexChanged">
                            <asp:ListItem>HELMI TRADING &amp; CONTRACTING W.L.L</asp:ListItem>
                            <asp:ListItem>ADI KARIYA QATAR W.L.L</asp:ListItem>
                            <asp:ListItem>NIZAM FURNITURE W.L.L</asp:ListItem>
                            <asp:ListItem>PANCACITRA QATAR CONTRACTING CO., W.L.L</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtcompany" Visible="false" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Bill Date
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtBillDate" runat="server" CssClass="txtColor" TabIndex="2" Width="30%"
                            AutoPostBack="True" OnTextChanged="txtBillDate_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" TargetControlID="txtBillDate"
                            PopupButtonID="txtEntryDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        Year :
                        <asp:TextBox ID="txtYear" runat="server" CssClass="txtColor" Width="20%"></asp:TextBox>
                        <asp:DropDownList runat="server" ID="ddlyear" Visible="false" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlyear_SelectedIndexChanged">
                        </asp:DropDownList>
                        Bill No :
                        <asp:TextBox ID="txtBillno" runat="server" CssClass="txtColor" TabIndex="3" Width="20%"
                            ReadOnly="true"></asp:TextBox>
                        <asp:DropDownList runat="server" ID="ddlbillNo" Enabled="false" Visible="false" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlbillNo_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label runat="server" ID="lblBillNO" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Party ID
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox ID="txtPrtyNM" runat="server" CssClass="txtColor" TabIndex="3" Width="57%"
                            AutoPostBack="True" OnTextChanged="txtPrtyNM_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtPrtyNM" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListParty">
                        </asp:AutoCompleteExtender>
                        &nbsp;Month :
                        <asp:TextBox ID="txtBillMY" runat="server" CssClass="txtColor" TabIndex="4" Width="15%"
                            Visible="true" OnTextChanged="txtBillMY_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtBillMY" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListBillMonth">
                        </asp:AutoCompleteExtender>
                        <asp:TextBox ID="txtPrtyID" runat="server" CssClass="txtColor" Width="20%" Visible="False"></asp:TextBox>
                        &nbsp; <span style="color: Green; font-weight: bold">e.g. JAN-2014</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Site&nbsp;&nbsp;
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox runat="server" ID="txtCostPid" CssClass="txtColor" Width="50%" TabIndex="5"
                            AutoPostBack="True" OnTextChanged="txtCostPid_TextChanged"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtSiteID" CssClass="txtColor" Width="20%" Visible="false"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtexpensebNo_AutoCompleteExtender" runat="server"
                            DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="txtCostPid"
                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                            UseContextKey="True" ServiceMethod="GetCompletionListSiteID" CompletionListItemCssClass="listItem"
                            CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bill Type :
                        <asp:DropDownList runat="server" ID="ddlBillType" CssClass="txtColor" Width="20%"
                            TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddlBillType_SelectedIndexChanged">
                            <asp:ListItem Value="Meter">Meter</asp:ListItem>
                            <asp:ListItem Value="Hour">Hour</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtbillType" Visible="false" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Name
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 79%; text-align: left; font-weight: bold; margin-left: 80px;">
                        <asp:TextBox runat="server" ID="txtSUBMITPNM" CssClass="txtColor" Width="40%" AutoPostBack="True"
                            OnTextChanged="txtSUBMITPNM_TextChanged" TabIndex="7"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtSUBMITPNM" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListName_Contact" CompletionListItemCssClass="listItem"
                            CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                        &nbsp;&nbsp; Contact No :
                        <asp:TextBox runat="server" ID="txtSUBMITPCNO" CssClass="txtColor" Width="33%" AutoPostBack="True"
                            OnTextChanged="txtSUBMITPCNO_TextChanged" TabIndex="8"></asp:TextBox>
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
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 100%; margin-bottom: 2%">
            <asp:GridView ID="gvDetails" runat="server" BackColor="White" BorderColor="White"
                BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None"
                Width="100%" AutoGenerateColumns="False" ShowFooter="True" Style="text-align: left"
                OnRowDataBound="gvDetails_RowDataBound" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                OnRowCommand="gvDetails_RowCommand" OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing"
                OnRowUpdating="gvDetails_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lblSL" runat="server" Style="text-align: center" Text='<%# Eval("BILLSL") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblSLEdit" runat="server" Style="text-align: center" Text='<%# Eval("BILLSL") %>'></asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <FooterStyle CssClass="txtalign" HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="4%" />
                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Style="text-align: center" Text='<%# Eval("BILLNM") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" TabIndex="32" ID="txtCategoryEdit" Style="text-align: center"
                                CssClass="txtColor" Text='<%# Eval("BILLNM") %>' Width="100%"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox runat="server" ID="txtCategory" Style="text-align: left" Width="100%"
                                CssClass="txtColor" TabIndex="9" AutoPostBack="True" OnTextChanged="txtCategory_TextChanged"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Worker">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTotWorkerEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Enabled="true" Font-Size="12px" TabIndex="33" Width="98%" Style="text-align: left"
                                Text='<%# Eval("TWORKER") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTotWorker" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Style="text-align: left" Font-Size="12px" TabIndex="10" Width="98%" Enabled="False"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTotWorker" runat="server" Text='<%# Eval("TWORKER") %>' Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Meter">
                        <EditItemTemplate>
                            <asp:TextBox ID="txttothrEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="34" Text='<%# Eval("RATEPTP") %>' Width="98%" Style="text-align: right"
                                AutoPostBack="True" OnTextChanged="txttothrEdit_TextChanged"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txttothr" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="11" Width="98%" Style="text-align: right" AutoPostBack="True"
                                OnTextChanged="txttothr_TextChanged"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbltothr" runat="server" Text='<%# Eval("RATEPTP") %>' Style="text-align: right"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Per Meter Rate">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPriceEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="35" Text='<%# Eval("TOTQPTP") %>' Width="98%" Style="text-align: right"
                                AutoPostBack="True" OnTextChanged="txtPriceEdit_TextChanged"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="12" Width="98%" Style="text-align: right" AutoPostBack="True"
                                OnTextChanged="txtPrice_TextChanged"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("TOTQPTP") %>' Style="text-align: right"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount In QR">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmountEdit" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Enabled="false" Font-Size="12px" TabIndex="56" Text='<%# Eval("AMTPTP") %>' Width="98%"
                                Style="text-align: right"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Enabled="false" Font-Size="12px" TabIndex="69" Width="98%" Style="text-align: right"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMTPTP") %>' Style="text-align: right"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" Height="20px"
                                CssClass="txtColor" ImageUrl="~/Images/update.jpg" TabIndex="36" ToolTip="Update"
                                Width="20px" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" Height="20px"
                                ImageUrl="~/Images/Cancel.jpg" TabIndex="37" ToolTip="Cancel" Width="20px" CssClass="txtColor" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="13" ToolTip="Save &amp; Continue"
                                ValidationGroup="validaiton" Width="15px" />
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Complete" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/checkmark.jpg" TabIndex="14" ToolTip="Complete"
                                ValidationGroup="validaiton" Width="15px" />
                            <asp:ImageButton ID="ImagebtnPPrint" runat="server" CommandName="SavePrint" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/print.gif" TabIndex="15" ToolTip="Save &amp; Print"
                                ValidationGroup="validaiton" Width="15px" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" Height="20px"
                                CssClass="txtColor" ImageUrl="~/Images/Edit.jpg" TabIndex="100" ToolTip="Edit"
                                Width="20px" />
                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" Height="20px"
                                ImageUrl="~/Images/delete.jpg" OnClientClick="return confMSG()" TabIndex="101"
                                CssClass="txtColor" ToolTip="Delete" Width="21px" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" Font-Names="Calibri"
                    Font-Size="14px" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#DEDFDE" ForeColor="Black" Font-Names="Calibri" Font-Size="12px" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#33276A" />
            </asp:GridView>
            <table style="width: 100%">
                <tr>
                    <td style="width: 7%">
                    </td>
                    <td style="width: 25%">
                    </td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 15%">
                    </td>
                    <td style="width: 15%; text-align: right">
                        Total Amount :
                    </td>
                    <td style="width: 15%">
                        <asp:TextBox ID="txtTotAmount" runat="server" ReadOnly="True" Width="100%" Style="text-align: right;"
                            Font-Bold="True"></asp:TextBox>
                    </td>
                    <td style="width: 10%">
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td class="style12">
                        <strong style="font-size: small">In Words :</strong>
                    </td>
                    <td class="style14">
                        <asp:TextBox ID="txtTotInWords" runat="server" ReadOnly="True" Width="750px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 57%">
                        <asp:Label ID="lblErrMsgExist" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                        <asp:Label ID="lblChkInternalID" runat="server" ForeColor="#990000" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
