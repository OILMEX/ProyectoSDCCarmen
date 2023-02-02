-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAllProyectosByEstatus] 
	-- Add the parameters for the stored procedure here
	@Estatus nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT p.Semaforo, p.IdProyecto, p.NombreProyecto, u.Ubicacion	as NombreArea, p.FechaInicio, p.FechaFin, p.IdResponsable,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 1) as Semaforo12MPI,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,1) as Porcentaje12MPI,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 2) as SemaforoSASP,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,2) as PorcentajeSASP,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 3) as SemaforoSAA,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,3) as PorcentajeSAA,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 4) as SemaforoSAST,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,4) as PorcentajeSAST,
	u.Ubicacion, p.IdUbicacion
	from tbl_Proyectos p, tbl_Ubicaciones u where  p.IdUbicacion=u.idUbicacion and p.Estatus=1 and p.EstatusTiempo=@Estatus;
END