<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="studentWiseResult_.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.studentWiseResult_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Money Receipt</title>

    <style type="text/css">
        
        .auto-style1 {
            width: 100%;
            font-size: 11pt;
        }

        .auto-style3 {
            font-size: medium;
            text-align: center;
        }

        .auto-style6 {
            text-align: center;
        }

        .auto-style7 {
            font-size: small;
            text-align: center;
        }

        .auto-style8 {
            width: 18%;
            text-align: right;
            padding-top: 0px;
        }

        .auto-style16 {
            font-size: 26pt;
            text-align: center;
        }

        .auto-style19 {
            font-size: large;
        }

        .auto-style20 {
            width: 20%;
            height: 31px;
        }

        .auto-style21 {
            width: 1%;
            height: 31px;
        }

        .auto-style22 {
            width: 40%;
            height: 31px;
        }

        .auto-style23 {
            width: 40%;
        }

        .auto-style24 {
            font-size: 8pt;
        }

        .tbl1 td{
            border:1px solid black
        }
         

        .auto-style25 {
            font-weight: bold;
            text-align: center;
        }

        .auto-style26 {
            width: 40%;
            font-weight: bold;
            text-align: center;
        }

        .auto-style27 {
            text-align: left;
            width: 12%;
        }

        .auto-style28 {
            width: 100%;
        }

        .auto-style29 {
            text-align: right;
        }
    </style>
