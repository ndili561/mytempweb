var reminder;
var redirect;
//var msgSession = 'Warning: Within next 3 minutes, if you do not do anything, system will refresh the login credential. Please save changed data.';
var ajaxInProgress = $('#ajaxInProgress'), timer;

$(function () {
    $('[title]').tooltip();
    $('[data-toggle="tooltip"]').tooltip();
    $.ajaxSetup({
        cache: false,
        headers: {
            'X-CSRF-Token': $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        beforeSend: function (xhr) {
            //xhr.setRequestHeader('X-CSRF-TOKEN', csrfToken);
            timer && clearTimeout(timer);
            timer = setTimeout(function () {
                $(ajaxInProgress).css('display', 'block');
            },
                300);
        },
        xhrFields: {
            withCredentials: true
        },
        complete: function () {
            clearTimeout(timer);
            $(ajaxInProgress).css('display', 'none');
            $(".panel-overlay").remove();
        },
        error: function (x, e) {
            debugger;
            console.log(x);
            console.log(e);
            if (x.status == 0) {
                displayErrorMessage('You are offline!!\n Please Check Your Network.');
            } else if (x.status == 404) {
                displayErrorMessage('Requested URL not found.');
                /*------>*/
            } else if (x.status == 550) { // <----- THIS IS MY CUSTOM ERROR CODE
                displayErrorMessage(x.responseText);
            } else if (x.status == 500) {
                displayErrorMessage('Internel Server Error.');
            } else if (e == 'parsererror') {
                displayErrorMessage('Error.\nParsing JSON Request failed.');
            } else if (e == 'timeout') {
                displayErrorMessage('Request Time out.');
            } else {
                displayErrorMessage('Unknow Error.\n' + x.responseText);
            }
            $(ajaxInProgress).css('display', 'none');
            $(".panel-overlay").remove();
        }
    });
    //handleSessionTimeOut();
});
function initializeDateControl() {
    $('.input-group.date').datepicker({
        autoclose: true,
        format: 'dd/mm/yyyy'
      
    });
}
function displaySuccessMessage(message) {
    var type = 'success';
    displayMessage(message, type, type);
}

function displayErrorMessage(message) {
    var type = 'error';
    displayMessage(message, type, 'Error');
}

function displayWarningMessage(message) {
    var type = 'warning';
    displayMessage(message, type, type);
}
function displayInfoMessage(message) {
    var type = 'info';
    displayMessage(message, type, type);
}
function displayMessage(message, type, title) {
    toastr.options = {
        closeButton: true,
        progressBar:true,
        positionClass:  'toast-top-right',
        onclick: null
    };
    var $toast = toastr[type](message, title);
}

function handleSessionTimeOut() {
    var sessionTimeReminder = (20 * 60000) - 3 * 60000; //time to remind, 3 minutes before session ends
    var sessionTimeout = (20 * 60000) - 5; //time to redirect, 5 milliseconds before session ends
    clearTimeout(reminder);
    clearTimeout(redirect);
    reminder = setTimeout(function () {
        displayErrorMessage("You are inactive in Logistics. You will be logout soon.");
    }, sessionTimeReminder);
    redirect = setTimeout(function () {
        //doRedirect();
    }, sessionTimeout);
}

function logoutUser() {
    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        var eqPos = cookie.indexOf("=");
        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
    window.location.href ="/Home/Signout";
}


function handleAjaxSuccess(divId, message) {
    $("#ajaxReturnMessage").val(message);
    $(".modal-backdrop").remove();
    $(divId).removeClass("in");
    $(divId).modal("hide");
    displaySuccessMessage(message);
    $(".btn-filter").click();
}

function showProgressWindow() {
    $loader.niftyOverlay('show');
    $(".panel-overlay").css('z-index', 9999999);
}

function showDialog(parentDivId,formId, url,title) {
    $.ajax({
        url: url,
        headers: {
            'X-CSRF-Token': $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        type: "GET",
        success: function (data) {
            if (data.message) {
                displayErrorMessage(data.message);
            } else {
                $(".modal-title").text(title);
                $(".modal-body").html(data);
                $("#" + parentDivId).modal("show");
                initializeDateControl();
                //hack to get clientside validation working
                if (formId !== '')
                    $.validator.unobtrusive.parse("#" + formId);
            }
        }
    });
    return false;
};

function submitModalForm(form, event) {
    console.log('Person save URL ' + $(form).attr('action'));
    event.preventDefault();
    event.stopImmediatePropagation();
    var model = objectifyForm(form.serializeArray());
    $.ajax({
        url: $(form).attr('action'),
        type: "POST",
        headers: {
            'X-CSRF-Token': $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: model,
        success: function (result) {
            if (result.message) {
                $(".close").click();
                $(".btn-filter").click();
                displaySuccessMessage(result.message);
            } else {
                $(".modal-body").html(result);
            }
        }
    });

}

function objectifyForm(formArray) {//serialize data function
    var propertyNames = [];
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        if (propertyNames.indexOf(formArray[i]['name'])===-1) {
            returnArray[formArray[i]['name']] = formArray[i]['value'];
            propertyNames.push(formArray[i]['name']);
        }
       
    }
    return returnArray;
}

function displayDeleteAlert(message,callbackFunction,inputParam) {
    swal({
            title: "Are you sure?",
            text: message,
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            closeOnConfirm: true,
            closeOnCancel: true
        },
        function (isConfirm) {
            if (isConfirm) {
                callbackFunction(inputParam);
            } else {
                swal("Cancelled", message, "error");
            }
        });
}

