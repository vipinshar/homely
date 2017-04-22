

/*
EXECUTE [PROC_SearchProperty] 0,'',1,0,500000,'','3,4',0,0,0,0
*/

CREATE PROCEDURE [dbo].[PROC_SearchProperty]
@CityId NUMERIC(18,0),
@Locality VARCHAR(50),
@PropertyType NUMERIC(18,0),
@MinPrice NUMERIC(8,2),
@MaxPrice NUMERIC(8,2),
@PropertyCode VARCHAR(20),
@Amenities VARCHAR(MAX),
@Avaliability NUMERIC(18,0),
@Ownership NUMERIC(18,0),
@Furnishing NUMERIC(18,0),
@Bedroom NUMERIC(18,0) 	                 
AS
BEGIN

	DECLARE @Query VARCHAR(MAX)
	
	SET @Query = 'SELECT DISTINCT 
		Locality,
		SocietyName,
		Rent,
		CAST(Pro.Area as VARCHAR)+ '' '' + ISNULL(Pro.Measure,'''') AS Area,
		Bathroom,
		Bedroom,
		[Description],
		PropertyCity,
		SPP.SubPropertyType,
		Pro.CreateDate,
		PropertyCode
		FROM tbl_Property Pro INNER JOIN tbl_Master_SubPropertyType SPP ON PRO.SubPropertyType = SPP.SubPropertyTypeID'
	
	IF(@Amenities != '')
	BEGIN
		SET @Query = @Query + ' CROSS APPLY [Split](Pro.Amenities, '','') SplPro
					CROSS APPLY [Split](''' + @Amenities +''', '','') SplAme'
	END
	
	SET @Query = @Query + ' WHERE Pro.[Enabled] = 1 And Rented = 0'
	
	IF(@Amenities != '')
	BEGIN
		SET @Query = @Query + ' AND SplPro.Data = SplAme.Data'
	END
	
	IF(@CityId != 0)
	BEGIN
		SET @Query = @Query + ' AND PropertyCity = ' + CAST(@CityId AS VARCHAR)
	END
	
	IF(@Locality != '')
	BEGIN
		SET @Query = @Query + ' AND Locality = ''' + @Locality +''''
	END
	
	IF(@PropertyType != '0')
	BEGIN
		SET @Query = @Query + ' AND SPP.SubPropertyTypeId = ' + CAST(@PropertyType AS VARCHAR)
	END
	
	IF(@PropertyCode != '')
	BEGIN
		SET @Query = @Query + ' AND PropertyCode = ''' + @PropertyCode + ''''
	END
	
	SET @Query = @Query + ' AND Rent >= ' + CAST(@MinPrice AS VARCHAR) + ' AND Rent <= ' + CAST(@MaxPrice AS VARCHAR)
	
	IF(@Avaliability != '0')
	BEGIN
		SET @Query = @Query + ' AND Availability = ' + CAST(@Avaliability AS VARCHAR)
	END
	
	IF(@Ownership != '0')
	BEGIN
		SET @Query = @Query + ' AND OwnershipType = ' + CAST(@Ownership AS VARCHAR)
	END
	
	IF(@Furnishing != '0')
	BEGIN
		SET @Query = @Query + ' AND Furnished = ' + CAST(@Furnishing AS VARCHAR)
	END
	
	IF(@Bedroom != '0')
	BEGIN
		SET @Query = @Query + ' AND Bedroom = ' + CAST(@Bedroom AS VARCHAR)
	END
	
	SET @Query = @Query + ' Order By CreateDate Desc'
	
	--PRINT @Query
	EXEC(@Query) 
 END

