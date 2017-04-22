CREATE TABLE [dbo].[tbl_Master_Availability] (
    [AvailabilityId] NUMERIC (18) IDENTITY (1, 1) NOT NULL,
    [Availability]   VARCHAR (20) NOT NULL,
    [Enabled]        BIT          CONSTRAINT [DF_tbl_Master_Availability_Enabled] DEFAULT ((1)) NOT NULL,
    [Order]          NUMERIC (18) CONSTRAINT [DF_tbl_Master_Availability_Order] DEFAULT ((0)) NOT NULL,
    [CreateDate]     DATETIME     CONSTRAINT [DF_tbl_Master_Availability_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Master_Availability] PRIMARY KEY CLUSTERED ([Availability] ASC)
);

