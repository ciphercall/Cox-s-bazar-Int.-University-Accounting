<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Process_Rec.aspx.cs" Inherits="AlchemyAccounting.Accounts.UI.Process_Rec" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
   

    <script type="text/javascript">
        function pageLoad() {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-5:+10" });
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style3 {
            font-size: xx-large;
            text-align: center;
            background-color: #66CCFF;
        }

        .auto-style5 {
            text-align: right;
            color: #000000;
        }
    .auto-style6 {
        text-align: center;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>

       
    <table class="auto-style1">
        <tr>
            <td class="auto-style3" style="width: 15%">&nbsp;</td>
            <td class="auto-style3" style="width: 20%">&nbsp;</td>
            <td class="auto-style3">Process-Receivable</td>
            <td class="auto-style3" style="width: 20%">&nbsp;</td>
            <td class="auto-style3" style="width: 15%">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 15%" class="auto-style6">&nbsp;</td>
            <td style="width: 20%" class="auto-style6">&nbsp;</td>
            <td class="auto-style6">
                <asp:Label ID="lblMSG" runat="server" Font-Size="9pt" ForeColor="Red" Visible="False"></asp:Label>
            </td>
            <td style="width: 20%" class="auto-style6">&nbsp;</td>
            <td style="width: 15%" class="auto-style6">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 15%">&nbsp;</td>
            <td class="auto-style5" style="width: 20%">Date :&nbsp;&nbsp;&nbsp; </td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static" CssClass="form-control" Width="100%" AutoPostBack="True"></asp:TextBox>
            </td>
            <td style="width: 20%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 15%">&nbsp;</td>
            <td style="width: 20%">&nbsp;</td>
            <td>
                <asp:Button ID="btnProcess" runat="server" CssClass="form-control-right " Text="Submit" OnClick="btnSubmit_Click" />
            </td>
            <td style="width: 20%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="lblSerial_JOUR" runat="server" Visible="false"></asp:Label> 
            </td>
                

        </tr>
    </table>
             </ContentTemplate>
    </asp:UpdatePanel>
    <asp:GridView ID="gv_Receivable" runat="server" AutoGenerateColumns="False">
                    
                    <HeaderStyle BackColor="Black" />
                </asp:GridView>
</asp:Content>

