<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdmissionTest.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.AdmissionTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            $("#txtTESTDTfooter,#txtTESTDTEdit,#txtPInDT,#txtPLCDT,#txtInDT_Trans,#txtTInDt_Ret").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-5:+10" });
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
            width: 100%;
        }

        .auto-style2 {
            font-size: xx-large;
            color: #FFFFFF;
            text-align: center;
            background-color: #2aabd2;
        }

        .auto-style3 {
            font-size: larger;
        }

        .auto-style4 {
            width: 77px;
        }

        .auto-style5 {
            width: 130px;
        }

        .auto-style6 {
            width: 95px;
        }
        .auto-style7 {
            width: 285px;
        }
        .auto-style8 {
            width: 107px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div style="border: double; border-width: 2px; border-radius: 10px">
                <div style="border: double; border-width: 2px; border-top-right-radius: 10px; border-top-left-radius: 10px" class="auto-style2"><span class="auto-style3">A</span>dmission <span class="auto-style3">T</span>est</div>


                <table class="auto-style1">
                    <tr>
                        <td style="text-align: right" class="auto-style6">&nbsp;</td>
                        <td class="auto-style4">
                            &nbsp;</td>
                        <td style="text-align: right; width: 12%;" class="auto-style8">&nbsp;</td>
                        <td class="auto-style7">
                            <asp:Label ID="lblMSG1" runat="server" Font-Bold="True" Font-Size="9pt" ForeColor="#009900" Visible="False"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6" style="text-align: right">Year&nbsp; :</td>
                        <td class="auto-style4">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" Height="100%" Width="158%">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style8" style="text-align: right">Semester&nbsp; :</td>
                        <td class="auto-style7">
                            <asp:DropDownList ID="ddlSemis" runat="server" AutoPostBack="True" CssClass="form-control" Height="100%" OnSelectedIndexChanged="ddlSemis_SelectedIndexChanged" Width="100%">
                                <asp:ListItem>Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblSemID" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <div style="padding:10px">
                            <asp:GridView ID="gvInfo" Width="100%" runat="server" AutoGenerateColumns="False" HeaderStyle-Height="100px" ShowFooter="True"
                                AllowSorting="True"
                                OnRowCancelingEdit="gvInfo_RowCancelingEdit" OnRowDeleting="gvInfo_RowDeleting" OnRowUpdating="gvInfo_RowUpdating" OnRowCommand="gvInfo_RowCommand" OnRowEditing="gvInfo_RowEditing">
                                <Columns>
                                    <asp:TemplateField HeaderText="  PROGRAM NAME    ">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPROGRAMNMedit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("PROGRAMNM") %>' AutoPostBack="True" OnTextChanged="txtPROGRAMNMedit_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtPROGRAMNMfooter_AutoCompleteExtender"  CompletionListCssClass="AutoColor" runat="server" CompletionInterval="10"
                                                CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetCompletionProgram" TargetControlID="txtPROGRAMNMedit" UseContextKey="True">
                                            </asp:AutoCompleteExtender>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPROGRAMNM" runat="server" Text='<%# Eval("PROGRAMNM") %>' Width="70%"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPROGRAMNMfooter" CssClass="form-control" Width="100%" runat="server" OnTextChanged="txtPROGRAMNMfooter_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtPROGRAMNMfooter_AutoCompleteExtender"  CompletionListCssClass="AutoColor"  runat="server" CompletionInterval="10"
                                                CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetCompletionProgram" TargetControlID="txtPROGRAMNMfooter" UseContextKey="True">
                                            </asp:AutoCompleteExtender>
                                        </FooterTemplate>
                                        <HeaderStyle Width="48%" />
                                    </asp:TemplateField >
                                    <asp:TemplateField Visible="false" >
                                        <FooterTemplate>
                                            <asp:Label ID="lblPROGRAMIDfooter" CssClass="form-control" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPROGRAMID" runat="server" Text='<%# Eval("PROGRAMID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <FooterTemplate>
                                            <asp:Label ID="lblPROGRAMTPfooter" CssClass="form-control" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPROGRAMTP" runat="server" Text='<%# Eval("PROGRAMTP") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" EXAM DATE ">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtTESTDTEdit" CssClass="form-control" AutoPostBack="true" Width="100%" runat="server" ClientIDMode="Static" Text='<%# Eval("TESTDT") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTESTDT" runat="server" Text='<%# Eval("TESTDT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTESTDTfooter" CssClass="form-control" AutoPostBack="true" ClientIDMode="Static" Width="100%" runat="server" OnTextChanged="txtTESTDTfooter_TextChanged"></asp:TextBox>
                                        </FooterTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" EXAM TIME ">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtTESTTMedit" runat="server" AutoPostBack="true" CssClass="form-control" Width="100%" Text='<%# Eval("TESTTM") %>' MaxLength="10"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTESTTM" runat="server" Text='<%# Eval("TESTTM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTESTTMfooter" CssClass="form-control" AutoPostBack="true" Width="100%" runat="server" OnTextChanged="txtTESTTMfooter_TextChanged" MaxLength="10"></asp:TextBox>
                                        </FooterTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" VENUE ">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtVENUEedit" CssClass="form-control" runat="server" Width="100%" Text='<%# Eval("VENUE") %>' MaxLength="50"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVENUE" runat="server" Text='<%# Eval("VENUE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtVENUEfooter" CssClass="form-control" Width="100%" runat="server" MaxLength="50"></asp:TextBox>
                                        </FooterTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" REMARKS ">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtREMARKSedit" runat="server" CssClass="form-control" Width="100%" Text='<%# Eval("REMARKS") %>' MaxLength="100"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtREMARKSfooter" CssClass="form-control" Width="100%" runat="server" MaxLength="100"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnPUpdate" CssClass="txtColor" runat="server" CommandName="Update" Height="20px"
                                                ImageUrl="~/Images/update.PNG" TabIndex="67" ToolTip="Update" Width="20px" />
                                            <asp:ImageButton
                                                ID="imgbtnPCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.PNG"
                                                TabIndex="68" ToolTip="Cancel" Width="20px" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imgbtnPAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                                Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="4" ToolTip="Save &amp; Continue"
                                                ValidationGroup="validaiton" Width="30px" />
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px"
                                                ImageUrl="~/Images/Edit.PNG" TabIndex="10" ToolTip="Edit" Width="20px" /><asp:ImageButton
                                                    ID="imgbtnPDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()"
                                                    Height="20px" ImageUrl="~/Images/delete.PNG" TabIndex="11" ToolTip="Delete" Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="8%"/>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="#2aabd2" BorderColor="Black" BorderStyle="Double" BorderWidth="2px" ForeColor="White" Height="30px" />
                            </asp:GridView>
                                </div>
                            
                            <div>
                                <asp:Label ID="lblMSG" runat="server" Visible="False"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
