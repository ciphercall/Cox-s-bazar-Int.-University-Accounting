﻿<%@ Page Title="Income Statement" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RptIncomeST.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.UI.RptIncomeST" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link href="../../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
<script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
<script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>

<script type = "text/javascript">
    $(document).ready(function () {
        $("#txtFrom,#txtTo").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
    });
</script>

<style type="text/css">
        #header
        {
            float: left;
            width:100%;
            background-color: transparent;
            height: 50px;
        }
        .style1
        {
            width: 2px;
        }
        .style2
        {
            width: 204px;
        }
        .style3
        {
            width: 170px;
            text-align: right;
            font-weight: 700;
        }

        #s_top
        {
            float:left;
            width:60%;
            margin: 2% 20% 0% 20%;
            border: 1px solid #cccccc;
            border-radius: 10px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center" style="font-weight:bold;">INCOME STATEMENT</h1>
    </div>
    <div>
        <table id="s_top">
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    From</td>
                <td class="style1">
                    <strong>:</strong></td>
                <td>
                    <asp:TextBox ID="txtFrom" runat="server" AutoPostBack="True" 
                        ClientIDMode="Static" ontextchanged="txtFrom_TextChanged" TabIndex="2"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    <strong>To</strong></td>
                <td class="style1">
                    <strong>:</strong></td>
                <td class="style2">
                    <asp:TextBox ID="txtTo" runat="server" AutoPostBack="True" 
                        ClientIDMode="Static" ontextchanged="txtTo_TextChanged" TabIndex="3"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td style="text-align: right">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Bold="True" 
                        Font-Italic="True" Width="150px" onclick="btnSearch_Click" TabIndex="4" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    
    </div>
</asp:Content>
