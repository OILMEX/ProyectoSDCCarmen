-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_MonitoreoIndicadoresResponsablesSubsistemas]
	@IdResponsable int
	
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
	FechaFinEtapa date, FechaFinSubEtapa date, SemaforoSoportes nvarchar(100), SemaforoComentarios nvarchar(100),
	Semaforo nvarchar(100), IdResponsable int, EstatusSubEtapa bit
	)
	
	insert into @FirstTable 
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
	exec dbo.SP_VistaCargaIndicadoresProyectos 0
	
	DECLARE @IdUbicacion int
	DECLARE @Rol varchar(100)
				
	SELECT @IdUbicacion=IdUbicacion, @Rol=Rol from tbl_Responsables where idResponsable=@IdResponsable
		
	IF @IdResponsable=0
		SET @Rol='Administrador'

    IF @Rol='Administrador' 
	BEGIN
		select 
		f.IdResponsable as idResponsable,
		NombreResponsable,
		IdEtapa,
		f.IdProyecto,
		IdSubEtapa,
		f.IdSubsistema,
		dbo.fct_DevuelveSemaforoIndicador(Valor, TipoIndicador, Meta) as Semaforo, IdConfigIndicadorResponsable,
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
		p.NombreProyecto,
		u.Ubicacion as NombreArea,
		p.FechaInicio,
		p.FechaFin,
		dbo.fct_DevuelveSemaforoSoportes(IdDataValoresIndicadores, CheckReqSoporte) as SemaforoSoportes,
		dbo.fct_DevuelveSemaforoNotas(IdDataValoresIndicadores, CheckReqSoporte) as SemaforoComentarios,
		sub.NombreSubsistema
		from 
		@FirstTable f,
		tbl_Responsables r,
		tbl_Proyectos p,
		tbl_Ubicaciones u,
		tbl_Subsistemas sub
		where f.IdResponsable=r.idResponsable
		and f.IdProyecto=p.IdProyecto
		and p.IdUbicacion=u.idUbicacion
		and f.IdSubsistema=sub.IdSubsistema
		and Valor is null 
	END
	ELSE
	BEGIN
		select 
		f.IdResponsable as idResponsable,
		NombreResponsable,
		IdEtapa,
		f.IdProyecto,
		IdSubEtapa,
		f.IdSubsistema,
		dbo.fct_DevuelveSemaforoIndicador(Valor, TipoIndicador, Meta) as Semaforo, IdConfigIndicadorResponsable,
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
		p.NombreProyecto,
		u.Ubicacion as NombreArea,
		p.FechaInicio,
		p.FechaFin,
		dbo.fct_DevuelveSemaforoSoportes(IdDataValoresIndicadores, CheckReqSoporte) as SemaforoSoportes,
		dbo.fct_DevuelveSemaforoNotas(IdDataValoresIndicadores, CheckReqSoporte) as SemaforoComentarios,
		sub.NombreSubsistema
		from 
		@FirstTable f,
		tbl_Responsables r,
		tbl_Proyectos p,
		tbl_Ubicaciones u,
		tbl_Subsistemas sub
		where f.IdResponsable=r.idResponsable
		and f.IdProyecto=p.IdProyecto
		and p.IdUbicacion=u.idUbicacion
		and f.IdSubsistema=sub.IdSubsistema
		and p.IdUbicacion=@IdUbicacion
		and Valor is null 
	END
END