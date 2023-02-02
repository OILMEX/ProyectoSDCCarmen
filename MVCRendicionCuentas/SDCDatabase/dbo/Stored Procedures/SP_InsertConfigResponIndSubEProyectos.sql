-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertConfigResponIndSubEProyectos]
	-- Add the parameters for the stored procedure here
	@IdConfigRelacionIndicadorProyecto int,
	@IdIndicador int,
	@IdResponsable int,
	@Estatus bit
AS
BEGIN
	
	Declare @IdConfigIndicadorResponsable int
	Declare @IdConfigSubEtapaIndicador int
	declare @IdSubEtapa int
	declare @IdProyecto int
	declare @TipoIndicador nvarchar(50)
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	select @IdConfigSubEtapaIndicador=IdConfigSubEtapaIndicador, @IdProyecto=IdProyecto  from Config_RelacionIndicadorProyecto where IdConfigRelacionIndicadorProyecto=@IdConfigRelacionIndicadorProyecto;
	select @IdSubEtapa=IdSubEtapa from Config_SubEtapaIndicador where IdConfigSubEtapaIndicador=@IdConfigSubEtapaIndicador;
	select @TipoIndicador=TipoIndicador from tbl_Indicadores where IdIndicador=@IdIndicador;
	
	select @IdConfigIndicadorResponsable=IdConfigIndicadorResponsable from Config_IndicadorResponsable 
	where IdResponsable=@IdResponsable and IdIndicador=@IdIndicador and
	IdConfigRelacionIndicadorProyecto=@IdConfigRelacionIndicadorProyecto;
	
	IF @IdConfigIndicadorResponsable is NULL
	begin
    -- Insert statements for procedure here
	insert into Config_IndicadorResponsable
	(IdResponsable,IdIndicador, IdConfigRelacionIndicadorProyecto, Estatus)
	VALUES (@IdResponsable, @IdIndicador,@IdConfigRelacionIndicadorProyecto, @Estatus);
	
	
	SET @IdConfigIndicadorResponsable= SCOPE_IDENTITY();
	
	
	IF @TipoIndicador= 'Programa'
	begin
		exec dbo.SP_InsertProgramaConfigIndicadorResponsable @IdConfigIndicadorResponsable, @IdIndicador;
	end
	
	exec dbo.SP_InsertRevisionesIndicadoresSubEtapas @IdConfigIndicadorResponsable, @IdSubEtapa, @IdProyecto;
	
	end
	ELSE
	begin
		update Config_IndicadorResponsable set Estatus=@Estatus 
		where IdConfigIndicadorResponsable=@IdConfigIndicadorResponsable;
	end
	
	
	
	
END