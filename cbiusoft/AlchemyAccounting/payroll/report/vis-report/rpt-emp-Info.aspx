<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpt-emp-Info.aspx.cs" Inherits="AlchemyAccounting.payroll.report.vis_report.rpt_emp_Info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="shortcut icon" href="../../../Images/favicon.ico" />
    <link href="../../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>
    <style media="print">
        .ShowHeader thead
        {
            display: table-header-group;
            border: 1px solid #000;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="width: 10%" rowspan="4">
            </td>
            <td style="width: 80%; text-align: center; font-family: Calibri; font-size: 25px">
                <asp:Label runat="server" ID="lblCompanyNM"></asp:Label>
                &nbsp;
            </td>
            <td style="width: 10%">
                <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                    style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit" />
            </td>
        </tr>
        <tr>
            <td style="width: 80%; text-align: center; font-family: Calibri; font-size: 25px">
                        <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 9px"></asp:Label>
            </td>
            <td style="width: 10%">
                &nbsp;</td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td style="width: 2%; text-align: left">
            </td>
            <td style="width: 48%; text-align: left; font-size: 19PX; font-family: Calibri">
                Employee Information</td>
            <td style="width: 40%; text-align: right; font-size: 14px; font-family: Calibri">
                Print date :
                <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri"></asp:Label>
                &nbsp;
            </td>
        </tr>
    </table>

    <div style="width: 96%; margin: 1% 2% 0% 2%;">
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" Font-Names="Calibri"
            OnRowDataBound="gvReport_RowDataBound" ShowFooter="false" Width="100%" OnRowCreated="gvReport_RowCreated">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="EMPID">
                    <HeaderStyle HorizontalAlign="Center" Width="4%" />
                    <ItemStyle HorizontalAlign="Left" Width="4%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Name" DataField="EMPNM">
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Qatar ID" DataField="QATARID">
                    <HeaderStyle HorizontalAlign="Center" Width="4%" />
                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="EXP. Date" DataField="IDEXPD">
                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PP No" DataField="PPNO">
                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="EXP. Date" DataField="PPEXPD">
                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="File No" DataField="FILENO">
                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Contact No" DataField="CONTACTNO">
                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Occupation" DataField="OCCUPATION">
                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Address" DataField="ADDRESS">
                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                </asp:BoundField>
            </Columns>
            <FooterStyle Font-Size="14px" />
            <HeaderStyle Font-Size="14px" />
            <RowStyle Font-Size="12px" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
