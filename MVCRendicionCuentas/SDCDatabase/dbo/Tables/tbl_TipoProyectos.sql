CREATE TABLE [dbo].[tbl_TipoProyectos] (
    [IdTipoProyecto]       INT            IDENTITY (1, 1) NOT NULL,
    [TipoProyecto]         NVARCHAR (200) NULL,
    [Estatus]              BIT            NULL,
    [UsuarioCreacion]      INT            NULL,
    [FechaCreacion]        DATETIME       NULL,
    [UsuarioActualizacion] INT            NULL,
    [FechaActualizacion]   DATETIME       NULL,
    CONSTRAINT [PK_tbl_TipoProyectos] PRIMARY KEY CLUSTERED ([IdTipoProyecto] ASC)
);

