CREATE TABLE [dbo].[tbl_SoportesAsignadosaIndicador] (
    [IdSoportesAsignadosaIndicador] INT            IDENTITY (1, 1) NOT NULL,
    [RutaDoc]                       NVARCHAR (MAX) NULL,
    [NombreDoc]                     NVARCHAR (300) NULL,
    [IdDataValoresIndicadores]      INT            NULL,
    [FechaCreacion]                 DATE           NULL,
    CONSTRAINT [PK_tbl_SoportesAsignadosaIndicador] PRIMARY KEY CLUSTERED ([IdSoportesAsignadosaIndicador] ASC),
    CONSTRAINT [FK_tbl_SoportesAsignadosaIndicador_Data_ValoresIndicador] FOREIGN KEY ([IdDataValoresIndicadores]) REFERENCES [dbo].[Data_ValoresIndicador] ([IdDataValoresIndicadores]) ON DELETE CASCADE
);







