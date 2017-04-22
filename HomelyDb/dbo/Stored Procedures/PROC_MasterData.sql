/*
Create Date	-	08-Jan-2014
Purpose		-	Select Master data.

Exec [PROC_MasterData]
*/

CREATE PROCEDURE [dbo].[PROC_MasterData]
AS
BEGIN

	--Transaction
	SELECT 
		TransactionId,
		[Transaction]
		FROM tbl_Master_Transaction
		WHERE [Enabled] = 1 
		ORDER BY [Order]			
		
	--Ownership
	SELECT 
		OwnershipId,
		[Ownership]
		FROM tbl_Master_Ownership
		WHERE [Enabled] = 1 
		ORDER BY [Order]
		
	--Furnished
	SELECT 
		FurnishedId,
		Furnished
		FROM tbl_Master_Furnished
		WHERE [Enabled] = 1 
		ORDER BY [Order]
		
	--Facing
	SELECT 
		FacingId,
		Facing
		FROM tbl_Master_Facing
		WHERE [Enabled] = 1 
		ORDER BY [Order]
		
	--Availability
	SELECT 
		AvailabilityId,
		Availability
		FROM tbl_Master_Availability
		WHERE [Enabled] = 1 
		ORDER BY [Order]
		
	--Amenities
	SELECT 
		AmenityId,
		Amenity,
		ImagePath,
		ImageHoverPath,
		BigImagePath,
		BigImageHoverPath
		FROM tbl_Master_Amenities
		WHERE [Enabled] = 1 
		ORDER BY [Order]
		
	--Property
	SELECT 
		SubPropertyTypeID,
		SubPropertyType
		FROM tbl_Master_SubPropertyType SPT INNER JOIN tbl_Master_PropertyType PT ON SPT.PropertyTypeID = PT.PropertyTypeID 
		WHERE SPT.[Enabled] = 1  AND PT.[Enabled] = 1
		ORDER BY [Order]

	--City		
	SELECT 
		SubCityId,
		SubCity
		FROM tbl_Master_SubCity
		WHERE [Enabled] = 1
		ORDER BY SubCity
		
	--State		
	SELECT 
		StateID,
		[State]
		FROM tbl_Master_State
		WHERE [Enabled] = 1
		ORDER BY [State]
		
		--Country
	  SELECT
	       CountryId,
	       CountryName,
	       CountryCode
	       FROM tbl_Master_Country
	       WHERE [Enabled]=1
END
