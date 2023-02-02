-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_SuperintendenciasParaProyecto]
	-- Add the parameters for the stored procedure here
	@IdCoordinacion int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT IdSuperintendencia, NombreSuperIntendencia  from tbl_Superintendencias where Tipo='Proyecto' 
	and IdCoordinacion=@IdCoordinacion
END