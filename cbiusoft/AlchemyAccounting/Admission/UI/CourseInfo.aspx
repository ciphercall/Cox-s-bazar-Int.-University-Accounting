<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseInfo.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.CourseInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
            width: 365px;
        }

        .auto-style2 {
            text-align: right;
            width: 234px;
        }

        .auto-style4 {
            width: 544px;
        }

        .auto-style6 {
            width: 234px;
        }

        .auto-style7 {
            width: 544px;
            text-align: center;
        }
        .auto-style8 {
            width: 234px;
            text-align: center;
        }

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
    <div style="border: double; border-color: black; border-radius: 10px; border-width: 2px">
        <div style="border: 2px double white; border-top-left-radius:10px;border-top-right-radius:10px; color: #FFFFFF; font-size: xx-large; text-align: center; background-color: #2aabd2;"><span class="auto-style1">C</span>ourse <span class="auto-style1">I</span>nformation</div>
        <table class="nav-justified">
            <tr>
                <td class="auto-style6" width="20%">&nbsp;</td>
                <td class="auto-style4" height="30" width="40%">
                    <asp:Label ID="lblProID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblCrsID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblCrsCD" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblProSrtNM" runat="server" Visible="False"></asp:Label>
                </td>
                <td width="20%">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" width="20%">Program Name :</td>
                <td class="auto-style4" height="30" width="40%">
                    <asp:DropDownList ID="ddlProNM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProNM_SelectedIndexChanged" Width="100%" AutoPostBack="True">
                    </asp:DropDownList>
                    </td>
                <td width="20%">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8" width="20%">&nbsp;</td>
                <td class="auto-style7" height="30" width="40%">
                    <asp:Label ID="lblMSG" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </td>
                <td width="20%">&nbsp;</td>
            </tr>
        </table>
        <div style="border: double; border-color: #808080; padding:10px; border-radius: 10px; border-width: 2px">
            <div style="color:black">
            <asp:GridView ID="gvCourse" runat="server"  AutoGenerateColumns="False" Width="100%" ShowFooter="True" OnRowCommand="gvCourse_RowCommand" OnRowDeleting="gvCourse_RowDeleting" OnRowCancelingEdit="gvCourse_RowCancelingEdit" OnRowEditing="gvCourse_RowEditing" OnRowUpdating="gvCourse_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="Course Name" SortExpression="COURSENM">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCOURSENMEdit" Width="100%" AutoPostBack="true" CssClass="form-control" runat="server" Text='<%# Eval("COURSENM") %>' OnTextChanged="txtCOURSENMEdit_TextChanged" MaxLength="100"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCOURSENM" runat="server" Text='<%# Eval("COURSENM") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCOURSENMFooter" Width="100%" CssClass="form-control" runat="server" PlaceHolder="Course Name" AutoPostBack="True" OnTextChanged="txtCOURSENMFooter_TextChanged" MaxLength="100"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle Width="18%"/>
                        <ItemStyle Width="18%"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course ID" SortExpression="COURSEID">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCOURSEIDEdit" Enabled="false" Width="100%" CssClass="form-control" runat="server" Text='<%# Eval("COURSEID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCOURSEID" runat="server" Text='<%# Eval("COURSEID") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCOURSEIDFooter" Enabled="false" Width="100%" CssClass="form-control" runat="server" PlaceHolder="CourseID"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle Width="10%"/>
                        <ItemStyle Width="10%"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Code" SortExpression="COURSECD">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCOURSECDEdit" Width="100%" CssClass="form-control" runat="server" Text='<%# Eval("COURSECD") %>' MaxLength="15"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCOURSECD" runat="server" Text='<%# Eval("COURSECD") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCOURSECDFooter" Width="100%" CssClass="form-control" runat="server" PlaceHolder="Course Code" MaxLength="15"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle Width="10%"/>
                        <ItemStyle Width="10%"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit " SortExpression="CREDITHH">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCREDITHHEdit" Width="100%" CssClass="form-control" runat="server" Text='<%# Eval("CREDITHH") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCREDITHH" runat="server" Text='<%# Eval("CREDITHH") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCREDITHHFooter" Width="100%" CssClass="form-control" runat="server" PlaceHolder="CR"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle Width="6%"/>
                        <ItemStyle Width="6%"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Semister">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlSemisterNMEdit" CssClass="form-control" Width="100%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSemisterNMEdit_SelectedIndexChanged">
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
                            <asp:Label ID="lblSem" runat="server" Text='<%# Eval("SEMID") %>' Visible="false"></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSEMID" runat="server" Text='<%# Eval("SEMID") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlSemisterNMFooter" CssClass="form-control" Width="100%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSemisterNMFooter_SelectedIndexChanged">
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
                        </FooterTemplate>
                        <HeaderStyle Width="8%"/>
                         <ItemStyle Width="8%"/>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="SL" Visible="false">
                        <EditItemTemplate>
                             <asp:Label ID="lblSEMSLEdit" runat="server" Text='<%# Eval("SEMSL") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSEMSL" runat="server" Text='<%# Eval("SEMSL") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblSEMSLFooter" runat="server" ></asp:Label>
                        </FooterTemplate> 
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Remarks" SortExpression="REMARKS">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtREMARKSEdit" Width="100%" CssClass="form-control" runat="server" Text='<%# Eval("REMARKS") %>' TabIndex="66" MaxLength="100"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtREMARKSFooter" Width="100%" CssClass="form-control" TabIndex="55" runat="server" PlaceHolder="Remarks" MaxLength="100"></asp:TextBox>
                        </FooterTemplate>
                        <HeaderStyle Width="15%"/>
                         <ItemStyle Width="15%"/>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnPUpdate"  CssClass="txtColor" runat="server" CommandName="Update" Height="20px"
                                ImageUrl="~/Images/update.PNG" TabIndex="67" ToolTip="Update" Width="20px" />
                            <asp:ImageButton
                                ID="imgbtnPCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.PNG"
                                TabIndex="68" ToolTip="Cancel" Width="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnPAdd" runat="server" CommandName="Add" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="56" ToolTip="Save &amp; Continue"
                                ValidationGroup="validaiton" Width="30px" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px"
                                ImageUrl="~/Images/Edit.PNG" TabIndex="10" ToolTip="Edit" Width="20px" />
                            <asp:ImageButton
                                    ID="imgbtnPDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()"
                                    Height="20px" ImageUrl="~/Images/delete.PNG" TabIndex="11" ToolTip="Delete" Width="20px" />
                        </ItemTemplate>
                        <HeaderStyle Width="6%" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataRowStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="#2aabd2" ForeColor="White" BorderColor="Black" BorderWidth="2px" />
            </asp:GridView>
                </div>
        </div>
    </div>
            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
