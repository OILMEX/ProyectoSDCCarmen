CREATE TABLE [dbo].[Dashboard_PublicacionAvanceSubsistema] (
    [IdPublicacionAvanceSubsistema] INT           IDENTITY (1, 1) NOT NULL,
    [IdSubsistema]                  INT           NULL,
    [IdPublicacion]                 INT           NULL,
    [Valor]                         INT           NULL,
    [Semaforo]                      NVARCHAR (50) NULL,
    [EstatusMejora]                 INT           NULL,
    CONSTRAINT [PK_Dashboard_PublicacionAvanceSubsistema] PRIMARY KEY CLUSTERED ([IdPublicacionAvanceSubsistema] ASC),
    CONSTRAINT [FK_Dashboard_PublicacionAvanceSubsistema_Dashboard_Publicaciones] FOREIGN KEY ([IdPublicacion]) REFERENCES [dbo].[Dashboard_Publicaciones] ([IdPublicacion]) ON DELETE CASCADE,
    CONSTRAINT [FK_Dashboard_PublicacionAvanceSubsistema_tbl_Subsistemas] FOREIGN KEY ([IdSubsistema]) REFERENCES [dbo].[tbl_Subsistemas] ([IdSubsistema])
);





