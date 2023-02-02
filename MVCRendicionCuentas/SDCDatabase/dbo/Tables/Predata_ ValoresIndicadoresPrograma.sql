CREATE TABLE [dbo].[Predata_ ValoresIndicadoresPrograma] (
    [IdPreVIP]                 INT        IDENTITY (1, 1) NOT NULL,
    [IdDataValoresIndicadores] INT        NULL,
    [EneroProg]                INT        NULL,
    [FebreroProg]              INT        NULL,
    [MarzoProg]                INT        NULL,
    [AbrilProg]                INT        NULL,
    [MayoProg]                 INT        NULL,
    [JunioProg]                INT        NULL,
    [JulioProg]                INT        NULL,
    [AgostoProg]               INT        NULL,
    [SeptiembreProg]           INT        NULL,
    [OctubreProg]              INT        NULL,
    [NoviembreProg]            INT        NULL,
    [DiciembreProg]            INT        NULL,
    [ProyeccionAnualProg]      INT        NULL,
    [AnioSiguienteProg]        INT        NULL,
    [AnioAnteriorReal]         INT        NULL,
    [EneroReal]                INT        NULL,
    [FebreroReal]              INT        NULL,
    [MarzoReal]                INT        NULL,
    [AbrilReal]                INT        NULL,
    [MayoReal]                 INT        NULL,
    [JunioReal]                INT        NULL,
    [JulioReal]                INT        NULL,
    [AgostoReal]               INT        NULL,
    [SeptiembreReal]           INT        NULL,
    [OctubreReal]              INT        NULL,
    [NoviembreReal]            INT        NULL,
    [DiciembreReal]            INT        NULL,
    [ProyeccionAnualReal]      INT        NULL,
    [AnioSiguienteReal]        INT        NULL,
    [FecCreacion]              DATETIME   NULL,
    [Resultado]                FLOAT (53) NULL,
    [AnioAnteriorProg]         INT        NULL,
    CONSTRAINT [PK_IdPreVIP] PRIMARY KEY CLUSTERED ([IdPreVIP] ASC),
    CONSTRAINT [FK_Predata_ ValoresIndicadoresPrograma_Predata_ ValoresIndicadoresPrograma] FOREIGN KEY ([IdDataValoresIndicadores]) REFERENCES [dbo].[Data_ValoresIndicador] ([IdDataValoresIndicadores]) ON DELETE CASCADE
);





