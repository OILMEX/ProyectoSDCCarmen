-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_VistaCargaIndicadoresProyectosByProyecto]
	-- Add the parameters for the stored procedure here
	@IdProyecto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @FirstTable TABLE (IdConfigIndicadorResponsable INT,Prefijo nvarchar(300), 
	DescripcionCorta nvarchar(MAX), EstatusLlenado  nvarchar(100), TipoIndicador nvarchar(100), TipoFrecuencia int, 
	TipoCalculo nvarchar(50),	Meta int, IdProyecto int,IdEtapa int, IdSubEtapa int, IdDataValoresIndicadores int, Valor int,
	CheckReqSoporte bit, CheckReqComentario bit, CheckSoporte bit, CheckComentarios bit, IdElemento int, IdIndicador int, 
	IdSubsistema int,
	ClaveEtapa int, NombreEtapa nvarchar(500),ClaveSubEtapa float, NombreSubEtapa nvarchar(500),NombreSubsistema nvarchar(50),
	DescripcionLargaSubsistema nvarchar(500), NombreElemento nvarchar(300), DescripcionElemento nvarchar(500),
	FechaFinEtapa date, FechaFinSubEtapa date, IdResponsable int, EstatusSubEtapa bit, FechaCompromisoParaEmpezarAMostrarse date 
	)
	
	
	insert into @FirstTable 
	(IdConfigIndicadorResponsable,
	Prefijo,
	DescripcionCorta,
	EstatusLlenado,
	TipoIndicador,
	TipoFrecuencia,
	TipoCalculo,
	Meta,
	IdProyecto,
	IdEtapa,
	IdSubEtapa,
	IdDataValoresIndicadores,
	Valor,
	CheckReqSoporte,
	CheckReqComentario,
	CheckSoporte,
	CheckComentarios,
	IdElemento,
	IdIndicador,
	IdSubsistema,
	ClaveEtapa,
	NombreEtapa,
	ClaveSubEtapa,
	NombreSubEtapa,
	NombreSubsistema,
	DescripcionLargaSubsistema,
	NombreElemento,
	DescripcionElemento,
	FechaFinEtapa,
	FechaFinSubEtapa,
	IdResponsable,
	EstatusSubEtapa,
	FechaCompromisoParaEmpezarAMostrarse
	) 
	(SELECT 
	cir.IdConfigIndicadorResponsable, 
	i.Prefijo, 
	i.DescripcionCorta, 
	'' as EstatusLlenado, 
	i.TipoIndicador, 
	i.TipoFrecuencia,
	i.TipoCalculo, 
	i.Meta, 
	crp.IdProyecto,
	et.IdEtapa,
    csi.IdSubEtapa,
    dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia),
	dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia)),
	i.CheckReqSoporte,
	i.CheckReqComentario,
	i.CheckSoporte,
	i.CheckComentarios,
	i.IdElemento,
	i.IdIndicador,
	e.IdSubsistema,
	et.Clave as ClaveEtapa,
	et.NombreEtapa,
	se.Clave as ClaveSubEtapa,
	se.NombreSubEtapa,
	sub.NombreSubsistema,
	sub.DescripcionLargaSubsistema,
	e.NombreElemento,
	e.DescripcionElemento,
	dbo.fct_DevuelveFechaFinEtapaProyecto(et.IdEtapa, crp.IdProyecto) as FechaFinEtapa,
	dbo.fct_DevuelveFechaFinSubEtapaProyecto(csi.IdSubEtapa, crp.IdProyecto) as FechaFinSubEtapa,
	cir.IdResponsable,
	dbo.fct_DevuelveEstatusSubEtapaProyecto(csi.IdSubEtapa,crp.IdProyecto) as EstatusSubEtapa,
	crp.FechaCompromisoParaEmpezarAMostrarse
	from 
	Config_IndicadorResponsable cir, 
	tbl_Indicadores i, 
	Config_RelacionIndicadorProyecto crp, 
	Config_SubEtapaIndicador csi,
	Config_FechasSubEtapasProyecto cfsp,
	tbl_Elementos e,
	tbl_Subsistemas sub,
	tbl_SubEtapas se,
	tbl_Etapas et,
	tbl_Proyectos p
	where 
	cir.IdIndicador=i.IdIndicador 
	and cir.IdConfigRelacionIndicadorProyecto=crp.IdConfigRelacionIndicadorProyecto
	and crp.IdProyecto=cfsp.IdProyecto
	and crp.IdProyecto=p.IdProyecto
	and p.Estatus=1
	and crp.IdConfigSubEtapaIndicador=csi.IdConfigSubEtapaIndicador
	and csi.IdSubEtapa = cfsp.IdSubEtapa
	and cfsp.EstatusTiempo='En Proceso'
	and i.IdElemento=e.IdElemento
	and e.IdSubsistema=sub.IdSubsistema
	and se.IdSubEtapa=csi.IdSubEtapa
	and se.IdEtapa=et.IdEtapa
	and cir.IdConfigRelacionIndicadorProyecto is not null
	and cir.Estatus=1
	and p.IdProyecto=@IdProyecto
	);
	
	
	
	select distinct IdConfigIndicadorResponsable,
	dbo.fct_DevuelveSemaforoIndicador(Valor, TipoIndicador, Meta) as Semaforo, 
	Prefijo,
	DescripcionCorta,
	EstatusLlenado,
	IdIndicador,
	IdElemento,
	IdSubsistema,
	TipoIndicador,
	TipoFrecuencia,
	TipoCalculo,
	Meta,
	IdProyecto,
	IdEtapa,
	IdSubEtapa,
	IdDataValoresIndicadores,
	Valor,
	CheckSoporte,
	CheckComentarios,
	dbo.fct_DevuelveSemaforoSoportes(IdDataValoresIndicadores, CheckReqSoporte) as SemaforoSoportes,
	dbo.fct_DevuelveSemaforoNotas(IdDataValoresIndicadores, CheckReqSoporte) as SemaforoComentarios,
	ClaveEtapa,
	NombreEtapa,
	ClaveSubEtapa,
	NombreSubEtapa,
	NombreSubsistema,
	DescripcionLargaSubsistema,
	NombreElemento,
	DescripcionElemento,
	FechaFinEtapa,
	FechaFinSubEtapa,
	IdResponsable,
	EstatusSubEtapa
	from @FirstTable
	where EstatusSubEtapa=1 and FechaCompromisoParaEmpezarAMostrarse IS NULL OR FechaCompromisoParaEmpezarAMostrarse <= GETDATE()
		
	
END