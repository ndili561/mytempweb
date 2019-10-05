using System;
using System.ComponentModel;
using System.Linq;

namespace CRM.Entity.Model.Common
{
    public class RepairDto
    {
        [DisplayName("Repair Id")]
        public long RepairId { get; set; }

        [DisplayName("Repair Line Id")]
        public int RepairLineId { get; set; }

        [DisplayName("Visit Id")]
        public long? VisitId { get; set; }
        [DisplayName("Operative Code")]

        public string OperativeCode { get; set; }
        [DisplayName("Operative Name")]

        public string OperativeName { get; set; }
        [DisplayName("Operative Mobile Number")]

        public string OperativeMobileNumber { get; set; }

        [DisplayName("Property Reference")]
        public string PropertyReference { get; set; }
        [DisplayName("Property Type")]
        public string PropertyType { get; set; }
        [DisplayName("Address1")]

        public string Address1 { get; set; }
        [DisplayName("Address2")]

        public string Address2 { get; set; }
        [DisplayName("Address3")]
        public string Address3 { get; set; }
        [DisplayName("Address4")]
        public string Address4 { get; set; }
        [DisplayName("PostCode")]
        public string PostCode { get; set; }

        public string ManagementAreaDescription { get; set; }
        [DisplayName("Repair Trade")]
        public string RepairTrade { get; set; }
        [DisplayName("Repair Location")]
        public string RepairLocation { get; set; }
        [DisplayName("Repair Description")]
        public string RepairDescription { get; set; }
        [DisplayName("Arrival Date")]
        public DateTime? ArrivalDate { get; set; }
        [DisplayName("Access Notes")]
        public string AccessNotes { get; set; }
        [DisplayName("AsbestosCount")]
        public int AsbestosCount { get; set; }
        [DisplayName("Latitude")]
        public virtual double? Latitude { get; set; }
        [DisplayName("Longitude")]
        public virtual double? Longitude { get; set; }
        public int? LineManagerId { get; set; }

        public string Trades { get; set; }
        public int? TradePersonStatusId { get; set; }
        public string VisitStatus { get; set; }//COMP,IJOB('Site' ),NOAC,NOAG,XSIT('In trans' )

        public string JobStatus
        {
            get
            {
                switch (VisitStatus)
                {
                    case "ABAN":
                      return "Abandon";
                    case "CANC":
                        return "Cancelled";
                    case "CARD":
                        return "Carded";
                    case "COMP":
                        return "Completed";
                    case "HOLD":
                        return "On Hold";
                    case "IJOB":
                        return "In Job";
                    case "MATA":
                        return "Materials Arrived";
                    case "MATR":
                        return "Materials required";
                    case "OPEN":
                        return "Repair is open";
                    case "PART":
                        return "Repair part complete";
                    case "RECD":
                    case "RECS":
                        return "Recorded";
                    case "WIP":
                        return "Work In Progress";
                    case "XSIT":
                        return "In Transit";
                    default:
                        return "Unknown";
                }
            }
        }

        public DateTime? JobLineStart { get; set; }
        public DateTime? JobLineEnd { get; set; }
        public DateTime? AppointmentDate { get; set; }//If BETWEEN DATEADD(HOUR,-1,GETDATE()) AND DATEADD(HOUR,72,GETDATE()) THEN VisitStatus is 'Appt'  
        public DateTime? InTransDate { get; set; }
        [DisplayName("Estimate")]
        public DateTime? RepairEstimate { get; set; }
        public string Address
        {
            get
            {
                string[] address = new string[] { Address1, Address2, Address3, Address4, PostCode };
                return string.Join(", ", address.Where(str => !string.IsNullOrWhiteSpace(str)));
            }
        }

        public string AddressHTML
        {
            get
            {
                string[] address = new string[] { Address1, Address2, Address3, Address4 };
                return string.Join("<br/>", address.Where(str => !string.IsNullOrWhiteSpace(str)));
            }
        }
        [DisplayName("Operative (Code : Name)")]
        public string Operative
        {
            get
            {
                var operative = OperativeCode + " : " + OperativeName;
                return operative.Replace(" ","").Trim() == ":" ? "Unassigned" : operative;
            }
        }

        [DisplayName("Repair (Id - LineId - VisitId)")]
        public string Repair
        {
            get { return RepairId + " - " + RepairLineId + " - " + VisitId; }
        }
    }
}
