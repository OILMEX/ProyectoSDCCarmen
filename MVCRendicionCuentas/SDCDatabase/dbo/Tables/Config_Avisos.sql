CREATE TABLE [dbo].[Config_Avisos] (
    [IdConfigAvisos]        INT IDENTITY (1, 1) NOT NULL,
    [DiadelMesPrimerAviso]  INT NULL,
    [DiadelMesSegundoAviso] INT NULL,
    [DiadelMesTercerAviso]  INT NULL,
    CONSTRAINT [PK_Config_Avisos] PRIMARY KEY CLUSTERED ([IdConfigAvisos] ASC)
);



