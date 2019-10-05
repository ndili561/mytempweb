using System;
using System.ComponentModel.DataAnnotations;
using Core.Utilities.Enum;

namespace Core.Utilities
{
    public class AuditModel : BaseModel
    {
        public AuditModel()
        {
        }

        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        [Display(Name = "Event Type")]
        public EventType EventType => OldValues != null && NewValues != null
            ? EventType.Update
            : NewValues != null
                ? EventType.Insert
                : EventType.Deleted;

        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}