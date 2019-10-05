namespace CRM.Entity.Model.Common
{
    public class JsonEmployeeTask
    {
        public long id { get; set; }
        public int resourceId { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string className { get; set; }
        public string styleName { get; set; }
        public string allDay { get; set; }
        public string description { get; set; }
        public JsonBusinessHours businessHours { get; set; }
        public string eventColor => className;
    }

    public class JsonBusinessHours
    {
        public int[] dow { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }

}