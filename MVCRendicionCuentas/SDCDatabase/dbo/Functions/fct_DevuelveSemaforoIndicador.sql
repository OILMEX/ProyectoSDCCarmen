-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fct_DevuelveSemaforoIndicador]
(
	-- Add the parameters for the function here
	@Valor int,@TipoIndicador nvarchar(50),@Meta int
)
RETURNS nvarchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Semaforo nvarchar(50)

	IF @TipoIndicador='Seguimiento'
	BEGIN
		IF @Meta=100
		BEGIN
			IF @Valor IS NOT NULL
			BEGIN
				IF(@Valor<81)
					SET @Semaforo='Rojo'
				ELSE IF (@Valor >80 and @Valor <96)
					SET @Semaforo='Amarillo'
				ELSE IF (@Valor >95)
					SET @Semaforo='Verde'		
			
			END
			ELSE
				SET @Semaforo='Rojo'
		END
		ELSE
		BEGIN
			IF @Valor IS NOT NULL
			BEGIN
				IF (@Valor>0)
					SET @Semaforo='Rojo'
				ELSE
					SET @Semaforo='Verde'	
			END
			ELSE
			BEGIN
					SET @Semaforo='Rojo'
			END		
		END	
	
	END
	ELSE --de tipo checkbox
	BEGIN
			IF @Valor IS NOT NULL
			BEGIN
				IF (@Valor>0)
					SET @Semaforo='Verde'
				ELSE
					SET @Semaforo='Rojo'	
			END
			ELSE
			BEGIN
					SET @Semaforo='Rojo'
			END		
	END

	-- Return the result of the function
	RETURN @Semaforo

END