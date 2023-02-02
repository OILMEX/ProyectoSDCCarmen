
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_VistaparaPivotGridProyectos] 
	-- Add the parameters for the stored procedure here
	@IdResponsable int,
	@EstatusTiempo nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @FirstTable TABLE (IdConfigIndicadorResponsable INT,Prefijo nvarchar(300), 
	DescripcionCorta nvarchar(MAX), EstatusLlenado  nvarchar(100), TipoIndicador nvarchar(100), TipoFrecuencia int, 
	TipoCalculo nvarchar(50),	Meta int, IdProyecto int,IdEtapa int, IdSubEtapa int, IdDataValoresIndicadores int, Valor int,
	CheckReqSoporte bit, CheckReqComentario bit, CheckSoporte bit, CheckComentarios bit, IdElemento int, IdIndicador int, 
	IdSubsistema int,
	ClaveEtapa int, NombreEtapa nvarchar(500),ClaveSubEtapa float, NombreSubEtapa nvarchar(500),NombreSubsistema nvarchar(50),
	DescripcionLargaSubsistema nvarchar(500), NombreElemento nvarchar(300), DescripcionElemento nvarchar(500),
	FechaFinEtapa date, FechaFinSubEtapa date, IdResponsable int, EstatusSubEtapa bit, FechaCompromisoParaEmpezarAMostrarse date 
	)
	
	DECLARE @IdUbicacion int
	DECLARE @Rol varchar(100)
	
	IF @IdResponsable=0
	SET @Rol='Administrador'
	ELSE
    SELECT @IdUbicacion=IdUbicacion, @Rol=Rol from tbl_Responsables where idResponsable=@IdResponsable
    
    IF (@IdResponsable>0)
    BEGIN
	insert into @FirstTable 
	(IdConfigIndicadorResponsable,
	Prefijo,
	DescripcionCorta,
	EstatusLlenado,
	TipoIndicador,
	TipoFrecuencia,
	TipoCalculo,
	Meta,
	IdProyecto,
	IdEtapa,
	IdSubEtapa,
	IdDataValoresIndicadores,
	Valor,
	CheckReqSoporte,
	CheckReqComentario,
	CheckSoporte,
	CheckComentarios,
	IdElemento,
	IdIndicador,
	IdSubsistema,
	ClaveEtapa,
	NombreEtapa,
	ClaveSubEtapa,
	NombreSubEtapa,
	NombreSubsistema,
	DescripcionLargaSubsistema,
	NombreElemento,
	DescripcionElemento,
	FechaFinEtapa,
	FechaFinSubEtapa,
	IdResponsable,
	EstatusSubEtapa,
	FechaCompromisoParaEmpezarAMostrarse
	) 
	(SELECT 
	cir.IdConfigIndicadorResponsable, 
	i.Prefijo, 
	i.DescripcionCorta, 
	'' as EstatusLlenado, 
	i.TipoIndicador, 
	i.TipoFrecuencia,
	i.TipoCalculo, 
	i.Meta, 
	crp.IdProyecto,
	et.IdEtapa,
    csi.IdSubEtapa,
    dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia),
	dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia)),
	i.CheckReqSoporte,
	i.CheckReqComentario,
	i.CheckSoporte,
	i.CheckComentarios,
	i.IdElemento,
	i.IdIndicador,
	e.IdSubsistema,
	et.Clave as ClaveEtapa,
	et.NombreEtapa,
	se.Clave as ClaveSubEtapa,
	se.NombreSubEtapa,
	sub.NombreSubsistema,
	sub.DescripcionLargaSubsistema,
	e.NombreElemento,
	e.DescripcionElemento,
	dbo.fct_DevuelveFechaFinEtapaProyecto(et.IdEtapa, crp.IdProyecto) as FechaFinEtapa,
	dbo.fct_DevuelveFechaFinSubEtapaProyecto(csi.IdSubEtapa, crp.IdProyecto) as FechaFinSubEtapa,
	cir.IdResponsable,
	dbo.fct_DevuelveEstatusSubEtapaProyecto(csi.IdSubEtapa,crp.IdProyecto) as EstatusSubEtapa,
	crp.FechaCompromisoParaEmpezarAMostrarse
	from 
	Config_IndicadorResponsable cir, 
	tbl_Indicadores i, 
	Config_RelacionIndicadorProyecto crp, 
	Config_SubEtapaIndicador csi,
	Config_FechasSubEtapasProyecto cfsp,
	tbl_Elementos e,
	tbl_Subsistemas sub,
	tbl_SubEtapas se,
	tbl_Etapas et,
	tbl_Proyectos p
	where 
	((@EstatusTiempo='' AND
	p.EstatusTiempo=p.EstatusTiempo)
	OR (@EstatusTiempo <> '' AND
	p.EstatusTiempo = @EstatusTiempo
	))
	and	cir.IdIndicador=i.IdIndicador 
	and cir.IdConfigRelacionIndicadorProyecto=crp.IdConfigRelacionIndicadorProyecto
	and crp.IdProyecto=cfsp.IdProyecto
	and crp.IdProyecto=p.IdProyecto
	and p.Estatus=1
	and crp.IdConfigSubEtapaIndicador=csi.IdConfigSubEtapaIndicador
	and csi.IdSubEtapa = cfsp.IdSubEtapa
	and cfsp.EstatusTiempo is not null
	and i.IdElemento=e.IdElemento
	and e.IdSubsistema=sub.IdSubsistema
	and se.IdSubEtapa=csi.IdSubEtapa
	and se.IdEtapa=et.IdEtapa
	and cir.IdConfigRelacionIndicadorProyecto is not null
	and crp.Estatus=1
	and cir.Estatus=1
	and cir.IdResponsable=@IdResponsable);
	END
	ELSE
	BEGIN
	
	insert into @FirstTable 
	(IdConfigIndicadorResponsable,
	Prefijo,
	DescripcionCorta,
	EstatusLlenado,
	TipoIndicador,
	TipoFrecuencia,
	TipoCalculo,
	Meta,
	IdProyecto,
	IdEtapa,
	IdSubEtapa,
	IdDataValoresIndicadores,
	Valor,
	CheckReqSoporte,
	CheckReqComentario,
	CheckSoporte,
	CheckComentarios,
	IdElemento,
	IdIndicador,
	IdSubsistema,
	ClaveEtapa,
	NombreEtapa,
	ClaveSubEtapa,
	NombreSubEtapa,
	NombreSubsistema,
	DescripcionLargaSubsistema,
	NombreElemento,
	DescripcionElemento,
	FechaFinEtapa,
	FechaFinSubEtapa,
	IdResponsable,
	EstatusSubEtapa,
	FechaCompromisoParaEmpezarAMostrarse
	) 
	(SELECT 
	cir.IdConfigIndicadorResponsable, 
	i.Prefijo, 
	i.DescripcionCorta, 
	'' as EstatusLlenado, 
	i.TipoIndicador, 
	i.TipoFrecuencia,
	i.TipoCalculo, 
	i.Meta, 
	crp.IdProyecto,
	et.IdEtapa,
    csi.IdSubEtapa,
    dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia),
	dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,null,i.TipoFrecuencia)),
	i.CheckReqSoporte,
	i.CheckReqComentario,
	i.CheckSoporte,
	i.CheckComentarios,
	i.IdElemento,
	i.IdIndicador,
	e.IdSubsistema,
	et.Clave as ClaveEtapa,
	et.NombreEtapa,
	se.Clave as ClaveSubEtapa,
	se.NombreSubEtapa,
	sub.NombreSubsistema,
	sub.DescripcionLargaSubsistema,
	e.NombreElemento,
	e.DescripcionElemento,
	dbo.fct_DevuelveFechaFinEtapaProyecto(et.IdEtapa, crp.IdProyecto) as FechaFinEtapa,
	dbo.fct_DevuelveFechaFinSubEtapaProyecto(csi.IdSubEtapa, crp.IdProyecto) as FechaFinSubEtapa,
	cir.IdResponsable,
	dbo.fct_DevuelveEstatusSubEtapaProyecto(csi.IdSubEtapa,crp.IdProyecto) as EstatusSubEtapa,
	crp.FechaCompromisoParaEmpezarAMostrarse
	from 
	Config_IndicadorResponsable cir, 
	tbl_Indicadores i, 
	Config_RelacionIndicadorProyecto crp, 
	Config_SubEtapaIndicador csi,
	Config_FechasSubEtapasProyecto cfsp,
	tbl_Elementos e,
	tbl_Subsistemas sub,
	tbl_SubEtapas se,
	tbl_Etapas et,
	tbl_Proyectos p
	where 
	((@EstatusTiempo='' AND
	p.EstatusTiempo=p.EstatusTiempo)
	OR (@EstatusTiempo <> '' AND
	p.EstatusTiempo = @EstatusTiempo
	))
	and cir.IdIndicador=i.IdIndicador 
	and cir.IdConfigRelacionIndicadorProyecto=crp.IdConfigRelacionIndicadorProyecto
	and crp.IdProyecto=cfsp.IdProyecto
	and crp.IdProyecto=p.IdProyecto
	and p.Estatus=1
	and crp.IdConfigSubEtapaIndicador=csi.IdConfigSubEtapaIndicador
	and csi.IdSubEtapa = cfsp.IdSubEtapa
	and cfsp.EstatusTiempo is not null
	and i.IdElemento=e.IdElemento
	and e.IdSubsistema=sub.IdSubsistema
	and se.IdSubEtapa=csi.IdSubEtapa
	and se.IdEtapa=et.IdEtapa
	and cir.IdConfigRelacionIndicadorProyecto is not null
	and crp.Estatus=1
	and cir.Estatus=1
	);
	
	END
	
	

    --------------------------
    
			DECLARE @SecondTable TABLE (IdConfigIndicadorResponsable INT,Prefijo nvarchar(300), 
			DescripcionCorta nvarchar(MAX), EstatusLlenado  nvarchar(100), TipoIndicador nvarchar(100), TipoFrecuencia int, 
			TipoCalculo nvarchar(50),	Meta int, IdProyecto int,IdEtapa int, IdSubEtapa int, IdDataValoresIndicadores int, Valor int,
			CheckReqSoporte bit, CheckReqComentario bit, CheckSoporte bit, CheckComentarios bit, IdElemento int, IdIndicador int, 
			IdSubsistema int,
			ClaveEtapa int, NombreEtapa nvarchar(500),ClaveSubEtapa float, NombreSubEtapa nvarchar(500),NombreSubsistema nvarchar(50),
			DescripcionLargaSubsistema nvarchar(500), NombreElemento nvarchar(300), DescripcionElemento nvarchar(500),
			FechaFinEtapa date, FechaFinSubEtapa date, SemaforoSoportes nvarchar(100), SemaforoComentarios nvarchar(100),
			Semaforo nvarchar(100), IdResponsable int, EstatusSubEtapa bit
			)
			
			insert into @SecondTable 
			( 
			IdConfigIndicadorResponsable,
			Semaforo,
			Prefijo,
			DescripcionCorta,
			EstatusLlenado,
			IdIndicador,
			IdElemento,
			IdSubsistema,
			TipoIndicador,
			TipoFrecuencia,
			TipoCalculo,
			Meta,
			IdProyecto,
			IdEtapa,
			IdSubEtapa,
			IdDataValoresIndicadores,
			Valor,
			CheckSoporte,
			CheckComentarios,
			SemaforoSoportes,
			SemaforoComentarios,
			ClaveEtapa,
			NombreEtapa,
			ClaveSubEtapa,
			NombreSubEtapa,
			NombreSubsistema,
			DescripcionLargaSubsistema,
			NombreElemento,
			DescripcionElemento,
			FechaFinEtapa,
			FechaFinSubEtapa,
			IdResponsable,
			EstatusSubEtapa
			) 
			select distinct IdConfigIndicadorResponsable,
			dbo.fct_DevuelveSemaforoIndicador(Valor, TipoIndicador, Meta) as Semaforo, 
			Prefijo,
			DescripcionCorta,
			EstatusLlenado,
			IdIndicador,
			IdElemento,
			IdSubsistema,
			TipoIndicador,
			TipoFrecuencia,
			TipoCalculo,
			Meta,
			IdProyecto,
			IdEtapa,
			IdSubEtapa,
			IdDataValoresIndicadores,
			Valor,
			CheckSoporte,
			CheckComentarios,
			dbo.fct_DevuelveSemaforoSoportes(IdDataValoresIndicadores, CheckReqSoporte) as SemaforoSoportes,
			dbo.fct_DevuelveSemaforoNotas(IdDataValoresIndicadores, CheckReqSoporte) as SemaforoComentarios,
			ClaveEtapa,
			NombreEtapa,
			ClaveSubEtapa,
			NombreSubEtapa,
			NombreSubsistema,
			DescripcionLargaSubsistema,
			NombreElemento,
			DescripcionElemento,
			FechaFinEtapa,
			FechaFinSubEtapa,
			IdResponsable,
			EstatusSubEtapa
			from @FirstTable
			where EstatusSubEtapa=1 and FechaCompromisoParaEmpezarAMostrarse IS NULL OR FechaCompromisoParaEmpezarAMostrarse <= GETDATE()
			
			
			select  
			p.IdProyecto,
			p.NombreProyecto,
			p.IdArea,
			p.FechaInicio,
			p.FechaFin,
			p.Estatus as EstatusProy,
			p.FechaCreacion as FechaCreacionProy,
			p.UsuarioCreacion as UsuarioCreacionProy,
			p.FechaActualizacion as FechaActualizacionProy,
			p.UsuarioActualizacion as UsuarioActualizacionProy,
			p.EstatusTiempo,
			p.Semaforo as SemaforoProy,
			p.IdElementoOrganigrama,
			p.IdTipoElementoOrganigrama,
			p.IdTipoProyecto,
			u.idUbicacion,
			u.Ubicacion,
			u.Estatus as EstatusUbi,
			resp.idResponsable,
			resp.NombreResponsable,
			resp.Usuario,
			resp.Contrasenia,
			resp.Correo,
			resp.Ficha,
			resp.IdPuesto,
			resp.IdArea as IdArea1Resp,
			resp.IdAquienReporta,
			resp.Rol,
			resp.Estatus as EstatusResp,
			resp.IdUbicacion as IdUbicacionResp,
			f.IdConfigIndicadorResponsable,
			f.Semaforo,
			f.Prefijo,
			f.DescripcionCorta,
			f.EstatusLlenado,
			f.IdIndicador,
			f.IdElemento,
			f.IdSubsistema,
			f.TipoIndicador,
			f.TipoFrecuencia,
			f.TipoCalculo,
			f.Meta,
			f.IdEtapa,
			f.IdSubEtapa,
			f.IdDataValoresIndicadores,
			f.Valor,
			f.CheckSoporte,
			f.CheckComentarios,
			f.SemaforoSoportes,
			f.SemaforoComentarios,
			f.ClaveEtapa,
			f.NombreEtapa,
			f.ClaveSubEtapa,
			f.NombreSubEtapa,
			f.NombreSubsistema,
			f.DescripcionLargaSubsistema,
			f.NombreElemento,
			f.DescripcionElemento,
			f.FechaFinEtapa,
			f.FechaFinSubEtapa,
			f.EstatusSubEtapa
			
			from @SecondTable f, tbl_Proyectos p, tbl_Responsables resp, tbl_Ubicaciones u 
			where f.IdProyecto=p.IdProyecto
			and f.IdResponsable=resp.idResponsable
			and p.IdUbicacion=u.idUbicacion;
END