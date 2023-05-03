<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="ProgramList.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.ProgramList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>:: Program List Report</title>
    <link href="../../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type ="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>
    <style type="text/css">
        .auto-style2 {
            border: 2px double white;
            font-size: xx-large;
            text-align: center;
            color: #FFFFFF;
            font-weight: 700;
            background-color: #2aabd2;
        }
        .auto-style4 {
            width: 100%;
        }
        .auto-style5 {
            font-size: larger;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

     <div style="border-style: double; margin:0 auto; border-width: 2px; border-radius: 10px; border-color: black; width:960px; color: #000000;">
         <div style=" border-top-left-radius: 10px; border-top-right-radius: 10px; " class="auto-style2" >
                    &nbsp;<span class="auto-style5">L</span>ist of <span class="auto-style5">P</span>rograms</div>
         <table class="auto-style4">
             <tr>
                 <td style="padding:10px">
                     <br />
                    <asp:GridView ID="gv_ProgramList" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="PROGRAMID" DataSourceID="SqlDataSource1" BorderColor="#333333" BorderWidth="1px" Font-Size="10pt">
                         <Columns>
                             
                             <asp:BoundField  DataField="PROGRAMNM" HeaderText="PROGRAMNM" SortExpression="PROGRAMNM" > 
                             </asp:BoundField> 
                             <asp:BoundField DataField="PROGRAMID" HeaderText="PROGRAMID"  SortExpression="PROGRAMID" ReadOnly="True" >
                             </asp:BoundField> 
                             <asp:BoundField DataField="PROGRAMSID" HeaderText="PROGRAMSID" SortExpression="PROGRAMSID">
                             </asp:BoundField> 
                             <asp:BoundField DataField="TOTCREDIT" HeaderText="TOTCREDIT" SortExpression="TOTCREDIT" >
                             </asp:BoundField>
                             <asp:BoundField DataField="COSTPERCR" HeaderText="COSTPERCR" SortExpression="COSTPERCR" >
                             </asp:BoundField>
                             <asp:BoundField DataField="TOTFEES" HeaderText="TOTFEES" SortExpression="TOTFEES" >
                             </asp:BoundField>
                         </Columns>

                         <HeaderStyle BackColor="#CCCCCC" />

                     </asp:GridView> 
                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" Global.connection="<%$Global.connections:asl_cbiuGlobal.connection %>" SelectCommand="SELECT [PROGRAMNM], [PROGRAMID], [PROGRAMSID], [TOTCREDIT], [COSTPERCR], [TOTFEES] FROM [EIM_PROGRAM]"></asp:SqlDataSource>
                 </td>
             </tr>
         </table>
        </div>
 </form>
</body>
</html>
