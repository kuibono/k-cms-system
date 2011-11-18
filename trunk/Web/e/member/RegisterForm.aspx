<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterForm.aspx.cs" Inherits="Web.e.member.RegisterForm" %>
<%= Voodoo.Basement.CreatePage.CreateHeaderString("会员注册") %>
    <style type="text/css">
        .style1
        {
            width: 100%;
            border-collapse: collapse;
            border-left-style: solid;
            border-left-width: 1px;
            border-right: 1px solid #C0C0C0;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom: 1px solid #C0C0C0;
            color:#000;
        }
        .style1 td
        {
            border:solid 1px #c0c0c0;
            line-height:30px;
            height:30px;
            padding:3px;
        }
        .style1 td input[type=text],.style1 td input[type=password]
        {
            width:200px;
        }
        .regform
        {
            color:#000;
        }
        .tb_header
        {
            background-color:#ccc;
            height:30px;
            line-height:30px;
            font-size:14px;
            color:#000;
        }
    </style>

        <div id="main_1" class="clearfix mg">
        	<div class="nav">您的位置：<a href="/">首页</a>> <a href="javascript:void(0)">注册</a></div>
            <div id="newslist" class="left inner" style="width: 660px;">
            <h1>会员注册</h1>
                <form method="post" action="Register.aspx">
                <table border="0" width="100%" class="regform">
                    <tr>
                        <td class="tb_header">
                            基本信息
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="1" class="style1">
                                <tr>
                                    <td width="100" align="right">
                                        帐&nbsp;&nbsp;&nbsp; 号 </td>
                                    <td>
                                        <input name="username"  id="username" type="text"/>&nbsp;*
                                        <input type="hidden" id="group" name="group" value="<%=Request.QueryString["group"] %>"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        密&nbsp;&nbsp;&nbsp; 码 </td>
                                    <td>
                                        <input name="userpass" id="userpass" type="password" />&nbsp;*
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        重复密码 </td>
                                    <td>
                                        <input name="userpasser" id="userpassr" type="password" />&nbsp;*
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        电子信箱 </td>
                                    <td>
                                        <input name="email" id="email" type="text" />&nbsp;*
                                    </td>
                                </tr>
                            </table>
                            
                            
                        </td>
                    </tr>
                    <tr>
                        <td  class="tb_header">
                            其他信息
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=formString%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;<input type="submit" value="注册" />&nbsp;
                        </td>
                    </tr>
                </table>
                </form>
            </div>
            <div class="right" style="width: 259px; margin-left:32px;">
                <h1>
                    功能菜单
                </h1>
                <ul class="inner">
                    <%=Voodoo.Basement.CreatePage.BuildUserMenuString()%>
                </ul>
            </div>
        </div>
 <%= Voodoo.Basement.CreatePage.CreateFooterString() %>