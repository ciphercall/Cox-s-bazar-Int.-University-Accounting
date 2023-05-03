<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationForReg.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.ApplicationForReg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            
        }
        //function pageLoad() {
        //    $("#txtDtOfBth").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+1" });
        //}
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
            color: #FFFFFF;
            font-size: xx-large;
            background-color: #2aabd2;
        }

        .auto-style2 {
            font-size: larger;
        }

        .auto-style4 {
            text-align: right;
        }

        .auto-style5 {
            width: 33%;
            text-align: left;
        }


        .auto-style7 {
            text-decoration: underline;
        }

        .auto-style12 {
            text-align: right;
            height: 35px;
        }

        .auto-style13 {
            width: 33%;
            height: 35px;
        }

        .auto-style15 {
            height: 35px;
        }

        .auto-style16 {
            width: 47px;
        }

        }

        .auto-style6 {
            text-align: center;
        }

        .Center {
            text-align: center;
        }

        .auto-style7 {
            text-align: left;
            text-decoration: underline;
            font-weight: 700;
            font-size: 12px;
        }

        .auto-style18 {
            color: #FF0000;
        }

        .auto-style22 {
            text-align: right;
            color: #FF0000;
            background-color: #CCCCCC;
        }

        .auto-style23 {
            background-color: #CCCCCC;
            text-align: right;
        }

        .auto-style24 {
            text-align: right;
            background-color: #CCCCCC;
        }

        .auto-style25 {
            text-align: center;
            background-color: #CCCCCC;
        }

        .auto-style26 {
            width: 33%;
            text-align: right;
            background-color: #CCCCCC;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div style="border-width: 2px; border-radius: 10px; border-color: black; border: double">
                <div style="border-width: 2px; border: double; border-top-left-radius: 10px; border-top-right-radius: 10px; text-align: center;" class="auto-style1">
                    <span class="auto-style2">A</span>pplication <span class="auto-style2">F</span>or <span class="auto-style2">R</span>egistration 
                </div>
                <table class="nav-justified">
                    <tr>
                        <td class="text-left" width="17%">&nbsp;</td>
                        <td class="auto-style6" width="33%">
                            <asp:Label ID="lblAdmtSL" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblStuID" runat="server" Visible="False"></asp:Label> 
                        </td>
                        <td class="text-left" colspan="3" width="13%">
                            <asp:Label ID="lblMSG1" runat="server" Font-Bold="True" Font-Size="9pt" ForeColor="#CC0000" Visible="False"></asp:Label>
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style4" width="17%">
                            <asp:Label ID="lblStuIDNO" runat="server" Visible="False">Student ID  :</asp:Label>
                        </td>
                        <td class="auto-style6" width="33%">
                            <asp:TextBox ID="txtStuID" runat="server" CssClass="form-control" Height="30px" OnTextChanged="txtStuID_TextChanged" Width="100%" Visible="False" AutoPostBack="True"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtStuID_AutoCompleteExtender" CompletionListCssClass="AutoColor" runat="server" CompletionInterval="10"
                                CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                ServiceMethod="GetCompletionStudentID" TargetControlID="txtStuID" UseContextKey="True">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td colspan="2" width="13%">
                            <asp:Label ID="lblSL" runat="server" Visible="False"></asp:Label>
                        </td>

                        <td class="auto-style5" width="33%">
                            <asp:Button ID="btnEdit" runat="server" BorderColor="#3399FF" BorderWidth="2px" CssClass="form-control-right" Height="100%" OnClick="btnEdit_Click" Text="Edit" ValidationGroup="Edit" Width="100px" />
                            <asp:Button ID="btnRefresh" runat="server" BorderColor="#3399FF" BorderWidth="2px" CssClass="form-control-right" Height="100%" Text="Refresh" ValidationGroup="Edit" Width="100px" OnClick="btnRefresh_Click" />
                        </td>
                        <td width="8%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Admission Year <span class="auto-style18">*</span>:</td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlAdmYR" runat="server" CssClass="form-control" Height="30px" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlAdmYR_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style4" colspan="2" width="14%">Admission Date <span class="auto-style18">*</span>:</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtAdmDT" runat="server" ClientIDMode="Static" CssClass="form-control" Height="30px" Width="100%"  MaxLength="10"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Program Name <span class="auto-style18">*</span>:</td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlProNM" runat="server" CssClass="form-control" Height="30px" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style4" colspan="2">Batch :</td>
                        <td class="">
                            <asp:DropDownList ID="ddlBCH" runat="server" AutoPostBack="True" CssClass="form-control select2" Height="30px" MaxLength="3"  Width="50%" OnSelectedIndexChanged="ddlBCH_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Semester Name <span class="auto-style18">*</span>:</td>
                        <td class="auto-style6" width="33%">
                            <asp:DropDownList ID="ddlSemNM" runat="server" AutoPostBack="True" CssClass="form-control" Height="30px" OnSelectedIndexChanged="ddlSemNM_SelectedIndexChanged" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style4" colspan="2">Session :</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtSession" runat="server" CssClass="form-control" Height="30px" MaxLength="6" Style="display: inline-block" Width="50%"></asp:TextBox>
                            <asp:TextBox ID="txtStuMaxID" runat="server" CssClass="form-control" Enabled="False" Height="30px" MaxLength="11" Visible="false" Width="50%"></asp:TextBox>
                            <asp:TextBox ID="txtStuIdNew" runat="server" CssClass="form-control" Enabled="False" Height="30px" Style="display: inline-block; margin-left: 3px;" Width="48%"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Student Name <span class="auto-style18">*</span>:</td>
                        <td class="auto-style6" width="33%">
                            <asp:TextBox ID="txtStuNM" runat="server" CssClass="form-control" Height="30px" Width="100%" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">Admission Type :</td>
                        <td class="auto-style5">
                            <asp:DropDownList ID="ddlAdmtTP" runat="server" CssClass="form-control" Height="30px"  Width="100%">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Regular</asp:ListItem>
                                <asp:ListItem Value="2">Credit Transfer</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style4">Father Name :</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtStuFNM" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">Occupation :</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtFOcup" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Occupation Details :</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtFOcupDTL" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Mother Name :</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtStuMNM" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">Occupation :</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtMOcup" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Spouse&#39;s Name :</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtSPNM" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">Occupation :</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtSPOcup" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Present Address:</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtPreAdrs1" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtPreAdrs2" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Permanent Address:</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtPerAdrs1" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtPerAdrs2" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Telephone Number :</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtTelNO" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="15"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">Mobile Number <span class="auto-style18">*</span>:</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtMoNO" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="11"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">E-mail :</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtEML" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">Nationality :</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtNatin" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="15">Bangladeshi</asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Religion :</td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlReli" runat="server" CssClass="form-control" Height="30px" Width="100%">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Islam</asp:ListItem>
                                <asp:ListItem>Christian</asp:ListItem>
                                <asp:ListItem>Hinduism</asp:ListItem>
                                <asp:ListItem>Buddhism</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style4" colspan="2">Date Of Birth :</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtDOB" runat="server" Height="30px" Width="100%" ClientIDMode="Static" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Gender :</td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlGNDR" runat="server" CssClass="form-control" Height="30px" Width="100%">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style4" colspan="2">Studente Type :</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtStuTP" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Place Of Birth :</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtPOB" runat="server" Height="30px" Width="100%" CssClass="form-control" AutoPostBack="True" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">Blood Group :</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtBG" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="20"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">National ID/P.P. No :</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtNPidNO" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="20"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Permanent Residence :</td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlPRcdnc" runat="server" CssClass="form-control" Height="30px" Width="100%">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem Value="Rural">Rural</asp:ListItem>
                                <asp:ListItem>Urban</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="PRcdnc" InitialValue="-1" runat="server" ControlToValidate="ddlPRcdnc"></asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style4" colspan="2">Marital Status :</td>
                        <td class="auto-style5">
                            <asp:DropDownList ID="ddlMSTTS" runat="server" CssClass="form-control" Height="30px" Width="100%">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        &nbsp;
                    </tr>
                    <tr>
                        <td colspan="5" class="text-left"><strong>Do You Want To Reside In The Hostel :</strong></td>
                        <td class="auto-style15"></td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style16">
                            <asp:DropDownList ID="ddlHSTL" runat="server" CssClass="form-control" Height="30px" Width="100%">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style16">&nbsp;</td>
                        <td class="auto-style6" colspan="2">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="text-left" colspan="2"><strong>Yearly Family Income &amp; Expenditure&nbsp; TK :</strong></td>
                        <td class="auto-style12" colspan="2"></td>
                        <td class="auto-style13"></td>
                        <td class="auto-style15"></td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Income&nbsp; :</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtINCM" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="18"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">Expenditure&nbsp;&nbsp; :</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtEXP" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="18"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="text-left" colspan="5"><span class="auto-style6"><strong>Guardian Information (Legal / Local) :</strong></span></td>
                        <td class="auto-style15"></td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Name Of&nbsp; Guardian :</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtGNM" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Relationship&nbsp; :</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtGurREL" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Full address :</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtGAdrs1" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtGAdrs2" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Telephone Number :</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtGTelNO" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="15"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">Mobile Number :</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtGMoNo" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="11"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">E-mail :</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtGML" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">&nbsp;</td>
                        <td class="auto-style5"></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style6">&nbsp;</td>
                        <td class="auto-style4" colspan="2">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style7" colspan="2"><strong>Particulars of Examination Passes :</strong></td>
                        <td class="auto-style4" colspan="2">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style6" colspan="5">
                            <asp:GridView ID="GridEdit" runat="server" AutoGenerateColumns="False" Font-Size="8pt" ShowFooter="True" Width="100%" OnRowCommand="GridView1_RowCommand" OnRowCancelingEdit="GridEdit_RowCancelingEdit" OnRowDeleting="GridEdit_RowDeleting" OnRowEditing="GridEdit_RowEditing" OnRowUpdating="GridEdit_RowUpdating">
                                <Columns>
                                    <asp:TemplateField Visible="false">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblEXAMSLEdit" runat="server" Text='<%# Bind("EXAMSL") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEXAMSL" runat="server" Text='<%# Bind("EXAMSL") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Exam Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEXAMNMEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Bind("EXAMNM") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEXAMNM" runat="server" Text='<%# Bind("EXAMNM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtEXAMNMFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Passed Year">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPASSYYEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Bind("PASSYY") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPASSYY" runat="server" Text='<%# Bind("PASSYY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPASSYYFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Exam Roll">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEXAMROLLEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Bind("EXAMROLL") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEXAMROLL" runat="server" Text='<%# Bind("EXAMROLL") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtEXAMROLLFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGROUPSUBEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Bind("GROUPSUB") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblGROUPSUB" runat="server" Text='<%# Bind("GROUPSUB") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtGROUPSUBFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Div/Grade/GPA">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDIVGRADEEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Bind("DIVGRADE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDIVGRADE" runat="server" Text='<%# Bind("DIVGRADE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDIVGRADEFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Isntitute Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtINSTITUTEEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Bind("INSTITUTE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblINSTITUTE" runat="server" Text='<%# Bind("INSTITUTE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtINSTITUTEFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Board/Uni.">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBOARDUNIEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Bind("BOARDUNI") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBOARDUNI" runat="server" Text='<%# Bind("BOARDUNI") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtBOARDUNIFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnPUpdate" runat="server" CommandName="Update" Height="20px"
                                                ImageUrl="~/Images/update.png" ValidationGroup="validaiton" TabIndex="67" ToolTip="Update" Width="20px" />
                                            <asp:ImageButton
                                                ID="imgbtnPCancel" runat="server" ValidationGroup="validaiton" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.png"
                                                TabIndex="68" ToolTip="Cancel" Width="20px" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imgbtnPAdd" runat="server" CommandName="Add" CssClass="txtColor"
                                                Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="14" ToolTip="Insert"
                                                ValidationGroup="validaiton" Width="30px" />

                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px"
                                                ImageUrl="~/Images/Edit.png" TabIndex="10" ToolTip="Edit" Width="20px" ValidationGroup="validaiton" /><asp:ImageButton
                                                    ID="imgbtnPDelete" runat="server" CommandName="Delete" ValidationGroup="validaiton" OnClientClick="return confMSG()"
                                                    Height="20px" ImageUrl="~/Images/delete.png" TabIndex="11" ToolTip="Delete" Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="8%" />
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#CCCCCC" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style6" colspan="5">
                            <asp:GridView ID="GridViewEQ" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewEQ_RowCommand" OnRowDeleting="GridViewEQ_RowDeleting" Width="100%">
                                <Columns>
                                    <%--  <asp:TemplateField>
                                        <ItemTemplate>
                                             <asp:Button runat="server" CommandName="Change" ID="btnEdit" Text="Edit" CommandArgument='<%# Container.DataItemIndex %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btn" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="delete" CssClass="form-control" Text="Delete" ValidationGroup="Delete" Width="100%" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                        <%-- <FooterTemplate>
            <asp:TextBox ID="txtnm" runat="server"></asp:TextBox>
            </FooterTemplate>--%><%--   <ItemTemplate>
                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" />
            </ItemTemplate>--%>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="1" HeaderStyle-Width="10%" HeaderText="Exam Name" ItemStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-Wrap="true">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle CssClass="text-center" Width="10%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="2" HeaderStyle-Width="10%" HeaderText="Passed Year" ItemStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-Wrap="true">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle CssClass="text-center" Width="7%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="3" HeaderStyle-Width="12%" HeaderText="Exam Roll" ItemStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-Wrap="true">
                                        <HeaderStyle Width="12%" />
                                        <ItemStyle CssClass="text-center" Width="12%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="4" HeaderStyle-Width="12%" HeaderText=" Group/Sub" ItemStyle-CssClass="text-center" ItemStyle-Width="12%" ItemStyle-Wrap="true">
                                        <HeaderStyle Width="12%" />
                                        <ItemStyle CssClass="text-center" Width="12%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="5" HeaderStyle-Width="10%" HeaderText="Div/Grade/GPA" ItemStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-Wrap="true">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle CssClass="text-center" Width="10%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="6" HeaderStyle-Width="15%" HeaderText="Institute Name" ItemStyle-CssClass="text-center" ItemStyle-Width="15%" ItemStyle-Wrap="true">
                                        <HeaderStyle Width="15%" />
                                        <ItemStyle CssClass="text-center" Width="15%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="7" HeaderStyle-Width="10%" HeaderText="Board/Uni." ItemStyle-CssClass="text-center" ItemStyle-Width="10%" ItemStyle-Wrap="true">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle CssClass="text-center" Width="10%" Wrap="True" />
                                    </asp:BoundField>
                                </Columns>
                                <EditRowStyle BackColor="White" BorderColor="#CCCCCC" BorderWidth="2px" />
                                <%--<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />--%>
                                <HeaderStyle BackColor="#cccccc" BorderColor="#999999" Font-Size="10px" Font-Underline="true" ForeColor="Black" HorizontalAlign="Center" Wrap="True" />
                                <PagerStyle BackColor="#ffffff" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" BorderColor="#999999" BorderWidth="2px" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            <asp:Label ID="lblGRDCount" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblCOUNT" runat="server" Visible="False">1</asp:Label>
                        </td>
                        <td class="auto-style6" colspan="5">
                            <table class="nav-justified" style="width:100%">
                                <tr>
                                    <td width="10%">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="100%" CssClass="form-control" OnClick="btnAdd_Click" Height="100%"  />
                                    </td>
                                    <td width="10%">
                                        <asp:TextBox ID="txtXmNM" runat="server" Width="100%" Placeholder="Exam" CssClass="form-control" Height="30px"></asp:TextBox>
                                    </td>
                                    <td width="10%">
                                        <asp:TextBox ID="txtPYr" runat="server" Width="100%" Placeholder="Passed YR" CssClass="form-control" Height="30px"></asp:TextBox>
                                    </td>
                                    <td width="12%">
                                        <asp:TextBox ID="txtXmRl" runat="server" Width="100%" Placeholder="Roll" CssClass="form-control" Height="30px"></asp:TextBox>
                                        </t>
                            <td width="12%">
                                <asp:TextBox ID="txtGrop" runat="server" Width="100%" Placeholder="Group" CssClass="form-control" Height="30px"></asp:TextBox>
                            </td>
                                        <td width="10%">
                                            <asp:TextBox ID="txtGPA" runat="server" Width="100%" Placeholder="GPA Point" CssClass="form-control" Height="30px"></asp:TextBox>
                                        </td>
                                        <td width="15%">
                                            <asp:TextBox ID="txtInsNM" runat="server" Width="100%" Placeholder="Ins Name" CssClass="form-control" Height="30px"></asp:TextBox>
                                        </td>
                                        <td width="10%">
                                            <asp:TextBox ID="txtBrd" runat="server" Width="100%" Placeholder="Board" CssClass="form-control" Height="30px"></asp:TextBox>
                                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-left" colspan="6"><span class="auto-style6"><strong>Admitted To Any Other Program Before ?</strong></span>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Name Of Program :</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtPreProgNM" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Name Of Institution :</td>
                        <td class="auto-style6" colspan="4">
                            <asp:TextBox ID="txtPreIns" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Session :</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtPreSSN" runat="server" Height="30px" Width="100%" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style7" colspan="6">If the appliciant is in service or self employed state position and name of the organaization .If in buisness or self-employed,state name of the firm and position.</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style6">&nbsp;</td>
                        <td class="auto-style4" colspan="2">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Name of Firm :</td>
                        <td class="auto-style6">
                            <asp:TextBox ID="txtFirmNM" runat="server" CssClass="form-control" Height="30px" Width="100%" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="auto-style4" colspan="2">Position :</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtPosisn" runat="server" CssClass="form-control" Height="30px" Width="100%" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style6">&nbsp;</td>
                        <td class="auto-style4" colspan="2">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style22" colspan="2"><strong class="left">Migration&#39;s Portion:</strong></td>
                        <td class="auto-style24" colspan="2">&nbsp;</td>
                        <td class="auto-style26">&nbsp;</td>
                        <td class="auto-style23">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style24">Student ID (Old) : </td>
                        <td class="auto-style25" colspan="2">
 
                            <asp:DropDownList ID="ddlStudentIDOld" runat="server" AutoPostBack="True" style="display:inline-block" Width="40%" 
                                   CssClass="form-control select2" OnSelectedIndexChanged="ddlStudentIDOld_TextChanged">
                                </asp:DropDownList>
                            <asp:TextBox ID="txtStudentIDOld" runat="server" CssClass="form-control" Height="30px" MaxLength="100" Style="display: inline-block" Width="40%" Font-Bold="True" Font-Size="13pt" OnTextChanged="txtStudentIDOld_TextChanged" AutoPostBack="True" Visible="False"></asp:TextBox>
                            <%-- <asp:AutoCompleteExtender ID="txtStudentIDOld0_AutoCompleteExtender"
                                runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=";, :"
                                Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentOLDID" CompletionListCssClass="AutoExtender"
                                CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                TargetControlID="txtStudentIDOld0" UseContextKey="True">
                            </asp:AutoCompleteExtender>--%>
                           <%-- <asp:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionListCssClass="AutoExtender" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="3" DelimiterCharacters=";, :" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentOLDID" TargetControlID="txtStudentIDOld" UseContextKey="True">
                            </asp:AutoCompleteExtender>--%>
                            <asp:TextBox ID="txtStudentNameOld"   runat="server" CssClass="form-control" Font-Bold="True" Style="display: inline-block" Font-Size="10pt" Height="30px" MaxLength="100" Width="59%"></asp:TextBox>
                        </td>
                        <td class="auto-style24">Date :</td>
                        <td class="auto-style24">
                            <asp:TextBox ID="txtMigrateDT" runat="server" CssClass="form-control" Font-Bold="True" ClientIDMode="Static" Font-Size="13pt" Height="30px" MaxLength="100" Width="58%"></asp:TextBox>
                        </td>
                        <td class="auto-style23">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style24">Program :</td>
                        <td class="auto-style25" colspan="2">
                            <asp:TextBox ID="txtProgNMMigrate" runat="server" CssClass="form-control" Font-Bold="True" Font-Size="10pt" Height="30px" MaxLength="100" ReadOnly="True" Width="100%"></asp:TextBox>
                        </td>
                        <td class="auto-style24">&nbsp;</td>
                        <td class="auto-style24">
                            <asp:Label ID="lblProgIDMigrate" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style23">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style24" colspan="6">.</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style6">
                            <asp:Label ID="lblMSG" runat="server" Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style4" colspan="2">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Photos :</td>
                        <td class="auto-style6">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" Height="100%" Width="100%" />

                        </td>
                        <td class="auto-style4" colspan="2">&nbsp;</td>
                        <td class="auto-style5">
                            <asp:Button ID="btnSUBMIT" runat="server" BackColor="White" CssClass="form-control" Font-Bold="True" Font-Size="12pt" ForeColor="Black" OnClick="btnSUBMIT_Click" Text="Submit" ValidationGroup="Submit" Style="display: inline-block" Width="120px" BorderColor="#3399FF" BorderWidth="3px" />
                            <asp:Button ID="btnDLT" runat="server" OnClientClick="return confMSG()" BackColor="White" BorderColor="#3399FF" BorderWidth="3px" CssClass="form-control" Font-Bold="True" ForeColor="Black" Style="display: inline-block" OnClick="btnDLT_Click" Text="Delete" Width="120px" ValidationGroup="Delete" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style6">
                            <asp:Label ID="lblImagePath" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style4" colspan="2">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div> 
            <script type="text/javascript" src="../select2.full.min.js"></script>
            <link href="../select2.min.css" rel="stylesheet" />
            <script type="text/javascript">
                function pageLoad() {
                    $(".select2").select2();
                    $("#txtAdmDT,#txtDOB,#txtMigrateDT").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+5" });
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSUBMIT" /> 
            <asp:PostBackTrigger ControlID="ddlBCH" /> 
            <%--<asp:AsyncPostBackTrigger ControlID="File" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
