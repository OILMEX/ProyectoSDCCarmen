CREATE TABLE [dbo].[Config_SubEtapaIndicador] (
    [IdConfigSubEtapaIndicador] INT IDENTITY (1, 1) NOT NULL,
    [IdIndicador]               INT NULL,
    [IdSubEtapa]                INT NULL,
    [Estatus]                   BIT NULL,
    CONSTRAINT [PK_Config_SubEtapaIndicador] PRIMARY KEY CLUSTERED ([IdConfigSubEtapaIndicador] ASC),
    CONSTRAINT [FK_Config_SubEtapaIndicador_tbl_Indicadores] FOREIGN KEY ([IdIndicador]) REFERENCES [dbo].[tbl_Indicadores] ([IdIndicador]) ON DELETE CASCADE,
    CONSTRAINT [FK_Config_SubEtapaIndicador_tbl_SubEtapas] FOREIGN KEY ([IdSubEtapa]) REFERENCES [dbo].[tbl_SubEtapas] ([IdSubEtapa])
);





