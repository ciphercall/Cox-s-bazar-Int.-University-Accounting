<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admission.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.Admission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            $("#txtExmDT,#txtDtOfBth,#txtMRDT").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+5" });
        }
        //function pageLoad() {
        //    $("#txtDtOfBth").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+1" });
        //}
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
            width: 100%;
            color:black;
        }

        .auto-style2 {
            color: #fff;
            font-size: xx-large;
        }

        .auto-style3 {
            font-size: larger;
        }

        .auto-style5 {
           
        }

        .auto-style6 {        text-align: right;
    }

        .auto-style7 {
            width: 241px;
            margin-top:15px
        }

        .auto-style8 {
            width: 142px;
        }
        .auto-style9 {
            height: 20px;
        }
        .auto-style10 {
            height: 20px;
            width: 506px;
            text-align: center;
        }
        .auto-style11 {
        }
        .auto-style12 {
            height: 20px;
            text-align: center;
        }
        .auto-style13 {
        width: 162px;
        text-align: right;
    }
        .auto-style14 {
            width: 162px;
            text-align: right;
        }
        .auto-style15 {
            width: 241px;
            text-align: right;
        }
        .auto-style16 {
            width: 162px;
            text-align: left;
        }
        .auto-style17 {
            color: red;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div style="border: double; border-width: 2px; border-radius: 10px">
                <div style="border: 2px double white; border-top-right-radius: 10px; border-top-left-radius: 10px; text-align: center; background-color: #2aabd2;" class="auto-style2"><span class="auto-style3">A</span>pplication <span class="auto-style3">F</span>orm <span class="auto-style3">F</span>or <span class="auto-style3">A</span>dmission</div>
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style16" width="25%">&nbsp;</td>
                        <td class="auto-style5" colspan="3" width="35%">
                            <asp:Label ID="lblSemID" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblProID" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblFrmNo" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSLGRD" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="text-left">
                            <asp:Label ID="lblMSG1" runat="server" Font-Bold="True" Font-Size="9pt" ForeColor="#33CC33" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style7">
                            &nbsp;</td>
                        <td class="auto-style7" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style14" width="25%">
                            <asp:Label ID="lblMRNAME" runat="server" style="text-align: right; color: #000000;" Text="M.R No  :" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style5" colspan="3">
                            <asp:DropDownList ID="ddlMRNO" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlMRNO_SelectedIndexChanged" Visible="False" Width="100px" TabIndex="1">
                                <asp:ListItem>Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style6">
                            <asp:Label ID="lblRoll" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style7">
                            <asp:Button ID="btnEdit" runat="server" BackColor="#2aabd2" BorderColor="Black" BorderWidth="3px" CssClass="form-control" Font-Bold="True" ForeColor="White" Height="100%" OnClick="btnEdit_Click" Text="EDIT" Width="100%" />
                        </td>
                        <td class="auto-style7" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Admission Year <span class="auto-style17">*</span>:</td>
                        <td class="auto-style8">
                            <asp:DropDownList ID="ddlExmYr" runat="server" CausesValidation="true" Width="100%" CssClass="form-control" TabIndex="2">
                            </asp:DropDownList>
                            <asp:Label ID="lblYR" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="text-right" width="10%">
                            
                            <asp:TextBox ID="txtFormNO" runat="server" CssClass="form-control" Enabled="False" Visible="False" Width="28%" Height="16px"></asp:TextBox>
                            
                        </td>
                        <td class="auto-style5">
                            &nbsp;</td>
                        <td class="auto-style6">
                            Semester Name <span class="auto-style17">*</span>:</td>
                        <td class="auto-style6">
                            <asp:DropDownList ID="ddlSemisNM" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSemisNM_SelectedIndexChanged" Width="100%" TabIndex="3">
                            </asp:DropDownList>
                            <asp:Label ID="lblSemNM" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style6" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Program Name <span class="auto-style17">*</span>:</td>
                        <td class="auto-style5" colspan="3">
                            <asp:DropDownList ID="ddlProgramNM" runat="server" CssClass="form-control" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlProgramNM_SelectedIndexChanged" TabIndex="4">
                            </asp:DropDownList>
                            <asp:Label ID="lblProSNM" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style6">Admission Date :</td>
                        <td class="auto-style7">
                            <asp:TextBox ID="txtExmDT" runat="server" CssClass="form-control" ReadOnly="True" Width="100%"></asp:TextBox>
                        </td>
                        <td class="auto-style7" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Student Name <span class="auto-style17">*</span>:</td>
                        <td class="auto-style5" colspan="5">
                            <asp:TextBox ID="txtStuNM" runat="server" Width="100%" CssClass="form-control" OnTextChanged="txtStuNM_TextChanged" MaxLength="100" TabIndex="5"></asp:TextBox>
                            <asp:Label ID="lblProNM" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Father Name :</td>
                        <td class="auto-style5" colspan="5">
                            <asp:TextBox ID="txtStuFNM" runat="server" Width="100%" CssClass="form-control" MaxLength="100" TabIndex="6"></asp:TextBox>
                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Mother Name :</td>
                        <td class="auto-style5" colspan="5">
                            <asp:TextBox ID="txtStuMNM" runat="server" Width="100%" CssClass="form-control" MaxLength="100" TabIndex="7"></asp:TextBox>
                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Present Address :</td>
                        <td class="auto-style5" colspan="5">
                            <asp:TextBox ID="txtPreAdrs1" runat="server" Width="100%" CssClass="form-control" MaxLength="50" TabIndex="8"></asp:TextBox>
                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">&nbsp;</td>
                        <td class="auto-style5" colspan="5">
                            <asp:TextBox ID="txtPreAdrs2" runat="server" Width="100%" CssClass="form-control" MaxLength="20" TabIndex="9"></asp:TextBox>
                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Permanent Address :</td>
                        <td class="auto-style5" colspan="5">
                            <asp:TextBox ID="txtPerAdrs1" runat="server" Width="100%" CssClass="form-control" MaxLength="80" TabIndex="10"></asp:TextBox>
                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">&nbsp;</td>
                        <td class="auto-style5" colspan="5">
                            <asp:TextBox ID="txtPerAdrs2" runat="server" Width="100%" CssClass="form-control" MaxLength="20" TabIndex="11"></asp:TextBox>
                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Mobile Number :</td>
                        <td class="auto-style5" colspan="3">
                            <asp:TextBox ID="txtMblNO" runat="server" Width="95%" CssClass="form-control" MaxLength="11" TabIndex="12"></asp:TextBox>
                        </td>
                        <td class="auto-style6">E-mail :</td>
                        <td class="auto-style7">
                            <asp:TextBox ID="txtEML" runat="server" Width="100%" CssClass="form-control" MaxLength="50" TabIndex="13"></asp:TextBox>
                        </td>
                        <td class="auto-style7" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Nationality :</td>
                        <td class="auto-style5" colspan="3">
                            <asp:TextBox ID="txtNation" runat="server" Width="95%" CssClass="form-control" MaxLength="15" TabIndex="14">Bangladeshi</asp:TextBox>
                        </td>
                        <td class="auto-style6">Religion :</td>
                        <td class="auto-style7">
                            <asp:DropDownList ID="ddlReli" runat="server" Width="100%" CssClass="form-control" TabIndex="15">
                                <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem>Islam</asp:ListItem>
                        <asp:ListItem>Christian</asp:ListItem>
                        <asp:ListItem>Hinduism</asp:ListItem>
                        <asp:ListItem>Buddhism</asp:ListItem>
                        <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblrel" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style7" width="4%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Date Of Birth :</td>
                        <td class="auto-style5" colspan="3">
                            <asp:TextBox ID="txtDtOfBth" runat="server" Width="95%" ClientIDMode="Static" CssClass="form-control" OnTextChanged="txtDtOfBth_TextChanged" MaxLength="50" TabIndex="16"></asp:TextBox>
                        </td>
                        <td class="auto-style6">Gender :</td>
                        <td class="auto-style7">
                            <asp:DropDownList ID="ddlGndr" runat="server" Width="100%" CssClass="form-control" TabIndex="17">
                                <asp:ListItem Value="-1">Select</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblGNDR" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style7" width="4%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">&nbsp;</td>
                        <td class="auto-style5" colspan="3">&nbsp;</td>
                        <td class="auto-style6">&nbsp;</td>
                        <td class="auto-style7">&nbsp;</td>
                        <td class="auto-style7" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">
                            <asp:Label ID="lblSL" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style5" colspan="5">
                            <asp:GridView ID="GridEdit" runat="server" AutoGenerateColumns="False" Width="100%" ShowFooter="True" OnRowCommand="GridEdit_RowCommand" OnRowCancelingEdit="GridEdit_RowCancelingEdit" OnRowDeleting="GridEdit_RowDeleting" OnRowEditing="GridEdit_RowEditing" OnRowUpdating="GridEdit_RowUpdating" >
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblEXAMSLEdit" runat="server" Text='<%# Eval("EXAMSL") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEXAMSL" runat="server" Text='<%# Eval("EXAMSL") %>'></asp:Label>
                                        </ItemTemplate>
                                       <%-- <FooterTemplate>
                                             <asp:Label ID="Label1" runat="server" Text='<%# Eval("EXAMSL") %>'></asp:Label>
                                        </FooterTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Exam Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEXAMNMEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("EXAMNM") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEXAMNM" runat="server" Text='<%# Eval("EXAMNM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <asp:TextBox ID="txtEXAMNMFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Session">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtSESSIONEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("SESSION") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSESSION" runat="server" Text='<%# Eval("SESSION") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <asp:TextBox ID="txtSESSIONFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGROUPSUBEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("GROUPSUB") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblGROUPSUB" runat="server" Text='<%# Eval("GROUPSUB") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <asp:TextBox ID="txtGROUPSUBFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Board">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBOARDUNIEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("BOARDUNI") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBOARDUNI" runat="server" Text='<%# Eval("BOARDUNI") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <asp:TextBox ID="txtBOARDUNIFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pass Year">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPASSYYEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("PASSYY") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPASSYY" runat="server" Text='<%# Eval("PASSYY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <asp:TextBox ID="txtPASSYYFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GPA">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGPAMARKSEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("GPAMARKS") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblGPAMARKS" runat="server" Text='<%# Eval("GPAMARKS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <asp:TextBox ID="txtGPAMARKSFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Letter GRD">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDIVGRADEEdit" CssClass="form-control" Width="100%" runat="server" Text='<%# Eval("DIVGRADE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDIVGRADE" runat="server" Text='<%# Eval("DIVGRADE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                             <asp:TextBox ID="txtDIVGRADEFooter" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnPUpdate"  runat="server" CommandName="Update" Height="20px"
                                ImageUrl="~/Images/update.png" TabIndex="67" ToolTip="Update" Width="20px" />
                            <asp:ImageButton
                                ID="imgbtnPCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cencel.png"
                                TabIndex="68" ToolTip="Cancel" Width="20px" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:ImageButton ID="imgbtnPAdd"  runat="server" CommandName="Add" CssClass="txtColor"
                                Height="30px" ImageUrl="~/Images/AddNewitem.jpg" TabIndex="14" ToolTip="Insert"
                                ValidationGroup="validaiton" Width="30px" />
                           
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnPEdit" runat="server" CommandName="Edit" Height="20px"
                                ImageUrl="~/Images/Edit.png" TabIndex="10" ToolTip="Edit" Width="20px" /><asp:ImageButton
                                    ID="imgbtnPDelete" runat="server" CommandName="Delete" OnClientClick="return confMSG()"
                                    Height="20px" ImageUrl="~/Images/delete.png" TabIndex="11" ToolTip="Delete" Width="20px" />
                        </ItemTemplate>
                                        <HeaderStyle Width="8%" />
                                        <ItemStyle Width="8%" />
                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#CCCCCC" Font-Size="7pt" />
                            </asp:GridView>
                        </td>
                        <td class="auto-style7" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">&nbsp;</td>
                        <td class="auto-style5" colspan="5">
                            <asp:GridView ID="GridViewEQ" Width="100%"  runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewEQ_RowCommand" OnRowDeleting="GridViewEQ_RowDeleting">
                                <Columns>
                                    <asp:TemplateField>

                                        <ItemTemplate>
                                            <asp:Button ID="btn"  runat="server" Text="Delete" ValidationGroup="Delete" CssClass="form-control" Width="100%" CommandName="delete" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="7%" />
                                        <%-- <FooterTemplate>
            <asp:TextBox ID="txtnm" runat="server"></asp:TextBox>
            </FooterTemplate>--%>

                                        <%--   <ItemTemplate>
                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" />
            </ItemTemplate>--%>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField>
                                        <ItemTemplate>
                                             <asp:Button runat="server" CommandName="Change" ID="btnEdit" Text="Edit" CommandArgument='<%# Container.DataItemIndex %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField HeaderStyle-Width="18%" ItemStyle-CssClass="text-center" HeaderText="Exam Name" ItemStyle-Wrap="true" DataField="1" ItemStyle-Width="18%" >
                                    <HeaderStyle Width="18%" />
                                    <ItemStyle CssClass="text-center" Width="18%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Width="8%" ItemStyle-CssClass="text-center" HeaderText="Session" ItemStyle-Wrap="true" DataField="2" ItemStyle-Width="8%">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle CssClass="text-center" Width="8%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Width="10%" ItemStyle-CssClass="text-center" HeaderText="Group" ItemStyle-Wrap="true" DataField="3" ItemStyle-Width="10%">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle CssClass="text-center" Width="10%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Width="11%"  ItemStyle-CssClass="text-center" HeaderText="Board" ItemStyle-Wrap="true" DataField="4" ItemStyle-Width="11%">
                                    <HeaderStyle Width="11%" />
                                    <ItemStyle CssClass="text-center" Width="11%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Width="10%" ItemStyle-CssClass="text-center" HeaderText="Passed Year" ItemStyle-Wrap="true" DataField="5" ItemStyle-Width="10%">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle CssClass="text-center" Width="10%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Width="8%" ItemStyle-CssClass="text-center" HeaderText="GPA" ItemStyle-Wrap="true" DataField="6" ItemStyle-Width="8%" >
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle CssClass="text-center" Width="8%" Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderStyle-Width="10%" ItemStyle-CssClass="text-center" HeaderText="Letter GRD" ItemStyle-Wrap="true" DataField="7" ItemStyle-Width="10%">

                                    <HeaderStyle Width="10%" />
                                    <ItemStyle CssClass="text-center" Width="10%" Wrap="True" />
                                    </asp:BoundField>

                                </Columns>
                                <EditRowStyle BackColor="White" BorderColor="#CCCCCC" BorderWidth="2px" />
                                <%--<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />--%>
                                <HeaderStyle BackColor="#cccccc" Font-Size="10px" ForeColor="Black" Font-Underline="true" BorderColor="#999999" HorizontalAlign="Center" Wrap="True" />
                                <PagerStyle BackColor="#ffffff" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" BorderColor="#999999" BorderWidth="2px" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>

                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style13" width="25%">
                            <asp:Label ID="lblGRDCount" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblCOUNT" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style5" colspan="5">
                            <table class="nav-justified">
                                <tr>
                                    <td width="7%">
                                        <asp:Button ID="btnKeep" Width="100%" CssClass="form-control" runat="server" Text="Add" OnClick="btnKeep_Click" ValidationGroup="Keep" />
                                    </td>
                                    <td width="18%">
                                        <asp:TextBox ID="txtNOXM" Width="100%" CssClass="form-control" placeholder="Exam Name" runat="server" MaxLength="50" TabIndex="18"></asp:TextBox>
                                    </td>
                                    <td width="8%">
                                        <asp:TextBox ID="txtSSN" Width="100%" CssClass="form-control" placeholder="Session" runat="server" MaxLength="20" TabIndex="19"></asp:TextBox>
                                    </td>
                                    <td width="10%">
                                        <asp:TextBox ID="txtGNM" Width="100%" CssClass="form-control" placeholder="Group" runat="server" MaxLength="50" TabIndex="20"></asp:TextBox>
                                    </td>
                                    <td width="11%">
                                        <asp:TextBox ID="txtBRD" Width="100%" CssClass="form-control" placeholder="Board" runat="server" MaxLength="50" TabIndex="21"></asp:TextBox>
                                    </td>
                                    <td width="10%">
                                        <asp:TextBox ID="txtYOPASS" Width="100%" CssClass="form-control" placeholder="Passed Yr" runat="server" MaxLength="7" TabIndex="22"></asp:TextBox>
                                    </td>
                                    <td width="8%">
                                        <asp:TextBox ID="txtGPA" Width="100%" CssClass="form-control" placeholder="GPA" runat="server" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td width="10%">
                                        <asp:TextBox ID="txtLTRGRD" Width="100%" CssClass="form-control" placeholder="Letter Grads" runat="server" AutoPostBack="True" OnTextChanged="txtLTRGRD_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%"><strong>Local Guardian/Ref :</strong></td>
                        <td class="auto-style5" colspan="3">&nbsp;</td>
                        <td class="auto-style6">&nbsp;</td>
                        <td class="auto-style7">&nbsp;</td>
                        <td class="auto-style7" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Name :</td>
                        <td class="auto-style5" colspan="5">
                            <asp:TextBox ID="txtGurNM" runat="server" Width="100%" CssClass="form-control" MaxLength="100" TabIndex="25"></asp:TextBox>
                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Relation (with Student) :</td>
                        <td class="auto-style5" colspan="3">
                            <asp:TextBox ID="txtGurStuRel" runat="server" Width="95%" CssClass="form-control" MaxLength="50" TabIndex="26"></asp:TextBox>
                        </td>
                        <td class="auto-style6">Profession :</td>
                        <td class="auto-style7">
                            <asp:TextBox ID="txtGurProf" runat="server" Width="100%" CssClass="form-control" MaxLength="50" TabIndex="27"></asp:TextBox>
                        </td>
                        <td class="auto-style7" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Address :</td>
                        <td class="auto-style5" colspan="5">
                            <asp:TextBox ID="txtGurAdrs" runat="server" Width="100%" CssClass="form-control" MaxLength="100" TabIndex="28"></asp:TextBox>
                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">Phone/Mobile Number :</td>
                        <td class="auto-style5" colspan="3">
                            <asp:TextBox ID="txtGurPhnNO" runat="server" Width="95%" CssClass="form-control" MaxLength="11" TabIndex="29"></asp:TextBox>
                        </td>
                        <td class="auto-style6">E-mail :</td>
                        <td class="auto-style7">
                            <asp:TextBox ID="txtGurEML" runat="server" Width="100%" CssClass="form-control" MaxLength="50" TabIndex="30"></asp:TextBox>
                        </td>
                        <td class="auto-style7" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">&nbsp;</td>
                        <td class="auto-style5" colspan="5">
                            <table class="nav-justified">
                                <tr>
                                    <td class="auto-style12" width="20%">Date</td>
                                    <td class="auto-style12" width="20%">Year</td>
                                    <td class="auto-style12" width="25%">M.R No</td>
                                    <td class="auto-style10" width="30%">Amount</td>
                                    <td class="auto-style9"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtMRDT" runat="server" ClientIDMode="Static" CssClass="form-control" Width="100%" AutoPostBack="True" OnTextChanged="txtMRDT_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMRYR" runat="server" CssClass="form-control" Width="100%" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMRNO" runat="server" CssClass="form-control" Width="100%" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td class="auto-style11" colspan="2">
                                        <asp:TextBox ID="txtMRAMNT" runat="server" CssClass="form-control" Width="100%" MaxLength="16" TabIndex="31"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="auto-style5" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">
                           

                            Photos :</td>
                        <td class="text-left" colspan="3">
                            &nbsp;<asp:FileUpload ID="FileUpload1" runat="server"  CssClass="form-control" Width="100%" Height="100%" SkinID="Images" TabIndex="32" />


                            <asp:Label ID="lblMSG" runat="server" ForeColor="Red" Visible="False"></asp:Label>


                        </td >
                        <td class="auto-style6">
                            <br />
                            <asp:Button ID="btnDLT" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderWidth="2px" CssClass="form-control-right" Font-Bold="True" Font-Size="11pt" ForeColor="#CC0000" Height="100%" OnClick="btnDLT_Click" OnClientClick="return confMSG()" Text="Delete" Visible="False" Width="100px" />
                            <br />
                        </td>
                        <td class="auto-style7">
                            <br />
                            <asp:Button ID="btnPrint" runat="server" BackColor="#2aabd2" BorderColor="Black" BorderWidth="2px" CssClass="form-control" Font-Bold="True" Font-Size="11pt" ForeColor="White" Height="80%" OnClick="btnPrint_Click" Text="Print" Visible="False" Width="100px" />
                            <asp:Button ID="btnInsertAdm" runat="server" BackColor="#2aabd2" BorderColor="Black" BorderWidth="2px" CssClass="form-control-right" Font-Bold="True" Font-Size="11pt" ForeColor="White" Height="100%" OnClick="btnInsertAdm_Click" Text="Submit" Width="120px" TabIndex="33" />
                        </td>
                        <td class="auto-style7" width="4%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13" width="25%">
                            <asp:Label ID="lblMRNO" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td class="auto-style5" colspan="4">
                            <asp:Label ID="lblImagePath" runat="server" style="text-align: left; float:left" Width="100%"></asp:Label>
                        </td>
                        <td class="auto-style15">
                            <asp:CheckBox ID="chkPrint" runat="server" Text="Create Receipt ?" Width="130px" />
                        </td>
                        <td class="auto-style15" width="4%">&nbsp;</td>
                    </tr>
                </table>
            </div>
     
        </ContentTemplate>

         <Triggers>
                <asp:PostBackTrigger ControlID="btnInsertAdm"  />
                <%--<asp:AsyncPostBackTrigger ControlID="File" />--%>
            </Triggers>
    </asp:UpdatePanel>
  
</asp:Content>
