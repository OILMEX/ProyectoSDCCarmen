-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertRevisiones12MPI]
	-- Add the parameters for the stored procedure here
	@IdConfigIndicadorResponsable int
	
	
AS
BEGIN

Declare @Frecuencia int
Declare @IdIndicador int
Declare @FechaFin datetime
Declare @FechaPivote date
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
select @FechaPivote=CONVERT(DATETIME,CONVERT(varchar(5),YEAR(GETDATE()))+'-01-01', 111);
	
select 	@IdIndicador=IdIndicador from Config_IndicadorResponsable where IdConfigIndicadorResponsable=@IdConfigIndicadorResponsable;
select @Frecuencia=TipoFrecuencia from tbl_Indicadores where IdIndicador=@IdIndicador;
set @FechaFin= CONVERT(DATETIME, '2017-01-01', 111);

delete from tbl_Revisiones where IdConfigIndicadorResponsable=@IdConfigIndicadorResponsable;


IF @FechaFin IS NOT NULL
BEGIN
	
	WHILE @FechaPivote <= @FechaFin
	BEGIN
	   	   
	   insert into tbl_Revisiones (FechaRevision, IdConfigIndicadorResponsable) VALUES (@FechaPivote, @IdConfigIndicadorResponsable);
	   SET @FechaPivote = DATEADD(DAY,@Frecuencia,@FechaPivote);
	END;
	
	insert into tbl_Revisiones (FechaRevision, IdConfigIndicadorResponsable) VALUES (@FechaFin, @IdConfigIndicadorResponsable);

END

    -- Insert statements for procedure here
	
	
	
END