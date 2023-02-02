-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_IndicadoresPorElementoFaltantesEnSubEtapaProyecto]
	-- Add the parameters for the stored procedure here
	@IdElemento int,
	@IdSubEtapa int,
	@IdProyecto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from tbl_Indicadores where Estatus=1  and IdElemento=@IdElemento and IdIndicador not in(select i.IdIndicador
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
csi.IdSubEtapa=@IdSubEtapa and
e.IdElemento=@IdElemento and
crip.IdProyecto=@IdProyecto);

END