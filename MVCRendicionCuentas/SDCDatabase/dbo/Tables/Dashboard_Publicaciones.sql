CREATE TABLE [dbo].[Dashboard_Publicaciones] (
    [IdPublicacion]    INT           IDENTITY (1, 1) NOT NULL,
    [FechaPublicacion] DATETIME      NULL,
    [TipoPublicacion]  NVARCHAR (50) NULL,
    [IdUbicacion]      INT           NULL,
    CONSTRAINT [PK_tbl_Publicaciones] PRIMARY KEY CLUSTERED ([IdPublicacion] ASC)
);



