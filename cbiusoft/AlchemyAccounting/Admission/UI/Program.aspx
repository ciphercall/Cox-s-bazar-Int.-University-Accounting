<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Program.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.Program" %>
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
</script>
   

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 19%;
            text-align: right;
        }
        .auto-style4 {
            text-align: center;
            color: #FFFFFF;
            font-size: xx-large;
        }
        .auto-style5 {
            font-size: larger;
        }
        .auto-style8 {
            width: 392px;
        }
        .auto-style9 {
            text-align: right;
            width: 150px;
        }
    .auto-style10 {
        width: 19%;
        text-align: right;
    }
    .auto-style11 {
        width: 392px;
        text-align: center;
    }
        .auto-style14 {
            text-align: right;
            width: 150px;
            height: 20px;
        }
        .auto-style15 {
            width: 392px;
            text-align: center;
            height: 20px;
        }
        .auto-style16 {
            width: 19%;
            text-align: right;
            height: 20px;
        }
        .auto-style17 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="Update1" runat="server"><ContentTemplate>
    <div style="border:double;border-radius:10px;border-width:2px">
        <div style="border-style: double; border-color: white; border-top-left-radius:10px;border-top-right-radius:10px; border-width:2px; background-color: #2aabd2;" class="auto-style4"><span class="auto-style5">P</span>rogram <span class="auto-style5">I</span>nformation</div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style14"></td>
                <td class="auto-style15">
                    <asp:Label ID="lblMSG" runat="server" Font-Size="9pt" ForeColor="#009900" style="text-align: center" Visible="False"></asp:Label>
                    <asp:Label ID="lblProgID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblPTP" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="auto-style16"></td>
                <td class="auto-style17"></td>
            </tr>
            <tr>
                <td class="auto-style9">Program Type :</td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlProgTP"  runat="server" Height="30px" Width="100%" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProgTP_SelectedIndexChanged">
                         <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem>Graduate</asp:ListItem>
                        <asp:ListItem>Under Graduate</asp:ListItem>
                        <asp:ListItem>Diploma & Others</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;
                    <asp:RequiredFieldValidator ID="PTP" runat="server" ControlToValidate="ddlProgTP" Font-Bold="True" Font-Size="12pt" ForeColor="#CC0000" SetFocusOnError="True" InitialValue="-1"></asp:RequiredFieldValidator>
                    a</td>
            </tr>
            <tr>
                <td class="auto-style9">Program Name :</td>
                <td class="auto-style8">
                    <asp:TextBox ID="txtProNM" runat="server" Height="30px" Width="98%" AutoPostBack="True" OnTextChanged="txtProNM_TextChanged" CssClass="form-control"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtProgramNM_AutoCompleteExtender"  runat="server" CompletionInterval="10"  
                        CompletionSetCount="3" DelimiterCharacters=""   CompletionListCssClass="AutoColor" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" 
                        ServiceMethod="GetCompletionProgram" TargetControlID="txtProNM" UseContextKey="True">
                    </asp:AutoCompleteExtender>
                    
                  
                </td>
                <td>
                    <asp:TextBox ID="txtProgID" runat="server" Height="30px" style="text-align: left;" Width="100%" Enabled="False" CssClass="form-control"></asp:TextBox>
                </td>
                <td>&nbsp;
                    <asp:RequiredFieldValidator ID="PNM" runat="server" ControlToValidate="txtProNM" Font-Bold="True" Font-Size="12pt" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Program Short Name :</td>
                <td colspan="2">
                    <asp:TextBox ID="txtProgSrtNM" runat="server" Height="30px" Width="100%" CssClass="form-control"></asp:TextBox>
                </td><td>&nbsp;
                    <asp:RequiredFieldValidator ID="PSNM" runat="server" ControlToValidate="txtProgSrtNM" Font-Bold="True" Font-Size="12pt" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Total Credit :</td>
                <td colspan="2">
                    <asp:TextBox ID="txtTtlCrdt" runat="server" Height="30px" Width="100%" CssClass="form-control"></asp:TextBox>
                </td><td>&nbsp;
                    <asp:RequiredFieldValidator ID="TCR" runat="server" ControlToValidate="txtTtlCrdt" Font-Bold="True" Font-Size="12pt" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Cost Per Credit :</td>
                <td colspan="2">
                    <asp:TextBox ID="txtCstPerCrdt" runat="server" Height="30px" Width="100%" CssClass="form-control"></asp:TextBox>
                </td><td>&nbsp;
                    <asp:RequiredFieldValidator ID="CPCR" runat="server" ControlToValidate="txtCstPerCrdt" Font-Bold="True" Font-Size="12pt" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Duration :</td>
                <td colspan="2">
                    <asp:TextBox ID="txtDuration" runat="server" Height="30px" Width="100%" CssClass="form-control"></asp:TextBox>
                </td><td>&nbsp;
                    <asp:RequiredFieldValidator ID="DUR" runat="server" ControlToValidate="txtDuration" Font-Bold="True" Font-Size="12pt" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Total Amount :</td>
                <td colspan="2">
                    <asp:TextBox ID="txtTtlAmnt" runat="server" Height="30px" Width="100%" CssClass="form-control"></asp:TextBox>
                </td><td>&nbsp;
                    <asp:RequiredFieldValidator ID="TAMN" runat="server" ControlToValidate="txtTtlAmnt" Font-Bold="True" Font-Size="12pt" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Remarks :</td>
                <td colspan="2">
                    <asp:TextBox ID="txtRemarks" runat="server" Height="30px" Width="100%" CssClass="form-control"></asp:TextBox>
                </td><td>&nbsp;
                    </td>
            </tr>
            <tr>
                <td class="auto-style9">&nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style9">&nbsp;</td>
                <td style="text-align: left;" class="auto-style11">
                    <asp:Button ID="btnUpd" runat="server" BackColor="White" BorderColor="#009999" BorderWidth="3px" CssClass="form-control" Font-Bold="True" ForeColor="#009999" Height="35px" OnClick="btnUpd_Click" Text="Update" ValidationGroup="Update" Width="100px" Visible="False" />
                    &nbsp;
                    <asp:Button ID="btnDlt" runat="server"  BackColor="White" BorderColor="#009999" BorderWidth="3px" CssClass="form-control" Font-Bold="True" ForeColor="#009999" Height="35px" OnClick="btnDlt_Click" OnClientClick = "return confMSG()" Text="Delete" ValidationGroup="Delete" Width="100px" Visible="False" />
                   <td class="auto-style10"> 
                       <asp:Button ID="btnInclds0" runat="server" BackColor="White" BorderColor="#009999" BorderWidth="3px" CssClass="form-control" Font-Bold="True" ForeColor="#009999" Height="35px" OnClick="btnInclds_Click" Text="Includes" Width="100%" />
                </td>
                <td width="10%">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style9">&nbsp;</td>
                <td style="text-align: left;" class="auto-style8">
                    &nbsp;<td class="auto-style3"> &nbsp;</td>
                <td width="10%">&nbsp;</td>
            </tr>
        </table>
    </div></ContentTemplate></asp:UpdatePanel>
</asp:Content>
