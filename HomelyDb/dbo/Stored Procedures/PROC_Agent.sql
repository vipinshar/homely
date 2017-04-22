
CREATE PROCEDURE [dbo].[PROC_Agent]
@EmailId VARCHAR(50),
@Password varchar(20),
@City INT,
@OwnershipType int,
@FirstName varchar(50),
@Mobile varchar(20),
@Alternate varchar(20)=NULL,
@CompanyName varchar(50),
@CompanyAddress nvarchar(500),
@PropertyType varchar(max),
@DealsIn varchar(50),
@SpecialiseIn varchar(max),
@Image VARCHAR(100),
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
				EmailId,[Password],City,OwnershipType,FirstName,Mobile,Alternate,CompanyName,CompanyAddress,PropertyType,DealsIn,SpecialiseIn,[Enabled],[Image]
			)
			VALUES
			(
				@EmailId,@Password,@City,@OwnershipType,@FirstName,@Mobile,@Alternate,@CompanyName,@CompanyAddress,@PropertyType,@DealsIn,@SpecialiseIn,1,@Image
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
					Alternate = @Alternate,
					CompanyName = @CompanyName,
					CompanyAddress = @CompanyAddress,
					PropertyType = @PropertyType,
					DealsIn = @DealsIn,
					SpecialiseIn = @SpecialiseIn
				WHERE RegistrationId = @RegistrationId
				
			IF(@Image != '')
			BEGIN
				UPDATE tbl_Registration
				SET 
					[Image] = @Image
				WHERE RegistrationId = @RegistrationId
			END
			
			SELECT 
				'1' AS ResultMessage
		END
	END
END

