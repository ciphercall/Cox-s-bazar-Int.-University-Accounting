<%@ Page Title="Salary Information" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="hr-sal.aspx.cs" Inherits="AlchemyAccounting.payroll.ui.hr_sal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
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
            margin-top: 1%;
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
        .txtalignright
        {
            text-align: right;
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
            Employee Salary Information</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%; border: 1px solid #000;">
                <tr>
                    <td style="text-align: right; width: 50%">
                        &nbsp;</td>
                    <td style="width: 50%">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
            </table>
        </div>
        <div class="def">
            <table style="width: 100%; font-family: Calibri; font-size: 14px">
                <tr>
                    <td style="width: 30%; text-align: right; font-weight: bold">
                        Month
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 69%">
                        <asp:TextBox ID="txtMY" runat="server" CssClass="txtColor txtalign" TabIndex="1"
                            Width="15%" MaxLength="7" AutoPostBack="True" OnTextChanged="txtMY_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtMY_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                            Enabled="True" ServicePath="" TargetControlID="txtMY" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" UseContextKey="True"
                            ServiceMethod="GetCompletionListMY" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                            CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <span style="color: Green; font-weight: bold">e.g. JAN-14</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%; text-align: right; font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 69%">
                        &nbsp;<asp:Label ID="lblError" runat="server" ForeColor="#CC0000" Visible="False" Style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
            </table>
            <div>
                
                        <asp:GridView ID="gvEmphour" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1"
                            GridLines="None" PageSize="15" Width="100%" ShowFooter="True"
                            OnRowCancelingEdit="gvEmphour_RowCancelingEdit" OnRowCommand="gvEmphour_RowCommand"
                            OnRowDataBound="gvEmphour_RowDataBound" OnRowDeleting="gvEmphour_RowDeleting"
                            OnRowEditing="gvEmphour_RowEditing" OnRowUpdating="gvEmphour_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpNM" runat="server" Text='<%# Eval("EMPNM") %>' Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblEmpNmEdit" runat="server" Text='<%# Eval("EMPNM") %>'></asp:Label>
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
                                    <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                    <ItemStyle HorizontalAlign="Left" Width="40%" />
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
                                <asp:TemplateField HeaderText="Bonus">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBonus" runat="server" Text='<%# Eval("BONUS") %>' Width="98%" CssClass="txtalignright"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBonusEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalignright"
                                            Font-Size="12px" TabIndex="42" Width="98%" Text='<%# Eval("BONUS") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtBonus" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="11"
                                            Width="98%" CssClass="txtColor txtalignright" Text=".00"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Addition">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOthAdd" runat="server" Text='<%# Eval("OTCADD") %>' Width="98%" CssClass="txtalignright"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtOthAddEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalignright"
                                            Font-Size="12px" TabIndex="43" Width="98%" Text='<%# Eval("OTCADD") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtOthAdd" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="12"
                                            Width="98%" CssClass="txtColor txtalignright" Text=".00"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Advance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdvance" runat="server" Text='<%# Eval("ADVANCE") %>' Width="98%" CssClass="txtalignright"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAdvanceEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalignright"
                                            Font-Size="12px" TabIndex="44" Width="98%" Text='<%# Eval("ADVANCE") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAdvance" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="13"
                                            Width="98%" CssClass="txtColor txtalignright" Text=".00"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Penalty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPenalty" runat="server" Text='<%# Eval("PENALTY") %>' Width="98%" CssClass="txtalignright"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPenaltyEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalignright"
                                            Font-Size="12px" TabIndex="45" Width="98%" Text='<%# Eval("PENALTY") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtPenalty" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="14"
                                            Width="98%" CssClass="txtColor txtalignright" Text=".00"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Deduction">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOthDed" runat="server" Text='<%# Eval("OTCDED") %>' Width="98%" CssClass="txtalignright"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtOthDedEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalignright"
                                            Font-Size="12px" TabIndex="46" Width="98%" Text='<%# Eval("OTCDED") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtOthDed" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="15"
                                            Width="98%" CssClass="txtColor txtalignright" Text=".00"></asp:TextBox>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
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
    </div>
</asp:Content>
