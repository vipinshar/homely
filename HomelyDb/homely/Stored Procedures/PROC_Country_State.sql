CREATE PROCEDURE [homely].[PROC_Country_State]
                 @CountryId INT
AS
   BEGIN
        SELECT
        [State],
		[StateID]
		FROM tbl_Master_State Sub INNER JOIN tbl_Master_Country Ct ON Ct.CountryID = Sub.CountryId
		WHERE Ct.CountryID = @CountryId AND Sub.[Enabled]  = 1 AND Ct.[Enabled]  = 1
    END 
  
