﻿<%@ Page Title="Trial Balance" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RptTrialBalance.aspx.cs" Inherits="AlchemyAccounting.Accounts.Report.UI.RptTrialBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="../../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
<link href="../../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
<script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
<script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>

<script type ="text/javascript">
    $(document).ready(function () {
        $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+0" });
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
        #s_top
        {
            float:left;
            width:60%;
            margin: 2% 20% 0% 20%;
            border: 1px solid #cccccc;
            border-radius: 10px;
        }
        .style1
        {
            width: 147px;
            text-align: right;
        }
        .style2
        {
            width: 2px;
            font-weight: bold;
        }
        .style3
        {
            width: 242px;
            text-align: left;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center" style="font-weight:bold;font-family:Century Gothic;">Trial 
            balance</h1>
    </div>

    <div>
        
        <table id="s_top">
            <tr>
                <td class="style1" style="text-align: right; font-weight: 700">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>Date</strong></td>
                <td class="style2">
                    :</td>
                <td class="style3">
                    <asp:TextBox ID="txtDate" runat="server" AutoPostBack="True" 
                        ClientIDMode="Static" ontextchanged="txtDate_TextChanged"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td style="text-align: right">
                    <asp:Button ID="btnSearch" runat="server" Font-Bold="True" Text="Search" 
                        Width="130px" onclick="btnSearch_Click" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td style="text-align: right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        
    </div>
</asp:Content>
