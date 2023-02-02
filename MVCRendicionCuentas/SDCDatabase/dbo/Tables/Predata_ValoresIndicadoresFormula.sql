CREATE TABLE [dbo].[Predata_ValoresIndicadoresFormula] (
    [IdValoresIndicadoresFormula] INT        IDENTITY (1, 1) NOT NULL,
    [ValorProgramado]             FLOAT (53) NULL,
    [ValorReal]                   FLOAT (53) NULL,
    [Resultado]                   FLOAT (53) NULL,
    [IdDataValoresIndicadores]    INT        NULL,
    CONSTRAINT [PK_Predata_ValoresIndicadoresFormula] PRIMARY KEY CLUSTERED ([IdValoresIndicadoresFormula] ASC),
    CONSTRAINT [FK_Predata_ValoresIndicadoresFormula_Data_ValoresIndicador] FOREIGN KEY ([IdDataValoresIndicadores]) REFERENCES [dbo].[Data_ValoresIndicador] ([IdDataValoresIndicadores]) ON DELETE CASCADE
);



