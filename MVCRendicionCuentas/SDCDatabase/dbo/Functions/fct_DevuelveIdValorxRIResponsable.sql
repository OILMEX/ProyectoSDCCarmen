-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveIdValorxRIResponsable] 
(
	-- Add the parameters for the function here
	@IdConfigIndicadorResponsable int,
	@IdRevision int,
	@Frecuencia int
	
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @FechaRevisionActual date
	DECLARE @IdDataValoresIndicadores nvarchar(50)
	DECLARE @FechaAnterior date
	
	
	
	--select @FechaRevisionActual=FechaRevision from tbl_Revisiones where idRevisiones=@IdRevision;
	--SET @FechaAnterior= DATEADD(DD, -@Frecuencia, @FechaRevisionActual);
	
	select top 1 @IdDataValoresIndicadores=IdDataValoresIndicadores 
	from Data_ValoresIndicador 
	where IdConfigIndicadorResponsable=@IdConfigIndicadorResponsable 
	order by IdDataValoresIndicadores desc;
	-- and FecCreacion>@FechaAnterior and FecCreacion <= @FechaRevisionActual;

	-- Return the result of the function
	RETURN @IdDataValoresIndicadores

END