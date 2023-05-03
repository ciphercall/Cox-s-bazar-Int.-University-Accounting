<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultsByStudent.aspx.cs" Inherits="AlchemyAccounting.Admission.Report.UMS_Reports.ResultsByStudent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>::Create Results By Student</title>
      <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .form-control:focus {
           border:solid 4px green !important;
        }
          .form-control
        {
            margin-left: 0px;
            }
 
        .auto-style2 {
            width: 100%;
            font-size: 14px;
        }
        .form-control {}
        .auto-style11 {
            color: #FFFFFF;
        }
        .auto-style12 {
            width: 5%;
            height: 39px;
        }
        .auto-style13 {
            text-align: right;
            width: 15%;
            height: 39px;
        }
        .auto-style14 {
            width: 30%;
            height: 39px;
        }
        .auto-style15 {
            width: 15%;
            height: 39px;
        }
        .auto-style16 {
            width: 30%;
            text-align: left;
            height: 39px;
        }
        .auto-style17 {
            height: 39px;
        }
        .auto-style18 {
            width: 5%;
            height: 42px;
        }
        .auto-style19 {
            text-align: right;
            width: 15%;
            height: 42px;
        }
        .auto-style20 {
            width: 30%;
            height: 42px;
        }
        .auto-style21 {
            width: 30%;
            text-align: left;
            height: 40px;
        }
        .auto-style22 {
            width: 15%;
            height: 42px;
        }
        .auto-style23 {
            width: 5%;
            height: 41px;
        }
        .auto-style24 {
            text-align: right;
            width: 15%;
            height: 41px;
        }
        .auto-style25 {
            width: 30%;
            height: 41px;
        }
        .auto-style26 {
            width: 15%;
            height: 41px;
        }
        </style>
