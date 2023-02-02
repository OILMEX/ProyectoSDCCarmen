-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
Create FUNCTION [dbo].[fct_DevuelveValor] 
(
	-- Add the parameters for the function here
	@IdDataValoresIndicadores int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Valor int

	-- Add the T-SQL statements to compute the return value here
	select @Valor=Valor from Data_ValoresIndicador where IdDataValoresIndicadores=@IdDataValoresIndicadores;

	-- Return the result of the function
	RETURN @Valor

END