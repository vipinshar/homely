
/*
Create Date	-	01-Jan-2014
Purpose		-	Select properties for the selected city.

Exec [PROC_Property_City] 1, 'CITY'	
Exec [PROC_Property_City] 0, 'Rent'	
Exec [PROC_Property_City] 0, 'Resale'	
*/

CREATE PROCEDURE [dbo].[PROC_Property_City]
@City INT, --City, Rent, Resale
@For VARCHAR(20)
AS
BEGIN

	DECLARE @Query VARCHAR(MAX)
	
	SELECT @Query = 
		'SELECT 
			Pro.Locality,
			Pro.SocietyName,
			Pro.Rent,
			CAST(Pro.Area AS VARCHAR) + '' '' + ISNULL(Pro.Measure, '''') AS Area,
			Pro.Bathroom,
			Pro.Bedroom,
			Pro.PropertyCode,
			Pro.TransactionType,
			Pro.CreateDate
			FROM tbl_Property  Pro LEFT OUTER JOIN tbl_Master_SubCity Sub ON Pro.PropertyCity = Sub.SubCityId
				LEFT OUTER JOIN tbl_Master_City Cit ON Cit.CityID = Sub.CityId
			WHERE Pro.[Enabled]  = 1 And Rented = 0 AND Pro.Verified = 1'
		
	IF(@For = 'City')
	BEGIN
		SELECT @Query = @Query + ' AND Sub.[Enabled]  = 1 AND Cit.[Enabled]  = 1 AND Cit.CityId = ' + CAST(@City AS VARCHAR)
	END
	ELSE IF(@For = 'Rent')
	BEGIN
		SELECT @Query = @Query + ' AND Pro.TransactionType = 4'
	END
	ELSE IF(@For = 'Resale')
	BEGIN
		SELECT @Query = @Query + ' AND Pro.TransactionType = 1'
	END
		
	SELECT @Query = @Query + ' Order By Pro.CreateDate Desc'
	
	--PRINT(@Query)
	EXEC(@Query)
END
