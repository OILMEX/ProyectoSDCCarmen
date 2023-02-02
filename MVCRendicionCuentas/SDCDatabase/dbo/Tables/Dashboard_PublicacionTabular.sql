CREATE TABLE [dbo].[Dashboard_PublicacionTabular] (
    [IdPublicacionTabular] INT           IDENTITY (1, 1) NOT NULL,
    [Mes]                  INT           NULL,
    [Valor]                INT           NULL,
    [Semaforo]             NVARCHAR (50) NULL,
    [IdPublicacion]        INT           NULL,
    [IdSubsistema]         INT           NULL,
    CONSTRAINT [PK_Dashboard_PublicacionTabular] PRIMARY KEY CLUSTERED ([IdPublicacionTabular] ASC),
    CONSTRAINT [FK_Dashboard_PublicacionTabular_Dashboard_Publicaciones] FOREIGN KEY ([IdPublicacion]) REFERENCES [dbo].[Dashboard_Publicaciones] ([IdPublicacion]) ON DELETE CASCADE,
    CONSTRAINT [FK_Dashboard_PublicacionTabular_tbl_Subsistemas] FOREIGN KEY ([IdSubsistema]) REFERENCES [dbo].[tbl_Subsistemas] ([IdSubsistema])
);



