CREATE TABLE [dbo].[tbl_TipoElementoOrganigrama] (
    [IdTipoElementoOrganigrama] INT            IDENTITY (1, 1) NOT NULL,
    [NombreElementoOrganigrama] NVARCHAR (500) NULL,
    CONSTRAINT [PK_tbl_TipoElementoOrganigrama] PRIMARY KEY CLUSTERED ([IdTipoElementoOrganigrama] ASC)
);

