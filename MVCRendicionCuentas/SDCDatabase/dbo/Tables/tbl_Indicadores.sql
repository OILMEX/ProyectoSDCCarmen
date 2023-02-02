CREATE TABLE [dbo].[tbl_Indicadores] (
    [IdIndicador]                INT             IDENTITY (1, 1) NOT NULL,
    [IdElemento]                 INT             NULL,
    [Clave]                      NVARCHAR (50)   NULL,
    [Prefijo]                    NVARCHAR (200)  NULL,
    [DescripcionCorta]           NVARCHAR (300)  NULL,
    [DescripcionLarga]           NVARCHAR (500)  NULL,
    [CheckSoporte]               BIT             NULL,
    [CheckComentarios]           BIT             NULL,
    [CheckReqSoporte]            BIT             NULL,
    [CheckReqComentario]         BIT             NULL,
    [CheckCreacionDesdeProyecto] BIT             NULL,
    [FechaCreacion]              DATETIME        NULL,
    [UsuarioCreacion]            INT             NULL,
    [FechaActualizacion]         DATETIME        NULL,
    [UsuarioActualizacion]       INT             NULL,
    [Estatus]                    BIT             NULL,
    [TipoIndicador]              NVARCHAR (100)  NULL,
    [TipoFrecuencia]             INT             NULL,
    [Meta]                       INT             NULL,
    [TipoCalculo]                NVARCHAR (100)  NULL,
    [DescripcionValorFormula]    NVARCHAR (500)  NULL,
    [EtiquetaValorProgramado]    NVARCHAR (100)  NULL,
    [EtiquetaValorReal]          NVARCHAR (100)  NULL,
    [CheckAplica]                BIT             NULL,
    [IndicadorFijo]              BIT             NULL,
    [FechaCompromiso]            DATE            NULL,
    [Ayuda]                      NVARCHAR (1000) NULL,
    CONSTRAINT [PK_tbl_Indicadores] PRIMARY KEY CLUSTERED ([IdIndicador] ASC)
);











