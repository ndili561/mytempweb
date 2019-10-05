var contacts = [];
var tasks = [];
$(document).ready(function () {
    getContactScheduling();
});

function getContactScheduling() {
    var contactId = $("#ContactId").val();
    var taskTypeId = $("#CalendarTaskTypeId").val();
    if (!$.isNumeric(contactId)) {
        contactId = 0;
    }
    if (!$.isNumeric(taskTypeId)) {
        taskTypeId = 0;
    }
    $.ajax({
        type: 'GET',
        data: { contactId: contactId, taskTypeId: taskTypeId},
        url: "/ContactCalendar/CalendarTask/GetCalendarSchedule",
        success: function (data) {
            $("#divLogisticsSchedule").replaceWith(data);
            reloadResheduling();
        }
    });
}

function reloadResheduling() {
    $('#divLogisticsSchedule').fullCalendar({
        schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
        now: new Date(),
        editable: true,
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
        resourceLabelText: 'Users',
        resources: contacts,
        events: tasks,
        eventRender: function (event, element, view) {
            var contact = $.grep(contacts, function (e) { return e.id == event.resourceId; });
            $('.fc-timeline-event').addClass(contact[0].eventColor);
            $('.fc-timeline-event').click();
        },
        eventClick: function (event, delta, revertFunc) {
            revertFunc();
            displayEditCalendarTask(event._id);
        },
        eventDrop: function (event, delta, revertFunc) {
            displayResheduleWindow(event._id, event.resourceId, event.title, event.start.format(), revertFunc);
        }
    });
}

function displayResheduleWindow(taskId, resourceId, title, scheduleDate, revertFunc) {
    var scheduleTaskId = taskId;
    var scheduleContactId = resourceId;
    var newScheduleDate = scheduleDate;
    bootbox.dialog({
        size: 'small',
        message: "Are you sure about this reschedule " + title + " on " + scheduleDate,
        buttons: {
            success: {
                label: "Ok",
                className: "btn-success",
                callback: function () {
                    rescheduleTask(scheduleTaskId, scheduleContactId, newScheduleDate);
                }
            },
            danger: {
                label: "Cancel",
                className: "btn-danger",
                callback: function () {
                    revertFunc();
                }
            }
        }
    });
}

function rescheduleTask(taskId, contactId, scheduleDate) {
        $.ajax({
            type: 'POST',
            url: "/ContactCalendar/CalendarTask/RescheduleCalendarTask",
            data: { taskId: taskId, contactId: contactId, scheduleDate: scheduleDate },
            success: function (returnResult) {
                displaySuccessMessage(returnResult.message);
            }
        });
}