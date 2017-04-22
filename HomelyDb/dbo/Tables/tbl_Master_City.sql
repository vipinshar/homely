CREATE TABLE [dbo].[tbl_Master_City] (
    [CityID]     NUMERIC (18)  NOT NULL,
    [StateID]    NUMERIC (18)  NOT NULL,
    [City]       VARCHAR (100) NOT NULL,
    [Enabled]    BIT           NOT NULL,
    [CreateDate] DATETIME      CONSTRAINT [DF_tbl_City_CreateDate] DEFAULT (getdate()) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_Master_City', @level2type = N'COLUMN', @level2name = N'Enabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_Master_City', @level2type = N'COLUMN', @level2name = N'CreateDate';

