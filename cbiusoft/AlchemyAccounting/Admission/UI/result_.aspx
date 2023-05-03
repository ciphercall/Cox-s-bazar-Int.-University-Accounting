<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="result_.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.result_" %>
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
                                        <asp:Button ID="btnSearch" CssClass="form-control" runat="server" Height="32px" Text="Search" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnInsertToResult" runat="server" Text="INSERT" Width="148px" CssClass="form-control-right" Height="32px" OnClick="btnInsertToResult_Click" BackColor="#CCCCCC" Font-Bold="True" ForeColor="#CC0000" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    <td class="auto-style3" width="3%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" width="2%">&nbsp;</td>
                                    <td class="auto-style3" colspan="4">
                                        <div style="padding:6px">
                                        <asp:GridView ID="gv_Result" CssClass="Grid" runat="server" EmptyDataText="No Data Found" AutoGenerateColumns="False" Width="100%" OnRowCommand="gv_Result_RowCommand" OnRowCancelingEdit="gv_Result_RowCancelingEdit" OnRowEditing="gv_Result_RowEditing" OnRowUpdating="gv_Result_RowUpdating" OnRowDeleting="gv_Result_RowDeleting">
                                            <Columns>
                                               <asp:TemplateField HeaderText="SL"> 
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTUDENTIDNew" runat="server" Text='<%# Eval("NEWSTUDENTID") %>'></asp:Label>
                                                    </ItemTemplate> 
                                                    <ItemStyle  Width="2%"/>
                                                    <HeaderStyle Width="2%"/>
                                                </asp:TemplateField> 
                                                 <asp:TemplateField HeaderText="Student ID">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtSTUDENTIDNewEdit"   Enabled="false" TabIndex="10" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("NEWSTUDENTID") %>' ></asp:TextBox>
                                                         </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTUDENTIDNew" runat="server" Text='<%# Eval("NEWSTUDENTID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtSTUDENTIDFooter" TabIndex="1" runat="server" CssClass="form-control" Width="100%" ></asp:TextBox>
                                                      </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="12%"/>
                                                    <HeaderStyle HorizontalAlign="Center" Width="12%"/>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="Student ID" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtSTUDENTIDEdit" AutoPostBack="true" Enabled="false" TabIndex="10" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("STUDENTID") %>' OnTextChanged="txtSTUDENTIDEdit_TextChanged"></asp:TextBox>
                                                       <asp:AutoCompleteExtender ID="STUDENTIDEdit_AutoCompleteExtender"  CompletionListCssClass="AutoColor"  runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentD" TargetControlID="txtSTUDENTIDEdit" UseContextKey="True">
                                                        </asp:AutoCompleteExtender>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTUDENTID" runat="server" Text='<%# Eval("STUDENTID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtSTUDENTIDFooter" TabIndex="1" AutoPostBack="true" runat="server" CssClass="form-control" Width="100%" OnTextChanged="txtSTUDENTIDFooter_TextChanged"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="STUDENTID_AutoCompleteExtender"   CompletionListCssClass="AutoColor" runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentD" TargetControlID="txtSTUDENTIDFooter" UseContextKey="True">
                                                        </asp:AutoCompleteExtender>
                                                    </FooterTemplate> 
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student Name"> 
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtSTUDENTNMEdit"  Enabled="false"  CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("STUDENTNM") %>'></asp:TextBox>
                                                    </EditItemTemplate>                 
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTUDENTNM" runat="server" Text='<%# Eval("STUDENTNM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtSTUDENTNMFooter"  Enabled="false"  runat="server" CssClass="form-control" Width="100%" ></asp:TextBox>                 
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="25%"/>
                                                    <HeaderStyle Width="25%"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Attend<br/>(10)">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtAttendEdit" CssClass="form-control" TabIndex="11" Width="100%" runat="server" Text='<%# Eval("M40") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAttend" runat="server" Text='<%# Eval("M40") %>'></asp:Label>
                                                    </ItemTemplate> 
                                                    <ItemStyle  Width="8%"/>
                                                    <HeaderStyle Width="8%"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="C.Test<br/>(10)">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCTestEdit" CssClass="form-control" TabIndex="11" Width="100%" runat="server" Text='<%# Eval("M40") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCTest" runat="server" Text='<%# Eval("M40") %>'></asp:Label>
                                                    </ItemTemplate> 
                                                    <ItemStyle  Width="8%"/>
                                                    <HeaderStyle Width="8%"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mid Term<br/>(10)">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtMidTermEdit" CssClass="form-control" TabIndex="11" Width="100%" runat="server" Text='<%# Eval("M40") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMidTerm" runat="server" Text='<%# Eval("M40") %>'></asp:Label>
                                                    </ItemTemplate> 
                                                    <ItemStyle  Width="8%"/>
                                                    <HeaderStyle Width="8%"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assignment<br/>(10)">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtAssignmentEdit" CssClass="form-control" TabIndex="11" Width="100%" runat="server" Text='<%# Eval("M40") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAssignment" runat="server" Text='<%# Eval("M40") %>'></asp:Label>
                                                    </ItemTemplate> 
                                                    <ItemStyle  Width="8%"/>
                                                    <HeaderStyle Width="8%"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="I/C<br/>(50)">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtICEdit" CssClass="form-control" TabIndex="12" Width="100%" runat="server" Text='<%# Eval("M60") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIC" runat="server" Text='<%# Eval("M60") %>'></asp:Label>
                                                    </ItemTemplate> 
                                                    <ItemStyle  Width="8%"/>
                                                    <HeaderStyle Width="8%"/>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="C. Final<br/>(50)">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtFinalEdit" CssClass="form-control" TabIndex="12" Width="100%" runat="server" Text='<%# Eval("M60") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFinal" runat="server" Text='<%# Eval("M60") %>'></asp:Label>
                                                    </ItemTemplate> 
                                                    <ItemStyle  Width="8%"/>
                                                    <HeaderStyle Width="8%"/>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="Total">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtTotalEdit" CssClass="form-control" TabIndex="13" Width="100%" runat="server" Text='<%# Eval("REMARKS") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                                    </ItemTemplate> 
                                                    <ItemStyle  Width="8%"/>
                                                    <HeaderStyle Width="8%"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ID="imgbtnPUpdate" CssClass="txtColor" runat="server" CommandName="Update" Height="20px"
                                                            ImageUrl="~/Images/update.PNG" TabIndex="14" ToolTip="Update" Width="20px" />
                                                        <asp:ImageButton
                                                            ID="imgbtnPCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.PNG"
                                                            TabIndex="15" ToolTip="Cancel" Width="20px" />
                                                    </EditItemTemplate>

                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px"
                                                            ImageUrl="~/Images/Edit.PNG" TabIndex="10" ToolTip="Edit" Width="20px" /> 
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton ID="imgbtnPAdd" runat="server" CommandName="SaveCon" CssClass="txtColor"
                                                            Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="5" ToolTip="Save &amp; Continue"
                                                            ValidationGroup="validaiton" Width="30px" />
                                                    </FooterTemplate>
                                                    <ItemStyle  Width="3%" HorizontalAlign="Center"/>
                                                    <HeaderStyle Width="3%"/>

                                                    <ItemStyle Width="3%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#2AABD2" BorderColor="Black" BorderWidth="2px" Font-Size="9pt" ForeColor="White" />
                                        </asp:GridView></div>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
