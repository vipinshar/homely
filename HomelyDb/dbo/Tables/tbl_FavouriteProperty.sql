CREATE TABLE [dbo].[tbl_FavouriteProperty] (
    [FavoriteId]     UNIQUEIDENTIFIER CONSTRAINT [DF_Table_1_FavoriteID] DEFAULT (newid()) NOT NULL,
    [RegistrationId] UNIQUEIDENTIFIER NOT NULL,
    [PropertyCode]   VARCHAR (20)     NOT NULL,
    [CreateDate]     DATETIME         CONSTRAINT [DF_tbl_FavoriteProperty_CreateDate] DEFAULT (getdate()) NOT NULL,
    [Deleted]        BIT              CONSTRAINT [DF_tbl_FavoriteProperty_Deleted] DEFAULT ((0)) NOT NULL
);

