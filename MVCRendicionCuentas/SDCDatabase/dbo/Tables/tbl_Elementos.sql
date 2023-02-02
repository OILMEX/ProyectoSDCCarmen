CREATE TABLE [dbo].[tbl_Elementos] (
    [IdElemento]           INT            IDENTITY (1, 1) NOT NULL,
    [IdSubsistema]         INT            NULL,
    [NombreElemento]       NVARCHAR (300) NULL,
    [DescripcionElemento]  NVARCHAR (500) NULL,
    [Estatus]              BIT            NULL,
    [FechaCreacion]        DATETIME       NULL,
    [UsuarioCreacion]      INT            NULL,
    [FechaActualizacion]   DATETIME       NULL,
    [UsuarioActualizacion] INT            NULL,
    CONSTRAINT [PK_tbl_Elementos] PRIMARY KEY CLUSTERED ([IdElemento] ASC),
    CONSTRAINT [FK_tbl_Elementos_tbl_Subsistemas] FOREIGN KEY ([IdSubsistema]) REFERENCES [dbo].[tbl_Subsistemas] ([IdSubsistema])
);

