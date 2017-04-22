/*
Create Date	-	18-Jan-2014
Purpose		-	Select City for the selected State.

Exec [PROC_City_State] 1	
*/

CREATE PROCEDURE [dbo].[PROC_City_State]
@StateId INT
AS
BEGIN

	SELECT 
		Sub.SubCityId,
		Sub.SubCity
		FROM tbl_Master_SubCity Sub INNER JOIN tbl_Master_City Cit ON Cit.CityID = Sub.CityId
		WHERE Cit.StateID = @StateId AND Sub.[Enabled]  = 1 AND Cit.[Enabled]  = 1
END
