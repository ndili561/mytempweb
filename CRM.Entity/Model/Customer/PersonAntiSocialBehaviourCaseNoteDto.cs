using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.Entity.Model.Customer
{
    public class PersonAntiSocialBehaviourCaseNoteDto : BaseDto
    {
        public int PersonAntiSocialBehaviourId { get; set; }
        public PersonAntiSocialBehaviourDto PersonAntiSocialBehaviour { get; set; }
        public string Note { get; set; }
        public DateTime? ActionDateTime { get; set; }

        private DateTime _actionDate;
        [Display(Name = "Action Date")]
        public DateTime ActionDate
        {
            get
            {
                if (_actionDate == DateTime.MinValue && ActionDateTime.HasValue)
                {
                    _actionDate = ActionDateTime.Value.Date;
                }
                return _actionDate;
            }
            set => _actionDate = value;
        }
        private string _actionTime;

        [Display(Name = "Action Time")]
        public string ActionTime
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_actionTime) && ActionDateTime.HasValue)
                {
                    _actionTime = ActionDateTime.Value.ToShortTimeString();
                }
                return _actionTime;
            }
            set => _actionTime = value;

        }
        public List<SelectListItem> ActionTimeSelectListItems { get; set; }
    }
}
