-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_VistaCargaIndicadores12MPIxReponsableSubadministrador]
	-- Add the parameters for the stored procedure here
	@IdReponsable int,
	@IdUbicacionPar int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @FirstTable TABLE (IdConfigIndicadorResponsable INT,Prefijo nvarchar(300), 
	DescripcionCorta nvarchar(MAX), EstatusLlenado  nvarchar(100), TipoIndicador nvarchar(100), TipoFrecuencia int, 
	TipoCalculo nvarchar(50), Meta int,	IdDataValoresIndicadores int, Valor int,
	CheckReqSoporte bit, CheckReqComentario bit, CheckSoporte bit, CheckComentarios bit, IdElemento int, 
	IdIndicador int, NombreElemento nvarchar(300), DescripcionElemento nvarchar(500), IdResponsable int)
	

	-- select * from Config_IndicadorResponsable where IdResponsable=@IdReponsable and IdConfigRelacionIndicadorProyecto=NULL and Estatus=1
    -- select top 1 * from tbl_Revisiones where IdConfigIndicadorResponsable=@Parameter and FechaRevision > GETDATE()
    
    DECLARE @IdUbicacion int
    DECLARE @Rol nvarchar(50)
    
    select @IdUbicacion=IdUbicacion, @Rol=Rol from tbl_Responsables where idResponsable=@IdReponsable

    -- Insert statements for procedure here
    IF(@Rol='SubAdministrador')
    begin
	insert into @FirstTable (IdConfigIndicadorResponsable,Prefijo,DescripcionCorta,EstatusLlenado,TipoIndicador,
	TipoFrecuencia, TipoCalculo,Meta, IdDataValoresIndicadores, Valor, CheckReqSoporte, CheckReqComentario,
	CheckSoporte,
	CheckComentarios, IdElemento, IdIndicador,NombreElemento, DescripcionElemento
	,IdResponsable)
	(SELECT cir.IdConfigIndicadorResponsable, i.Prefijo, i.DescripcionCorta, '' as EstatusLlenado , i.TipoIndicador,
	i.TipoFrecuencia, i.TipoCalculo, i.Meta,
	dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia),
	dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia)),
	i.CheckReqSoporte,
	i.CheckReqComentario,
	i.CheckSoporte,
	i.CheckComentarios,
	i.IdElemento,
	i.IdIndicador,
	e.NombreElemento,
	e.DescripcionElemento,
	cir.IdResponsable
	from Config_IndicadorResponsable cir, 
	tbl_Indicadores i, 
	tbl_Elementos e,
	tbl_Responsables r
	where 
	cir.IdResponsable=r.idResponsable
	and cir.IdIndicador=i.IdIndicador
	and i.IdElemento=e.IdElemento
	and cir.IdConfigRelacionIndicadorProyecto is null
	and cir.Estatus=1
	and r.IdUbicacion=@IdUbicacion
	-- and cir.IdResponsable=@IdReponsable
	 );
	end
	ELSE
	BEGIN
	insert into @FirstTable (IdConfigIndicadorResponsable,Prefijo,DescripcionCorta,EstatusLlenado,TipoIndicador,
	TipoFrecuencia, TipoCalculo,Meta, IdDataValoresIndicadores, Valor, CheckReqSoporte, CheckReqComentario,
	CheckSoporte,
	CheckComentarios, IdElemento, IdIndicador,NombreElemento, DescripcionElemento
	,IdResponsable)
	(SELECT cir.IdConfigIndicadorResponsable, i.Prefijo, i.DescripcionCorta, '' as EstatusLlenado , i.TipoIndicador,
	i.TipoFrecuencia, i.TipoCalculo, i.Meta,
	dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia),
	dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia)),
	i.CheckReqSoporte,
	i.CheckReqComentario,
	i.CheckSoporte,
	i.CheckComentarios,
	i.IdElemento,
	i.IdIndicador,
	e.NombreElemento,
	e.DescripcionElemento,
	cir.IdResponsable
	
	from Config_IndicadorResponsable cir, 
	tbl_Indicadores i, 
	tbl_Elementos e,
	tbl_Responsables r
	where 
	cir.IdIndicador=i.IdIndicador
	and cir.IdResponsable=r.idResponsable 
	and
	((@IdUbicacionPar IS NULL AND	
						r.IdUbicacion=r.IdUbicacion)
						 OR 
						 (@IdUbicacionPar IS NOT NULL AND
						 r.IdUbicacion = @IdUbicacionPar	))
	and i.IdElemento=e.IdElemento
	and cir.IdConfigRelacionIndicadorProyecto is null
	and cir.Estatus=1
	 );
	END 
	
	 select distinct IdConfigIndicadorResponsable,
	 dbo.fct_DevuelveSemaforoIndicador(Valor, TipoIndicador, Meta) as Semaforo, 
	 Prefijo,
	 DescripcionCorta,
	 EstatusLlenado,
	 TipoIndicador,
	TipoFrecuencia,
	 TipoCalculo,
	 Meta, 
	 IdDataValoresIndicadores, 
	 Valor,
	CheckSoporte,
	CheckComentarios,
	dbo.fct_DevuelveSemaforoSoportes(IdDataValoresIndicadores, CheckReqSoporte) as SemaforoSoportes,
	dbo.fct_DevuelveSemaforoNotas(IdDataValoresIndicadores, CheckReqSoporte) as SemaforoComentarios,
	IdElemento, 
	IdIndicador,
	 NombreElemento, 
	 DescripcionElemento,
	 IdResponsable
	from @FirstTable;
	
	
	
	
	
	
END