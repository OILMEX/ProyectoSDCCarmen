CREATE TABLE [dbo].[LogAuditoria] (
    [idRef]      INT            NULL,
    [id]         INT            IDENTITY (1, 1) NOT NULL,
    [Movimiento] NVARCHAR (20)  NULL,
    [Elemento]   NVARCHAR (80)  NULL,
    [Log]        NVARCHAR (MAX) NULL,
    [fecha]      VARCHAR (100)  NULL,
    [usuario]    NVARCHAR (120) NULL,
    [personal]   NVARCHAR (80)  NULL,
    [rol]        NVARCHAR (80)  NULL,
    CONSTRAINT [PK__LogAudit__3213E83F06CD04F7] PRIMARY KEY CLUSTERED ([id] ASC)
);

