-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveSemaforoSoportesdeIndicadoresAgrupados]
(
	-- Add the parameters for the function here
	@IdIndicador int,
	@IdSubEtapa int,
	@IdProyecto int
	
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Resultado int
	DECLARE @Semaforo nvarchar(50)

	select @Resultado=COUNT(*) from Config_SubEtapaIndicador csi, Config_RelacionIndicadorProyecto crip, Config_IndicadorResponsable cir,
Data_ValoresIndicador da, tbl_SoportesAsignadosaIndicador so
where 
da.IdConfigIndicadorResponsable=cir.IdConfigIndicadorResponsable and
cir.IdConfigRelacionIndicadorProyecto=crip.IdConfigRelacionIndicadorProyecto and
crip.IdConfigSubEtapaIndicador = csi.IdConfigSubEtapaIndicador and
so.IdDataValoresIndicadores=da.IdDataValoresIndicadores and
csi.IdIndicador=@IdIndicador and
csi.IdSubEtapa=@IdSubEtapa and
crip.IdProyecto =@IdProyecto 

	IF @Resultado>0
		SET @Semaforo='Verde';
	ELSE
		SET @Semaforo='Rojo';
	
	

	-- Return the result of the function
	RETURN @Semaforo

END