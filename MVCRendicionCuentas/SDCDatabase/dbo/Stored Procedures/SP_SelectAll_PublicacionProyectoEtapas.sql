-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_PublicacionProyectoEtapas]
	-- Add the parameters for the stored procedure here
	@IdPublicacionProyecto int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *, dbo.fct_DevuelveClaveEtapa(IdEtapa) as Clave from dbo.Dashboard_PublicacionProyectoEtapa where IdPublicacionProyecto=@IdPublicacionProyecto
END