CREATE TABLE [dbo].[Data_ValoresIndicador] (
    [IdDataValoresIndicadores]     INT           IDENTITY (1, 1) NOT NULL,
    [Valor]                        FLOAT (53)    NULL,
    [FecCreacion]                  DATETIME      NULL,
    [Tipo]                         NVARCHAR (50) NULL,
    [IdConfigIndicadorResponsable] INT           NULL,
    CONSTRAINT [PK_Data_ValoresIndicador] PRIMARY KEY CLUSTERED ([IdDataValoresIndicadores] ASC),
    CONSTRAINT [FK_Data_ValoresIndicador_Config_IndicadorResponsable] FOREIGN KEY ([IdConfigIndicadorResponsable]) REFERENCES [dbo].[Config_IndicadorResponsable] ([IdConfigIndicadorResponsable])
);





