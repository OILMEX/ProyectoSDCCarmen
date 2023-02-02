-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION fct_DevuelveIdElemento
(
	-- Add the parameters for the function here
	@Nombre nvarchar(100)
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @IdElemento int

	-- Add the T-SQL statements to compute the return value here
	SELECT @IdElemento=IdElemento from tbl_Elementos where NombreElemento=@Nombre;

	-- Return the result of the function
	RETURN @IdElemento

END