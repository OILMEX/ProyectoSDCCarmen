-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveEstatusMejoraElementos]
(
	-- Add the parameters for the function here
	@ValorActualElemento int, @IdPublicacionPasada int, @IdElemento int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @EstatusMejora int
	DECLARE @ValorPasadoElemento int
	
	
	SELECT @ValorPasadoElemento=Valor from Dashboard_PublicacionElemento where IdPublicacion=@IdPublicacionPasada and IdElemento=@IdElemento;
	
	
	IF @ValorActualElemento=@ValorPasadoElemento
		SET @EstatusMejora=3
	ELSE IF @ValorActualElemento<@ValorPasadoElemento
		SET @EstatusMejora=2
	ELSE IF @ValorActualElemento>@ValorPasadoElemento
		SET @EstatusMejora=1
	

	-- Return the result of the function
	RETURN @EstatusMejora

END