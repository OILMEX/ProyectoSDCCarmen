-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_VistaIndicadoresActivadosPeroNoAsignadosEnProyectos]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select i.Prefijo, 
	i.DescripcionCorta, 
	i.TipoIndicador, 
	i.TipoFrecuencia,
	i.TipoCalculo, 
	i.Meta,
	etapa.IdEtapa,
    i.CheckReqSoporte,
	i.CheckReqComentario,
	i.CheckSoporte,
	i.CheckComentarios,
	i.IdElemento,
	i.IdIndicador,
	elem.IdSubsistema,
	etapa.Clave as ClaveEtapa,
	etapa.NombreEtapa,
	cfsp.IdSubEtapa,
	sub.Clave as ClaveSubEtapa,
	sub.NombreSubEtapa,
	dbo.fct_DevuelveFechaFinEtapaProyecto(etapa.IdEtapa, crp.IdProyecto) as FechaFinEtapa,
	cfsp.FechaFin as FechaFinSubEtapa,
	cfsp.EstatusTiempo as EstatusTiempoSubEtapa,
	subsistemas.NombreSubsistema,
	subsistemas.DescripcionLargaSubsistema,
	elem.NombreElemento,
	elem.DescripcionElemento,
	crp.FechaCompromisoParaEmpezarAMostrarse,
	p.*,
	resp.NombreResponsable
	

 from tbl_Indicadores i, Config_RelacionIndicadorProyecto crp, Config_SubEtapaIndicador csi, 
tbl_Proyectos p
, Config_FechasSubEtapasProyecto cfsp, 
tbl_Etapas etapa, 
tbl_SubEtapas sub, 
tbl_Subsistemas subsistemas, 
tbl_Elementos elem,
tbl_Responsables resp
where
crp.IdProyecto=p.IdProyecto
and crp.IdConfigSubEtapaIndicador=csi.IdConfigSubEtapaIndicador
and csi.IdIndicador=i.IdIndicador
and p.IdProyecto = cfsp.IdProyecto
and cfsp.IdSubEtapa= csi.IdSubEtapa
and cfsp.IdSubEtapa = sub.IdSubEtapa
and sub.IdEtapa=etapa.IdEtapa
and i.IdElemento=elem.IdElemento
and elem.IdSubsistema=subsistemas.IdSubsistema
and resp.idResponsable = p.IdResponsable 
and p.Estatus=1
and crp.Estatus=1
and cfsp.EstatusTiempo='En Proceso'
and cfsp.Estatus=1
and crp.IdConfigRelacionIndicadorProyecto not in 
(select cir.IdConfigRelacionIndicadorProyecto from Config_IndicadorResponsable cir where cir.Estatus=1 and cir.IdConfigRelacionIndicadorProyecto is not null)

END