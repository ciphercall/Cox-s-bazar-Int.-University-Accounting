<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-commission.aspx.cs"
    Inherits="AlchemyAccounting.payroll.report.vis_report.rpt_commission" %>

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
            width: 80%;
            height: 26px;
        }
        .style2
        {
            width: 10%;
            height: 26px;
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
                        <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 9px"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-family: Calibri; font-size: 18px; font-weight: bold;"
                        class="style1">
                        Site Wise Commission &amp; Payment Calculation
                    </td>
                    <td class="style2">
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
            </table>
            <div style="width: 100%; border: 1px solid #000">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 40%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                            &nbsp;
                        </td>
                        <td style="width: 49%; font-family: Calibri; font-size: 15px; text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 40%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: left">
                            Bill Date
                        </td>
                        <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                            :
                        </td>
                        <td style="width: 49%; font-family: Calibri; font-size: 15px; text-align: left">
                            <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 40%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: left">
                            Site Name
                        </td>
                        <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                            :
                        </td>
                        <td style="width: 49%; font-family: Calibri; font-size: 15px; text-align: left">
                            <asp:Label ID="lblSiteNM" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 40%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: left">
                            Description
                        </td>
                        <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                            :
                        </td>
                        <td style="width: 49%; font-family: Calibri; font-size: 15px; text-align: left">
                            <asp:Label ID="lblSiteDes" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 40%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: left">
                            Bill Amount (QR)
                        </td>
                        <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                            :
                        </td>
                        <td style="width: 49%; font-family: Calibri; font-size: 15px; text-align: left">
                            <asp:Label ID="lblBillAmount" runat="server"></asp:Label>
                            <div style="width: 20%; height: 1px; border-bottom: 1px solid #000">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 40%; font-family: Calibri; font-size: 15px; text-align: left">
                            <span style="font-weight: bold">Commission </span>(<asp:Label ID="lblCommission"
                                runat="server"></asp:Label>
                            %)
                        </td>
                        <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                            :
                        </td>
                        <td style="width: 49%; font-family: Calibri; font-size: 15px; text-align: left">
                            <asp:Label ID="lblCommissionAmt" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div style="width: 100%">
                    <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
                        OnRowDataBound="gvReport_RowDataBound" ShowHeader="False" Width="100%" GridLines="None">
                        <Columns>
                            <asp:BoundField>
                                <ItemStyle HorizontalAlign="Right" Width="10%" Font-Bold="True" />
                            </asp:BoundField>
                            <asp:BoundField>
                                <ItemStyle HorizontalAlign="Left" Width="40%" Font-Bold="True" />
                            </asp:BoundField>
                            <asp:BoundField>
                                <ItemStyle HorizontalAlign="Center" Width="1%" Font-Bold="True" />
                            </asp:BoundField>
                            <asp:BoundField>
                                <ItemStyle HorizontalAlign="Left" Width="49%" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle Font-Size="14px" BorderStyle="None" />
                    </asp:GridView>
                </div>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 40%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: left">
                            Amount (QR)
                        </td>
                        <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                            :&nbsp;
                        </td>
                        <td style="width: 49%; font-family: Calibri; font-size: 15px; text-align: left;">
                            <asp:Label ID="lblAmt" runat="server"></asp:Label>
                            <div style="width: 20%; height: 1px; border-top: 1px solid #000">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 40%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: left">
                            Grand Total (QR)
                        </td>
                        <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                            :
                        </td>
                        <td style="width: 49%; font-family: Calibri; font-size: 15px; text-align: left">
                            <asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 40%; font-family: Calibri; font-size: 15px; font-weight: bold;
                            text-align: right">
                            &nbsp;
                        </td>
                        <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                            &nbsp;
                        </td>
                        <td style="width: 49%; font-family: Calibri; font-size: 15px; text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        &nbsp;
                    </td>
                    <td style="font-family: Calibri; font-size: 15px; text-align: left">
                        <span style="font-weight: bold">In Words : </span>
                        <asp:Label ID="lblInWords" runat="server" Font-Names="Calibri" Font-Size="15px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        &nbsp;</td>
                    <td style="font-family: Calibri; font-size: 15px; text-align: left">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 12%">
                        <asp:Label ID="lblTotAmount" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 15%">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 15%">
                        &nbsp;
                    </td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                    <td style="width: 16%">
                        &nbsp;
                    </td>
                    <td style="width: 12%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 12%">
                    </td>
                    <td style="width: 15%">
                    </td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 15%">
                    </td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 15%">
                    </td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 16%">
                    </td>
                    <td style="width: 12%">
                    </td>
                </tr>
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
                <tr>
                    <td style="width: 12%">
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px;
                        border-top: 1px solid #000;">
                            &nbsp;Authorised By 
                            Manager</td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px;
                        border-top: 1px solid #000;">
                            Prepared By</td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 15%; text-align: center; font-family: Calibri; font-size: 14px;
                        border-top: 1px solid #000;">
                            &nbsp;Checked By</td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 16%; text-align: center; font-family: Calibri; font-size: 14px;
                        border-top: 1px solid #000;">
                            Received By</td>
                    <td style="width: 12%">
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
