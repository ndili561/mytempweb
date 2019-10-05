var userCalendarTasks = [];
var eventsForCookie = [];
$(document).ready(function () {
    onContactIdChange();
});
function hideDisplayCalendar() {
    var contactId = $("#ContactId").val();
    if (contactId === undefined || contactId === null || contactId === '') {
        $('#divLogisticscalendar').hide();
        $('#divCalendarAction').hide();
        $('#divCalendarTasks').hide();
    } else {
        $('#divLogisticscalendar').fullCalendar('refetchEvents');
        $('#divLogisticscalendar').show();
        $('#divCalendarAction').show();
        $('#divCalendarTasks').show();
    }
}
function onContactIdChange() {
    var contactId = $("#ContactId").val();
    if (contactId !== undefined && contactId !== null && contactId !== '') {
        $.ajax({
            url: "/ContactCalendar/CalendarTask/GetCalendarTasks",
            data: { contactId: contactId },
            type: "GET",
            success: function (data) {
                $("#divLogisticscalendar").replaceWith(data);
                refreshCalendar();
            }
        });
    } else {
        userCalendarTasks = [];
        eventsForCookie = []; 
        hideDisplayCalendar();
    }
    $('#divLogisticsCalendarEvents .fc-event').each(function () {
        // store data so the calendar knows to render an event upon drop
        $(this).data('event', {
            title: $.trim($(this).text()), // use the element's text as the event title
            stick: true, // maintain when user navigates (see docs on the renderEvent method)
            className: $(this).data('class')

        });


        // make the event draggable using jQuery UI
        $(this).draggable({
            zIndex: 99999,
            revert: true,      // will cause the event to go back to its
            revertDuration: 0  //  original position after the drag
        });
    });
}

function refreshCalendar() {
    $('#divLogisticscalendar').fullCalendar({
        schedulerLicenseKey: 'CC-Attribution-NonCommercial-NoDerivatives',
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        editable: true,
        droppable: true, // this allows things to be dropped onto the calendar
        drop: function () {
            // is the "remove after drop" checkbox checked?
            if ($('#drop-remove').is(':checked')) {
                // if so, remove the element from the "Draggable Events" list
                $(this).remove();
            }
        },
        defaultDate: new Date(),
        eventLimit: true, // allow "more" link when too many events
        events: userCalendarTasks,
        eventRender: function (event, element, view) {
            $(element).attr("id", event._id);
            if (event.description !== undefined) {
               // element.find('.fc-title').append("<div class='col-md-3'><a  style='zIndex: 999999' class='text-bold' href='/ContactCalendar/CalendarTask/Edit?id=" + event.id+"'>" + event.description+"</a></div>");
                element.find('.fc-title').append("<div class='col-md-3'><a style='zIndex: 999999' class='text-lg text-bold' href='#' onclick='displayEditCalendarTask(" + event.id + ")'>" + event.description +"</a></div>");
            } else {
                if (view.name == 'agendaDay') {
                    var taskType = "";
                    if (element["0"].innerText.length > 12) {
                        taskType = element["0"].innerText.substring(12, element["0"].innerText.trim().length);
                    } else {
                        taskType = element["0"].innerText.substring(5, element["0"].innerText.trim().length);
                    }
                    var scheduleDate = moment(view.start).toISOString();
                    var scheduleTime = element.find(".fc-time")["0"].innerText;
                    var newTaskHtml = "<div class='col-xs-6'><a href='#' class='text-2x text-bold text-success' style='zIndex: 999999;' onclick=\"displayAddTaskNamePopup(\'" + scheduleDate + "\',\'" + scheduleTime + "\',\'" + taskType + "\')\"> Save </a></div>";
                    element.find('.fc-content').append(newTaskHtml);  
                }
            }
            $('.fc-content').addClass("col-xs-12");
            element.find('.fc-time').addClass("col-xs-1");
            element.find('.fc-title').addClass("col-xs-6");
            if (view.name == 'listDay') {
                element.append("<div style='cursor:pointer' class='col-xs-1 text-danger closeon fa fa-remove fa-2x' style='zIndex: 999999'></div>");
            } else {
                element.find(".fc-content").prepend("<div style='cursor:pointer' class='col-xs-1 text-danger closeon fa fa-remove fa-2x' style='zIndex: 999999'></div>");
            }
            element.find(".closeon").on('click', function () {
                confirmDeleteCalendarTask(event._id);
            });
        }
    });
    hideDisplayCalendar();

}

