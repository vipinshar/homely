
/*
Create Date	-	06-Jan-2014
Purpose		-	Select the detils of selected Property.

Exec [PROC_PropertyDetails] 'HOME100079'		
*/

CREATE PROCEDURE [dbo].[PROC_PropertyDetails]
@PropertyCode VARCHAR(20)
AS
BEGIN

SELECT 
		Locality,
		SocietyName,
		Rent,
		--Area,
		CAST(Pro.Area as varchar)+ ' ' + ISNULL(Pro.Measure,'') AS Area,
		Bathroom,
		Bedroom,
		Balcony,
	    FloorInBuilding,
	    Fur.Furnished,
	    Avail.Availability,
	    Fac.Facing,
		[Description],
		Mcity.City,
		SPP.SubPropertyType,
		Pro.CreateDate,
		'+91 ' + Mobile AS Mobile,
		EmailID,
		Own.Ownership,
	    Pro.OwnerName,
	    --Pro.Floor,
	    CASE Pro.Floor WHEN 0 THEN 'GROUND FLOOR' ELSE CAST(Pro.Floor AS VARCHAR) END AS Floor,
		SCty.SubCity AS City
		FROM tbl_Property Pro INNER JOIN tbl_Master_SubPropertyType SPP ON PRO.SubPropertyType = SPP.SubPropertyTypeID 
			LEFT OUTER JOIN tbl_Master_SubCity SCty ON Pro.PropertyCity = SCty.SubCityId
			INNER JOIN tbl_Master_Furnished Fur ON Pro.Furnished = Fur.FurnishedId
			INNER JOIN tbl_Master_Facing Fac ON Pro.Facing = Fac.FacingId
			INNER JOIN tbl_Master_Availability Avail ON Pro.Availability = Avail.AvailabilityId
			INNER JOIN tbl_Master_Ownership Own ON Pro.OwnershipType= Own.OwnershipId
			INNER JOIN tbl_Master_City MCity on Pro.PropertyCity= MCity.CityID
		WHERE PropertyCode = @PropertyCode	
		IF((SELECT Amenities 
			FROM tbl_Property
			WHERE PropertyCode = @PropertyCode) != '')
		BEGIN
			SELECT 
				Ame.AmenityId,
				Ame.Amenity,
				Ame.BigImagePath,
				Ame.BigImageHoverPath
				FROM tbl_Property Pro CROSS APPLY [Split](Pro.Amenities, ',') Spl
					INNER JOIN tbl_Master_Amenities Ame ON Ame.AmenityId = Spl.Data
				WHERE PropertyCode = @PropertyCode
				ORDER BY Ame.[Order]
		END
		ELSE
		BEGIN
			SELECT 
				AmenityId,
				Amenity,
				BigImagePath,
				BigImageHoverPath
				FROM tbl_Master_Amenities
				WHERE 1!=1
		END
END
