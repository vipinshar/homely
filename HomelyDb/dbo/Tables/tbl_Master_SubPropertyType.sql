CREATE TABLE [dbo].[tbl_Master_SubPropertyType] (
    [SubPropertyTypeID] NUMERIC (18) IDENTITY (1, 1) NOT NULL,
    [PropertyTypeID]    INT          NOT NULL,
    [SubPropertyType]   VARCHAR (50) NOT NULL,
    [Enabled]           BIT          CONSTRAINT [DF_tbl_SubPropertyType_Enabled] DEFAULT ((1)) NOT NULL,
    [Order]             NUMERIC (18) CONSTRAINT [DF_tbl_Master_SubPropertyType_Order] DEFAULT ((0)) NOT NULL,
    [CreateDate]        DATETIME     CONSTRAINT [DF_tbl_SubPropertyType_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tbl_SubPropertyType] PRIMARY KEY CLUSTERED ([SubPropertyType] ASC)
);

