<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-workingHour-Periodic-Site.aspx.cs" Inherits="AlchemyAccounting.payroll.report.vis_report.rpt_workingHour_Periodic_Site" %>

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
                        <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 9px"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: center; font-family: Calibri; font-size: 18px;
                        font-weight: bold;">
                        Working Hour(Summarized) - Site Wise</td>
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
                        From
                        Date
                    </td>
                    <td style="font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center" 
                        class="style1">
                        :
                    </td>
                    <td style="width: 34%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblDate" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                    <td style="width: 15%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        To Date</td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        :</td>
                    <td style="width: 29%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblToDate" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        &nbsp;</td>
                    <td style="font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center" 
                        class="style1">
                        &nbsp;</td>
                    <td style="width: 34%; font-family: Calibri; font-size: 15px; text-align: left">
                        &nbsp;</td>
                    <td style="width: 15%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        &nbsp;</td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        &nbsp;</td>
                    <td style="width: 29%; font-family: Calibri; font-size: 15px; text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 20%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        Site Name </td>
                    <td style="font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center" 
                        class="style1">
                        :</td>
                    <td style="width: 34%; font-family: Calibri; font-size: 15px; text-align: left">
                        <asp:Label ID="lblSiteNM" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                    <td style="width: 15%; font-family: Calibri; font-size: 15px; font-weight: bold;
                        text-align: right">
                        &nbsp;</td>
                    <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                        &nbsp;</td>
                    <td style="width: 29%; font-family: Calibri; font-size: 15px; text-align: left">
                        &nbsp;</td>
                </tr>
               
            </table>
            <div style="width: 96%; margin: 1% 2% 0% 2%;">
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
                    OnRowDataBound="gvReport_RowDataBound" ShowFooter="true" Width="100%" 
                    OnRowCreated="gvReport_RowCreated">
                    <Columns>
                        <asp:BoundField HeaderText="Prticulars">
                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Normal Hour">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Normal OT">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Friday OT">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total Hour">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
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
                        &nbsp;</td>
                    <td style="width: 1%; text-align: center; font-family: Calibri; font-size: 14px;
                        font-weight: bold">
                        &nbsp;</td>
                    <td style="width: 84%">
                        &nbsp;</td>
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
        </div>
    </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
