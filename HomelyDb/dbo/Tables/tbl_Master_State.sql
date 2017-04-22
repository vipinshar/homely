CREATE TABLE [dbo].[tbl_Master_State] (
    [CountryID]  INT           NULL,
    [StateID]    NUMERIC (18)  NOT NULL,
    [State]      VARCHAR (100) NOT NULL,
    [Enabled]    BIT           CONSTRAINT [DF_tbl_State_Enabled] DEFAULT ((1)) NOT NULL,
    [CreateDate] DATETIME      CONSTRAINT [DF_tbl_State_CreateDate] DEFAULT (getdate()) NOT NULL
);

