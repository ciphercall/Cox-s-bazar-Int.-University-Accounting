<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student_wise_due_statement.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.Student_wise_due_statement" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>  
    <link href="../../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>
     <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript">

        function pageLoad() {
            $("#txtDate").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+5" });
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
     .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
            max-height: 200px;
            list-style: none;
            overflow: scroll;
            border: 1px solid #4079ae;
            border-radius: 5px;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: #808080;
        }

        .AutoExtenderHighlight {
            color: #1674c4;
            background-color: #ddd;
            cursor: pointer;
            font-size:15px;
            border: 1px solid #808080;
            border-radius: 4px;
        }

        #divwidth1 {
            width: auto !important;
            height: 180px !important;
        }

            #divwidth1 div {
                width: auto !important;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server"> 
    <div style="width:960px; margin:0 auto; border:2px solid #4072e9; border-radius:5px" >
         <div>
            <table style="width:100%">
                <tr>
                    <td style="font-size: xx-large;width:100%; text-align: center;" class="auto-style3">Cox&#39;s Bazar International University</td>
                </tr>
            </table>
        </div>
    <div style="border-top-left-radius:10px; border-top-right-radius:10px;border-bottom-left-radius:10px;border-bottom-right-radius:10px; width:350px; margin:0 auto; text-align: center; color: #FFFFFF; background-color: #666666; font-weight: 700; font-size: x-large;" class="auto-style5" >Student Wise Due Statement</div>
        <hr />
        <div>

            <table class="auto-style1">
                <tr>
                    <td style="width: 30%" class="text-right">Program Name :</td>
                    <td style="width: 30%">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 30%">
                        <asp:DropDownList ID="ddlYYSem" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlYYSem_SelectedIndexChanged" Width="161px" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="text-right">Date :</td>
                    <td>
                        <asp:TextBox ID="txtDate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblProgramID" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="form-control right" Text="Preview" Width="100px" OnClick="btnSubmit_Click" BackColor="#3399FF" ForeColor="White" />
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>

        </div>
    </div>
    </form>
</body>
</html>
