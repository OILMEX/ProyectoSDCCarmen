-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_FechasSubEtapasxProyecto]
	-- Add the parameters for the stored procedure here
	@IdProyecto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select cfsp.*, se.Clave as ClaveSubEtapa, se.NombreSubEtapa, se.IdEtapa, e.Clave as ClaveEtapa,
	e.NombreEtapa, 
	dbo.fct_DevuelveFechaInicioEtapaProyecto(se.IdEtapa, cfsp.IdProyecto) as FechaInicioEtapa,
	dbo.fct_DevuelveFechaFinEtapaProyecto(se.IdEtapa, cfsp.IdProyecto) as FechaFinEtapa,
	dbo.fct_DevuelveEstatusEtapaProyecto(se.IdEtapa,cfsp.IdProyecto) as EstatusEtapa
from
Config_FechasSubEtapasProyecto cfsp,
tbl_SubEtapas se,
tbl_Etapas e
where
cfsp.IdSubEtapa=se.IdSubEtapa and
se.IdEtapa=e.IdEtapa and
cfsp.IdProyecto=@IdProyecto;


END