</head>
<body style="width: 100%; text-align: left; margin: 0 auto; background-image: url(/Images/Logo.png); background-repeat: no-repeat; padding-top: 20px; background-position: center">
    <form id="form1" runat="server">
        <div style="margin: 0 auto; width: 960px; margin: 0 auto; margin-top: 10px; border-width: 2px; border-radius: 5px; border-color: black; border: double">

            <table class="auto-style1">
                <tr>
                    <td class="auto-style8" rowspan="2">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/CoxLOGO.png" Height="60px" Style="text-align: right" Width="57px" />
                    </td>
                    <td class="auto-style16">
                        <strong>Cox&#39;s Bazar International University</strong></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Dynamic Cox Kingdom , Kolatoli Circle , Cox&#39;s Bazar- 4700 , Bangladesh. Phone : + 88034152510</td>
                    <td>&nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style8">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style8">&nbsp;</td>
                    <td class="auto-style3">
                        <h1 style="margin-bottom: 0px" class="auto-style19">STUDENT WISE RESULT</h1>
                        <hr style="width: 70%;" />
                    </td>
                    <td style="width: 18%">&nbsp;</td>
                </tr>

            </table>

            <asp:FormView ID="FormView1" runat="server" Width="100%">
                <ItemTemplate>

                    <table class="auto-style1">
                        <tr>
                            <td style="width: 20%">Student Name </td>
                            <td style="width: 1%"><strong>:</strong></td>
                            <td style="width: 40%"><%#Eval("STUDENTNM")%></td>
                            <td rowspan="7" style="width: 30%; text-align: left">
                                <table class="auto-style1 tbl1" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="auto-style26">Numerical Gread</td>
                                        <td colspan="2" class="auto-style25">Letter Grade</td>
                                        <td style="width: 30%" class="auto-style25">Grade Point</td>
                                    </tr>
                                    <tr class="auto-style24">
                                        <td class="auto-style23">80% Above</td>
                                        <td class="auto-style27">&nbsp; A+</td>
                                        <td style="width: 17%">A Plus</td>
                                        <td style="width: 30%">&nbsp;</td>
                                    </tr>
                                    <tr class="auto-style24">
                                        <td class="auto-style23">75% to less than 80%</td>
                                        <td class="auto-style27">&nbsp; A</td>
                                        <td style="width: 17%">A Regular</td>
                                        <td style="width: 30%">&nbsp;</td>
                                    </tr>
                                    <tr class="auto-style24">
                                        <td class="auto-style23">70% to less than 75%</td>
                                        <td class="auto-style27">&nbsp; A-</td>
                                        <td style="width: 17%">A Minus</td>
                                        <td style="width: 30%">&nbsp;</td>
                                    </tr>
                                    <tr class="auto-style24">
                                        <td class="auto-style23">65% to less than 70%</td>
                                        <td class="auto-style27">&nbsp; B+</td>
                                        <td style="width: 17%">B Plus</td>
                                        <td style="width: 30%">&nbsp;</td>
                                    </tr>
                                    <tr class="auto-style24">
                                        <td class="auto-style23">60% to less than 65%</td>
                                        <td class="auto-style27">&nbsp; B-</td>
                                        <td style="width: 17%">B Minus</td>
                                        <td style="width: 30%">&nbsp;</td>
                                    </tr>
                                    <tr class="auto-style24">
                                        <td class="auto-style23">55% to less than 60%</td>
                                        <td class="auto-style27">&nbsp; B</td>
                                        <td style="width: 17%">B Regular</td>
                                        <td style="width: 30%">&nbsp;</td>
                                    </tr>
                                    <tr class="auto-style24">
                                        <td class="auto-style23">50% to less than 55%</td>
                                        <td class="auto-style27">&nbsp; C+</td>
                                        <td style="width: 17%">C Plus</td>
                                        <td style="width: 30%">&nbsp;</td>
                                    </tr>
                                    <tr class="auto-style24">
                                        <td class="auto-style23">45% to less than 50%</td>
                                        <td class="auto-style27">&nbsp; C</td>
                                        <td style="width: 17%">C Regular</td>
                                        <td style="width: 30%">&nbsp;</td>
                                    </tr>
                                    <tr class="auto-style24">
                                        <td class="auto-style23">40% to less than 45%</td>
                                        <td class="auto-style27">&nbsp; D</td>
                                        <td style="width: 17%">&nbsp;</td>
                                        <td style="width: 30%">&nbsp;</td>
                                    </tr>
                                    <tr class="auto-style24">
                                        <td class="auto-style23">Less than 40%</td>
                                        <td class="auto-style27">&nbsp; F</td>
                                        <td style="width: 17%">&nbsp;</td>
                                        <td style="width: 30%">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td rowspan="7" style="width: 2%; text-align: left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 20%">Student ID </td>
                            <td style="width: 1%"><strong>:</strong></td>
                            <td style="width: 40%"><strong><%#Eval("NEWSTUDENTID")%></strong></td>
                        </tr>
                        <tr>
                            <td style="width: 20%">Program </td>
                            <td style="width: 1%"><strong>:</strong></td>
                            <td style="width: 40%"><%#Eval("PROGRAMNM")%></td>
                        </tr>
                        <tr>
                            <td style="width: 20%">Program Commenced</td>
                            <td style="width: 1%"><strong>:</strong></td>
                            <td style="width: 40%"><%#Eval("SEM")%></td>
                        </tr>
                        <tr>
                            <td style="width: 20%">Program Ended</td>
                            <td style="width: 1%"><strong>:</strong></td>
                            <td style="width: 40%"></td>
                        </tr>
                        <tr>
                            <td style="width: 20%">&nbsp;</td>
                            <td style="width: 1%">&nbsp;</td>
                            <td style="width: 40%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style20"></td>
                            <td class="auto-style21"></td>
                            <td class="auto-style22"></td>
                        </tr>
                    </table>

                </ItemTemplate>
            </asp:FormView>
            <br />
           <%-- <table class="auto-style28" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="auto-style6" style="width: 10%;border-bottom:1px solid black;border-left:1px solid black;border-top:1px solid black">
                        Name of Semester</td>
                    <td class="auto-style6" style="width: 10%;border-bottom:1px solid black;border-left:1px solid black;border-top:1px solid black">Course Code</td>
                    <td class="auto-style6" style="width: 30%;border-bottom:1px solid black;border-left:1px solid black;border-top:1px solid black">Course Title</td>
                    <td class="auto-style6" style="width: 4%;border-bottom:1px solid black;border-left:1px solid black;border-top:1px solid black">Credit Hour</td>
                    <td class="auto-style6" style="width: 4%;border-bottom:1px solid black;border-left:1px solid black;border-top:1px solid black">Letter Grade</td>
                    <td class="auto-style6" style="width: 4%;border-bottom:1px solid black;border-left:1px solid black;border-top:1px solid black">Grade Point</td>
                    <td class="auto-style6" style="width: 4%;border-bottom:1px solid black;border-left:1px solid black;border-top:1px solid black;border-right:1px solid black;">CGPA</td>
                </tr>
            </table>--%>
            <asp:Repeater ID="Repeater1" runat="server"  OnItemDataBound="Repeater1_ItemDataBound" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSemID" Text='<%#Eval("SEMID") %>' Style="font-weight: bold" Visible="false"></asp:Label>

                    <asp:GridView ID="GridView1" runat="server"  CssClass="Grid" AutoGenerateColumns="False" Width="100%" ShowFooter="false" ShowHeader="false"  OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Name of Semester" DataField="SEMESTERNM">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Course Code" DataField="COURSECD">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Course Title" DataField="COURSENM">
                                <HeaderStyle Width="22%" />
                                <ItemStyle HorizontalAlign="Left" Width="22%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Credit Hour" DataField="CREDITHH">
                                <HeaderStyle Width="4%" />
                                <ItemStyle HorizontalAlign="Center" Width="4%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Letter Gread" DataField="LG">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Grade Point" DataField="GP">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="CGPA" DataField="CGPA">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle Font-Size="10pt" HorizontalAlign="Center" />
                    </asp:GridView>
                    <div style="width:100%;border-bottom:1PX solid black"></div>
                </ItemTemplate>
            </asp:Repeater>
            &nbsp;
            <br />
            <table class="auto-style28">
                <tr>
                    <td class="auto-style29" style="width: 39%"><strong>Obtained CGPA&nbsp; :</strong></td>
                    <td style="width: 18%"><strong>&nbsp; 4.00 (A+)&nbsp;</strong></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
