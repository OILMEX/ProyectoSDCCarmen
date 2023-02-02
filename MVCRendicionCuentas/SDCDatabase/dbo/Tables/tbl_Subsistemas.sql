CREATE TABLE [dbo].[tbl_Subsistemas] (
    [IdSubsistema]               INT            IDENTITY (1, 1) NOT NULL,
    [NombreSubsistema]           NVARCHAR (50)  NULL,
    [DescripcionLargaSubsistema] NVARCHAR (500) NULL,
    [CheckAplicaProyecto]        BIT            NULL,
    [Estatus]                    BIT            NULL,
    [Semaforo]                   NVARCHAR (50)  NULL,
    [Porcentaje]                 INT            NULL,
    CONSTRAINT [PK_tbl_Subsistemas] PRIMARY KEY CLUSTERED ([IdSubsistema] ASC)
);



