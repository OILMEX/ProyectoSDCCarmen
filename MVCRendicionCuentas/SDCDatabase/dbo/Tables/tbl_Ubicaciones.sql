CREATE TABLE [dbo].[tbl_Ubicaciones] (
    [idUbicacion]          INT            IDENTITY (1, 1) NOT NULL,
    [Ubicacion]            NVARCHAR (500) NULL,
    [Estatus]              BIT            NULL,
    [UsuarioCreacion]      INT            NULL,
    [FechaCreacion]        DATETIME       NULL,
    [UsuarioActualizacion] INT            NULL,
    [FechaActualizacion]   DATETIME       NULL,
    CONSTRAINT [PK_tbl_Ubicaciones] PRIMARY KEY CLUSTERED ([idUbicacion] ASC)
);

