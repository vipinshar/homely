CREATE TABLE [dbo].[tbl_Master_Facing] (
    [FacingId]   NUMERIC (18) IDENTITY (1, 1) NOT NULL,
    [Facing]     VARCHAR (20) NOT NULL,
    [Enabled]    BIT          CONSTRAINT [DF_tbl_Facing_Enabled] DEFAULT ((1)) NOT NULL,
    [Order]      NUMERIC (18) CONSTRAINT [DF_tbl_Facing_Order] DEFAULT ((0)) NOT NULL,
    [CreateDate] DATETIME     CONSTRAINT [DF_tbl_Facing_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Facing] PRIMARY KEY CLUSTERED ([Facing] ASC)
);

