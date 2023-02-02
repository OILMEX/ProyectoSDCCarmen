-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveFechaFinEtapaProyecto] 
(
	-- Add the parameters for the function here
	@IdEtapa int,
	@IdProyecto int
	
)
RETURNS date
AS
BEGIN
	-- Declare the return variable here
	DECLARE @FechaFin date

	-- Add the T-SQL statements to compute the return value here
	SELECT @FechaFin=FechaFin from Config_FechasEtapasProyecto where IdProyecto=@IdProyecto and IdEtapa=@IdEtapa;

	-- Return the result of the function
	RETURN @FechaFin

END