function displayAddTaskNamePopup(scheduleDate, scheduleTime, taskType) {
    var scheduleStartTime;
    var scheduleEndTime;
    if (scheduleTime.indexOf('-') !== -1) {
        scheduleStartTime = scheduleTime.split('-')[0];
        scheduleEndTime = scheduleTime.split('-')[1];
    } else {
        scheduleStartTime = scheduleTime;
        var hour = scheduleTime.split(':')[0];
        var minute = scheduleTime.split(':')[1];
        var time = moment(hour + ':' + minute, 'HH:mm');
        time.add(120, 'm');
        scheduleEndTime = time.toISOString();;
    }
    displayAddCalendarTask($("#ContactId").val(), scheduleDate, scheduleStartTime, scheduleEndTime, taskType);
    
}
function savecalendar(contactId) {
    var eventsFromCalendar = $('#divLogisticscalendar').fullCalendar('clientEvents');
    var startDate = moment($('#divLogisticscalendar').fullCalendar('getView').start).toISOString();
    var endDate = moment($('#divLogisticscalendar').fullCalendar('getView').end).toISOString();
    var calendarViewType = $('#divLogisticscalendar').fullCalendar('getView').name;
    $.each(eventsFromCalendar, function (index, value) {
        var endTime = $("#" + value._id).find(".fc-time span").text();
        var event = new Object();
        event.id = value.id;
        event.start = value.start;
        if (value.end === undefined || value.end === null) {
            if (endTime.indexOf('-') > -1) {
                event.end = endTime.split('-')[1];
            } else {
                var hour = endTime.split('-')[0].split(':')[0];
                var minute = endTime.split('-')[0].split(':')[1];
                var time = moment(hour + ':' + minute, 'HH:mm');
                time.add(120, 'm');
                event.end = time;
            }
        } else {
            event.end = value.end; 
        }
       
        event.title = value.title;
        event.allDay = value.allDay;
        eventsForCookie.push(event);
    });

    $.ajax(
        {
            url: '/ContactCalendar/CalendarTask/UpdateCalender',
            type: 'POST',
            data: {
                contactId: contactId,
                calendarViewType: calendarViewType,
                calendarStartDate: startDate,
                calendarEndDate: endDate ,
                model: JSON.stringify(eventsForCookie)
            },
            success: function (response) {
                if (response.success) {
                    displayErrorMessage(response.message);
                    onContactIdChange();
                } else {
                    displaySuccessMessage(response.message);
                }
                userCalendarTasks = [];
                eventsForCookie = [];
            }
        });

};

function emailCalendar() {
    var contactId = $('#ContactId').val();
    if ($('#ContactId').val() === '') {
        displayErrorMessage('Please select contact before email the calendar.');
        return false;
    }
    $.ajax({
        url: '/ContactCalendar/CalendarTask/EmailLogisticsCalendar?contactId=' + contactId,
        type: 'POST',
        data: {contactId: contactId},
        success: function (response) {
            if (response.success) {
                displayErrorMessage(response.message);
            } else {
                displaySuccessMessage(response.message);
            }
        }
     });
    return true;
}
function downloadCalendar() {
    if ($('#ContactId').val()==='') {
        displayErrorMessage('Please select contact before exporting the calendar.');
        return false;
    }
    window.location.href = '/ContactCalendar/CalendarTask/ExportCalendar';
    return true;
}