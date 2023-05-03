<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptMoneyReceipt.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.rptMoneyReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            border-spacing:0px
        }
        .newStyle1 {
            font-family: Verdana, Geneva, Tahoma, sans-serif;
        }
        .auto-style16 {
            text-align: center;
        }
        .auto-style26 {
            color: #FFFFFF;
            font-size: 10pt;
           
        }
        .auto-style34 {
            text-align: center;
        }
        .auto-style35 {
            text-align: center;
            width: 136px;
        }
        .auto-style36 {
            text-align: left;
        }
        .auto-style37 {
            text-align: right;
        }
        .auto-style38 {
            height: 25px;
        }
        .auto-style39 {
            text-align: center;
            width: 136px;
            height: 25px;
        }
        .auto-style40 {
            text-align: center;
            height: 25px;
        }
        .auto-style41 {
            text-align: right;
            height: 25px;
        }
    
a:link
{
    color: #034af3;
}

        .auto-style42 {
            color: #999999;
        }
        .auto-style46 {
            font-size: 10pt;
            background-color: #ebeaea;
            text-align: center;
        }
        .auto-style47 {
            width: 18%;
            height: 23px;
            background-color: #ebeaea;
        }
        .auto-style48 {
            text-align: center;
            font-size: 11pt;
            height: 23px;
            background-color: #ebeaea;
        }
        .auto-style49 {
            font-size: 11pt;
            height: 23px;
            background-color: #ebeaea;
        }
        .auto-style50 {
            font-size: 8pt;
        }
        .auto-style52 {
            background-color: #ebeaea;
        }
        .auto-style53 {
            font-size: 11pt;
            background-color: #ebeaea;
        }
        .auto-style54 {
            text-align: center;
            font-size: 11pt;
            background-color: #ebeaea;
        }
        .auto-style55 {
            background-color: #ebeaea;
            font-size: 24pt;
            text-align: center;
        }
    </style>
</head>
<body style="width: 100%; text-align: left; margin:0 auto;  background-position:center">
    <form id="form1" runat="server">
    <div style="border-width:2px; width:900px; margin:0 auto; border-color:black;border:solid;padding:10px">   
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style52" rowspan="3" style="width: 5%; text-align: right;">
                        <asp:Image ID="Image1" runat="server" Height="70px" ImageUrl="~/Images/CoxLogo.png" Width="70px" />
                    </td>
                    <td class="auto-style55" style="width: 15%">
                        <strong style="width: 35%; text-align: left">Cox&#39;s Bazar International University</strong></td>
                    <td class="auto-style52" style="width: 5%">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style54">==============E&nbsp; n&nbsp; l&nbsp; i&nbsp; g&nbsp; h&nbsp; t&nbsp; e&nbsp; n&nbsp; i&nbsp; n&nbsp; g&nbsp;&nbsp;&nbsp; T&nbsp; o&nbsp; m&nbsp; o&nbsp; r&nbsp; r&nbsp; o w ===========</td>
                    <td class="auto-style52" style="width: 5%">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style48">Campus &amp; Office :Dynamic Cox Kingdom, Kolatoli Circle, Cox&#39;s Bazar-4700</td>
                    <td class="auto-style47" style="width: 5%"></td>
                </tr>
                <tr>
                    <td class="auto-style52" style="width: 5%">&nbsp;</td>
                    <td class="auto-style54">Bangladesh,&nbsp; Cell :&nbsp;&nbsp; +8801762686274,&nbsp;&nbsp;&nbsp;&nbsp; +8801762686275</td>
                    <td class="auto-style52" style="width: 5%">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style52" style="width: 5%">&nbsp;</td>
                    <td class="auto-style46"><span class="auto-style50">E-mail : </span><a href="mailto:cbiu.bd@gmail.com"><span class="auto-style50">cbiu.bd@gmail.com</span></a><span class="auto-style50">,&nbsp; </span><a href="mailto:info@cbiu.ac.bd"><span class="auto-style50">info@cbiu.ac.bd</span></a><span class="auto-style50">,&nbsp;&nbsp; Website : www.cbiu.ac.bd</span></td>
                    <td class="auto-style52" style="width: 5%">&nbsp;</td>
                </tr>
            </table>
