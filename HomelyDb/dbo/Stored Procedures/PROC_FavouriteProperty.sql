--PROC_FavouriteProperty '8800992928'
CREATE PROCEDURE [dbo].[PROC_FavouriteProperty]
@UserName VARCHAR(50)
AS
BEGIN

	SELECT
		Pro.Locality,
		Pro.SocietyName,
		Pro.Rent,
		Pro.Area,
		Pro.Bathroom,
		Pro.Bedroom,
		Pro.PropertyCode,
		Pro.TransactionType,
		Pro.CreateDate
		FROM tbl_Registration Reg INNER JOIN tbl_FavouriteProperty Fav ON Reg.RegistrationID = Fav.RegistrationID 
			INNER JOIN tbl_Property Pro ON Pro.PropertyCode = Fav.PropertyCode  
			INNER JOIN tbl_Master_SubCity Sub ON Pro.PropertyCity = Sub.SubCityId
			INNER JOIN tbl_Master_City Cit ON Cit.CityID = Sub.CityId
		WHERE (Reg.EmailId = @UserName OR Reg.Mobile = @UserName) 
			AND Pro.[Enabled]  = 1 And Rented = 0 AND Pro.Verified = 1	
			AND Sub.[Enabled]  = 1 AND Cit.[Enabled]  = 1
		Order By Pro.CreateDate Desc	
END
