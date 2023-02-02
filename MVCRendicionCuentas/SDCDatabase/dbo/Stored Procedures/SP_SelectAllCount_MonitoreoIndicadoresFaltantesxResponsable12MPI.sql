-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAllCount_MonitoreoIndicadoresFaltantesxResponsable12MPI] 
	-- Add the parameters for the stored procedure here
	@IdResponsable int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select COUNT(*) as Resultado
	from (SELECT distinct cir.IdConfigIndicadorResponsable, cir.IdResponsable, r.NombreResponsable, e.IdSubsistema, i.Prefijo, i.DescripcionCorta, '' as EstatusLlenado , i.TipoIndicador,
	i.TipoFrecuencia, i.TipoCalculo, i.Meta,
	dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,dbo.fct_DevuelveIdProximaRevision(cir.IdConfigIndicadorResponsable),i.TipoFrecuencia) as IdDataValoresIndicadores,
	dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,dbo.fct_DevuelveIdProximaRevision(cir.IdConfigIndicadorResponsable),i.TipoFrecuencia)) as Valor,
	i.CheckReqSoporte,
	i.CheckReqComentario,
	i.CheckSoporte,
	i.CheckComentarios
	from Config_IndicadorResponsable cir, tbl_Indicadores i , tbl_Responsables r, tbl_Elementos e
	where 
	cir.IdIndicador=i.IdIndicador and
	cir.IdResponsable=r.idResponsable and
	i.IdElemento=e.IdElemento and
	cir.Estatus=1 and
	 cir.IdConfigRelacionIndicadorProyecto is null
	) v
	where Valor is null and IdResponsable=@IdResponsable
END