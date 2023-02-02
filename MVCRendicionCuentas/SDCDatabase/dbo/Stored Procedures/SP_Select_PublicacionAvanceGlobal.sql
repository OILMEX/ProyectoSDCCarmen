-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_PublicacionAvanceGlobal]
	-- Add the parameters for the stored procedure here
	@IdPublicacion int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select *, dbo.fct_DevuelveSemaforoIndicador(Valor,'Seguimiento',100) as Semaforo from Dashboard_PublicacionAvanceGlobal where IdPublicacion=@IdPublicacion;
END