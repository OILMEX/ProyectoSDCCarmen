-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveClaveEtapa] 
(
	-- Add the parameters for the function here
	@IdEtapa int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ClaveEtapa int

	-- Add the T-SQL statements to compute the return value here
	SELECT @ClaveEtapa=Clave from tbl_Etapas where IdEtapa=@IdEtapa

	-- Return the result of the function
	RETURN @ClaveEtapa

END