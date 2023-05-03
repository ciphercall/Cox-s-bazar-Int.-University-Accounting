<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-quotation.aspx.cs"
    Inherits="AlchemyAccounting.payroll.report.vis_report.rpt_quotation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />
    <link href="../../../report.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            window.print();
        }
    </script>
    <style type="text/css" media="print">
        .ShowHeader thead
        {
            display: table-header-group;
            border: 1px solid #000;
        }
    </style>
    <style type="text/css">
        .style1
        {
            width: 20%;
            height: 18px;
        }
        .style2
        {
            text-decoration: underline;
        }
        .rost
        {
            list-style-type: circle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page">
        <div style="float: left; width: 100%; border-bottom: 1px solid #000">
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%" rowspan="2">
                        &nbsp; &nbsp;
                        <div style="width: 140px; height: 80px; margin-top: -40px">
                            &nbsp;</div>
                    </td>
                    <td style="width: 80%; text-align: center">
                        &nbsp;
                        <asp:Label runat="server" ID="lblCompanyNM" Style="font-size: 20px; font-weight: 700"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                            style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit;
                            text-align: right" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; font-size: 12px" colspan="2">
                        <strong><span class="style1">Print Date :</span></strong>
                        <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri" Font-Size="12px"
                            CssClass="style1"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 100%; margin-top: 5px; font-family: Calibri; font-size: 14px">
            <table style="width: 100%">
                <tr>
                    <td style="width: 100%; text-align: center; font-weight: bold">
                        <div style="border-bottom: 1px solid #000; font-size: 20px; width: 15%; margin: 0 45% 0 45%">
                            Quotation
                        </div>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 80%; text-align: left; font-weight: bold">
                    </td>
                    <td style="width: 20%; text-align: left; font-weight: bold">
                        Date :
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: left; font-weight: bold">
                        To,
                    </td>
                    <td style="width: 20%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: left; font-weight: bold">
                        <asp:Label ID="lblComNm" runat="server"></asp:Label>
                    </td>
                    <td style="width: 20%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: left; font-weight: bold">
                        <asp:Label ID="lblCompAdd" runat="server"></asp:Label>
                    </td>
                    <td style="width: 20%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: left; font-weight: bold">
                        <asp:Label ID="lblCompCont" runat="server"></asp:Label>
                    </td>
                    <td style="width: 20%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 20%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: left; font-weight: bold">
                        Quotation No :
                        <asp:Label ID="lblQuotNo" runat="server"></asp:Label>
                    </td>
                    <td style="width: 20%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                    <td style="width: 20%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: left; font-weight: bold">
                        Atten :
                        <asp:Label ID="lblAttNm" runat="server"></asp:Label>
                    </td>
                    <td style="width: 20%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%; text-align: left; font-weight: bold">
                        <asp:Label ID="lblAttDesig" runat="server"></asp:Label>
                    </td>
                    <td style="width: 20%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        Dear Sir,
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        <span>We take this opportunity to introduce ourselves as manpower supply and Contracting Company in the Name of
                        <asp:Label runat="server" ID="lblPrepCompanyNm" 
                           ></asp:Label>
                    &nbsp;Company situated
                            in Doha Qatar. We are pleased to quote our lowest price for the following Category.</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        <span class="style2">Subject : </span>
                        <asp:Label ID="lblSubj" runat="server" CssClass="style2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowDataBound="GridView1_RowDataBound" ShowFooter="True" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:BoundField HeaderText="SL">
                                    <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Description">
                                    <HeaderStyle HorizontalAlign="Center" Width="47%" />
                                    <ItemStyle HorizontalAlign="Left" Width="47%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Unit">
                                    <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Rate">
                                    <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                    <ItemStyle HorizontalAlign="Right" Width="12%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Qty">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Total(Qrs)">
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                    <ItemStyle HorizontalAlign="Right" Width="15%" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold" class="style2">
                        Terms &amp; Conditions :
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left">
                        <div style="margin-left: 10%">
                            <%--<asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
                                GridLines="None" ShowHeader="False">
                                <Columns>
                                    <asp:BoundField DataField="QTDESC"></asp:BoundField>
                                </Columns>
                            </asp:GridView>--%>
                            <asp:BulletedList ID="bltListOne" runat="server" BulletStyle="Circle" DataTextField="QTDESC"
                                DataValueField="">
                            </asp:BulletedList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left">
                        <span>We would be grateful if you give us an opportunity to serve at your projects with
                            our best manpower in case any clarification / information required please feel free
                            to contact our office anytime.</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        Best Regards,
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        <asp:Label ID="lblPrepNm" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        <asp:Label ID="lblPrepDesig" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        <asp:Label ID="lblPrepCont" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        <asp:Label runat="server" ID="lblPrepCompany" 
                           ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        Email : <span class="style2">pancacitraqatar@yahoo.com</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: left; font-weight: bold">
                        &nbsp;
                    </td>
                </tr>
            </table>
            &nbsp;&nbsp;&nbsp;
        </div>
    </div>
    </form>
</body>
</html>
