<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmissionReport.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.AdmissionReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: Admission Report</title>
    <style type="text/css">
        .txtColor:focus {
           border:solid 4px green !important;
        }
          .txtColor
        {
            margin-left: 0px;
            }
 

        .auto-style4 {
            width: 100%;
        }
        .auto-style6 {
            width: 15%;
            height: 26px;
        }
        .auto-style7 {
            width: 20%;
            height: 26px;
        }
        .auto-style8 {
            text-align: right;
            width: 25%;
            height: 26px;
        }
        .auto-style9 {
            width: 35%;
            height: 26px;
        }
        .auto-style5 {
            text-align: right;
        }
        .auto-style10 {
            width: 20%;
        }
        .auto-style11 {
            font-size: xx-large;
        }
        .auto-style12 {
            font-size: larger;
        }
    </style>
</head>
<body>
      <form id="form1" runat="server">
      <asp:ScriptManager ID="Script1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
  
      <div style="border-style: double; margin:0 auto; border-width: 2px; border-radius: 10px; border-color: black; width:960px; color: #000000;">
         <div style=" border: 2px double white; border-top-left-radius: 10px; border-top-right-radius: 10px; text-align: center; color: #FFFFFF; font-weight: 700; background-color: #2aabd2;" class="auto-style11" >
                    <span class="auto-style12">A</span>dmission <span class="auto-style12">R</span>eport</div>
         <table style="width:100%;">
             <tr>
                 <td style="padding:15px">
                     <div>

                     <table class="auto-style4">
                         <tr>
                             <td style="text-align: right; " class="auto-style6">Semester Name&nbsp;&nbsp; :</td>
                             <td class="auto-style7" style="width: 35%">
                                 <asp:DropDownList ID="ddlSemNM" CssClass="txtColor" runat="server" Width="120px" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemNM_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <asp:Label ID="lblSemID" runat="server" Visible="False"></asp:Label>
                             </td>
                             <td class="auto-style8" style="width: 20%">Academic Year&nbsp; :</td>
                             <td class="auto-style9">
                                 <asp:DropDownList ID="ddlYR"  CssClass="txtColor" runat="server" Height="30px" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="ddlYR_SelectedIndexChanged" >
                                 </asp:DropDownList>
                             </td>
                         </tr>
                         <tr>
                             <td style="text-align: right; width:15%">Program Name&nbsp;&nbsp; :</td>
                             <td style="width:35%" class="auto-style10">
                                 <asp:DropDownList ID="ddlProgNM"  CssClass="txtColor" runat="server" Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgNM_SelectedIndexChanged" >
                                 </asp:DropDownList>
                             </td>
                             <td style="width:20%" class="auto-style5">
                                 <asp:Label ID="lblProgID" runat="server" Visible="False"></asp:Label>
                                 </td>
                             <td style="width:35%">
                                 <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="txtColor" BackColor="#0099FF" BorderColor="#333333" BorderWidth="2px" Font-Bold="True" ForeColor="White" Height="100%"  Width="120px" OnClick="btnPrint_Click1" />
                             </td>
                         </tr>
                     </table >
                   
                     </div>
                     <br />
                     <asp:GridView ID="gv_Admission" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gv_CourseProgram_RowDataBound" Font-Size="9pt" style="margin-bottom: 0px">
                         <Columns>
                             <asp:BoundField  Headertext="ROLL NO"  >
                                 <HeaderStyle Width="6%" />
                                 <ItemStyle Width="6%" />
                             </asp:BoundField>                     
                             <asp:BoundField  HeaderText="STUDENT NAME" >
                             <HeaderStyle HorizontalAlign="Center" Width="15%" />
                             <ItemStyle HorizontalAlign="Left" Width="15%" />
                             </asp:BoundField>
                              
                             <asp:BoundField  HeaderText="MOBILE NO"    >
                             <HeaderStyle HorizontalAlign="Center" Width="7%" />
                             <ItemStyle HorizontalAlign="Center" Width="7%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="MR DATE"   >
                             <HeaderStyle HorizontalAlign="Center" Width="8%" />
                             <ItemStyle HorizontalAlign="Center" Width="8%" />
                             </asp:BoundField>
                              
                             <asp:BoundField  HeaderText="MR NO"  >
                             <HeaderStyle HorizontalAlign="Center" Width="7%" />
                             <ItemStyle HorizontalAlign="Left" Width="7%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="MR AMOUNT" >
                              
                             <HeaderStyle Width="6%" />
                             <ItemStyle HorizontalAlign="Right" Width="6%" />
                             </asp:BoundField>
                              
                         </Columns>
                         <HeaderStyle Font-Size="8pt" />
                     </asp:GridView>
                 </td>
             </tr>
         </table>
        </div>

            </ContentTemplate></asp:UpdatePanel></form>
</body>
</html>
