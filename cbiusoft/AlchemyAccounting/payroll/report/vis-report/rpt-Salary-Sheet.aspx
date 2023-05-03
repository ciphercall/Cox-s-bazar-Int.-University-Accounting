<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-Salary-Sheet.aspx.cs" Inherits="AlchemyAccounting.payroll.report.vis_report.rpt_Salary_Sheet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />
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
        #btnPrint
        {
            font-weight: 700;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table style="width: 100%; font-family: Calibri;">
                <tr>
                    <td style="width: 60%">
                        <asp:Label ID="lblCompNM" runat="server" Font-Size="20px" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="text-align: right; width: 40%">
                        <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60%">
                        <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 9px"></asp:Label>
                    </td>
                    <td style="text-align: right; width: 40%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 60%">
                        <strong>SALARY SHEET</strong></td>
                    <td style="text-align: right; width: 40%">
                        <asp:Label ID="lblTime" runat="server" Style="text-align: right; font-family: Calibri;
                            font-size: 11px;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 60%">
                        MONTH :
                        <asp:Label ID="lblMonth" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; TOTAL DAYS :
                        <asp:Label ID="lblMonthDay" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: right; width: 40%">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div style="width: 100%; margin: 1% 0% 0% 0%;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size="12px"
                    OnRowDataBound="GridView1_RowDataBound" ShowHeaderWhenEmpty="True" 
                    Width="100%" ShowFooter="True">
                    <Columns>
                        <asp:BoundField HeaderText="Employee Name">
                            <HeaderStyle HorizontalAlign="Center" Width="17%" />
                            <ItemStyle HorizontalAlign="Left" Width="17%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Work Days">
                            <HeaderStyle HorizontalAlign="Center" Width="3%" />
                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="OT Days">
                            <HeaderStyle HorizontalAlign="Center" Width="3%" />
                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Rate Per Day">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Rate Per Hour">
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="OT Hour">
                            <HeaderStyle HorizontalAlign="Center" Width="3%" />
                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="OT Amount">
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Basic">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Food">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Other Addition">
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Gross Amount">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Advance">
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Penalty">
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Other Deduction">
                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Net Amount">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle Font-Names="Calibri" Font-Size="14px" Font-Bold="True" />
                    <HeaderStyle Font-Names="Calibri" Font-Size="16px" />
                    <RowStyle Font-Names="Calibri" Font-Size="12px" />
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
