$(function () {
    attachCalendar();
    onTaskStatusChange();
});
function displayAddCalendarItemView(userId, scheduleDate, scheduleStartTime, scheduleEndTime, taskType) {
    var url = "/Calendar/Create?userId=" + userId + "&scheduleDate=" + scheduleDate + "&scheduleStartTime=" + scheduleStartTime + "&scheduleEndTime=" + scheduleEndTime + "&TaskTypeSelected=" + taskType;
    showDialog("divTaskParent", "frmTaskParent", url, "Add Calendar task");
}

function displayEditCalendarItemView(taskId) {
    var url = "/Calendar/Edit?id=" + taskId;
    showDialog("divTaskParent", 'frmTaskParent', url, "Edit Calendar task");
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
    displayDeleteAlert("Are you sure you want to delete this task?", deleteCalendarTask, taskId);
}

function deleteCalendarTask(taskId) {
    $.ajax({
        type: 'GET',
        url: "/Calendar/DeleteUserPersonTask",
        data: { taskId: taskId },
        success: function (returnResult) {
            if (returnResult.success) {
                $('#divCrmCalendar').fullCalendar('removeEvents', taskId);
                swal("Deleted!", returnResult.message, "success");  
            } else {
                swal({
                    title: "Delete failed",
                    text: returnResult.message
                });
            }
       
        }
    });
}
