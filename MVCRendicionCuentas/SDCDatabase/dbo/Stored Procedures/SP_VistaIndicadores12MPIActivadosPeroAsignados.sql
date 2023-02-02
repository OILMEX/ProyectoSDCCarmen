-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_VistaIndicadores12MPIActivadosPeroAsignados 
	-- Add the parameters for the stored procedure here
	@IdResponsable int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @Rol nvarchar(110)
	DECLARE @IdUbicacion int

	select @Rol=Rol, @IdUbicacion=IdUbicacion from tbl_Responsables where idResponsable=@IdResponsable;
    -- Insert statements for procedure here
	if @Rol='Administrador'
	begin
		SELECT i.Prefijo, i.DescripcionCorta, '' as EstatusLlenado , i.TipoIndicador,
		i.TipoFrecuencia, i.TipoCalculo, i.Meta,
		i.CheckReqSoporte,
		i.CheckReqComentario,
		i.CheckSoporte,
		i.CheckComentarios,
		i.IdElemento,
		i.IdIndicador,
		e.NombreElemento,
		e.DescripcionElemento
		
		from  
		tbl_Indicadores i, 
		tbl_Elementos e
		where 
		i.IdElemento=e.IdElemento and
		i.Estatus=1
		and i.CheckAplica=1
		and i.IdIndicador not in
		(select cir.IdIndicador from Config_IndicadorResponsable cir where cir.Estatus=1 and cir.IdConfigRelacionIndicadorProyecto is null)
	end	
	else 
	begin
		SELECT i.Prefijo, i.DescripcionCorta, '' as EstatusLlenado , i.TipoIndicador,
			i.TipoFrecuencia, i.TipoCalculo, i.Meta,
			i.CheckReqSoporte,
			i.CheckReqComentario,
			i.CheckSoporte,
			i.CheckComentarios,
			i.IdElemento,
			i.IdIndicador,
			e.NombreElemento,
			e.DescripcionElemento
			from  
			tbl_Indicadores i, 
			tbl_Elementos e
			where 
			i.IdElemento=e.IdElemento and
			i.Estatus=1
			and i.CheckAplica=1
			and i.IdIndicador not in
			(select cir.IdIndicador from Config_IndicadorResponsable cir, tbl_Responsables r
			 where cir.IdResponsable=r.idResponsable and cir.Estatus=1 and cir.IdConfigRelacionIndicadorProyecto is null and r.IdUbicacion=@IdUbicacion)
	
	end
	
	
END