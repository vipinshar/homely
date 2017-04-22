CREATE PROCEDURE [dbo].[PROC_PostRequirment]
 @EmailId varchar(100),
 @PropertyType NUMERIC(18,0),
 @City NUMERIC(18,0),
 @OtherCity VARCHAR(100),
 @Locality VARCHAR(100),
 @RequiredArea NUMERIC(18,0),
 @Amenities varchar(MAX),
 @TransactionType NUMERIC(18,0),
 @MinimumPrice NUMERIC(18,0),
 @MaximumPrice NUMERIC(18,0),
 @BHK VARCHAR(MAX),
 @AgeConstruction NUMERIC(18,0),
 @ClientName varchar(100),
 @ClientMobile varchar(20),
 @ClientEmail varchar(100),
 @ClientState NUMERIC(18,0),
 @ClientCity NUMERIC(18,0),
 @ClientOtherCity varchar(100),
 @ClientAddress varchar(100),
 @ClientAddress2 varchar(100),
 @ClientAddress3 varchar(100),
 @ClientPinCode varchar(20)
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
	
	INSERT INTO  tbl_PostRequirment 
	(
	    RegistrationID,
		PropertyType,
		City,
		CityOther,
		Locality,
		RequiredArea,
		TransactionType,
		Amenities,
		MinimumPrice,
		maximumPrice,
		BHK,
		AgeConstruction,
		ClientName,
		ClientEmail,
		ClientAddress,
		ClientAddress2,
		ClientAddress3,
		ClientMobile,
		ClientState,
		ClientCity,
		ClientCityOther,
		ClientPinCode,
		CreatedDate,
		[Enabled]
     )
	VALUES
	(
	    @RegistrationID,
		@PropertyType,
		@City,
		@OtherCity,
		@Locality,
		@RequiredArea,
		@TransactionType,
		@Amenities,
		@MinimumPrice,
		@maximumPrice,
		@BHK,
		@AgeConstruction,
		@ClientName,
		@ClientEmail,
		@ClientAddress,
		@ClientAddress2,
		@ClientAddress3,
		@ClientMobile,
		@ClientState,
		@ClientCity,
		@ClientOtherCity,
		@ClientPinCode,
		GETDATE(),
		1
	)
END
