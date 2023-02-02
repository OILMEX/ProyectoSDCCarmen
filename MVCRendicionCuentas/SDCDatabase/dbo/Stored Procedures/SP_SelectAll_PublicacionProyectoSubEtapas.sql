-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_PublicacionProyectoSubEtapas]
	-- Add the parameters for the stored procedure here
	@IdPublicacionProyectoEtapa int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *, dbo.fct_DevuelveClaveSubEtapa(IdSubEtapa) as Clave from Dashboard_PublicacionProyectoSubetapa where IdPublicacionProyectoEtapa=@IdPublicacionProyectoEtapa;
END