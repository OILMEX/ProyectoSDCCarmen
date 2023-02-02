-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveClaveSubEtapa] 
(
	-- Add the parameters for the function here
	@IdSubEtapa int
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ClaveSubEtapa float

	-- Add the T-SQL statements to compute the return value here
	SELECT @ClaveSubEtapa=Clave from tbl_SubEtapas where IdSubEtapa=@IdSubEtapa

	-- Return the result of the function
	RETURN @ClaveSubEtapa

END