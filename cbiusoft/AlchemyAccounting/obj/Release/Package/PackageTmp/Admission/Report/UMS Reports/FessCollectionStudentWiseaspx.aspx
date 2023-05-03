<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FessCollectionStudentWiseaspx.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.FessCollectionStudentWiseaspx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
    <title>::Fees Collection Report By Student</title>
    <link href="../../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            $("#txtFrDT,#txtToDT").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+5" });
        }

    </script>
    <style type="text/css">
        .txtColor:focus {
           border:solid 4px green !important;
        }
          .txtColor
        {
            margin-left: 0px;
            }
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: right;
        }
        
    </style>
</head>
<body>
     <form id="form1" runat="server">
         <asp:ScriptManager ID="Script1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
             <div style="border-style: double; margin:0 auto; border-width: 2px; border-radius: 10px; border-color: black; width:960px; color: #000000;">
         <div style=" border-top-left-radius: 10px; border-top-right-radius: 10px; color: #FFFFFF; text-align: center; font-size: xx-large; background-color: #0099FF;" class="auto-style2" >
                    Fees Collection Report</div>
    <div style="margin-bottom: 0px">

        <table class="auto-style1">
            <tr>
                <td style="width: 20%">&nbsp;</td>
                <td style="width: 20%">
                    <asp:Label ID="lblMSG" runat="server" Font-Size="10pt" ForeColor="#CC0000"></asp:Label>
                </td>
                <td style="width: 20%">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" style="width: 20%">Fees Name&nbsp;&nbsp; :</td>
                <td style="width: 20%">
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="100%" AutoPostBack="True" CssClass="txtColor">
                    </asp:DropDownList>
                </td>
                <td style="width: 20%">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" style="width: 20%">From Date&nbsp; :</td>
                <td style="width: 20%">
                    <asp:TextBox ID="txtFrDT" runat="server" ClientIDMode="Static" Height="25px" OnTextChanged="txtFrDT_TextChanged" Width="100%" AutoPostBack="True" CssClass="txtColor"></asp:TextBox>
                </td>
                <td style="width: 20%">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    To Date&nbsp; :</td>
                <td style="width: 20%" class="auto-style2">
                    <asp:TextBox ID="txtToDT" runat="server" ClientIDMode="Static" Height="25px" Width="100%" OnTextChanged="txtToDT_TextChanged" AutoPostBack="True" CssClass="txtColor"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">&nbsp;</td>
                <td style="width: 20%; text-align: right;">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" CssClass="txtColor" />
                </td>
                <td style="width: 20%">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 20%">&nbsp;</td>
                <td style="width: 20%">&nbsp;</td>
                <td style="width: 20%">&nbsp;</td>
            </tr>
        </table>

    </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
