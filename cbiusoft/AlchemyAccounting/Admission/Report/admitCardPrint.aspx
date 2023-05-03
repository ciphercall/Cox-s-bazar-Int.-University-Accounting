<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admitCardPrint.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.admitCardPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style11 {
            width: 100%;
        }

        .auto-style12 {
            text-align: center;
            margin-top:5px;
        }

        .auto-style13 {
            text-align: center;
            font-size: xx-large;
        }

        .auto-style14 {
            width: 50px;
            height: 50px;
        }

        .auto-style15 {
            font-size: 16pt;
        }

        .auto-style16 {
            text-align: right;
        }

        .auto-style17 {
            text-align: right;
            width: 15%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">  
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="RepeatR_ItemDataBound" >
            <ItemTemplate>
                <br />
                <div style="height:500px;font-size:11pt">
                <table class="auto-style11">
                    <tr>
                        <td style="width: 15%; ">Semester Final<br />
                            Exam of:&nbsp;<strong><%=Session["SEMNM"].ToString()%></strong></td>
                        <td class="auto-style12" style="width: 40%">
                            <div style="display: inline-block">
                                <img class="auto-style14" src="../../Images/CoxLOGO.png" /></div>
                            <div style="display: inline-block; vertical-align: top">
                                <strong><span class="auto-style15">Cox&#39;s Bazar International University</span>
                                    <br />
                                    Kolatoli Circle , Coz&#39;s Bazar-4700</strong>
                            </div>

                        </td>
                        <td style="width: 10%" rowspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Image ID="Image1" runat="server" Height="110px" Width="90px" ImageUrl='<%#Eval("IMAGE") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">&nbsp;</td>
                        <td style="width: 40%">
                            <div style="border: 1px solid #000000; width: 250px; margin: 0 auto; border-radius: 5px; margin-top: 8px" class="auto-style13"><strong>Admit Card</strong></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">&nbsp;</td>
                        <td style="width: 40%">&nbsp;</td>
                    </tr>
                </table>
                <table class="auto-style11">
                    <tr>
                        <td class="auto-style17">Name <strong>:</strong></td>
                        <td style="width: 30%">&nbsp;<%#Eval("STUDENTNM") %></td>
                        <td class="auto-style16" style="width: 10%">ID<strong> :</strong></td>
                        <td style="width: 15%">&nbsp; <strong><%#Eval("NEWSTUDENTID") %></strong> 
                            <asp:Label ID="lblStuID" runat="server" Visible="false" Text='<%#Eval("STUDENTID") %>'></asp:Label>
                        </td>
                        <td class="auto-style16" style="width: 15%">Batch<strong> :</strong></td>
                        <td style="width: 15%">&nbsp;<%#Eval("BATCH") %></td>
                    </tr>
                    <tr>
                        <td class="auto-style17">Program <strong>:</strong></td>
                        <td style="width: 30%">&nbsp;<%#Eval("PROGRAMNM") %></td>
                        <td class="auto-style16" style="width: 10%">Session <strong>:</strong></td>
                        <td style="width: 15%">&nbsp;<%#Eval("SESSION") %></td>
                        <td class="auto-style16" style="width: 15%">&nbsp;</td>
                        <td style="width: 15%">&nbsp;</td>
                    </tr>
                </table>
            
        &nbsp;<table class="auto-style11">
            <tr>
                <td colspan="3">
                    <asp:GridView ID="gv_Course" runat="server" Width="95%" AutoGenerateColumns="False"  Style="margin: 0px auto">
                        <Columns>
                            <asp:BoundField HeaderText="SL."  >
                                <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                <ItemStyle HorizontalAlign="Center" Width="4%" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Course Code" DataField="COURSECD">
                                <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                <ItemStyle HorizontalAlign="Center" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Course Name" DataField="COURSENM">
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Remarks" DataField="REMARKS">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#CCCCCC" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">&nbsp;</td>
                <td style="width: 30%">&nbsp;</td>
                <td class="auto-style12" style="width: 30%">
                    <br /><br />
                    ___________________<br />
                    Controller of Examination</td>
            </tr>
        </table> 
                    </div>
                </ItemTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
