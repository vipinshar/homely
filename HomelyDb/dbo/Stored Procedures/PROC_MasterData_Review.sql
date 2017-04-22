/*
Create Date	-	08-Feb-2014
Exec [PROC_MasterData_Review]
*/

CREATE PROCEDURE [dbo].[PROC_MasterData_Review]
AS
BEGIN

	--Ownership
	SELECT 
		OwnershipId,
		[Ownership]
		FROM tbl_Master_Ownership
		WHERE [Enabled] = 1 
		ORDER BY [Order]
		
	--Society
	SELECT DISTINCT
		SocietyName
		FROM tbl_Property
		WHERE [Enabled] = 1 AND ISNULL(SocietyName, '') != '' 
		ORDER BY SocietyName
		
	--Location
	SELECT DISTINCT
		Locality
		FROM tbl_Property
		WHERE [Enabled] = 1 AND ISNULL(Locality, '') != '' 
		ORDER BY Locality
		
	--Locality
	SELECT DISTINCT
		Locality
		FROM tbl_Property
		WHERE [Enabled] = 1 AND ISNULL(Locality, '') != ''  AND 1 != 1
		ORDER BY Locality
END
