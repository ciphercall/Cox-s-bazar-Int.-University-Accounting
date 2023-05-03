<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SALDRCR.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.SALDRCR" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <%--    <script type="text/javascript" src="../../Scripts/jquery-1.9.0.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.js"></script>
    <link href="../../css/jquery-ui-1.8.16.custom.css" rel="stylesheet" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" />--%>
    <link href="../../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../../Content/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="../../Scripts/jquery-ui.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui-1.8.11.min.js"></script>
    <%-- <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" />--%>
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" />
    <link href="../../css/jquery-ui-1.8.16.custom.css" rel="stylesheet" />

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
        .ui-datepicker-calendar {
            display: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrip" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            &nbsp;<asp:Label ID="lblPostID" runat="server" Visible="False"></asp:Label>
            <div style="padding: 15px;">

                <table class="nav-justified">
                    <tr>
                        <td>

                            <div>
                                <asp:DropDownList ID="ddlTransMY_Month" CssClass="form-control" runat="server" Width="97px" AutoPostBack="True" OnSelectedIndexChanged="ddlTransMY_Month_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>JAN</asp:ListItem>
                                    <asp:ListItem>FEB</asp:ListItem>
                                    <asp:ListItem>MAR</asp:ListItem>
                                    <asp:ListItem>APR</asp:ListItem>
                                    <asp:ListItem>MAY</asp:ListItem>
                                    <asp:ListItem>JUN</asp:ListItem>
                                    <asp:ListItem>JUL</asp:ListItem>
                                    <asp:ListItem>AGU</asp:ListItem>
                                    <asp:ListItem>SEP</asp:ListItem>
                                    <asp:ListItem>OCT</asp:ListItem>
                                    <asp:ListItem>NOV</asp:ListItem>
                                    <asp:ListItem>DEC</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlTransMY_Year" runat="server" CssClass="form-control" Width="110px" AutoPostBack="True" OnSelectedIndexChanged="ddlTransMY_Year_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Label ID="lblTransMY" runat="server" Visible="False"></asp:Label>
                            </div>
                            <br />
                            <br />
                            <div>
                                <asp:GridView ID="Gv_HR_SALDRCR" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" OnRowCancelingEdit="Gv_HR_SALDRCR_RowCancelingEdit" OnRowCommand="Gv_HR_SALDRCR_RowCommand" OnRowDeleting="Gv_HR_SALDRCR_RowDeleting" OnRowEditing="Gv_HR_SALDRCR_RowEditing" OnRowUpdating="Gv_HR_SALDRCR_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEMPNMEdit" runat="server" TabIndex="10" AutoPostBack="true" CssClass="form-control" Text='<%# Eval("EMPNM") %>' Width="100%" OnTextChanged="txtEMPNMEdit_TextChanged"></asp:TextBox>
                                               <asp:AutoCompleteExtender ID="AutoCompleteExtender"
                                                    runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=""
                                                    Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionMemberNM" CompletionListCssClass="AutoExtender"
                                                    CompletionListItemCssClass="AutoExtenderList"
                                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                    TargetControlID="txtEMPNMEd.it" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEMPNM" runat="server" Text='<%# Eval("EMPNM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtEMPNMFooter" TabIndex="50" AutoPostBack="true" runat="server" Width="100%" CssClass="form-control" OnTextChanged="txtEMPNMFooter_TextChanged" />

                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender"
                                                    runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=""
                                                    Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionMemberNM" CompletionListCssClass="AutoExtender"
                                                    CompletionListItemCssClass="AutoExtenderList"
                                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                    TargetControlID="txtEMPNMFooter" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                            </FooterTemplate>
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMPID">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEMPIDEdit" ReadOnly="true" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("EMPID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEMPID" runat="server" Text='<%# Eval("EMPID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtEMPIDFooter" ReadOnly="true" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month Day">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtMMDAYEdit" runat="server" TabIndex="11" CssClass="form-control" Text='<%# Eval("MMDAY") %>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMMDAY" runat="server" Text='<%# Eval("MMDAY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtMMDAYFooter" TabIndex="51" runat="server" Width="100%" CssClass="form-control" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Holiday">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtHDAYEdit" runat="server" TabIndex="12" CssClass="form-control" Text='<%# Eval("HDAY") %>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblHDAY" runat="server" Text='<%# Eval("HDAY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtHDAYFooter" TabIndex="52" runat="server" Width="100%" CssClass="form-control" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Present Day">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPREDAYEdit" runat="server" TabIndex="13" CssClass="form-control" Text='<%# Eval("PREDAY") %>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPREDAY" runat="server" Text='<%# Eval("PREDAY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPREDAYFooter" TabIndex="53" runat="server" Width="100%" CssClass="form-control" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Leave Day">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtLDAYEdit" runat="server" TabIndex="14" CssClass="form-control" Text='<%# Eval("LDAY") %>' Width="100%" AutoPostBack="true" OnTextChanged="txtLDAYEdit_TextChanged"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLDAY" runat="server" Text='<%# Eval("LDAY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtLDAYFooter" TabIndex="54" runat="server" Width="100%" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtLDAYFooter_TextChanged" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Absent Day">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtABSDAYEdit" runat="server" Enabled="false" TabIndex="15" CssClass="form-control" Text='<%# Eval("ABSDAY") %>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblABSDAY" runat="server" Text='<%# Eval("ABSDAY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtABSDAYFooter" TabIndex="55" Enabled="false" runat="server" Width="100%" CssClass="form-control" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Allowance">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtALLOWANCEEdit" runat="server" TabIndex="16" CssClass="form-control" Text='<%# Eval("ALLOWANCE") %>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblALLOWANCE" runat="server" Text='<%# Eval("ALLOWANCE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtALLOWANCEFooter" TabIndex="56" runat="server" Width="100%" CssClass="form-control" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advance">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtADVANCEEdit" runat="server" TabIndex="17" CssClass="form-control" Text='<%# Eval("ADVANCE") %>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblADVANCE" runat="server" Text='<%# Eval("ADVANCE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtADVANCEFooter" TabIndex="57" runat="server" Width="100%" CssClass="form-control" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="imgbtnPUpdate" TabIndex="18" runat="server" CommandName="Update" CssClass="txtColor" Height="20px" ImageUrl="~/Images/update.png" ToolTip="Update" Width="20px" />
                                                <asp:ImageButton ID="imgbtnPCancel" TabIndex="19" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.png" ToolTip="Cancel" Width="20px" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="imgbtnPAdd" runat="server" TabIndex="58" CommandName="Add" CssClass="txtColor" Height="30px" ImageUrl="~/Images/AddNewitem.jpg" ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="30px" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit.png" TabIndex="10" ToolTip="Edit" Width="20px" />
                                                <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/Delete.png" OnClientClick="return confMSG()" TabIndex="11" ToolTip="Delete" Width="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="4%" />
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#0099FF" ForeColor="White" />
                                </asp:GridView>
                            </div>
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

