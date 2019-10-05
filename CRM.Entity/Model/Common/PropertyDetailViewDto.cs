using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CRM.Entity.Enum;

namespace CRM.Entity.Model.Common
{
    public class PropertyDetailViewDto
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public int AssetId { get; set; }
        [Display(Name = "Void Id")]
        public int? LatestVoidId { get; set; }
        [Display(Name = "Property Code")]
        public string PropertyCode { get; set; }
        [Display(Name = "Property Type")]
        public string PropertyType { get; set; }
        [Display(Name = "Number Of Bedrooms")]
        public int? Bedrooms { get; set; }
        [Display(Name = "Wheelchair Adapted")]
        public bool? WheelchairAdapted { get; set; }
        [Display(Name = "Ramped Access")]
        public bool? RampedAccess { get; set; }
        [Display(Name = "Walk-In Shower")]
        public bool? WalkInShower { get; set; }
        [Display(Name = "Step-In Shower")]
        public bool? StepInShower { get; set; }
        [Display(Name = "Stairlift")]
        public bool? Stairlift { get; set; }
        [Display(Name = "AgeRestriction")]
        public string AgeRestriction { get; set; }
        public string formattedAgeRestriction => AgeRestriction?.Length > 1 ? AgeRestriction.Substring(0, 2) + "+" : "18+";
        [Display(Name = "Pets")]
        public string Pets { get; set; }
        [Display(Name = "Steps To Access")]
        public int? NumberOfSteps { get; set; }
        [Display(Name = "Lift Access")]
        public bool? Lift { get; set; }
        [Display(Name = "Communal Entrance")]
        public bool? CommunalEntrance { get; set; }
        [Display(Name = "High Rise")]
        public bool? HighRise { get; set; }
        [Display(Name = "Neighbourhood Id")]
        public int? NeighbourhoodId { get; set; }
        public string Area { get; set; }
        [Display(Name = "Tenancy End")]
        public DateTime? TenancyEndDate { get; set; }
        [Display(Name = "Current Main Tenant")]
        public string CurrentMainTenant { get; set; }

        [Display(Name = "Rent")]
        public decimal? Rent { get; set; }
        [Display(Name = "Service Charges")]
        public decimal? ServiceCharges { get; set; }
        [Display(Name = "Total Charges")]
        public decimal? TotalCharges => Rent + ServiceCharges;
        [Display(Name = "Landlord")]
        public string Landlord { get; set; }
        [Display(Name = "Rent Frequency")]
        public string RentFrequency { get; set; }
        [Display(Name = "Address")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostCode { get; set; }

        [Display(Name = "Driveway")]
        public bool? Driveway { get; set; }
        [Display(Name = "Outbuildings")]
        public bool? Outbuildings { get; set; }
        [Display(Name = "Parking")]
        public bool? Parking { get; set; }
        [Display(Name = "Bin")]
        public bool? Bin { get; set; }
        [Display(Name = "Garden")]
        public bool? Garden { get; set; }
        
        [Display(Name = "Number Of Bathrooms")]
        public int? NumberOfBathrooms { get; set; }
        [Display(Name = "Number Of Reception Rooms")]
        public int? NumberOfReceptionRooms { get; set; }
        [Display(Name = "Bath / Shower / Communal")]
        public string BathroomType { get; set; }
        [Display(Name = "Separate WC")]
        public bool? SeparateWC { get; set; }
        [Display(Name = "WC Upstairs / Downstairs")]
        public string WCType { get; set; }
        [Display(Name = "Trust Care")]
        public bool? Trustcare { get; set; }
        [Display(Name = "Floor Level")]
        public int? FloorLevel { get; set; }
        [Display(Name = "Concierge")]
        public bool? Concierge { get; set; }
        [Display(Name = "Door Entry Control")]
        public bool? DoorEntry { get; set; }
        [Display(Name = "Washer Space")]
        public bool? WasherSpace { get; set; }
        [Display(Name = "Dryer Space")]
        public bool? DryerSpace { get; set; }
        [Display(Name = "Communal Laundry")]
        public bool? CommunalLaundry { get; set; }
        [Display(Name = "Furnished")]
        public bool? Furnished { get; set; }
        [Display(Name = "Electric Meter Type")]
        public string ElectricMeterType { get; set; }
        [Display(Name = "Electricity Supplier")]
        public string ElectricSupplier { get; set; }
        [Display(Name = "Electric Meter Location")]
        public string ElectricMeterLocation { get; set; }
        [Display(Name = "Gas Meter Type")]
        public string GasMeterType { get; set; }
        [Display(Name = "Gas Supplier")]
        public string GasSupplier { get; set; }
        [Display(Name = "Gas Meter Location")]
        public string GasMeterLocation { get; set; }
        [Display(Name = "Other Adaptations")]
        public string OtherAdaptations { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public List<byte[]> Images { get; set; }
        public string Comment { get; set; }
        public DateTime? GasLastServicedDate { get; set; }
        public string Address
        {
            get
            {
                const string linebreak = "\n";
                var sb = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(AddressLine1))
                {
                    sb.Append(AddressLine1).Append(linebreak);
                }
                if (!string.IsNullOrWhiteSpace(AddressLine2))
                {
                    sb.Append(AddressLine2).Append(linebreak);
                }
                if (!string.IsNullOrWhiteSpace(AddressLine3))
                {
                    sb.Append(AddressLine3).Append(linebreak);
                }
                if (!string.IsNullOrWhiteSpace(AddressLine4))
                {
                    sb.Append(AddressLine4).Append(linebreak);
                }
                if (!string.IsNullOrWhiteSpace(PostCode))
                {
                    sb.Append(PostCode).Append(linebreak);
                }
                return (sb.ToString());
            }
        }
    }
}
