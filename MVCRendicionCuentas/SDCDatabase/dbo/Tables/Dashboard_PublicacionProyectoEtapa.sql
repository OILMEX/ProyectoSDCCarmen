CREATE TABLE [dbo].[Dashboard_PublicacionProyectoEtapa] (
    [IdPublicacionProyectoEtapa] INT            IDENTITY (1, 1) NOT NULL,
    [IdPublicacionProyecto]      INT            NULL,
    [IdEtapa]                    INT            NULL,
    [NombreEtapa]                NVARCHAR (500) NULL,
    [FechaFin]                   DATE           NULL,
    [IdPublicacion]              INT            NULL,
    [Semaforo12MPI]              NVARCHAR (50)  NULL,
    [Valor12MPI]                 INT            NULL,
    [SemaforoSASP]               NVARCHAR (50)  NULL,
    [ValorSASP]                  INT            NULL,
    [SemaforoSAA]                NVARCHAR (50)  NULL,
    [ValorSAA]                   INT            NULL,
    [SemaforoSAST]               NVARCHAR (50)  NULL,
    [ValorSAST]                  INT            NULL,
    [EstatusTiempo]              NVARCHAR (50)  NULL,
    [IndicadoresCargados]        INT            NULL,
    [IndicadoresNoCargados]      INT            NULL,
    CONSTRAINT [PK_Dashboard_PublicacionProyectoEtapa] PRIMARY KEY CLUSTERED ([IdPublicacionProyectoEtapa] ASC),
    CONSTRAINT [FK_Dashboard_PublicacionProyectoEtapa_Dashboard_Publicaciones] FOREIGN KEY ([IdPublicacion]) REFERENCES [dbo].[Dashboard_Publicaciones] ([IdPublicacion]) ON DELETE CASCADE,
    CONSTRAINT [FK_Dashboard_PublicacionProyectoEtapa_Dashboard_PublicacionProyectos] FOREIGN KEY ([IdPublicacionProyecto]) REFERENCES [dbo].[Dashboard_PublicacionProyectos] ([IdPublicacionProyecto]),
    CONSTRAINT [FK_Dashboard_PublicacionProyectoEtapa_tbl_Etapas] FOREIGN KEY ([IdEtapa]) REFERENCES [dbo].[tbl_Etapas] ([IdEtapa])
);







