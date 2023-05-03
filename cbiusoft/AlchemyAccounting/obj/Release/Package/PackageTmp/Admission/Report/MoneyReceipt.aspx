<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneyReceipt.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.MoneyReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Money Receipt</title>
   
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            color: #000000;
            font-size: small;
        }
        .auto-style3 {
            font-size: larger;
            text-align: center;
        }
        .auto-style4 {
            font-size: small;
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
            text-align:right;
            padding-top:0px;
        }
        .auto-style9 {
            text-decoration: none;
        }
        .auto-style10 {
            height: 23px;
            width:10%
        }
        .auto-style11 {
            width: 168px;
        }
        .auto-style13 {
            text-align: left;
        }
        .auto-style14 {
            height: 23px;
        }
        .auto-style15 {
            height: 23px;
            text-align: center;
        }
        .auto-style16 {
            font-size: 26pt;
            text-align: center;
        }
        .auto-style17 {
            height: 23px;
            width: 10%;
            text-align: right;
        }
        .auto-style18 {
            text-align: right;
        }
    </style>
</head>
<body style="width: 100%; text-align: left; margin:0 auto; background-image:url(/Images/Logo.png); background-repeat:no-repeat;padding-top:20px; background-position:center">
    <form id="form1" runat="server">
    <div  style="margin:0 auto; width:960px; margin:0 auto; margin-top:10px;border-width: 2px; border-radius: 5px; border-color: black; border: double ">
        
        <table class="auto-style1">
            <tr>
                <td class="auto-style8" rowspan="3">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/CoxLOGO.png" Height="60px" style="text-align: right" Width="57px" />
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
                <td class="auto-style7"><span class="auto-style4">Fox :+88 034152511, Cell : +8801762686274-5, e-mail</span><span class="auto-style2"> </span><a href="mailto::cbiu.bd@gmail.com" class="auto-style9"><span class="auto-style2">:cbiu.bd@gmail.com</span></a><span class="auto-style4">, Web :www.cbiu.ac.bd</span></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">&nbsp;</td>
                <td class="auto-style3">
                    <h1 style="margin-bottom:0px">MONEY RECEIPT</h1>
                    <hr style="width: 70%;" />
                </td>
                <td  style="width:18%">&nbsp;</td>
            </tr>

        </table>
        <table class="auto-style1">
            <tr>
                <td class="auto-style11" style="width: 2%">&nbsp;</td>
                <td class="auto-style18" style="width: 6%">
                    &nbsp;</td>
                <td style="width: 16%">
                    &nbsp;</td>
                <td class="auto-style13" style="width: 5%">
                    &nbsp;</td>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style11" style="width: 2%">&nbsp;</td>
                <td class="auto-style18" style="width: 6%">
                    SL. No.&nbsp; :</td>
                <td style="width: 16%">
                    &nbsp;
                    <asp:Label ID="lblTransNO" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td class="auto-style13" style="width: 5%">
                    Date&nbsp; :</td>
                <td style="width: 10%">
                    <asp:Label ID="lblTransDate" runat="server"></asp:Label>
                </td>
                <td style="width: 2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style11" style="width: 2%">&nbsp;</td>
                <td class="auto-style18" style="width: 6%">
                    &nbsp;</td>
                <td style="width: 16%">
                    &nbsp;</td>
                <td class="auto-style13" style="width: 5%">
                    &nbsp;</td>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style11" style="width: 2%">&nbsp;</td>
                <td class="auto-style18" style="width: 6%">
                    Program Name&nbsp; :</td>
                <td colspan="3">
                    <asp:Label ID="lblProNM" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                </td>
                <td style="width: 2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style11" style="width: 2%">&nbsp;</td>
                <td class="auto-style18" style="width: 6%">
                    Received From&nbsp; :</td>
                <td style="width: 16%">
                    <asp:Label ID="lblRcvFR" runat="server"></asp:Label>
                </td>
                <td class="auto-style13" style="width: 5%">&nbsp;</td>
                <td style="width: 10%">&nbsp;</td>
                <td style="width: 2%">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style11" style="width: 2%">&nbsp;</td>
                <td class="auto-style18" style="width: 6%">
                    Student&#39;s ID No&nbsp; :</td>
                <td style="width: 16%">
                    <asp:Label ID="lblStuID" runat="server"></asp:Label>
                </td>
                <td class="auto-style13" style="width: 5%">
                    Semester Name&nbsp; :</td>
                <td style="width: 10%">
                    <asp:Label ID="lblSemNM" runat="server"></asp:Label>
                </td>
                <td style="width: 2%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10" style="width: 2%"></td>
                <td class="auto-style17" style="width: 6%"></td>
                <td class="auto-style10" style="width: 16%"></td>
                <td class="auto-style10" style="width: 5%"></td>
                <td class="auto-style10" style="width: 10%"></td>
                <td class="auto-style10" style="width: 2%">&nbsp;</td>
            </tr>
            </table>
        <table class="auto-style1">
            <tr>
                <td>
                    <table class="auto-style1">
                        <tr>
                            <td style="width: 5%">&nbsp;</td>
                            <td>
                    <asp:GridView ID="gv_MR" runat="server" AutoGenerateColumns="False"  Width="100%" style="text-align: left">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("SL") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle  Width="2%" HorizontalAlign="Center"/>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Particulars Of Fees" SortExpression="FEESID">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("FEESNM") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FEESNM") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="30%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Eval("REMARKS") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="20%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                            </td>
                            <td style="width: 5%">&nbsp;</td>
                        </tr>
                    </table>
                    <table class="auto-style1">
                        <tr>
                            <td style="width: 5%; text-align: right;">&nbsp;</td>
                            <td style="width: 30%; text-align: right;">Amount&nbsp; :&nbsp; &nbsp;&nbsp; </td>
                            <td style="width: 10%; text-align: right;">
                                &nbsp;<asp:Label ID="lblAmount" runat="server" style="font-weight: 700"></asp:Label>
                                &nbsp;<strong>/</strong>=</td>
                            <td style="width: 20%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 5%; text-align: right;">&nbsp;</td>
                            <td style="width: 30%; text-align: right;">Vat&nbsp; :&nbsp;&nbsp;&nbsp;&nbsp; </td>
                            <td style="width: 10%; text-align: right;">
                                <strong>
                                <asp:Label ID="lblVat" runat="server"></asp:Label>
                                &nbsp;/</strong>=</td>
                            <td style="width: 20%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 5%; text-align: right;">&nbsp;</td>
                            <td style="width: 30%; text-align: right;">Total Amount&nbsp; :&nbsp;&nbsp;&nbsp;&nbsp; </td>
                            <td style="width: 10%; text-align: right;">
                                <strong>
                                <asp:Label ID="lblVatAmount" runat="server"></asp:Label>
                                &nbsp;/=</strong></td>
                            <td style="width: 20%">&nbsp;</td>
                        </tr>
                    </table>
                    <table class="auto-style1">
                        <tr>
                            <td style="width: 6%">&nbsp;</td>
                            <td class="auto-style18" style="width: 12%"><span style="box-sizing: border-box; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; orphans: auto; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; color: rgb(0, 0, 0); font-family: Calibri; font-size: 16px; line-height: normal; text-align: -webkit-center; float: none; display: inline !important; background-color: rgb(255, 255, 255);">Account N</span><span style="color: rgb(51, 51, 51); font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 13.63636302948px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 18.1818180084229px; orphans: auto; text-align: right; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">ame&nbsp; 
                                :</span></td>
                            <td colspan="2">
                                <asp:Label ID="lblAcNM" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 6%">&nbsp;</td>
                            <td class="auto-style18" style="width: 12%">PO/DD No&nbsp; : </td>
                            <td colspan="2">
                                <asp:Label ID="lblPODDNO" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 6%">&nbsp;</td>
                            <td class="auto-style18" style="width: 12%">PO Date&nbsp; : </td>
                            <td colspan="2">
                                <asp:Label ID="lblPODT" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 6%">&nbsp;</td>
                            <td class="auto-style18" style="width: 12%">PO Bank&nbsp; : </td>
                            <td colspan="2">
                                <asp:Label ID="lblPOBNK" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 6%">&nbsp;</td>
                            <td class="auto-style18" style="width: 12%">PO Branch&nbsp; :</td>
                            <td colspan="2">
                                <asp:Label ID="lblPOBRNC" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 6%">&nbsp;</td>
                            <td class="auto-style18" style="width: 12%">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 6%">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
