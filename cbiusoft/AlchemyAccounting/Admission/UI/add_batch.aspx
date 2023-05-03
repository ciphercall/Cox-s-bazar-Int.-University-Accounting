<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add_batch.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.add_batch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <table class="nav-justified" style="WIDTH: 100%">
            <tr>
                <td class="text-right" width="30%">Year :</td>
                <td class="text-left" width="30%">
                   
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="19px" Width="62px">
                        <asp:ListItem>2014</asp:ListItem>
                        <asp:ListItem>2015</asp:ListItem>
                        <asp:ListItem>2016</asp:ListItem>
                        <asp:ListItem>2017</asp:ListItem>
                        <asp:ListItem>2018</asp:ListItem>
                    </asp:DropDownList>
                   
                </td>
                <td width="30%">&nbsp;</td>
            </tr>
            <tr>
                <td class="text-right" width="30%">Semester :</td>
                <td class="text-left" width="30%">
                   
                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                         <asp:ListItem Value="1">SPRING</asp:ListItem> 
                        <asp:ListItem Value="2">SUMMER</asp:ListItem> 
                    </asp:DropDownList>
                   
                </td>
                <td width="30%">&nbsp;</td>
            </tr>
            <tr>
                <td class="text-right" width="30%">Batch :</td>
                <td class="text-left" width="30%">
                    <asp:TextBox ID="TextBox3" runat="server" placeholder="000" Width="51%"></asp:TextBox>
                </td>
                <td width="30%">&nbsp;</td>
            </tr>
            <tr>
                <td width="30%">&nbsp;</td>
                <td class="text-left" width="30%">
                    <asp:Button ID="Update" runat="server" Text="Update" Width="131px" OnClick="Update_Click" />
                    <asp:Button ID="Update0" runat="server" Text="Data" Width="131px" OnClick="Update_Click" />
                </td>
                <td width="30%">&nbsp;</td>
            </tr>
            <tr>
                <td width="30%" colspan="3">&nbsp;&nbsp;
                                        <asp:Button ID="all" runat="server" Text="ALL ID" Width="131px" OnClick="all_Click" />
                
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns> 
                        <asp:BoundField DataField="SL" HeaderText="SL">
                            <HeaderStyle HorizontalAlign="Center" Width="3%" />
                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="STUDENTNM" HeaderText="NAME">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="STUDENTID" HeaderText="ID">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                         <asp:BoundField DataField="NEWSTUDENTID" HeaderText="NEW ID">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BATCH" HeaderText="Batch No">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                         <asp:BoundField DataField="PROGRAMSID" HeaderText="PROGRAM S">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
