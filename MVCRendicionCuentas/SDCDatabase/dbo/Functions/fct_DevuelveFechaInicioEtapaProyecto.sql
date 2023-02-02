-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveFechaInicioEtapaProyecto]
(
	@IdEtapa int,
	@IdProyecto int
)
RETURNS date
AS
BEGIN
	-- Declare the return variable here
	DECLARE @FechaInicio date

	-- Add the T-SQL statements to compute the return value here
	SELECT @FechaInicio=FechaInicio from Config_FechasEtapasProyecto where IdProyecto=@IdProyecto and IdEtapa=@IdEtapa;

	-- Return the result of the function
	RETURN @FechaInicio

END