-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAllProyectosByEstatusAndRol] 
	-- Add the parameters for the stored procedure here
	@Estatus nvarchar(50),
	@IdResponsable int
AS
BEGIN
	DECLARE @IdUbicacion int
	DECLARE @Rol nvarchar(50)
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT @IdUbicacion=IdUbicacion, @Rol=Rol from tbl_Responsables where idResponsable=@IdResponsable
	
	IF @Rol='Administrador'
	begin
    -- Insert statements for procedure here
	SELECT p.Semaforo, p.IdProyecto, p.NombreProyecto, u.Ubicacion as NombreArea, p.FechaInicio, p.FechaFin, p.IdResponsable,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 1) as Semaforo12MPI,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,1) as Porcentaje12MPI,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 2) as SemaforoSASP,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,2) as PorcentajeSASP,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 3) as SemaforoSAA,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,3) as PorcentajeSAA,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 4) as SemaforoSAST,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,4) as PorcentajeSAST,
	u.Ubicacion	
	from tbl_Proyectos p, tbl_Ubicaciones u 
	where 
	p.IdUbicacion=u.idUbicacion 
	and p.Estatus=1 
	and p.EstatusTiempo=@Estatus;
	
	end
	ELSE IF @Rol='SubAdministrador'
	BEGIN
	-- Insert statements for procedure here
	SELECT p.Semaforo, p.IdProyecto, p.NombreProyecto, u.Ubicacion as NombreArea, p.FechaInicio, p.FechaFin, p.IdResponsable,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 1) as Semaforo12MPI,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,1) as Porcentaje12MPI,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 2) as SemaforoSASP,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,2) as PorcentajeSASP,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 3) as SemaforoSAA,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,3) as PorcentajeSAA,
	dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 4) as SemaforoSAST,
	dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,4) as PorcentajeSAST,
	u.Ubicacion	
	from tbl_Proyectos p, tbl_Ubicaciones u 
	where 
	p.IdUbicacion=u.idUbicacion 
	and p.Estatus=1 
	and p.EstatusTiempo=@Estatus
	and p.IdUbicacion=@IdUbicacion;
	END
	ELSE --Cuando si por de algun milagro se logean con un usuario tipo carga
	BEGIN
		SELECT p.Semaforo, p.IdProyecto, p.NombreProyecto, u.Ubicacion as NombreArea, p.FechaInicio, p.FechaFin, p.IdResponsable,
		dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 1) as Semaforo12MPI,
		dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,1) as Porcentaje12MPI,
		dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 2) as SemaforoSASP,
		dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,2) as PorcentajeSASP,
		dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 3) as SemaforoSAA,
		dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,3) as PorcentajeSAA,
		dbo.fct_DevuelveSemaforoSubsistemaProyecto(p.IdProyecto, 4) as SemaforoSAST,
		dbo.fct_DevuelvePorcentajeSubsistemaProyecto(p.IdProyecto,4) as PorcentajeSAST,
		u.Ubicacion	
		from tbl_Proyectos p, tbl_Ubicaciones u 
		where 
		p.IdUbicacion=u.idUbicacion 
		and p.Estatus=1 
		and p.EstatusTiempo=@Estatus
		and p.IdUbicacion=10000;
	
	END
	
	
	
END