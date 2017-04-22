CREATE TABLE [dbo].[tbl_Master_Messages] (
    [MessageId]   NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [Message]     NVARCHAR (MAX) NOT NULL,
    [MessageFor]  VARCHAR (20)   NOT NULL,
    [MessageType] VARCHAR (20)   NOT NULL,
    [Enabled]     BIT            CONSTRAINT [DF_tbl_Master_Messages_Enabled] DEFAULT ((1)) NOT NULL,
    [CreateDate]  DATETIME       CONSTRAINT [DF_tbl_Master_Messages_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tbl_Master_Messages] PRIMARY KEY CLUSTERED ([MessageFor] ASC, [MessageType] ASC)
);

