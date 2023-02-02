-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectPromedioRT12MPI]
	@IdResponsable int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	DECLARE @Resultado int
	
	SET NOCOUNT ON;
	
	DECLARE @IdUbicacion int
	DECLARE @Rol varchar(100)
				
	SELECT @IdUbicacion=IdUbicacion, @Rol=Rol from tbl_Responsables where idResponsable=@IdResponsable
				
	IF @IdResponsable=0
		SET @Rol='Administrador'
		
		DECLARE @IndicadoresLlenos INT=NULL;
				DECLARE @IndicadoresFaltantes INT = NULL
				DECLARE @TotalPendientes int = NULL
				
    IF @Rol='Administrador' 
		BEGIN
    
			select @Resultado=AVG(v.ValorSaneado) from (SELECT cir.IdConfigIndicadorResponsable, i.Prefijo, i.DescripcionCorta, '' as EstatusLlenado , i.TipoIndicador,
			i.TipoFrecuencia, i.TipoCalculo, i.Meta, cir.IdResponsable,
			ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia)), i.Meta),0) as ValorSaneado,
			i.CheckReqSoporte,
			i.CheckReqComentario,
			i.CheckSoporte,
			i.CheckComentarios
			from Config_IndicadorResponsable cir, tbl_Indicadores i  
			where 
			cir.Estatus=1 and
			cir.IdIndicador=i.IdIndicador
			and cir.IdConfigRelacionIndicadorProyecto is null
			 ) v
			 
			 
			 
							
		END
	 ELSE
	 BEGIN
			select @Resultado=AVG(v.ValorSaneado) from (SELECT cir.IdConfigIndicadorResponsable, i.Prefijo, i.DescripcionCorta, '' as EstatusLlenado , i.TipoIndicador,
			i.TipoFrecuencia, i.TipoCalculo, i.Meta, cir.IdResponsable,
			ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia)), i.Meta),0) as ValorSaneado,
			i.CheckReqSoporte,
			i.CheckReqComentario,
			i.CheckSoporte,
			i.CheckComentarios
			from Config_IndicadorResponsable cir, tbl_Indicadores i, tbl_Responsables r 
			where 
			cir.Estatus=1 
			and cir.IdIndicador=i.IdIndicador
			and cir.IdResponsable=r.idResponsable
			and r.IdUbicacion=@IdUbicacion
			and cir.IdConfigRelacionIndicadorProyecto is null
			 ) v
			 
			
			 
			 
	 END
	 
	 IF @Resultado is null
		Set @Resultado= -1
	 
	 RETURN @Resultado
	 
END