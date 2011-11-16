<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="Web.e.member.LoginForm" %>
<%@ Import Namespace="Voodoo.Basement" %>
<%
    if (UserAction.opuser.ID <= 0)
    {
 %>
<form action="/e/member/Login.aspx" id="fm_log" name="fm_log" method="post">
    <p>
        账号：<input type="text" name="username" />
        密码：<input type="password" name="userpass" />
        <a href="javscript:void(0)" onclick="fm_log.submit()">登录</a> &nbsp; <a href="#">注册</a>
        <a href="#">忘记密码</a>
    </p>
</form>
<%
    }
    else
    {
%>
    <p>
    欢迎您 <%=UserAction.opuser.UserName%> &nbsp;登陆次数：<%=UserAction.opuser.LoginCount%>
    </p>
<%
    }
%>