-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAllCount_IndicadoresLlenosFaltantes12MPI] 
	-- Add the parameters for the stored procedure here
	@Tipo int,
	@IdResponsable int
AS
BEGIN
	
	Declare @Resultado int
	Declare @TablaPivote TABLE(Valor int)
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @IdUbicacion int
	DECLARE @Rol varchar(100)
				
	SELECT @IdUbicacion=IdUbicacion, @Rol=Rol from tbl_Responsables where idResponsable=@IdResponsable
				
	IF @IdResponsable=0
		SET @Rol='Administrador'
				
	IF @Rol='Administrador' 
	BEGIN
		insert into @TablaPivote (Valor) 
		(SELECT 
		dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia))
		from Config_IndicadorResponsable cir, tbl_Indicadores i 
		where 
		cir.IdIndicador=i.IdIndicador
		and cir.Estatus=1
		and cir.IdConfigRelacionIndicadorProyecto is null)
	END
	ELSE
	BEGIN
		insert into @TablaPivote (Valor) 
		(SELECT 
		dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia))
		from Config_IndicadorResponsable cir, tbl_Indicadores i, tbl_Responsables r 
		where 
		cir.IdIndicador=i.IdIndicador
		and cir.IdResponsable=r.idResponsable
		and r.IdUbicacion=@IdUbicacion
		and cir.Estatus=1
		and cir.IdConfigRelacionIndicadorProyecto is null)
	END
	
	if @Tipo=1 -- Leno
	select @Resultado=count(*) from @TablaPivote where Valor is not null;
	else
	select @Resultado=count(*) from @TablaPivote where Valor is null;
	
	RETURN  @Resultado
	
	
	
	
END