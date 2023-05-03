<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-emp-pay.aspx.cs" Inherits="AlchemyAccounting.payroll.report.vis_report.rpt_emp_pay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />
    <link href="../../../report.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            window.print();
        }
    </script>
    <style type="text/css" media="print">
        .ShowHeader thead
        {
            display: table-header-group;
            border: 1px solid #000;
        }
    </style>
    <style type="text/css">
        .style1
        {
            width: 20%;
            height: 18px;
        }
        .style2
        {
            width: 1%;
            height: 18px;
        }
        .style3
        {
            height: 18px;
        }
        .style4
        {
            width: 15%;
            height: 18px;
        }
        .style5
        {
            width: 29%;
            height: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page">
        <div style="float: left; width: 100%; border-bottom: 1px solid #000">
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%" rowspan="3">
                        &nbsp; &nbsp;
                        <div style="width: 140px; height: 80px; margin-top: -40px">
                            <img alt="logo" height="100%;" src="../../../Images/logo.png" width="100%" />
                        </div>
                    </td>
                    <td style="width: 80%; text-align: center">
                        &nbsp;
                        <asp:Label runat="server" ID="lblCompanyNM" Style="font-size: 20px; font-weight: 700"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                            style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit;
                            text-align: right" />
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
                    <td style="text-align: right; font-size: 12px" colspan="2">
                        <strong><span class="style1">Print Date :</span></strong>
                        <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri" Font-Size="12px"
                            CssClass="style1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%; text-align: right">
                        <strong>Month : </strong>
                    </td>
                    <td style="text-align: left" colspan="2">
                        <asp:Label ID="lblMonth" runat="server" Font-Names="Calibri" Font-Size="12px" Style="font-size: 14px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <strong>Year : </strong>
                        <asp:Label ID="lblYear" runat="server" Font-Names="Calibri" Font-Size="12px" Style="font-size: 14px"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 100%; margin-top: 5px; font-family: Calibri; font-size: 14px">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right; font-weight: bold" class="style1">
                        Name
                    </td>
                    <td style="text-align: center; font-weight: bold" class="style2">
                        :
                    </td>
                    <td style="text-align: left" class="style3">
                        <asp:Label ID="lblName" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                    </td>
                    <td style="text-align: right; font-weight: bold" class="style4" colspan="2">
                        P.P No
                    </td>
                    <td style="text-align: center; font-weight: bold" class="style2" colspan="2">
                        :
                    </td>
                    <td style="text-align: left" class="style5">
                        <asp:Label ID="lblPassport" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%; text-align: right; font-weight: bold">
                        Qatar ID
                    </td>
                    <td style="width: 1%; text-align: center; font-weight: bold">
                        :
                    </td>
                    <td style="width: 20%; text-align: left">
                        <asp:Label ID="lblQatarID" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                    </td>
                    <td style="width: 30%; text-align: left">
                        <strong>Occupation : </strong>
                        <asp:Label ID="lblOccupation" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                    </td>
                    <td style="width: 29%; text-align: left">
                        <strong>Site : </strong>
                        <asp:Label ID="lblSite" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 100%; margin-top: 2%; font-family: Calibri; font-size: 14px">
            <div style="float: left; width: 45%">
                <asp:GridView ID="gvSal15" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSal15_RowDataBound"
                    Width="100%" ShowFooter="True" ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:BoundField HeaderText="Date">
                            <HeaderStyle HorizontalAlign="Center" Width="50%" />
                            <ItemStyle HorizontalAlign="Center" Width="50%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Normal Hour">
                            <HeaderStyle HorizontalAlign="Center" Width="25%" />
                            <ItemStyle HorizontalAlign="Center" Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Normal OT">
                            <HeaderStyle HorizontalAlign="Center" Width="25%" />
                            <ItemStyle HorizontalAlign="Center" Width="25%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <div style="float: left; width: 45%; margin-left: 10%">
                <asp:GridView ID="gvSal16" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSal16_RowDataBound"
                    Width="100%" ShowFooter="True" ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:BoundField HeaderText="Date">
                            <HeaderStyle HorizontalAlign="Center" Width="50%" />
                            <ItemStyle HorizontalAlign="Center" Width="50%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Normal Hour">
                            <HeaderStyle HorizontalAlign="Center" Width="25%" />
                            <ItemStyle HorizontalAlign="Center" Width="25%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Normal OT">
                            <HeaderStyle HorizontalAlign="Center" Width="25%" />
                            <ItemStyle HorizontalAlign="Center" Width="25%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div style="float: left; width: 100%; margin-top: 5%">
            <div style="float: left; width: 45%; border: 1px solid #000">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Basic
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblBasic" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Normal Hours
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblNormalHR" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Normal OT
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblNormalOT" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Friday OT
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblFridayOT" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Total Hours
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblTotHR" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Total Amount
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblTotAmt" runat="server" Font-Names="Calibri" Style="font-size: 14px;"></asp:Label>
                            <div style="width: 50%; border-bottom: 1px solid #000;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Food
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblFood" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Bonus
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblBonus" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Other Addition
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblOtcAdd" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Gross Amount
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblGrossAmt" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                            <div style="width: 50%; border-bottom: 1px solid #000;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Advance
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblAdv" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            Penalty
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblPenalty" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold; font-size: 13px">
                            Other Deduction
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblOtherDed" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                            <div style="width: 50%; border-bottom: 1px solid #000;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right; font-weight: bold">
                            G. Total Salary
                        </td>
                        <td style="width: 1%; text-align: center; font-weight: bold">
                            :
                        </td>
                        <td style="width: 69%;">
                            <asp:Label ID="lblGSal" runat="server" Font-Names="Calibri" Style="font-size: 14px"></asp:Label>
                            <div style="width: 50%; border-bottom: 1px solid #000;"></div>
                            <div style="width: 50%; margin-top: 5px; border-bottom: 1px solid #000;"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Label ID="lblNormalHR15" runat="server" Font-Names="Calibri" 
                Style="font-size: 14px" Visible="False"></asp:Label>
            <asp:Label ID="lblNormalOT15" runat="server" Font-Names="Calibri" 
                Style="font-size: 14px" Visible="False"></asp:Label>
            <asp:Label ID="lblNormalHR16" runat="server" Font-Names="Calibri" 
                Style="font-size: 14px" Visible="False"></asp:Label>
            <asp:Label ID="lblNormalOT16" runat="server" Font-Names="Calibri" 
                Style="font-size: 14px" Visible="False"></asp:Label>
        </div>
        <div style="float: left; width: 100%; margin-top: 2%">
            <table style="width: 100%">
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 32%">&nbsp;</td>
                    <td style="width: 32%">&nbsp;</td>
                    <td style="width: 32%">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 32%">&nbsp;</td>
                    <td style="width: 32%">&nbsp;</td>
                    <td style="width: 32%">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%"></td>
                    <td style="width: 32%"></td>
                    <td style="width: 32%"></td>
                    <td style="width: 32%"></td>
                    <td style="width: 2%"></td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 32%; text-align: center">TIME KEEPER</td>
                    <td style="width: 32%; text-align: center">MANAGER</td>
                    <td style="width: 32%; text-align: center">SIGNATURE of WORKER</td>
                    <td style="width: 2%">&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
