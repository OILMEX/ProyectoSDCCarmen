CREATE TABLE [dbo].[Config_FechasEtapasProyecto] (
    [IdConfigFechasEtapasProyecto] INT           IDENTITY (1, 1) NOT NULL,
    [IdProyecto]                   INT           NULL,
    [IdEtapa]                      INT           NULL,
    [FechaFin]                     DATE          NULL,
    [EstatusTiempo]                NVARCHAR (50) NULL,
    [FechaInicio]                  DATE          NULL,
    [Estatus]                      BIT           NULL,
    CONSTRAINT [PK_Config_FechasEtapasProyecto] PRIMARY KEY CLUSTERED ([IdConfigFechasEtapasProyecto] ASC),
    CONSTRAINT [FK_Config_FechasEtapasProyecto_tbl_Etapas] FOREIGN KEY ([IdEtapa]) REFERENCES [dbo].[tbl_Etapas] ([IdEtapa]),
    CONSTRAINT [FK_Config_FechasEtapasProyecto_tbl_Proyectos] FOREIGN KEY ([IdProyecto]) REFERENCES [dbo].[tbl_Proyectos] ([IdProyecto]) ON DELETE CASCADE
);







