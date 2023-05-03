<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultCreate.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.ResultCreate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: Result By Course</title>
    <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />

    <style type="text/css">
        .form-control:focus {
            border: solid 4px green !important;
        }

        .form-control {
            margin-left: 0px;
        }

        .auto-style2 {
            width: 100%;
            font-size: 13px;
        }

        .auto-style8 {
            width: 30%;
            text-align: right;
            height: 36px;
        }

        .auto-style9 {
            text-align: right;
            width: 12%;
        }

        .auto-style10 {
            width: 90%;
        }

        .form-control {
        }

        .auto-style11 {
            color: #FFFFFF;
        }

        .auto-style12 {
            width: 5%;
            height: 39px;
        }

        .auto-style13 {
            text-align: right;
            width: 12%;
            height: 39px;
        }

        .auto-style14 {
            width: 30%;
            height: 39px;
        }

        .auto-style15 {
            width: 15%;
            height: 39px;
        }

        .auto-style16 {
            width: 30%;
            text-align: right;
            height: 39px;
        }

        .auto-style17 {
            width: 5%;
            height: 33px;
        }

        .auto-style18 {
            text-align: right;
            width: 12%;
            height: 33px;
        }

        .auto-style19 {
            width: 30%;
            height: 33px;
        }

        .auto-style20 {
            width: 15%;
            height: 33px;
        }

        .auto-style21 {
            width: 5%;
            height: 36px;
        }

        .auto-style22 {
            text-align: right;
            width: 12%;
            height: 36px;
        }

        .auto-style23 {
            width: 30%;
            height: 36px;
        }

        .auto-style24 {
            width: 15%;
            height: 36px;
        }
        .auto-style25 {
            width: 15%;
            height: 33px;
            text-align: right;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="Script1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Update1" runat="server">
            <ContentTemplate>
                <div style="width: 960px; margin: 0 auto; border: double; border-color: black; border-width: 2px">
                    <div>

                        <table class="auto-style2">
                            <caption>
                                <br />
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="auto-style11" style="font-weight: 700; font-size: xx-large; text-align: center; background-color: #2aabd2;">Results By Course</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <br />
                                        <table class="auto-style2">
                                            <tr>
                                                <td class="auto-style12"></td>
                                                <td class="auto-style13">Registration Year :</td>
                                                <td class="auto-style14">
                                                    <asp:DropDownList ID="ddlYR" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" OnSelectedIndexChanged="ddlYR_SelectedIndexChanged" Width="120px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right;" class="auto-style15">Semester Name&nbsp; :</td>
                                                <td class="auto-style16">
                                                    <asp:DropDownList ID="ddlSemNM" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" OnSelectedIndexChanged="ddlSemNM_SelectedIndexChanged" Width="100%" TabIndex="1">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="auto-style15"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style21"></td>
                                                <td class="auto-style22">Semester ID :</td>
                                                <td class="auto-style23">
                                                    <asp:DropDownList ID="ddlSemesterID" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" Width="100%" OnSelectedIndexChanged="ddlSemesterID_SelectedIndexChanged" TabIndex="2">
                                                        
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right;" class="auto-style24">Program Name&nbsp; :</td>
                                                <td class="auto-style8">
                                                    <asp:DropDownList ID="ddlProgNM" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" OnSelectedIndexChanged="ddlProgNM_SelectedIndexChanged" Width="100%" TabIndex="3">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="auto-style24"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style17"></td>
                                                <td class="auto-style18">Course Name&nbsp; :</td>
                                                <td class="auto-style19">
                                                    <asp:DropDownList ID="ddlCourseNM" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" OnSelectedIndexChanged="ddlCourseNM_SelectedIndexChanged" Width="100%" TabIndex="4">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="auto-style25">Batch&nbsp; :</td>
                                                <td class="auto-style19" style="text-align: right;">
                                                    <asp:TextBox ID="txtBatch" CssClass="form-control"  runat="server" Width="124px"></asp:TextBox>
                                                    <asp:Label ID="lblCrsID" runat="server" Visible="False"></asp:Label>
                                                </td>
                                                <td class="auto-style20"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 5%">&nbsp;</td>
                                                <td class="auto-style9">&nbsp;</td>
                                                <td style="width: 30%">
                                                    <asp:Label ID="lblProgID" runat="server" Visible="False"></asp:Label>
                                                </td>
                                                <td style="width: 15%">&nbsp;</td>
                                                <td class="auto-style10" style="width: 30%; text-align: right;">
                                                    <asp:Button ID="btnSearch" runat="server" BorderColor="#0066FF" BorderWidth="2px" CssClass="form-control-right" Font-Bold="True" Height="30px" OnClick="btnSearch_Click" Text="Search" Width="134px" TabIndex="5" />
                                                    <asp:Label ID="lblSemID" runat="server" Visible="False"></asp:Label>
                                                    <asp:Button ID="btnPrint" runat="server" BorderColor="#0066FF" BorderWidth="2px" CssClass="form-control-right" Font-Bold="True" Height="30px" OnClick="btnPrint_Click" Text="Print" Width="134px" TabIndex="5" />
                                                </td>
                                                <td style="width: 15%">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 5%">&nbsp;</td>
                                                <td colspan="4"></td>
                                                <td style="width: 15%">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </caption>
                        </table>
                    </div>

                    <div>
                        <table class="auto-style2">
                            <tr>
                                <td style="padding: 10px">
                                    <asp:GridView ID="gv_Result" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gv_Result_RowDataBound" Style="margin-bottom: 0px">
                                        <Columns>
                                            <asp:BoundField HeaderText="SL.">
                                                <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                                <ItemStyle HorizontalAlign="Left" Width="4%" Font-Bold="True" Font-Size="12pt" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Student ID">
                                                <HeaderStyle HorizontalAlign="Center" Width="13%" />
                                                <ItemStyle HorizontalAlign="Center" Width="13%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Student Name">
                                                <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                                <ItemStyle HorizontalAlign="Left" Width="25%" />
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
