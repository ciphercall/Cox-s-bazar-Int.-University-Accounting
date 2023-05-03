<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransReportPrint.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.TransReportPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            border: double;
            border-color: black;
            border-width: 2px;
        }

        .auto-style2 {
            width: 526px;
            text-align: center;
            background-color: #CCCCCC;
        }

        .auto-style3 {
            background-color: #CCCCCC;
        }
        .auto-style5 {
            background-color: #CCCCCC;
            height: 39px;
        }
        .auto-style6 {
            text-align: right;
        }
        .auto-style7 {
            background-color: #CCCCCC;
            text-align: center;
        }
        .auto-style8 {
            text-align: center;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 960px; border: double; margin: 0 auto; padding: 20px; border-width: 2px; border-color: black">
            <div style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="font-size: xx-large;border-radius:4px; width: 100%; text-align: center;" class="auto-style3">Cox&#39;s Bazar International University</td>
                    </tr>
                </table>
            </div>
            <div style="border-top-left-radius: 10px; border-top-right-radius: 10px; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px; width: 350px; margin: 0 auto; text-align: center; color: black; font-weight: 700; font-size: x-large;" class="auto-style5">Collection Report</div>
            <hr /> 
           <%-- <table class="auto-style1">
                <tr>
                    <td class="auto-style2"><strong style="width: 40%; text-align: right;">Particular </strong></td>
                    <td class="auto-style7"><strong style="width: 15%">Amount</strong></td>
                    <td class="auto-style7"><strong style="width: 10%"> Vat</strong></td>
                    <td class="auto-style7"><strong style="width: 15%">Total Amount</strong></td>
                </tr>
            </table>--%>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style7" style="width:40%; " > Particular </td>
                      <td style="background-color: #CCCCCC;width:15%" class="auto-style8"  >  
                          Amount
                    </td> 
                </tr>
            </table>
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <div style="border: double; border-width: 2px; border-color: black;">
                        &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblText" Text="Date :" Style="font-weight: bold"></asp:Label>
                        <asp:Label runat="server" ID="lbldate" Text='<%#Eval("TRANSDT") %>' Style="font-weight: bold"></asp:Label>
                    </div>
                    <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                        <ItemTemplate>
                            <div style="border: double; border-width: 1px; border-color: black;">
                            <asp:Label runat="server" ID="lblHead" Text='<%#Eval("h2") %>' Style="font-weight: bold"></asp:Label>
                             <asp:Label runat="server" Visible="false" ID="lbldate1" Text='<%#Eval("TRANSNO") %>' Style="font-weight: bold"></asp:Label>
                                <asp:Label runat="server" Visible="false" ID="lblTransDT" Text='<%#Eval("TRANSDT") %>' Style="font-weight: bold"></asp:Label>
                      </div>
                    <asp:GridView ID="gv_Trans" runat="server" Width="100%" ShowHeader="false" ShowFooter="true" AutoGenerateColumns="False" Font-Size="11pt" Style="margin-bottom: 0px" OnRowDataBound="gv_Trans_RowDataBound">
                        <Columns>                         
                            <asp:BoundField HeaderText="Fees Name">
                                <HeaderStyle Width="40%" />
                                <ItemStyle HorizontalAlign="Left" Width="40%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Amount">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="Right" Width="15%" />
                            </asp:BoundField> 
                        </Columns>
                        <HeaderStyle Font-Size="11pt" BackColor="#CCCCCC" />
                    </asp:GridView>
                           <%-- <div style="border: double; border-width: 1px; border-color: red;">
                    <asp:Label runat="server" ID="lblAmount" Text='<%#Eval("AMOUNT") %>' Style="font-weight: bold"></asp:Label>
                                </div>--%>
                      </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3" style="width:40%; text-align: right;" > Grand Total :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                      <td style="background-color: #CCCCCC;width:15%" class="auto-style6"  > 
            <asp:Label ID="lblTtlAmnt" runat="server"></asp:Label>&nbsp;&nbsp;
                    </td> 
                </tr>
            </table>
        </div>

        <div>
        </div>
    </form>

</body>
</html>
