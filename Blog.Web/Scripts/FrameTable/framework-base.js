var exceptPage = new Array("/Home/About", "/Privilege/Index", "/OnlineReview/Details", "/Home/About_Teacher", "/PaperQuality/Details", "/StudentPaper/Index");

function checkPageIsInFramework() {
    var isout = 0;
    for (var i in exceptPage) {
        var pageurl = window.location.href.toUpperCase();
        if (pageurl.indexOf(exceptPage[i].toUpperCase()) > -1) {
            return;
        }
    }
    var windowname = window.name;
    if (windowname != undefined &&
        typeof (windowname != undefined) &&
        windowname != null &&
        windowname != ""
        ) {
        var src = top.$("#" + windowname).attr("src");
        var data_id = top.$("#" + windowname).attr("data-id");
        if (src != data_id) {
            isout++;
        }
    }
    else {
        isout++;
    }
    if (isout > 0) {
        alert("当前页面必须在OMS阅卷系统框架内运行，请不要单独运行页面！");
        window.location = "/Login/Index";
    }
}
checkPageIsInFramework();
$(function () {
    checkButtonPrivilege();
});
function checkButtonPrivilege() {
    var privileges = "";
    var allprivilegbutton = $("[data-privileg]");
    for (var i = 0; i < allprivilegbutton.length; i++) {
        privileges += $(allprivilegbutton[i]).attr("data-privileg") + ",";
    }
    if (privileges != "") {
        privileges = privileges.substr(0, privileges.length - 1);
        $.post("/ClientsData/CheckButtonPrivilege", { privileges: privileges }, function (data) {
            for (var i in data) {
                if (!data[i].Value) {
                    $("[data-privileg='" + data[i].Key + "']").hide();
                }
            }
        });
    }
}