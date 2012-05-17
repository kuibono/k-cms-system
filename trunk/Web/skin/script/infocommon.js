var tj = function (id) {
    $.getJSON("/e/tool/BookOperate.aspx?action=tj&jsonpostback=?&id=" + id, function (json) {
        $("#tjcount").text(json.data);
    })
}