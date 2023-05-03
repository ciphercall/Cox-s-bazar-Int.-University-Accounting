<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MOnthlySalaryReportPrint.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.MOnthlySalaryReportPrint" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Fees Collection Report</title>
    
    <style type="text/css">
        .auto-style3 {
            width: 100%;
        }
        .auto-style5 {
            font-size: x-large;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:0 auto;margin:0 auto;  border-radius:10px;border-width:2px;border-color:black;border:groove">
        <div>
            <table class="auto-style3">
                <tr>
                    <td style="text-align: center; font-size: xx-large">Cox&#39;s Bazar International University</td>
                </tr>
            </table>
        </div>
    <div style="border-top-left-radius:10px; border-top-right-radius:10px;border-bottom-left-radius:10px;border-bottom-right-radius:10px; width:350px; margin:0 auto; text-align: center; color: #FFFFFF; background-color: #666666;" class="auto-style5" >Monthly Salary Report </div>
        <table class="auto-style3">
            <tr>
                <td>
                    <table class="auto-style3">
                        <tr>
                            <td style="width: 15%">Month : </td>
                            <td style="85%">
                                <asp:Label ID="lblMonth" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%">Department : </td>
                            <td style="85%">
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td style="padding:5px">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="12pt" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                           
                            <asp:BoundField  HeaderText="Employee Name" > 
                                <HeaderStyle Width="10%"/>
                                <ItemStyle Width="10%"/>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Post Name"> 
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Joining Date"> 
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Month Day" > 
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Holiday" > 
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Present Day" >
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Absent day" > 
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Leave Day" > 
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Basic Salary" >
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/> 
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Allowance" > 
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Total Paid" > 
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Advanc" > 
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Net Paid" > 
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                       <asp:BoundField  HeaderText="Bank AC/NO." > 
                                <HeaderStyle Width="5%"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#CCCCCC" Font-Size="9pt" />
                    </asp:GridView>
                    <div>
                    </div>
                   
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>