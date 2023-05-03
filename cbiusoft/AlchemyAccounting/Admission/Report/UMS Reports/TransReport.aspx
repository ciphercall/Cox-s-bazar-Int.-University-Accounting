<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="TransReport.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.TransReport" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Students Reports</title>
     <link href="../../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            $("#txtFromDT,#txtToDT,#txtFromDTBNK,#txtToDTBNK").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+5" });
        }
       
    </script>
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
            text-align: left;
        }
        .auto-style11 {
            font-size: xx-large;
        }
        .auto-style12 {
            width: 35%;
            height: 26px;
            text-align: center;
            background-color: #CCCCCC;
        }
        .auto-style13 {
            text-align: center;
            width: 25%;
            height: 26px;
            background-color: #CCCCCC;
        }
        .auto-style14 {
            height: 26px;
            text-align: center;
            background-color: #CCCCCC;
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
                    Collection <span class="auto-style1">R</span>eport</div>
         <table style="width:100%;">
             <tr>
                 <td style="padding:15px">
                     <div>

                     <table class="auto-style4">
                         <tr>
                             <td style="asp: GridView ID=&quot;gv_Trans&quot; runat=&quot;server&quot; Width=&quot;100%&quot; AutoGenerateColumns=&quot;False&quot; Font-Size=&quot;9pt&quot; style=&quot;margin-bottom: 0px&quot;&gt;;" class="auto-style14">&nbsp;</td>
                             <td class="auto-style13" style="width: 15%">&nbsp;</td>
                             <td class="auto-style13" style="width: 25%">
                                COLLECTION IN CASH</td>
                             <td class="auto-style12" style="width: 30%">
                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td class="auto-style6" style="text-align: right; asp: GridView ID=&quot;gv_Trans&quot; runat=&quot;server&quot; Width=&quot;100%&quot; AutoGenerateColumns=&quot;False&quot; Font-Size=&quot;9pt&quot; style=&quot;margin-bottom: 0px&quot;&gt;;">&nbsp;</td>
                             <td class="auto-style8" style="width: 15%">From Date&nbsp; :</td>
                             <td class="auto-style8" style="width: 25%">
                                 <asp:TextBox ID="txtFromDT" runat="server" AutoPostBack="True" ClientIDMode="Static" Height="100%" OnTextChanged="txtFromDT_TextChanged" Width="100%"></asp:TextBox>
                             </td>
                             <td class="auto-style9" style="width: 30%">
                                 <asp:Label ID="lblMSG" runat="server" ForeColor="Red" style="text-align: left" Visible="False"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td class="auto-style6" style="text-align: right; ">&nbsp;</td>
                             <td class="auto-style8" style="width: 8%">To Date&nbsp; :</td>
                             <td class="auto-style8" style="width: 20%">
                                 <asp:TextBox ID="txtToDT" runat="server" AutoPostBack="True" ClientIDMode="Static" Height="100%" OnTextChanged="txtToDT_TextChanged" Width="100%"></asp:TextBox>
                             </td>
                             <td class="auto-style9">&nbsp;</td>
                         </tr>
                         <tr>
                             <td class="auto-style6" style="text-align: right; ">&nbsp;</td>
                             <td class="auto-style8" style="width: 8%">&nbsp;</td>
                             <td class="auto-style8" style="width: 20%">
                                 <asp:Button ID="btnPrint" runat="server" BackColor="#0099FF" BorderColor="#333333" BorderWidth="2px" CssClass="txtColor" Font-Bold="True" ForeColor="White" Height="30px" OnClick="btnPrint_Click" Text="Print" Width="120px" />
                             </td>
                             <td class="auto-style9">&nbsp;</td>
                         </tr>
                     </table >
                   <br />
                     </div>
                     <br />
                     
                     <table class="auto-style4">
                         <tr>
                             <td class="auto-style14" style="asp: GridView ID=&quot;gv_Trans&quot; runat=&quot;server&quot; Width=&quot;100%&quot; AutoGenerateColumns=&quot;False&quot; Font-Size=&quot;9pt&quot; style=&quot;margin-bottom: 0px&quot;&gt;;">&nbsp;</td>
                             <td class="auto-style13" style="width: 15%">&nbsp;</td>
                             <td class="auto-style13" style="width: 25%">COLLECTION IN BANK</td>
                             <td class="auto-style12" style="width: 30%">&nbsp;</td>
                         </tr>
                         <tr>
                             <td class="auto-style6" style="text-align: right; asp: GridView ID=&quot;gv_Trans&quot; runat=&quot;server&quot; Width=&quot;100%&quot; AutoGenerateColumns=&quot;False&quot; Font-Size=&quot;9pt&quot; style=&quot;margin-bottom: 0px&quot;&gt;;">&nbsp;</td>
                             <td class="auto-style8" style="width: 15%">From Date&nbsp; :</td>
                             <td class="auto-style8" style="width: 25%">
                                 <asp:TextBox ID="txtFromDTBNK" runat="server" AutoPostBack="True" ClientIDMode="Static" Height="100%"  Width="100%" OnTextChanged="txtFromDTBNK_TextChanged"></asp:TextBox>
                             </td>
                             <td class="auto-style9" style="width: 30%">
                                 <asp:Label ID="lblMSGBNK" runat="server" ForeColor="Red" style="text-align: left" Visible="False"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td class="auto-style6" style="text-align: right; ">&nbsp;</td>
                             <td class="auto-style8" style="width: 8%">To Date&nbsp; :</td>
                             <td class="auto-style8" style="width: 20%">
                                 <asp:TextBox ID="txtToDTBNK" runat="server" AutoPostBack="True" ClientIDMode="Static" Height="100%"  Width="100%" OnTextChanged="txtToDTBNK_TextChanged"></asp:TextBox>
                             </td>
                             <td class="auto-style9">&nbsp;</td>
                         </tr>
                         <tr>
                             <td class="auto-style6" style="text-align: right; ">&nbsp;</td>
                             <td class="auto-style8" style="width: 8%">&nbsp;</td>
                             <td class="auto-style8" style="width: 20%">
                                 <asp:Button ID="btnPrintBNK" runat="server" BackColor="#0099FF" BorderColor="#333333" BorderWidth="2px" CssClass="txtColor" Font-Bold="True" ForeColor="White" Height="30px"  Text="Print" Width="120px" OnClick="btnPrintBNK_Click" />
                             </td>
                             <td class="auto-style9">&nbsp;</td>
                         </tr>
                     </table>
                     
                 </td>
             </tr>
         </table>
        </div>

            </ContentTemplate></asp:UpdatePanel></form>
    </body>
</html>

