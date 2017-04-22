

/*
Create Date	-	31-Dec-2013
Purpose		-	Select data for home page.

Exec PROC_HomePage		
*/

CREATE PROCEDURE [dbo].[PROC_HomePage]
AS
BEGIN

	--Local valiables will be used to show Map image details
	DECLARE @FreshListing INT = 0,
			@LatestListing INT = 0,
			@DelhiPropertyCount INT = 0,
			@GZBPropertyCount INT = 0,
			@GGNPropertyCount INT = 0,
			@NoidaPropertyCount INT = 0,
			@MumbaiPropertyCount INT = 0
	
	--Count of the Property those are verified and not rented
	SELECT @FreshListing = COUNT(1)
		FROM tbl_Property
		WHERE [Enabled]  = 1 And Rented = 0 AND Verified = 1
	
	--Count of the Property those are verified and not rented and
	--1. Registered within 24 hours
	--2. If no Property is registered in 24 hours then select Property registered within 1 week
	--3. If no Property is registered in 1 week then select Property registered within 1 month
	SELECT @LatestListing = COUNT(1)
		FROM tbl_Property
		WHERE [Enabled]  = 1 And Rented = 0 AND Verified = 1 AND CreateDate > DATEADD(HOUR, -24, GETDATE())		
		
	IF(@LatestListing = 0)
	BEGIN
		SELECT @LatestListing = COUNT(1)
		FROM tbl_Property
		WHERE [Enabled]  = 1 And Rented = 0 AND Verified = 1 AND CreateDate > DATEADD(WEEK, -1, GETDATE())		
	END	

	IF(@LatestListing = 0)
	BEGIN
		SELECT @LatestListing = COUNT(1)
		FROM tbl_Property
		WHERE [Enabled]  = 1 And Rented = 0 AND Verified = 1 AND CreateDate > DATEADD(MONTH, -1, GETDATE())		
	END	
		
	--Delhi properties count
	SELECT @DelhiPropertyCount = COUNT(1) 
		FROM tbl_Property Pro INNER JOIN tbl_Master_SubCity Sub ON Pro.PropertyCity = Sub.SubCityId
		WHERE Sub.CityId = 1 AND Pro.[Enabled]  = 1 And Rented = 0 AND Sub.[Enabled] = 1  AND Pro.Verified = 1
		
	--Ghaziabad properties count
	SELECT @GZBPropertyCount = COUNT(1) 
		FROM tbl_Property Pro INNER JOIN tbl_Master_SubCity Sub ON Pro.PropertyCity = Sub.SubCityId
		WHERE Sub.CityId = 4 AND Pro.[Enabled]  = 1 And Rented = 0 AND Sub.[Enabled] = 1 AND Pro.Verified = 1
		
	--Gurgaon properties count
	SELECT @GGNPropertyCount = COUNT(1) 
		FROM tbl_Property Pro INNER JOIN tbl_Master_SubCity Sub ON Pro.PropertyCity = Sub.SubCityId
		WHERE Sub.CityId = 3 AND Pro.[Enabled]  = 1 And Rented = 0 AND Sub.[Enabled] = 1 AND Pro.Verified = 1
		
	--Noida properties count
	SELECT @NoidaPropertyCount = COUNT(1) 
		FROM tbl_Property Pro INNER JOIN tbl_Master_SubCity Sub ON Pro.PropertyCity = Sub.SubCityId
		WHERE Sub.CityId = 2 AND Pro.[Enabled]  = 1 And Rented = 0 AND Sub.[Enabled] = 1 AND Pro.Verified = 1
		
	--Faridabad properties count
	SELECT @MumbaiPropertyCount = COUNT(1) 
		FROM tbl_Property Pro INNER JOIN tbl_Master_SubCity Sub ON Pro.PropertyCity = Sub.SubCityId
		WHERE Sub.CityId = 5 AND Pro.[Enabled]  = 1 And Rented = 0 AND Sub.[Enabled] = 1 AND Pro.Verified = 1
			
	--Total listings
	SELECT 
		@FreshListing AS FreshListing,
		@LatestListing AS LatestListing,
		@DelhiPropertyCount AS DelhiPropertyCount,
		@GZBPropertyCount AS GZBPropertyCount,
		@GGNPropertyCount AS GGNPropertyCount,
		@NoidaPropertyCount AS NoidaPropertyCount,
		@MumbaiPropertyCount AS MumbaiPropertyCount

	--Delhi properties	
	SELECT 
		CASE WHEN LEN(Pro.Locality) > 15 THEN LEFT(Pro.Locality, 15) + '...'  ELSE Pro.Locality END AS Locality,
		CASE WHEN LEN(Pro.SocietyName) > 15 THEN LEFT(Pro.SocietyName, 15) + '...'  ELSE Pro.SocietyName END AS SocietyName,
		Pro.Rent,
		CAST(Pro.Area AS VARCHAR) + ' ' + ISNULL(Pro.Measure, '') AS Area,
		Pro.Bathroom,
		Pro.Bedroom,
		Pro.PropertyCode,
		Pro.TransactionType
		FROM tbl_Property Pro INNER JOIN tbl_Master_SubCity Sub ON Pro.PropertyCity = Sub.SubCityId
		WHERE Sub.CityId = 4/*GZB*/ /*1 /*Delhi*/*/ AND Pro.[Enabled]  = 1 And Rented = 0 AND Sub.[Enabled]  = 1 AND Pro.Verified = 1
		Order By Pro.CreateDate Desc	
			
	--Propery reviews
	SELECT TOP(3)
		Review,
		Name,
		'images/testi-img01.jpg' AS [Image]
		FROM tbl_PropertyReview
		WHERE [Enabled]  = 1 AND HomePage = 1  	
		Order By CreateDate Desc
	
	--Hot properties		
	SELECT 
		Locality,
		SocietyName,
		Rent,
		CAST(Area AS VARCHAR) + ' ' + ISNULL(Measure, '') AS Area,
		Bathroom,
		Bedroom,
		PropertyCode
		FROM tbl_Property
		WHERE HotProperty =1 AND [Enabled] = 1 And Rented = 0 AND Verified = 1
		Order By CreateDate Desc
END
