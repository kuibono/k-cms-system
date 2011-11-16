/* 设为首页 */
function setMyHome() {
  if (document.all) {
    document.body.style.behavior='url(#default#homepage)';
 	document.body.setHomePage(location.href);
 
  } else if (window.sidebar) {
    if(window.netscape) {
        try {  
            netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");  
        } catch (e) {  
  		    alert( "该操作被浏览器拒绝，如果想启用该功能，请在地址栏内输入 about:config,然后将项 signed.applets.codebase_principal_support 值该为true" );  
        }
    } 
    var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components. interfaces.nsIPrefBranch);
    prefs.setCharPref('browser.startup.homepage',location.href);
  }
}

/* 添加收藏  */
function addFav() {
    if (document.all) {
		window.external.addFavorite(location.href, document.title);
} else if (window.sidebar) {
		window.sidebar.addPanel(document.title, location.href, "");
	}
} 

$(function(){
    $("#div_login").load("/e/member/LoginForm.aspx");
})