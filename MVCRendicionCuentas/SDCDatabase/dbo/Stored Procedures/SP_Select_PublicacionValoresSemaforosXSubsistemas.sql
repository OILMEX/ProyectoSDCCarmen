-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_PublicacionValoresSemaforosXSubsistemas]
	-- Add the parameters for the stored procedure here
	@IdPublicacion int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from Dashboard_PublicacionAvanceSubsistema where IdPublicacion=@IdPublicacion
END