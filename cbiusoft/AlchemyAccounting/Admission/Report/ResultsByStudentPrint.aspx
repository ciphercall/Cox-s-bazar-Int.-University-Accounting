<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultsByStudentPrint.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.ResultsByStudentPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 78%;
        }
        .auto-style3 {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 960px;margin:0 auto; border-color: black; border-width: 2px; border: double">

            <table class="auto-style1">
                <tr>
                    <td>
                        <table class="auto-style1">
                            <tr>
                                <td style="width: 3%;" class="auto-style2">&nbsp;</td>
                                <td colspan="4" style="text-align: center; font-size: xx-large">Cox&#39;s Bazar International University</td>
                                <td style="width: 3%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 3%;" class="auto-style2">
                                    
                                </td>
                                
                                <td colspan="4"><div style="border-top-left-radius: 10px; border-top-right-radius: 10px; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px; width: 350px; margin: 0 auto; text-align: center; color: #FFFFFF; background-color: #666666; font-weight: 700; font-size: x-large;" class="auto-style5">Results</div><hr /></td>
                                <td style="width: 3%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2" style="width: 3%">&nbsp;</td>
                                <td class="auto-style3">Student Name&nbsp; :</td>
                                <td>
                                    <asp:Label ID="lblStuNM" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style3">Student ID&nbsp; :</td>
                                <td>
                                    <asp:Label ID="lblStuID" runat="server" style="font-weight: 700" ></asp:Label>
                                </td>
                                <td style="width: 3%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2" style="width: 3%">&nbsp;</td>
                                <td class="auto-style3">Program Name&nbsp; :</td>
                                <td>
                                    <asp:Label ID="lblProgNM" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style3">Semester ID :</td>
                                <td>
                                    <asp:Label ID="lblSemID" runat="server" ></asp:Label>
                                </td>
                                <td style="width: 3%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2" style="width: 3%">&nbsp;</td>
                                <td style="text-align: right">Semester Name&nbsp; :</td>
                                <td>
                                    <asp:Label ID="lblSemNM" runat="server"></asp:Label>
                                    -<asp:Label ID="lblYR" runat="server"  ></asp:Label>
                                </td>
                                <td class="auto-style3">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td style="width: 3%">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding:10px">
                        <asp:GridView ID="gv_Result" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gv_Result_RowDataBound" Style="margin-bottom: 0px">
                            <Columns>
                                <asp:BoundField HeaderText="SL.">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                    <ItemStyle HorizontalAlign="Left" Width="3%" Font-Bold="True" Font-Size="12pt" />
                                </asp:BoundField>
                                <%--<asp:BoundField  HeaderText="Student Name" >
                                 <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                             </asp:BoundField>--%>
                                <asp:BoundField HeaderText="Course Name">
                                    <HeaderStyle HorizontalAlign="Center" Width="18%" />
                                    <ItemStyle HorizontalAlign="Left" Width="18%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="CGPA">
                                    <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="L. Grade">
                                    <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                    <ItemStyle HorizontalAlign="Center" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remarks">
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
    </form>
</body>
</html>