&nbsp;<table class="auto-style1">
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style34" colspan="5">
                            <asp:Label ID="lblMRNO0" runat="server" Font-Size="22pt">Money Receipt</asp:Label>
                        </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style35"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="Label18" runat="server" Text="M.R No" style="font-size: 9pt; font-weight: 700;"></asp:Label></div></td>
                    <td class="auto-style16"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;">Date</div></td>
                    <td class="auto-style16"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;">FormNo</div></td>
                    <td class="auto-style16"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;">Program ID</div></td>
                    <td class="auto-style16"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;">Semester</div></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style35"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="lblMRNO" runat="server" style="font-size: 9pt" Font-Size="11pt"></asp:Label></div></td>
                    <td class="auto-style16"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="lblMRDT" runat="server" Font-Size="11pt"></asp:Label></div></td>
                    <td class="auto-style16"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="lblFRNO" runat="server" Font-Size="11pt"></asp:Label></div></td>
                    <td class="auto-style16"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="lblProSNM" runat="server" Font-Size="11pt"></asp:Label></div></td>
                    <td class="auto-style16"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="lblSemNM" runat="server" Font-Size="11pt"></asp:Label></div></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style35"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;">Name</div></td>
                    <td class="auto-style36" colspan="4"> <div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="lblStuNM" runat="server" Font-Size="11pt"></asp:Label></div></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style35">&nbsp;</td>
                    <td class="auto-style16">&nbsp;</td>
                    <td class="auto-style16">&nbsp;</td>
                    <td class="auto-style16">&nbsp;</td>
                    <td class="auto-style16">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style35"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="Label12" runat="server" Text="SL NO" Font-Size="9pt" Font-Bold="True"></asp:Label></div></td>
                    <td class="auto-style16" colspan="3"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="Label13" runat="server" Text="Particulars" Font-Size="11pt"></asp:Label></div></td>
                    <td class="auto-style16"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="Label14" runat="server" Text="Amount" Font-Size="11pt"></asp:Label></div></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style35"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="lblSL" runat="server" Font-Size="11pt">01.</asp:Label></div></td>
                    <td class="auto-style16" colspan="3"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="lblParNM" runat="server" Font-Size="11pt">ADMISSION FORM FEE</asp:Label></div></td>
                    <td class="auto-style37"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="lblAmnt" runat="server" Font-Size="11pt"></asp:Label></div></td>
                    <td class="auto-style37">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style38"></td>
                    <td class="auto-style39"></td>
                    <td class="auto-style40" colspan="2"></td>
                    <td class="auto-style40"><div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;">Total&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</div></td>
                    <td class="auto-style41">
                       <div style="border-style: solid; border-color:#edecec ; border-width: 1px; width:100%;"><asp:Label ID="lblTamnt" runat="server" Font-Size="11pt"></asp:Label></div>
                    </td>
                    <td class="auto-style41"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style34">In Word&nbsp; :</td>
                    <td colspan="4"><asp:Label ID="lblInWrdAmnt" runat="server" Font-Size="11pt"></asp:Label></td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table class="auto-style1">
                <tr>
                    <td style="width: 10%">&nbsp;</td>
                    <td style="width: 15%; text-align: center;">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%; text-align: center;">&nbsp;</td>
                    <td style="width: 10%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 10%">&nbsp;</td>
                    <td style="width: 15%; text-align: center;">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%; text-align: center;">&nbsp;</td>
                    <td style="width: 10%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 10%">&nbsp;</td>
                    <td style="width: 15%; text-align: center;">Received By</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%; text-align: center;">Verified By</td>
                    <td style="width: 10%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 10%">&nbsp;</td>
                    <td style="width: 15%">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%">&nbsp;</td>
                    <td style="width: 2%">&nbsp;</td>
                    <td style="width: 15%">&nbsp;</td>
                    <td style="width: 10%">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style26" colspan="9" style="text-align: center; background-color: #999999">Chittagong Information Office : RF Classic Point (1st Floor), 8 Katalgonj Chawkbazar, Chittagong , Bangladesh. Tel :+88031-2856948, Cell : 01762686274-5</td>
                </tr>
                <tr>
                    <td class="auto-style26" colspan="9" style="text-align: center; background-color: #FFFFFF"><span class="auto-style42"><span class="auto-style50">Developed By :</span></span><span class="auto-style50"> </span> <a href="http://www.alchemy-bd.com/" target="_blank"><span class="auto-style50">Alchemy Software</span></a> </td>
                </tr>
            </table>
        </div>
        </div>
 
    </form>
 
</body>
</html>
