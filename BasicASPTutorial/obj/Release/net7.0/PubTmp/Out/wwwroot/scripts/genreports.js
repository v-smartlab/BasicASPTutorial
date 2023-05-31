$(document).ready(function () {
    $(".btnGenerate").click(function () {
        ReportManager.GenerateReport();
    })
    $("#btnGen901").click(function () {
        ReportManager.GenerateReport901();
    })
});

var ReportManager = {
    GenerateReport: function () {
        /*
        var VarId = $("#Id").val();
        var VarName = $("#Name").val();
        var VarScore = $("#Score").val();
        var VarDateUpDate = $("#DateUpDate").val();       
        var repType = $("#repType").val();
        var data = '?repType=' + repType + '&Id=' + VarId + ' &Name=' + VarName + ' &Score=' + VarScore + ' &DateUpDate=' + VarDateUpDate;
        */
       //alert(data);
        var data = $('form#formstudent').serialize();
        win = window.open(rootUrl + "/GenReport/StudentList?" + data, '_blank');
        //win = window.open('/GenReport/AmuletList' + data, '_ifDemo'); // iFrame
        win.window.focus();
    },
    GenerateReport901: function () {
        var amlStd = $("#amlStd").val();
        var amlLsd = $("#amlLsd").val();
        var ddl1 = ''; //$("#Select1").val();
        var data = '?us[std]=' + amlStd + '&us[lsd]=' + amlLsd + '&us[Report_PRODUCT_TYPE]=' + ddl1;
        //alert(data);
        var newUrl = 'http://localhost/RptServer/Reports/Rpt_FZ901.aspx' + data;
        win = window.open(newUrl, '_blank');
        win.window.focus();
    },
};