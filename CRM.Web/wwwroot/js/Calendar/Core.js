var userCalendarTasks = [];
var eventsForCookie = [];
$(document).ready(function () {
    onEmployeeIdChange();
    $("#EmployeeId").change(function () {
        onEmployeeIdChange();
    });
});
function hideDisplayCalendar() {
    //var employeeId = $("#EmployeeId").val();
    //if (employeeId === undefined || employeeId === null || employeeId === '') {
    //    $('#divCrmCalendar').hide();
    //    $('#divCalendarAction').hide();
    //    $('#divCalendarTasks').hide();
    //} else {
    //    $('#divCrmCalendar').fullCalendar('refetchEvents');
    //    $('#divCrmCalendar').show();
    //    $('#divCalendarAction').show();
    //    $('#divCalendarTasks').show();
    //}
}
function onEmployeeIdChange() {
    var employeeId = $("#EmployeeId").val();
    if (employeeId !== undefined && employeeId !== null && employeeId !== '') {
        $.ajax({
            url: "/Calendar/GetCalendarTask",
            data: { employeeId: employeeId },
            type: "GET",
            success: function (data) {
              
                $("#divCrmCalendar").replaceWith(data);
                refreshCalendar();
            }
        });
    } else {
        userCalendarTasks = [];
        eventsForCookie = []; 
        hideDisplayCalendar();
    }
    $('#divCrmCalendarEvents .fc-event').each(function () {
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
    $('#divCrmCalendar').fullCalendar({
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
        defaultView: 'agendaDay',
        eventLimit: true, // allow "more" link when too many events
        events: userCalendarTasks,
        eventRender: function (event, element, view) {
            $(element).attr("id", event._id);
            if ($.isNumeric(event.id)) {
                var description="";
                if (event.description !== null) {
                    description = event.description;
                }
               // element.find('.fc-title').append("<div class='col-md-3'><a  style='zIndex: 999999' class='text-bold' href='/ContactCalendar/CalendarTask/Edit?id=" + event.id+"'>" + event.description+"</a></div>");
                element.find('.fc-content').append("<div class='pull-left m-r'>" + description + "</danger></div><div class='pull-right m-r'><button type='button' class='btn btn-success pull-right' onclick='displayEditCalendarItemView(" + event.id + ")'>Edit</button></div>");
            } else {
                if (view.name == 'agenda' || view.name == 'agendaDay') {
                    var taskType = $(element["0"]).find('.fc-title').text();
                    var scheduleDate = moment(view.start).toISOString();
                    var newTaskHtml = "<div id='btnAddCalendarTask' class='pull-right m-r'><button type='button' class='btn btn-success' onclick=\"displayAddTaskNamePopup(\'" + scheduleDate + "\')\"> Create </button></div>";
                    element.find('.fc-content').append(newTaskHtml);  
                }
            }
            $('.fc-content').addClass("col-xs-12");
            element.find('.fc-time').addClass("col-xs-1");
            element.find('.fc-title').addClass("col-xs-6");
            if (view.name == 'listDay') {
                element.append("<div style='cursor:pointer' class='col-xs-1 text-danger closeon fa fa-remove fa-2x'></div>");
            } else {
                element.find(".fc-content").prepend("<div style='cursor:pointer' class='col-xs-1 text-danger closeon fa fa-remove fa-2x'></div>");
            }
            element.find(".closeon").on('click', function () {
                confirmDeleteCalendarTask(event._id);
            });
            //"fc-event-container"
        }
    
        });
    hideDisplayCalendar();

}

function displayAddTaskNamePopup(scheduleDate) {
    var scheduleStartTime;
    var scheduleEndTime;
    var scheduleTime = $("div.fc-time.col-xs-1").attr('data-full');
    var taskType = $("div.fc-title.col-xs-6").text();
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
    displayAddCalendarItemView($("#EmployeeId").val(), scheduleDate, scheduleStartTime, scheduleEndTime, taskType);
    
}
function savecalendar() {
    var employeeId = $("#EmployeeId").val();
    var eventsFromCalendar = $('#divCrmCalendar').fullCalendar('clientEvents');
    var startDate = moment($('#divCrmCalendar').fullCalendar('getView').start).toISOString();
    var endDate = moment($('#divCrmCalendar').fullCalendar('getView').end).toISOString();
    var calendarViewType = $('#divCrmCalendar').fullCalendar('getView').name;
    $.each(eventsFromCalendar, function (index, value) {
        var endTime = $("#" + value._id).find(".fc-time span").text();
        var event = {};
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
    var model = {
        EmployeeId: employeeId,
        CalendarViewType: calendarViewType,
        CalendarStartDate: startDate,
        CalendarEndDate: endDate,
        Events: JSON.stringify(eventsForCookie)
    };
    $.ajax(
        {
            url: '/Calendar/UpdateCalender',
            type: 'POST',
            contentType: "application/json",
            data: JSON.stringify(model),
            success: function (response) {
                if (response.success) {
                    displayErrorMessage(response.message);
                    onEmployeeIdChange();
                } else {
                    displaySuccessMessage(response.message);
                }
                userCalendarTasks = [];
                eventsForCookie = [];
            },
            headers: {
                'X-CSRF-Token': $('input:hidden[name="__RequestVerificationToken"]').val()
            }

        });

};

function emailCalendar() {
    var employeeId = $('#EmployeeId').val();
    if ($('#EmployeeId').val() === '') {
        displayErrorMessage('Please select contact before email the calendar.');
        return false;
    }
    $.ajax({
        url: '/Calendar/EmailCrmCalendar?employeeId=' + employeeId,
        type: 'POST',
        data: {employeeId: employeeId},
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
    if ($('#EmployeeId').val()==='') {
        displayErrorMessage('Please select contact before exporting the calendar.');
        return false;
    }
    window.location.href = '/Calendar/ExportCalendar';
    return true;
}