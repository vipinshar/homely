--select * from [Split]('1,2,3,4,5,6,7', ',')
CREATE FUNCTION [dbo].[Split]
(
	@RowData varchar(2000),
	@SplitOn varchar(5)
)  
RETURNs @RtnValue table 
(
	Id int identity(1,1),
	Data varchar(100)
) 
AS  
BEGIN 
	Declare @Cnt int
	Set @Cnt = 1

	While (Charindex(@SplitOn,@RowData)>0)
	Begin
		Insert Into @RtnValue (data)
		Select 
			Data = ltrim(rtrim(Substring(@RowData,1,Charindex(@SplitOn,@RowData)-1)))

		Set @RowData = Substring(@RowData,Charindex(@SplitOn,@RowData)+1,len(@RowData))
		Set @Cnt = @Cnt + 1
	End
	
	Insert Into @RtnValue (data)
	Select Data = ltrim(rtrim(@RowData))

	Return
END










