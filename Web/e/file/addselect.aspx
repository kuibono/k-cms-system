<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addselect.aspx.cs" Inherits="Web.e.file.addselect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <table class="style1" border="1" cellpadding="0" cellspacing="0" style="border-collapse:collapse">
        <tr>
            <td colspan="2">
                上传</td>
        </tr>
        <tr>
            <td width="100">
                上传文件</td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                别名</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btn_UpLoad" runat="server" Text="上传" 
                    onclick="btn_UpLoad_Click" />
            </td>
        </tr>
    </table>
    
    <asp:DataList ID="DataList1" runat="server" BorderStyle="Solid" 
        BorderWidth="1px" RepeatColumns="3" RepeatDirection="Horizontal">
        <ItemTemplate>
             <img src="<%#Eval("FileDirectory") %>/<%#Eval("FileName") %>" width="105" height="118" style="border:solid 1px gray; margin:2px;" />
        </ItemTemplate>
        
    </asp:DataList>
    
    </form>
</body>
</html>
