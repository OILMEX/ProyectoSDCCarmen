-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectOne_PublicacionProyectoSeleccionado]
	-- Add the parameters for the stored procedure here
	@IdPublicacionProyecto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Dashboard_PublicacionProyectos where IdPublicacionProyecto=@IdPublicacionProyecto;
END