-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_PublicacionElementoxSubsistema]
	-- Add the parameters for the stored procedure here
	@IdPublicacion int,
	@IdSubsistema int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select d.*,  e.DescripcionElemento 
	from Dashboard_PublicacionElemento d, tbl_Elementos e 
	where 
	d.IdElemento=e.IdElemento and
	d.IdPublicacion=@IdPublicacion and d.IdSubsistema=@IdSubsistema
END