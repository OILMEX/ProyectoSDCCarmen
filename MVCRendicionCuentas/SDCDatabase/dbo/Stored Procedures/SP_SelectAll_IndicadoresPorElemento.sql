-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_IndicadoresPorElemento]
	-- Add the parameters for the stored procedure here
	@IdElemento int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select i.* from tbl_Indicadores i where i.IdElemento=@IdElemento and Estatus=1 and CheckCreacionDesdeProyecto=0 ;
	
END