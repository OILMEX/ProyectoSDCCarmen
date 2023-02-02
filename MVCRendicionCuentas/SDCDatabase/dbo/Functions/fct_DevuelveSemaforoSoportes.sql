-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveSemaforoSoportes]
(
	-- Add the parameters for the function here
	@IdDataValoresIndicadores int,
	@ReqSoporte bit
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Semaforo nvarchar(50)
	DECLARE @Contador int

	SET @Semaforo='Rojo'; 
	
	IF @ReqSoporte=1
	begin
	SELECT @Contador=COUNT(*) from tbl_SoportesAsignadosaIndicador where IdDataValoresIndicadores=@IdDataValoresIndicadores;
	IF @Contador>0
		SET @Semaforo='Verde';
	ELSE
		SET @Semaforo='Rojo';
	
	end
	-- Add the T-SQL statements to compute the return value here
	
	
	

	-- Return the result of the function
	RETURN @Semaforo

END