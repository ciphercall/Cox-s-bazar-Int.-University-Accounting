<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarkingReport.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.MarkingReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .gStyle tr:hover{
            background:#bbd777;
        }
        .auto-style1 {
            width: 100%;
            font-size: xx-small;
            border: 1px solid #000000;
        }

            .auto-style1 td {
                border: 1px solid #000000;
            }
        /*.table, th, td {
            border: 1px solid black;
            
        }*/

        .auto-style2 {
            font-size: small;
            text-align: center;
        }

        .auto-style3 {
            text-align: center;
            background-color: #FFFFFF;
        }

        .auto-style4 {
            font-size: xx-small;
            text-align: center;
            background-color: #FFFFFF;
            color: #000000;
        }

        .auto-style7 {
            font-size: small;
            text-align: center;
            background-color: #FFFFFF;
        }

        .auto-style8 {
            font-size: xx-small;
            text-align: center;
            background-color: #FFFFFF;
            color: #FFFFFF;
        }

        .auto-style9 {
            font-size: small;
        }

        .auto-style10 {
            width: 136px;
            height: 144px;
        }
    </style>
</head>
<body style="border: 1px solid">
    <form id="form1" runat="server">
        <div>
            <div>
                <div style="margin: 0 auto">

                    <div style="width: 100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="font-size: xx-large; width: 100%; text-align: center;" class="auto-style3">
                                    <img class="auto-style10" src="../../Images/logoColor.png" style="width: 50px; height: 50px; display: inline-block; margin-left: -0px" /><div style="display: inline-block">
                                        Cox&#39;s Bazar International University<br />
                                        <span class="auto-style9" style="margin-left: -50px">Kolatoli Circle, Cox&#39;s&nbsp; Bazar-4700</span>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="border-top-left-radius: 10px; border-top-right-radius: 10px; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px; width: 100%; margin: 0 auto; text-align: center; color: black; font-weight: 700; font-size: large;" class="auto-style8">
                        &nbsp;<asp:Label ID="lblProgNM" runat="server"></asp:Label>
                        <br />
                        Semester Final Result
                        -
                            <asp:Label ID="lblSemester" runat="server"></asp:Label>
                        <br />
                        Batch :
                            <asp:Label ID="lblBatch" runat="server"></asp:Label>
                        &nbsp; |&nbsp; Semester :
                            <asp:Label ID="lblSemesterNM" runat="server"></asp:Label>
                    </div>
                    <hr />

                </div>
            </div>
            <table class="auto-style1" id=" " style="border: 0.8px solid #000000; background-color: #CCCCCC;" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="auto-style2" style="width: 10%; background-color: #FFFFFF;" rowspan="2">NAME</td>
                    <td class="auto-style7" style="width: 5%" rowspan="2">ID</td>
                    <td class="auto-style3" style="width: 10%" colspan="5">
                        <asp:Label ID="lblSub1" runat="server" Font-Size="10pt"></asp:Label>

                    </td>
                    <td class="auto-style3" style="width: 10%;" colspan="5">
                        <asp:Label ID="lblSub2" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                    <td class="auto-style3" style="width: 10%" colspan="5">
                        <asp:Label ID="lblSub3" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                    <td class="auto-style3" style="width: 10%;" colspan="5">
                        <asp:Label ID="lblSub4" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                    <td class="auto-style3" style="width: 10%" colspan="5">
                        <asp:Label ID="lblSub5" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                    <td class="auto-style3" style="width: 10%;" colspan="5">
                        <asp:Label ID="lblSub6" runat="server" Font-Size="10pt"></asp:Label>
                    </td>
                    <td class="auto-style3" style="width: 2%;" rowspan="2">CGPA</td>
                </tr>
                <tr>
                    <td class="auto-style4" style="width: 2%">40</td>
                    <td class="auto-style4" style="width: 2%">60</td>
                    <td class="auto-style4" style="width: 2%">Total</td>
                    <td class="auto-style4" style="width: 2%">GPA</td>
                    <td class="auto-style4" style="width: 2%">GL</td>
                    <td class="auto-style4" style="width: 2%">40</td>
                    <td class="auto-style4" style="width: 2%">60</td>
                    <td class="auto-style4" style="width: 2%">Total</td>
                    <td class="auto-style4" style="width: 2%">GPA</td>
                    <td class="auto-style4" style="width: 2%">GL</td>
                    <td class="auto-style4" style="width: 2%">40</td>
                    <td class="auto-style4" style="width: 2%">60</td>
                    <td class="auto-style4" style="width: 2%">Total</td>
                    <td class="auto-style4" style="width: 2%">GPA</td>
                    <td class="auto-style4" style="width: 2%">GL</td>
                    <td class="auto-style4" style="width: 2%">40</td>
                    <td class="auto-style4" style="width: 2%">60</td>
                    <td class="auto-style4" style="width: 2%">Total</td>
                    <td class="auto-style4" style="width: 2%">GPA</td>
                    <td class="auto-style4" style="width: 2%">GL</td>
                    <td class="auto-style4" style="width: 2%">40</td>
                    <td class="auto-style4" style="width: 2%">60</td>
                    <td class="auto-style4" style="width: 2%">Total</td>
                    <td class="auto-style4" style="width: 2%">GPA</td>
                    <td class="auto-style4" style="width: 2%">GL</td>
                    <td class="auto-style4" style="width: 2%">40</td>
                    <td class="auto-style4" style="width: 2%">60</td>
                    <td class="auto-style4" style="width: 2%">Total</td>
                    <td class="auto-style4" style="width: 2%">GPA</td>
                    <td class="auto-style4" style="width: 2%">GL</td>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="gv_CrsReg" CssClass="gStyle" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="8pt" OnRowDataBound="gv_CrsReg_RowDataBound" ShowHeader="False" OnSelectedIndexChanged="gv_CrsReg_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField>
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                    <asp:BoundField>
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
