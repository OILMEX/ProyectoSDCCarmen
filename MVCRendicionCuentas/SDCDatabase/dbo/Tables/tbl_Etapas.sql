CREATE TABLE [dbo].[tbl_Etapas] (
    [IdEtapa]              INT            IDENTITY (1, 1) NOT NULL,
    [NombreEtapa]          NVARCHAR (500) NULL,
    [Estatus]              BIT            NULL,
    [FechaCreacion]        DATETIME       NULL,
    [UsuarioCreacion]      INT            NULL,
    [FechaActualizacion]   DATETIME       NULL,
    [UsuarioActualizacion] INT            NULL,
    [Clave]                INT            NULL,
    CONSTRAINT [PK_tbl_Etapas] PRIMARY KEY CLUSTERED ([IdEtapa] ASC),
    CONSTRAINT [FK_tbl_Etapas_tbl_Responsables] FOREIGN KEY ([UsuarioActualizacion]) REFERENCES [dbo].[tbl_Responsables] ([idResponsable])
);

