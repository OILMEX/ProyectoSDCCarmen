-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertConfiguracionProyecto]
	-- Add the parameters for the stored procedure here
	@IdProyecto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
    --Configuración de Fechas
    insert into Config_FechasEtapasProyecto (IdProyecto, IdEtapa) (select @IdProyecto, e.IdEtapa  from tbl_Etapas e where Estatus=1 );
    insert into Config_FechasSubEtapasProyecto (IdProyecto, IdSubEtapa) (select @IdProyecto, IdSubEtapa from tbl_SubEtapas se where se.Estatus=1);
    
    --Insertando configuracion de indicadores subetapas al proyecto
    insert into Config_RelacionIndicadorProyecto (IdProyecto, IdConfigSubEtapaIndicador, Estatus, FrecuenciaActualizacionDesdeProyecto) 
    (select @IdProyecto, csi.IdConfigSubEtapaIndicador, csi.Estatus, i.TipoFrecuencia from Config_SubEtapaIndicador csi, tbl_Indicadores i 
    where csi.IdIndicador=i.IdIndicador and i.Estatus=1 and csi.Estatus=1);
    
    
	
END