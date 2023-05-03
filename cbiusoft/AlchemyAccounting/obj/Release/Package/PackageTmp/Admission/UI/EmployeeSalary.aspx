<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeSalary.aspx.cs" Inherits="AlchemyAccounting.Info.UI.EmployeeSalary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            $("#txtPFEFDTEdit,#txtPFEFDTFooter,#txtPFETDTEdit,#txtPFETDTFooter,#txtJOBEFDTEdit,#txtJOBEFDTFooter,#txtJOBETDTFooter,#txtJOBETDTEdit").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-10:+10" });
        }
        $(function () {
            navigator.geolocation.getCurrentPosition(showPosition);
            navigator.geolocation.getCurrentPosition(showPosition, positionError);

            function showPosition(position) {
                var coordinates = position.coords;
                //$("#lat").val(coordinates.latitude);
                //$("#lon").val(coordinates.longitude);
                $("#txtLtude").val(coordinates.longitude);
                $("#txtLngTude").val(coordinates.latitude);


                $("#txtLtude").val(
                    //$("#lat").val() + "," + $("#lon").val()
                );
                $("#txtLngTude").val(
                    //$("#lat").val() + "," + $("#lon").val()
                );
            }
        });

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

    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
           


                <asp:Label ID="lblHDAYID" runat="server" Visible="False"></asp:Label>

                <div style="padding: 0px; text-align: center;">
                    <br />
                    <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>
                    <table class="nav-justified">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtLtude" runat="server" ClientIDMode="Static" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="txtLngTude" runat="server" ClientIDMode="Static" Visible="False"></asp:TextBox>
                                <table class="nav-justified">
                                    <tr>
                                        <td style="width: 25%">&nbsp;</td>
                                        <td style="width: 50%">
                                            <asp:DropDownList ID="ddlEmp" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 25%">&nbsp;</td>
                                    </tr>
                                   
                                </table>
                                <br />
                                <asp:GridView ID="gv_Salary" runat="server" AutoGenerateColumns="False" Font-Size="8pt"
                                    OnRowCommand="gv_Salary_RowCommand" ShowFooter="True" Visible="true" Width="100%" OnRowCancelingEdit="gv_Salary_RowCancelingEdit" OnRowDeleting="gv_Salary_RowDeleting" OnRowEditing="gv_Salary_RowEditing" OnRowUpdating="gv_Salary_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Post Name">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPOSTNMEdit" AutoPostBack="true" TabIndex="14"  runat="server" CssClass="form-control" Text='<%# Eval("POSTNM") %>' Width="100%" OnTextChanged="txtPOSTNMEdit_TextChanged"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="txtPOSTNMEdit_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionPostNM" TargetControlID="txtPOSTNMEdit" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPOSTNM" runat="server" Text='<%# Eval("POSTNM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPOSTNMFooter" TabIndex="1" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtPOSTNMFooter_TextChanged" Width="100%"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="txtPOSTNMFooter_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionPostNM" TargetControlID="txtPOSTNMFooter" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                            </FooterTemplate>
                                            <ItemStyle Width="10%" />
                                            <HeaderStyle  Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblPOSTIDEdit" runat="server"  TabIndex="15" Text='<%# Eval("POSTID") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPOSTID" runat="server" Text='<%# Eval("POSTID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblPOSTIDFooter" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <EditItemTemplate>

                                                <asp:DropDownList ID="ddlStatsEdit" runat="server"  TabIndex="16" CssClass="form-control" Text='<%# Eval("SALSTATUS") %>' Width="100%">

                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>STANDARD</asp:ListItem>
                                                    <asp:ListItem>INCREMENT</asp:ListItem>
                                                    <asp:ListItem>PROMOTION</asp:ListItem>
                                                    <asp:ListItem>DEMOTION</asp:ListItem>
                                                    <asp:ListItem>SUSPEND</asp:ListItem>

                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("SALSTATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlStatsFooter"  TabIndex="2" runat="server" CssClass="form-control" Width="100%">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>STANDARD</asp:ListItem>
                                                    <asp:ListItem>INCREMENT</asp:ListItem>
                                                    <asp:ListItem>PROMOTION</asp:ListItem>
                                                    <asp:ListItem>DEMOTION</asp:ListItem>
                                                    <asp:ListItem>SUSPEND</asp:ListItem>

                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle Width="8%" />
                                             <HeaderStyle  Width="8%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salary(Basic)">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtBASICSALEdit" runat="server"  TabIndex="17"  CssClass="form-control" Text='<%# Eval("BASICSAL") %>' Width="100%">0.00</asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBASICSAL" runat="server" Text='<%# Eval("BASICSAL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtBASICSALFooter"  TabIndex="3" runat="server" CssClass="form-control" Width="100%">0.00</asp:TextBox>
                                            </FooterTemplate>
                                              <ItemStyle HorizontalAlign="Center" Width="4%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="4%"  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="House Rent">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtHOUSERENTEdit" runat="server"  TabIndex="18" CssClass="form-control" Text='<%# Eval("HOUSERENT") %>' Width="100%">0.00</asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblHOUSERENT" runat="server" Text='<%# Eval("HOUSERENT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtHOUSERENTFooter"  TabIndex="4" runat="server" CssClass="form-control" Width="100%">0.00</asp:TextBox>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Width="4%" />
                                             <ItemStyle HorizontalAlign="Right" Width="4%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Medical">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtMEDICALEdit" CssClass="form-control"  TabIndex="19" runat="server" Text='<%# Eval("MEDICAL") %>'>0.00</asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMEDICAL" runat="server" Text='<%# Eval("MEDICAL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtMEDICALFooter"  TabIndex="5" runat="server" CssClass="form-control" Width="100%">0.00</asp:TextBox>
                                            </FooterTemplate>
                                               <HeaderStyle HorizontalAlign="Right" Width="4%" />
                                             <ItemStyle HorizontalAlign="Right" Width="4%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transport">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtTRANSPORTEdit" CssClass="form-control"  TabIndex="20" runat="server" Text='<%# Eval("TRANSPORT") %>'>0.00</asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTRANSPORT" runat="server" Text='<%# Eval("TRANSPORT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTRANSPORTFooter"  TabIndex="6" runat="server" CssClass="form-control" Width="100%">0.00</asp:TextBox>
                                            </FooterTemplate>
                                               <HeaderStyle HorizontalAlign="Right" Width="3%" />
                                             <ItemStyle HorizontalAlign="Right" Width="3%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stamp">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRSTAMPEdit" CssClass="form-control"  TabIndex="21" runat="server" Text='<%# Eval("RSTAMP") %>'>0.00</asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRSTAMP" runat="server" Text='<%# Eval("RSTAMP") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtRSTAMPFooter"  TabIndex="7" runat="server" CssClass="form-control" Width="100%">0.00</asp:TextBox>
                                            </FooterTemplate>
                                               <HeaderStyle HorizontalAlign="Right" Width="3%" />
                                             <ItemStyle HorizontalAlign="Right" Width="3%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PF. Rate">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPFRATEEdit" CssClass="form-control" TabIndex="22"  runat="server" Text='<%# Eval("PFRATE") %>'>0.00</asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPFRATE" runat="server" Text='<%# Eval("PFRATE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPFRATEFooter" runat="server"  TabIndex="8" CssClass="form-control" Width="100%">0.00</asp:TextBox>
                                            </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Right" Width="3%" />
                                             <ItemStyle HorizontalAlign="Right" Width="3%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PF From">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPFEFDTEdit" ClientIDMode="Static"  TabIndex="23" CssClass="form-control" runat="server" Text='<%# Eval("PFEFDT") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPFEFDT" runat="server" Text='<%# Eval("PFEFDT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPFEFDTFooter" ClientIDMode="Static"  TabIndex="9" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="3%"  />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PF To">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPFETDTEdit" ClientIDMode="Static"  TabIndex="24" CssClass="form-control" runat="server" Text='<%# Eval("PFETDT") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPFETDT" runat="server" Text='<%# Eval("PFETDT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPFETDTFooter" ClientIDMode="Static"  TabIndex="10" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                            </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Right" Width="3%" />
                                             <ItemStyle HorizontalAlign="Right" Width="3%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job From">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtJOBEFDTEdit" ClientIDMode="Static"  TabIndex="25" runat="server" CssClass="form-control" Text='<%# Eval("JOBEFDT") %>' OnTextChanged="txtJOBEFDTEdit_TextChanged"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblJOBEFDT" runat="server" Text='<%# Eval("JOBEFDT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtJOBEFDTFooter" ClientIDMode="Static"  TabIndex="11" runat="server" CssClass="form-control" Width="100%" OnTextChanged="txtJOBEFDTFooter_TextChanged"></asp:TextBox>
                                            </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Right" Width="4%" />
                                             <ItemStyle HorizontalAlign="Right" Width="4%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job To">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtJOBETDTEdit" ClientIDMode="Static"  TabIndex="26" CssClass="form-control" runat="server" Text='<%# Eval("JOBETDT") %>' OnTextChanged="txtJOBETDTEdit_TextChanged"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblJOBETDT" runat="server" Text='<%# Eval("JOBETDT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtJOBETDTFooter" ClientIDMode="Static"  TabIndex="12" AutoPostBack="true" runat="server" CssClass="form-control" Width="100%" OnTextChanged="txtJOBETDTFooter_TextChanged"></asp:TextBox>
                                            </FooterTemplate>
                                             <HeaderStyle HorizontalAlign="Right" Width="4%" />
                                             <ItemStyle HorizontalAlign="Right" Width="4%"/>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="imgbtnPUpdate" runat="server"  TabIndex="27" CommandName="Update" CssClass="txtColor" Height="20px" ImageUrl="~/Images/update.png"  ToolTip="Update" Width="20px" />
                                                <asp:ImageButton ID="imgbtnPCancel" runat="server"  TabIndex="28" CommandName="Cancel" Height="20px" ImageUrl="~/Image/Cencel.png"  ToolTip="Cancel" Width="20px" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="imgbtnPAdd" runat="server"  TabIndex="13" CommandName="Add" CssClass="txtColor" Height="25px" ImageUrl="~/Images/AddNewitem.jpg" ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="25px" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit.png" ToolTip="Edit" Width="20px" />
                                                <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/Delete.png" OnClientClick="return confMSG()" ToolTip="Delete" Width="20px" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="3%" />
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#006699" ForeColor="White" />
                                </asp:GridView>

                            </td>
                        </tr>

                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
