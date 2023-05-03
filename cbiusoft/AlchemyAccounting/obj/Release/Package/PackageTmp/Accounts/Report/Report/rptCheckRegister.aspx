<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptCheckRegister.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.Report.rptCheckRegister" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title></title>
    <link href="../../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type ="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>

        <style media="print">

        .showHeader thead
         {
            display: table-header-group;
            border: 1px solid #000;
         }

    </style>

    <style type ="text/css">
        #main
        {
            float:left;
            border: 1px solid #cccccc;
            width: 100%;
            padding-bottom:40px;
        }
                #btnPrint
        {
            font-weight: 700;
            font-style: italic;
        }
        .style8
        {
            text-align: center;
            font-family: Calibri;
            font-size: 16px;
            width: 950px;
        }
        .style9
        {
            text-align: center;
            width: 950px;
        }
        .style13
        {
            width: 2px;
            font-weight: 700;
        }
        .style16
        {
            width: 640px;
            font-weight: bold;
            font-family: Calibri;
            font-size: 16px;
        }
        .style18
        {
            width: 135px;
            font-weight: bold;
        }
        .style19
        {
            width: 135px;
        }
        .style1
        {
            font-family: Calibri;
            font-size: 20px;
            width: 891px;
        }
        .style22
        {
            font-family: Calibri;
            font-size: 16px;
        }
        .style23
        {
            width: 203px;
        }
        .style24
        {
            width: 284px;
        }
        .style25
        {
            width: 6px;
        }
        .style26
        {
            width: 4px;
        }
        .style27
        {
            width: 250px;
        }
        .style28
        {
            font-family: Calibri;
            font-size: 16px;
            width: 250px;
        }
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: center;
            border:1px solid #808080
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%;">
            <tr>
                <td class="style6" style="min-width: 20%">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td class="style1" style="text-align: center;">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Label ID="lblCompNM" runat="server" style="font-weight: 700"></asp:Label>
                </td>
                <td class="style26" style="text-align: right">
                    &nbsp;</td>
                <td style="text-align: right" class="style27">
                    <input id="print" tabindex="1" type="button" value="Print" onclick = "ClosePrint()"/></td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style9">
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Label ID="lblAddress" runat="server" 
                        style="font-family: Calibri; font-size: 9px; text-align: center;"></asp:Label>
                </td>
                <td class="style26">
                    &nbsp;</td>
                <td style="text-align: right" class="style28">
                    <asp:Label ID="lblTime" runat="server"></asp:Label>
                    </td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style9">
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Label ID="lblHeadNM" runat="server" 
                        style="font-weight: 700; font-size: 16px; font-family: Calibri;"></asp:Label>
                </td>
                <td class="style26">
                    &nbsp;</td>
                <td style="text-align: right" class="style28">
                    <asp:Label runat="server" ID="lbltimepm"></asp:Label>
                </td>
                <td style="text-align: right">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style8">
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       PERIOD <strong>:&nbsp; </strong>
                    <asp:Label ID="lblFrom" runat="server"></asp:Label>
                    <strong>&nbsp;&nbsp;</strong>TO&nbsp;
                    <asp:Label ID="lblTo" runat="server"></asp:Label>
                </td>
                <td class="style26">
                    &nbsp;</td>
                <td style="text-align: right; font-family: Calibri; font-size: 16px;" 
                    class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style8"><strong style="text-align: center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CHEQUE REGISTER</strong></td>
                    
                <td class="style26">
                    &nbsp;</td>
                <td style="text-align: right; font-family: Calibri; font-size: 16px;" 
                    class="style27">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
            <table class="auto-style1" style="border:2px solid #808080">
                <tr>
                     <td class="auto-style2"  rowspan="2" width="10%">Bank Name</td>
                    <td class="auto-style2"  rowspan="2" width="8%">Date</td>
                    <td class="auto-style2" rowspan="2" width="8%">Doc. No.</td>
                    <td class="auto-style2" colspan="3" width="30%">Cheque Details</td>
                    <td class="auto-style2" rowspan="2" width="15%">Paid To</td>
                    <td class="auto-style2" rowspan="2" width="15%">Remarks </td>
                    <td class="auto-style2" rowspan="2" width="14%">Amount</td>
                </tr>
                <tr>
                    <td class="auto-style2" width="10%">Cheque No</td>
                    <td class="auto-style2" width="10%">Date</td>
                    <td class="auto-style2" width="10%">Mode</td>
                </tr>
            </table>
        <div class="showHeader">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                Width="100%" onrowdatabound="GridView1_RowDataBound"  OnRowCreated="GridView1_RowCreated" 
                ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:BoundField>
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField>
                    <HeaderStyle HorizontalAlign="Center" Width="8%" />
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    </asp:BoundField>
                    <asp:BoundField>
                    <HeaderStyle HorizontalAlign="Center" Width="8%" />
                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                    </asp:BoundField>
                    <asp:BoundField>
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField>
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField>
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField >
                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField  >
                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField >
                    <HeaderStyle HorizontalAlign="Center" Width="14%" />
                    <ItemStyle HorizontalAlign="Right" Width="14%" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                <RowStyle Font-Names="Calibri" Font-Size="14px" />
            </asp:GridView>
            </div>
        
        </div>
    </div>
    </form>
</body>
</html>

