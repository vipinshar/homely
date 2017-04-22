CREATE PROCEDURE [dbo].[PROC_ContacUs]
@EmailID VARCHAR(50),
@Name varchar(50),
@Mobile varchar(20),
@Email varchar(50),
@QueryType varchar(50)= NULL,
@Query varchar(max)= NULL,
@QueryFrom VARCHAR(50)
  AS
BEGIN
	DECLARE @RegistrationID UNIQUEIDENTIFIER = NULL
	
	IF(ISNULL(@EmailID , '') != '')
	BEGIN
		SELECT @RegistrationID = RegistrationID
			FROM tbl_Registration
			WHERE EmailId = @EmailId
	END  

	INSERT INTO tbl_ContactUs
	(
		RegistrationID,
		Name,
		Mobile,
		Email,
		QueryType,
		Query,
		QueryFrom,
		CreateDate,
		[Enabled]
	)
	VALUES
	( 
		@RegistrationID,
		@Name,
		@Mobile,
		@Email,
		@QueryType,
		@Query,
		@QueryFrom,
		GETDATE(),
		1
	)

END
