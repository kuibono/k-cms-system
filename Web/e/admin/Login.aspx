<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.e.admin.Login" %>

<%@ Register Assembly="Voodoo" Namespace="Voodoo.UI" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>��̨��¼</title>
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
      <h2>������Ϣ</h2>
      <ul class="news" id="qdkb_html" style="color:#FFFFFF;">
        &nbsp;&nbsp;&nbsp;&nbsp;��ӭʹ��רҵ��.NET��վ��Ϣ����ϵͳ
      </ul>
      <div class="help_center">
        <h3><a href="http://help.dedecms.com/" target="_blank">��������</a></h3>
        <p><strong>�ṩרҵ�ļ���������</strong></p>
      </div>
      <div class="bbs">
        <h3><a href="http://bbs.dedecms.com/" target="_blank">������̳</a></h3>
        <p><strong>�����²�Ʒ��Ϣ,����������,��Դ���µ���Ϣ</strong></p>
      </div>
    </div>
    <form name="form1" runat="server">
      <fieldset class="right_main">
      <legend>�û���¼</legend>     
      <dl class="setting">
        <dt>
          <label>�û���</label>
        </dt>
        <dd><span class="text_input">
            <cc1:VTextBox ID="txt_UserName" runat="server" EnableValidate="true" EnableNull="false"></cc1:VTextBox>
        </span></dd>
        <dt>
          <label>�ܡ���</label>
        </dt>
        <dd><span class="text_input">
            <cc1:VTextBox ID="txt_Userpass" runat="server" EnableValidate="true" EnableNull="false" TextMode="Password"></cc1:VTextBox>
        </span></dd>
        <dt>
          <label>��֤��</label>
        </dt>
        <dd><span class="short_input">
            <cc1:VTextBox ID="txt_VCode" runat="server" EnableValidate="true" EnableNull="false" ></cc1:VTextBox>
        </span>
          <span class="yzm">
            <img id="vdimgck" align="absmiddle" onClick="this.src=this.src+'?'" style="cursor: pointer;" alt="�����壿�������" src="/e/admin/tool/safecode.ashx"/>
          </span></dd>

        <dd>
          <asp:Button ID="btn_Login" runat="server" class="login_btn" Text="" OnClick="btn_Login_Click" />
        </dd>
      </dl>
      </fieldset>
    </form>
  </div>
  <div id="footer">&copy; 2011 �Ͼ���Ϣ���̴�ѧ ��������ѧԺ</div>
</div>
</body>
</html>