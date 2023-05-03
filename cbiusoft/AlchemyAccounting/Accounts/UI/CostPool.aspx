<%@ Page Title="Costpool Entry" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"
    CodeBehind="CostPool.aspx.cs" Inherits="AlchemyAccounting.Accounts.UI.CostPool" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
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
        #entry
        {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            border-radius: 10px;
            margin-top: 10px;
        }
        #grid
        {
            float: left;
            width: 100%;
        }
        .style3
        {
            width: 124px;
        }
        .style4
        {
            width: 17px;
        }
        .style5
        {
            text-align: left;
        }
        .style6
        {
            width: 95px;
        }
        .style8
        {
            text-align: center;
            width: 1px;
        }
        .style9
        {
            width: 1px;
        }
        .style10
        {
            width: 238px;
        }
        .style11
        {
            text-align: left;
            width: 238px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center" style="font-weight: bold;">
            Cost Pool Entry</h1>
    </div>
    <div id="entry">
        <table style="width: 100%;">
            <tr>
                <td class="style3">
                    <asp:Label ID="lblPSID" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style6">
                    &nbsp;
                </td>
                <td class="style9">
                    &nbsp;
                </td>
                <td class="style10">
                    <asp:Label ID="lblCatID" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMaxCatID" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style6" style="text-align: right">
                    <strong>Category Name</strong>
                </td>
                <td class="style8">
                    <strong>:</strong>
                </td>
                <td class="style11">
                    <asp:TextBox ID="txtCategoryNM" runat="server" TabIndex="1" CssClass="txtColor" Width="250px"
                        OnTextChanged="txtCategoryNM_TextChanged" AutoPostBack="True"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtCategoryNM_AutoCompleteExtender" runat="server"
                        DelimiterCharacters="" Enabled="True" ServicePath="" TargetControlID="txtCategoryNM"
                        MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                        UseContextKey="True" ServiceMethod="GetCompletionList">
                    </asp:AutoCompleteExtender>
                </td>
                <td class="style5">
                    <asp:Button ID="Search" runat="server" Font-Bold="True" Font-Italic="True" TabIndex="2"
                        CssClass="txtColor" Text="Search" OnClick="Search_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblErrMsg" runat="server" Visible="False" Font-Names="Calibri" Font-Size="14px"
                        ForeColor="#990000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="lblChkItemID" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style6">
                    &nbsp;
                </td>
                <td class="style9">
                    &nbsp;
                </td>
                <td class="style10">
                    <asp:Label ID="lblIMaxItemID" runat="server" Visible="False"></asp:Label>
                </td>
                <td> 
                </td>
            </tr>
        </table>
        <div id="grid">
            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" CssClass="Gridview"
                HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                HeaderStyle-ForeColor="White" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating"
                OnRowCommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" Width="100%"
                Font-Names="Calibri">
                <Columns>
                    <asp:TemplateField HeaderText="Cat ID">
                        <ItemTemplate>
                            <asp:Label ID="lblCatGID" runat="server" Text='<%# Eval("CATID") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblCatGIDEdit" runat="server" Text='<%#Eval("CATID") %>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="3%" />
                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cost Pool ID">
                        <ItemTemplate>
                            <asp:Label ID="lblCOSTPID" runat="server" Text='<%# Eval("COSTPID") %>' Style="text-align: center" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblCOSTPIDEdit" runat="server" Text='<%#Eval("COSTPID") %>' Style="text-align: center" />
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cost Pool Name">
                        <ItemTemplate>
                            <asp:Label ID="lblCOSTPNM" runat="server" Text='<%# Eval("COSTPNM") %>' Style="text-align: left" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCOSTPNMEdit" runat="server" Text='<%#Eval("COSTPNM") %>' Width="98%"
                                TabIndex="10" CssClass="txtColor" Font-Names="Calibri" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCOSTPNM" runat="server" Width="98%" TabIndex="3" CssClass="txtColor"
                                Font-Names="Calibri" />
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="29%" />
                        <ItemStyle HorizontalAlign="Left" Width="29%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="COSTPSID">
                        <ItemTemplate>
                            <asp:Label ID="lblCOSTPSID" runat="server" Text='<%#Eval("COSTPSID") %>' Width="98%" Style="text-align: center;" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCOSTPSIDEdit" runat="server" Text='<%#Eval("COSTPSID") %>' TabIndex="11"
                                CssClass="txtColor" Width="98%" Style="text-align: center;" Font-Names="Calibri" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCOSTPSID" runat="server" TabIndex="4" CssClass="txtColor" Style="text-align: center"
                                Width="98%" Font-Names="Calibri" />
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                        <ItemStyle Width="12%" HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Percent">
                        <ItemTemplate>
                            <asp:Label ID="lblPercent" runat="server" Text='<%#Eval("CPCNT") %>' Style="text-align: right;"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPercentEdit" runat="server" TabIndex="12" CssClass="txtColor"
                                Width="98%" Text='<%#Eval("CPCNT") %>' Style="text-align: right;"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPercent" runat="server" TabIndex="5" CssClass="txtColor" Width="98%"
                                Style="text-align: right;"></asp:TextBox>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Effect From">
                        <ItemTemplate>
                            <asp:Label ID="lblEFECTFR" runat="server" Text='<%#Eval("EFECTFR") %>' Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEFECTFREdit" runat="server" Text='<%#Eval("EFECTFR") %>' TabIndex="13"
                                CssClass="txtColor" Width="98%" Font-Names="Calibri" AutoPostBack="True" ClientIDMode="Static"
                                OnTextChanged="txtEFECTFREdit_TextChanged"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEFECTFREdit"
                                PopupButtonID="txtEFECTFREdit" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEFECTFR" runat="server" TabIndex="6" CssClass="txtColor" Style="text-align: left"
                                Width="98%" Font-Names="Calibri" AutoPostBack="True" ClientIDMode="Static" OnTextChanged="txtEFECTFR_TextChanged"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEFECTFR"
                                PopupButtonID="txtEFECTFR" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="9%" />
                        <ItemStyle HorizontalAlign="Center" Width="9%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Effect To">
                        <ItemTemplate>
                            <asp:Label ID="lblEFECTTO" runat="server" Text='<%#Eval("EFECTTO") %>' Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEFECTTOEdit" runat="server" Text='<%#Eval("EFECTTO") %>' TabIndex="13"
                                CssClass="txtColor" Width="98%" Font-Names="Calibri" AutoPostBack="True" ClientIDMode="Static"
                                OnTextChanged="txtEFECTTOEdit_TextChanged"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEFECTTOEdit"
                                PopupButtonID="txtEFECTTOEdit" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEFECTTO" runat="server" TabIndex="6" CssClass="txtColor" Style="text-align: left"
                                Width="98%" Font-Names="Calibri" AutoPostBack="True" ClientIDMode="Static" OnTextChanged="txtEFECTTO_TextChanged"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEFECTTO"
                                PopupButtonID="txtEFECTTO" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="9%" />
                        <ItemStyle HorizontalAlign="Center" Width="9%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtREMARKSEdit" runat="server" Text='<%#Eval("REMARKS") %>' Width="98%"
                                TabIndex="15" CssClass="txtColor" Font-Names="Calibri"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtREMARKS" runat="server" Width="98%" TabIndex="8" CssClass="txtColor"
                                Font-Names="Calibri"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblREMARKS" runat="server" Width="98%" Text='<%#Eval("REMARKS") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="18%" />
                        <ItemStyle Width="18%" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/update.jpg"
                                ToolTip="Update" Height="20px" Width="20px" TabIndex="16" CssClass="txtColor" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.jpg"
                                ToolTip="Cancel" Height="20px" Width="20px" TabIndex="17" CssClass="txtColor" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.jpg"
                                ToolTip="Edit" Height="20px" Width="20px" TabIndex="30" CssClass="txtColor" />
                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                ImageUrl="~/Images/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
                                TabIndex="31" CssClass="txtColor" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/AddNewitem.jpg"
                                CommandName="AddNew" Width="20px" Height="20px" ToolTip="Add new Record" ValidationGroup="validaiton"
                                TabIndex="9" CssClass="txtColor" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999966" />
                <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White"></HeaderStyle>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
