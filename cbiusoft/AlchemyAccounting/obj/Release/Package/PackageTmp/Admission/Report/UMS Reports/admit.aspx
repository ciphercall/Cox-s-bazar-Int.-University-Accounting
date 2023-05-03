<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="admit.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.admit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            text-align: center;
            color: #FFFFFF;
        }

        .auto-style2 {
            font-size: large;
        }
        .auto-style3 {
            text-align: center;
            color: #CC0000;
            font-size: x-large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <table class="nav-justified" width="100%">
              <tr style="">
                <td class="auto-style3" colspan="3"><strong>Admit Card<hr /> 
                    </strong></td>
            </tr>
              <tr style="background: #808080;border-radius:5px">
                <td class="auto-style1" colspan="3"><strong><span class="auto-style2">A</span>ll <span class="auto-style2">S</span>tudent</strong></td>
            </tr>
            <tr>
                <td style="width: 30%" class="text-right">Program Name:</td>
                <td style="width: 30%">
                    <asp:DropDownList ID="ddlProNM" runat="server" CssClass="form-control" Height="30px" Width="100%" >
                    </asp:DropDownList></td>
                <td style="width: 30%">
                    <asp:DropDownList ID="ddlSemNM" runat="server" TabIndex="8" Width="200PX" CssClass="form-control"> 
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 30%" class="text-right">Semester:</td>
                <td style="width: 30%">
                    <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="8" Width="100%" CssClass="form-control">
                        <asp:ListItem Value="00">Select</asp:ListItem>
                        <asp:ListItem Value="01">1st</asp:ListItem>
                        <asp:ListItem Value="02">2nd</asp:ListItem>
                        <asp:ListItem Value="03">3rd</asp:ListItem>
                        <asp:ListItem Value="04">4th</asp:ListItem>
                        <asp:ListItem Value="05">5th</asp:ListItem>
                        <asp:ListItem Value="06">6th</asp:ListItem>
                        <asp:ListItem Value="07">7th</asp:ListItem>
                        <asp:ListItem Value="08">8th</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="width: 30%">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 30%"></td>
                <td style="width: 30%">
                    <asp:Button ID="btnAdmit" runat="server" BackColor="White" CssClass="form-control" Font-Bold="True" Font-Size="12pt" ForeColor="Black" Text="Submit" Width="120px" BorderColor="#3399FF" BorderWidth="3px" OnClick="btnAdmit_Click" />
                    </td>
                <td style="width: 30%">
                    
                </td>
            </tr>
            <tr>
                <td style="width: 30%">&nbsp;</td>
                <td style="width: 30%">&nbsp;</td>
                <td style="width: 30%">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 30%">&nbsp;</td>
                <td style="width: 30%">&nbsp;</td>
                <td style="width: 30%">&nbsp;</td>
            </tr>
            <tr style="background: #808080;border-radius:5px">
                <td class="auto-style1" colspan="3"><strong><span class="auto-style2">S</span>pecific&nbsp; <span class="auto-style2">S</span>tudent</strong></td>
            </tr>
            
            <tr>
                <td style="width: 30%" class="text-right">Program Name:</td>
                <td style="width: 30%">
                    <asp:DropDownList ID="ddlProNM2" runat="server" CssClass="form-control" Height="30px" Width="100%" OnSelectedIndexChanged="ddlProNM2_SelectedIndexChanged" >
                    </asp:DropDownList></td>
                <td style="width: 30%">
                    <asp:DropDownList ID="ddlSemNM2" runat="server" TabIndex="8" Width="200PX" CssClass="form-control"> 
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td style="width: 30%" class="text-right">Semester:</td>
                <td style="width: 30%">
                    <asp:DropDownList ID="ddlSemester2" runat="server" TabIndex="8" Width="100%" CssClass="form-control" OnSelectedIndexChanged="ddlSemester2_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="00">Select</asp:ListItem>
                        <asp:ListItem Value="01">1st</asp:ListItem>
                        <asp:ListItem Value="02">2nd</asp:ListItem>
                        <asp:ListItem Value="03">3rd</asp:ListItem>
                        <asp:ListItem Value="04">4th</asp:ListItem>
                        <asp:ListItem Value="05">5th</asp:ListItem>
                        <asp:ListItem Value="06">6th</asp:ListItem>
                        <asp:ListItem Value="07">7th</asp:ListItem>
                        <asp:ListItem Value="08">8th</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="width: 30%">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 30%" class="text-right">Student ID:</td>
                <td style="width: 30%">
                    <asp:DropDownList ID="ddlStudent" runat="server" CssClass="form-control" Height="30px" Width="100%">
                    </asp:DropDownList></td>
                <td style="width: 30%">&nbsp;</td>
            </tr>

            <tr>
                <td style="width: 30%" class="text-right"></td>
                <td style="width: 30%">
                   <asp:Button ID="btnStuAdmit" runat="server" BackColor="White" CssClass="form-control" Font-Bold="True" Font-Size="12pt" ForeColor="Black" Text="Submit" Width="120px" BorderColor="#3399FF" BorderWidth="3px" OnClick="btnStuAdmit_Click" /></td>
                <td style="width: 30%">
                    
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
