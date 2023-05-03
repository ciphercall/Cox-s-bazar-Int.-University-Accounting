<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarkingRipoerForm.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.MarkingRipoerForm" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>::Marking Report</title>
    <link href="../../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            $("#txtFrDT,#txtToDT").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+5" });
        }

    </script>
    <style type="text/css">
        .txtColor:focus {
            border: solid 4px green !important;
        }

        .txtColor {
            margin-left: 0px;
        }

        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="Script1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Update1" runat="server">
            <ContentTemplate>
                <div style="border-style: double; margin: 0 auto; border-width: 2px; border-radius: 10px; border-color: black; width: 960px; color: #000000;">
                    <div style="border-top-left-radius: 10px; border-top-right-radius: 10px; color: #FFFFFF; text-align: center; font-size: xx-large; background-color: #0099FF;" class="auto-style2">
                        Result Sheet
                    </div>
                    <div style="margin-bottom: 0px">

                        <table class="auto-style1">
                            <tr>
                                <td style="width: 20%">&nbsp;</td>
                                <td style="width: 20%">
                                    <asp:Label ID="lblMSG" runat="server" Font-Size="10pt" ForeColor="#CC0000" Visible="False"></asp:Label>
                                </td>
                                <td style="width: 20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2" style="width: 20%">Program&nbsp; :</td>
                                <td style="width: 20%">
                                    <asp:DropDownList ID="ddlProgramNM" runat="server" Height="25px" OnSelectedIndexChanged="ddlProgramNM_SelectedIndexChanged" Width="100%" AutoPostBack="True" CssClass="txtColor">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 20%">&nbsp;</td>
                            </tr> 
                            <tr>
                                 <asp:DropDownList ID="ddlSemester" Visible="false" runat="server" AutoPostBack="True" CssClass="txtColor" Height="25px" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" Width="100%">
                                    </asp:DropDownList>
                                <td class="auto-style2">Semester&nbsp; :</td>
                                <td style="width: 20%" class="auto-style2">
                                    <asp:DropDownList ID="ddlSEM" runat="server" AutoPostBack="True" CssClass="txtColor" Height="25px" OnSelectedIndexChanged="ddlSEM_SelectedIndexChanged" Width="100%">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem Value="01">1st</asp:ListItem>
                                        <asp:ListItem Value="02">2nd</asp:ListItem>
                                        <asp:ListItem Value="03">3rd</asp:ListItem>
                                        <asp:ListItem Value="04">4th</asp:ListItem>
                                        <asp:ListItem Value="05">5th</asp:ListItem>
                                        <asp:ListItem Value="06">6th</asp:ListItem>
                                        <asp:ListItem Value="07">7th</asp:ListItem>
                                        <asp:ListItem Value="08">8th</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" class="auto-style2">Batch&nbsp; :</td>
                                <td style="width: 20%; text-align: left;"> 
                                    <asp:DropDownList ID="ddlBatch" runat="server" CssClass="txtColor" Height="25px"  Width="100%"/> 
                                </td>
                                <td style="width: 20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 20%">&nbsp;</td>
                                <td style="width: 20%; text-align: right">
                                    <asp:Label ID="lblProID" runat="server" Visible="False"></asp:Label>
                                    <asp:Button ID="Button1" runat="server" CssClass="txtColor" OnClick="Submit" Text="Search" />
                                </td>
                                <td style="width: 20%">&nbsp;</td>
                            </tr>
                        </table>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

