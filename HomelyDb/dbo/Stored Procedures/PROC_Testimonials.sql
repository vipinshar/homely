/*
Exec PROC_Testimonials		
*/

CREATE PROCEDURE [dbo].[PROC_Testimonials]
AS
BEGIN

	--Propery reviews
	SELECT
		Review,
		Name
		FROM tbl_PropertyReview
		WHERE [Enabled]  = 1
		Order By CreateDate Desc
END
