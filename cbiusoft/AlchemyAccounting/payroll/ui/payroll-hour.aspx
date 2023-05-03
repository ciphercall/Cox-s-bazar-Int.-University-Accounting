<%@ Page Title="Employee Working Hour" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="payroll-hour.aspx.cs" Inherits="AlchemyAccounting.payroll.ui.payroll_hour" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
                //                alert("Clicked Yes");
            }
            else {
                //                alert("Clicked No");
                return false;
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }

            return true;
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
            Employee Working Hour</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%; border: 1px solid #000;">
                <tr>
                    <td style="text-align: right; width: 50%">
                        <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="15px" Text="Refresh" Width="80px" OnClick="btnRefresh_Click" />
                    </td>
                    <td style="width: 50%">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 100%; margin: 1% 0 1% 0">
            <table style="width: 100%">
                <tr>
                    <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        Date
                    </td>
                    <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        :
                    </td>
                    <td style="width: 69%; font-family: Calibri; font-size: 14px">
                        <asp:TextBox ID="txtDt" runat="server" AutoPostBack="True" CssClass="txtColor" TabIndex="1"
                            Width="15%" OnTextChanged="txtDt_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDt"
                            PopupButtonID="txtIDExpDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        <asp:Label ID="lblMy" runat="server" Visible="False"></asp:Label>
                        DAY TYPE :
                        <asp:Label ID="lblDayTp" runat="server" ForeColor="#3366CC" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        Site
                    </td>
                    <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        :
                    </td>
                    <td style="width: 69%; font-family: Calibri; font-size: 14px">
                        <asp:TextBox ID="txtSiteNM" runat="server" CssClass="txtColor" TabIndex="2" Width="30%"
                            AutoPostBack="True" OnTextChanged="txtSiteNM_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtSiteNM_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtSiteNM" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListSite" CompletionListCssClass="completionList"
                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                        <asp:TextBox ID="txtSiteID" runat="server" Width="20%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 69%; font-family: Calibri; font-size: 14px">
                        <asp:Label ID="lblError" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 100%; margin: 0 0 1% 0">
            <asp:GridView ID="gvEmphour" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1"
                GridLines="None" PageSize="15" Width="100%" ShowFooter="True" OnPageIndexChanging="gvEmphour_PageIndexChanging"
                OnRowCancelingEdit="gvEmphour_RowCancelingEdit" OnRowCommand="gvEmphour_RowCommand"
                OnRowDataBound="gvEmphour_RowDataBound" OnRowDeleting="gvEmphour_RowDeleting"
                OnRowEditing="gvEmphour_RowEditing" OnRowUpdating="gvEmphour_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                            <asp:Label ID="lblEmpNM" runat="server" Text='<%# Eval("EMPNM") %>' Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmpNMEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                Font-Size="12px" TabIndex="41" Width="98%" Text='<%# Eval("EMPNM") %>' AutoPostBack="True"
                                OnTextChanged="txtEmpNMEdit_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                Enabled="True" ServicePath="" TargetControlID="txtEmpNMEdit" MinimumPrefixLength="1"
                                CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                ServiceMethod="GetCompletionListEmployeeInfo" CompletionListCssClass="completionList"
                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                            </asp:AutoCompleteExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEmpNM" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="10"
                                Width="98%" CssClass="txtColor" AutoPostBack="True" OnTextChanged="txtEmpNM_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                Enabled="True" ServicePath="" TargetControlID="txtEmpNM" MinimumPrefixLength="1"
                                CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                ServiceMethod="GetCompletionListEmployeeInfo" CompletionListCssClass="completionList"
                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                            </asp:AutoCompleteExtender>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="30%" />
                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee ID" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="lblEmpIDEdit" runat="server" Text='<%# Eval("EMPID") %>'></asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEmpID" runat="server" CssClass="txtColor" Font-Names="Calibri"
                                Font-Size="12px" TabIndex="330" Width="98%" ReadOnly="True"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEmpID" runat="server" Text='<%# Eval("EMPID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trade">
                        <ItemTemplate>
                            <asp:Label ID="lblTrade" runat="server" Text='<%# Eval("TRADE") %>' Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTradeEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                Font-Size="12px" TabIndex="42" Width="98%" Text='<%# Eval("TRADE") %>' AutoPostBack="True"
                                OnTextChanged="txtTradeEdit_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtTradeEdit_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                Enabled="True" ServicePath="" TargetControlID="txtTradeEdit" MinimumPrefixLength="1"
                                CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                ServiceMethod="GetCompletionListTrade" CompletionListCssClass="completionList"
                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                            </asp:AutoCompleteExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTrade" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="11"
                                Width="98%" CssClass="txtColor" AutoPostBack="True" OnTextChanged="txtTrade_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtTrade_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                Enabled="True" ServicePath="" TargetControlID="txtTrade" MinimumPrefixLength="1"
                                CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                                ServiceMethod="GetCompletionListTrade" CompletionListCssClass="completionList"
                                CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                            </asp:AutoCompleteExtender>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Normal Hour">
                        <ItemTemplate>
                            <asp:Label ID="lblNHr" runat="server" Text='<%# Eval("NORMALHR") %>' Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNHrEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalign"
                                Font-Size="12px" TabIndex="43" Width="98%" Text='<%# Eval("NORMALHR") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNHr" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="12"
                                Width="98%" CssClass="txtColor txtalign" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Normal OT">
                        <ItemTemplate>
                            <asp:Label ID="lblNOT" runat="server" Text='<%# Eval("NORMALOT") %>' Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNOTEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalign"
                                Font-Size="12px" TabIndex="44" Width="98%" Text='<%# Eval("NORMALOT") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNOT" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="13"
                                Width="98%" CssClass="txtColor txtalign" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Friday OT">
                        <ItemTemplate>
                            <asp:Label ID="lblFOT" runat="server" Text='<%# Eval("FRIDAYOT") %>' Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFOTEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalign"
                                Font-Size="12px" TabIndex="45" Width="98%" Text='<%# Eval("FRIDAYOT") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFOT" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="14"
                                Width="98%" CssClass="txtColor txtalign" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Holiday OT">
                        <ItemTemplate>
                            <asp:Label ID="lblHOT" runat="server" Text='<%# Eval("HOLIDAYOT") %>' Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtHOTEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalign"
                                Font-Size="12px" TabIndex="46" Width="98%" Text='<%# Eval("HOLIDAYOT") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtHOT" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="15"
                                Width="98%" CssClass="txtColor txtalign" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SL" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="lblSLEdit" runat="server" Text='<%# Eval("SL") %>'></asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSL" runat="server" Text='<%# Eval("SL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" Height="20px"
                                ImageUrl="~/Images/Edit.jpg" TabIndex="100" ToolTip="Edit" Width="15px" />
                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" Height="20px"
                                ImageUrl="~/Images/delete.jpg" OnClientClick="return confMSG()" TabIndex="101"
                                ToolTip="Delete" Width="15px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" Height="20px"
                                ImageUrl="~/Images/update.jpg" TabIndex="47" ToolTip="Update" Width="15px" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" Height="20px"
                                ImageUrl="~/Images/Cancel.jpg" TabIndex="48" ToolTip="Cancel" Width="15px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="16" ToolTip="Save &amp; Continue"
                                ValidationGroup="validaiton" Width="15px" />
                            <%--<asp:ImageButton ID="ImageButton1" runat="server" CommandName="Complete" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/checkmark.jpg" TabIndex="35" ToolTip="Complete"
                                ValidationGroup="validaiton" Width="30px" />
                            <asp:ImageButton ID="ImagebtnPPrint" runat="server" CommandName="SavePrint" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/print.gif" TabIndex="36" ToolTip="Save &amp; Print"
                                ValidationGroup="validaiton" Width="30px" />--%>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#33276A" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
