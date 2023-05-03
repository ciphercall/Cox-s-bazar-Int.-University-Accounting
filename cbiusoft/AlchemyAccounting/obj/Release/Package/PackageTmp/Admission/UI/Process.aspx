<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Process.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.Process" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />

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

        .auto-style2 {
            text-align: right;
        }

        .auto-style3 {
            font-size: xx-large;
            text-align: center;
            background-color: #66CCFF;
        }

        .auto-style4 {
            color: #333333;
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
    <asp:ScriptManager ID="script" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>

       
    <table class="auto-style1">
        <tr>
            <td class="auto-style3" style="width: 15%">&nbsp;</td>
            <td class="auto-style3" style="width: 20%">&nbsp;</td>
            <td class="auto-style3">Process</td>
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
            <td class="auto-style2" style="width: 20%"><span class="auto-style4">Month :&nbsp;&nbsp;&nbsp; </span></td>
            <td>
                <asp:DropDownList ID="ddlMonth_Year" runat="server" CssClass="form-control" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_Year_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width: 20%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
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
                <asp:Button ID="btnSubmit" runat="server" CssClass="form-control " Text="Submit" OnClick="btnSubmit_Click" />
            </td>
            <td style="width: 20%">&nbsp;</td>
            <td style="width: 15%">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;</td>
                

        </tr>
    </table>
             </ContentTemplate>
    </asp:UpdatePanel>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="TRANSMY"></asp:BoundField>
                        <asp:BoundField HeaderText="EMPID"></asp:BoundField>
                        <asp:BoundField HeaderText="POSTID"></asp:BoundField>
                        <asp:BoundField HeaderText="MMDAY"></asp:BoundField>
                        <asp:BoundField HeaderText="HDAY"></asp:BoundField>
                        <asp:BoundField HeaderText="PREDAY"></asp:BoundField>
                        <asp:BoundField HeaderText="ABSDAY"></asp:BoundField>
                        <asp:BoundField HeaderText="LDAY"></asp:BoundField>
                        <asp:BoundField HeaderText="BASICSAL"></asp:BoundField>
                        <asp:BoundField HeaderText="ALLOWANCE"></asp:BoundField>
                        <asp:BoundField HeaderText="TOTPAID"></asp:BoundField>
                        <asp:BoundField HeaderText="ADVANCE"></asp:BoundField>
                        <asp:BoundField HeaderText="NETPAID"></asp:BoundField>

                    </Columns>
                    <HeaderStyle BackColor="Black" />
                </asp:GridView>
</asp:Content>
