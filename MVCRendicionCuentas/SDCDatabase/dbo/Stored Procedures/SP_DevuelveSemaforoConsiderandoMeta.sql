-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_DevuelveSemaforoConsiderandoMeta]
	-- Add the parameters for the stored procedure here
	@Valor int,
	@IdConfigResponsable int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @IdIndicador int
	declare @Meta int
	declare @TipoIndicador nvarchar(100)
	
	select @IdIndicador=IdIndicador from Config_IndicadorResponsable where IdConfigIndicadorResponsable=@IdConfigResponsable;
	select @Meta=Meta, @TipoIndicador=TipoIndicador from tbl_Indicadores where IdIndicador=@IdIndicador;
	
	declare @Semaforo nvarchar(50)
	set @Semaforo=dbo.fct_DevuelveSemaforoIndicador(@Valor,@TipoIndicador,@Meta);
	
	select @Semaforo
END