using System.ComponentModel;
using CRM.Entity.Model.Customer;

namespace CRM.Entity.Model.Common
{
    public class PropertyModel
    {
        public int Id { get; set; }
        [DisplayName("Property Code")]
        public string PROPERTY_CODE { get; set; }
        [DisplayName("Property Type")]
        public string PROPERTY_TYPE { get; set; }
        [DisplayName("Property Condition")]
        public string PROPERTY_CONDITION { get; set; }
        [DisplayName("Address Line1")]
        public string ADDRESS_LINE_1 { get; set; }
        [DisplayName("Address Line2")]
        public string ADDRESS_LINE_2 { get; set; }
        [DisplayName("Address Line3")]
        public string ADDRESS_LINE_3 { get; set; }
        [DisplayName("Address Line4")]
        public string ADDRESS_LINE_4 { get; set; }
        [DisplayName("Post Code")]
        public string POST_CODE { get; set; }
        [DisplayName("Street Name")]
        public string STREET_NAME { get; set; }
        [DisplayName("Neighbourhood Code")]
        public string NEIGHBOURHOOD_CODE { get; set; }
        [DisplayName("Neighbourhood Description")]
        public string NEIGHBOURHOOD_DESCRIPTION { get; set; }
        [DisplayName("Sub Neighbourhood Code")]
        public string SUB_NEIGHBOURHOOD_CODE { get; set; }
        [DisplayName("Sub Neighbourhood Description")]
        public string SUB_NEIGHBOURHOOD_DESCRIPTION { get; set; }
        [DisplayName("Neighbourhood Comb")]
        public string NEIGHBOURHOOD_COMB { get; set; }
        [DisplayName("Ward Code")]
        public string WARD_CODE { get; set; }
        [DisplayName("Ward Name")]
        public string WARD_NAME { get; set; }
        [DisplayName("Housing Team")]
        public string HOUSING_TEAM { get; set; }
        [DisplayName("Income Team")]
        public string INCOME_TEAM { get; set; }
        [DisplayName("Management Team")]
        public string MANAGEMENT_TEAM { get; set; }
        [DisplayName("Management Area")]
        public string MGMT_AREA { get; set; }
        [DisplayName("Current Income Patch Code")]
        public string CURRENT_INCOME_PATCH_CODE { get; set; }
        [DisplayName("Current Income Patch Description")]
        public string CURRENT_INCOME_PATCH_DESCRIPTION { get; set; }
        [DisplayName("Current Income Patch Email")]
        public string CURRENT_INCOME_PATCH_EMAIL { get; set; }
        [DisplayName("Income Patch Code")]
        public string INCOME_PATCH_CODE { get; set; }
        [DisplayName("Income Patch Description")]
        public string INCOME_PATCH_DESCRIPTION { get; set; }
        [DisplayName("Income Patch Email")]
        public string INCOME_PATCH_EMAIL { get; set; }
        [DisplayName("Current FTA Arrears Patch Code")]
        public string CURRENT_FTA_ARREARS_PATCH_CODE { get; set; }
        [DisplayName("Current FTA Arrears Patch Description")]
        public string CURRENT_FTA_ARREARS_PATCH_DESCRIPTION { get; set; }
        [DisplayName("Current FTA Arrears Patch Email")]
        public string CURRENT_FTA_ARREARS_PATCH_EMAIL { get; set; }
        [DisplayName("FTA Arrears Patch Code")]
        public string FTA_ARREARS_PATCH_CODE { get; set; }
        [DisplayName("FTA Arrears Patch De")]
        public string FTA_ARREARS_PATCH_DESCRIPTION { get; set; }
        [DisplayName("FTA Arrears Patch Email")]
        public string FTA_ARREARS_PATCH_EMAIL { get; set; }
        [DisplayName("Repairs Patch Code")]
        public string REPAIRS_PATCH_CODE { get; set; }
        [DisplayName("Repairs Patch Description")]
        public string REPAIRS_PATCH_DESCRIPTION { get; set; }
        [DisplayName("Repairs Patch Email")]
        public string REPAIRS_PATCH_EMAIL { get; set; }
        [DisplayName("Estate Patch Code")]
        public string ESTATE_PATCH_CODE { get; set; }
        [DisplayName("Estate Patch Description")]
        public string ESTATE_PATCH_DESCRIPTION { get; set; }
        [DisplayName("Estate Patch Email")]
        public string ESTATE_PATCH_EMAIL { get; set; }
        [DisplayName("Maint Patch Code")]
        public string MAINT_PATCH_CODE { get; set; }
        [DisplayName("Maint Patch Description")]
        public string MAINT_PATCH_DESCRIPTION { get; set; }
        [DisplayName("Maint Patch Email")]
        public string MAINT_PATCH_EMAIL { get; set; }
        [DisplayName("Officer Code Based On Status")]
        public string OFFICER_CODE_BASED_ON_STATUS { get; set; }
        [DisplayName("Officer Description Based On Status")]
        public string OFFICER_DESC_BASED_ON_STATUS { get; set; }
        [DisplayName("Build Date")]
        public string BUILD_DATE { get; set; }
        [DisplayName("Property Status")]
        public string PROPERTY_STATUS { get; set; }
        [DisplayName("New Teanancy Due To Start")]
        public string NEW_TENANCY_DUE_TO_START { get; set; }
        [DisplayName("Property Status Start Date")]
        public string PROPERTY_STATUS_START_DATE { get; set; }
        [DisplayName("Properrty Sub Status")]
        public string PROPERTY_SUB_STATUS { get; set; }
        [DisplayName("Properrty Sub Status Group")]
        public string PROPERTY_SUB_STATUS_GROUP { get; set; }
        [DisplayName("Target Rent Rate Current")]
        public decimal? TARGET_RENT_RATE_CURRENT { get; set; }
        [DisplayName("Target Rent Rate")]
        public decimal? TARGET_RENT_RATE { get; set; }
        [DisplayName("Target Rent Rate Current")]
        public decimal? TARGET_RENT_RATE_PREVIOUS { get; set; }
        [DisplayName("Target Rent Rate Start Date")]
        public string TARGET_RENT_RATE_START_DATE { get; set; }
        [DisplayName("Target Rent Rate End Date")]
        public string TARGET_RENT_RATE_END_DATE { get; set; }
        [DisplayName("Floor Level")]
        public int? FLOOR_LEVEL { get; set; }
        [DisplayName("Number Of Bedrooms")]
        public int? NUMBER_OF_BEDROOMS { get; set; }
        [DisplayName("Number Of Bathrooms")]
        public int? NUMBER_OF_BATHROOMS { get; set; }
        [DisplayName("Number Of Bed Spaces")]
        public int? NUMBER_OF_BED_SPACES { get; set; }
        [DisplayName("Heating")]
        public string HEATING { get; set; }
        [DisplayName("Garden")]
        public string GARDEN_FLAG { get; set; }
        [DisplayName("Parkinng")]
        public string PARKING_FLAG { get; set; }
        [DisplayName("Lift Access")]
        public string LIFT_ACCESS_FLAG { get; set; }
        [DisplayName("Disabled Access")]
        public string DISABLED_ACCESS_FLAG { get; set; }
        [DisplayName("Asbestos Check")]
        public string ASBESTOS_CHECK_FLAG { get; set; }
        [DisplayName("Gas Check")]
        public string GAS_CHECK_FLAG { get; set; }
        [DisplayName("Not Worth Repairing")]
        public string NOT_WORTH_REPAIRING_FLAG { get; set; }
        [DisplayName("About Turn")]
        public string ABOUT_TURN { get; set; }
        [DisplayName("Homes Plus")]

