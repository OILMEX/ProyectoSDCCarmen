CREATE TABLE [dbo].[tbl_ComentariosAsignadosIndicador] (
    [IdComentariosAsignadosaIndicador] INT            IDENTITY (1, 1) NOT NULL,
    [Comentario]                       NVARCHAR (500) NULL,
    [IdDataValoresIndicadores]         INT            NULL,
    [FechaCreacion]                    DATE           NULL,
    CONSTRAINT [PK_tbl_ComentariosAsignadosIndicador] PRIMARY KEY CLUSTERED ([IdComentariosAsignadosaIndicador] ASC),
    CONSTRAINT [FK_tbl_ComentariosAsignadosIndicador_Data_ValoresIndicador] FOREIGN KEY ([IdDataValoresIndicadores]) REFERENCES [dbo].[Data_ValoresIndicador] ([IdDataValoresIndicadores]) ON DELETE CASCADE
);





