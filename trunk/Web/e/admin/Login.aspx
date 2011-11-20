<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.e.admin.Login" %>

<%@ Register Assembly="Voodoo" Namespace="Voodoo.UI" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>后台登录</title>
<link href="/css/loginstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="container">
  <div id="header">
    <h1></h1>
    <ul class="nav">
    </ul>
  </div>
  <div id="content">
    <div class="left_main">
      <h2>渠道信息</h2>
      <ul class="news" id="qdkb_html" style="color:#FFFFFF;">
        &nbsp;&nbsp;&nbsp;&nbsp;欢迎使用专业的.NET网站信息管理系统
      </ul>
      <div class="help_center">
        <h3><a href="http://help.dedecms.com/" target="_blank">帮助中心</a></h3>
        <p><strong>提供专业的技术问题解答</strong></p>
      </div>
      <div class="bbs">
        <h3><a href="http://bbs.dedecms.com/" target="_blank">技术论坛</a></h3>
        <p><strong>发布新产品信息,技术问题解答,资源更新等信息</strong></p>
      </div>
    </div>
    <form name="form1" runat="server">
      <fieldset class="right_main">
      <legend>用户登录</legend>     
      <dl class="setting">
        <dt>
          <label>用户名</label>
        </dt>
        <dd><span class="text_input">
            <cc1:VTextBox ID="txt_UserName" runat="server" EnableValidate="true" EnableNull="false"></cc1:VTextBox>
        </span></dd>
        <dt>
          <label>密　码</label>
        </dt>
        <dd><span class="text_input">
            <cc1:VTextBox ID="txt_Userpass" runat="server" EnableValidate="true" EnableNull="false" TextMode="Password"></cc1:VTextBox>
        </span></dd>
        <dt>
          <label>验证码</label>
        </dt>
        <dd><span class="short_input">
            <cc1:VTextBox ID="txt_VCode" runat="server" EnableValidate="true" EnableNull="false" ></cc1:VTextBox>
        </span>
          <span class="yzm">
            <img id="vdimgck" align="absmiddle" onClick="this.src=this.src+'?'" style="cursor: pointer;" alt="看不清？点击更换" src="/e/admin/tool/safecode.ashx"/>
          </span></dd>

        <dd>
          <asp:Button ID="btn_Login" runat="server" class="login_btn" Text="" OnClick="btn_Login_Click" />
        </dd>
      </dl>
      </fieldset>
    </form>
  </div>
  <div id="footer">&copy; 2011 南京信息工程大学 计算机软件学院</div>
</div>
</body>
</html>