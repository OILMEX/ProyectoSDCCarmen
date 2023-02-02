CREATE TABLE [dbo].[tbl_Gerencias] (
    [IdGerencia]           INT            IDENTITY (1, 1) NOT NULL,
    [NombreGerencia]       NVARCHAR (500) NULL,
    [FechaCreacion]        DATETIME       NULL,
    [UsuarioCreacion]      INT            NULL,
    [FechaActualizacion]   DATETIME       NULL,
    [UsuarioActualizacion] INT            NULL,
    [IdSubdireccion]       INT            NULL,
    [Estatus]              BIT            NULL,
    [ClaveGerencias]       NVARCHAR (100) NULL,
    [Tipo]                 NVARCHAR (50)  NULL,
    CONSTRAINT [PK_tbl_Gerencias] PRIMARY KEY CLUSTERED ([IdGerencia] ASC),
    CONSTRAINT [FK_tbl_Gerencias_tbl_Subdirecciones] FOREIGN KEY ([IdSubdireccion]) REFERENCES [dbo].[tbl_Subdirecciones] ([IdSubdireccion])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo: Organigrama o Proyecto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_Gerencias', @level2type = N'COLUMN', @level2name = N'Tipo';

