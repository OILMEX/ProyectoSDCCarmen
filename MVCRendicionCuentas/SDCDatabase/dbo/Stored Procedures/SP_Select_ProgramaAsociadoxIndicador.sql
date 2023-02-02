-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_ProgramaAsociadoxIndicador] 
	-- Add the parameters for the stored procedure here
	@IdIndicador int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select p.IdRelProgramaAsociado
      ,p.IdIndicador
      ,p.EneroProg
      ,p.FebreroProg
      ,p.MarzoProg
      ,p.AbrilProg
      ,p.MayoProg
      ,p.JunioProg
      ,p.JulioProg
      ,p.AgostoProg
      ,p.SeptiembreProg
      ,p.OctubreProg
      ,p.NoviembreProg
      ,p.DiciembreProg
      ,p.ProyeccionAnualProg
      ,p.AnioSiguienteProg
      ,p.AnioAnteriorReal
      ,p.EneroReal
      ,p.FebreroReal
      ,p.MarzoReal
      ,p.AbrilReal
      ,p.MayoReal
      ,p.JunioReal
      ,p.JulioReal
      ,p.AgostoReal
      ,p.SeptiembreReal
      ,p.OctubreReal
      ,p.NoviembreReal
      ,p.DiciembreReal
      ,p.ProyeccionAnualReal
      ,p.AnioSiguienteReal
      ,p.FecCreacion
      ,p.IdConfigIndicadorResponsable
      ,p.Tipo
      ,p.AnioAnteriorProg
      ,p.FechaCreacion
      ,p.UsuarioCreacion
      ,p.FechaActualizacion
      ,p.UsuarioActualizacion 
      ,r.NombreResponsable as NombreUsuarioCreacion
      ,res.NombreResponsable as NombreUsuarioActualizacion
      from Rel_ProgramaAsociadoIndicador p, tbl_Responsables r, tbl_Responsables res
      where 
      p.UsuarioCreacion=r.idResponsable and
      p.UsuarioActualizacion=res.idResponsable and
      IdIndicador=@IdIndicador and IdConfigIndicadorResponsable is null;
END