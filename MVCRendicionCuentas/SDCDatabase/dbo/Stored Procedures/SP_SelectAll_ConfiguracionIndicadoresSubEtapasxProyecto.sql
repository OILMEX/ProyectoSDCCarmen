-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_ConfiguracionIndicadoresSubEtapasxProyecto] 
	-- Add the parameters for the stored procedure here
	@IdProyecto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
select crip.IdConfigRelacionIndicadorProyecto, i.Prefijo, i.CheckCreacionDesdeProyecto, i.DescripcionCorta, 
i.DescripcionLarga, i.TipoIndicador,i.TipoCalculo, i.Meta,crip.FrecuenciaActualizacionDesdeProyecto as TipoFrecuencia, crip.Estatus,
csi.IdSubEtapa, e.IdElemento, e.NombreElemento, e.DescripcionElemento, s.IdSubsistema, s.NombreSubsistema,
se.IdEtapa, i.IdIndicador, dbo.fct_DevuelveEstatusSubEtapaProyecto(csi.IdSubEtapa, crip.IdProyecto) as EstatusSubEtapa
, dbo.fct_DevuelveEstatusEtapaProyecto(se.IdEtapa,crip.IdProyecto) as EstatusEtapa,
crip.FechaCompromisoParaEmpezarAMostrarse, crip.FrecuenciaActualizacionDesdeProyecto
from 
Config_RelacionIndicadorProyecto crip,
Config_SubEtapaIndicador csi,
tbl_Indicadores i,
tbl_Elementos e,
tbl_Subsistemas s,
tbl_SubEtapas se
where
crip.IdConfigSubEtapaIndicador=csi.IdConfigSubEtapaIndicador and
csi.IdIndicador=i.IdIndicador and
i.IdElemento=e.IdElemento and
e.IdSubsistema=s.IdSubsistema and
csi.IdSubEtapa=se.IdSubEtapa and
crip.Estatus=1 and
crip.IdProyecto=@IdProyecto;



END