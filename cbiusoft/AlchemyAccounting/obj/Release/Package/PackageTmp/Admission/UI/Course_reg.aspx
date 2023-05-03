<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Course_reg.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.Course_reg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad() {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-5:+10" });
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
            font-size: larger;
        }

        .auto-style2 {
            text-align: right;
            width: 16%;
        }

        .auto-style3 {
            text-align: right;
            width: 18%;
        }

        .auto-style4 {
            font-size: 16px;
        }

        .auto-style5 {
            text-align: left;
            width: 16%;
        }

        .auto-style6 {
            text-align: left;
            width: 18%;
        }

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div style="border: double; border-radius: 10px; border-width: 2px">
                <div style="border: 2px double white; border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: #2aabd2; color: #FFFFFF; font-size: xx-large; text-align: center;" class="auto-style4"><span class="auto-style1">C</span>ourse <span class="auto-style1">R</span>egistration <span class="auto-style1">F</span>orm</div>
                <table class="nav-justified">
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td class="text-left">&nbsp;</td>
                        <td class="auto-style5">&nbsp;</td>
                        <td width="30%" class="text-left">&nbsp;</td>
                        <td width="5%" class="text-left">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td class="text-left">
                            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                            <asp:Label ID="lblProgID" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSemID" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style5">
                            <asp:Label ID="lblMSG" runat="server" ForeColor="#339933" Style="text-align: left" Visible="False" Font-Size="9pt"></asp:Label>
                        </td>
                        <td class="text-left" width="30%">
                            <asp:Button ID="btnEdit" runat="server" BorderColor="#2aabd2" CssClass="form-control-right" Font-Bold="True" ForeColor="Black" Text="Edit" BorderWidth="2px" Font-Size="12pt" Height="100%" OnClick="btnEdit_Click" Width="120px" />
                        </td>
                        <td class="text-left" width="5%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Registration Year : </td>
                        <td>
                            <asp:DropDownList ID="ddlYr" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlYr_SelectedIndexChanged" Width="48%"  AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtDate" runat="server"  CssClass="form-control" Width="48%"></asp:TextBox>
                        </td>
                        <td class="auto-style2">Semester Name : </td>
                        <td width="30%">
                            <asp:DropDownList ID="ddlSemNM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSemNM_SelectedIndexChanged" Width="100%"  AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td width="5%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Program Name : </td>
                        <td width="30%">
                            <asp:DropDownList ID="ddlProgNM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProgNM_SelectedIndexChanged" Width="100%"   AutoPostBack="True">
                            </asp:DropDownList>

                        </td>
                        <td class="auto-style2">Student ID : </td>
                        <td>
                            <asp:TextBox ID="txtStuID" runat="server" CssClass="form-control"   Width="100%" MaxLength="11" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtStuIDNew" runat="server" AutoPostBack="True" CssClass="form-control" OnTextChanged="txtStuIDNew_TextChanged"  Width="100%" MaxLength="12"></asp:TextBox>
                     <%--       <asp:AutoCompleteExtender ID="txtStuID_AutoCompleteExtender" CompletionListCssClass="AutoColor" runat="server" 
                                CompletionInterval="10" 
                                CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" 
                                ServiceMethod="GetCompletionStudentID" TargetControlID="txtStuIDNew" UseContextKey="True">
                            </asp:AutoCompleteExtender>--%>
                            <asp:AutoCompleteExtender ID="txtStuID_AutoCompleteExtender"
                                runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=";, :"
                                Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentID" CompletionListCssClass="AutoExtender"
                                CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                TargetControlID="txtStuIDNew" UseContextKey="True">
                            </asp:AutoCompleteExtender>

                            <asp:DropDownList ID="ddlStudentEdit" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%" Visible="False" OnSelectedIndexChanged="ddlStudentEdit_SelectedIndexChanged">
                            </asp:DropDownList>

                            <asp:Label ID="lblStuNM" runat="server" CssClass="form-control" ForeColor="#999999" Font-Size="8pt"></asp:Label>

                        </td>
                        <td width="5%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">SESSION :</td>
                        <td>
                            <asp:TextBox ID="txtSSN" runat="server" CssClass="form-control" Width="100%" MaxLength="10"></asp:TextBox>
                        </td>
                        <td class="auto-style2">BATCH : </td>
                        <td>
                            <asp:TextBox ID="txtBtch" Enabled="false" runat="server" CssClass="form-control" Width="100%" MaxLength="20"></asp:TextBox>
                        </td>
                        <td width="5%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Remarks : </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtRmrk" runat="server" CssClass="form-control" Width="100%"  MaxLength="100"></asp:TextBox>
                        </td>
                        <td width="5%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td colspan="3">
                            <asp:Button ID="btnPrintEdit" runat="server" Height="35px" BackColor="White" BorderColor="#00CCFF" BorderWidth="2px" CssClass="form-control right" Font-Bold="True" Font-Size="10pt" OnClick="btnPrintEdit_Click" Text="Print" Visible="False" Width="80px" />
                            <asp:Button ID="BtnRefresh" runat="server" Height="35px"  BorderColor="#00CCFF" BorderWidth="2px" CssClass="form-control right" OnClick="BtnRefresh_Click" Text="Refresh"  Width="80px" />
                        </td>
                        <td width="5%">&nbsp;</td>
                    </tr>
                </table>
                <div style="width: 100%">
                    <asp:Label ID="lblMSGCourse" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    <br />
                    <asp:GridView ID="gv_CourseReg" runat="server" AutoGenerateColumns="false" OnRowCancelingEdit="gv_CourseReg_RowCancelingEdit" OnRowCommand="gv_CourseReg_RowCommand" OnRowDeleting="gv_CourseReg_RowDeleting" OnRowEditing="gv_CourseReg_RowEditing" OnRowUpdating="gv_CourseReg_RowUpdating" ShowFooter="True" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Semester">
                                <ItemTemplate>
                                    <asp:Label ID="lblSemID" runat="server" Text='<%# Eval("SEM") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlSemIDEdit" runat="server" AutoPostBack="true" Width="100%" CssClass="form-control" OnSelectedIndexChanged="ddlSemIDEdit_SelectedIndexChanged">
                                        <asp:ListItem Value="01">1st</asp:ListItem>
                                        <asp:ListItem Value="02">2nd</asp:ListItem>
                                        <asp:ListItem Value="03">3rd</asp:ListItem>
                                        <asp:ListItem Value="04">4th</asp:ListItem>
                                        <asp:ListItem Value="05">5th</asp:ListItem>
                                        <asp:ListItem Value="06">6th</asp:ListItem>
                                        <asp:ListItem Value="07">7th</asp:ListItem>
                                        <asp:ListItem Value="08">8th</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblSemIDEdit" runat="server" Visible="false" Text='<%# Eval("SEM") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlSemIDFooter" runat="server" AutoPostBack="true" Width="100%" CssClass="form-control" OnSelectedIndexChanged="ddlSemIDFooter_SelectedIndexChanged">
                                        <asp:ListItem Value="00">Select</asp:ListItem>
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
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course CD">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSECD") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlCourseEdit"  runat="server" AutoPostBack="true" Width="100%" CssClass="form-control" OnSelectedIndexChanged="ddlCourseEdit_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblCourseEdit" runat="server" Visible="false" Text='<%# Eval("COURSECD") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlCourseFooter" runat="server"  AutoPostBack="true" Width="100%" CssClass="form-control" OnSelectedIndexChanged="ddlCourseFooter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("COURSEID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblCourseIDEdit" runat="server" Text='<%# Eval("COURSEID") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblCourseIDFooter" runat="server" CssClass="form-control"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Course">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseNM" runat="server" Text='<%# Eval("COURSENM") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCourseNMEdit" runat="server" ReadOnly="true" Width="100%" CssClass="form-control" Text='<%# Eval("COURSENM") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCourseNMFooter" ReadOnly="true" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle Width="20%" />
                                <ItemStyle CssClass="text-center" Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit Hr.">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseHr" runat="server" Text='<%# Eval("CREDITHH") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCourseHrEdit" runat="server" ReadOnly="true" Width="100%" CssClass="form-control" Text='<%# Eval("CREDITHH") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCourseHrFooter" ReadOnly="true" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit Cost">
                                <ItemTemplate>
                                    <asp:Label ID="lblCrCost" runat="server" Text='<%# Eval("CRCOST") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCrCostEdit" runat="server" ReadOnly="true" Width="100%" CssClass="form-control" Text='<%# Eval("CRCOST") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCrCostFooter" runat="server" ReadOnly="true" Width="100%" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRemarksEdit" Width="100%" runat="server" CssClass="form-control" Text='<%# Eval("REMARKS") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtRemarksFooter" Width="100%" runat="server" CssClass="form-control"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderStyle Width="15%" />
                                <ItemStyle Width="15%" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnPUpdate" runat="server"   CommandName="Update" CssClass="txtColor" Height="20px" ImageUrl="~/Images/update.png" ToolTip="Update" Width="20px" />
                                    <asp:ImageButton ID="imgbtnPCancel" runat="server"  CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.png" ToolTip="Cancel" Width="20px" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="imgbtnPAdd" runat="server"  CommandName="Add" Font-Bold="true" Font-Size="Small" CssClass="form-control" Height="30px" Text="ADD" BorderColor="#3333cc" BorderWidth="2px" ToolTip="Insert" ValidationGroup="validaiton" Width="100%" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnPEdit" runat="server"    CommandName="Edit"  Height="20px" ImageUrl="~/Images/Edit.png"  ToolTip="Edit" Width="30px" />
                                    <asp:ImageButton ID="imgbtnPDelete" runat="server" CommandName="Delete" Height="20px" ImageUrl="~/Images/delete.png" OnClientClick="return confMSG()"  ToolTip="Delete" Width="20px" />
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                <FooterStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                           <%-- <asp:TemplateField>
                                <FooterTemplate>
                                </FooterTemplate>
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" Height="20px"  ToolTip="Edit" CssClass="form-control"/>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="3%"/>
                            </asp:TemplateField>--%>
                        </Columns>
                        <HeaderStyle BackColor="#CCCCCC" BorderColor="#333333" BorderWidth="2px" ForeColor="#333333" />
                    </asp:GridView>
                    <br />
                    <div>
                        <asp:Label ID="lblWrng" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger  ControlID="BtnRefresh"/>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
