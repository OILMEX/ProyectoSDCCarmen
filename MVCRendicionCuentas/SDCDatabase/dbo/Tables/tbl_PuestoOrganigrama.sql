CREATE TABLE [dbo].[tbl_PuestoOrganigrama] (
    [idPuestoOrganigrama]       INT            IDENTITY (1, 1) NOT NULL,
    [NombrePuesto]              NVARCHAR (200) NULL,
    [IdElemento]                INT            NULL,
    [IdTipoElementoOrganigrama] INT            NULL,
    [Estatus]                   BIT            NULL,
    [Clave]                     NVARCHAR (200) NULL,
    CONSTRAINT [PK_tbl_PuestoOrganigrama] PRIMARY KEY CLUSTERED ([idPuestoOrganigrama] ASC)
);

