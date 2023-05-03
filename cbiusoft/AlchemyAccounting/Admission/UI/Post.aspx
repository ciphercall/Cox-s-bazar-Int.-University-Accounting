<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="AlchemyAccounting.Info.UI.Post" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
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

         
            &nbsp;<asp:Label ID="lblPostID" runat="server" Visible="False"></asp:Label>
                <div style="padding:15px;">
                   
                    <table class="nav-justified">
                        <tr>
                            <td>
                                <asp:GridView ID="gv_post" runat="server" AutoGenerateColumns="False"  Width="100%" OnRowCommand="gv_post_RowCommand" ShowFooter="True" OnRowDataBound="gv_post_RowDataBound" OnRowCancelingEdit="gv_post_RowCancelingEdit" OnRowDeleting="gv_post_RowDeleting" OnRowEditing="gv_post_RowEditing" OnRowUpdating="gv_post_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPOSTID" runat="server" Text='<%# Eval("POSTID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                               <asp:TextBox ID="txtItemCD" ReadOnly="true" runat="server" Width="98%"  CssClass="form-control" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="6%" />
                                            <ItemStyle Width="6%" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Post Name">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPOSTNMEdit" runat="server" TabIndex="10" CssClass="form-control" Text='<%# Eval("POSTNM") %>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPOSTNM" runat="server" Text='<%# Eval("POSTNM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPOSTNMFooter" TabIndex="1" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
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
                                             <HeaderStyle Width="30%" />
                                            <ItemStyle Width="30%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="imgbtnPUpdate" TabIndex="12" runat="server" CommandName="Update" CssClass="txtColor" Height="20px" ImageUrl="~/Images/update.png"  ToolTip="Update" Width="20px" />
                                                <asp:ImageButton ID="imgbtnPCancel" TabIndex="13" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.png"  ToolTip="Cancel" Width="20px" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="imgbtnPAdd" runat="server" TabIndex="3" CommandName="Add" CssClass="txtColor" Height="30px" ImageUrl="~/Images/AddNewitem.jpg"  ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="30px" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit.png" TabIndex="10" ToolTip="Edit" Width="20px" />
                                                <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/Delete.png" OnClientClick="return confMSG()" TabIndex="11" ToolTip="Delete" Width="20px" />
                                            </ItemTemplate>
                                             <HeaderStyle Width="4%" />
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#2aabd2" BorderColor="#333333" BorderWidth="2px" ForeColor="White" />
                                </asp:GridView>
                                 <div style="display:none"> 
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
