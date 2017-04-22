CREATE procedure [dbo].[PROC_Search1]
                
    AS
        BEGIN
               SELECT 
		            PropertyCode,
		          
		            --Image1,
		              Rent,
		            Bedroom,
            		Bathroom,
		            Area,
		            Locality,
		            CreateDate
		      FROM tbl_Property
		       WHERE  [Enabled] = 1 And Rented = 0
		        Order By CreateDate Desc 
         END

