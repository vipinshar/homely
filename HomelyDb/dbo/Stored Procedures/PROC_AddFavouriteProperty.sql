--PROC_AddFavoriteProperty 'devendrasinghpratap@gmail.com', 'HOME100001'
CREATE PROCEDURE [dbo].[PROC_AddFavouriteProperty]
@EmailId VARCHAR(50),
@PropertyCode VARCHAR(20)
AS
BEGIN

	DECLARE @RegistrationID UNIQUEIDENTIFIER = NULL
    
    --If Email is not blank that means Property is added by registered user.		
	IF(ISNULL(@EmailId , '') != '')
	BEGIN
		SELECT @RegistrationID = RegistrationID
			FROM tbl_Registration
			WHERE EmailId = @EmailId
	END

	IF EXISTS	
	(
		SELECT 1 FROM tbl_FavouriteProperty
			WHERE RegistrationID = @RegistrationID AND PropertyCode = @PropertyCode AND Deleted = 0
	)
	BEGIN
		SELECT 'You have already marked this Property as favourite.' As ResultMesssage
	END
	ELSE
	BEGIN
		INSERT INTO tbl_FavouriteProperty
		(
			RegistrationId,
			PropertyCode,
			CreateDate,
			Deleted
		)
		Values
		(
			@RegistrationID,
			@PropertyCode,
			GETDATE(),
			0
		)
	END
	
	SELECT 'The Property has been marked as favourite.' As ResultMesssage
END
