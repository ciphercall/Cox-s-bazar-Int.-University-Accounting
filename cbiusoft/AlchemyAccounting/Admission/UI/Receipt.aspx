<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.Receipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            $("#txtTESTDTfooter,#txtTESTDTEdit,#txtPInDT,#txtPLCDT,#txtInDT_Trans,#txtTInDt_Ret").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-5:+10" });
        }

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

        .auto-style2 {
            font-size: larger;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
   
            <div style="border: double; border-width: 2px; border-radius: 10px">
                <div style="border: 2px double white; border-top-right-radius: 10px; border-top-left-radius: 10px; background-color: #2aabd2; color: #FFFFFF;" class="auto-style1"><span class="auto-style2">F</span>ees <span class="auto-style2">I</span>nformation</div>

         
            &nbsp;<asp:Label ID="lblFeesID" runat="server" Visible="False"></asp:Label>
                <div style="padding:15px;">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" OnRowCommand="GridView1_RowCommand" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        
                        <ItemTemplate>
                            <asp:Label ID="lblFEESID" runat="server" Text='<%# Eval("FEESID") %>'></asp:Label>
                        </ItemTemplate>
                        
                        <HeaderStyle Width="6%" />
                        <ItemStyle Width="6%" />
                        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Particular Of Fees">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFEESNMEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("FEESNM") %>' MaxLength="100"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFEESNM" runat="server" Text='<%# Eval("FEESNM") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFEESNMFooter" CssClass="form-control" Width="100%" runat="server" MaxLength="100"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount in Taka">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFEESRTEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("FEESRT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFEESRT" runat="server" Text='<%# Eval("FEESRT") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFEESRTFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                        <ItemStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtREMARKSEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("REMARKS") %>' MaxLength="100"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtREMARKSFooter" CssClass="form-control" Width="100%" runat="server" MaxLength="100"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnPUpdate" CssClass="txtColor" runat="server" CommandName="Update" Height="20px"
                                ImageUrl="~/Images/update.png" TabIndex="67" ToolTip="Update" Width="20px" />
                            <asp:ImageButton
                                ID="imgbtnPCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.png"
                                TabIndex="68" ToolTip="Cancel" Width="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnPAdd" runat="server" CommandName="Add" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="56" ToolTip="Save &amp; Continue"
                                ValidationGroup="validaiton" Width="30px" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px"
                                ImageUrl="~/Images/Edit.png" TabIndex="10" ToolTip="Edit" Width="20px" /><%--<asp:ImageButton
                                    ID="imgbtnPDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()"
                                    Height="20px" ImageUrl="~/Images/delete.png" TabIndex="11" ToolTip="Delete" Width="20px" />--%>
                        </ItemTemplate>
                         <ItemStyle Width="5%" HorizontalAlign="Center" />

                    </asp:TemplateField>

                </Columns>
                <HeaderStyle BackColor="#2aabd2" BorderWidth="2px" ForeColor="White" BorderColor="#333333" />
            </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
