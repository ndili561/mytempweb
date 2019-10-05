using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.DAL.Database.Entities.Employee
{
    public class ApplicationTask : BaseEntity
    {
        public int ApplicationId { get; set; }
        public WebApplication Application { get; set; }
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public Task Task { get; set; }
        public string Comment { get; set; }
    }
}
