-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_PublicacionesProyectosByTipo]
	-- Add the parameters for the stored procedure here
	@IdPublicacion int,
	@IdTipoProyecto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @IdTipoProyecto=0
	SELECT * from Dashboard_PublicacionProyectos where IdPublicacion=@IdPublicacion
	ELSE
	SELECT * from Dashboard_PublicacionProyectos where IdPublicacion=@IdPublicacion and IdTipoProyecto=@IdTipoProyecto
	
END