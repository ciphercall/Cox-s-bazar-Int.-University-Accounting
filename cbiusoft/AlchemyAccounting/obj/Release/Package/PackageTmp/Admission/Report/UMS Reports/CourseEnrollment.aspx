<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseEnrollment.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.CourseEnrollment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>:: Course Enrollment</title>
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
            text-align: center;
            color: #FFFFFF;
            font-weight: 700;
            border: 2px double white;
            background-color: #2aabd2;
        }
        .auto-style3 {
            font-size: larger;
        }
        .auto-style4 {
            width: 100%;
        }
        .auto-style5 {
            text-align: right;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="Script1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
    <div style="border-style: double; margin:0 auto; border-width: 2px; border-radius: 10px; border-color: black; width:960px; color: #000000;">
         <div style=" border-top-left-radius: 10px; border-top-right-radius: 10px; " class="auto-style2" >
                    <span class="auto-style3">C</span>ourse <span class="auto-style3">E</span>nrollment</div>
         <table class="auto-style4">
             <tr>
                 <td>
                     <table class="auto-style4">
                         <tr>
                             <td style="text-align: right; " class="auto-style6">Semester Name&nbsp;&nbsp; :</td>
                             <td class="auto-style7">
                                 <asp:DropDownList ID="ddlSemNM"  runat="server" CssClass="form-control" Width="120px" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemNM_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <asp:Label ID="lblSemID" runat="server" Visible="False"></asp:Label>
                             </td>
                             <td class="auto-style8">Academic Year&nbsp; :</td>
                             <td class="auto-style9">
                                 <asp:DropDownList ID="ddlYR"  runat="server" CssClass="form-control" Height="30px" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="ddlYR_SelectedIndexChanged">
                                 </asp:DropDownList>
                             </td>
                         </tr>
                         <tr>
                             <td style="text-align: right; width:15%">Course Name&nbsp;&nbsp; :</td>
                             <td style="width:20%">
                                 <asp:DropDownList ID="ddlCrsNM"  runat="server" Width="230px" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="ddlCrsNM_SelectedIndexChanged">
                                 </asp:DropDownList>
                             </td>
                             <td style="width:25%" class="auto-style5">
                                 <asp:Label ID="lblCrsID" runat="server" Visible="False"></asp:Label>
                                 </td>
                             <td style="width:35%">
                                 <asp:Button ID="btnPrint" runat="server" Text="Print" BackColor="#2aabd2" BorderColor="#333333" BorderWidth="2px" Font-Bold="True" ForeColor="White" Height="100%" OnClick="btnPrint_Click" Width="120px" />
                             </td>
                         </tr>
                     </table >
                   
                     <div style="padding:10px">
                     <asp:GridView ID="gv_Course" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gv_Course_RowDataBound" style="margin-bottom: 0px">
                         <Columns>
                             <%--<asp:BoundField  Headertext="Course Name"  >
                                  <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" Width="15%" Font-Bold="True" Font-Size="12pt" />
                             </asp:BoundField>  --%>                   
                             <asp:BoundField  HeaderText="Student Name" >
                                 <HeaderStyle HorizontalAlign="Left" Width="25%" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                             </asp:BoundField>
                              
                             <asp:BoundField  HeaderText="Student ID"  >
                                 <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="Enroll Date"  >
                                 <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="Remarks"  >
                                 <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                             </asp:BoundField>
                              
                         </Columns>
                         <HeaderStyle BackColor="#CCCCCC" />
                     </asp:GridView>
                         </div>
                 </td>
             </tr>
         </table>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
