<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="AlchemyAccounting.Info.UI.Department" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/jquery-1.9.0.js"></script>
    <script src="../../Scripts/jquery-ui.js"></script>
    <script type="text/javascript">
        
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
        .auto-style1 {
            text-align: center;
            font-size: xx-large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrip" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>



            <asp:Label ID="lblDEPTID" runat="server" Visible="False"></asp:Label>
            <div style="padding: 15px;">

                <table class="nav-justified">
                    <tr>
                        <td>
                            <asp:GridView ID="gv_DEPT" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="gv_DEPT_RowCommand" ShowFooter="True" OnRowDataBound="gv_DEPT_RowDataBound" OnRowCancelingEdit="gv_DEPT_RowCancelingEdit" OnRowDeleting="gv_DEPT_RowDeleting" OnRowEditing="gv_DEPT_RowEditing" OnRowUpdating="gv_DEPT_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDEPTID" runat="server" Text='<%# Eval("DEPTID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtItemCD" ReadOnly="true" runat="server" Width="98%" CssClass="form-control" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="6%" />
                                        <ItemStyle Width="6%" HorizontalAlign="Center"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDEPTNMEdit" runat="server" TabIndex="10" CssClass="form-control" Text='<%# Eval("DEPTNM") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDEPTNM" runat="server" Text='<%# Eval("DEPTNM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDEPTNMFooter" TabIndex="1" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="20%" />
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dept Short Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDEPTSNMEdit" ReadOnly="true" runat="server" TabIndex="10" CssClass="form-control" Text='<%# Eval("DEPTSNM") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDEPTSNM" runat="server" Text='<%# Eval("DEPTSNM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDEPTSNMFooter" TabIndex="1" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtREMARKSEdit" runat="server" TabIndex="11" CssClass="form-control" MaxLength="100" Text='<%# Eval("REMARKS") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtREMARKSFooter" runat="server" TabIndex="2" CssClass="form-control" MaxLength="100" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="25%" />
                                        <ItemStyle Width="25%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnPUpdate" TabIndex="12" runat="server" CommandName="Update" CssClass="txtColor" Height="20px" ImageUrl="~/Images/update.png" ToolTip="Update" Width="20px" />
                                            <asp:ImageButton ID="imgbtnPCancel" TabIndex="13" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.png" ToolTip="Cancel" Width="20px" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imgbtnPAdd" runat="server" TabIndex="3" CommandName="Add" CssClass="txtColor" Height="30px" ImageUrl="~/Images/AddNewitem.jpg" ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="30px" />
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit.png" ToolTip="Edit" Width="20px" />
                                            <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/Delete.png" OnClientClick="return confMSG()" ToolTip="Delete" Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="4%" />
                                        <ItemStyle Width="4%" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#2aabd2" BorderColor="#333333" BorderWidth="2px" ForeColor="White" />
                            </asp:GridView>
                            <div style="display: none">
                                <asp:TextBox ID="txtLtude" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtLngTude" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>

                        </td>
                    </tr>
                </table>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

