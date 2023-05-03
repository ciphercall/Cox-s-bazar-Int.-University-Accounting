<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultPrint.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.ResultPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Result By Course </title>
    <style type="text/css">
        .gv_style th{
            text-align:center;
        }
        .gv_style tr:hover{
            background:#dec5c5;
        }
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            width: 100%;
            padding:10px;
        }
        .auto-style3 {
            text-align: center;
            width: 100%;
            height: 17px;
        }
        .auto-style5 {
            font-weight: bold;
        }
        .auto-style7 {
            width: 100%;
        }
        .auto-style8 {
            font-size: medium;
        }
        .auto-style9 {
            font-weight: normal;
        }
        .auto-style10 {
            font-size: medium;
            font-weight: normal;
        }
        .auto-style11 {
            width: 15%;
            font-size: medium;
        }
        .auto-style12 {
            width: 35%;
            font-weight: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
  
     <div style="width:960px;margin:0 auto ;height:942px; border:double;border-color:black;border-width:2px">
        <div>
            <table style="width:100%">
                <tr>
                    <td style="font-size: xx-large;width:100%" class="auto-style1">Cox&#39;s Bazar International University</td>
                </tr>
                <tr>
                    <td style="font-size: xx-large;width:100%" class="auto-style1"><div style="border-top-left-radius:10px; border-top-right-radius:10px;border-bottom-left-radius:10px;border-bottom-right-radius:10px; width:350px; margin:0 auto; text-align: center; color: #FFFFFF; background-color: #666666; font-weight: 700; font-size: x-large;" class="auto-style5" >Results</div></td>
                </tr>
                <tr>
                    <td style="font-size: small; " class="auto-style3">
                        <b><asp:Label ID="lblProgNM" runat="server" Font-Bold="True"   CssClass="auto-style8"></asp:Label>
                                &nbsp;&nbsp;
                        &nbsp;-
                        </b>
                        <asp:Label ID="lblSemesterID" runat="server" style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: small;padding:10px; width:100%" >
                        <table class="auto-style7">
                            <tr>
                                <td style="width: 15%" class="auto-style8"><span class="auto-style9">Course
                        </span>
                        <span class="auto-style10">Title</span></td>
                                <td style="width: 35%"><strong>:</strong><asp:Label ID="lblCourseNM" runat="server"   style=" text-align: left" CssClass="auto-style8"></asp:Label>
                                </td>
                                <td class="auto-style11">Batch</td>
                                <td style="width: 20%"><span class="auto-style8">:</span></span><asp:Label ID="lblBatch" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%" class="auto-style8">Course Code</td>
                                <td class="auto-style12"><strong>:</strong><asp:Label ID="lblCourseCD" runat="server"   style=" text-align: left" CssClass="auto-style8"></asp:Label>
                                </td>
                                <td style="width: 15%" class="auto-style8"><strong class="auto-style9">Session</strong></td>
                                <td style="width: 20%"></span><span class="auto-style8">:</span><asp:Label ID="lblSemNM" runat="server"></asp:Label>
                        &nbsp;-
                        <asp:Label ID="lblYR" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </table>
                    </td>
                </tr>
            </table>
        </div>
    
   <div>
       <table class="auto-style2">
           <tr>
               <td>
                     <asp:GridView ID="gv_Result" CssClass="gv_style" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gv_Result_RowDataBound" style="margin-bottom: 0px" >
                         <Columns>
                             <asp:BoundField  Headertext="SL."  >
                                  <HeaderStyle HorizontalAlign="Center" Width="4%" />
                        <ItemStyle HorizontalAlign="Left" Width="4%" Font-Bold="True" Font-Size="12pt" />
                             </asp:BoundField>                     
                             <asp:BoundField   HeaderText="Student ID" >
                                 <HeaderStyle HorizontalAlign="Center" Width="13%" />
                        <ItemStyle HorizontalAlign="Center" Width="13%" />
                             </asp:BoundField>
                              <asp:BoundField  HeaderText="Student Name" >
                                 <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="CGPA"  >
                                 <HeaderStyle HorizontalAlign="Center" Width="6%" />
                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="L. Grade"  >
                                 <HeaderStyle HorizontalAlign="Center" Width="6%" />
                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="Remarks"  >
                                 <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                             </asp:BoundField>
                              
                         </Columns>
                         <HeaderStyle BackColor="#CCCCCC" />
                     </asp:GridView>
               </td>
           </tr>
       </table>
   </div>
          </div>
    </form>
</body>
</html>
