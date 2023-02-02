-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateEstatusEtapaProyectos] 
	-- Add the parameters for the stored procedure here
	@IdEtapa int,
	@IdProyecto int,
	@Estatus bit
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Config_FechasEtapasProyecto set Estatus=@Estatus where IdEtapa=@IdEtapa and IdProyecto=@IdProyecto;
	
	IF @Estatus=0 -- False
	begin
		UPDATE Config_FechasSubEtapasProyecto set Estatus=0 where IdProyecto=@IdProyecto and IdSubEtapa in (select IdSubEtapa from tbl_SubEtapas where IdEtapa=@IdEtapa)
	end
	 
END