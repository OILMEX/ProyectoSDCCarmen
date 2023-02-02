CREATE TABLE [dbo].[tbl_Subdirecciones] (
    [IdSubdireccion]       INT            IDENTITY (1, 1) NOT NULL,
    [NombreSubdireccion]   NVARCHAR (500) NULL,
    [FechaCreacion]        DATETIME       NULL,
    [UsuarioCreacion]      INT            NULL,
    [FechaActualizacion]   DATETIME       NULL,
    [UsuarioActualizacion] INT            NULL,
    [Estatus]              BIT            NULL,
    [ClaveSubdireccion]    NVARCHAR (100) NULL,
    [Tipo]                 NVARCHAR (50)  NULL,
    CONSTRAINT [PK_tbl_Subdirecciones] PRIMARY KEY CLUSTERED ([IdSubdireccion] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo: Organigrama o Proyecto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_Subdirecciones', @level2type = N'COLUMN', @level2name = N'Tipo';

