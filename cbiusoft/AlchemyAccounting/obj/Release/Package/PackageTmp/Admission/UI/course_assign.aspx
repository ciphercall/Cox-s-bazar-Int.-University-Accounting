<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="course_assign.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.course_assign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
            color: #999999;
            text-align: center;
        }

        .gv tr:hover {
            background-color: #ed9393 !important;
            color: Black;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>

            <p class="auto-style1">
                <strong>Course Assign</strong>
                <hr />
                <p>
                </p>
                <table class="nav-justified">
                    <tr>
                        <td style="width: 10%"><strong>Teacher : </strong></td>
                        <td style="width: 35%">
                            <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged1" Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 50%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 10%"><strong>Program : </strong></td>
                        <td style="width: 35%">
                            <asp:DropDownList ID="ddlProgram" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 50%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gv_Assign" runat="server" AutoGenerateColumns="False" CssClass="gv" Font-Size="9pt" OnRowDataBound="gv_Assign_RowDataBound" OnRowDeleting="gv_Assign_RowDeleting" Style="margin-bottom: 0px" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SL" HeaderText="SL.">
                        <HeaderStyle Width="2%" />
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="COURSECD" HeaderText="Course Code">
                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="COURSENM" HeaderText="Course Name">
                        <HeaderStyle HorizontalAlign="Center" Width="35%" />
                        <ItemStyle HorizontalAlign="Left" Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CREDITHH" HeaderText="Course Credit">
                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SEMID" HeaderText="Remarks">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Check">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/update.jpg" TabIndex="10" ToolTip="Edit" Width="20px" />
                            </ItemTemplate>
                            <HeaderStyle Width="3%" />
                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="sl" runat="server" Text='<%#Eval("SLL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="courseID" runat="server" Text='<%#Eval("COURSEID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#CCCCCC" Font-Size="10pt" />
                </asp:GridView>
            </p>
        </ContentTemplate>
    </asp:UpdatePanel>
     <script src="../../Scripts/jquery-1.4.1.js"></script>
</asp:Content>
