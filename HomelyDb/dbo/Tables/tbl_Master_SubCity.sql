CREATE TABLE [dbo].[tbl_Master_SubCity] (
    [SubCity]    VARCHAR (100) NOT NULL,
    [SubCityId]  INT           IDENTITY (1, 1) NOT NULL,
    [CityId]     INT           NOT NULL,
    [Enabled]    BIT           CONSTRAINT [DF_tbl_SubCity_Enabled] DEFAULT ((1)) NOT NULL,
    [CreateDate] DATETIME      CONSTRAINT [DF_tbl_SubCity_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tbl_SubCity] PRIMARY KEY CLUSTERED ([SubCityId] ASC)
);

