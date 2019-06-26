
function getExplorer() {//判断浏览器类型
    var explorer = window.navigator.userAgent;
//ie
    if (explorer.indexOf("rv:11") > -1) {
         return 'ie';
    }
//firefox
    else if (explorer.indexOf("Firefox") >= 0) {
        return 'Firefox';
    }
//Chrome
    else if (explorer.indexOf("Chrome") >= 0) {
        return 'Chrome';
    }
//Opera
    else if (explorer.indexOf("Opera") >= 0) {
        return 'Opera';
    }
//Safari
    else if (explorer.indexOf("Safari") >= 0) {
        return 'Safari';
    }
}
    function toExcel(tableid) {
        if (getExplorer() == 'ie') {
            try {
                var allStr = "";
                var str = "";
                if (tableid != null && tableid != "") {
                    str = getTblData(tableid);
                }
                if (str != null) {
                    allStr += str;
                }else {
                    alert("导出的表不存在！");
                return;
            }
            var fileName = '报表';
                doFileExport(fileName, allStr);
            }
            catch (e) {
                alert("导出发生异常:" + e.name + "->" + e.description + "!");
            }
        }
        else {
        tableToExcel(tableid)
    }
    }

    var tableToExcel = (function () {
        var uri = 'data:application/vnd.ms-excel;base64,',
            template = '<html xmlns: o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x: ExcelWorkbook><x: ExcelWorksheets><x: ExcelWorksheet><x: Name>{worksheet}</x: Name><x: WorksheetOptions><x: DisplayGridlines/></x: WorksheetOptions></x: ExcelWorksheet ></x: ExcelWorksheets ></x: ExcelWorkbook ></xml >< ![endif]-- ></head > <body><table>{table}</table></body></html > ',
base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) },
    format = function (s, c) {
        return s.replace(/{(\w+)}/g,
            function (m, p) { return c[p]; })
    }
return function (table, name) {
    if (!table.nodeType) table = document.getElementById("Table")
    var ctx = { worksheet: "en " || 'Worksheet', table: table.innerHTML }
    window.location.href = uri + base64(format(template, ctx))
}
    }) ()

function getTblData(inTbl, inWindow) {
    var rows = 0;
    var tblDocument = document;
    /*if (!!inWindow && inWindow != "") {
    if (!document.all(inWindow)) {
    return null;
    }
    else {
    tblDocument = eval(inWindow).document;
    }
    }*/
    var curTbl = tblDocument.getElementById(inTbl);
    if (curTbl.rows.length > 65000) {
        alert('源行数不能大于65000行');
        return false;
    }
    if (curTbl.rows.length <= 1) {
        alert('数据源没有数据');
        return false;
    }
    var outStr = "";
    if (curTbl != null) {
        for (var j = 0; j < curTbl.rows.length; j++) {
            for (var i = 0; i < curTbl.rows[j].cells.length; i++) {
                if (i == 0 && rows > 0) {
                    outStr += " \t";
                    rows -= 1;
                }
                var tc = curTbl.rows[j].cells[i];
                if (j > 0 && tc.hasChildNodes() && tc.firstChild.nodeName.toLowerCase() == "input") {
                    if (tc.firstChild.type.toLowerCase() == "checkbox") {
                        if (tc.firstChild.checked == true) {
                            outStr += "是" + "\t";
                        }
                        else {
                            outStr += "否" + "\t";
                        }
                    }
                }
                else {

                    outStr += " " + curTbl.rows[j].cells[i].innerText + "\t";
                }
                if (curTbl.rows[j].cells[i].colSpan > 1) {
                    for (var k = 0; k < curTbl.rows[j].cells[i].colSpan - 1; k++) {
                        outStr += "\t ";
                    }
                }
                if (i == 0) {
                    if (rows == 0 && curTbl.rows[j].cells[i].rowSpan > 1) {
                        rows = curTbl.rows[j].cells[i].rowSpan - 1;
                    }
                }
            }
            outStr += "\r\n";
        }
    }
    else {
        outStr = null;
        alert(inTbl + "不存在!");
    }
    return outStr;
}

function doFileExport(inName, inStr) {
    var xlsWin = null;
    if (!!document.all("glbHideFrm")) {
        xlsWin = glbHideFrm;
    }
    else {
        var width = 1;
        var height = 1;
        var openPara = "left=" + (window.screen.width + width / 2)
            + ",top=" + (window.screen.height / 2 + height / 2)
            + ",scrollbars=no,width=" + width + ",height=" + height;
        xlsWin = window.open("", "", openPara);
    }
    xlsWin.document.write(inStr);
    xlsWin.document.close();
    xlsWin.document.execCommand('Saveas', true, inName);
    xlsWin.close();
}
  
