CREATE TABLE [dbo].[Dashboard_PublicacionProyectoSubetapa] (
    [IdPublicacionSemaforoSubsistemaProyectoSubetapa] INT            IDENTITY (1, 1) NOT NULL,
    [IdPublicacionProyectoEtapa]                      INT            NULL,
    [NombreSubEtapa]                                  NVARCHAR (500) NULL,
    [IdSubEtapa]                                      INT            NULL,
    [IdPublicacion]                                   INT            NULL,
    [IdEtapa]                                         INT            NULL,
    [IdPublicacionProyecto]                           INT            NULL,
    [Semaforo12MPI]                                   NVARCHAR (50)  NULL,
    [Valor12MPI]                                      INT            NULL,
    [SemaforoSASP]                                    NVARCHAR (50)  NULL,
    [ValorSASP]                                       INT            NULL,
    [SemaforoSAA]                                     NVARCHAR (50)  NULL,
    [ValorSAA]                                        INT            NULL,
    [SemaforoSAST]                                    NVARCHAR (50)  NULL,
    [ValorSAST]                                       INT            NULL,
    [EstatusTiempo]                                   NVARCHAR (50)  NULL,
    [FechaFinSubEtapa]                                DATE           NULL,
    [IndicadoresCargados]                             INT            NULL,
    [IndicadoresNoCargados]                           INT            NULL,
    CONSTRAINT [PK_Dashboard_PublicacionSemaforoSubsistemaProyectoSubetapa] PRIMARY KEY CLUSTERED ([IdPublicacionSemaforoSubsistemaProyectoSubetapa] ASC),
    CONSTRAINT [FK_Dashboard_PublicacionSemaforoSubsistemaProyectoSubetapa_Dashboard_Publicaciones] FOREIGN KEY ([IdPublicacion]) REFERENCES [dbo].[Dashboard_Publicaciones] ([IdPublicacion]) ON DELETE CASCADE,
    CONSTRAINT [FK_Dashboard_PublicacionSemaforoSubsistemaProyectoSubetapa_Dashboard_PublicacionProyectoEtapa] FOREIGN KEY ([IdPublicacionProyectoEtapa]) REFERENCES [dbo].[Dashboard_PublicacionProyectoEtapa] ([IdPublicacionProyectoEtapa]),
    CONSTRAINT [FK_Dashboard_PublicacionSemaforoSubsistemaProyectoSubetapa_Dashboard_PublicacionProyectos] FOREIGN KEY ([IdPublicacionProyecto]) REFERENCES [dbo].[Dashboard_PublicacionProyectos] ([IdPublicacionProyecto]),
    CONSTRAINT [FK_Dashboard_PublicacionSemaforoSubsistemaProyectoSubetapa_tbl_Etapas] FOREIGN KEY ([IdEtapa]) REFERENCES [dbo].[tbl_Etapas] ([IdEtapa]),
    CONSTRAINT [FK_Dashboard_PublicacionSemaforoSubsistemaProyectoSubetapa_tbl_SubEtapas] FOREIGN KEY ([IdSubEtapa]) REFERENCES [dbo].[tbl_SubEtapas] ([IdSubEtapa])
);