        public string HOMES_PLUS { get; set; }
        [DisplayName("Age Restriction")]
        public string AGE_RESTRICTION { get; set; }
        [DisplayName("Age Restriction Level")]
        public string AGE_RESTRICTION_LEVEL { get; set; }
        [DisplayName("Age Restriction Amount")]
        public decimal? AGE_RESTRICTION_AMOUNT { get; set; }
        [DisplayName("Pets Allowed")]
        public string PETS_ALLOWED { get; set; }
        [DisplayName("Ramped Access")]
        public string RAMPED_ACCESS { get; set; }
        [DisplayName("Stair Lift")]
        public string STAIRLIFT { get; set; }
        [DisplayName("Step In Shower Tray")]
        public string STEP_IN_SHOWER_TRAY { get; set; }
        [DisplayName("Leve Acess Property")]
        public string LEVEL_ACCESS_PROP { get; set; }
        [DisplayName("Overbath Shower")]
        public string OVERBATH_SHOWER { get; set; }
        [DisplayName("Affordable Rent")]
        public decimal? AFFORDABLE_RENT { get; set; }
        [DisplayName("Management Area")]
        public string MANAGEMENT_AREA { get; set; }
        [DisplayName("Management Area Description")]
        public string MANAGEMENT_AREA_DESCRIPTION { get; set; }
        [DisplayName("Block")]
        public string BLOCK_FLAG { get; set; }
        [DisplayName("Block Reference")]
        public string BLOCK_REFERENCE { get; set; }
        [DisplayName("Scheme")]
        public string SCHEME { get; set; }
        [DisplayName("Rent Cost Center")]
        public string RENT_COST_CENTRE { get; set; }
        [DisplayName("Repair Cost Center")]
        public string REPAIRS_COST_CENTRE { get; set; }
        [DisplayName("Accounts Company")]
        public string ACCOUNTS_COMPANY { get; set; }
        [DisplayName("Northing")]
        public string NORTHING { get; set; }
        [DisplayName("Easting")]
        public string EASTING { get; set; }
        [DisplayName("Company")]
        public string COMPANY { get; set; }
        public virtual double? Latitude { get; set; }

        public virtual double? Longitude { get; set; }
        public int AssetId { get; set; }
        public int PersonId { get; set; }
        public PersonDto Person { get; set; }
    }
}
