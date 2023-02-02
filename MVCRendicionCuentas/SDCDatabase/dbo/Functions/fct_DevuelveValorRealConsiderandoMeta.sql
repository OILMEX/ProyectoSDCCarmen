-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveValorRealConsiderandoMeta]
(
	-- Add the parameters for the function here
	@Valor int,
	@Meta int
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ValorSaneado int
	
	SET @ValorSaneado=@Valor;
	
	IF @Valor is not NULL
	BEGIN
	
		IF @Meta is not NULL
		BEGIN
			IF @Meta=0
				IF @Valor > 0
					SET @ValorSaneado=0
				ELSE
					SET @ValorSaneado = 100	
		END
		
	
	END
	ELSE
	BEGIN
		set @ValorSaneado=0
	END
	
	RETURN @ValorSaneado

END