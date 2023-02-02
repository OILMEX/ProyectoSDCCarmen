-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveFechaFinSubEtapaProyecto] 
(
	-- Add the parameters for the function here
	@IdSubEtapa int,
	@IdProyecto int
	
)
RETURNS date
AS
BEGIN
	-- Declare the return variable here
	DECLARE @FechaFin date

	-- Add the T-SQL statements to compute the return value here
	SELECT @FechaFin=FechaFin from Config_FechasSubEtapasProyecto where IdProyecto=@IdProyecto and IdSubEtapa=@IdSubEtapa;

	-- Return the result of the function
	RETURN @FechaFin

END