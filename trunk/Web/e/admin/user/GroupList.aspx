<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupList.aspx.cs" Inherits="Web.e.admin.user.GroupList" %>
<%@ Import Namespace="Voodoo" %>
<%@ Register Assembly="Voodoo" Namespace="Voodoo.UI" TagPrefix="vd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>会员群组</title>
    <link rel="stylesheet" type="text/css" href="/css/management.css" />
    <script type="text/javascript" src="/script/jquery-1.7.min.js"></script>
    <script type="text/javascript" src="/script/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="1" cellpadding="0" cellspacing="1" class="list">
            <thead>
                <tr>
                    <th>
                        <input type="checkbox" id="checkall" />
                    </th>
                    <th>
                        ID
                    </th>
                    <th>
                        名称
                    </th>
                    <th>
                        级别
                    </th>

                    <th>
                        管理
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rp_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <input name="id" type="checkbox" value="<%#Eval("ID") %>" />
                            </td>
                            <td>
                                <%#Eval("ID") %>
                            </td>
                            <td>
                                <%#Eval("GroupName")%>
                            </td>
                            <td>
                                <%#Eval("Grade")%>
                            </td>
                            <td>
                                <a href="?id=<%#Eval("ID") %>&action=disable">停用</a> 
                                <a href="?id=<%#Eval("ID") %>&action=enable">启用</a> 
                                <a href="GroupEdit.aspx?id=<%#Eval("ID") %>">修改</a> 
                                <a href="?id=<%#Eval("ID") %>&action=del">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="10" class="ctrlPn">
                        <asp:Button ID="btn_disable" Text="停用" runat="server" 
                            onclick="btn_disable_Click" />
                        <asp:Button ID="btn_enable" Text="启用" runat="server" 
                            onclick="btn_enable_Click" />
                        <asp:Button ID="btn_Add" Text="新增" runat="server" OnClientClick="location.href='GroupEdit.aspx';return false" />
                        <asp:Button ID="Button1" Text="删除" runat="server" onclick="Button1_Click" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </form>
</body>
</html>
