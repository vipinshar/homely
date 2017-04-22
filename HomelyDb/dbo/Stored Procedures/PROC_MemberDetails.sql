--[PROC_MemberDetails] 'devendrasingpratap@yahoo1.com'
CREATE PROCEDURE [dbo].[PROC_MemberDetails]
@EmailId VARCHAR(50)
AS
BEGIN
		SELECT
			RegistrationID,
			OwnershipType,			 
			FirstName,
			EmailId,
			City,
			Mobile,
			Alternate,
			[Image],
			CompanyName,
			CompanyAddress,
			DealsIn,
			PropertyType,
			SpecialiseIn,
			PermanentAddress,
			PropertiesAt
		FROM tbl_Registration
		WHERE EmailId = @EmailId
END


