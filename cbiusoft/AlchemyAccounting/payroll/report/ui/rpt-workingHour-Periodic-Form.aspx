﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rpt-workingHour-Periodic-Form.aspx.cs" Inherits="AlchemyAccounting.payroll.report.ui.rpt_workingHour_Periodic_Form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <link rel="shortcut icon" href="../../Images/favicon.ico" />
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }

            return true;
        }

        function confMSG() {
            if (confirm("Are you Sure to Delete?")) {
                //                alert("Clicked Yes");
            }
            else {
                //                alert("Clicked No");
                return false;
            }

        }
    </script>
    <style type="text/css">
        #header
        {
            float: left;
            width: 100%;
            background-color: transparent;
            height: 50px;
        }
        #header h1
        {
            font-family: Century Gothic;
            font-weight: bold;
        }
        #entry
        {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            margin-top: 10px;
            margin-bottom: 30px;
            border-radius: 10px;
            text-align: left;
        }
        .Gridview
        {
            font-family: Verdana;
            font-size: 10pt;
            font-weight: normal;
            color: black;
            margin-right: 0px;
            text-align: left;
        }
        .txtColor:focus
        {
            border: solid 4px green !important;
        }
        .txtColor
        {
            margin-left: 0px;
            text-align: left;
        }
        .def
        {
            float: left;
            width: 100%;
        }
        #toolbar
        {
            float: left;
            width: 100%;
            background-color: #cccccc;
            border-radius: 10px 10px 0px 0px;
        }
        .ui-accordion
        {
            text-align: left;
        }
        .txtalign
        {
            text-align: center;
        }
        .passport
        {
            float: left;
            width: 100%;
            height: 250px;
        }
        .sign
        {
            float: left;
            width: 100%;
            height: 150px;
            margin-top: 10%;
        }
        
         .well
        {
            background-color: white;
        }
        
        /*Calendar Control CSS*/
        .cal_Theme1 .ajax__calendar_other .ajax__calendar_day, .cal_Theme1 .ajax__calendar_other .ajax__calendar_year
        {
            color: White; /*Your background color of calender control*/
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center">
             Working Hour(Summarized) - Periodic</h1>
    </div>
    <div id="entry">
        <div id="toolbar">
            <table style="width: 100%; border: 1px solid #000;">
                <tr>
                    <td style="text-align: right; width: 50%">
                        <%--<asp:Button runat="server" ID="btnUpdate" CssClass="txtColor" Font-Bold="True" Font-Names="Calibri"
                            Style="text-align: center" Font-Size="15px" TabIndex="9" Text="Edit" Width="80px"
                            OnClick="btnUpdate_Click" Height="30px" />
                        <asp:Button runat="server" ID="btnCancel" CssClass="txtColor" Font-Bold="True" Font-Names="Calibri"
                            Style="text-align: center" Font-Size="15px" Text="Cancel" Width="80px" OnClick="btnCancel_Click"
                            Visible="false" Height="30px" />--%>
                    </td>
                    <td style="width: 50%">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
            </table>
        </div>

    </div>
    <div class="def">
        <table style="width: 100%">
            <tr>
                <td style="width: 12%">
                </td>
                <td style="width: 15%; text-align: right">
                    From
                </td>
                <td style="width: 2%; text-align: center">
                    :
                </td>
                <td style="width: 15%">
                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="txtColor" Width="90%" AutoPostBack="True"
                        OnTextChanged="txtFromDate_TextChanged" TabIndex="1"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1 well"
                        Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                    </asp:CalendarExtender>
                </td>
                <td style="width: 15%; text-align: right">
                    TO
                </td>
                <td style="width: 2%; text-align: center">
                    :
                </td>
                <td style="width: 16%">
                    <asp:TextBox runat="server" ID="txtToDate" CssClass="txtColor" Width="90%" AutoPostBack="True"
                        OnTextChanged="txtToDate_TextChanged" TabIndex="2"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1 well"
                        Format="dd/MM/yyyy" TargetControlID="txtToDate">
                    </asp:CalendarExtender>
                </td>
                <td style="width: 12%">
                </td>
            </tr>
            <tr>
                <td style="width: 12%">
                    &nbsp;
                </td>
                <td style="width: 15%; text-align: right">
                    &nbsp;</td>
                <td style="width: 2%; text-align: center">
                    &nbsp;</td>
                <td style="width: 15%">
                    &nbsp;
                </td>
                <td style="width: 15%; text-align: right">
                    &nbsp;
                </td>
                <td style="width: 2%; text-align: center">
                    &nbsp;
                </td>
                <td style="width: 16%">
                    &nbsp;
                </td>
                <td style="width: 12%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 12%">
                    &nbsp;
                </td>
                <td style="width: 15%; text-align: right">
                    &nbsp;
                </td>
                <td style="width: 2%; text-align: center">
                    &nbsp;
                </td>
                <td style="width: 15%">
                    <asp:Button ID="btnSubmit" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="15px"
                        Text="View Report" Width="110px" OnClick="btnSubmit_Click" TabIndex="3" />
                    &nbsp;
                </td>
                <td style="width: 15%; text-align: right">
                    <asp:Label ID="lblErrmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                    &nbsp;
                </td>
                <td style="width: 2%; text-align: center">
                    &nbsp;
                </td>
                <td style="width: 16%">
                    &nbsp;
                </td>
                <td style="width: 12%">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
