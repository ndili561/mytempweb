﻿namespace CRM.Entity.Model.Customer
{
    public class PersonAlertCommentDto : BaseDto
    {
        public int PersonAlertId { get; set; }
        public PersonAlertDto PersonAlert { get; set; }
        public string Notes { get; set; }
    }
}
