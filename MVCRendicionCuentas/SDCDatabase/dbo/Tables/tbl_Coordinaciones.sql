CREATE TABLE [dbo].[tbl_Coordinaciones] (
    [IdCoordinacion]       INT            IDENTITY (1, 1) NOT NULL,
    [NombreCoordinacion]   NVARCHAR (500) NULL,
    [FechaCreacion]        DATETIME       NULL,
    [UsuarioCreacion]      INT            NULL,
    [FechaActualizacion]   DATETIME       NULL,
    [UsuarioActualizacion] INT            NULL,
    [IdGerencia]           INT            NULL,
    [Estatus]              BIT            NULL,
    [ClaveCoordinaciones]  NVARCHAR (100) NULL,
    [Tipo]                 NVARCHAR (50)  NULL,
    CONSTRAINT [PK_tbl_Coordinaciones] PRIMARY KEY CLUSTERED ([IdCoordinacion] ASC),
    CONSTRAINT [FK_tbl_Coordinaciones_tbl_Gerencias] FOREIGN KEY ([IdGerencia]) REFERENCES [dbo].[tbl_Gerencias] ([IdGerencia])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo: Organigrama o Proyecto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_Coordinaciones', @level2type = N'COLUMN', @level2name = N'Tipo';

