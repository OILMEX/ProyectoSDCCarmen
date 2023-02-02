CREATE TABLE [dbo].[Config_FechasSubEtapasProyecto] (
    [IdConfigFechasSubEtapasProyecto] INT           IDENTITY (1, 1) NOT NULL,
    [IdProyecto]                      INT           NULL,
    [IdSubEtapa]                      INT           NULL,
    [FechaFin]                        DATE          NULL,
    [EstatusTiempo]                   NVARCHAR (50) NULL,
    [FechaInicio]                     DATE          NULL,
    [Estatus]                         BIT           NULL,
    CONSTRAINT [PK_Config_FechasSubEtapasProyecto] PRIMARY KEY CLUSTERED ([IdConfigFechasSubEtapasProyecto] ASC),
    CONSTRAINT [FK_Config_FechasSubEtapasProyecto_tbl_Proyectos] FOREIGN KEY ([IdProyecto]) REFERENCES [dbo].[tbl_Proyectos] ([IdProyecto]) ON DELETE CASCADE,
    CONSTRAINT [FK_Config_FechasSubEtapasProyecto_tbl_SubEtapas] FOREIGN KEY ([IdSubEtapa]) REFERENCES [dbo].[tbl_SubEtapas] ([IdSubEtapa])
);









