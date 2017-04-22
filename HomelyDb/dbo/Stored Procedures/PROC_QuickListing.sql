CREATE PROCEDURE [dbo].[PROC_QuickListing]
                  @EmailID VARCHAR(50),
                  @ContactingFor VARCHAR(50),
                  @Name VARCHAR(50),
                  @Email VARCHAR(20),
                  @Mobile VARCHAR(20)
  AS
    BEGIN
       DECLARE @RegistrationID UNIQUEIDENTIFIER = NULL
	
	IF(ISNULL(@EmailID , '') != '')
	BEGIN
		SELECT @RegistrationID = RegistrationID
			FROM tbl_Registration
			WHERE EmailId = @EmailId
	END  
          INSERT INTO tbl_QuickListing
          ( 
            RegistrationID,
            ContactFor,
            Name,
            Email,
            Mobile,
            CreateDate,
            [Enabled]
          ) 
          VALUES 
          ( 
            @RegistrationID,
            @ContactingFor,
            @Name,
            @Email,
            @Mobile,
            GETDATE(),
            1
           )
    END
    
   
