CREATE TABLE [dbo].[tbl_Responsables] (
    [idResponsable]        INT            IDENTITY (1, 1) NOT NULL,
    [NombreResponsable]    NVARCHAR (500) NULL,
    [Usuario]              NVARCHAR (200) NULL,
    [Contrasenia]          NVARCHAR (200) NULL,
    [Correo]               NVARCHAR (200) NULL,
    [Ficha]                NVARCHAR (200) NULL,
    [IdPuesto]             INT            NULL,
    [IdArea]               INT            NULL,
    [IdAquienReporta]      INT            NULL,
    [Rol]                  NVARCHAR (50)  NULL,
    [Estatus]              BIT            NULL,
    [IdUbicacion]          INT            NULL,
    [FechaActualizacion]   DATETIME       NULL,
    [UsuarioActualizacion] INT            NULL,
    CONSTRAINT [PK_tbl_Responsables] PRIMARY KEY CLUSTERED ([idResponsable] ASC),
    CONSTRAINT [FK_tbl_Responsables_tbl_Ubicaciones] FOREIGN KEY ([IdUbicacion]) REFERENCES [dbo].[tbl_Ubicaciones] ([idUbicacion])
);





