CREATE TABLE [dbo].[tbl_Organigrama] (
    [idOrganigrama]        INT            IDENTITY (1, 1) NOT NULL,
    [ClaveElemento]        NVARCHAR (100) NULL,
    [NombreElemento]       NVARCHAR (500) NULL,
    [NodoPadre]            INT            NULL,
    [FechaCreacion]        DATETIME       NULL,
    [UsuarioCreacion]      INT            NULL,
    [FechaActualizacion]   DATETIME       NULL,
    [UsuarioActualizacion] INT            NULL,
    CONSTRAINT [PK_tbl_Organigrama] PRIMARY KEY CLUSTERED ([idOrganigrama] ASC),
    CONSTRAINT [FK_tbl_Organigrama_tbl_Responsables] FOREIGN KEY ([UsuarioCreacion]) REFERENCES [dbo].[tbl_Responsables] ([idResponsable]),
    CONSTRAINT [FK_tbl_Organigrama_tbl_Responsables1] FOREIGN KEY ([UsuarioActualizacion]) REFERENCES [dbo].[tbl_Responsables] ([idResponsable])
);

