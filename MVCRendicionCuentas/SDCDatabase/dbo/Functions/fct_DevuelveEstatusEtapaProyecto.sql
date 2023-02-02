-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveEstatusEtapaProyecto] 
(
	-- Add the parameters for the function here
	@IdEtapa int,
	@IdProyecto int
)
RETURNS bit
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Estatus bit

	-- Add the T-SQL statements to compute the return value here
	SELECT @Estatus=Estatus from Config_FechasEtapasProyecto where IdEtapa=@IdEtapa and IdProyecto=@IdProyecto
	-- Return the result of the function
	RETURN @Estatus

END