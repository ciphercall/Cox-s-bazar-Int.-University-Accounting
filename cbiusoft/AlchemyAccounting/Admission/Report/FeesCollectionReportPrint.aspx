<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeesCollectionReportPrint.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.FeesCollectionReportPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Fees Collection Report</title>
    
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
    <div style="margin:0 auto; width:816px;   border-radius:10px;border-width:2px;border-color:black;border:groove">
        <div>
            <table class="auto-style3">
                <tr>
                    <td style="text-align: center; font-size: xx-large">Cox&#39;s Bazar International University</td>
                </tr>
            </table>
        </div>
    <div style="border-top-left-radius:10px; border-top-right-radius:10px;border-bottom-left-radius:10px;border-bottom-right-radius:10px; width:350px; margin:0 auto; text-align: center; color: #FFFFFF; background-color: #666666;" class="auto-style5" >Fees Collection </div>
        <table class="auto-style3">
            <tr>
                <td>
                    <table class="auto-style3">
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style4" style="width: 20%">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td class="auto-style4" style="width: 20%">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style4" style="width: 20%">From Date&nbsp; :</td>
                            <td>
                                <asp:Label ID="lblFrDT" runat="server"></asp:Label>
                            </td>
                            <td class="auto-style4" style="width: 20%">To Date&nbsp; :</td>
                            <td>
                                <asp:Label ID="lblToDT" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td style="padding:5px">
                    <asp:GridView ID="gv_Fees" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="12pt" OnRowDataBound="gv_Fees_RowDataBound">
                        <Columns>
                            <asp:BoundField  HeaderText="Fees ID">
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Fees Particulars">
                            <HeaderStyle Width="20%" />
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Amount">
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Right" Width="10%" />
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