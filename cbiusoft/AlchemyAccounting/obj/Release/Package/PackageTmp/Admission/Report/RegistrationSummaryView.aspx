<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationSummaryView.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.RegistrationSummaryView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            
        }
        .auto-style2 {
            text-align: center;
        }
        .auto-style3 {
            text-align: center;
            font-weight: bold;
        }
        .auto-style4 {
            text-align: center;
            font-size: small;
        }
        .auto-style5 {
            text-align: center;
            font-weight: bold;
            font-size: small;
        }
        .auto-style6 {
            font-size: small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server"> 
    <div>
       <div style="border-style: double; margin:0 auto; border-width: 2px; border-radius: 10px; border-color: black; width:100%; color: #000000;">
     <div>
            <table style="width:100%">
                <tr>
                    <td style="font-size: xx-large;width:100%; text-align: center;" class="auto-style3">Cox&#39;s Bazar International University</td>
                </tr>
            </table>
        </div>
    <div style="border-top-left-radius:10px; border-top-right-radius:10px;border-bottom-left-radius:10px;border-bottom-right-radius:10px; width:370px; margin:0 auto; text-align: center; color: #FFFFFF; background-color: #666666; font-weight: 700; font-size: x-large;" class="auto-style5" >Registration Summary format</div>
        <hr />
           <div>
               <table class="auto-style1">
                   <tr>
                       <td style="width: 10%">Program :</td>
                       <td style="width: 30%">
                           <asp:Label ID="lblProgram" runat="server" ></asp:Label>
                       </td>
                       <td style="width: 10%">Semester :</td>
                       <td style="width: 30%">
                           <asp:Label ID="lblSemester" runat="server" ></asp:Label>
                       </td>
                   </tr>
                   <tr>
                       <td style="width: 10%">Batch :</td>
                       <td style="width: 30%">
                           <asp:Label ID="lblBatch" runat="server" ></asp:Label>
                       </td>
                       <td style="width: 10%">Session :</td>
                       <td style="width: 30%">
                           <asp:Label ID="lblSession" runat="server"  ></asp:Label>
                       </td>
                   </tr>
               </table>
           </div>
           <table    border="1" width="100%">
               <tr>
                   <%--<td rowspan="2" style="width: 4%" class="auto-style5">SL</td>--%>
                   <%--<td rowspan="2" style="width: 8%" class="auto-style5">Student ID</td>
                   <td rowspan="2" style="width: 12%" class="auto-style5">Name of Student</td>--%>
                   <td rowspan="2" style="width: 40%" class="auto-style5">Particulars</td>
                   <td rowspan="2" style="width: 20%" class="auto-style5">Name of School &amp; College</td>
                   <td colspan="2" style="width: 16%" class="auto-style2"><b><span class="auto-style6">Marks/Div./Class / Point/Grade Obtain</span></b></td>
                   <td rowspan="2" style="width: 12%" class="auto-style5">Roll&nbsp; No.</td>
               </tr>
               <tr>
                   <td class="auto-style4" style="width: 8%">Exam</td>
                   <td class="auto-style4" style="width: 8%">Year</td>
               </tr>
           </table>
            <asp:GridView ID="Gv_RegSummery" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="Gv_RegSummery_RowDataBound" Font-Size="9pt" style="margin-bottom: 0px" ShowHeader="False" OnRowCreated="Gv_RegSummery_RowCreated">
                         <Columns>
                             <%--<asp:BoundField >
                                 <HeaderStyle Width="4%" />
                                 <ItemStyle Width="4%" />
                             </asp:BoundField>   --%>
                             <%--<asp:BoundField     >
                                 <HeaderStyle Width="8%" />
                                 <ItemStyle Width="8%" />
                             </asp:BoundField>  
                             <asp:BoundField     >
                                 <HeaderStyle Width="12%" />
                                 <ItemStyle Width="12%" />
                             </asp:BoundField> --%> 
                             <asp:BoundField >
                                 <HeaderStyle Width="40%" />
                                 <ItemStyle Width="40%" />
                             </asp:BoundField>      
                             <asp:BoundField  >
                             <HeaderStyle HorizontalAlign="Center" Width="20%" />
                             <ItemStyle HorizontalAlign="Left" Width="20%" />
                             </asp:BoundField> 
                             <asp:BoundField  >
                             <HeaderStyle HorizontalAlign="Center" Width="8%" />
                             <ItemStyle HorizontalAlign="Center" Width="8%" />
                             </asp:BoundField>
                             <asp:BoundField  >
                             <HeaderStyle HorizontalAlign="Center" Width="8%" />
                             <ItemStyle HorizontalAlign="Center" Width="8%" />
                             </asp:BoundField> 
                             <asp:BoundField > 
                             <HeaderStyle Width="12%" />
                             <ItemStyle HorizontalAlign="Center" Width="12%" />
                             </asp:BoundField> 
                         </Columns> 
                     </asp:GridView>
    </div>
    </form>
</body>
</html>
