
/*******************************************************************************************************
Crete Date	- 04-Jan-2014 
Purpose		- Add a new property.

*******************************************************************************************************/

CREATE PROCEDURE [dbo].[PROC_AddProperty]
	@EmailId VARCHAR(50), -- Will be used to identify that the property is registered by registered  member.
	@TransactionType INT,
	@OwnershipType INT,
	@PropertyType INT,
	@PropertyCity INT,
	@Locality VARCHAR(100),
	@Area INT,
	@Measure varchar(50),
	@Rent INT,
	@SocietyName VARCHAR(50),
	@Bedroom INT,
	@Floor INT,
	@Description VARCHAR(500),
	@Bathroom INT,
	@Balcony INT,
	@FloorInBuilding VARCHAR(50),
	--@AgeOfConstruction INT,
	@Furnished INT,
	@Facing INT,
	@Amenities VARCHAR(MAX),
	@ShoppingMallDistance INT,
	@HospitalDistance INT,
	@SchoolDistance INT,
	@ATMDistance INT,
	--Member details
	@Name VARCHAR(50),
	@MemberCity INT,
	@MemberAddress VARCHAR(500),
	@Mobile VARCHAR(15),
	@AlternateNumber VARCHAR(15),
	@MemberEmailID VARCHAR(50),
	@Availability int,
	@PropertyOtherCity VARCHAR(50),
	@OwnerOtherCity VARCHAR(50),
	@MemberState INT,
	@MemberAddress2 VARCHAR(500),
	@MemberAddress3 VARCHAR(500),
	@MemberPinCode VARCHAR(20)
AS
BEGIN

	DECLARE @RegistrationID UNIQUEIDENTIFIER = NULL,
			@PropertyNumberSeries INT = NULL
	
	--If Email is not blank that means Property is added by registered user.		
	IF(ISNULL(@EmailId, '') != '')
	BEGIN
		SELECT @RegistrationID = RegistrationID
			FROM tbl_Registration
			WHERE EmailId = @EmailId
	END
	
	--Add Property
	INSERT INTO tbl_Property
	(
		RegistrationID,
		TransactionType,
		OwnershipType,
		SubPropertyType,
		PropertyCity,
		Locality,
		Area,
		Measure,
		Rent,
		SocietyName,
		Bedroom,
		[Floor],
		[Description],
		Bathroom,
		Balcony,
		FloorInBuilding,
		--AgeOfConstruction,
		Furnished,
		Facing,
		Amenities,
		ShoppingMallDistance,
		HospitalDistance,
		SchoolDistance,
		ATMDistance,
		OwnerName,
		OwnerCity,
		OwnerAddress,
		Mobile,
		AlternateNumber,
		EmailID,
		Verified,
		Availability,
		PropertyOtherCity,
		OwnerOtherCity,
		OwnerState,
		OwnerAddress2,
		OwnerAddress3,
		OwnerPinCode
	)
	VALUES
	(
		@RegistrationID,
		@TransactionType,
		@OwnershipType,
		@PropertyType,
		@PropertyCity,
		@Locality,
		@Area,
		@Measure,
		@Rent,
		@SocietyName,
		@Bedroom,
		@Floor,
		@Description,
		@Bathroom,
		@Balcony,
		@FloorInBuilding,
		--@AgeOfConstruction,
		@Furnished,
		@Facing,
		@Amenities,
		@ShoppingMallDistance,
		@HospitalDistance,
		@SchoolDistance,
		@ATMDistance,
		@Name,
		@MemberCity,
		@MemberAddress,
		@Mobile,
		@AlternateNumber,
		@MemberEmailID,
		1,
		@Availability,
		@PropertyOtherCity,
		@OwnerOtherCity,
		@MemberState,
		@MemberAddress2,
		@MemberAddress3,
		@MemberPinCode	
	)
	
	--Wiil get last generated value in the Idenetity column
	SELECT @PropertyNumberSeries = @@IDENTITY
		FROM tbl_Property
	
	--Set Property code (id)
	UPDATE tbl_Property
		SET PropertyCode = PropertyCode + CAST(PropertyNumberSeries AS VARCHAR(50))
		WHERE PropertyNumberSeries = @PropertyNumberSeries

	--Get Propert code (id) which will be used to show in Thank You page.
	SELECT 
		PropertyCode
		FROM tbl_Property
		WHERE PropertyNumberSeries = @PropertyNumberSeries
END
