<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Results.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.Results" %>

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
    <script type="text/javascript">
        $(function () {
            $("[id*=gv_Result] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>
    <title>::Results Information</title>
    <style type="text/css">
        .auto-style1 {
            font-size: larger;
        }

        .auto-style2 {
            width: 100%;
        }

        .auto-style3 {
            text-align: right;
        }
         td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #A1DCF2;
        }
        .auto-style4 {
            text-align: right;
            height: 22px;
           
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div style="border: double; border-radius: 10px; border-width: 2px">
                <div style="border: 2px double white; height: 100%; border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: #2aabd2; color: #FFFFFF; font-size: xx-large; text-align: center;" class="auto-style4"><span class="auto-style1">R</span>esult<span class="auto-style1"> I</span>nformation</div>
                <table class="auto-style2">
                    <tr>
                        <td>
                            <div style="text-align: center">
                            <asp:Label ID="lblMSG" runat="server" style="text-align: center" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lblSemID" runat="server" Visible="False" ></asp:Label>
                                <asp:Label ID="lblProgID" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblCourseID" runat="server" Visible="False"></asp:Label>
                              
                            </div>
                            
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style4" width="2%">&nbsp;</td>
                                    <td class="auto-style4" width="16%">Registration Year :</td>
                                    <td class="auto-style4" width="30%">
                                        <asp:DropDownList ID="ddlYR" runat="server" CssClass="form-control"  Width="120px" OnSelectedIndexChanged="ddlYR_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style4" width="15%">Semester Name&nbsp; :</td>
                                    <td class="auto-style4" style=" margin-right:30px" width="30%">
                                        <asp:DropDownList ID="ddlSemNM" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSemNM_SelectedIndexChanged" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style4" width="3%"></td>
                                </tr>
                                <tr>
                                    <td width="2%">&nbsp;</td>
                                    <td class="auto-style3" width="16%">Program Name&nbsp; :</td>
                                    <td width="30%">
                                        <asp:DropDownList ID="ddlProgNM" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlProgNM_SelectedIndexChanged" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="15%" class="auto-style3">Course Name :</td>
                                    <td width="30%">
                                        <asp:DropDownList ID="ddlCourseNM" runat="server" AutoPostBack="True" CssClass="form-control"  Width="100%" OnSelectedIndexChanged="ddlCourseNM_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="3%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" width="2%">&nbsp;</td>
                                    <td class="auto-style3" width="16%">Semester ID :</td>
                                    <td class="auto-style3" width="30%">
                                        <asp:DropDownList ID="ddlSemID" CssClass="form-control" Width="70%"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSemID_SelectedIndexChanged">
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
                                    </td>
                                    <td class="text-left" width="15%">
                                        &nbsp;</td>
                                    <td class="text-right" width="30%">
                                        <asp:Button ID="btnSearch" CssClass="form-control-right" runat="server" Height="32px" Text="Search" OnClick="btnSearch_Click" BackColor="#FF9933" Font-Bold="True" ForeColor="White" />
                                        <asp:Button ID="btnInsertToResult" runat="server" Text="INSERT" Width="148px" CssClass="form-control-right" Height="32px" OnClick="btnInsertToResult_Click" BackColor="#CCCCCC" Font-Bold="True" ForeColor="#CC0000" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    <td class="auto-style3" width="3%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" width="2%">&nbsp;</td>
                                    <td class="auto-style3" colspan="4">
                                        <div style="padding:6px">
                                        </div>
                                    </td>
                                    <td class="auto-style3" width="3%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" width="2%">&nbsp;</td>
                                    <td class="auto-style3" colspan="4">
                                        <asp:GridView ID="GridView1" runat="server"  Visible="False" AutoGenerateColumns="False" Width="16px" ShowHeader="False">
                                            <Columns>
                                               <%-- <asp:TemplateField>
                                                    <ItemTemplate> 
                                                         <asp:Label ID="lblStuID" runat="server" Text='<%# Eval("STUDENTID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle  Width="100%"/>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField="STUDENTID"/>
                                                
                                            </Columns>
                                        </asp:GridView> 
                                    </td>
                                    <td class="auto-style3" width="3%">&nbsp;</td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gv_Result" runat="server" AutoGenerateColumns="False" CssClass="Grid" EmptyDataText="No Data Found" OnRowCancelingEdit="gv_Result_RowCancelingEdit" OnRowCommand="gv_Result_RowCommand" OnRowDeleting="gv_Result_RowDeleting" OnRowEditing="gv_Result_RowEditing" OnRowUpdating="gv_Result_RowUpdating" Width="100%">
                    <Columns>
                        <%--  <asp:TemplateField HeaderText="SEMESTER SL">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblSEMID" Visible="false" runat="server" Text='<%# Eval("SEM") %>' > </asp:Label>
                                                        <asp:DropDownList ID="ddlSemisterNMEdit" CssClass="form-control" Width="100%" runat="server" AutoPostBack="True">
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
                                                    </EditItemTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSEMID" runat="server" Text='<%# Eval("SEM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlSemisterNMFooter" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="True">
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
                                                    <HeaderStyle Width="10%" />
                                                    <ItemStyle CssClass="text-center" Width="10%" Wrap="True" />
                                                </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Student ID" Visible="false">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSTUDENTIDEdit" runat="server" AutoPostBack="true" CssClass="form-control" Enabled="false" OnTextChanged="txtSTUDENTIDEdit_TextChanged" TabIndex="10" Text='<%# Eval("STUDENTID") %>' Width="100%"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="STUDENTIDEdit_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionListCssClass="AutoColor" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentD" TargetControlID="txtSTUDENTIDEdit" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSTUDENTID" runat="server" Text='<%# Eval("STUDENTID") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtSTUDENTIDFooter" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtSTUDENTIDFooter_TextChanged" TabIndex="1" Width="100%"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="STUDENTID_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionListCssClass="AutoColor" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentD" TargetControlID="txtSTUDENTIDFooter" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                            </FooterTemplate>
                            <ItemStyle Width="12%" />
                            <HeaderStyle Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student ID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSTUDENTIDNewEdit" runat="server" CssClass="form-control" Enabled="false" TabIndex="10" Text='<%# Eval("NEWSTUDENTID") %>' Width="100%"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSTUDENTIDNew" runat="server" Text='<%# Eval("NEWSTUDENTID") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtSTUDENTIDFooter0" runat="server" CssClass="form-control" TabIndex="1" Width="100%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                            <HeaderStyle Width="12%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSTUDENTNMEdit" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("STUDENTNM") %>' Width="100%"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSTUDENTNM" runat="server" Text='<%# Eval("STUDENTNM") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtSTUDENTNMFooter" runat="server" CssClass="form-control" Enabled="false" Width="100%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="25%" />
                            <HeaderStyle Width="25%" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="CREDIT Hr">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCREDITHHEdit" AutoPostBack="true" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("CREDITHH") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtCREDITHH" runat="server" Text='<%# Eval("CREDITHH") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtCREDITHHFooter" AutoPostBack="true" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Marks-40">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtM40Edit" runat="server" CssClass="form-control" TabIndex="11" Text='<%# Eval("M40") %>' Width="100%"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblM40" runat="server" Text='<%# Eval("M40") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtM40Footer" runat="server" CssClass="form-control" TabIndex="2" Width="100%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                            <HeaderStyle Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marks-60">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtM60Edit" runat="server" CssClass="form-control" TabIndex="12" Text='<%# Eval("M60") %>' Width="100%"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblM60" runat="server" Text='<%# Eval("M60") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtM60Footer" runat="server" CssClass="form-control" TabIndex="3" Width="100%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                            <HeaderStyle Width="8%" />
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="CGPA" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCGPAEdit" CssClass="form-control" TabIndex="11" Width="100%" runat="server" Text='<%# Eval("CGPA") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCGPA" runat="server" Text='<%# Eval("CGPA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtCGPAFooter" runat="server" TabIndex="2" CssClass="form-control" Width="100%"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle  Width="10%"/>
                                                    <HeaderStyle Width="10%"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GRADE" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtGRADEEdit" CssClass="form-control" TabIndex="12" Width="100%" runat="server" Text='<%# Eval("GRADE") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRADE" runat="server" Text='<%# Eval("GRADE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtGRADEFooter" runat="server" TabIndex="3" CssClass="form-control" Width="100%"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle  Width="10%"/>
                                                    <HeaderStyle Width="10%"/>
                                                </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Remarks">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtREMARKSEdit" runat="server" CssClass="form-control" TabIndex="13" Text='<%# Eval("REMARKS") %>' Width="100%"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtREMARKSFooter" runat="server" CssClass="form-control" TabIndex="4" Width="100%"></asp:TextBox>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgbtnPUpdate" runat="server" CommandName="Update" CssClass="txtColor" Height="20px" ImageUrl="~/Images/update.PNG" TabIndex="14" ToolTip="Update" Width="20px" />
                                <asp:ImageButton ID="imgbtnPCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.PNG" TabIndex="15" ToolTip="Cancel" Width="20px" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit.PNG" TabIndex="10" ToolTip="Edit" Width="20px" />
                                <%--<asp:ImageButton
                                                                ID="imgbtnPDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()"
                                                                Height="20px" ImageUrl="~/Images/delete.PNG" TabIndex="11" ToolTip="Delete" Width="20px" />--%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ID="imgbtnPAdd" runat="server" CommandName="SaveCon" CssClass="txtColor" Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="5" ToolTip="Save &amp; Continue" ValidationGroup="validaiton" Width="30px" />
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                            <HeaderStyle Width="3%" />
                            <ItemStyle Width="3%" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#2AABD2" BorderColor="Black" BorderWidth="2px" Font-Size="9pt" ForeColor="White" />
                </asp:GridView>
            </div>
        </ContentTemplate> 
    </asp:UpdatePanel>
</asp:Content>
