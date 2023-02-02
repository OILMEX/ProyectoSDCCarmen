-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertConfigResponsables12MPI]
	-- Add the parameters for the stored procedure here
	@IdIndicador int,
	@IdResponsable int,
	@Estatus bit
AS
BEGIN
	
	Declare @IdConfigIndicadorResponsable int
	declare @TipoIndicador nvarchar(50)
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	select @IdConfigIndicadorResponsable=IdConfigIndicadorResponsable from Config_IndicadorResponsable 
	where IdResponsable=@IdResponsable and IdIndicador=@IdIndicador and
	IdConfigRelacionIndicadorProyecto is NULL;
	select @TipoIndicador=TipoIndicador from tbl_Indicadores where IdIndicador=@IdIndicador;
	
	IF @IdConfigIndicadorResponsable is NULL
	begin
    -- Insert statements for procedure here
	insert into Config_IndicadorResponsable
	(IdResponsable,IdIndicador, Estatus)
	VALUES (@IdResponsable, @IdIndicador, @Estatus);
	
	SET @IdConfigIndicadorResponsable= SCOPE_IDENTITY();
	
	IF @TipoIndicador= 'Programa'
	begin
		exec dbo.SP_InsertProgramaConfigIndicadorResponsable @IdConfigIndicadorResponsable, @IdIndicador;
	end
	
	exec dbo.SP_InsertRevisiones12MPI @IdConfigIndicadorResponsable;
	
	end
	ELSE
	begin
		update Config_IndicadorResponsable set Estatus=@Estatus where IdConfigIndicadorResponsable=@IdConfigIndicadorResponsable;
	end
	
	
	
	
END