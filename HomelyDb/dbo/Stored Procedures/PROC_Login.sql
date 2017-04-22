
CREATE PROCEDURE [dbo].[PROC_Login]
    @UserID VARCHAR(50), --Email/Mobile
	@Password VARCHAR(20)
AS
 
BEGIN 
     SELECT 
      Alternate,
	Mobile,
		EmailId,
		FirstName 
		FROM tbl_Registration 
		WHERE (EmailId = @UserID OR Mobile = @UserID) AND [Password] = @Password AND [Enabled] = 1
END
