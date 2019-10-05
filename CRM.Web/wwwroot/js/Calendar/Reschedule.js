var contacts = [];
var tasks = [];
$(document).ready(function () {
    getContactScheduling();
});

function getContactScheduling() {
    var employeeIds = JSON.stringify($("#EmployeeIds").val());
    var taskTypeId = $("#TaskTypeId").val();
    if (!$.isNumeric(taskTypeId)) {
        taskTypeId = 0;
    }
    $.ajax({
        type: 'GET',
        data: { employeeIds: employeeIds, taskTypeId: taskTypeId },
        url: "/Calendar/GetSchedule",
        success: function (data) {
            $("#divCrmSchedule").replaceWith(data);
        }
    });
}

function reloadResheduling() {
    $('#divCrmSchedule').fullCalendar({
        schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
        now: new Date(),
        editable: true,
        selectable: true,
        aspectRatio: 1.35,
        scrollTime: '07:00',
        header: {
            left: 'today prev,next',
            center: 'title',
            right: 'timelineDay,timelineTenDay,timelineMonth,timelineYear'
        },
        defaultView: 'timelineDay',
        views: {
            timelineDay: {
                buttonText: ':15 slots',
                slotDuration: '00:15'
            },
            timelineTenDay: {
                type: 'timeline',
                duration: { days: 10 }
            }
        },
        navLinks: true,
        resourceAreaWidth: '15%',
        resourceLabelText: 'Employees',
        resources: contacts,
        events: tasks,
        eventRender: function (event, element, view) {
            var contact = $.grep(contacts, function (e) { return e.id == event.resourceId; });
            $('.fc-timeline-event').addClass(contact[0].eventColor);
            $('.fc-timeline-event').click();
        },
        eventClick: function (event, delta, revertFunc) {
            displayEditCalendarItemView(event._id);
            if (typeof (revertFunc) ==="function") {
                revertFunc();
            }
        },
        eventDrop: function (event, delta, revertFunc) {
            displayResheduleWindow(event._id, event.resourceId, event.title, event.start.format(), event.end.format(), revertFunc);
        }
    });
}

function displayResheduleWindow(userPersonTaskId, scheduleEmployeeId, title, scheduleStartDate, scheduleEndDate, revertFunc) {
    var message = "Are you sure about this reschedule " + title + " on " + scheduleStartDate;
    displayRescheduleAlert(message, userPersonTaskId, scheduleEmployeeId, scheduleStartDate, scheduleEndDate, revertFunc);
}
function displayRescheduleAlert(message, userPersonTaskId, employeeId, scheduleStartDate, scheduleEndDate, revertFunc) {
    swal({
            title: "Are you sure?",
            text: message,
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, reschedule it!",
            cancelButtonText: "No, cancel!",
            closeOnConfirm: true,
            closeOnCancel: true
        },
        function (isConfirm) {
            if (isConfirm) {
                rescheduleTask(userPersonTaskId, employeeId, scheduleStartDate, scheduleEndDate);
            } else {
                revertFunc();
                swal("Cancelled", message, "error");
            }
        });
}
function rescheduleTask(userPersonTaskId, employeeId, scheduleStartDate, scheduleEndDate) {
    $.ajax({
        type: 'POST',
        url: "/Calendar/RescheduleCalendarTask",
        data: { userPersonTaskId: userPersonTaskId, employeeId: employeeId, scheduleStartDate: scheduleStartDate, scheduleEndDate: scheduleEndDate },
        success: function (returnResult) {
            displaySuccessMessage(returnResult.message);
            getContactScheduling();
        }
    });
}