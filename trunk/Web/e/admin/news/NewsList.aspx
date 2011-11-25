﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="Web.e.admin.news.NewsList" %>

<%@ Import Namespace="Voodoo" %>
<%@ Register Assembly="Voodoo" Namespace="Voodoo.UI" TagPrefix="vd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信息管理</title>
    <link rel="stylesheet" type="text/css" href="../../data/css/management.css" />

    <script type="text/javascript" src="../../data/script/jquery-1.7.min.js"></script>

    <script type="text/javascript" src="../../data/script/common.js"></script>
    <script type="text/javascript">
        $(function() {
            $("#btn_Add").click(function() {
                if ($("#ddl_Class_search").val() != "") {
                    location.href = "NewsEdit.aspx?class=" + $("#ddl_Class_search").val();
                }
                return false;
            });
        })
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="search">
            搜索：
            <asp:DropDownList ID="ddl_Prop" runat="server">
                <asp:ListItem Text="--不限属性--" Value=""></asp:ListItem>
                <asp:ListItem Text="置顶" Value="SetTop"></asp:ListItem>
                <asp:ListItem Text="推荐" Value="Tuijian"></asp:ListItem>
                <asp:ListItem Text="头条" Value="Toutiao"></asp:ListItem>
                <asp:ListItem Text="未审核" Value="UnAudit"></asp:ListItem>
                <asp:ListItem Text="已审核" Value="Audit"></asp:ListItem>
            </asp:DropDownList>
            |
            <asp:TextBox ID="txt_Key" runat="server"></asp:TextBox>
            <asp:DropDownList ID="txt_Column" runat="server">
                <asp:ListItem Text="--不限字段--" Value=""></asp:ListItem>
                <asp:ListItem Text="标题" Value="Title"></asp:ListItem>
                <asp:ListItem Text="发布者" Value="Author"></asp:ListItem>
                <asp:ListItem Text="ID" Value="ID"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddl_Class" runat="server">
                <asp:ListItem Text="--所有栏目--" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddl_Zt" runat="server">
                <asp:ListItem Text="--所有专题--" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddl_Order" runat="server">
                <asp:ListItem Text="按发布时间" Value="NewsTime"></asp:ListItem>
                <asp:ListItem Text="按信息ID" Value="ID"></asp:ListItem>
                <asp:ListItem Text="按点击率" Value="ClickCount"></asp:ListItem>
                <asp:ListItem Text="按评论数" Value="ReplyCount"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddl_Desc" runat="server">
                <asp:ListItem Text="按信息倒序排列" Value="DESC"></asp:ListItem>
                <asp:ListItem Text="顺序排列" Value="ASC"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btn_Search" runat="server" Text="搜索" />
        </div>
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
                        标题
                    </th>
                    <th>
                        发布者
                    </th>
                    <th>
                        发布时间
                    </th>
                    <th>
                        点击
                    </th>
                    <th>
                        评论
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
                                <%#Eval("Title")%>
                            </td>
                            <td>
                                <%#Eval("Author")%>
                            </td>
                            <td>
                                <%#Eval("NewsTime")%>
                            </td>
                            <td>
                                <%#Eval("ClickCount")%>
                            </td>
                            <td>
                                <%#Eval("ReplyCount")%>
                            </td>
                            <td>
                                <a href="NewsEdit.aspx?id=<%#Eval("ID") %>&class=<%#Eval("ClassID") %>">修改</a> <a href="?id=<%#Eval("ID") %>&action=del&class=<%=cls %>">
                                    删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="8" class="ctrlPn">
                        <asp:Button ID="btn_disable" Text="审核" runat="server" OnClick="btn_disable_Click" />
                        <asp:Button ID="btn_enable" Text="撤销审核" runat="server" OnClick="btn_enable_Click" />
                        <asp:Button ID="btn_Tj" Text="推荐" runat="server" OnClick="btn_Tj_Click" />
                        <asp:Button ID="btn_First" Text="头条" runat="server" OnClick="btn_First_Click" />
                        <asp:Button ID="btn_SetTop" Text="置顶" runat="server" OnClick="btn_SetTop_Click" />
                        <asp:Button ID="Button1" Text="删除" runat="server" OnClick="Button1_Click" />
                        &nbsp;
                        <asp:DropDownList ID="ddl_Class_search" runat="server"></asp:DropDownList>
                        <asp:Button ID="btn_Add" Text="新增" runat="server" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btn_createPage" Text="重新生成" runat="server" 
                            onclick="btn_createPage_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <vd:AspNetPager ID="pager" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="后页"
                            PrevPageText="前页" AlwaysShow="true" OnPageChanged="pager_PageChanged">
                        </vd:AspNetPager>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    </form>
</body>
</html>
