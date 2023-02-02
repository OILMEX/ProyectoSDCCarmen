-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertConfigIndicador12MPICompromisos] 
	-- Add the parameters for the stored procedure here
	@IdIndicador int,
	@CheckAplica bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update tbl_Indicadores set CheckAplica=@CheckAplica where IdIndicador=@IdIndicador;
	
END