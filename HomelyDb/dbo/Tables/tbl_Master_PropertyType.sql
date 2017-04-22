CREATE TABLE [dbo].[tbl_Master_PropertyType] (
    [PropertyTypeID] INT          IDENTITY (1, 1) NOT NULL,
    [PropertyType]   VARCHAR (50) NOT NULL,
    [Enabled]        BIT          CONSTRAINT [DF_tbl_PropertyType_Enabled] DEFAULT ((1)) NOT NULL,
    [CreateDate]     DATETIME     CONSTRAINT [DF_tbl_PropertyType_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tbl_PropertyType] PRIMARY KEY CLUSTERED ([PropertyType] ASC)
);

