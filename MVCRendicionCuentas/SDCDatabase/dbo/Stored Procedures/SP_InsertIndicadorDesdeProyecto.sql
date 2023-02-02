-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertIndicadorDesdeProyecto]
    -- Add the parameters for the stored procedure here
    @IdProyecto int,
    @IdSubEtapa int,
    @IdIndicador int
AS
BEGIN
    
    Declare @IdConfigSubEtapaIndicador int;
    Declare @TipoIndicador nvarchar(50);
    Declare @TipoCalculo nvarchar(50);
    
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    
    select @TipoIndicador=TipoIndicador, @TipoCalculo=TipoCalculo from tbl_Indicadores where IdIndicador=@IdIndicador;

    -- Insert statements for procedure here
    Insert into Config_SubEtapaIndicador (IdIndicador,IdSubEtapa, Estatus) 
    VALUES (@IdIndicador, @IdSubEtapa, 1);
    
    SET @IdConfigSubEtapaIndicador = SCOPE_IDENTITY()
    
    Insert into Config_RelacionIndicadorProyecto (IdProyecto, IdConfigSubEtapaIndicador, Estatus)
    VALUES (@IdProyecto, @IdConfigSubEtapaIndicador, 1);
    
    IF @TipoIndicador='Seguimiento'
    begin
        IF @TipoCalculo = 'Programa'
        begin
            insert into Rel_ProgramaAsociadoIndicador (IdIndicador) VALUES (@IdIndicador);
        end
    end
    
END