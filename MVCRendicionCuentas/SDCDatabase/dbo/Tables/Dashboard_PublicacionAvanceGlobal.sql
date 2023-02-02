CREATE TABLE [dbo].[Dashboard_PublicacionAvanceGlobal] (
    [IdPublicacionAvanceG] INT           IDENTITY (1, 1) NOT NULL,
    [IdPublicacion]        INT           NULL,
    [Valor]                INT           NULL,
    [EstatusMejora]        NVARCHAR (50) NULL,
    CONSTRAINT [PK_Dashboard_PublicacionAvanceGlobal] PRIMARY KEY CLUSTERED ([IdPublicacionAvanceG] ASC),
    CONSTRAINT [FK_Dashboard_PublicacionAvanceGlobal_Dashboard_Publicaciones] FOREIGN KEY ([IdPublicacion]) REFERENCES [dbo].[Dashboard_Publicaciones] ([IdPublicacion])
);

