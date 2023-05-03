<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="post_info.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.post_info" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Fees Collection Report</title>
    <link href="../../MyStyle.css" rel="stylesheet" />
    
    <style type="text/css">
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            text-align: right;
        }
        .auto-style5 {
            font-size: x-large;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin:0 auto; width:816px;   border-radius:10px;border-width:2px;border-color:black;border:groove">
        <div>
            <table class="auto-style3">
                <tr>
                    <td style="text-align: center; font-size: xx-large">Cox&#39;s Bazar International University</td>
                </tr>
            </table>
        </div>
    <div style="border-top-left-radius:10px; border-top-right-radius:10px;border-bottom-left-radius:10px;border-bottom-right-radius:10px; width:350px; margin:0 auto; text-align: center; color: #FFFFFF; background-color: #666666;" class="auto-style5" >Post Information</div>
        <table class="auto-style3">
            <tr>
                <td>
                    <table class="auto-style3">
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style4" style="width: 20%">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td class="auto-style4" style="width: 20%">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td style="padding:5px">
                    <asp:GridView ID="gv_post" CssClass="Grid" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="12pt" >
                        <Columns> 
                            <asp:BoundField  HeaderText="Post " DataField="POSTNM">
                            <HeaderStyle Width="60%" />
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Remarks" DataField="REMARKS">
                            <HeaderStyle Width="40%" />
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                       
                        </Columns>
                        <HeaderStyle BackColor="#CCCCCC" />
                    </asp:GridView>
                   
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>