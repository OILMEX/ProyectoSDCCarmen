-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_ComentariosdeIndicadores]
	-- Add the parameters for the stored procedure here
	@IdIndicador int,
	@IdSubEtapa int,
	@IdProyecto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select so.*, r.NombreResponsable from Config_SubEtapaIndicador csi, Config_RelacionIndicadorProyecto crip, Config_IndicadorResponsable cir,
Data_ValoresIndicador da, tbl_ComentariosAsignadosIndicador so, tbl_Responsables r
where 
da.IdConfigIndicadorResponsable=cir.IdConfigIndicadorResponsable and
cir.IdResponsable=r.idResponsable and
cir.IdConfigRelacionIndicadorProyecto=crip.IdConfigRelacionIndicadorProyecto and
crip.IdConfigSubEtapaIndicador = csi.IdConfigSubEtapaIndicador and
so.IdDataValoresIndicadores=da.IdDataValoresIndicadores and
csi.IdIndicador=@IdIndicador and
csi.IdSubEtapa=@IdSubEtapa and
crip.IdProyecto =@IdProyecto 



END