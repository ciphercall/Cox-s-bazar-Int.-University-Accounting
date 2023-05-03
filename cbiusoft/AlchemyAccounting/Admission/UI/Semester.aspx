<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Semester.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.Semester" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
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
        .MainDiv{border:double;
            border-width:2px;
            border-radius:10px}
        
        .auto-style1 {
            width: 100%;
            
        }
        .auto-style2 {
            text-align: right;
            width: 132px;
        }
        .auto-style5 {
            width: 459px;
        }
        .auto-style6 {
            color: #FFFFFF;
            text-align: center;
            font-size: xx-large;
            border: 2px double white;
            background-color: #2aabd2;
        }
        .auto-style7 {
            font-size: larger;
        }
        .auto-style8 {
            width: 487px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    <div style="text-align: center">
        <br />
        <div class="MainDiv">
        <table class="auto-style1">
            <div>
                <caption style="text-align: center">
                   <div style="border-top-right-radius: 10px; border-top-left-radius: 10px; " class="auto-style6"> <span class="auto-style7">S</span></span>emester <span class="auto-style7">I</span>nformation</div><br />
                    <asp:Label ID="lblMSG" runat="server" ForeColor="#CC0000" style="text-align: center"></asp:Label>
                    <tr>
                        <td class="auto-style2" width="15%">Semester Name :</td>
                        <td class="auto-style8">
                            <asp:TextBox ID="txtSemester" runat="server" AutoPostBack="True" Height="30px" CssClass="form-control" OnTextChanged="txtSemester_TextChanged" Width="99%" MaxLength="100"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtSemester_AutoCompleteExtender" CompletionListCssClass="AutoColor" runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionSemester" TargetControlID="txtSemester" UseContextKey="True">
                            </asp:AutoCompleteExtender>
                        </td>
                        <td style="text-align: right" >
                            <asp:TextBox ID="txtSemiID" runat="server" Height="30px" CssClass="form-control" Enabled="False" Width="100%"></asp:TextBox>
                        </td>
                        <td width="10%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style2" width="15%">Start Time :</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtStartTime" CssClass="form-control" runat="server" Height="30px" Width="100%" MaxLength="20"></asp:TextBox>
                        </td>
                        <td width="10%" height="25px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style2" width="15%">Remarks :</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" Height="30px" Width="100%" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2" width="15%">&nbsp;</td>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style2" width="15%">&nbsp;</td>
                        <td style="text-align: left" class="auto-style8">
                            &nbsp;
                            <asp:Label ID="lblSemisID" runat="server" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="SemNM" runat="server" ControlToValidate="txtSemester" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="Strttm" runat="server" ControlToValidate="txtStartTime" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnDlt" runat="server" BorderColor="Black" BorderWidth="2px" CssClass="form-control" Height="35px" OnClick="btnDlt_Click" OnClientClick="return confMSG()" Text="Delete" Visible="False" />
                            <asp:Button ID="btnUpdt" runat="server" BorderColor="Black" BorderWidth="2px" CssClass="form-control" Height="35px" OnClick="btnUpdt_Click" Text="Update" Visible="False" />
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnIncld" runat="server" BorderColor="Black" BorderWidth="2px" CssClass="form-control" Height="35px" OnClick="btnIncld_Click" Text="Include" Width="100%" />
                            &nbsp;</td>
                        <td width="10%">&nbsp;</td>
                    </tr>
                </caption>
            </div>
            <tr>
                <td class="auto-style2" width="15%">&nbsp;</td>
                <td class="auto-style8" style="text-align: left">&nbsp;</td>
                <td style="text-align: right">&nbsp;</td>
                <td width="10%">&nbsp;</td>
            </tr>
            </table>
         
            </div>
    </div>
        </ContentTemplate></asp:UpdatePanel>
</asp:Content>
