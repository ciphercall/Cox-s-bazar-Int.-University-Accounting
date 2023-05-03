<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentLedgerReport.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.StudentLedgerReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="font-size: xx-large; width: 100%; text-align: center;" class="auto-style3">Cox&#39;s Bazar International University</td>
                    </tr>
                </table>
            </div>
            <div style="border-top-left-radius: 10px; border-top-right-radius: 10px; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px; width: 350px; margin: 0 auto; text-align: center; color: #FFFFFF; background-color: #666666; font-weight: 700; font-size: x-large;" class="auto-style5">Student Ledger Report</div>
            <hr />
            <div>

                <table class="auto-style1">
                    <tr>
                        <td style="width: 5%">&nbsp;</td>
                        <td style="width: 10%">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="width: 5%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td style="width: 10%">Student ID :</td>
                        <td style="width: 28%">
                            <asp:Label ID="lblStuID" runat="server"></asp:Label>
                        </td>
                        <td style="width: 12%">Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                        <td style="width: 28%">
                            <asp:Label ID="lblStuNM" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Program&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                        <td>
                            <asp:Label ID="lblProgram" runat="server"></asp:Label> - <asp:Label ID="lblBatch1" runat="server"></asp:Label>
                        </td>
                        <td>Admit Year : </td>
                        <td>
                            <asp:Label ID="lblYR" runat="server"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:GridView ID="Gv_StuLedger" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="Gv_StuLedger_RowDataBound" Font-Size="9pt" Style="margin-bottom: 0px">
                                <Columns>
                                    <asp:BoundField HeaderText="Date">
                                        <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Transaction No">
                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Semester" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Year">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Fees">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Remarks">

                                        <HeaderStyle Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Debit">

                                        <HeaderStyle Width="6%" />
                                        <ItemStyle HorizontalAlign="Right" Width="6%" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Credit">

                                        <HeaderStyle Width="6%" />
                                        <ItemStyle HorizontalAlign="Right" Width="6%" />
                                    </asp:BoundField>

                                    <asp:BoundField HeaderText="Balance">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                    </asp:BoundField>

                                </Columns>
                                <HeaderStyle Font-Size="8pt" BackColor="#CCCCCC" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </form>
</body>
</html>
