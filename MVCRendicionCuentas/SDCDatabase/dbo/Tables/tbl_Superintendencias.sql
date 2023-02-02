CREATE TABLE [dbo].[tbl_Superintendencias] (
    [IdSuperintendencia]     INT            IDENTITY (1, 1) NOT NULL,
    [NombreSuperIntendencia] NVARCHAR (500) NULL,
    [FechaCreacion]          DATETIME       NULL,
    [UsuarioCreacion]        INT            NULL,
    [FechaActualizacion]     DATETIME       NULL,
    [UsuarioActualizacion]   INT            NULL,
    [IdCoordinacion]         INT            NULL,
    [Estatus]                BIT            NULL,
    [ClaveSuperintendencias] NVARCHAR (100) NULL,
    [Tipo]                   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_tbl_Superintendencias] PRIMARY KEY CLUSTERED ([IdSuperintendencia] ASC),
    CONSTRAINT [FK_tbl_Superintendencias_tbl_Coordinaciones] FOREIGN KEY ([IdCoordinacion]) REFERENCES [dbo].[tbl_Coordinaciones] ([IdCoordinacion])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo: Organigrama o Proyecto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_Superintendencias', @level2type = N'COLUMN', @level2name = N'Tipo';

