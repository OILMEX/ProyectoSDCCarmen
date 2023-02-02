-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveIdProximaRevision]
(
	-- Add the parameters for the function here
	@IdConfigIndicadorResponsable int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Id int

	-- Add the T-SQL statements to compute the return value here
	select top 1 @Id=idRevisiones from tbl_Revisiones 
	where IdConfigIndicadorResponsable=@IdConfigIndicadorResponsable and FechaRevision > GETDATE()

	-- Return the result of the function
	RETURN @Id

END