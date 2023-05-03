<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="CoursesOfProgram.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.CoursesOfProgram" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>:: Coursees Of Program</title>
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
            font-size: xx-large;
        }
        .auto-style3 {
            font-size: larger;
        }
        .auto-style4 {
            width: 100%;
        }
        .auto-style8 {
            background-color: #2aabd2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
      <div style="border-style: double; margin:0 auto; border-width: 2px; border-radius: 10px; border-color: black; width:960px; color: #000000;">
         <div style=" border: 2px double white; border-top-left-radius: 10px; border-top-right-radius: 10px; text-align: center; color: #FFFFFF; font-weight: 700; background-color: #2aabd2;" class="auto-style2" >
                    <span class="auto-style3">L</span>ist<span class="auto-style3"> </span>
                    of<span class="auto-style3"> C</span><span class="auto-style8">ourses <span class="auto-style3">(P</span>rogram<span class="auto-style3">)</span></span></div>
         <table class="auto-style4">
             <tr>
                 <td style="padding:15px">
                     <asp:GridView ID="gv_CourseProgram" runat="server" Width="100%" AutoGenerateColumns="False"  OnRowCreated="gv_CourseProgram_RowCreated" OnRowDataBound="gv_CourseProgram_RowDataBound">
                         <Columns>
                             <asp:BoundField  Headertext="Program name"  >
                                  <HeaderStyle HorizontalAlign="Center" Width="7%" />
                        <ItemStyle HorizontalAlign="Left" Width="7%" Font-Bold="True" Font-Size="12pt" />
                             </asp:BoundField>                     
                             <asp:BoundField  HeaderText="Course Name" >
                                 <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                             </asp:BoundField>
                              
                             <asp:BoundField  HeaderText="Course Code"  >
                                 <HeaderStyle HorizontalAlign="Center" Width="6%" />
                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="Course ID"  >
                                 <HeaderStyle HorizontalAlign="Center" Width="6%" />
                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                             </asp:BoundField>
                              
                         </Columns>
                     </asp:GridView>
                 </td>
             </tr>
         </table>
        </div>
</form>
</body>
</html>
