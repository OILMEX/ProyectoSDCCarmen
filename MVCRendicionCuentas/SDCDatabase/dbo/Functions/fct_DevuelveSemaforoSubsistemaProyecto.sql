-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveSemaforoSubsistemaProyecto]
(
	-- Add the parameters for the function here
	@IdProyecto int,
	@IdSubsistema int
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Semaforo nvarchar(50)

	-- Add the T-SQL statements to compute the return value here
	select @Semaforo=Semaforo from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=@IdSubsistema;

	-- Return the result of the function
	RETURN @Semaforo

END