</head>

    <body>
    <form id="form1" runat="server"> 
        <asp:ScriptManager ID="sds" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
     <div style="width:960px;margin:0 auto ;  border:double;border-color:black;border-width:2px">
        <div>
            
            <table class="auto-style2">
                <caption>
                    <br/>
                    <tr>
                        <td>&nbsp;</td>
                        <td class="auto-style11" style="font-weight: 700; font-size: xx-large;text-align: center; background-color: #2aabd2;">Results By Student</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <br />
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style12"></td>
                                    <td class="auto-style13">Registration Year :</td>
                                    <td class="auto-style14">
                                        <asp:DropDownList ID="ddlYR" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" OnSelectedIndexChanged="ddlYR_SelectedIndexChanged" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right;" class="auto-style15">Semester Name&nbsp; :</td>
                                    <td class="auto-style16">
                                        <asp:DropDownList ID="ddlSemNM" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" OnSelectedIndexChanged="ddlSemNM_SelectedIndexChanged" Width="100%" TabIndex="1">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style15"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style12"></td>
                                    <td class="auto-style13">Program Name&nbsp; :</td>
                                    <td class="auto-style14">
                                        <asp:DropDownList ID="ddlProgNM" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" OnSelectedIndexChanged="ddlProgNM_SelectedIndexChanged" Width="100%" TabIndex="2">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right;" class="auto-style17">Student&nbsp; ID&nbsp; :</td>
                                    <td class="auto-style16">
                                        <asp:TextBox ID="txtstuID" runat="server" Width="100%" OnTextChanged="txtstuID_TextChanged" CssClass="form-control"  AutoPostBack="True" Height="35px" TabIndex="3"></asp:TextBox>
                                        <asp:TextBox ID="txtstuIDNew" runat="server" Visible="false"></asp:TextBox>
                                   <asp:AutoCompleteExtender ID="txtstuID_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentD" TargetControlID="txtstuID" UseContextKey="True">
                                                        </asp:AutoCompleteExtender>
                                         </td>
                                    <td class="auto-style15"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style23"></td>
                                    <td class="auto-style24">Semester ID :</td>
                                    <td class="auto-style25">
                                        <asp:DropDownList ID="ddlSemesterID" runat="server" AutoPostBack="True" CssClass="form-control" Height="35px" OnSelectedIndexChanged="ddlSemesterID_SelectedIndexChanged" TabIndex="4" Width="100%">
                                            <asp:ListItem Value="01">1st- Semester</asp:ListItem>
                                            <asp:ListItem Value="02">2nd- Semester</asp:ListItem>
                                            <asp:ListItem Value="03">3rd- Semester</asp:ListItem>
                                            <asp:ListItem Value="04">4th- Semester</asp:ListItem>
                                            <asp:ListItem Value="05">5th- Semester</asp:ListItem>
                                            <asp:ListItem Value="06">6th- Semester</asp:ListItem>
                                            <asp:ListItem Value="07">7th- Semester</asp:ListItem>
                                            <asp:ListItem Value="08">8th- Semester</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right;" class="auto-style26">
                                        Student Name&nbsp; :</td>
                                    <td class="auto-style25" style="text-align: right;">
                                        <asp:TextBox ID="txtstuNM" runat="server" AutoPostBack="True" Enabled="False" Height="35px" CssClass="form-control"  OnTextChanged="txtstuID_TextChanged" Width="100%"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txtstuNM_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="3" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionStudentD" TargetControlID="txtstuNM" UseContextKey="True">
                                        </asp:AutoCompleteExtender>
                                    </td>
                                    <td class="auto-style26"></td>
                                 </tr>
                                <tr>
                                    <td style="width: 5%">&nbsp;</td>
                                    <td colspan="4" style="text-align: right">
                                        <asp:Label ID="lblProgID" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblSemID" runat="server" Visible="False"></asp:Label>
                                        <asp:Button ID="btnSerach" runat="server" BorderColor="#0066FF" BorderWidth="2px" CssClass="form-control-right" Font-Bold="True" Height="30px"  Text="Search" Width="134px" OnClick="btnSerach_Click" TabIndex="5" />
                                        <asp:Button ID="btnPrint" runat="server" BorderColor="#0066FF" BorderWidth="2px" CssClass="form-control-right" Font-Bold="True" Height="30px" OnClick="btnPrint_Click" Text="Print" Width="143px" TabIndex="6" />
                                        <asp:Button ID="btnPrintFinal" runat="server" BorderColor="#0066FF" BorderWidth="2px" CssClass="form-control-right" Font-Bold="True" Height="30px"  Text="Total Result" Width="143px" TabIndex="6" OnClick="btnPrintFinal_Click" />

                                    </td>
                                    <td style="width: 15%">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </caption>
                </table>
        </div>
    
   <div>
       <table class="auto-style2">
           <tr>
               <td style="padding:10px">
                     <asp:GridView ID="gv_Result" runat="server" AutoGenerateColumns="False" OnRowDataBound="gv_Result_RowDataBound" style="margin-bottom: 0px" Width="100%">
                         <Columns>
                             <asp:BoundField Headertext="SL.">
                             <HeaderStyle HorizontalAlign="Center" Width="3%" />
                             <ItemStyle Font-Bold="True" Font-Size="12pt" HorizontalAlign="Left" Width="3%" />
                             </asp:BoundField>
                             <%--<asp:BoundField  HeaderText="Student Name" >
                                 <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                             </asp:BoundField>--%>
                             <asp:BoundField HeaderText="Course Name">
                             <HeaderStyle HorizontalAlign="Center" Width="18%" />
                             <ItemStyle HorizontalAlign="Center" Width="18%" />
                             </asp:BoundField>
                             <asp:BoundField HeaderText="CGPA">
                             <HeaderStyle HorizontalAlign="Center" Width="6%" />
                             <ItemStyle HorizontalAlign="Center" Width="6%" />
                             </asp:BoundField>
                             <asp:BoundField HeaderText="L. Grade">
                             <HeaderStyle HorizontalAlign="Center" Width="6%" />
                             <ItemStyle HorizontalAlign="Center" Width="6%" />
                             </asp:BoundField>
                             <asp:BoundField HeaderText="Remarks">
                             <HeaderStyle HorizontalAlign="Center" Width="10%" />
                             <ItemStyle HorizontalAlign="Left" Width="10%" />
                             </asp:BoundField>
                         </Columns>
                         <HeaderStyle BackColor="#CCCCCC" />
                     </asp:GridView>
               </td>
           </tr>
           
          
           
       </table>
   </div>
          </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>