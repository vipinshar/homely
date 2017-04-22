--PROC_ChangePassword '8800992928', '111111', '111111'
--PROC_ChangePassword 'devendrasinghpratap@gmail.com', '111111', '111111'
CREATE PROCEDURE [dbo].[PROC_ChangePassword]
	@UserName VARCHAR(50), --Email/Mobile
	@OldPwd VARCHAR(20),
	@NewPwd VARCHAR(20)
 AS
BEGIN
       
    --Check is Email or Mobile already registered
	IF EXISTS(SELECT 1
			FROM tbl_Registration
			WHERE (EmailId = @UserName OR Mobile = @UserName) AND [Password] = @OldPwd)
	 BEGIN
	   
		UPDATE tbl_Registration
			SET [Password] = @NewPwd
			WHERE (EmailId = @UserName OR Mobile = @UserName)
			
		SELECT
			 'Password has been updated successfully.' AS ResultMessage
	END  
	ELSE
	BEGIN
		SELECT
			 'User / Password does not exist.' AS ResultMessage
	END
 END