&nbsp;</td>
            </tr>
        </table>
        <table class="auto-style1">
            <tr>
                <td style="width: 6%">&nbsp;</td>
                <td style="width: 15%" class="auto-style13">&nbsp;</td>
                <td style="width: 60%">&nbsp;</td>
                <td style="width: 15%">&nbsp;</td>
                <td style="width: 6%">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 6%">&nbsp;</td>
                <td style="width: 15%" class="auto-style13">Taka in words&nbsp;&nbsp;&nbsp; :</td>
                <td style="width: 60%">
                    <asp:Label ID="lblTKinWRD" runat="server"></asp:Label>
                    &nbsp; </td>
                <td style="width: 15%">&nbsp;</td>
                <td style="width: 6%">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 6%">&nbsp;</td>
                <td style="width: 15%">&nbsp;</td>
                <td style="width: 60%">&nbsp;</td>
                <td style="width: 15%">&nbsp;</td>
                <td style="width: 6%">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 6%">&nbsp;</td>
                <td style="width: 15%">&nbsp;</td>
                <td style="width: 60%">&nbsp;</td>
                <td style="width: 15%">&nbsp;</td>
                <td style="width: 6%">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 6%">&nbsp;</td>
                <td style="width: 15%">&nbsp;</td>
                <td style="width: 60%">&nbsp;</td>
                <td class="auto-style6" style="width: 15%">&nbsp;</td>
                <td style="width: 6%" class="auto-style6">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15" style="width: 6%">&nbsp;</td>
                <td class="auto-style14" style="width: 15%"><hr />
                    <div class="auto-style6">
                        Student&#39;s Signature </div>
                </td>
                <td class="auto-style14" style="width: 60%"></td>
                
                <td class="auto-style14" style="width: 15%"><hr />
                    <div class="auto-style6">
                        Accounts Officer</div>
                </td>
                <td class="auto-style14" style="width: 6%">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15" style="width: 6%">&nbsp;</td>
                <td class="auto-style14" style="width: 15%">&nbsp;</td>
                <td class="auto-style14" style="width: 60%">&nbsp;</td>
                
                <td class="auto-style14" style="width: 15%">&nbsp;</td>
                <td class="auto-style14" style="width: 6%">&nbsp;</td>
            </tr>
        </table>

    </div>
    </form>
</body>
</html>
