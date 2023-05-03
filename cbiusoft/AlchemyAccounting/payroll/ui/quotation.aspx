<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="quotation.aspx.cs" Inherits="AlchemyAccounting.payroll.ui.quotation" %>

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
        .txtalignright
        {
            text-align: right;
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
            Quotation</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%; border: 1px solid #000;">
                <tr>
                    <td style="text-align: right; width: 50%">
                    <asp:Label ID="lblEdit" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblDelete" runat="server" Visible="False"></asp:Label>
                        <asp:Button ID="btnEdit" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                            Text="Edit" Width="80px" OnClick="btnEdit_Click" />
                        <asp:Button ID="btnRefresh" runat="server" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="15px" Text="Refresh" Width="80px" OnClick="btnRefresh_Click" />
                    </td>
                    <td style="width: 50%">
                        <asp:Button ID="btnPrint" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                            Text="Print" Width="80px" OnClick="btnPrint_Click" />
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
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
                                <asp:TextBox ID="txtYear" runat="server" ReadOnly="True" Width="10%"></asp:TextBox>
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" TabIndex="1" Visible="False"
                                    Width="12%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Transaction No
                            </td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :
                            </td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtTransNo" runat="server" ReadOnly="True" Width="15%"></asp:TextBox>
                                <asp:DropDownList ID="ddlTransNo" runat="server" AutoPostBack="True" CssClass="txtColor"
                                    OnSelectedIndexChanged="ddlTransNo_SelectedIndexChanged" TabIndex="2" Visible="False"
                                    Width="15%">
                                </asp:DropDownList>
                                <asp:Label ID="lblMxTransNo" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblTransNo" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Quotation No
                            </td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :
                            </td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtQuotation" runat="server" ReadOnly="True" Width="26%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Company Name
                            </td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :
                            </td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="txtColor" TabIndex="2"
                                    Width="40%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Company Address
                            </td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :
                            </td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtCompanyAddr" runat="server" CssClass="txtColor" TabIndex="3"
                                    Width="40%" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Company Contact
                            </td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :
                            </td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtCompanyCont" runat="server" CssClass="txtColor" TabIndex="4"
                                    Width="40%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Atten. Person Name
                            </td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :
                            </td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtAttenPersonNm" runat="server" CssClass="txtColor" TabIndex="5"
                                    Width="40%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Atten. Person Desig.
                            </td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :
                            </td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtAttenPersonDesig" runat="server" CssClass="txtColor" TabIndex="6"
                                    Width="40%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Subject
                            </td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :
                            </td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtSub" runat="server" CssClass="txtColor" TabIndex="7" Width="40%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Prepared By
                            </td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :
                            </td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtPrepNM" runat="server" CssClass="txtColor" TabIndex="8" Width="40%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Prep. Desig.
                            </td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :
                            </td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtPrepDesig" runat="server" CssClass="txtColor" TabIndex="9" Width="40%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Prep. Contact
                            </td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :
                            </td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtPrepContact" runat="server" CssClass="txtColor" TabIndex="10"
                                    Width="40%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                Prep. Company Name</td>
                            <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                :</td>
                            <td style="width: 69%; font-family: Calibri; font-size: 14px">
                                <asp:TextBox ID="txtPrepCompanyNm" runat="server" CssClass="txtColor" 
                                    TabIndex="11" Width="40%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: right; font-family: Calibri; font-size: 14px;
                                font-weight: bold">
                                <asp:Label ID="lblQtTP" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblMxGridSL" runat="server" Visible="False"></asp:Label>
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
                            <asp:TemplateField HeaderText="Quote. Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblQTTP" runat="server" Text='<%# Eval("QTTP") %>' Style="text-align: center"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlQtTpEdit" runat="server" CssClass="txtColor txtalign" Width="98%"
                                        TabIndex="42">
                                        <asp:ListItem>PRICE</asp:ListItem>
                                        <asp:ListItem>TERMS</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlQtTp" runat="server" CssClass="txtColor txtalign" Width="98%"
                                        TabIndex="12" AutoPostBack="True" OnSelectedIndexChanged="ddlQtTp_SelectedIndexChanged">
                                        <asp:ListItem>PRICE</asp:ListItem>
                                        <asp:ListItem>TERMS</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <asp:Label ID="lblSL" runat="server" Text='<%# Eval("QTSL") %>' Style="text-align: center"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblSLEdit" runat="server" Text='<%# Eval("QTSL") %>' Style="text-align: center"></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                <FooterStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("QTDESC") %>' Style="text-align: left"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                        Font-Size="12px" TabIndex="43" Width="98%" Text='<%# Eval("QTDESC") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDesc" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="13"
                                        Width="98%" CssClass="txtColor"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                <ItemStyle HorizontalAlign="Left" Width="40%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UNIT") %>' Style="text-align: left"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUnitEdit" runat="server" Font-Names="Calibri" CssClass="txtColor"
                                        Font-Size="12px" TabIndex="43" Width="98%" Text='<%# Eval("UNIT") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtUnit" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="14"
                                        Width="98%" CssClass="txtColor"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                <ItemStyle HorizontalAlign="Left" Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("QTRATE") %>' CssClass="txtalignright"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRateEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalignright"
                                        AutoPostBack="true" Font-Size="12px" TabIndex="44" Width="98%" Text='<%# Eval("QTRATE") %>'
                                        OnTextChanged="txtRateEdit_TextChanged"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtRate" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="15"
                                        Width="98%" CssClass="txtColor txtalignright" AutoPostBack="True" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("QTQTY") %>' Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtQTYEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalignright"
                                        AutoPostBack="true" Font-Size="12px" TabIndex="45" Width="98%" Text='<%# Eval("QTQTY") %>'
                                        OnTextChanged="txtQTYEdit_TextChanged"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtQTy" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="16"
                                        Width="98%" CssClass="txtColor txtalignright" AutoPostBack="True" OnTextChanged="txtQTy_TextChanged"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total (Qrs)">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("QTQRS") %>' Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTotalEdit" runat="server" Font-Names="Calibri" CssClass="txtColor txtalignright"
                                        ReadOnly="true" Font-Size="12px" TabIndex="46" Width="98%" Text='<%# Eval("QTQRS") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTotal" runat="server" Font-Names="Calibri" Font-Size="12px" TabIndex="17"
                                        ReadOnly="true" Width="98%" CssClass="txtColor txtalignright"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
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
                                        Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="18" ToolTip="Save &amp; Continue"
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
