using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Repository;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.Employee
{
    public class UserPersonTaskCalendarFileService : IUserPersonTaskCalendarFileService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserPersonTaskCalendarFileService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private byte[] GetCalendarFile(UserDto model)
        {

            var sb = new StringBuilder();
            var DateFormat = "yyyyMMddTHHmmssZ";
            var now = DateTime.Now.ToUniversalTime().ToString(DateFormat);
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("//Microsoft Corporation//Outlook 14.0 MIMEDIR//EN");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine($"NAME:{model.Name}");
            sb.AppendLine("X-WR-CALNAME:Nabin Kumar");
            sb.AppendLine("X-PRIMARY-CALENDAR:TRUE");
            sb.AppendLine($"X-OWNER;CN=\"{model.Name}\":{model.Email}");
            sb.AppendLine($"X-WR-CALNAME:{model.Name}");
            sb.AppendLine($"DESCRIPTION:{model.Name}");
            sb.AppendLine($"X-WR-CALDESC:{model.Name}");
            sb.AppendLine("TIMEZONE-ID:Europe/London");
            sb.AppendLine("X-WR-TIMEZONE:Europe/London");
            sb.AppendLine("REFRESH-INTERVAL;VALUE=DURATION:PT12H");
            sb.AppendLine("X-PUBLISHED-TTL:PT12H");
            sb.AppendLine("COLOR:34:50:105");
            sb.AppendLine("CALSCALE:GREGORIAN");
            sb.AppendLine("METHOD:PUBLISH");
            foreach (var task in model.Tasks)
            {
                var dtStart = Convert.ToDateTime(task.ScheduleStartTime);
                var dtEnd = Convert.ToDateTime(task.ScheduleEndTime);
                sb.AppendLine("TZ:+00");
                sb.AppendLine("BEGIN:VEVENT");
                sb.AppendLine("DTSTART:" + dtStart.ToUniversalTime().ToString(DateFormat));
                sb.AppendLine("DTEND:" + dtEnd.ToUniversalTime().ToString(DateFormat));
                sb.AppendLine("DTSTAMP:" + now);
                sb.AppendLine("UID:" + task.Id);
                sb.AppendLine("CREATED:" + now);
                sb.AppendLine("X-ALT-DESC;FMTTYPE=text/html:" + task.Description);
                sb.AppendLine($"DESCRIPTION: TaskId = {task.Id} \n " + task.Description);
                sb.AppendLine("LAST-MODIFIED:" + now);
                // sb.AppendLine("LOCATION:" + res.Location);
                sb.AppendLine("SEQUENCE:0");
                sb.AppendLine("STATUS:CONFIRMED");
                if (string.IsNullOrWhiteSpace(task.Description))
                {
                    sb.AppendLine("SUMMARY: TaskId = {res.Id} \n" + task.Description);
                }
                else
                {
                    sb.AppendLine("SUMMARY: TaskId = {res.Id} \n" + task.Description);
                }

                sb.AppendLine("TRANSP:OPAQUE");
                sb.AppendLine("END:VEVENT");
            }
            sb.AppendLine("END:VCALENDAR");
            var calendarBytes = Encoding.UTF8.GetBytes(sb.ToString());
            return calendarBytes;
        }

        public async Task<UserDto> GetAsync(int employeeId)
        {
            var model = _mapper.Map<UserDto>(await _repository.CRMContext.Users.Include(x => x.Tasks).AsNoTracking().FirstOrDefaultAsync(x => x.Id == employeeId));
            model.FileContent = GetCalendarFile(model);
            return model;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(UserDto entityDto)
        {
            var entity = _mapper.Map<User>(entityDto);
            if (entityDto.Id > 0)
            {
                if (entityDto.FileContent != null)
                {
                    using (var ms = new MemoryStream(entityDto.FileContent))
                    {
                        var calendars = Calendar.Load(ms);
                        var occurrences = calendars.GetOccurrences(DateTime.Today, DateTime.Today.AddDays(30));
                        foreach (var occurrence in occurrences)
                        {
                            var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                            var startTime = TimeZoneInfo.ConvertTime(occurrence.Period.StartTime.AsSystemLocal, TimeZoneInfo.Local, britishZone);
                            var endTime = TimeZoneInfo.ConvertTime(occurrence.Period.EndTime.AsSystemLocal, TimeZoneInfo.Local, britishZone);
                            var rc = occurrence.Source as IRecurringComponent;
                            if (rc != null)
                            {
                                int taskId;
                                int.TryParse(rc.Uid, out taskId);
                                if (taskId == 0)
                                {
                                    var taskEntity = new UserPersonTask
                                    {
                                        Comment = "outlook",
                                        UserId = entityDto.Id,
                                        ScheduleStartTime = startTime,
                                        ScheduleEndTime = endTime,
                                        Description = rc.Summary,
                                        TaskTypeId = 1,
                                        TaskStatusId = 2
                                    };
                                    await _repository.SaveAndReturnEntityAsync(taskEntity);
                                    taskEntity.Description = "TaskId: " + taskEntity.Id + " | " + rc.Summary;
                                    await _repository.SaveAndReturnEntityAsync(taskEntity);
                                }
                                else
                                {
                                    var task = _repository.CRMContext.UserPersonTasks.AsNoTracking().FirstOrDefault(x => x.Id == taskId);
                                    if (task != null)
                                    {
                                        task.Comment = "outlook";
                                        task.UserId = entityDto.Id;
                                        task.ScheduleStartTime = startTime;
                                        task.ScheduleEndTime = endTime;
                                        task.Description = "TaskId: " + task.Id + " | " + rc.Summary;
                                        task.TaskTypeId = 1;
                                    }
                                    await _repository.SaveAndReturnEntityAsync(task);
                                }
                            }
                        }

                    }

                }
            }
            entity.SuccessMessage = "The calendar file has been uploaded successfully.";
            return entity;
        }


    }
}
