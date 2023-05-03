<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="migration.aspx.cs" Inherits="AlchemyAccounting.Admission.UI.migration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            $("#txtdate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+5" });
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
        }

        .auto-style5 {
            text-align: right;
        }

        .auto-style6 {
            color: #CC0000;
            font-size: large;
        }

        .auto-style7 {
            text-align: center;
            color: #CC0000;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="auto-style7">migration</h1>
    <div style="margin: 0 auto; padding: 10px; width: 95%; height: auto; border: 1px solid #d3d3d3; border-radius: 5px; box-shadow: 0 0 5px rgba(0, 0, 0, 0.1); background: none repeat scroll 0 0 white;">
        <table class="auto-style1">
            <tr>
                <td colspan="4" class="text-left">
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style5" width="40%">&nbsp;</td>
                            <td width="40%" class="auto-style5">Migration Date :</td>
                            <td width="20%"><asp:TextBox ID="txtdate" runat="server" ClientIDMode="Static" CssClass="form-control" TabIndex="2" Width="100%" OnTextChanged="txtStudentIDFrom_TextChanged"></asp:TextBox>
   
                            </td>
                        </tr>
                    </table>
   
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4"><span class="auto-style6"><strong>Form Program</strong></span>&nbsp; :</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5" width="20%">Semester Name : </td>
                <td>
                    <asp:DropDownList ID="ddlSemNMFrom" runat="server" CssClass="form-control" Width="100%" TabIndex="0" AutoPostBack="True" OnSelectedIndexChanged="ddlSemNMFrom_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="auto-style5" width="20%">Program Name : </td>
                <td width="30%">
                    <asp:DropDownList ID="ddlProgNMFrom" runat="server" CssClass="form-control" Width="100%" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlProgNMFrom_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">Student ID Old: </td>
                <td>
                    <asp:TextBox ID="txtStudentIDFrom" runat="server" CssClass="form-control" TabIndex="2" Width="100%" OnTextChanged="txtStudentIDFrom_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender"
                        runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=";, :"
                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentID" CompletionListCssClass="AutoExtender"
                        CompletionListItemCssClass="AutoExtenderList"
                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                        TargetControlID="txtStudentIDFrom" UseContextKey="True">
                    </asp:AutoCompleteExtender>
                </td>
                <td class="auto-style5">Student&nbsp; Name :</td>
                <td>
                    <asp:TextBox ID="txtStudentNMFrom" runat="server" CssClass="form-control" Width="100%" Enabled="False"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;<hr />
                    <br />
                    <div style="align-items:center">
                    <asp:Image ID="Image1" CssClass="form-control" ImageUrl="../loader.gif" runat="server" Height="21px" Visible="False" Width="100%" /> 
                        </div>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" class="auto-style6"><strong>To Program :</strong></td>
                <td class="auto-style6">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">Year :</td>
                <td>
                    <asp:DropDownList ID="ddlRegYR" runat="server" CssClass="form-control" TabIndex="2" Width="150px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">Semester Name : </td>
                <td>
                    <asp:DropDownList ID="ddlSemNMTo" runat="server" TabIndex="3" CssClass="form-control" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlSemNMTo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="auto-style5">Program Name : </td>
                <td>
                    <asp:DropDownList ID="ddlProgNMTo" runat="server" TabIndex="4" CssClass="form-control" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlProgNMTo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">Student ID New: </td>
                <td>
                    <asp:TextBox ID="txtStudentIDNew" runat="server" CssClass="form-control" Width="100%" MaxLength="10" Enabled="False"></asp:TextBox>
                </td>
                <td class="auto-style5">Student&nbsp; Name :</td>
                <td>
                    <asp:TextBox ID="txtStudentNMTo" runat="server" CssClass="form-control" Width="100%" MaxLength="10" Enabled="False"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style5" colspan="2">
                    <asp:Label ID="lblMSG" CssClass="form-control left" runat="server" BorderColor="Red" BorderWidth="2px" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                </td>
                <td class="auto-style5">
                    <asp:Button ID="btnMigrat" runat="server" CssClass="form-control-right" BackColor="#0066FF" BorderColor="Black" BorderWidth="3px" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="40px" Text="MIGRAT" Width="48%" OnClick="btnMigrat_Click" />
                    <asp:Button ID="btnMigrat0" runat="server" CssClass="form-control-right" BackColor="#990000" BorderColor="Black" BorderWidth="3px" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="40px" Text="REFRESH" Width="48%" />
                    &nbsp;
                </td>
                <td class="auto-style5">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5" colspan="4">
                    <asp:GridView ID="gv_Student" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="gv_StuEdu" runat="server">
                    </asp:GridView>
                    <asp:GridView ID="gv_Trans" runat="server">
                    </asp:GridView>
                </td>
                <td class="auto-style5">&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
