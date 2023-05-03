<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Receivable.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.receivable" %>
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
        .Radius{
            border-radius:10px;
            border:1px solid #808080;
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
            width: 134px;
            text-align: right;
        }

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div style="border: double; border-width: 2px; border-radius: 10px">
                <div style="border: 2px double white; border-top-right-radius: 10px; border-top-left-radius: 10px; background-color: #2aabd2; color: #FFFFFF; text-align: center; font-size: xx-large;" class="auto-style1"><span class="auto-style1">R</span>eceivable/<span class="auto-style1">P</span>ayable <span class="auto-style1">E</span>ntry</div>
                <table class="auto-style2">
                    <tr>
                        <td>&nbsp;</td>
                        <td width="5%">&nbsp;</td>
                        <td width="5%">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td width="5%">
                            <asp:Button ID="btnEdit" runat="server" CssClass="form-control" Height="35px" OnClick="btnEdit_Click" Text="EDIT" Width="120px" BorderColor="#2aabd2" BorderWidth="3px" Font-Bold="True" />
                        </td>
                        <td width="5%">
                            <asp:Button ID="btnPrint" runat="server" CssClass="form-control" Enabled="false" Height="35px" OnClick="btnPrint_Click" Text="PRINT" Width="120px" BorderColor="#2aabd2" BorderWidth="3px" Font-Bold="True" Visible="False" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                &nbsp;<table class="auto-style2">
                    <tr>
                        <td class="text-left" width="20%">&nbsp;</td>
                        <td class="text-left" width="30%">
                            <asp:Label ID="lblStuID" runat="server" Visible="False"></asp:Label>
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
                                <asp:TextBox ID="txtDT" runat="server" ClientIDMode="Static" CssClass="form-control" OnTextChanged="txtDT_TextChanged" Width="120px" style="display:inline-block" AutoPostBack="True"  MaxLength="10"></asp:TextBox>
                                <asp:TextBox ID="txtTransNO" runat="server" CssClass="form-control" Enabled="False" EnableTheming="True" style="display:inline-block"  Width="30%"></asp:TextBox>
                                <asp:DropDownList ID="ddlTransNO" runat="server" AutoPostBack="True" CssClass="form-control" Height="33px" style="display:inline-block"  OnSelectedIndexChanged="ddlTransNO_SelectedIndexChanged" Visible="False" Width="100px">
                                </asp:DropDownList>
                            </div>
                            <div class="text-left">
                            </div>
                        </td>
                        <td class="auto-style8" width="16%">Trans For :</td>
                        <td width="30%" class="text-center">
                            <asp:DropDownList ID="ddlTransFor" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlTransFor_SelectedIndexChanged" Width="50%">
                                <asp:ListItem>RECEIVABLE</asp:ListItem>
                                <asp:ListItem>PAYABLE</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5" width="20%">Reg. Year :&nbsp;&nbsp;&nbsp; </td>
                        <td width="30%">
                            <asp:DropDownList ID="ddlRegYR" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlRegYR_SelectedIndexChanged" Width="120px" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtYR" runat="server" CssClass="form-control" Height="36px" Visible="False" Width="54%"></asp:TextBox>
                            <asp:Label ID="lblYR" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style10" width="16%">Batch/Semester&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp; </td>
                        <td class="text-center" width="30%">
                            <asp:DropDownList ID="ddlBatch" runat="server" AutoPostBack="True" CssClass="form-control" style="display:inline-block"  Width="42%" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSemNM" runat="server" Enabled="false" CssClass="form-control"  style="display:inline-block" Width="55%">
                            </asp:DropDownList> 
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="20%" class="auto-style5">Program Name :</td>
                        <td width="30%" class="text-left">
                            <asp:DropDownList ID="ddlProgNM" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlProgNM_SelectedIndexChanged" Width="100%">
                            </asp:DropDownList>
                            <asp:Label ID="lblPRONM" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td width="15%" class="auto-style5">Fees Name&nbsp; :&nbsp;&nbsp;&nbsp;</td>
                        <td width="30%" class="text-left">
                            <asp:DropDownList ID="ddlFeesForRec" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlFeesForRec_SelectedIndexChanged" Width="100%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlFeesForPay" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlFeesForPay_SelectedIndexChanged" Visible="False" Width="100%">
                            </asp:DropDownList>
                            <asp:Label ID="lblFeesID" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="20%" class="auto-style5">Remarks :&nbsp;&nbsp; </td>
                        <td colspan="3" class="text-left">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Width="100%" MaxLength="100"></asp:TextBox>
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5" width="20%">&nbsp;</td>
                        <td width="30%">
                            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="#009900" Text="Label" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style9" width="30%">
                            <asp:Button ID="btnComplete" runat="server" BackColor="#2aabd2" CssClass="form-control-right" Font-Bold="True" ForeColor="White" Height="30px" OnClick="btnComplete_Click" Text="Complete" Visible="False" />
                        </td>
                        <td width="8%">
                            <asp:Button ID="btnComplete0" runat="server" BackColor="#2aabd2" CssClass="form-control-right" Font-Bold="True" ForeColor="White" Height="30px" OnClick="btnComplete_Click" Text="Updated" Visible="False" />
                        </td>
                    </tr>
                </table>
                <table class="auto-style2">
                    <tr>
                        <td style="padding: 10px;">
                            <div style="border-radius: 10px">
                                <asp:GridView ID="gv_Trans" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" OnRowCommand="gv_Trans_RowCommand" OnRowDeleting="gv_Trans_RowDeleting" OnRowCancelingEdit="gv_Trans_RowCancelingEdit" OnRowEditing="gv_Trans_RowEditing" OnRowUpdating="gv_Trans_RowUpdating" BorderColor="#333333" BorderWidth="2px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Student ID">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtSTUDENTIDEdit"  Visible="false"  runat="server" ReadOnly="true" CssClass="form-control" Width="100%" Text='<%# Eval("STUDENTID") %>' ></asp:TextBox>
                                                <asp:TextBox ID="txtSTUDENTIDNewEdit" runat="server" ReadOnly="true" AutoPostBack="true" CssClass="form-control" Width="100%" Text='<%# Eval("NEWSTUDENTID") %>' OnTextChanged="txtSTUDENTIDNewEdit_TextChanged" ></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTUDENTID" Visible="false" runat="server" Text='<%# Eval("STUDENTID") %>'></asp:Label>
                                                <asp:Label ID="lblSTUDENTIDNew" runat="server" Text='<%# Eval("NEWSTUDENTID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtSTUDENTIDFooter" runat="server"  Visible="false"  CssClass="form-control" Width="100%" ></asp:TextBox>    
                                                <asp:TextBox ID="txtSTUDENTIDNewFooter" runat="server" AutoPostBack="true" CssClass="form-control" Width="100%" OnTextChanged="txtSTUDENTIDNewFooter_TextChanged"  ></asp:TextBox>                                  
                                                 <asp:AutoCompleteExtender ID="AutoCompleteExtender2"
                                                    runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=";, :"
                                                    Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentID" CompletionListCssClass="AutoExtender"
                                                    CompletionListItemCssClass="AutoExtenderList"
                                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                    TargetControlID="txtSTUDENTIDNewFooter" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                            </FooterTemplate>

                                            <HeaderStyle Width="15%" HorizontalAlign="Center" />
                                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Student Name">
                                            <EditItemTemplate> 
                                                <asp:TextBox ID="txtSTUDENTNMEdit" CssClass="form-control" ReadOnly="true" Width="100%" runat="server" Text='<%# Eval("STUDENTNM") %>' ></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTUDENTNM" runat="server"  Text='<%#Eval("STUDENTNM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate> 
                                                <asp:TextBox ID="txtSTUDENTNMFooter" CssClass="form-control" ReadOnly="true" Width="100%" runat="server" ></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle Width="30%" HorizontalAlign="Center" />
                                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAMOUNTEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAMOUNT" runat="server" Text='<%# Eval("AMOUNT")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAMOUNTFooter"  runat="server" CssClass="form-control" Width="100%" ></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle Width="14%" />
                                            <ItemStyle Width="14%" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtREMARKSEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("REMARKS") %>' MaxLength="100"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblREMARKS" runat="server" Text='<%#Eval("REMARKS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtREMARKSFooter"  runat="server" CssClass="form-control" Width="100%" MaxLength="100"></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle Width="25%" HorizontalAlign="Center" />
                                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="imgbtnPUpdate" CssClass="txtColor" runat="server" CommandName="Update" Height="20px"
                                                    ImageUrl="~/Images/update.png"  ToolTip="Update" Width="20px" />
                                                <asp:ImageButton
                                                    ID="imgbtnPCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.png"
                                                     ToolTip="Cancel" Width="20px" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="imgbtnPAdd" runat="server" CommandName="Add" CssClass="txtColor"
                                                    Height="30px" ImageUrl="~/Images/AddNewitem.jpg" ToolTip="Insert"
                                                    ValidationGroup="validaiton" Width="30px" />
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Complete" CssClass="txtColor"
                                                    Height="30px" ImageUrl="~/Images/Complete.png"  ToolTip="Insert"
                                                    ValidationGroup="validaiton" Width="30px" />
                                                <asp:ImageButton ID="ImageButton2" runat="server" Enabled="false" CommandName="Print" CssClass="txtColor"
                                                    Height="30px" ImageUrl="~/Images/print.png"  ToolTip="Insert"
                                                    ValidationGroup="validaiton" Width="30px" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px"
                                                    ImageUrl="~/Images/Edit.png"  ToolTip="Edit" Width="20px" /> &nbsp;&nbsp; <asp:ImageButton
                                                        ID="imgbtnPDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()"
                                                        Height="20px" ImageUrl="~/Images/delete.png"   ToolTip="Delete" Width="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="12%" />
                                            <ItemStyle Width="12%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#2aabd2" Width="100%" BorderColor="#333333" BorderWidth="2px" ForeColor="White" />
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

