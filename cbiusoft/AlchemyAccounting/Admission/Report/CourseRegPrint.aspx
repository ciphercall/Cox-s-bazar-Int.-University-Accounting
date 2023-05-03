<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseRegPrint.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.CourseRegPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Course Reg Print</title>
    <style type="text/css">
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            text-align: right;
        }
        .auto-style5 {
            font-size: x-large;
        }
        .auto-style6 {
            text-align: center;
        
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:0 auto; width:800px; height:942px border-radius:10px;border-width:2px;border-color:black;border:groove">
        <div>
            <table class="auto-style3">
                <tr>
                    <td style="text-align: center; font-size: xx-large">Cox&#39;s Bazar International University</td>
                </tr>
            </table>
        </div>
    <div style="border-top-left-radius:10px; border-top-right-radius:10px;border-bottom-left-radius:10px;border-bottom-right-radius:10px; width:350px; margin:0 auto; text-align: center; color: #FFFFFF; background-color: #666666;" class="auto-style5" >Course Registration </div>
        <table class="auto-style3">
            <tr>
                <td>
                    <table class="auto-style3">
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style4" style="width: 20%">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td class="auto-style4" style="width: 20%">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style4" style="width: 20%">Registration Year&nbsp; :</td>
                            <td>
                                <asp:Label ID="lblRegYR" runat="server"></asp:Label>
                            </td>
                            <td class="auto-style4" style="width: 20%">Semester Name&nbsp; :</td>
                            <td>
                                <asp:Label ID="lblSemNM" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style4" style="width: 20%">Program Name :</td>
                            <td>
                                <asp:Label ID="lblProNM" runat="server"></asp:Label>
                            </td>
                            <td class="auto-style4" style="width: 20%">Student ID&nbsp; :</td>
                            <td>
                                <asp:Label ID="lblStuID" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style4" style="width: 20%">Session&nbsp; :</td>
                            <td>
                                <asp:Label ID="lblSession" runat="server"></asp:Label>
                            </td>
                            <td class="auto-style4" style="width: 20%">Batch&nbsp;&nbsp; :</td>
                            <td>
                                <asp:Label ID="lblBtch" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding:5px">
                    <asp:GridView ID="gv_CrsReg" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="11pt" OnRowDataBound="gv_CrsReg_RowDataBound">
                        <Columns>
                            <asp:BoundField  HeaderText="SEMESTER SL">
                            <HeaderStyle Width="7%" />
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="COURSEID" SortExpression="COURSEID">
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="CREDIT HH" SortExpression="CREDITHH">
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="CREDIT COST" SortExpression="CRCOST">
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="REMARKS" SortExpression="REMARKS">
                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#CCCCCC" />
                    </asp:GridView>
                    <div>
                        <table class="auto-style3">
                            <tr>
                                <td style="width: 2%">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 3%">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 3%">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 2%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 2%">&nbsp;</td>
                                <td colspan="5">Note : Please mark &quot;<strong>R</strong>&quot; for retake and &quot;<strong>Imp</strong>&quot; for inprovment of grade of course/s in the remark column.Improvment must be done within
                                    two consecutive semester</td>
                                <td style="width: 2%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 2%">&nbsp;</td>
                                <td colspan="5">
                                    <table class="auto-style3">
                                        <tr>
                                            <td style="width: 40%"><strong>Advisor&#39;s Comment </strong>
                                                <br />
                                                Maximum credits elegible for :<br />
                                                Credit recommended :<br />
                                            </td>
                                            <td style="width: 20%">&nbsp;</td>
                                            <td style="width: 40%">
                                                <br />
                                                Name of the Advisor/Seal :<br />
                                                <br />
                                                Signature.................................................</td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 2%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 2%">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 3%">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 3%">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 2%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 2%">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 3%">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 3%">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 2%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style6" style="width: 2%">&nbsp;</td>
                                <td style=" border-top:double;" class="auto-style6">Student Signature</td>
                                <td class="auto-style6" style="width: 3%">&nbsp;</td>
                                <td style=" border-top:double" class="auto-style6">Signature of Accounts Department</td>
                                <td class="auto-style6" style="width: 3%">&nbsp;</td>
                                <td style=" border-top:double" class="auto-style6">Signature of the Head of Department</td>
                                <td class="auto-style6" style="width: 2%">&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
