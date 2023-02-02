-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAllCount_IndicadoresLlenosFaltantesXProyecto] 
	-- Add the parameters for the stored procedure here
	@Tipo int,
	@IdProyecto int,
	@IdEtapa int,
	@IdSubetapa int,
	@TipoBusqueda varchar(100)
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
	

	IF @TipoBusqueda='PROYECTO'
	BEGIN
		IF @Tipo=1 --Lleno
		BEGIN
			select @Resultado=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
			where Valor is not null 
			and IdProyecto = @IdProyecto
		END
		ELSE --Faltantes
		BEGIN
			select @Resultado=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
			where Valor is null
			 and IdProyecto=@IdProyecto
		END
    END
    ELSE IF @TipoBusqueda='ETAPA'
    BEGIN
    
		IF @Tipo=1 --Lleno
			BEGIN
				select @Resultado=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
				where Valor is not null 
				and IdProyecto = @IdProyecto AND IdEtapa=@IdEtapa
			END
			ELSE --Faltantes
			BEGIN
				select @Resultado=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
				where Valor is null
				 and IdProyecto = @IdProyecto AND IdEtapa=@IdEtapa
			END
    
    END	
    ELSE IF @TipoBusqueda='SUBETAPA'
    BEGIN
    
		IF @Tipo=1 --Lleno
			BEGIN
				select @Resultado=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
				where Valor is not null 
				and IdProyecto = @IdProyecto AND IdEtapa=@IdEtapa AND IdSubEtapa=@IdSubetapa
			END
			ELSE --Faltantes
			BEGIN
				select @Resultado=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
				where Valor is null
				 and IdProyecto = @IdProyecto AND IdEtapa=@IdEtapa AND IdSubEtapa=@IdSubetapa
			END
    
    END		
    
    
		IF @Resultado is null
		set @Resultado=0
	
	
	RETURN @Resultado
	
END