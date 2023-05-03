<%@ Page Title="Closing Balance Entry" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="OpeningBalance.aspx.cs" Inherits="AlchemyAccounting.Accounts.UI.OpeningBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });

            var dlg = $("").dialog({
                autoOpen: false,
                modal: false,
                title: "",
                position: ["center", "center"],
                resizable: false
            });
            dlg.parent().appendTo(jQuery("form:first"));
        });

        function shai_dailog() {
            $("").dialog("open");
        }
        function execute_txt() {

        }

        //------------- show iframe dialog --------------------
        function showInFrameDialog(ptr, page, newTitle) {
            var width = $(window).width();
            var height = $(window).height();
            var frame = $('<iframe/>');
            frame = $(frame).attr('width', (width - 60));
            frame = $(frame).attr('height', (height - 110));
            frame = $(frame).attr('src', page);

            var container = $('<div id="frameDialog"></div>');
            container = container.append(frame);
            container.dialog({
                autoOpen: true,
                modal: true,
                title: newTitle,
                width: (width - 100),
                height: (height - 50),
                onEscapeClose: false,
                resizable: true,
                dragable: true
            });


            return false;
        }


        //------------- show iframe dialog --------------------
        function showPageInIFrame(page) {
            //    var width = $(window).width();
            //    var height = $(window).height();
            var frame = $('<iframe/>');
            frame = $(frame).attr('width', 200);
            frame = $(frame).attr('height', 200);
            frame = $(frame).attr('src', page);

            var container = $('<div id="frameDialog"></div>');
            container = container.append(frame);

            container.dialog({
                autoOpen: true,
                modal: false,
                title: newTitle,
                width: (width - 100),
                height: (height - 50),
                onEscapeClose: false,
                resizable: true,
                dragable: true
            });
            return false;
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
            border-radius: 10px;
            margin-top: 10px;
            margin-bottom: 30px;
        }
        #toolbar
        {
            float: left;
            width: 100%;
            height: 60px;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        .style1
        {
            width: 27px;
        }
        .style2
        {
            width: 6px;
        }
        #msg
        {
            float: left;
            width: 40%;
            height: 100px;
        }
        #grid
        {
            float: left;
            width: 90%;
            margin: 0% 5% 0% 5%;
        }
        
        .style11
        {
            width: 359px;
        }
        .style12
        {
            width: 58px;
        }
        .style13
        {
            width: 36px;
        }
        #btnEdit
        {
            float: right;
            width: 40px;
            height: 19px;
            padding-bottom: 2px;
            background: #f2f2f2;
            border: 1px solid;
            border-radius: 5px;
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
        .style21
        {
            width: 521px;
            text-align: right;
        }
        .style23
        {
            width: 521px;
            text-align: right;
            font-weight: bold;
        }
        .style24
        {
            font-weight: bold;
            width: 2px;
        }
        .style25
        {
            width: 2px;
        }
        .style26
        {
            width: 118px;
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
        <h1 align="center">
            Opening Balance Entry</h1>
    </div>
    <div id="entry"> 
        <div id="toolbar">
            <table style="width: 90%; margin: 0% 5% 0% 5%;">
                <tr>
                    <td class="style11">
                        <asp:Label ID="lblTotCount" runat="server"></asp:Label>
                    </td>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td class="style12">
                        &nbsp;
                    </td>
                    <td class="style13">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblVCount" runat="server"></asp:Label>
                        <asp:Label ID="lbltxtChg" runat="server"></asp:Label>
                        <asp:Label ID="lbltxtShw" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style11" style="text-align: right">
                        &nbsp;
                    </td>
                    <td class="style1">
                        <%--<asp:Button ID="btnEdit" runat="server" TabIndex="25" Text="EDIT" 
                        onclick="btnEdit_Click" OnClientClick="return ShowHide()"/>--%>
                    </td>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td class="style12">
                        &nbsp;
                    </td>
                    <td class="style13" style="text-align: right">
                        <strong>Date:&nbsp;&nbsp;</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static" OnTextChanged="txtDate_TextChanged"
                            AutoPostBack="True" TabIndex="1" CssClass="txtColor"></asp:TextBox>
                        <asp:Label ID="lblMY" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style11">
                        &nbsp;
                    </td>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td class="style12">
                        &nbsp;
                    </td>
                    <td class="style13">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <div id="grid">
            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" CssClass="Gridview"
                HeaderStyle-BackColor="#61A6F8" ShowFooter="True" HeaderStyle-Font-Bold="true"
                HeaderStyle-ForeColor="White" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating"
                OnRowCommand="gvDetails_RowCommand" OnRowDataBound="gvDetails_RowDataBound" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("TRANSDT") %>' Width="80px" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%#Eval("TRANSDT") %>' Width="80px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <ControlStyle Width="80px" />
                        <FooterStyle Width="80px" HorizontalAlign="Center" />
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="lblVouchNo" runat="server" Text='<%# Eval("TRANSNO") %>' Width="50px"
                                Style="text-align: center" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblVouchNo" runat="server" Text='<%#Eval("TRANSNO") %>' Width="50px"
                                Style="text-align: center" />
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                        <ControlStyle Width="30px" />
                        <FooterStyle Width="40px" HorizontalAlign="Center" />
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Account Head">
                        <ItemTemplate>
                            <asp:Label ID="lblAccHdNM" runat="server" Text='<%# Eval("ACCOUNTNM") %>' Width="400px"
                                Style="text-align: left" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDebitCDEdit" runat="server" Text='<%#Eval("ACCOUNTNM") %>' Width="400px"
                                OnTextChanged="txtDebitCDEdit_TextChanged" TabIndex="12" CssClass="txtColor"/>
                            <asp:AutoCompleteExtender ID="txtDebitCDEdit_AutoCompleteExtender" runat="server"
                                TargetControlID="txtDebitCDEdit" UseContextKey="True" MinimumPrefixLength="1"
                                CompletionInterval="10" EnableCaching="true" CompletionSetCount="3" ServiceMethod="GetCompletionListDebit">
                            </asp:AutoCompleteExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDebitCD" runat="server" Width="400px" TabIndex="2" CssClass="txtColor" OnTextChanged="txtDebitCD_TextChanged" />
                            <asp:AutoCompleteExtender ID="txtDebitCD_AutoCompleteExtender" runat="server" TargetControlID="txtDebitCD"
                                UseContextKey="True" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true"
                                CompletionSetCount="3" ServiceMethod="GetCompletionListDebit">
                            </asp:AutoCompleteExtender>
                            <%--<asp:RequiredFieldValidator ID="accHd" runat="server" 
            ControlToValidate="txtAccHead" Text="*" ValidationGroup="validaiton" 
            Font-Bold="True" Font-Italic="True" ForeColor="Red"/>--%>
                        </FooterTemplate>
                        <ControlStyle Width="400px" />
                        <FooterStyle Width="400px" />
                        <HeaderStyle Width="400px" HorizontalAlign="Center" />
                        <ItemStyle Width="400px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debit Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblDebitAmt" runat="server" Text='<%#Eval("DEBITAMT") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDbAmtEdit" runat="server" Text='<%#Eval("DEBITAMT") %>' Style="text-align: right"
                                TabIndex="13" CssClass="txtColor"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDbAmt" runat="server" TabIndex="3" Style="text-align: right" CssClass="txtColor"/>
                        </FooterTemplate>
                        <ControlStyle Width="100px" />
                        <FooterStyle Width="100px" HorizontalAlign="Right" />
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblCrAmt" runat="server" Text='<%#Eval("CREDITAMT") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCrAmtEdit" runat="server" Text='<%#Eval("CREDITAMT") %>' Style="text-align: right"
                                TabIndex="14" CssClass="txtColor"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCrAmt" runat="server" TabIndex="4" Style="text-align: right" CssClass="txtColor"/>
                        </FooterTemplate>
                        <ControlStyle Width="100px" />
                        <FooterStyle Width="100px" HorizontalAlign="Right" />
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/update.jpg"
                                ToolTip="Update" Height="20px" Width="20px" TabIndex="15" CssClass="txtColor"/>
                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.jpg"
                                ToolTip="Cancel" Height="20px" Width="20px" TabIndex="16" CssClass="txtColor"/>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.jpg"
                                ToolTip="Edit" Height="20px" Width="20px" TabIndex="10" CssClass="txtColor"/>
                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                ImageUrl="~/Images/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
                                TabIndex="11" CssClass="txtColor"/>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/AddNewitem.jpg"
                                CommandName="AddNew" Width="30px" Height="30px" ToolTip="Add new Record" ValidationGroup="validaiton"
                                TabIndex="5" CssClass="txtColor"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999966" />
                <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White"></HeaderStyle>
            </asp:GridView>
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td class="style23">
                        Total
                    </td>
                    <td class="style24">
                        :
                    </td>
                    <td class="style26">
                        <asp:TextBox ID="txtTotDebit" runat="server"  Height="30" ReadOnly="True" Font-Bold="True" Style="text-align: right"
                            Width="140px" TabIndex="10"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;<asp:TextBox ID="txtTotCredit" runat="server" ReadOnly="True" Font-Bold="True"
                            Style="text-align: right" Width="140px"  Height="30" TabIndex="11"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td class="style21">
                        &nbsp;
                    </td>
                    <td class="style25">
                        &nbsp;
                    </td>
                    <td class="style26">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td class="style21">
                        &nbsp;
                    </td>
                    <td class="style25">
                        &nbsp;
                    </td>
                    <td class="style26">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
