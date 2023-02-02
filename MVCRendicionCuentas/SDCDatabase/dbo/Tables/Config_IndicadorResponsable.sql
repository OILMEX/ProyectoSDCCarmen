CREATE TABLE [dbo].[Config_IndicadorResponsable] (
    [IdConfigIndicadorResponsable]      INT IDENTITY (1, 1) NOT NULL,
    [IdResponsable]                     INT NULL,
    [IdIndicador]                       INT NULL,
    [IdConfigRelacionIndicadorProyecto] INT NULL,
    [Estatus]                           BIT NULL,
    CONSTRAINT [PK_Config_IndicadorResponsable] PRIMARY KEY CLUSTERED ([IdConfigIndicadorResponsable] ASC),
    CONSTRAINT [FK_Config_IndicadorResponsable_Config_RelacionIndicadorProyecto] FOREIGN KEY ([IdConfigRelacionIndicadorProyecto]) REFERENCES [dbo].[Config_RelacionIndicadorProyecto] ([IdConfigRelacionIndicadorProyecto]) ON DELETE CASCADE,
    CONSTRAINT [FK_Config_IndicadorResponsable_tbl_Indicadores] FOREIGN KEY ([IdIndicador]) REFERENCES [dbo].[tbl_Indicadores] ([IdIndicador]) ON DELETE CASCADE,
    CONSTRAINT [FK_Config_IndicadorResponsable_tbl_Responsables] FOREIGN KEY ([IdResponsable]) REFERENCES [dbo].[tbl_Responsables] ([idResponsable]) ON DELETE CASCADE
);



