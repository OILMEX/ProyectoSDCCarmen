CREATE TABLE [dbo].[Dashboard_PublicacionElemento] (
    [IdPublicacionElemento] INT            IDENTITY (1, 1) NOT NULL,
    [NombreElemento]        NVARCHAR (500) NULL,
    [IdElemento]            INT            NULL,
    [Valor]                 INT            NULL,
    [Semaforo]              NVARCHAR (50)  NULL,
    [EstatusMejora]         INT            NULL,
    [IdSubsistema]          INT            NULL,
    [IdPublicacion]         INT            NULL,
    CONSTRAINT [PK_Dashboard_PublicacioinElemento] PRIMARY KEY CLUSTERED ([IdPublicacionElemento] ASC),
    CONSTRAINT [FK_Dashboard_PublicacionElemento_Dashboard_Publicaciones] FOREIGN KEY ([IdPublicacion]) REFERENCES [dbo].[Dashboard_Publicaciones] ([IdPublicacion]) ON DELETE CASCADE,
    CONSTRAINT [FK_Dashboard_PublicacionElemento_tbl_Elementos] FOREIGN KEY ([IdElemento]) REFERENCES [dbo].[tbl_Elementos] ([IdElemento]),
    CONSTRAINT [FK_Dashboard_PublicacionElemento_tbl_Subsistemas] FOREIGN KEY ([IdSubsistema]) REFERENCES [dbo].[tbl_Subsistemas] ([IdSubsistema])
);





