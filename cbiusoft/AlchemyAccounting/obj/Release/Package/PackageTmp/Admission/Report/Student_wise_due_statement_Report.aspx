<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student_wise_due_statement_Report.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.Student_wise_due_statement_Report" %>

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
            <table style="width:100%">
                <tr>
                    <td style="font-size: xx-large;width:100%; text-align: center;" class="auto-style3">Cox&#39;s Bazar International University</td>
                </tr>
            </table>
        </div>
    <div style="border-top-left-radius:10px; border-top-right-radius:10px;border-bottom-left-radius:10px;border-bottom-right-radius:10px; width:350px; margin:0 auto; text-align: center; color: #FFFFFF; background-color: #666666; font-weight: 700; font-size: x-large;" class="auto-style5" >Student Wise Due Statment </div>
        <hr />
        <div>
            <table class="auto-style1">
                <tr>
                    <td style="width: 10%">Program :</td>
                    <td style="width: 35%">
                        <asp:Label ID="lblProgNM" runat="server"></asp:Label>
                    </td>
                    <td style="width: 10%">Date :</td>
                    <td style="width: 30%">
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="auto-style1">
                <tr>
                    <td>
                        <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="Grid_RowDataBound" Font-Size="9pt" style="margin-bottom: 0px">
                         <Columns>
                             <asp:BoundField  Headertext="STUDENT ID"  >
                                 <HeaderStyle Width="6%" />
                                 <ItemStyle HorizontalAlign="Center" Width="6%" />
                             </asp:BoundField>                     
                             <asp:BoundField  HeaderText="STUDENT NAME" >
                             <HeaderStyle HorizontalAlign="Center" Width="20%" />
                             <ItemStyle HorizontalAlign="Left" Width="20%" />
                             </asp:BoundField>
                              
                             <asp:BoundField  HeaderText="YEAR"    >
                             <HeaderStyle HorizontalAlign="Center" Width="7%" />
                             <ItemStyle HorizontalAlign="Center" Width="7%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="CONTACT"   >
                             <HeaderStyle HorizontalAlign="Center" Width="8%" />
                             <ItemStyle HorizontalAlign="Center" Width="8%" />
                             </asp:BoundField>
                              
                             <asp:BoundField  HeaderText="DEBIT"  >
                             <HeaderStyle HorizontalAlign="Center" Width="8%" />
                             <ItemStyle HorizontalAlign="Right" Width="8%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="CREDIT" >
                              
                             <HeaderStyle Width="8%" />
                             <ItemStyle HorizontalAlign="Right" Width="8%" />
                             </asp:BoundField>
                              <asp:BoundField  HeaderText="BALANCE" >
                              
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

