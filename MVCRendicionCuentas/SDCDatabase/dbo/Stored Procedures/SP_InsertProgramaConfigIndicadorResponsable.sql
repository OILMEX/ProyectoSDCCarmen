-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertProgramaConfigIndicadorResponsable]
	-- Add the parameters for the stored procedure here
	@IdConfigIndicadorResponsable int,
	@IdIndicador int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	declare @Count int
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	select @Count=COUNT(*) from Rel_ProgramaAsociadoIndicador where IdIndicador=@IdIndicador and IdConfigIndicadorResponsable=@IdConfigIndicadorResponsable;

if @Count = 0
begin 
    -- Insert statements for procedure here
	insert into Rel_ProgramaAsociadoIndicador ([IdIndicador]
      ,[EneroProg]
      ,[FebreroProg]
      ,[MarzoProg]
      ,[AbrilProg]
      ,[MayoProg]
      ,[JunioProg]
      ,[JulioProg]
      ,[AgostoProg]
      ,[SeptiembreProg]
      ,[OctubreProg]
      ,[NoviembreProg]
      ,[DiciembreProg]
      ,[ProyeccionAnualProg]
      ,[AnioSiguienteProg]
      ,[AnioAnteriorReal]
      ,[EneroReal]
      ,[FebreroReal]
      ,[MarzoReal]
      ,[AbrilReal]
      ,[MayoReal]
      ,[JunioReal]
      ,[JulioReal]
      ,[AgostoReal]
      ,[SeptiembreReal]
      ,[OctubreReal]
      ,[NoviembreReal]
      ,[DiciembreReal]
      ,[ProyeccionAnualReal]
      ,[AnioSiguienteReal]
      ,[FecCreacion]
      ,[IdConfigIndicadorResponsable]
      ,[Tipo]
      ,[AnioAnteriorProg]
      ,[FechaCreacion]
      ,[UsuarioCreacion]
      ,[FechaActualizacion]
      ,[UsuarioActualizacion])
      (
      select top 1
      [IdIndicador]
      ,[EneroProg]
      ,[FebreroProg]
      ,[MarzoProg]
      ,[AbrilProg]
      ,[MayoProg]
      ,[JunioProg]
      ,[JulioProg]
      ,[AgostoProg]
      ,[SeptiembreProg]
      ,[OctubreProg]
      ,[NoviembreProg]
      ,[DiciembreProg]
      ,[ProyeccionAnualProg]
      ,[AnioSiguienteProg]
      ,[AnioAnteriorReal]
      ,[EneroReal]
      ,[FebreroReal]
      ,[MarzoReal]
      ,[AbrilReal]
      ,[MayoReal]
      ,[JunioReal]
      ,[JulioReal]
      ,[AgostoReal]
      ,[SeptiembreReal]
      ,[OctubreReal]
      ,[NoviembreReal]
      ,[DiciembreReal]
      ,[ProyeccionAnualReal]
      ,[AnioSiguienteReal]
      ,[FecCreacion]
      ,@IdConfigIndicadorResponsable
      ,[Tipo]
      ,[AnioAnteriorProg]
      ,[FechaCreacion]
      ,[UsuarioCreacion]
      ,[FechaActualizacion]
      ,[UsuarioActualizacion]
      from Rel_ProgramaAsociadoIndicador where IdIndicador=@IdIndicador and IdConfigIndicadorResponsable is null
      )
      end
END