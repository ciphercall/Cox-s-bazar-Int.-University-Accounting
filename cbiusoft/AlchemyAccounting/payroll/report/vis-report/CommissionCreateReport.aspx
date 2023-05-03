<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommissionCreateReport.aspx.cs"
    Inherits="AlchemyAccounting.payroll.report.vis_report.CommissionCreateReport" %>

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
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            background-color: #FAFAFA;
            font: 14px "Calibri";
        }
        *
        {
            box-sizing: border-box;
            -moz-box-sizing: border-box;
        }
        .page
        {
            width: 29.7cm;
            min-height: 21cm;
            padding: .5cm;
            margin: 1cm auto;
            border: 1px #D3D3D3 solid;
            border-radius: 5px;
            background: white;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        }
        .subpage
        {
            padding: 1cm;
            border: 5px red solid;
            height: 237mm;
            outline: 2cm #FFEAEA solid;
        }
        
        @page
        {
            size: A4;
            margin: 0;
        }
        @media print
        {
            .page
            {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: avoid; /* here always for subpage */
            }
        }
        

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page">
        <table style="width: 100%">
            <tr>
                <td style="width: 10%">
                    <div style="width: 120px; height: 60px;">
                        <%--<img alt="logo" height="100%;" src="../../../Images/logo.png" width="100%" />--%>
                    </div>
                </td>
                <td style="width: 80%; text-align: center">
                    &nbsp;
                    <asp:Label runat="server" ID="lblCompanyNM" Font-Size="18px"></asp:Label>
                    <asp:Label ID="lblAddress" runat="server" Style="font-family: Calibri; font-size: 9px"
                        Visible="False"></asp:Label>
                </td>
                <td style="width: 10%">
                    <input id="print" tabindex="1" type="button" value="Print" onclick="ClosePrint()"
                        style="font-family: Calibri; font-size: 15px; font-weight: bold; font-style: inherit;
                        text-align: right" />
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">
                    &nbsp;
                </td>
                <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                    &nbsp;
                </td>
                <td style="font-family: Calibri; font-size: 15px; text-align: left" colspan="2">
                    &nbsp;
                </td>
                <td style="width: 39%; font-family: Calibri; font-size: 12px; text-align: right">
                    <strong>Print Date :</strong>
                    <asp:Label ID="lblPrintDate" runat="server" Font-Names="Calibri" Font-Size="12px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">
                    &nbsp;
                </td>
                <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                    &nbsp;
                </td>
                <td style="width: 25%; font-family: Calibri; font-size: 15px; text-align: left">
                    &nbsp;
                </td>
                <td style="width: 20%; font-family: Calibri; font-size: 15px; text-align: left">
                    &nbsp;
                </td>
                <td style="width: 39%; font-family: Calibri; font-size: 15px; text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">
                    Company Name
                </td>
                <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                    :
                </td>
                <td style="font-family: Calibri; font-size: 15px; text-align: left" colspan="3">
                    <asp:Label runat="server" ID="lblCompanyNM0" Visible="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">
                    Work Site
                </td>
                <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                    :
                </td>
                <td style="font-family: Calibri; font-size: 15px; text-align: left" colspan="3">
                    <asp:Label ID="lblWorkSite" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">
                    Particulars&nbsp;
                </td>
                <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                    :
                </td>
                <td style="font-family: Calibri; font-size: 15px; text-align: left" colspan="3">
                    &nbsp;<asp:Label ID="lblParticulars" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; font-family: Calibri; font-size: 15px; text-align: left">
                    Group
                </td>
                <td style="width: 1%; font-family: Calibri; font-size: 15px; font-weight: bold; text-align: center">
                    :
                </td>
                <td style="font-family: Calibri; font-size: 15px; text-align: left" colspan="3">
                    &nbsp;<asp:Label ID="lblGroup" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <table style="width: 100%; border: 1px solid black;">
            <tr>
                <td style="width: 5%; font-family: Calibri; font-size: 15px; text-align: center ; border: 1px solid black;">
                    SL No
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black; ">
                    Issue Date
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black;">
                    Total Invoice Amount
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black;">
                    Company Percent
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black;">
                    Commission Amount
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black;">
                    Car Rent
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black;">
                    Advance
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black;">
                    Amount
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black;">
                    Company Advance
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black;">
                    Grand Total
                </td>
            </tr>
            <tr>
                <td style="width: 5%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black;">
                    <asp:Label ID="lblSL" runat="server"></asp:Label>
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black;">
                    <asp:Label ID="lblIssueDate" runat="server"></asp:Label>
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: right; border: 1px solid black;">
                    <asp:Label ID="lblinvoice" runat="server"></asp:Label>
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: center; border: 1px solid black;">
                    <asp:Label ID="lblpercent" runat="server"></asp:Label>
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: right; border: 1px solid black;">
                    <asp:Label ID="lblComAmt" runat="server"></asp:Label>
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: right; border: 1px solid black;">
                    <asp:Label ID="lblcarrent" runat="server"></asp:Label>
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: right; border: 1px solid black;">
                    <asp:Label ID="lblAdvance" runat="server"></asp:Label>
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: right; border: 1px solid black;">
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: right; border: 1px solid black;">
                    <asp:Label ID="lblCompanyAdvance" runat="server"></asp:Label>
                </td>
                <td style="width: 9%; font-family: Calibri; font-size: 15px; text-align: right; border: 1px solid black;">
                    <asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="width: 100%">
            <tr>
                <td style="font-family: Calibri; font-size: 15px; text-align: left">
                    Remarks &nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-family: Calibri; font-size: 15px; text-align: left">
                    In Words&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblInWords" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <table style="width: 100%">
            <tr>
                <td style="width: 10%; font-family: Calibri; font-size: 15px; text-align: left">
                    Manager
                </td>
                <td style="width: 30%; font-family: Calibri; font-size: 15px; text-align: left">
                    Chief Accountant
                </td>
                <td style="width: 10%; font-family: Calibri; font-size: 15px; text-align: left">
                    Authorized By
                </td>
                <td style="width: 10%; font-family: Calibri; font-size: 15px; text-align: left">
                    Checked By
                </td>
                <td style="width: 10%; font-family: Calibri; font-size: 15px; text-align: left">
                    Receiver&#39;s Signature
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
