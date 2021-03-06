﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Database.Entities.Lookup;

namespace CRM.DAL.Database.Entities.Common
{
    public class PropertyUserTask : BaseEntity
    {
        public string LegacyTaskId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property Property { get; set; }
        public DateTime ScheduleStartTime { get; set; }
        public DateTime ScheduleEndTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }
        public int? PropertyVoidId { get; set; }
        [ForeignKey("PropertyVoidId")]
        public PropertyVoid PropertyVoid { get; set; }
        public int? TaskId { get; set; }
        [ForeignKey("TaskId")]
        public Task Task { get; set; }
        public int TaskTypeId { get; set; }
        [ForeignKey("TaskTypeId")]
        public TaskType TaskType { get; set; }
        public int TaskStatusId { get; set; }
        [ForeignKey("TaskStatusId")]
        public TaskStatus TaskStatus { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }

    }
}
