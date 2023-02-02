CREATE TABLE [dbo].[tbl_Proyectos] (
    [IdProyecto]                INT            IDENTITY (1, 1) NOT NULL,
    [NombreProyecto]            NVARCHAR (500) NULL,
    [IdResponsable]             INT            NULL,
    [IdArea]                    INT            NULL,
    [FechaInicio]               DATE           NULL,
    [FechaFin]                  DATE           NULL,
    [Estatus]                   BIT            NULL,
    [FechaCreacion]             DATETIME       NULL,
    [UsuarioCreacion]           INT            NULL,
    [FechaActualizacion]        DATETIME       NULL,
    [UsuarioActualizacion]      INT            NULL,
    [EstatusTiempo]             NVARCHAR (50)  NULL,
    [Semaforo]                  NVARCHAR (50)  NULL,
    [IdUbicacion]               INT            NULL,
    [IdElementoOrganigrama]     INT            NULL,
    [IdTipoElementoOrganigrama] INT            NULL,
    [IdTipoProyecto]            INT            NULL,
    CONSTRAINT [PK_tbl_Proyectos] PRIMARY KEY CLUSTERED ([IdProyecto] ASC),
    CONSTRAINT [FK_tbl_Proyectos_tbl_Areas] FOREIGN KEY ([IdArea]) REFERENCES [dbo].[tbl_Areas] ([idArea]),
    CONSTRAINT [FK_tbl_Proyectos_tbl_Responsables] FOREIGN KEY ([UsuarioCreacion]) REFERENCES [dbo].[tbl_Responsables] ([idResponsable]),
    CONSTRAINT [FK_tbl_Proyectos_tbl_Responsables1] FOREIGN KEY ([UsuarioActualizacion]) REFERENCES [dbo].[tbl_Responsables] ([idResponsable]),
    CONSTRAINT [FK_tbl_Proyectos_tbl_Responsables2] FOREIGN KEY ([IdResponsable]) REFERENCES [dbo].[tbl_Responsables] ([idResponsable]),
    CONSTRAINT [FK_tbl_Proyectos_tbl_Ubicaciones] FOREIGN KEY ([IdUbicacion]) REFERENCES [dbo].[tbl_Ubicaciones] ([idUbicacion])
);








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'En Proceso o Concluido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_Proyectos', @level2type = N'COLUMN', @level2name = N'EstatusTiempo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1: Subdireccion, 2 Gerencia, 3 Coordinacion, 4 Superintendencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tbl_Proyectos', @level2type = N'COLUMN', @level2name = N'IdTipoElementoOrganigrama';

