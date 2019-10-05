$(function () {
    initializeCalendarFileUpload();
    attachCalendar();
    onTaskStatusChange();
});
function displayAddCalendarTask(contactId, scheduleDate, scheduleStartTime, scheduleEndTime, taskType) {
    var url = "/ContactCalendar/CalendarTask/Create";
    showDialog("divCalendarTaskParent", "divCalendarTaskChild", 'Create', { contactId: contactId, scheduleDate: scheduleDate, scheduleStartTime: scheduleStartTime, scheduleEndTime: scheduleEndTime, TaskTypeName: taskType}, 'frmCalendarTask', url);
}

function displayEditCalendarTask(taskId) {
    var url = "/ContactCalendar/CalendarTask/Edit";
    showDialog("divCalendarTaskParent", "divCalendarTaskChild", 'Edit', { id: taskId }, 'frmCalendarTask', url);
    $(".modal-content").css("width", "600px");
}
function handleCalendarTaskCreate(data) {
    if (data.success) {
        handleAjaxSuccess("#divCalendarTaskParent", data.message);
    }
    else {
        $('#divCalendarTaskChild').replaceWith(data);
    }
}
function initializeCalendarFileUpload() {
    if ($('#fileupload').length > 0) {
        $('#fileupload').fileupload({
            dataType: 'json',
            add: function (e, data) {
                var goUpload = true;
                var uploadFile = data.files[0];
                if ($("#ContactId").val() == "") {
                    displayErrorMessage('You must select contact first');
                    goUpload = false;
                }
                if (!(/\.(|ics)$/i).test(uploadFile.name)) {
                    displayErrorMessage('You must select an ics file only');
                    goUpload = false;
                }
                if (goUpload == true) {
                    data.formData = { contactId: $("#ContactId").val() };
                    data.submit();
                }
            },
            done: function (e, data) {
                if (data.result.success === true) {
                    displaySuccessMessage(data.result.message);
                }
                else {
                    displayErrorMessage(data.result.message);
                }
            }
        });   
    }
}


var attachCalendar = function () {
    $('.datetime').datepicker();
    $('.datetime').datepicker();
}

function onTaskStatusChange() {
    var taskStatus = $("#TaskStatus").val();
    if (taskStatus === "50" || taskStatus === "30") {
        $('#divUpdateDateAndTime').show();  
        if (taskStatus === "50") {
            $('#divCompletedDateAndTime').show();  
            $('#divScheduleDateAndTime').hide();  
        } else if (taskStatus === "30") {
            $('#divScheduleDateAndTime').show();  
            $('#divCompletedDateAndTime').hide(); 
        }
    } else {
        $('#divUpdateDateAndTime').hide();   
    }
}
function confirmDeleteCalendarTask(taskId) {
    bootbox.dialog({
        size: 'small',
        message: "Are you sure you want to delete?",
        buttons: {
            success: {
                label: "Ok",
                className: "btn-success",
                callback: function () {
                    if ($.isNumeric(taskId)) {
                        debugger;
                        $.ajax({
                            type: 'POST',
                            url: "/ContactCalendar/CalendarTask/DeleteContactCalendarTask",
                            data: { taskId: taskId },
                            success: function(returnResult) {
                                $('#divLogisticscalendar').fullCalendar('removeEvents', taskId);
                                displaySuccessMessage(returnResult.message);
                            }
                        });
                    } else {
                        $('#divLogisticscalendar').fullCalendar('removeEvents', taskId);
                    }
                }
            },
            danger: {
                label: "Cancel",
                className: "btn-danger",
                callback: function () {
                    return true;
                }
            }
        }
    });
    $(".modal-content").css("width", "400px");
    $(".modal-dialog").css("min-width", "1%");
}