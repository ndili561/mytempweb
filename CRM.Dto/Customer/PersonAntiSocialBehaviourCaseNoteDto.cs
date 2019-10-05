using System;

namespace CRM.Dto.Customer
{
    public class PersonAntiSocialBehaviourCaseNoteDto: BaseDto
    {
        public int PersonAntiSocialBehaviourId { get; set; }
        public PersonAntiSocialBehaviourDto PersonAntiSocialBehaviour { get; set; }
        public string Note { get; set; }
        public DateTime? ActionDateTime { get; set; }
    }
}
