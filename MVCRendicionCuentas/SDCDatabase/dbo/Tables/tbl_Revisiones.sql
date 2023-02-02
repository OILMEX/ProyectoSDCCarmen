CREATE TABLE [dbo].[tbl_Revisiones] (
    [idRevisiones]                 INT  IDENTITY (1, 1) NOT NULL,
    [FechaRevision]                DATE NULL,
    [IdConfigIndicadorResponsable] INT  NULL,
    CONSTRAINT [PK_tbl_Revisiones] PRIMARY KEY CLUSTERED ([idRevisiones] ASC),
    CONSTRAINT [FK_tbl_Revisiones_Config_IndicadorResponsable] FOREIGN KEY ([IdConfigIndicadorResponsable]) REFERENCES [dbo].[Config_IndicadorResponsable] ([IdConfigIndicadorResponsable]) ON DELETE CASCADE
);





