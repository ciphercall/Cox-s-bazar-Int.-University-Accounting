<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-Invoice.aspx.cs" Inherits="AlchemyAccounting.payroll.report.vis_report.rpt_Invoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />
    <link href="../../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>
    <style media="print">
        .ShowHeader thead
        {
            display: table-header-group;
            border: 1px solid #000;
        }
    </style>
    <style type="text/css">
        .style1
        {
            width: 1%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%" rowspan="3">
                        <div style="width: 140px; height: 80px;">
                            <img alt="logo" height="100%;" src="../../../Images/logo.png" width="100%" />
                        </div>
                    </td>
                    <td style="width: 80%; text-align: center">
                        <asp:Label ID="lblCompNM" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="23px"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                            style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: center">
                        <asp:Label ID="lblAddress" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: center; font-family: Calibri; font-size: 18px;
                        font-weight: bold;">
                        INVOICE
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                    </td>
                    <td style="text-align: right; font-family: Calibri; font-size: 14px;" colspan="2">
                        <strong>Print Date :</strong>
                        <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                    <td style="text-align: right; font-family: Calibri; font-size: 14px;" colspan="2">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Party Name
                    </td>
                    <td style="font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center"
                        class="style1">
                        :
                    </td>
                    <td style="width: 34%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblPartyName" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                    <td style="width: 15%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Invoice Date
                    </td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        :
                    </td>
                    <td style="width: 29%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblDate" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Address
                    </td>
                    <td style="font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center"
                        class="style1">
                        :
                    </td>
                    <td style="width: 34%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblPartyAddress" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                    <td style="width: 15%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Invoice No
                    </td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        :
                    </td>
                    <td style="width: 29%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblDocNo" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Site Name
                    </td>
                    <td style="font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center"
                        class="style1">
                        :
                    </td>
                    <td style="width: 34%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblSite" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                    <td style="width: 15%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        &nbsp;
                    </td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        &nbsp;
                    </td>
                    <td style="width: 29%; font-family: Calibri; font-size: 15px; text-align: left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Sub
                    </td>
                    <td style="font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center"
                        class="style1">
                        :
                    </td>
                    <td style="width: 34%; font-family: Calibri; font-size: 15px; text-align: left">
                        &nbsp;Invoice For The &nbsp;Month of&nbsp;
                        <asp:Label ID="lblmonth" runat="server" ToolTip="10"></asp:Label>
                    </td>
                    <td style="width: 15%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        &nbsp;
                    </td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        &nbsp;
                    </td>
                    <td style="width: 29%; font-family: Calibri; font-size: 15px; text-align: left">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div style="width: 96%; margin: 1% 2% 0% 2%;">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
                    OnRowDataBound="gvReport_RowDataBound" ShowFooter="True" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="Serial">
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Category">
                            <HeaderStyle HorizontalAlign="Center" Width="40%" />
                            <ItemStyle HorizontalAlign="Left" Width="40%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total Worker">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total Hour">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Price">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Amount">
                            <HeaderStyle HorizontalAlign="Center" Width="25%" />
                            <ItemStyle HorizontalAlign="Right" Width="25%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Size="14px" />
                    <HeaderStyle Font-Size="14px" />
                    <RowStyle Font-Size="12px" />
                </asp:GridView>
            </div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 15%">
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td style="width: 84%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; text-align: right; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 84%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; text-align: right; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        In Words
                    </td>
                    <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        :
                    </td>
                    <td style="width: 84%">
                        <asp:Label ID="lblInWords" runat="server" Font-Names="Calibri" Font-Size="15px"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" style="font-family: Calibri">
                <tr>
                    <td style="width: 7%">
                    </td>
                    <td style="width: 73%">
                        &nbsp;Kindly Arrange to release the payment as soon as possible.
                    </td>
                    <td style="width: 20%">
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 12%">
                        &nbsp;
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 16%; text-align: center; font-family: Calibri; font-size: 14px">
                        &nbsp;
                    </td>
                    <td style="width: 12%">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div style="width: 96%; margin: 1% 2% 0% 2%; font-family: Calibri">
                <table style="border: 3px solid black; width: 100%;">
                    <tr>
                        <th style="border: 1px solid black; width:50%">
                           Invoice Submitted By
                        </th> 
                        <th style="border: 1px solid black; width:50%">
                           Receiver Signe & Company Seal
                        </th>
                    </tr>
                    <tr>
                        <td style="border: 1px solid black; width:50%">


                            Best Regards,<br />
                            <br />
                            <br />
                            <br />
                        <asp:Label ID="lblName" runat="server" Font-Size="12pt"></asp:Label>
                            <br />
                            Marketing Manager<br />
                            Mobile :
                        <asp:Label ID="lblph" runat="server" Font-Size="12pt"></asp:Label>
                            <br />
                            E-mail&nbsp; :
                        <asp:Label ID="lblmail" runat="server" Font-Size="12pt"></asp:Label>
                            <br />
                        <asp:Label ID="lblcompany" runat="server" Font-Size="13pt"></asp:Label>


                        </td>
                        <td style="border: 1px solid black; width:50%">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
