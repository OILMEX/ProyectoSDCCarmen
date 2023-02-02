-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAllCount_IndicadoresLlenosFaltantesSubsistemas] 
	-- Add the parameters for the stored procedure here
	@IdSubsistema int,
	@Tipo int,
	@IdResponsable int
AS
BEGIN

	Declare @Resultado int
	DECLARE @TablaPivote TABLE (Valor int, IdSubsistema int)
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    --Insertando Información por Elemento Subsistemas
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
	
	IF @Tipo=1 --Lleno
		select @Resultado=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable where Valor is not null and IdSubsistema=@IdSubsistema;
		else --Faltantes
		select @Resultado=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable where Valor is null and IdSubsistema=@IdSubsistema;
	END
	ELSE
	BEGIN
		IF @Tipo=1 --Lleno
		BEGIN
			select @Resultado=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
			where Valor is not null and IdSubsistema=@IdSubsistema 
			and IdProyecto in (select IdProyecto from tbl_Proyectos where IdUbicacion=@IdUbicacion and Estatus=1) ;
		END
		else --Faltantes
		BEGIN
			select @Resultado=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
			where Valor is null and IdSubsistema=@IdSubsistema
			 and IdProyecto in (select IdProyecto from tbl_Proyectos where IdUbicacion=@IdUbicacion and Estatus=1) ;
		END
	END
	
	RETURN @Resultado
	
END