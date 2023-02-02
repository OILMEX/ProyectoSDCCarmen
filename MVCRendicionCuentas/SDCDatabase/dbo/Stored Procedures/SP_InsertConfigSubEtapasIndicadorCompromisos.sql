-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertConfigSubEtapasIndicadorCompromisos] 
	-- Add the parameters for the stored procedure here
	@IdIndicador int,
	@IdSubEtapa int,
	@Estatus bit
	
AS
BEGIN
	
	Declare @IdConfiguSubEtapaIndicador int
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
    select @IdConfiguSubEtapaIndicador=IdConfigSubEtapaIndicador from Config_SubEtapaIndicador 
    where IdIndicador=@IdIndicador and IdSubEtapa=@IdSubEtapa and Estatus = 1;
	
	if @IdConfiguSubEtapaIndicador is not null
	begin
		Update Config_SubEtapaIndicador set Estatus=@Estatus 
		where IdConfigSubEtapaIndicador=@IdConfiguSubEtapaIndicador;
	end
	else
	begin
		Insert into Config_SubEtapaIndicador (IdIndicador,IdSubEtapa, Estatus)
		VALUES (@IdIndicador, @IdSubEtapa, 1);
	end
	
	
END