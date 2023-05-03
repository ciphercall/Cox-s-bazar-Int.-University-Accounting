<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceivableReport.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.ReceivableReport" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Students Reports</title>
     <link href="../../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />
    
    <script type="text/javascript">

        function pageLoad() {
            $("#txtFromDT,#txtToDT").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+5" });
        }
       
    </script>
    <style type="text/css">
        .txtColor:focus {
           border:solid 4px green !important;
        }
          .txtColor
        {
            margin-left: 0px;
            border-radius:10px;
            
            }
 
          .right{
              float:right;
          }
        .auto-style11 {
            font-size: xx-large;
        }
        </style>
</head>
<body>
     <form id="form1" runat="server">
    <asp:ScriptManager ID="Script1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
   
      <div style="border-style: double; margin:0 auto; border-width: 2px; border-radius: 10px; border-color: black; width:960px; color: #000000;">
         <div style=" border: 2px double white; border-top-left-radius: 10px; border-top-right-radius: 10px; text-align: center; color: #FFFFFF; font-weight: 700; background-color: #2aabd2; font-size: xx-large;" class="auto-style11" >
                    Receivable <span class="auto-style1">R</span>eport</div>
         <table style="width:100%;">
             <tr>
                 <td style="padding:15px">
                     <div>

                   
                   
                         <table class="nav-justified">
                             <tr>
                                 <td class="text-right" style="width: 40%">Form Date :</td>
                                 <td style="width: 30%">
                                     <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 <td style="width: 30%">&nbsp;</td>
                             </tr>
                             <tr>
                                 <td class="text-right">To Date :</td>
                                 <td>
                                     <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 <td>
                                     <asp:Label ID="lblMSG" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td>&nbsp;</td>
                                 <td>
                                     <asp:Button ID="btnSubmit" runat="server" CssClass="form-control right" Text="Preview" Width="100px" BackColor="#3399FF" BorderColor="#3399FF" Font-Bold="True" ForeColor="White" />
                                 </td>
                                 <td>&nbsp;</td>
                             </tr>
                         </table>

                   
                   
                     </div>
                     <br />
                     
                 </td>
             </tr>
         </table>
        </div>

            </ContentTemplate></asp:UpdatePanel></form>
    </body>
</html>

