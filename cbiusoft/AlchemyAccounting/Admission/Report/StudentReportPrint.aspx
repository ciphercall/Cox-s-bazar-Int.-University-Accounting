﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentReportPrint.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.StudentReportPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: right;
        }
    </style>
</head>
<body>
     <form id="form1" runat="server">
    <div style="border-style: double; margin:0 auto; border-width: 2px; border-radius: 10px; border-color: black; width:960px; color: #000000;">
     <div>
            <table style="width:100%">
                <tr>
                    <td style="font-size: xx-large;width:100%; text-align: center;" class="auto-style3">Cox&#39;s Bazar International University</td>
                </tr>
            </table>
        </div>
    <div style="border-top-left-radius:10px; border-top-right-radius:10px;border-bottom-left-radius:10px;border-bottom-right-radius:10px; width:350px; margin:0 auto; text-align: center; color: #FFFFFF; background-color: #666666; font-weight: 700; font-size: x-large;" class="auto-style5" >Student Report</div>
        <hr />
     
        <table class="auto-style1">
            <tr>
                
                <td>
                   <table class="auto-style1">
                        <tr>
                            <td class="auto-style2" style="width: 15%">Semester&nbsp; :</td>
                            <td style="width: 30%">
                                <asp:Label ID="lblSem" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="auto-style2" style="width: 15%">Academic Year&nbsp; :</td>
                            <td style="width: 20%">
                                <asp:Label ID="lblYR" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2" style="width: 15%">Program Name&nbsp; :</td>
                            <td style="width: 30%">
                                <asp:Label ID="lblProg" runat="server" Font-Bold="True"></asp:Label> - <%=Session["BATCH"].ToString() %> Batch
                            </td>
                            <td class="auto-style2" style="width: 15%">&nbsp;</td>
                            <td style="width: 20%">&nbsp;</td>
                        </tr>
                    </table></td>
            </tr>
            <tr>
                <td style="padding:10px">
                    <asp:GridView ID="gv_Student" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gv_Student_RowDataBound" Font-Size="9pt" style="margin-bottom: 0px">
                         <Columns>
                             <asp:BoundField  Headertext="SL."  >
                                 <HeaderStyle Width="2%" />
                                 <ItemStyle  HorizontalAlign="Center" Width="2%" />
                             </asp:BoundField>  
                             <asp:BoundField  HeaderText="ID" >
                             <HeaderStyle HorizontalAlign="Center" Width="5%" />
                             <ItemStyle HorizontalAlign="Center" Width="5%" />
                             </asp:BoundField>                   
                             <asp:BoundField  HeaderText="STUDENT NAME" >
                             <HeaderStyle HorizontalAlign="Center" Width="15%" />
                             <ItemStyle HorizontalAlign="Left" Width="15%" />
                             </asp:BoundField>
                              
                             <asp:BoundField  HeaderText="MOBILE NO"    >
                             <HeaderStyle HorizontalAlign="Center" Width="7%" />
                             <ItemStyle HorizontalAlign="Center" Width="7%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="SESSION"   >
                             <HeaderStyle HorizontalAlign="Center" Width="6%" />
                             <ItemStyle HorizontalAlign="Center" Width="6%" />
                             </asp:BoundField>
                              
                             <asp:BoundField  HeaderText="BATCH"  >
                             <HeaderStyle HorizontalAlign="Center" Width="3%" />
                             <ItemStyle HorizontalAlign="Center" Width="3%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="ADMISSION DATE" >
                              
                             <HeaderStyle HorizontalAlign="Center" Width="6%" />
                             <ItemStyle HorizontalAlign="Center" Width="6%" />
                             </asp:BoundField>
                              
                         </Columns>
                         <HeaderStyle Font-Size="8pt" BackColor="#CCCCCC" />
                     </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
