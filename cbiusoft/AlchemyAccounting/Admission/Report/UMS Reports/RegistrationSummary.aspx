<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationSummary.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.RegistrationSummary" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>:: Course Enrollment</title>
    <link href="../../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery-ui.js" type="text/javascript"></script>
   
    

    <script type ="text/javascript">
        function ClosePrint() {
            var print = document.getElementById("print");
            print.style.visibility = "hidden";
            //            print.display = false;

            window.print();
        }
    </script>

      

    <style type="text/css">
        
        .auto-style2 {
            font-size: xx-large;
            text-align: center;
            color: #FFFFFF;
            font-weight: 700;
            border: 2px double white;
            background-color: #2aabd2;
        }
        .auto-style4 {
            width: 100%;
        }
        .auto-style5 {
            text-align: right;
        }
        .auto-style6 {
            width: 15%;
            height: 26px;
        }
        .auto-style7 {
            width: 28%;
            height: 26px;
        }
        .auto-style8 {
            text-align: right;
            width: 25%;
            height: 26px;
        }
        .auto-style9 {
            width: 35%;
            height: 26px;
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
        .auto-style10 {
            width: 28%;
        }
        .form-control {}
    </style>
</head>
<body>
    <form id="form1" runat="server"> 
          <asp:ScriptManager ID="sds" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
    <div style="border-style: double; margin:0 auto; border-width: 2px; border-radius: 10px; border-color: black; width:960px; color: #000000;">
         <div style=" border-top-left-radius: 10px; border-top-right-radius: 10px; " class="auto-style2" >
                    Registration Summary</div>
         <table class="auto-style4">
             <tr>
                 <td>
                     <table class="auto-style4">
                         <tr>
                             <td style="text-align: right; " class="auto-style6">Program&nbsp;&nbsp; :</td>
                             <td class="auto-style7">
                                 <asp:DropDownList ID="ddlProg"  runat="server" CssClass="form-control" Width="100%" Height="30px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemNM_SelectedIndexChanged">
                                 </asp:DropDownList>
                                 <asp:Label ID="lblProgram" runat="server" Visible="False"></asp:Label>
                             </td>
                             <td class="auto-style8">Semester&nbsp; :</td>
                             <td class="auto-style9">
                                 <asp:DropDownList ID="ddlSem"  runat="server" CssClass="form-control" Height="25px" Width="198px" AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                 </asp:DropDownList>
                             </td>
                         </tr>
                         <tr>
                             <td style="text-align: right; width:15%">Batch&nbsp;&nbsp; :</td>
                             <td style="width:20%">
                                 <asp:TextBox ID="txtBatch" runat="server" Width="100%" Height="25px" OnTextChanged="txtBatch_TextChanged"></asp:TextBox>
                                  <asp:AutoCompleteExtender ID="txtBatch_AutoCompleteExtender"
                                runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=";, :"
                                Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionBatch" CompletionListCssClass="AutoExtender"
                                CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                TargetControlID="txtBatch" UseContextKey="True">
                            </asp:AutoCompleteExtender>
                             </td>
                             <td style="width:25%" class="auto-style5">
                                 Session :</td>
                             <td style="width:35%">
                                 <asp:TextBox ID="txtSession" runat="server" Width="198px" Height="25px"></asp:TextBox>
                                  <asp:AutoCompleteExtender ID="txtSession_AutoCompleteExtender1"
                                runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters=";, :"
                                Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionSession" CompletionListCssClass="AutoExtender"
                                CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                TargetControlID="txtSession" UseContextKey="True">
                            </asp:AutoCompleteExtender>
                             </td>
                         </tr>
                         <tr>
                             <td style="text-align: right; width:15%">&nbsp;</td>
                             <td style="width:20%">&nbsp;</td>
                             <td class="auto-style5" style="width:25%">&nbsp;</td>
                             <td style="width:35%">
                                 <asp:Button ID="btnPrint0" runat="server" BackColor="#2aabd2" BorderColor="#333333" BorderWidth="2px" Font-Bold="True" ForeColor="White" Height="25px" OnClick="btnPrint_Click" Text="Print" Width="120px" />
                                 <asp:Label ID="lblSemester" runat="server" Visible="False"></asp:Label>
                             </td>
                         </tr>
                     </table >
                   
                     <div style="padding:10px">
                     
                         </div>
                 </td>
             </tr>
         </table>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
