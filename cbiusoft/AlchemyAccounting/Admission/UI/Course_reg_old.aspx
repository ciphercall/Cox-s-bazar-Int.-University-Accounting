<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Course_reg_old.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.Course_reg1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
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

        .auto-style7 {
            width: 100%;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="Script1" runat="server"></asp:ScriptManager>
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
                            <asp:Button ID="btnNext" runat="server" BackColor="White" BorderColor="#2aabd2" BorderWidth="2px" CssClass="form-control" Font-Bold="True" Font-Size="10pt" Height="40px" OnClick="btnNext_Click" Text="Next " Visible="False" Width="120px" />
                        </td>
                        <td class="text-left" width="5%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Registration Year : </td>
                        <td>
                            <asp:DropDownList ID="ddlYr" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlYr_SelectedIndexChanged" Width="50%" TabIndex="1" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static" CssClass="form-control" TabIndex="4" Width="50%"></asp:TextBox>
                        </td>
                        <td class="auto-style2">Semester Name : </td>
                        <td width="30%">
                            <asp:DropDownList ID="ddlSemNM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSemNM_SelectedIndexChanged" Width="100%" TabIndex="2" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td width="5%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Program Name : </td>
                        <td width="30%">
                            <asp:DropDownList ID="ddlProgNM" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="100%" TabIndex="3" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtProgramNM" runat="server" AutoPostBack="True" CssClass="form-control" MaxLength="11" TabIndex="4" Visible="False" Width="100%" Enabled="False"></asp:TextBox>

                        </td>
                        <td class="auto-style2">Student ID : </td>
                        <td>
                            <asp:TextBox ID="txtStuID" runat="server" AutoPostBack="True" CssClass="form-control" OnTextChanged="txtStuID_TextChanged" TabIndex="4" Width="100%" MaxLength="11"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtStuID_AutoCompleteExtender" CompletionListCssClass="AutoColor" runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentID" TargetControlID="txtStuID" UseContextKey="True">
                            </asp:AutoCompleteExtender>

                            <asp:TextBox ID="txtStuIDEdit" runat="server" AutoPostBack="True" CssClass="form-control" MaxLength="11" TabIndex="4" Visible="False" Width="100%" OnTextChanged="txtStuIDEdit_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtStuIDEdit_AutoCompleteExtender" CompletionListCssClass="AutoColor" runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentIDEdit" TargetControlID="txtStuIDEdit" UseContextKey="True">
                            </asp:AutoCompleteExtender>

                        </td>
                        <td width="5%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">SESSION :</td>
                        <td>
                            <asp:TextBox ID="txtSSN" runat="server" CssClass="form-control" TabIndex="5" Width="100%" MaxLength="10"></asp:TextBox>
                        </td>
                        <td class="auto-style2">BATCH : </td>
                        <td>
                            <asp:TextBox ID="txtBtch" runat="server" CssClass="form-control" Width="100%" TabIndex="6" MaxLength="20"></asp:TextBox>
                        </td>
                        <td width="5%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Remarks : </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtRmrk" runat="server" CssClass="form-control" Width="100%" TabIndex="7" MaxLength="100"></asp:TextBox>
                        </td>
                        <td width="5%">&nbsp;</td>
                    </tr>
                </table>
                <div style=" width:100%">
                    <asp:Label ID="lblPreview" runat="server" Text="Preview :" Visible="False"></asp:Label> 
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Label ID="lblPreviewTXT" runat="server" Text="which data already inserted" Visible="False" style="color: #CCCCCC"></asp:Label> 
                    &nbsp;&nbsp;  
                    <asp:GridView ID="gv_Preview" runat="server" Width="100%" CssClass="" AutoGenerateColumns="False" Font-Size="9pt">
                        <Columns>
                            <asp:BoundField DataField="SEM" HeaderText="Semester">
                                <HeaderStyle Width="6%" />
                                <ItemStyle Width="6%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COURSECD" HeaderText="Course ID">
                                <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                             <asp:BoundField DataField="COURSENM" HeaderText="Course Name">
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREDITHH" HeaderText="Credit Hr">
                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CRCOST" HeaderText="Credit Cost">
                                <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                <ItemStyle HorizontalAlign="Right" Width="8%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="REMARKS" HeaderText="Remarks">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle Font-Size="8pt" />
                    </asp:GridView>
                </div>
                <div style="padding: 15px">
                    <div>
                        <table class="auto-style7">
                            <tr>
                                <td>
                                    <div style="width:100%">
                                        <asp:Label ID="lbl1" runat="server" Font-Bold="True" Text="Course Registered :&nbsp;" Visible="False"></asp:Label>
                                        &nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ID="imgbtnPUpdate" CssClass="txtColor" runat="server" CommandName="Update" Height="20px"
                                                            ImageUrl="~/Images/update.PNG" TabIndex="67" ToolTip="Update" Width="20px" />
                                                        <asp:ImageButton
                                                            ID="imgbtnPCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.PNG"
                                                            TabIndex="68" ToolTip="Cancel" Width="20px" />
                                                    </EditItemTemplate>

                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px"
                                                            ImageUrl="~/Images/Edit.PNG" TabIndex="10" ToolTip="Edit" Width="20px" /><%--<asp:ImageButton
                                                                ID="imgbtnPDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()"
                                                                Height="20px" ImageUrl="~/Images/delete.PNG" TabIndex="11" ToolTip="Delete" Width="20px" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="6%" />

                                                    <ItemStyle Width="6%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Semester">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblSEMID" Visible="false" runat="server" Text='<%# Eval("SEM") %>'></asp:Label>

                                                        <asp:DropDownList ID="ddlSemisterNMEdit" CssClass="form-control" Width="100%" runat="server" AutoPostBack="True">
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
                                                    </EditItemTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSEMID" runat="server" Text='<%# Eval("SEM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="4%" />
                                                    <ItemStyle CssClass="text-center" Width="4%" Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Course ID">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCOURSEIDEdit" AutoPostBack="true" runat="server" Text='<%# Eval("COURSECD") %>' OnTextChanged="txtCOURSEIDEdit_TextChanged" CssClass="form-control" Width="100%"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoComplete" runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionCourseID" TargetControlID="txtCOURSEIDEdit" UseContextKey="True">
                                                        </asp:AutoCompleteExtender>
                                                    </EditItemTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCOURSEID" runat="server" Text='<%# Eval("COURSECD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="4%" />
                                                    <ItemStyle CssClass="text-center" Width="4%" Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit Hr.">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCREDITHHEdit" CssClass="form-control" Width="100%" Enabled="false" runat="server" Text='<%# Eval("CREDITHH") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREDITHH" runat="server" Text='<%# Eval("CREDITHH") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle CssClass="text-center" Width="5%" Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit Cost">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCRCOSTEdit" CssClass="form-control" Width="100%" Enabled="false" runat="server" Text='<%# Eval("CRCOST") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCRCOST" runat="server" Text='<%# Eval("CRCOST") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="5%" />
                                                    <ItemStyle CssClass="text-center" Width="5%" Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtREMARKSEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("REMARKS") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="12%" />
                                                    <ItemStyle CssClass="text-center" Width="12%" Wrap="True" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#3399FF" />
                                            <HeaderStyle BackColor="#2AABD2" ForeColor="White" />
                                        </asp:GridView>
                                        <div>

                                            <asp:Label ID="lblMSGCourse" runat="server" ForeColor="Red" Visible="False"></asp:Label>

                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    &nbsp;<asp:Label ID="lbl2" runat="server" Font-Bold="True" Text="Course Registered :&nbsp;"></asp:Label>
                    <asp:GridView ID="GridViewEQ" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="GridViewEQ_RowCommand" OnRowDeleting="GridViewEQ_RowDeleting">
                        <Columns>
                            <%--  <asp:TemplateField>
                                        <ItemTemplate>
                                             <asp:Button runat="server" CommandName="Change" ID="btnEdit" Text="Edit" CommandArgument='<%# Container.DataItemIndex %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btn" Height="30px" runat="server" BorderColor="Black" BorderWidth="2px" CommandArgument="<%# Container.DataItemIndex %>" CommandName="delete" CssClass="form-control" Text="Delete" ValidationGroup="Delete" Width="100%" />
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                                <ItemStyle Width="4%" />
                                <%-- <FooterTemplate>
            <asp:TextBox ID="txtnm" runat="server"></asp:TextBox>
            </FooterTemplate>--%><%--   <ItemTemplate>
                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" />
            </ItemTemplate>--%>
                                <ItemStyle Width="6%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="1" HeaderText="Semester" ItemStyle-CssClass="text-center" ItemStyle-Wrap="true">
                                <HeaderStyle Width="4%" />
                                <ItemStyle CssClass="text-center" Width="4%" Wrap="True" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="2" HeaderText="Course ID" ItemStyle-CssClass="text-center" ItemStyle-Wrap="true">
                                <HeaderStyle Width="4%" />
                                <ItemStyle CssClass="text-center" Width="4%" Wrap="True" />
                            </asp:BoundField>
                            <asp:BoundField DataField="3" HeaderText="Credit Hr." ItemStyle-CssClass="text-center" ItemStyle-Wrap="true">
                                <HeaderStyle Width="5%" />
                                <ItemStyle CssClass="text-center" Width="5%" Wrap="True" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="4" HeaderText="Credit Cost" ItemStyle-CssClass="text-center" ItemStyle-Wrap="true">
                                <HeaderStyle Width="5%" />
                                <ItemStyle CssClass="text-center" Width="5%" Wrap="True" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="5" HeaderText="Remarks" ItemStyle-CssClass="text-center" ItemStyle-Wrap="true">
                                <HeaderStyle Width="9%" />
                                <ItemStyle CssClass="text-center" Width="9%" Wrap="True" />
                            </asp:BoundField>

                        </Columns>
                        <EditRowStyle BackColor="White" BorderColor="#CCCCCC" BorderWidth="2px" />
                        <%--<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />--%>
                        <HeaderStyle BackColor="#2aabd2" BorderColor="Black" Font-Size="12px" Font-Underline="true" ForeColor="White" HorizontalAlign="Center" Wrap="True" BorderWidth="2px" Height="20px" />
                        <PagerStyle BackColor="#ffffff" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" BorderColor="#999999" BorderWidth="2px" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                    <div>

                        <asp:Label ID="lblWrng" runat="server" ForeColor="Red"></asp:Label>

                    </div>
                    <br />
                    <div>
                        <table style="width: 100%">
                            <tr>
                                <td width="7%">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" Width="100%" CssClass="form-control" OnClick="btnAdd_Click" Height="100%" ValidationGroup="Add" BorderColor="Black" BorderWidth="2px" TabIndex="12" />
                                    <asp:Button ID="btnEditAdd" runat="server" Text="Add" Width="100%" CssClass="form-control" Height="100%" ValidationGroup="Add" BorderColor="#3399FF" BorderWidth="2px" OnClick="btnEditAdd_Click" Visible="False" />

                                </td>
                                <td width="6%" height="35">
                                    <asp:DropDownList ID="ddlSemisterNM" CssClass="form-control" Width="100%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSemisterNM_SelectedIndexChanged" Height="35px">
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
                                    <asp:TextBox ID="txtSemID" runat="server" Width="50px" Visible="False"></asp:TextBox>
                                </td>
                                <td width="6%">
                                    <asp:TextBox ID="txtCrsID" runat="server" AutoPostBack="True" CssClass="form-control" Height="100%" MaxLength="4" OnTextChanged="txtCrsID_TextChanged" Placeholder="Course ID" TabIndex="8" Width="100%"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtCrsID_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionListCssClass="AutoColor" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionCourseID" TargetControlID="txtCrsID" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                    <asp:TextBox ID="txtCourseID" runat="server" Width="50px" Visible="False"></asp:TextBox>
                                </td>
                                <td width="6%">
                                    <asp:TextBox ID="txtCrditHr" runat="server" Width="100%" Placeholder="Credit Hr." CssClass="form-control" Height="100%" OnTextChanged="txtCrditHr_TextChanged" TabIndex="9" Enabled="False"></asp:TextBox>
                                </td>
                                <td width="7%">
                                    <asp:TextBox ID="txtCrditCst" runat="server" Width="100%" Placeholder="Credit Cost" CssClass="form-control" Height="100%" Enabled="False" TabIndex="10" OnTextChanged="txtCrditCst_TextChanged"></asp:TextBox>
                                </td>
                                <td width="10%">
                                    <asp:TextBox ID="txtRemark" runat="server" Width="100%" Placeholder="Remarks" CssClass="form-control" Height="100%" TabIndex="11" OnTextChanged="txtRemark_TextChanged" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div style="float: right; width: 100%;">
                            <br />
                            <asp:Button ID="btnInsert" runat="server" Text="Save" CssClass="form-control-right" BackColor="White" BorderColor="#2aabd2" BorderWidth="2px" Height="40px" Width="80px" OnClick="btnInsert_Click" Font-Bold="True" Font-Size="10pt" />

                            <asp:Button ID="btnPrint" runat="server" Text="Save &amp; Print" CssClass="form-control-right" BackColor="White" BorderColor="#2aabd2" BorderWidth="2px" Height="40px" Width="150px" Font-Bold="True" OnClick="btnPrint_Click" Font-Size="9pt" />

                            <asp:Button ID="btnDLT" runat="server" BackColor="White" BorderColor="Black" BorderWidth="2px" CssClass="form-control-right" Font-Bold="True" OnClientClick="return confMSG()" Font-Size="10pt" Height="40px" Text="Delete" Visible="False" Width="80px" OnClick="btnDLT_Click" />

                            <asp:Button ID="btnPrintEdit" runat="server" BackColor="White" BorderColor="Black" BorderWidth="2px" CssClass="form-control-right" Font-Bold="True" Font-Size="10pt" Height="40px" Text="Print" Width="80px" OnClick="btnPrintEdit_Click" Visible="False" />

                        </div>
                    </div>
                </div>
                <table class="auto-style7">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
