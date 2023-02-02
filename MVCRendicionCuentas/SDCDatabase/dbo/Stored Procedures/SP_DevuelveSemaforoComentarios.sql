-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_DevuelveSemaforoComentarios]
	-- Add the parameters for the stored procedure here
	@IdDataValoresIndicadores int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @ReqComentario bit
	DECLARE @SemaforoComentario nvarchar(50)

    -- Insert statements for procedure here
	select @ReqComentario=i.CheckReqComentario from Data_ValoresIndicador dv, Config_IndicadorResponsable cir, tbl_Indicadores i
	where dv.IdConfigIndicadorResponsable=cir.IdConfigIndicadorResponsable
	and cir.IdIndicador=i.IdIndicador
	and IdDataValoresIndicadores=@IdDataValoresIndicadores
	
	SET @SemaforoComentario=[dbo].[fct_DevuelveSemaforoNotas](@IdDataValoresIndicadores,@ReqComentario);
	
	SELECT @SemaforoComentario
END