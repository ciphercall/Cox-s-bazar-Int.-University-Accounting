<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthlySalaryReport.aspx.cs" 
    Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.MonthlySalaryReport" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Fees Collection Report</title>
    <link href="../../../css/ui-lightness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {
            $("#txtFrDT,#txtToDT").datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+5" });
        }

    </script>
    <style type="text/css">
        .txtColor:focus {
           border:solid 4px green !important;
        }
          .txtColor
        {
            margin-left: 0px;
            }
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: right;
        }
       
    .form-control {
  display: block;
  padding: 6px 12px;
  font-size: 14px;
  line-height: 1.42857143;
  color: #555;
  background-color: #fff;
  background-image: none;
  border: 1px solid #ccc;
  border-radius: 4px;
  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
          box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
  -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
       -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
          transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
    }
  select {
    background: #fff !important;
  }
  button,
select {
  text-transform: none;
}
* {
  -webkit-box-sizing: border-box;
     -moz-box-sizing: border-box;
          box-sizing: border-box;
}
  * {
    color: #000 !important;
    text-shadow: none !important;
    background: transparent !important;
    -webkit-box-shadow: none !important;
            box-shadow: none !important;
  }
        .auto-style3 {
            text-align: right;
            background-color: #3399FF;
        }
       
    </style>
</head>
<body>
     <form id="form1" runat="server">
         <asp:ScriptManager ID="Script1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
             <div style="border-style: double; margin:0 auto; border-width: 2px; border-radius: 10px; border-color: black; width:960px; color: #000000;">
         <div style=" border-top-left-radius: 10px; border-top-right-radius: 10px; color: #FFFFFF; text-align: center; font-size: xx-large; background-color: #0099FF;" class="auto-style3" >
                    Monthly Salary Report</div>
    <div style="margin-bottom: 0px">

        <table class="auto-style1">
            <tr>
                <td style="width: 20%">&nbsp;</td>
                <td style="width: 20%">
                    <asp:Label ID="lblMSG" runat="server" Font-Size="10pt" ForeColor="#CC0000"></asp:Label>
                </td>
                <td style="width: 20%">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" style="width: 20%">Month : </td>
                <td style="width: 20%">
                    <asp:DropDownList ID="ddlMonth_Year" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlMonth_Year_SelectedIndexChanged" Width="100%">
                    </asp:DropDownList>
                </td>
                <td style="width: 20%">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="lblDeptID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2" style="width: 20%">
                    Department :</td>
                <td style="width: 20%">
                    <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlMonth_Year_SelectedIndexChanged" Width="100%">
                    </asp:DropDownList>
                </td>
                <td style="width: 20%">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 20%">&nbsp;</td>
                <td style="width: 20%; text-align: right;">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="Button1_Click" Text="Search" CssClass="txtColor" />
                </td>
                <td style="width: 20%">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 20%">&nbsp;</td>
                <td style="width: 20%">&nbsp;</td>
                <td style="width: 20%">&nbsp;</td>
            </tr>
        </table>

    </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

