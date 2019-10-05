namespace CRM.DAL.Migrations.SQLScript
{
   public partial class SqlProgrammability    {

       public static string Drop_PropertyDetailView
       {
           get { return @"IF  EXISTS (SELECT * FROM sys.views
                                  WHERE object_id = OBJECT_ID(N'dbo.PropertyDetailView'))
                                  DROP VIEW dbo.PropertyDetailView"; } 
       }


        public static string Create_PropertyDetailView
        {
            get { return @"EXEC ('Create View [dbo].[PropertyDetailView] AS 
                         SELECT cp.Id                                                AS Id,
                                (SELECT MAX(VoidId) FROM CloudVoids.dbo.VOID v 
									Where v.PropertyCode =	vp.PropertyCode)		AS LatestVoidId,
                                (SELECT TOP 1 Id FROM CRM.dbo.Persons p WHERE TenantCode IN (SELECT c.TENANT_CODE FROM
								 MasterReferenceData.dbo.TBL_CUSTOMER c
								 INNER JOIN MasterReferenceData.dbo.TBL_TENANCY t
									ON t.[PLACE-REF] = c.PROPERTYREF
									AND t.TENANCY_STATUS =''ACTIVE''
								 WHERE c.PROPERTYREF = a.PropertyCode
								 AND TenantCode IS NOT NULL
								 AND ISNULL(IsDuplicate,0) = 0
								 ))	
																					AS PersonId,
                                a.AssetId										    AS AssetId,
								a.PropertyCode										AS PropertyCode,
								IBS.PROPERTY_TYPE									AS PropertyType,
								(CASE IBS.NUMBER_OF_BEDROOMS	
									WHEN 0 THEN vp.PropertyNumBedrooms 
									ELSE IBS.NUMBER_OF_BEDROOMS 
									END)											AS Bedrooms,
								vp.IsWheelChairAdapted								AS WheelchairAdapted,
                                vp.HasRampedAccess									AS RampedAccess,
                                vp.HasWalkInShower									AS WalkInShower,
                                vp.HasStepInShower									AS StepInShower,
                                vp.IsLevelAccessProperty							AS LevelAccessProperty,
                                vp.HasStairlift										AS Stairlift,
                                cast(  vp.AgeCriteria	as varchar(10))				AS AgeRestriction,
                                ibs.PETS_ALLOWED									AS Pets,
                                CAST(ISNULL(vp.NumStepsToAccess , 0)	as int)		AS NumberOfSteps,
                                vp.HasLift											AS Lift,
                                vp.HasCommunalEntrance								AS CommunalEntrance,
                                vp.IsHighRise										AS HighRise,
                               	(SELECT TOP 1 ps.NeighbourhoodId  
								FROM MasterReferenceData.dbo.PropertyPostcodes pp
								INNER JOIN MasterReferenceData.dbo.PropertyOutputAreas ps
								ON pp.OutputAreaId = ps.OutputAreaId 
								WHERE pp.Postcode = IBS.POST_CODE)					AS NeighbourhoodId,
								(SELECT TOP 1 pn.Name  
								FROM MasterReferenceData.dbo.PropertyPostcodes ps 
								INNER JOIN MasterReferenceData.dbo.PropertyOutputAreas po
								ON ps.OutputAreaId = po.OutputAreaId
								INNER JOIN MasterReferenceData.dbo.PropertyNeighbourhoods pn
								ON po.NeighbourhoodId = pn.NeighbourhoodId  
								WHERE ps.Postcode = IBS.POST_CODE)					AS Area,
                               (SELECT TOP 1 CAST(psd.rent as decimal) 
									FROM Cloudvoids.dbo.PropertyShop psd) 			AS Rent ,
								(SELECT  TOP 1 CAST(psd.ServiceCharges as decimal) 
									FROM Cloudvoids.dbo.PropertyShop psd
									ORDER BY psd.PaymentCycleId DESC)				AS ServiceCharges,
                                vp.LandLordcode										AS Landlord ,
                                (SELECT TOP 1 pc.Name 
								FROM Cloudvoids.dbo.PropertyShopStaticDetails pssd 
								LEFT JOIN Cloudvoids.dbo.PaymentCycle pc 
									ON pssd.PaymentCycleID = pc.PaymentCycleID 
									WHERE pssd.PropertyCode =  vp.PropertyCode
									ORDER BY pssd.PaymentCycleID DESC
									)
          																			AS RentFrequency ,
                                IBS.ADDRESS_LINE_1									AS AddressLine1 ,
                                IBS.ADDRESS_LINE_2									AS AddressLine2 ,
                                IBS.ADDRESS_LINE_3									AS AddressLine3 ,
                                IBS.ADDRESS_LINE_4									AS AddressLine4 ,
                                IBS.POST_CODE										AS PostCode ,
								vp.HasDriveway										AS Driveway,
								vp.HasOutbuildings									AS Outbuildings,
								vp.HasParking										AS Parking,
								vp.HasBin											AS Bin,
								vp.HasGarden											AS Garden,
								vp.PropertyNumBathrooms								AS NumberOfBathrooms,
								vp.PropertyNumReceptionRooms							AS NumberOfReceptionRooms,
								vp.BathroomType										AS BathroomType,
								vp.HasWasherSpace									AS SeparateWC,
								vp.WCType											AS WCType,
								CAST(0 as bit)										AS Trustcare,
								vp.FlatFloorLevel									AS FloorLevel,
								CAST(0 as bit)										AS Concierge,
								vp.HasDoorEntry										AS DoorEntry,
								vp.HasWasherSpace									AS WasherSpace,
								vp.HasDryerSpace									AS DryerSpace,
								vp.HasCommunalLaundry								AS CommunalLaundry,
								vp.HasFurnishings									AS Furnished,
								vp.ElectricMeterType								AS ElectricMeterType,
								vp.ElectricSupplier									AS ElectricSupplier,
								vp.ElectricMeterLocation			                AS ElectricMeterLocation,
								vp.GasMeterType										AS GasMeterType,
								vp.GasSupplier										AS GasSupplier,
								vp.GasMeterLocation									AS GasMeterLocation,
								''''												AS OtherAdaptations,
								cast(pll.Longitude as DECIMAL(19,13))				AS Longitude,
								cast(pll.Latitude as DECIMAL(19,13))				AS Latitude,
                                0													AS MutualExchangeProperty ,
								(SELECT Max( t.TENANCY_END_DATE)  
								FROM MasterReferenceData.dbo.TBL_CUSTOMER c
								INNER JOIN MasterReferenceData.dbo.TBL_TENANCY t
								ON t.[PLACE-REF] = c.PROPERTYREF
								AND c.PROPERTYREF =  vp.PropertyCode)				AS TenancyEndDate,
								(SELECT TOP 1 c.FIRSTNAME +'' ''+ c.LASTNAME
								FROM MasterReferenceData.dbo.TBL_CUSTOMER c
								INNER JOIN MasterReferenceData.dbo.TBL_TENANCY t
								ON t.[PLACE-REF] = c.PROPERTYREF
                                AND c.PROPERTYREF =  IBS.PROPERTY_CODE
								AND t.TENANCY_STATUS =''ACTIVE'')					AS CurrentMainTenant
                                ,(SELECT  CASE WHEN PAI.HeatingLastServiced IS NOT NULL AND PAI.HeatingLastServiced > ISNULL(pai.HeatingServiceDate,''01-01-1900'') THEN  PAI.HeatingLastServiced 
                                    WHEN PAI.HeatingServiceDate IS NOT NULL AND PAI.HeatingServiceDate > ISNULL(pai.HeatingLastServiced,''01-01-1900'') THEN  PAI.HeatingServiceDate
                                    ELSE ''01-01-1900''
                                    END AS GasLastServicedDate
                                    FROM [property].[dbo].[Tbl_PropertyAdditionalInfo] PAI 
                                     LEFT JOIN [repair].[dbo].Tbl_Heating H 
                                            ON H.PropertyRef = IBS.Property_Code 
                                     LEFT JOIN [repair].[dbo].Tbl_RepairDetails RD 
                                            ON H.FKRepairId = RD.FKRepairID 
                                            AND H.FKRepairlineID = RD.PKRepairLineId
                                     LEFT JOIN [repair].[dbo].[Tbl_HeatingStatus] HS 
                                            ON HS.PKHeatingStatusID = PAI.FKHeatingStatus
                                     LEFT JOIN [repair].[dbo].[Tbl_HeatingStatus] HS2 
                                            on hs2.PKHeatingStatusID = H.FKHeatingStatusID
                                          LEFT JOIN [property].[dbo].[Tbl_PropertyEnergyType] pet 
                                            on pet.PKEnergyTypeID = pai.PKEnergyTypeID
                                    WHERE   PAI.PKPropertyCode =IBS.PROPERTY_CODE
									
									AND (IBS.PROPERTY_STATUS in (''occupied'',''void'')                                                                                          --Pulls main compliance properties
                                    AND pet.EnergyTypeDescription in (''gas'',''gas cut'',''unknown'')
                                    AND IBS.Scheme NOT IN (''shared'', ''COM'', ''GARA'', ''GARB'', ''GARC'', ''GARLNK'',''LEASE'')
                                    OR IBS.HEATING IN (''GAS'',''GASW'') 
                                    AND pet.EnergyTypeValue is null 
                                    AND IBS.PROPERTY_STATUS in (''occupied'',''void'')
                                    AND IBS.Scheme NOT IN (''shared'', ''COM'', ''GARA'', ''GARB'', ''GARC'', ''GARLNK'',''LEASE'')
                                    )
                                   )                                            AS GasLastServicedDate
    
                       FROM    CRM.dbo.Properties AS cp
								INNER JOIN MasterReferenceData.dbo.TBL_PROPERTY AS IBS 
									ON cp.PropertyCode = IBS.PROPERTY_CODE
								INNER JOIN Assets.dbo.Asset AS A
                                    ON A.PropertyCode = cp.PropertyCode
								LEFT JOIN Cloudvoids.dbo.Property AS vp
                                    ON A.PropertyCode = vp.PropertyCode
								LEFT JOIN MasterReferenceData.dbo.TBL_POSTCODE_LONGLAT pll 
									ON pll.PostCode = IBS.POST_CODE
								AND (SELECT TOP 1 ps.NeighbourhoodId  
								FROM MasterReferenceData.dbo.PropertyPostcodes pp
								INNER JOIN MasterReferenceData.dbo.PropertyOutputAreas ps
								ON pp.OutputAreaId = ps.OutputAreaId 
								WHERE pp.Postcode = IBS.POST_CODE)  IS NOT null   ')"; }
        }

   }

}
