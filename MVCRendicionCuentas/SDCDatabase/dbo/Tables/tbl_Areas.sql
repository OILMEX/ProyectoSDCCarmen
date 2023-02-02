CREATE TABLE [dbo].[tbl_Areas] (
    [idArea]     INT            IDENTITY (1, 1) NOT NULL,
    [NombreArea] NVARCHAR (200) NULL,
    CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED ([idArea] ASC)
);

