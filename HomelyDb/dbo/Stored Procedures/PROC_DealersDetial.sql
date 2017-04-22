/*
Create Date	-	18-Jan-2014
Purpose		-	Select Delears detail.

Exec [PROC_DealersDetial] 0, 0, '33', 2
*/

CREATE PROCEDURE [dbo].[PROC_DealersDetial]
@StateId NUMERIC(18,0),
@CityId NUMERIC(18,0),
@Location VARCHAR(100),
@PropertyType NUMERIC(18,0)
AS
BEGIN

	DECLARE @Query VARCHAR(MAX)
	
	--Select Dealers detail
	SET @Query = 'SELECT
		Mobile,
		EmailId,
		'''' AS Image
		FROM tbl_Registration
		WHERE (OwnershipType = 2 OR OwnershipType = 4)'
		
	--IF(@StateId != 0)
	--BEGIN
	--	SET @Query = @Query + ' AND State = ' + CAST(@StateId AS VARCHAR)
	--END
	
	IF(@CityId != 0)
	BEGIN
		SET @Query = @Query + ' AND City = ' + CAST(@CityId AS VARCHAR)
	END
	
	IF(@Location != '')
	BEGIN
		SET @Query = @Query + ' AND CompanyAddress like ''%' + @Location + '%'''
	END
	
	IF(@PropertyType != '0')
	BEGIN
		SET @Query = @Query + ' AND PropertyType = ' + CAST(@PropertyType AS VARCHAR)
	END	
	
	PRINT @Query
	EXEC(@Query) 	
END
