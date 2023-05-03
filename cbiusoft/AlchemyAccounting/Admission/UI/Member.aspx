<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="AlchemyAccounting.Info.UI.Member" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/jquery-1.9.1.js"></script>
    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            $("#txtEmpDOB,#txtJoinDT,#txtCRDISUDT").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100+0" });
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
        .auto-style2 {
            text-align: right;
        }

        .auto-style3 {
            text-align: right;
            width: 12%;
        }

        .auto-style4 {
            text-align: left;
            width: 12%;
        }

        .auto-style5 {
            width: 28%;
        }

        .auto-style6 {
            text-align: left;
            text-decoration: underline;
        }

        .border {
            /*border-color: red;
            border: 1px solid red;*/
        }

        .auto-style7 {
            color: #FF0000;
            font-weight: 700;
            font-size: 15px;
        }

        .auto-style8 {
            width: 2%; 
        }

        .auto-style9 {
            text-align: right;
            width: 15%; 
        }

        .auto-style10 {
            width: 30%; 
        }

        .auto-style11 {
            text-align: right;
            width: 18%; 
        }

        .auto-style12 {
            width: 28%; 
        }

        .auto-style14 {
            width: 100%;
        }
        .auto-style15 {
            height: 22px;
            text-align: right;
        }
        .auto-style16 {
            height: 22px;
            text-align: right;
        }
        .auto-style17 {
            height: 22px;
            text-align: right;
            width: 30%;
        }
        .auto-style18 {
            text-align: right;
            width: 30%;
        }
        .auto-style19 {
            text-align: right;
            height: 52px;
        }
        .auto-style20 {
            height: 52px;
        }
        .auto-style21 {
            text-align: right;
            width: 30%;
            height: 52px;
        }
        .auto-style22 {
            height: 22px; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scrip" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            &nbsp;<table class="nav-justified" style="width: 100%">
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">&nbsp;</td>
                    <td colspan="2" style="width: 30%">
                        <asp:Label ID="lblMSG" Width="100%" CssClass="alert alert-success" runat="server" Font-Bold="True" ForeColor="#009933" Visible="False"></asp:Label>
                        
                            </td>
                    <td class="auto-style5">
                        <asp:Label ID="lblEmpID" runat="server" Visible="False"></asp:Label>
                        <asp:Button ID="btnEdit" runat="server" BackColor="#333333" BorderColor="Red" CssClass="form-control" Font-Bold="True" ForeColor="White" OnClick="btnEdit_Click" Text="Edit" Width="100px" />
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Department<span class="auto-style7">*</span> : &nbsp; </td>
                    <td style="width: 30%">
                        <%--<asp:AutoCompleteExtender ID="txtEmpNM_AutoCompleteExtender" runat="server"
                            DelimiterCharacters="" Enabled="True" TargetControlID="txtEmpNM"
                            MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                            UseContextKey="True" ServiceMethod="GetCompletionMemberNM">
                        </asp:AutoCompleteExtender>--%>
                        <asp:DropDownList ID="ddlDPT"  runat="server" AutoPostBack="True" CssClass="form-control border" OnSelectedIndexChanged="ddlDPT_SelectedIndexChanged" Width="100%">
                        </asp:DropDownList>
                        <asp:Label ID="lblDeptID" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblDeptSID" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td class="auto-style2" style="width: 30%">
                        <asp:Label ID="lblSL" Visible="false" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="auto-style5" style="width: 30%">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td style="width: 10%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Employee Name<span class="auto-style7">*</span> :</td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtEmpNM" runat="server" CssClass="form-control" MaxLength="100" OnTextChanged="txtEmpNM_TextChanged" Width="100%"></asp:TextBox>
                        <asp:DropDownList ID="ddlEmpEdit" runat="server" AutoPostBack="True" BackColor="#CCCCCC" CssClass="form-control border" Font-Bold="True" Font-Size="10pt" ForeColor="Maroon" OnSelectedIndexChanged="ddlEmpEdit_SelectedIndexChanged" Visible="false" Width="100%">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2" style="width: 30%">
                        <asp:TextBox ID="txtID" runat="server" AutoPostBack="True" CssClass="form-control border" Enabled="False" OnTextChanged="txtEmpNM_TextChanged" Width="150px"></asp:TextBox>
                       
                        Post Name <span class="auto-style7">*</span>:</td>
                    <td class="auto-style5" style="width: 30%">
                        <asp:DropDownList ID="ddlPostNM" runat="server" AutoPostBack="True" CssClass="form-control border" OnSelectedIndexChanged="ddlPostNM_SelectedIndexChanged" Width="100%">
                        </asp:DropDownList>
                        <asp:Label ID="lblPostID" runat="server"  Visible="false"></asp:Label>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                    <td style="width: 10%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Guardian Name : </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtEmpGNM" runat="server" CssClass="form-control" MaxLength="100" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style2" style="width: 30%">Mother Name : </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtEmpMNM" runat="server" CssClass="form-control" MaxLength="100" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Present Address&nbsp; : </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtEmpPreAddress" runat="server" CssClass="form-control" MaxLength="100" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Permanent Address&nbsp; : </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtEmpPerAddress" runat="server" CssClass="form-control" MaxLength="100" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Contact Number<span class="auto-style7">*</span> : </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtEmpCNO" runat="server" CssClass="form-control" MaxLength="11" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style3" style="width: 18%">Email : </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtEMPEmail" runat="server" CssClass="form-control" MaxLength="50" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Date Of Birth : </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtEmpDOB" ClientIDMode="Static" runat="server" CssClass="form-control" MaxLength="10" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style3" style="width: 18%">Gender<span class="auto-style7">*</span> : </td>
                    <td class="auto-style5">
                        <asp:DropDownList ID="ddlEmpGen" runat="server" CssClass="form-control" Width="100%">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>MALE</asp:ListItem>
                            <asp:ListItem>FEMALE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Voter ID No&nbsp; : </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtEmpVoterID" runat="server" CssClass="form-control" MaxLength="25" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style3" style="width: 18%">Blood Group : </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtEmpBld" runat="server" CssClass="form-control" MaxLength="5" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style6" colspan="2" style="width: 30%">Reference&nbsp; Information 1</td>
                    <td class="auto-style3" style="width: 18%">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Name : </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtRef1NM" runat="server" CssClass="form-control" MaxLength="50" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style3" style="width: 18%">Designation : </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtRef1Desig" runat="server" CssClass="form-control" MaxLength="30" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Address : </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtRef1Address" runat="server" CssClass="form-control" MaxLength="100" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Contact No : </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtRef1CNO" runat="server" CssClass="form-control" MaxLength="11" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style4" style="width: 18%">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style6" colspan="2" style="width: 30%">Reference&nbsp; Information 2</td>
                    <td class="auto-style4" style="width: 18%">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Name : </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtRef2NM" runat="server" CssClass="form-control" MaxLength="50" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style3" style="width: 18%">Designation : </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtRef2Desig" runat="server" CssClass="form-control" MaxLength="30" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Address : </td>
                    <td colspan="3" class="auto-style2">
                        <asp:TextBox ID="txtRef2Address" runat="server" CssClass="form-control" MaxLength="100" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Contact No : </td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtRef2CNO" runat="server" CssClass="form-control" MaxLength="11" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style3" style="width: 18%">&nbsp;</td>
                    <td class="auto-style5"> 
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style8"></td>
                    <td class="auto-style9">Joining Date<span class="auto-style7">*</span> : </td>
                    <td class="auto-style10">
                        <asp:TextBox ID="txtJoinDT" ClientIDMode="Static" runat="server" CssClass="form-control" MaxLength="12" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style11">Bank AC. NO</td>
                    <td class="auto-style12">
                        <asp:TextBox ID="txtBankAcNO" runat="server" ClientIDMode="Static" CssClass="form-control" MaxLength="12" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style12"></td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">Status <span class="auto-style7">*</span>: </td>
                    <td style="width: 30%">
                        <asp:DropDownList ID="ddlSTATS" runat="server" CssClass="form-control border" Width="100%">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>A</asp:ListItem>
                            <asp:ListItem>I</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3" style="width: 18%">&nbsp;</td>
                    <td class="auto-style5">
                        &nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        <hr />
                        <div style="border:2px solid #eae8e8; border-radius:10px"><br />
                        <table class="auto-style14" cellspacing="0"   bgcolor="#CCCCCC">
                            <tr>
                                <td class="auto-style16" width="17%">Basic Salary&nbsp; :&nbsp;  </td>
                                <td class="auto-style15" width="30%">
                                    <asp:TextBox ID="txtBasicSalary" runat="server" CssClass="form-control" Width="100%">0.00</asp:TextBox>
                                </td>
                                <td class="auto-style17">House Rent :</td>
                                <td class="auto-style15" width="25%">
                                    <asp:TextBox ID="txtHRent" runat="server" CssClass="form-control" Width="100%">0.00</asp:TextBox>
                                </td>
                                <td class="auto-style15" width="1%"></td>
                            </tr>
                            <tr>
                                <td width="17%" class="auto-style19">Medical Allowance : </td>
                                <td width="30%" class="auto-style20">
                                    <asp:TextBox ID="txtMAllwnce" runat="server" CssClass="form-control" Width="100%">0.00</asp:TextBox>
                                </td>
                                <td class="auto-style21">Entertainment conveyance :&nbsp; </td>
                                <td width="25%" class="auto-style20">
                                    <asp:TextBox ID="txtEnterConvey" runat="server" CssClass="form-control" Width="100%">0.00</asp:TextBox>
                                </td>
                                <td width="1%" class="auto-style20"></td>
                            </tr>
                            <tr>
                                <td class="auto-style16" width="17%">Other : </td>
                                <td class="auto-style15" width="30%">
                                    <asp:TextBox ID="txtOther" runat="server" CssClass="form-control" Width="100%">0.00</asp:TextBox>
                                </td>
                                <td class="auto-style17"></td>
                                <td class="auto-style22" width="25%">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit" Width="120px" />
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" OnClick="btnDelete_Click" OnClientClick="return confMSG()" Text="Delete" Visible="False" Width="120px" />
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-info" OnClick="btnUpdate_Click" Text="Update" Visible="False" Width="120px" />
                                </td>
                                <td class="auto-style15" width="1%"></td>
                            </tr>
                            <tr>
                                <td width="17%" class="auto-style2">&nbsp;</td>
                                <td width="30%">&nbsp;</td>
                                <td class="auto-style18">&nbsp;</td>
                                <td width="25%">&nbsp;</td>
                                <td width="1%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="17%" class="auto-style2">&nbsp;</td>
                                <td width="30%">&nbsp;</td>
                                <td class="auto-style18">&nbsp;</td>
                                <td width="25%">&nbsp;</td>
                                <td width="1%">&nbsp;</td>
                            </tr>
                        </table>
                            </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 2%">&nbsp;</td>
                    <td class="auto-style2" style="width: 15%">&nbsp;</td>
                    <td style="width: 30%">
                        <asp:TextBox ID="txtCRDNO" runat="server" CssClass="form-control" Visible="False" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style4" style="width: 18%">&nbsp;</td>
                    <td class="auto-style5">
                        <asp:TextBox ID="txtCRDISUDT" runat="server" ClientIDMode="Static" CssClass="form-control" Visible="False" Width="100%"></asp:TextBox>
                    </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
            </table>
            
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


