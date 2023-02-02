CREATE TABLE [dbo].[Config_RelacionIndicadorProyecto] (
    [IdConfigRelacionIndicadorProyecto]    INT  IDENTITY (1, 1) NOT NULL,
    [IdProyecto]                           INT  NULL,
    [IdConfigSubEtapaIndicador]            INT  NULL,
    [Estatus]                              BIT  NULL,
    [FechaCompromisoParaEmpezarAMostrarse] DATE NULL,
    [FrecuenciaActualizacionDesdeProyecto] INT  NULL,
    CONSTRAINT [PK_ConfigRelacionIndicadorProyecto] PRIMARY KEY CLUSTERED ([IdConfigRelacionIndicadorProyecto] ASC),
    CONSTRAINT [FK_Config_RelacionIndicadorProyecto_Config_SubEtapaIndicador] FOREIGN KEY ([IdConfigSubEtapaIndicador]) REFERENCES [dbo].[Config_SubEtapaIndicador] ([IdConfigSubEtapaIndicador]),
    CONSTRAINT [FK_Config_RelacionIndicadorProyecto_tbl_Proyectos] FOREIGN KEY ([IdProyecto]) REFERENCES [dbo].[tbl_Proyectos] ([IdProyecto])
);



