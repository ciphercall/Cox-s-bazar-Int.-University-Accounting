<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FessCollectionStudentWiseaspxPrint.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.FessCollectionStudentWiseaspxPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Course Reg Print</title>
    <style type="text/css">
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            text-align: right;
        }
        .auto-style5 {
            font-size: x-large;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:0 auto; width:816px; height:1104px; border-radius:10px;border-width:2px;border-color:black;border:groove">
        <div>
            <table class="auto-style3">
                <tr>
                    <td style="text-align: center; font-size: xx-large">Cox&#39;s Bazar International University</td>
                </tr>
            </table>
        </div>
    <div style="border-top-left-radius:10px; border-top-right-radius:10px;border-bottom-left-radius:10px;border-bottom-right-radius:10px; width:350px; margin:0 auto; text-align: center; color: #FFFFFF; background-color: #666666;" class="auto-style5" >Fees Collection&nbsp; By Students</div>
        <table class="auto-style3">
            <tr>
                <td>
                    <table class="auto-style3">
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style4" style="width: 12%">&nbsp;</td>
                            <td style="width: 30%">
                                &nbsp;</td>
                            <td class="auto-style4" style="width: 12%">&nbsp;</td>
                            <td style="width: 15%">
                                &nbsp;</td>
                            <td style="width: 11%">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style4" style="width: 12%">Fees Name&nbsp; :</td>
                            <td style="width: 30%">
                                <asp:Label ID="lblFeesNM" runat="server"></asp:Label>
                            </td>
                            <td class="auto-style4" style="width: 12%">From Date&nbsp; :</td>
                            <td style="width: 15%">
                                <asp:Label ID="lblFrDT" runat="server"></asp:Label>
                            </td>
                            <td style="width: 11%; text-align: right;">
                                To Date&nbsp; :</td>
                            <td>
                                <asp:Label ID="lblToDT" runat="server"></asp:Label>
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td style="padding:5px">
                    <asp:GridView ID="gv_Fees" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="12pt" OnRowDataBound="gv_Fees_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="NEWSTUDENTID"  HeaderText="Student ID">
                            <HeaderStyle Width="8%" />
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                             <asp:BoundField  HeaderText="Student ID" Visible="false">
                            <HeaderStyle Width="8%" />
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Student Name">
                            <HeaderStyle Width="20%" />
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Program">
                            <HeaderStyle Width="16%" />
                            <ItemStyle HorizontalAlign="Left" Width="16%" />
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Amount">
                            <HeaderStyle Width="8%" />
                            <ItemStyle HorizontalAlign="Right" Width="8%" />
                            </asp:BoundField>
                       
                        </Columns>
                        <HeaderStyle BackColor="#CCCCCC" />
                    </asp:GridView>
                    <div>
                        <table class="auto-style3">
                            <tr>
                                <td style="width: 30%; text-align: right;" colspan="2">&nbsp;</td>
                                <td style="text-align: right;">Total Amount&nbsp; :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblAmount" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; text-align: right;">In Word&nbsp; :&nbsp;&nbsp; </td>
                                <td style="text-align: left;" colspan="2">
                                <asp:Label ID="lblInWord" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                   
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>