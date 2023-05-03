<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Collection.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.Collection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            $("#txtDT,#txtPODT").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-5:+10" });
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
        .auto-style1 {
            font-size: larger;
        }

        .auto-style2 {
            width: 100%;
        }

        .auto-style5 {
            text-align: right;
        }

        .auto-style8 {
            width: 134px;
            text-align: right;
        }

        .auto-style9 {
            text-align: right;
            height: 23px;
        }

        .auto-style10 {
            text-align: right;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div style="border: double; border-width: 2px; border-radius: 10px">
                <div style="border: 2px double white; border-top-right-radius: 10px; border-top-left-radius: 10px; background-color: #2aabd2; color: #FFFFFF; text-align: center; font-size: xx-large;" class="auto-style1"><span class="auto-style1">C</span>ollection <span class="auto-style1">I</span>nformation</div>
                <table class="auto-style2">
                    <tr>
                        <td>&nbsp;</td>
                        <td width="5%">&nbsp;</td>
                        <td width="5%">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td  >&nbsp;</td>
                        <td width="5%" class="center">
                            <asp:Button ID="btnEdit" runat="server" CssClass="form-control" Height="35px" OnClick="btnEdit_Click" Text="EDIT" Width="120px" BorderColor="#2aabd2" BorderWidth="3px" Font-Bold="True" />
                        </td>
                        <td width="5%" class="center">
                            <asp:Button ID="btnPrint" runat="server" CssClass="form-control" Height="35px" OnClick="btnPrint_Click" Text="PRINT" Width="120px" BorderColor="#2aabd2" BorderWidth="3px" Font-Bold="True" />
                        </td>
                        <td width="5%" class="center">
                            <asp:Button ID="btnPrint0" runat="server" BorderColor="Red" BorderWidth="3px" CssClass="form-control" Font-Bold="True" Height="35px"  Text="REFRESH" Width="120px" BackColor="#FF6666" ForeColor="#663300" OnClick="btnPrint0_Click" />
                        </td>
                        <td  >&nbsp;</td>
                    </tr>
                </table>
                &nbsp;<table class="auto-style2">
                    <tr>
                        <td class="text-left" width="20%">&nbsp;</td>
                        <td class="text-left" width="30%">
                            <asp:Label ID="lblStuID" runat="server" Visible="False"></asp:Label>  
                            <asp:Label ID="lblFeesID" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="text-left" width="15%">
                            <asp:Label ID="lblMSG" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                        </td>
                        <td class="text-left" width="30%">
                            <asp:Label ID="lblTransNO" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSL" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="text-left" width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="20%" class="auto-style5">Date :&nbsp;&nbsp; </td>
                        <td width="30%">
                            <div class="text-left">
                                <asp:TextBox ID="txtDT" runat="server" ClientIDMode="Static" style="display:inline-block" CssClass="form-control" OnTextChanged="txtDT_TextChanged" Width="120px" AutoPostBack="True" TabIndex="1" MaxLength="10"></asp:TextBox>
                                <asp:TextBox ID="txtTransNO" runat="server" style="display:inline-block"  CssClass="form-control" Enabled="False" EnableTheming="True" Width="45%"></asp:TextBox>
                                <asp:DropDownList ID="ddlTransNO" runat="server"  style="display:inline-block" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlTransNO_SelectedIndexChanged" Visible="False" Width="100px">
                                </asp:DropDownList>
                            </div>
                            <div class="text-left">
                            </div>
                        </td>
                        <td class="auto-style8" width="16%">Reg. Year :&nbsp;&nbsp;&nbsp; </td>
                        <td width="30%" class="text-center">
                            <asp:TextBox ID="txtYR" runat="server" CssClass="form-control" Width="54%" Height="36px" Visible="False"></asp:TextBox>
                            <asp:DropDownList ID="ddlRegYR" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlRegYR_SelectedIndexChanged" TabIndex="2" Width="150px" AutoPostBack="True" >
                            </asp:DropDownList>
                            <asp:Label ID="lblYR" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="20%" class="auto-style5">Batch/Semester&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp; </td>
                        <td width="30%" class="text-left">
                            <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control" Width="120px" style="display: inline-block;" AutoPostBack="True" TabIndex="3" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSemNM" runat="server" CssClass="form-control" Enabled="false" Width="45%"   style="display: inline-block;" >
                            </asp:DropDownList>
                              
                            <asp:Label ID="lblSEMNM" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td width="15%" class="auto-style5">Program Name :</td>
                        <td width="30%" class="text-left">
                            <asp:DropDownList ID="ddlProgNM" runat="server" CssClass="form-control" Width="100%" OnSelectedIndexChanged="ddlProgNM_SelectedIndexChanged" AutoPostBack="True" TabIndex="4">
                            </asp:DropDownList> 
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="20%" class="auto-style5">Student ID No&nbsp; :&nbsp;&nbsp; </td>
                        <td width="30%">
                            <asp:TextBox ID="txtStuID" runat="server" CssClass="form-control" Width="100%" Visible="false"></asp:TextBox>
                             <asp:TextBox ID="txtStuIDNew" runat="server" CssClass="form-control" Width="100%" OnTextChanged="txtStuIDNew_TextChanged" AutoPostBack="True" MaxLength="12" BorderColor="#3399FF" BorderWidth="3px" ForeColor="#CC0000"></asp:TextBox>
                            <%--                             <asp:AutoCompleteExtender ID="txtStuID_AutoCompleteExtender"  CompletionListCssClass="AutoColor"  runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentID" TargetControlID="txtStuID" UseContextKey="True">
                            </asp:AutoCompleteExtender>--%>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender"
                                runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=""
                                Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentID" CompletionListCssClass="AutoExtender"
                                CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                TargetControlID="txtStuIDNew" UseContextKey="True">
                            </asp:AutoCompleteExtender>
                            <asp:Label ID="lblmsg1" runat="server" Font-Bold="True" Font-Size="7pt" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style5" width="15%">Student Name :</td>
                        <td width="30%">
                            <asp:TextBox ID="txtStuNM" runat="server" CssClass="form-control" Enabled="False" OnTextChanged="txtStuID_TextChanged" Width="100%"></asp:TextBox>

                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1"
                                runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=";, :"
                                Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentID" CompletionListCssClass="AutoExtender"
                                CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                TargetControlID="txtStuNM" UseContextKey="True">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5" width="20%" class="auto-style10">
                            <asp:Label ID="lblAccNO" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" class="auto-style5"><span style="color: rgb(0, 0, 0); font-family: Calibri; font-size: 16px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: -webkit-center; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;"></span>Account Name&nbsp; :</td>
                        <td class="text-left" colspan="3">
                            <asp:TextBox ID="txtAcNM" runat="server" CssClass="form-control" MaxLength="15" TabIndex="6" Width="100%" AutoPostBack="True" OnTextChanged="txtAcNM_TextChanged"></asp:TextBox>

                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2"
                                runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=";, :"
                                Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionAccNM" CompletionListCssClass="AutoExtender"
                                CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                TargetControlID="txtAcNM" UseContextKey="True">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5" width="20%">PO/DD No :&nbsp;&nbsp;&nbsp; </td>
                        <td class="text-left" width="30%">
                            <asp:TextBox ID="txtPODDNO" runat="server" CssClass="form-control" MaxLength="15" TabIndex="6" Width="100%"></asp:TextBox>
                        </td>
                        <td class="auto-style5" width="15%">PO Date :&nbsp;&nbsp; </td>
                        <td class="text-left" width="30%">
                            <asp:TextBox ID="txtPODT" runat="server" AutoPostBack="True" ClientIDMode="Static" CssClass="form-control" MaxLength="10" OnTextChanged="txtPODT_TextChanged" TabIndex="7" Width="100%"></asp:TextBox>
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="20%" class="auto-style5">PO Bank :&nbsp;&nbsp; </td>
                        <td width="30%" class="text-left">
                            <asp:TextBox ID="txtPOBNK" runat="server" CssClass="form-control" Width="100%" TabIndex="8" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="auto-style5" width="15%">PO Branch :</td>
                        <td width="30%" class="text-left">
                            <asp:TextBox ID="txtPOBRNC" runat="server" CssClass="form-control" Width="100%" TabIndex="9" MaxLength="100"></asp:TextBox>
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="20%" class="auto-style5">Remarks :&nbsp;&nbsp; </td>
                        <td colspan="3" class="text-left">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Width="100%" TabIndex="10" MaxLength="100"></asp:TextBox>
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5" width="20%">&nbsp;</td>
                        <td width="30%">
                            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="#009900" Text="Label" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style9" width="15%">&nbsp;</td>
                        <td width="30%" class="auto-style9">
                            <asp:Button ID="btnComplete" runat="server" CssClass="form-control-right" OnClick="btnComplete_Click" Text="Complete" BackColor="#2aabd2" Font-Bold="True" ForeColor="White" Height="30px" Visible="False" />
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                </table>
                <table class="auto-style2">
                    <tr>
                        <td style="padding: 10px;">
                            <div style="border-radius: 10px">
                                <asp:GridView ID="gv_Trans" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" OnRowCommand="gv_Trans_RowCommand" OnRowDeleting="gv_Trans_RowDeleting" OnRowCancelingEdit="gv_Trans_RowCancelingEdit" OnRowEditing="gv_Trans_RowEditing" OnRowUpdating="gv_Trans_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Fees Name">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtFEESNMEdit" runat="server" AutoPostBack="true" CssClass="form-control" Width="100%" Text='<%# Eval("FEESNM") %>' OnTextChanged="txtFEESNMEdit_TextChanged"></asp:TextBox>

                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2"
                                                    runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=";, :"
                                                    Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionFEESNM" CompletionListCssClass="AutoExtender"
                                                    CompletionListItemCssClass="AutoExtenderList"
                                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                    TargetControlID="txtFEESNMEdit" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFEESNM" runat="server" Text='<%# Eval("FEESNM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFEESNMFooter" TabIndex="11" runat="server" AutoPostBack="true" CssClass="form-control" Width="100%"  OnTextChanged="txtFEESNMFooter_TextChanged"></asp:TextBox>
                                                
                                                 <asp:AutoCompleteExtender ID="AutoCompleteExtender2"
                                                    runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=";, :"
                                                    Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionFEESNM" CompletionListCssClass="AutoExtender"
                                                    CompletionListItemCssClass="AutoExtenderList"
                                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                    TargetControlID="txtFEESNMFooter" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                            </FooterTemplate>

                                            <HeaderStyle Width="25%" HorizontalAlign="Center" />
                                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField    >
                                       <EditItemTemplate>
                                            <asp:Label ID="lblFEESSLEdit" runat="server" Text='<%# Eval("FEESSL") %>'></asp:Label>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                            <asp:Label ID="lblFEESSL" runat="server" Text='<%# Eval("FEESSL") %>'></asp:Label>
                                        </ItemTemplate>
                                      <FooterTemplate>
                                           <asp:Label ID="lblFEESSLFooter" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>--%>
                                        <asp:TemplateField Visible="false">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblFEESIDEdit" runat="server" Text='<%# Eval("FEESID") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFEESID" runat="server" Text='<%# Eval("FEESID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFEESIDFooter" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAMOUNTEdit" CssClass="form-control" AutoPostBack="true" Width="100%" runat="server" Text='<%# Eval("AMOUNT") %>' OnTextChanged="txtAMOUNTEdit_TextChanged"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAMOUNT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAMOUNTFooter" TabIndex="12" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" OnTextChanged="txtAMOUNTFooter_TextChanged"  ></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    <%--    <asp:TemplateField HeaderText="Vat">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtVatEdit" ReadOnly="true" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("VATAMOUNT") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVat" runat="server" Text='<%# Eval("VATAMOUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtVatFooter" ReadOnly="true" runat="server" CssClass="form-control" Width="100%" ></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Total">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtTotalEdit" ReadOnly="true" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("TOTAL") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("TOTAL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTotalFooter" ReadOnly="true" runat="server" CssClass="form-control" Width="100%" ></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtREMARKSEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("REMARKS") %>' MaxLength="100"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtREMARKSFooter" TabIndex="13" runat="server" CssClass="form-control" Width="100%"  MaxLength="100"></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle Width="20%" HorizontalAlign="Center" />
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="imgbtnPUpdate" CssClass="txtColor" runat="server" CommandName="Update" Height="20px"
                                                    ImageUrl="~/Images/update.png" TabIndex="67" ToolTip="Update" Width="20px" />
                                                <asp:ImageButton
                                                    ID="imgbtnPCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.png"
                                                    TabIndex="68" ToolTip="Cancel" Width="20px" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="imgbtnPAdd" runat="server" CommandName="Add" CssClass="txtColor"
                                                    Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="14" ToolTip="Insert"
                                                    ValidationGroup="validaiton" Width="30px" />
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Complete" CssClass="txtColor"
                                                    Height="30px" ImageUrl="~/Images/Complete.png" TabIndex="15" ToolTip="Complete"
                                                    ValidationGroup="validaiton" Width="30px" />
                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Print" CssClass="txtColor"
                                                    Height="30px" ImageUrl="~/Images/print.png" TabIndex="16" ToolTip="Print"
                                                    ValidationGroup="validaiton" Width="30px" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px"
                                                    ImageUrl="~/Images/Edit.png" TabIndex="10" ToolTip="Edit" Width="20px" /><asp:ImageButton
                                                        ID="imgbtnPDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()"
                                                        Height="20px" ImageUrl="~/Images/delete.png" TabIndex="11" ToolTip="Delete" Width="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="8%" />
                                            <ItemStyle Width="8%"  HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#2aabd2" BorderColor="Black" BorderWidth="2px" ForeColor="White" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                <table class="auto-style2">
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
