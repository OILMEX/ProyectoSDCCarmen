-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveEstatusMejora]
(
	-- Add the parameters for the function here
	@ValorMesAnterior int, @ValorMesActual int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @EstatusMejora int

	-- Add the T-SQL statements to compute the return value here
	IF @ValorMesActual=@ValorMesAnterior
		SET @EstatusMejora=3
	ELSE IF @ValorMesActual<@ValorMesAnterior
		SET @EstatusMejora=2
	ELSE IF @ValorMesActual>@ValorMesAnterior
		SET @EstatusMejora=1

	-- Return the result of the function
	RETURN @EstatusMejora

END