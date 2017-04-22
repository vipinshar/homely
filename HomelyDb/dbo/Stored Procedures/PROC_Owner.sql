CREATE PROCEDURE [dbo].[PROC_Owner]
@EmailId VARCHAR(50),
@Password varchar(20),
@City INT,
@OwnershipType int,
@FirstName VARCHAR(50),
@Mobile varchar(15),
@Alternate varchar(15),
@For VARCHAR(10), --Insert/update
@RegistrationID VARCHAR(50)
AS
BEGIN
	IF(@For = 'Insert')
	BEGIN
		IF EXISTS(SELECT 1
					FROM tbl_Registration
					WHERE EmailId = @EmailId OR Mobile = @Mobile)
		BEGIN
			SELECT 
				'0' AS ResultMessage
		END
		ELSE
		BEGIN
			--USER OWNER REGISTRETION
			INSERT INTO tbl_Registration
			(
				EmailId,Password,City,OwnershipType,FirstName,Mobile,Alternate,[Enabled]
			)
			VALUES
			(
				@EmailId,@Password,@City,@OwnershipType,@FirstName,@Mobile,@Alternate,1
			)
			
			SELECT 
				'1' AS ResultMessage
		END
	END
	ELSE IF(@For = 'Update')
	BEGIN
		IF EXISTS(SELECT 1
					FROM tbl_Registration
					WHERE (EmailId = @EmailId OR Mobile = @Mobile) AND RegistrationId != @RegistrationId)
		BEGIN
			SELECT 
				'0' AS ResultMessage
		END
		ELSE
		BEGIN
			UPDATE tbl_Registration
				SET 
					EmailId = @EmailId,
					City = @City,
					FirstName = @FirstName,
					Mobile = @Mobile,
					Alternate = @Alternate
				WHERE RegistrationId = @RegistrationId
			
			SELECT 
				'1' AS ResultMessage
		END
	END
END
