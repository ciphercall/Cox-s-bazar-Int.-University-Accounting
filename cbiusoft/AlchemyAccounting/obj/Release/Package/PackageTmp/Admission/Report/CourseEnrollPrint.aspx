<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseEnrollPrint.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.CourseEnrollPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Course Enrollment Print</title>
    <script type="text/javascript" src="jquery.js"></script> 
	<script type="text/javascript" src="tableExport.js"></script> 
	<script type="text/javascript" src="jquery.base64.js"></script>
		<script type="text/javascript" src="html2canvas.js"></script>
		<script type="text/javascript" src="sprintf.js"></script>  
    <script src="jspdf.js"></script>
	<script type="text/javascript" src="base64.js"></script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            
        }
        .auto-style2 {
            text-align: right;
        }
        .auto-style3 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div style="border-style: double; margin:0 auto; border-width: 2px; border-radius: 10px; border-color: black; width:960px; color: #000000;">
    <div style="width:960px;margin:0 auto ;">
        <div>
            <table style="width:100%">
                <tr>
                    <td style="font-size: xx-large;width:100%" class="auto-style3">Cox&#39;s Bazar International University</td>
                </tr>
            </table>
        </div>
    <div style="border-top-left-radius:10px; border-top-right-radius:10px;border-bottom-left-radius:10px;border-bottom-right-radius:10px; width:350px; margin:0 auto; text-align: center; color: #FFFFFF; background-color: #666666;" class="auto-style5" >Course Enrollment </div>
     <a href="#" onclick ="$('#form1').tableExport({type:'pdf',escape:'false'});">Create PDF</a>
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
                            <td class="auto-style2" style="width: 15%">Course Name&nbsp; :</td>
                            <td style="width: 30%">
                                <asp:Label ID="lblCrs" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="auto-style2" style="width: 15%">&nbsp;</td>
                            <td style="width: 20%">&nbsp;</td>
                        </tr>
                    </table>
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
                              <asp:BoundField DataField="NEWSTUDENTID" HeaderText="Student ID"  >
                                 <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                             </asp:BoundField>
                             <asp:BoundField  HeaderText="Student ID" Visible="false" >
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
             </div>
    </form>
</body>
</html>
