-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertValoresIndicadores] 
	-- Add the parameters for the stored procedure here
	@IdDataValoresIndicadores int,
	@Valor float,
	@IdConfigIndicadorResponsable int
AS
BEGIN
	DECLARE @IdDataValoresIndicadoresReturn int
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @IdDataValoresIndicadores is null
	begin
		insert into Data_ValoresIndicador (Valor, IdConfigIndicadorResponsable, FecCreacion) 
		VALUES (@Valor, @IdConfigIndicadorResponsable, GETDATE())
		
		set @IdDataValoresIndicadoresReturn=SCOPE_IDENTITY();
	end
	else
	begin
		update Data_ValoresIndicador set Valor=@Valor, FecCreacion=GETDATE()
		where IdDataValoresIndicadores=@IdDataValoresIndicadores;
		set @IdDataValoresIndicadoresReturn=@IdDataValoresIndicadores;
	end
	
	select @IdDataValoresIndicadoresReturn as IdDataValoresIndicadores;
	
END