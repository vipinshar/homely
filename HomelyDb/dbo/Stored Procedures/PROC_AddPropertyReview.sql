
/*******************************************************************************************************
Crete Date	- 08-Feb-2014 
Purpose		- Add a property review.

*******************************************************************************************************/

CREATE PROCEDURE [dbo].[PROC_AddPropertyReview]
	@OwnershipType NUMERIC(18,0),
	@ReviewFor VARCHAR(50),
	@ReviewForValue VARCHAR(50),
	@Review VARCHAR(MAX),
	@RatingLocation1 VARCHAR(10),
	@RatingLocation2 VARCHAR(10),
	@RatingLocation3 VARCHAR(10),
	@RatingLocation4 VARCHAR(10),
	@RatingLocation5 VARCHAR(10),
	@RatingLocation6 VARCHAR(10),
	@RatingSociety1 VARCHAR(10),
	@RatingSociety2 VARCHAR(10),
	@RatingSociety3 VARCHAR(10),
	@RatingSociety4 VARCHAR(10),
	@RatingSociety5 VARCHAR(10),
	@RatingSociety6 VARCHAR(10),
	@RatingSociety7 VARCHAR(10)
AS
BEGIN

	--Add Property review
	INSERT INTO tbl_PropertyReview
	(
		OwnershipType,
		ReviewFor,
		ReviewForValue,
		Review,
		RatingLocation1,
		RatingLocation2,
		RatingLocation3,
		RatingLocation4,
		RatingLocation5,
		RatingLocation6,
		RatingSociety1,
		RatingSociety2,
		RatingSociety3,
		RatingSociety4,
		RatingSociety5,
		RatingSociety6,
		RatingSociety7
	)
	VALUES
	(
		@OwnershipType,
		@ReviewFor,
		@ReviewForValue,
		@Review,
		@RatingLocation1,
		@RatingLocation2,
		@RatingLocation3,
		@RatingLocation4,
		@RatingLocation5,
		@RatingLocation6,
		@RatingSociety1,
		@RatingSociety2,
		@RatingSociety3,
		@RatingSociety4,
		@RatingSociety5,
		@RatingSociety6,
		@RatingSociety7
	)
	
	SELECT 
		'Rate / Review has been submitted successfully.' AS ResultMessage
END
