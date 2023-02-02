CREATE TABLE [dbo].[Dashboard_PublicacionProyectos] (
    [IdPublicacionProyecto] INT            IDENTITY (1, 1) NOT NULL,
    [Semaforo]              NVARCHAR (50)  NULL,
    [NombreProyecto]        NVARCHAR (500) NULL,
    [Responsable]           NVARCHAR (500) NULL,
    [IdResponsable]         INT            NULL,
    [FechaInicio]           DATE           NULL,
    [FechaFin]              DATE           NULL,
    [IdPublicacion]         INT            NULL,
    [IdProyecto]            INT            NULL,
    [Semaforo12MPI]         NVARCHAR (50)  NULL,
    [Valor12MPI]            INT            NULL,
    [SemaforoSASP]          NVARCHAR (50)  NULL,
    [ValorSASP]             INT            NULL,
    [SemaforoSAA]           NVARCHAR (50)  NULL,
    [ValorSAA]              INT            NULL,
    [SemaforoSAST]          NVARCHAR (50)  NULL,
    [ValorSAST]             INT            NULL,
    [IdTipoProyecto]        NVARCHAR (50)  NULL,
    [ElementoOrganigrama]   NVARCHAR (500) NULL,
    [IndicadoresCargados]   INT            NULL,
    [IndicadoresNoCargados] INT            NULL,
    CONSTRAINT [PK_Dashboard_PublicacionProyectos] PRIMARY KEY CLUSTERED ([IdPublicacionProyecto] ASC),
    CONSTRAINT [FK_Dashboard_PublicacionProyectos_Dashboard_Publicaciones] FOREIGN KEY ([IdPublicacion]) REFERENCES [dbo].[Dashboard_Publicaciones] ([IdPublicacion]) ON DELETE CASCADE,
    CONSTRAINT [FK_Dashboard_PublicacionProyectos_tbl_Proyectos] FOREIGN KEY ([IdProyecto]) REFERENCES [dbo].[tbl_Proyectos] ([IdProyecto]),
    CONSTRAINT [FK_Dashboard_PublicacionProyectos_tbl_Responsables] FOREIGN KEY ([IdResponsable]) REFERENCES [dbo].[tbl_Responsables] ([idResponsable])
);









