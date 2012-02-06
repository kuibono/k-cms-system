<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Createpages.aspx.cs" Inherits="Web.e.admin.system.Update.Createpages" %>

<%@ Register assembly="Voodoo" namespace="Voodoo.UI" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>数据更新中心</title>
    <link rel="stylesheet" type="text/css" href="../../../data/css/management.css" />
    <script type="text/javascript" src="../../../data/script/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="../../../data/script/common.js"></script>
    <style>
    td
    {
        height:30px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table border="1" cellpadding="0" cellspacing="1" class="list">
        <tbody>
            <tr>
                <td>
                     
                    <asp:Button ID="btn_Index" runat="server" Text="生成首页" 
                        onclick="btn_Index_Click" />
                     
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Button ID="btn_List" runat="server" Text="生成列表页" 
                         onclick="btn_List_Click" />
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Button ID="btn_Content" runat="server" Text="生成内容页" 
                         onclick="btn_Content_Click" />
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Button ID="btn_Chapter" runat="server" Text="生成章节" 
                         onclick="btn_Chapter_Click" />
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Button ID="btn_ClearAll" runat="server" Text="清理所有静态页面" 
                         onclick="btn_ClearAll_Click"  />
                </td>
            </tr>
                        <tr>
                <td>
                     <asp:Button ID="btn_GenSitreMap" runat="server" Text="生成SiteMap" 
                         onclick="btn_GenSitreMap_Click"  />
                </td>
            </tr>
            <tr>
                <td>
                     
                    <cc1:VTextBox ID="txt_SQL" runat="server" TextMode="MultiLine" Width="300px"></cc1:VTextBox>
                    <asp:Button ID="btn_Excute" runat="server" onclick="btn_Excute_Click" 
                        Text="Excute" />
                     
                </td>
            </tr>
            <tr>
                <td>
                     
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                     
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
