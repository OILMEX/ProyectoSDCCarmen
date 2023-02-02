CREATE TABLE [dbo].[tbl_HistorialCambioFechas] (
    [idCambioFecha]  INT            IDENTITY (1, 1) NOT NULL,
    [idProyecto]     INT            NOT NULL,
    [idSubetapa]     INT            NOT NULL,
    [idEtapa]        INT            NOT NULL,
    [FechaInicioAnt] DATE           NULL,
    [FechaFinAnt]    DATE           NULL,
    [FechaInicio]    DATE           NOT NULL,
    [FechaFin]       DATE           NOT NULL,
    [Motivo]         NVARCHAR (300) NOT NULL,
    [IdResponsable]  INT            NOT NULL,
    [FechaCreacion]  DATETIME       CONSTRAINT [DF_tbl_HistorialCambioFechas_FechaCreacion] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tbl_HistorialCambioFechas] PRIMARY KEY CLUSTERED ([idCambioFecha] ASC)
);

