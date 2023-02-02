-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertRevisionesIndicadoresSubEtapas]
	-- Add the parameters for the stored procedure here
	@IdConfigIndicadorResponsable int,
	@IdSubEtapa int,
	@IdProyecto int
	
AS
BEGIN

Declare @Frecuencia int
Declare @IdIndicador int
Declare @FechaProyectoFin date
Declare @FechaPivote date
Declare @FechaFinSubetapa date
Declare @FechaInicial date


	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

	
select 	@IdIndicador=IdIndicador from Config_IndicadorResponsable where IdConfigIndicadorResponsable=@IdConfigIndicadorResponsable;
select @Frecuencia=crip.FrecuenciaActualizacionDesdeProyecto from Config_IndicadorResponsable cir, Config_RelacionIndicadorProyecto crip
  where cir.IdConfigRelacionIndicadorProyecto=crip.IdConfigRelacionIndicadorProyecto and
  cir.IdConfigIndicadorResponsable=@IdConfigIndicadorResponsable;
select @FechaFinSubetapa=FechaFin, @FechaInicial=FechaInicio from Config_FechasSubEtapasProyecto where IdProyecto=@IdProyecto and IdSubEtapa=@IdSubEtapa;

SET @FechaPivote=@FechaInicial;

delete from tbl_Revisiones where IdConfigIndicadorResponsable=@IdConfigIndicadorResponsable;

if @FechaFinSubetapa IS NOT NULL 
BEGIN
	
	WHILE @FechaPivote <= @FechaFinSubetapa
	BEGIN
	   	 
	   if @FechaPivote!=@FechaInicial
	   begin	   
	   insert into tbl_Revisiones (FechaRevision, IdConfigIndicadorResponsable) VALUES (@FechaPivote, @IdConfigIndicadorResponsable);
	   end
	   
	   SET @FechaPivote = DATEADD(DAY,@Frecuencia,@FechaPivote);
	   
	END;
	
	insert into tbl_Revisiones (FechaRevision, IdConfigIndicadorResponsable) VALUES (@FechaFinSubetapa, @IdConfigIndicadorResponsable);

END

    -- Insert statements for procedure here
	
	
	
END