-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAllResponsablesIndicadorXProyecto]
	@IdProyecto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select cir.IdResponsable, csi.IdIndicador, csi.IdSubEtapa, se.IdEtapa, r.Ficha, r.NombreResponsable, r.Correo  
from Config_IndicadorResponsable cir, Config_RelacionIndicadorProyecto crp, Config_SubEtapaIndicador csi, tbl_Responsables r, tbl_SubEtapas se
where cir.IdResponsable=r.idResponsable and cir.IdConfigRelacionIndicadorProyecto=crp.IdConfigRelacionIndicadorProyecto
and crp.IdConfigSubEtapaIndicador=csi.IdConfigSubEtapaIndicador and csi.IdSubEtapa=se.IdSubEtapa
and crp.IdProyecto=@IdProyecto
and crp.Estatus=1
and cir.Estatus=1;

END