<%@ Page Title="Category & Item Entry" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CatItemEntry.aspx.cs" Inherits="AlchemyAccounting.Stock.UI.CatItemEntry" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="../../css/ui-darkness/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ui-darkness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.9.0.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">
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

    <style type ="text/css">
        #header
        {
            float: left;
            width:100%;
            background-color: transparent;
            height: 50px;
        }
        #entry
        {
            float: left;
            width: 100%;
            background-color: transparent;
            border: 1px solid #000;
            border-radius: 10px;
            margin-top: 10px;
        }
        #grid
        {
            float:left;
            width:100%;
            
        }
        .style1
        {
            width: 227px;
        }
        .style2
        {
            width: 3px;
            text-align: center;
        }
        .style3
        {
            width: 124px;
        }
        .style4
        {
            width: 17px;
        }
        .style5
        {
            text-align: left;
        }
        .style6
        {
            width: 95px;
        }
        .style8
        {
            text-align: center;
            width: 1px;
        }
        .style9
        {
            width: 1px;
        }
        .style10
        {
            width: 238px;
        }
        .style11
        {
            text-align: left;
            width: 238px;
        }
        .Gridview
         {
            font-family:Verdana;
            font-size:10pt;
            font-weight:normal;
            color:black;
            margin-right: 0px;
            text-align: left;
         }
        </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header">
        <h1 align="center" style="font-weight: bold;">Category & Item Entry</h1>
    </div>
    <div id="entry">
        
        <table style="width:100%;">
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style6">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td class="style10">
                    <asp:Label ID="lblCatID" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblMaxCatID" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style6" style="text-align: right">
                    <strong>Category Name</strong></td>
                <td class="style8">
                    <strong>:</strong></td>
                <td class="style11">
                    <asp:TextBox ID="txtCategoryNM" runat="server" TabIndex="1" Width="250px" 
                        ontextchanged="txtCategoryNM_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtCategoryNM_AutoCompleteExtender" 
                        runat="server" DelimiterCharacters="" Enabled="True" ServicePath="" 
                        TargetControlID="txtCategoryNM" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="true" CompletionSetCount="3"
                    UseContextKey="True" ServiceMethod="GetCompletionList">
                    </asp:AutoCompleteExtender>
                </td>
                <td class="style5">
                    <asp:Button ID="Search" runat="server" Font-Bold="True" Font-Italic="True" 
                        TabIndex="2" Text="Search" onclick="Search_Click" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    <asp:Label ID="lblChkItemID" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style6">
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;</td>
                <td class="style10">
                    <asp:Label ID="lblIMaxItemID" runat="server" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
        </table>
        <div id="grid">
            
<asp:GridView ID="gvDetails" runat="server" 
        AutoGenerateColumns="False" CssClass="Gridview" HeaderStyle-BackColor="#61A6F8" 
ShowFooter="True" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" 
        onrowcancelingedit="gvDetails_RowCancelingEdit" 
        onrowdeleting="gvDetails_RowDeleting" onrowediting="gvDetails_RowEditing" 
        onrowupdating="gvDetails_RowUpdating" 
        onrowcommand="gvDetails_RowCommand"
        OnRowDataBound="gvDetails_RowDataBound" Width="100%">
     
<Columns>
<asp:TemplateField HeaderText="CatID">
    <ItemTemplate>
        <asp:Label ID="lblCatGID" runat="server" Text='<%# Eval("CATID") %>' 
            Width="80px"/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:Label ID="lblCatGID" runat="server" Text='<%#Eval("CATID") %>' 
            Width="80px"/>
    </EditItemTemplate>
    
    <FooterTemplate>
        
    </FooterTemplate> 
    <ControlStyle Width="50px" />
    <FooterStyle HorizontalAlign="Center" Width="50px" />
    <HeaderStyle HorizontalAlign="Center" Width="50px" />
    <ItemStyle HorizontalAlign="Center" Width="50px" />
</asp:TemplateField>

