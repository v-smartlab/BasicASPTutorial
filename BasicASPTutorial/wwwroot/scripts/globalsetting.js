const rootUrl = ""; // [Empty] for localhost:82 , [/CoreMVC2] for localhost/CoreMVC2
const regDFm = /(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d/; // {dd/MM/yyyy}

$(function () {
    var bootstrapButton = $.fn.button.noConflict();
    $.fn.bootstrapBtn = bootstrapButton;

    $('.datepicker').datepicker({ dateFormat: "dd/mm/yy" });
    $(".datepicker").blur(function () {
        datebox_onblur(this, event);
    }).datepicker({ dateFormat: "dd/mm/yy" });

    $("input:text:visible:first").focus(); /* Set focus to first element */
    $('input').keypress(function (e) {
        /* Disable Submit by Enter key and focus to next element by index */
        /* ENTER PRESSED*/
        if (e.keyCode == 13) {
            /* FOCUS ELEMENT */
            var inputs = $(this).parents("form").eq(0).find(":input");
            var idx = inputs.index(this);
            if (idx == inputs.length - 1) {
                inputs[0].select()
            } else {
                inputs[idx + 1].focus(); //  handles submit buttons
                inputs[idx + 1].select();
            }
            return false;
        }
    });
});

// check date fomate datepicker
function datebox_onblur(sender, event) {
    if (!validateUSDate(sender))
        sender.value = "";
}

function validateUSDate(sender) {
    /*********************************************
     Ex. mm/dd/yyyy or mm-dd-yyyy or mm.dd.yyyy
     /^\d{1,2}(\-|\/|\.)\d{1,2}\1\d{4}$/
     **********************************************/
    //Format: dd/mm/yyyy
    var strValue = sender.value;
    var objRegExp = /^\d{1,2}(\/)\d{1,2}\1\d{4}$/;
    //check to see if in correct format
    if (!objRegExp.test(strValue))
        return false; //doesn't match pattern, bad date
    else {
        var strSeparator = "/";
        var arrayDate = strValue.split(strSeparator);
        arrayDate[0] = "00" + arrayDate[0];
        arrayDate[0] = arrayDate[0].substr(arrayDate[0].length - 2);
        arrayDate[1] = "00" + arrayDate[1];
        arrayDate[1] = arrayDate[1].substr(arrayDate[1].length - 2);
        //create a lookup for months not equal to Feb.
        var arrayLookup = {
            '01': 31, '03': 31,
            '04': 30, '05': 31,
            '06': 30, '07': 31,
            '08': 31, '09': 30,
            '10': 31, '11': 30, '12': 31
        };

        var intDay = parseInt(arrayDate[0], 10);
        var intMonth = parseInt(arrayDate[1], 10);
        var intYear = parseInt(arrayDate[2], 10);

        //if (intYear < 2200) {
        //    intYear += 543;
        //    arrayDate[2] = intYear;
        //}
        if (intYear < 1753) {
            return false;
        }
        //check if month value and day value agree
        if (arrayLookup[arrayDate[1]] != null) {
            if (intDay <= arrayLookup[arrayDate[1]] && intDay != 0) {
                sender.value = arrayDate.join("/");
                return true; //found in lookup table, good date
            }
        }
        if (intMonth == 2) {
            if (intDay > 0 && intDay < 29) {
                sender.value = arrayDate.join("/");
                return true;
            }
            else if (intDay == 29) {
                if ((intYear % 4 == 0) && (intYear % 100 != 0) ||
                    (intYear % 400 == 0)) {
                    // year div by 4 and ((not div by 100) or div by 400) ->ok
                    sender.value = arrayDate.join("/");
                    return true;
                }
            }
        }
    }
    return false; //any other values, bad date
}

function pg_SetPageNo(pn, hpn, ff, ft) { // _Paging.cshtml
    $("#" + hpn).val(pn);
    switch (ft) {
        case 'button': $("button#" + ff).click(); break;
        case 'form': $("form#" + ff).submit(); break;
        default: $("form#" + ff).submit();
    }
} 

function fnMsgDialog(msg) {
    //alert(msg);
    $("#dialog-msg").html(msg);
    // Define the Dialog and its properties.
    $("#dialog-msg").dialog({
        resizable: false,
        modal: true,
        title: "Inform...",
        height: 'auto',
        //autoOpen: false,
        width: 400,
        //zindex: 1001, // Default is 1000
        buttons: {
            "Close": function () {
                $(this).dialog('close');
            }
        }
    });
}

function fnConfirmDialog(doType, url) {
    var str;
    switch (doType) {
        case 'Delete':
            str = "ต้องการลบข้อมูล จริงหรือไม่...";
            break;
        case 'Save':
            str = "ต้องการบันทึกข้อมูล จริงหรือไม่...";
            break;
    }
    //alert('ABC');
    $("#dialog-msg").html("<br/>" + str);
    // Define the Dialog and its properties.
    $("#dialog-msg").dialog({
        resizable: false,
        modal: true,
        title: "ยืนยันการทำงาน...",
        height: 210,
        width: 360,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                switch (doType) {
                    case 'Delete':
                        //var data = $('form.frmList').serialize();
                        //window.location.replace(url + '&' + data);
                        //alert(url);
                        window.location.replace(url);
                        break;
                    case 'Save':
                        $(url).unbind('submit');
                        $(url).submit();
                        break;
                }
            },
            "No": function () {
                $(this).dialog('close');
                if (doType === 'Save') {
                    $(url).bind('submit');
                }
            }
        }
    });
}