
CREATE PROCEDURE [dbo].[PROC_ForgotPassword]
	@EmailId VARCHAR(50),
	@Mobile VARCHAR(20)
 AS
    BEGIN
       
        --Check is Email or Mobile already registered
      IF EXISTS(SELECT 1
				FROM tbl_Registration
				WHERE EmailId = @EmailId AND Mobile = @Mobile)
         BEGIN
           
              SELECT FirstName,EmailId,[Password] from tbl_Registration where @EmailId=EmailId AND @Mobile=Mobile
			
         END  
      ELSE
       SELECT
             'The Email Id and Mobile No. provodeYou by you does not exist. Please check entered Email Id and Mobile No.' AS ResultMessage,
			0 AS Result
 END

