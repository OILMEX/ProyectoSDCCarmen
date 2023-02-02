-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateRelacionIndicadorSubEtapaProyecto]
	-- Add the parameters for the stored procedure here
	@IdConfigRelacionIndicadorProyecto int,
	@Estatus bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Config_RelacionIndicadorProyecto set Estatus=@Estatus 
	where IdConfigRelacionIndicadorProyecto=@IdConfigRelacionIndicadorProyecto;


END