<asp:TemplateField HeaderText="ItemID">
    <ItemTemplate>
        <asp:Label ID="lblItemID" runat="server" Text='<%# Eval("ITEMID") %>' 
            Width="50px" style="text-align: center"/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:Label ID="lblItemID" runat="server" Text='<%#Eval("ITEMID") %>' 
            Width="50px" style="text-align: center"/>
    </EditItemTemplate>
    
    <FooterTemplate>
        
    </FooterTemplate>
     
    <ControlStyle Width="80px" />
    <FooterStyle HorizontalAlign="Center" Width="80px" />
    <HeaderStyle HorizontalAlign="Center" Width="80px" />
    <ItemStyle HorizontalAlign="Center" Width="80px" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Item Name">
    <ItemTemplate>
        <asp:Label ID="lblItemNM" runat="server" Text='<%# Eval("ITEMNM") %>' 
            Width="250px" style="text-align: left"/>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtItemNMEdit" runat="server" Text='<%#Eval("ITEMNM") %>' 
            Width="250px" ontextchanged="txtItemNM_TextChanged" TabIndex="10"/>
    </EditItemTemplate>
    
    <FooterTemplate>
        <asp:TextBox ID="txtItemNM" runat="server" Width="250px" TabIndex="3" 
            ontextchanged="txtItemNM_TextChanged"/>
    </FooterTemplate> 
    <ControlStyle Width="250px" />
    <FooterStyle HorizontalAlign="Left" Width="250px" />
    <HeaderStyle HorizontalAlign="Center" Width="250px" />
    <ItemStyle HorizontalAlign="Left" Width="250px" />
