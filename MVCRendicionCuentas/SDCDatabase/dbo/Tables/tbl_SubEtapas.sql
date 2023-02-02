CREATE TABLE [dbo].[tbl_SubEtapas] (
    [IdSubEtapa]           INT            IDENTITY (1, 1) NOT NULL,
    [IdEtapa]              INT            NULL,
    [NombreSubEtapa]       NVARCHAR (500) NULL,
    [Estatus]              BIT            NULL,
    [FechaCreacion]        DATETIME       NULL,
    [UsuarioCreacion]      INT            NULL,
    [FechaActualizacion]   DATETIME       NULL,
    [UsuarioActualizacion] INT            NULL,
    [Clave]                FLOAT (53)     NULL,
    CONSTRAINT [PK_tbl_SubEtapas] PRIMARY KEY CLUSTERED ([IdSubEtapa] ASC),
    CONSTRAINT [FK_tbl_SubEtapas_tbl_Etapas] FOREIGN KEY ([UsuarioCreacion]) REFERENCES [dbo].[tbl_Responsables] ([idResponsable]),
    CONSTRAINT [FK_tbl_SubEtapas_tbl_Responsables] FOREIGN KEY ([UsuarioCreacion]) REFERENCES [dbo].[tbl_Responsables] ([idResponsable]),
    CONSTRAINT [FK_tbl_SubEtapas_tbl_Responsables1] FOREIGN KEY ([UsuarioActualizacion]) REFERENCES [dbo].[tbl_Responsables] ([idResponsable])
);

