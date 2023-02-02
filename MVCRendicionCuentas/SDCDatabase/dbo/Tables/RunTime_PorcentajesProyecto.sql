CREATE TABLE [dbo].[RunTime_PorcentajesProyecto] (
    [IdPorcentajesProyecto] INT           IDENTITY (1, 1) NOT NULL,
    [IdProyecto]            INT           NULL,
    [IdSubsistema]          INT           NULL,
    [Porcentaje]            INT           NULL,
    [Semaforo]              NVARCHAR (50) NULL,
    CONSTRAINT [PK_RunTime_PorcentajesProyecto] PRIMARY KEY CLUSTERED ([IdPorcentajesProyecto] ASC),
    CONSTRAINT [FK_RunTime_PorcentajesProyecto_tbl_Proyectos] FOREIGN KEY ([IdProyecto]) REFERENCES [dbo].[tbl_Proyectos] ([IdProyecto]) ON DELETE CASCADE,
    CONSTRAINT [FK_RunTime_PorcentajesProyecto_tbl_Subsistemas] FOREIGN KEY ([IdSubsistema]) REFERENCES [dbo].[tbl_Subsistemas] ([IdSubsistema])
);





