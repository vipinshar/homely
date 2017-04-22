/*
Create Date	-	18-Jan-2014
Purpose		-	Select Properties detail for Comprasion.

Exec [PROC_PropertyComparison] 'HOME100001', 'HOME100008', 'HOME100010'		
*/

CREATE PROCEDURE [dbo].[PROC_PropertyComparison]
@PropertyCode1 VARCHAR(20),
@PropertyCode2 VARCHAR(20),
@PropertyCode3 VARCHAR(20)
AS
BEGIN

	--Select Properties details
	SELECT
		Pro.PropertyCode, 
		Tra.[Transaction],
		Own.[Ownership],
		Spp.SubPropertyType,
		Sct.SubCity,
		Locality,
		CAST(Area AS VARCHAR(20)) + ' SQ FT' AS Area,
		SocietyName,
		Rent,
		Bedroom,
		Bathroom,
		Balcony,
		CASE [FLOOR] WHEN 0 THEN 'GROUND FLOOR' ELSE CAST(Floor as VARCHAR) END AS Floor,
		[Description],
		Pro.CreateDate,
		FloorInBuilding,
		--CAST(AgeOfConstruction AS VARCHAR) + ' Year' AS Age,
		Fur.Furnished,
		Fac.Facing
		FROM tbl_Property Pro INNER JOIN tbl_Master_Transaction Tra ON  Pro.TransactionType = Tra.TransactionId
			INNER JOIN tbl_Master_Ownership Own ON Pro.OwnershipType = Own.OwnershipId
			INNER JOIN tbl_Master_SubPropertyType Spp ON Pro.SubPropertyType = Spp.SubPropertyTypeID 
			INNER JOIN tbl_Master_SubCity Sct ON Pro.PropertyCity = Sct.SubCityId
			INNER JOIN tbl_Master_Furnished Fur ON Pro.Furnished = Fur.FurnishedId
			INNER JOIN tbl_Master_Facing Fac ON Pro.Facing = Fac.FacingId
		WHERE (PropertyCode = @PropertyCode1 OR PropertyCode = @PropertyCode2 OR PropertyCode = @PropertyCode3)	
		
		
	--Select Amenities for Property1
	IF((SELECT Amenities 
		FROM tbl_Property
		WHERE PropertyCode = @PropertyCode1) != '')
	BEGIN
		SELECT 
			Ame.Amenity,
			Ame.ImageHoverPath
			FROM tbl_Property Pro CROSS APPLY [Split](Pro.Amenities, ',') Spl
				INNER JOIN tbl_Master_Amenities Ame ON Ame.AmenityId = Spl.Data
			WHERE PropertyCode = @PropertyCode1
			ORDER BY Ame.[Order]
	END
	ELSE
	BEGIN
		SELECT 
			Amenity,
			ImageHoverPath
			FROM tbl_Master_Amenities
			WHERE 1!=1
	END
	
	--Select Amenities for Property2
	IF((SELECT Amenities 
		FROM tbl_Property
		WHERE PropertyCode = @PropertyCode2) != '')
	BEGIN
		SELECT 
			Ame.Amenity,
			Ame.ImageHoverPath
			FROM tbl_Property Pro CROSS APPLY [Split](Pro.Amenities, ',') Spl
				INNER JOIN tbl_Master_Amenities Ame ON Ame.AmenityId = Spl.Data
			WHERE PropertyCode = @PropertyCode2
			ORDER BY Ame.[Order]
	END
	ELSE
	BEGIN
		SELECT 
			Amenity,
			ImageHoverPath
			FROM tbl_Master_Amenities
			WHERE 1!=1
	END
	
	--Select Amenities for Property3
	IF((SELECT Amenities 
		FROM tbl_Property
		WHERE PropertyCode = @PropertyCode3) != '')
	BEGIN
		SELECT 
			Ame.Amenity,
			Ame.ImageHoverPath
			FROM tbl_Property Pro CROSS APPLY [Split](Pro.Amenities, ',') Spl
				INNER JOIN tbl_Master_Amenities Ame ON Ame.AmenityId = Spl.Data
			WHERE PropertyCode = @PropertyCode3
			ORDER BY Ame.[Order]
	END
	ELSE
	BEGIN
		SELECT 
			Amenity,
			ImageHoverPath
			FROM tbl_Master_Amenities
			WHERE 1!=1
	END
END
