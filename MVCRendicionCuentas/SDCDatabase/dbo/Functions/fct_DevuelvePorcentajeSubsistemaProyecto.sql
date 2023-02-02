-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelvePorcentajeSubsistemaProyecto]
(
	-- Add the parameters for the function here
	@IdProyecto int,
	@IdSubsistema int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Porcentaje int

	-- Add the T-SQL statements to compute the return value here
	select @Porcentaje=Porcentaje from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=@IdSubsistema;

	-- Return the result of the function
	RETURN @Porcentaje

END