</asp:TemplateField>

 <asp:TemplateField HeaderText="Brand">
 <ItemTemplate>
        <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("BRAND") %>' 
            style="text-align: center"/>
    </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtBrandEdit" runat="server" Text='<%#Eval("BRAND") %>' 
             style="text-align: right" TabIndex="11" 
             ontextchanged="txtBrandEdit_TextChanged" />
     </EditItemTemplate>
     <FooterTemplate>
         <asp:TextBox ID="txtBrand" runat="server" TabIndex="4" 
             style="text-align: left" />
     </FooterTemplate>
     <ControlStyle Width="100px" />
     <FooterStyle HorizontalAlign="Left" Width="100px" />
     <HeaderStyle HorizontalAlign="Center" Width="100px" />
     <ItemStyle Width="100px" />
 </asp:TemplateField>

 <asp:TemplateField HeaderText="Unit">
     <ItemTemplate>
        <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("UNIT") %>' 
             style="text-align: center"></asp:Label>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtUnitEdit" runat="server" Text='<%#Eval("UNIT") %>' 
            style="text-align: right" TabIndex="12"/>
    </EditItemTemplate>

     <FooterTemplate>
        <asp:TextBox ID="txtUnit" runat="server" TabIndex="4" 
             style="text-align: left" Width="80px"/>
     </FooterTemplate>

     <ControlStyle Width="80px" />
     <FooterStyle HorizontalAlign="Left" Width="80px" />

     <HeaderStyle HorizontalAlign="Center" Width="80px" />

     <ItemStyle HorizontalAlign="Left" Width="80px" />

 </asp:TemplateField>

  <asp:TemplateField HeaderText="Quantity Per Carton">
     <ItemTemplate>
        <asp:Label ID="lblPack" runat="server" Text='<%#Eval("PQTY") %>' 
             style="text-align: center"></asp:Label>
    </ItemTemplate>

    <EditItemTemplate>
        <asp:TextBox ID="txtPackEdit" runat="server" Text='<%#Eval("PQTY") %>' 
            style="text-align: right" TabIndex="12"/>
    </EditItemTemplate>

     <FooterTemplate>
        <asp:TextBox ID="txtPack" runat="server" TabIndex="4" 
             style="text-align: left" Width="80px"/>
     </FooterTemplate>

     <ControlStyle Width="80px" />
     <FooterStyle HorizontalAlign="Left" Width="80px" />

     <HeaderStyle HorizontalAlign="Center" Width="80px" />

     <ItemStyle HorizontalAlign="Left" Width="80px" />

 </asp:TemplateField>

    <asp:TemplateField HeaderText="Buy Rate">
        <EditItemTemplate>
            <asp:TextBox ID="txtBuyRTEdit" runat="server" style="text-align: right" Text='<%#Eval("BUYRT") %>'
                Width="90px" TabIndex="13"></asp:TextBox>
        </EditItemTemplate>
        <FooterTemplate>
            <asp:TextBox ID="txtBuyRT" runat="server" Width="90px" 
                style="text-align: right" TabIndex="6" ></asp:TextBox>
        </FooterTemplate>
        <ItemTemplate>
            <asp:Label ID="lblBuyRT" runat="server" Width="90px" style="text-align: right" Text='<%#Eval("BUYRT") %>'></asp:Label>
        </ItemTemplate>
        <ControlStyle Width="90px" />
        <FooterStyle HorizontalAlign="Right" Width="90px" />
        <HeaderStyle HorizontalAlign="Center" Width="90px" />
        <ItemStyle Width="90px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Sale Rate">
        <EditItemTemplate>
            <asp:TextBox ID="txtSaleRTEdit" runat="server" Width="90px" 
                style="text-align: right" Text='<%#Eval("SALERT") %>' TabIndex="14"></asp:TextBox>
        </EditItemTemplate>
        <FooterTemplate>
            <asp:TextBox ID="txtSaleRT" runat="server" Width="90px" 
                style="text-align: right" TabIndex="7"></asp:TextBox>
        </FooterTemplate>
        <ItemTemplate>
            <asp:Label ID="lblSaleRT" runat="server" style="text-align: right" Text='<%#Eval("SALERT") %>'
                Width="90px"></asp:Label>
        </ItemTemplate>
        <ControlStyle Width="90px" />
        <FooterStyle HorizontalAlign="Right" Width="90px" />
        <HeaderStyle HorizontalAlign="Center" Width="90px" />
        <ItemStyle HorizontalAlign="Right" Width="90px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Min Stock Qty">
        <EditItemTemplate>
            <asp:TextBox ID="txtMinsQtyEdit" runat="server" Width="80px" 
                style="text-align: right" Text='<%#Eval("MINSQTY") %>' TabIndex="15"></asp:TextBox>
        </EditItemTemplate>
        <FooterTemplate>
            <asp:TextBox ID="txtMinsQty" runat="server" style="text-align: right" 
                Width="60px" TabIndex="8"></asp:TextBox>
        </FooterTemplate>
        <ItemTemplate>
            <asp:Label ID="lblMisQty" runat="server" style="text-align: right" Width="60px" Text='<%#Eval("MINSQTY") %>'></asp:Label>
        </ItemTemplate>
        <ControlStyle Width="60px" />
        <FooterStyle HorizontalAlign="Right" Width="60px" />
        <HeaderStyle HorizontalAlign="Center" Width="60px" />
        <ItemStyle HorizontalAlign="Right" Width="60px" />
    </asp:TemplateField>

 <asp:TemplateField>
<EditItemTemplate>
<asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" 
        ImageUrl="~/Images/update.jpg" ToolTip="Update" Height="20px" Width="20px" 
        TabIndex="16" />
<asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" 
        ImageUrl="~/Images/Cancel.jpg" ToolTip="Cancel" Height="20px" Width="20px" 
        TabIndex="17" />

</EditItemTemplate>
<ItemTemplate>
<asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" 
        ImageUrl="~/Images/Edit.jpg" ToolTip="Edit" Height="20px" Width="20px" 
        TabIndex="10" />
<asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server" 
        ImageUrl="~/Images/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" OnClientClick="return confMSG()"
        TabIndex="11" />
</ItemTemplate>
<FooterTemplate>
<asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/AddNewitem.jpg" 
        CommandName="AddNew" Width="30px" Height="30px" ToolTip="Add new Record" 
        ValidationGroup="validaiton" TabIndex="9" />

</FooterTemplate>
 </asp:TemplateField>

 </Columns>

    <EditRowStyle BackColor="#999966" />

<HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White"></HeaderStyle>
</asp:GridView>



        </div>
        
    </div>

</asp:Content>