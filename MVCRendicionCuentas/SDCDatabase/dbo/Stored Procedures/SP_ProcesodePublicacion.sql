-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ProcesodePublicacion]
	-- Add the parameters for the stored procedure here
	@Tipo nvarchar(50)
	
AS
BEGIN
	
	DECLARE @IdPublicacion int
	DECLARE @MES int
	DECLARE @Valor12MPI int
	DECLARE @Semaforo12MPI nvarchar(50)
	DECLARE @EstatusMejora12MPI int
	--
	DECLARE @ValorSASP int
	DECLARE @SemaforoSASP nvarchar(50)
	DECLARE @EstatusMejoraSASP int
	--
	DECLARE @ValorSAA int
	DECLARE @SemaforoSAA nvarchar(50)
	DECLARE @EstatusMejoraSAA int
	--
	DECLARE @ValorSAST int
	DECLARE @SemaforoSAST nvarchar(50)
	DECLARE @EstatusMejoraSAST int
	--
	DECLARE @ValorSSPA int
	DECLARE @SemaforoSSPA nvarchar(50)
	DECLARE @EstatusMejoraSSPA int
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    insert into dbo.Dashboard_Publicaciones (FechaPublicacion,TipoPublicacion)
    VALUES(GETDATE(),@Tipo);
    
    set @IdPublicacion = SCOPE_IDENTITY();
    
    exec @Valor12MPI=  dbo.SP_SelectPromedioRT12MPI;
    exec @ValorSASP = dbo.SP_SelectPromedioRTSubsistemas 2
	exec @ValorSAA = dbo.SP_SelectPromedioRTSubsistemas 3
	exec @ValorSAST = dbo.SP_SelectPromedioRTSubsistemas 4
	set  @ValorSSPA= (@Valor12MPI+@ValorSASP+@ValorSAA+@ValorSAST)/4;
	
	set @Semaforo12MPI= dbo.fct_DevuelveSemaforoIndicador(@Valor12MPI,'Seguimiento',100);
	set @SemaforoSASP= dbo.fct_DevuelveSemaforoIndicador(@ValorSASP,'Seguimiento',100);
	set @SemaforoSAA= dbo.fct_DevuelveSemaforoIndicador(@ValorSAA,'Seguimiento',100);
	set @SemaforoSAST= dbo.fct_DevuelveSemaforoIndicador(@ValorSAST,'Seguimiento',100);
	set @SemaforoSSPA= dbo.fct_DevuelveSemaforoIndicador(@ValorSSPA,'Seguimiento',100);
	
	insert into dbo.Dashboard_PublicacionAvanceSubsistema (IdSubsistema,IdPublicacion,Valor,Semaforo)
	VALUES (1,@IdPublicacion,@Valor12MPI,@Semaforo12MPI);
	--
	insert into dbo.Dashboard_PublicacionAvanceSubsistema (IdSubsistema,IdPublicacion,Valor,Semaforo)
	VALUES (2,@IdPublicacion,@ValorSASP,@SemaforoSASP);
	--
	insert into dbo.Dashboard_PublicacionAvanceSubsistema (IdSubsistema,IdPublicacion,Valor,Semaforo)
	VALUES (3,@IdPublicacion,@ValorSAA,@SemaforoSAA);
	--
	insert into dbo.Dashboard_PublicacionAvanceSubsistema (IdSubsistema,IdPublicacion,Valor,Semaforo)
	VALUES (4,@IdPublicacion,@ValorSAST,@SemaforoSAST);
    --
	insert into dbo.Dashboard_PublicacionAvanceSubsistema (IdSubsistema,IdPublicacion,Valor,Semaforo)
	VALUES (5,@IdPublicacion,@ValorSSPA,@SemaforoSSPA);
	
	SET @MES=datepart(month,getdate());
	
	insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
	VALUES (1,@IdPublicacion,@MES,@Valor12MPI,@Semaforo12MPI)
	--
	insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
	VALUES (2,@IdPublicacion,@MES,@ValorSASP,@SemaforoSASP)
	--
	insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
	VALUES (3,@IdPublicacion,@MES,@ValorSAA,@SemaforoSAA)
	--
	insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
	VALUES (4,@IdPublicacion,@MES,@ValorSAST,@SemaforoSAST)
	--
	insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
	VALUES (5,@IdPublicacion,@MES,@ValorSSPA,@SemaforoSSPA)
	
	--Insertando Información por Elemento Subsistemas
	DECLARE @FirstTable TABLE (IdConfigIndicadorResponsable INT,Prefijo nvarchar(300), 
	DescripcionCorta nvarchar(MAX), EstatusLlenado  nvarchar(100), TipoIndicador nvarchar(100), TipoFrecuencia int, 
	TipoCalculo nvarchar(50),	Meta int, IdProyecto int,IdEtapa int, IdSubEtapa int, IdDataValoresIndicadores int, Valor int,
	CheckReqSoporte bit, CheckReqComentario bit, CheckSoporte bit, CheckComentarios bit, IdElemento int, IdIndicador int, 
	IdSubsistema int,
	ClaveEtapa int, NombreEtapa nvarchar(500),ClaveSubEtapa float, NombreSubEtapa nvarchar(500),NombreSubsistema nvarchar(50),
	DescripcionLargaSubsistema nvarchar(500), NombreElemento nvarchar(300), DescripcionElemento nvarchar(500),
	FechaFinEtapa date, FechaFinSubEtapa date, SemaforoSoportes nvarchar(100), SemaforoComentarios nvarchar(100),
	Semaforo nvarchar(100)
	)
	
	insert into @FirstTable 
	(Semaforo, 
	IdConfigIndicadorResponsable,
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
	FechaFinSubEtapa
	) 
	exec dbo.SP_VistaCargaIndicadoresProyectos 0
	
	insert into Dashboard_PublicacionElemento(IdSubsistema,IdPublicacion,IdElemento,NombreElemento,Valor,Semaforo)
	(select  IdSubsistema,@IdPublicacion,IdElemento,NombreElemento, (Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))), dbo.fct_DevuelveSemaforoIndicador((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))),'Seguimiento',100)  from @FirstTable group by IdElemento, IdSubsistema,NombreElemento)
	
	--Insertando Información por Elemento 12MPI
	
	DECLARE @SecondTable TABLE (IdConfigIndicadorResponsable INT,Prefijo nvarchar(300), 
	DescripcionCorta nvarchar(MAX), EstatusLlenado  nvarchar(100), TipoIndicador nvarchar(100), TipoFrecuencia int, 
	TipoCalculo nvarchar(50), Meta int,	IdDataValoresIndicadores int, Valor int,
	CheckReqSoporte bit, CheckReqComentario bit, CheckSoporte bit, CheckComentarios bit, IdElemento int, 
	IdIndicador int, NombreElemento nvarchar(300), DescripcionElemento nvarchar(500), Semaforo nvarchar(100),SemaforoSoportes nvarchar(100),
	SemaforoComentarios nvarchar(100)
	)
	
	insert into @SecondTable 
	( Semaforo, 
	 IdConfigIndicadorResponsable,
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
	SemaforoSoportes,
	SemaforoComentarios,
	IdElemento, 
	IdIndicador, 
	NombreElemento, 
	DescripcionElemento)
	exec dbo.SP_VistaCargaIndicadores12MPIxReponsable 0
	
	
	insert into Dashboard_PublicacionElemento(IdSubsistema,IdPublicacion,IdElemento,NombreElemento,Valor,Semaforo)
	(select  1,@IdPublicacion,IdElemento,NombreElemento, (Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))), dbo.fct_DevuelveSemaforoIndicador((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))),'Seguimiento',100)  from @SecondTable group by IdElemento,NombreElemento)
	
	
	
	--Sección inserción por proyectos
	
	DECLARE @IdProyecto int
	DECLARE @NombreProyecto nvarchar(500)
	DECLARE @IdResponsable int
	DECLARE @FechaInicio date
	DECLARE @FehaFin date
	DECLARE @IdPublicacionProyecto int
	
	
	-- Declaración del cursor

		DECLARE cProyectos CURSOR FOR

		SELECT  IdProyecto,NombreProyecto, IdResponsable, FechaInicio, 
			FechaFin
		FROM tbl_Proyectos
		
		-- Apertura del cursor

		OPEN cProyectos

		-- Lectura de la primera fila del cursor

		FETCH cProyectos INTO    @IdProyecto,@NombreProyecto, @IdResponsable, @FechaInicio,	@FehaFin

		 

		WHILE (@@FETCH_STATUS = 0 )

		BEGIN

		--Logica de inserción
		
		DECLARE @Valor12MPIProyecto int
		DECLARE @Semaforo12MPIProyecto nvarchar(50)
		
		--
		DECLARE @ValorSASPProyecto int
		DECLARE @SemaforoSASPProyecto nvarchar(50)
		
		--
		DECLARE @ValorSAAProyecto int
		DECLARE @SemaforoSAAProyecto nvarchar(50)
		
		--
		DECLARE @ValorSASTProyecto int
		DECLARE @SemaforoSASTProyecto nvarchar(50)
		
		DECLARE @NombreResponsable nvarchar(200)
		
		select @Valor12MPIProyecto=Porcentaje from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=1;
		select @Semaforo12MPIProyecto=Semaforo from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=1;
		
		select @ValorSASPProyecto=Porcentaje from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=2;
		select @SemaforoSASPProyecto=Semaforo from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=2;
		
		select @ValorSAAProyecto=Porcentaje from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=3;
		select @SemaforoSAAProyecto=Semaforo from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=3;
		
		select @ValorSASTProyecto=Porcentaje from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=4;
		select @SemaforoSASTProyecto=Semaforo from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=4;
		
		
		Select @NombreResponsable=NombreArea from tbl_Areas where idArea=@IdResponsable;
		
		insert into Dashboard_PublicacionProyectos 
		(IdPublicacion,IdProyecto,NombreProyecto,Responsable,IdResponsable,FechaInicio,FechaFin,
		Valor12MPI,Semaforo12MPI,ValorSASP,SemaforoSASP,ValorSAA,SemaforoSAA,ValorSAST,SemaforoSAST)
		VALUES(@IdPublicacion,@IdProyecto,@NombreProyecto,@NombreResponsable,@IdResponsable,@FechaInicio,@FehaFin,
		@Valor12MPI,@Semaforo12MPI,@ValorSASP,@SemaforoSASP,@ValorSAA,@SemaforoSAA,@ValorSAST,@SemaforoSAST)
		
		SET @IdPublicacionProyecto=SCOPE_IDENTITY();
		
		--Inserción de datos de Etapa del proyecto
		--DECLARANDO EL CURSOS PARA INSERTAR TODAS LAS ETAPAS CON ESTATUS=1
		DECLARE @IdEtapaCursos int
		
				-- Declaración del cursor

					DECLARE cEtapas CURSOR FOR

					SELECT  IdEtapa	FROM tbl_Etapas

					-- Apertura del cursor

					OPEN cEtapas

					-- Lectura de la primera fila del cursor

					FETCH cEtapas INTO    @IdEtapaCursos

					 

					WHILE (@@FETCH_STATUS = 0 )

					BEGIN
					
					DECLARE @IdEtapa int;
					declare @NombreEtapa nvarchar(500)
					declare @FechaFinEtapa date
					DECLARE @Valor12MPIEtapa int
					DECLARE @Semaforo12MPIEtapa nvarchar(50)
					DECLARE @CountEnProcesoEtapa int
					DECLARE @EstatusTiempoEtapa nvarchar(500)
					declare @IdPublicacionProyectoEtapa int
					--
					DECLARE @ValorSASPEtapa int
					DECLARE @SemaforoSASPEtapa nvarchar(50)
					
					--
					DECLARE @ValorSAAEtapa int
					DECLARE @SemaforoSAAEtapa nvarchar(50)
					
					--
					DECLARE @ValorSASTEtapa int
					DECLARE @SemaforoSASTEtapa nvarchar(50)
					
					
					select @EstatusTiempoEtapa=cfep.EstatusTiempo, @FechaFinEtapa=cfep.FechaFin, @NombreEtapa=e.NombreEtapa from Config_FechasEtapasProyecto  cfep, tbl_Etapas e
					where cfep.IdEtapa=e.IdEtapa and IdProyecto=@IdProyecto and cfep.IdEtapa=@IdEtapaCursos
					
					select @ValorSASPEtapa= AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta)) 
					from @FirstTable where IdEtapa=@IdEtapaCursos and IdSubsistema =2 and IdProyecto=@IdProyecto
					set @SemaforoSASPEtapa= dbo.fct_DevuelveSemaforoIndicador(@ValorSASPEtapa,'Seguimiento',100)
					--
						select @ValorSAAEtapa= AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta)) 
					from @FirstTable where IdEtapa=@IdEtapaCursos and IdSubsistema =3 and IdProyecto=@IdProyecto
					set @SemaforoSAAEtapa= dbo.fct_DevuelveSemaforoIndicador(@ValorSAAEtapa,'Seguimiento',100)
					--
						select @ValorSASTEtapa= AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta)) 
					from @FirstTable where IdEtapa=@IdEtapaCursos and IdSubsistema =4 and IdProyecto=@IdProyecto
					set @SemaforoSASTEtapa= dbo.fct_DevuelveSemaforoIndicador(@ValorSASTEtapa,'Seguimiento',100)
					
					--Se insertan los valores para Dashboard_PublicacionProyectoEtapa
					insert into Dashboard_PublicacionProyectoEtapa 
					(IdPublicacion,IdPublicacionProyecto,IdEtapa,NombreEtapa,FechaFin,
					Valor12MPI,Semaforo12MPI,ValorSASP,SemaforoSASP,ValorSAA,SemaforoSAA,ValorSAST,SemaforoSAST,EstatusTiempo)
					VALUES(@IdPublicacion,@IdPublicacionProyecto,@IdEtapaCursos,@NombreEtapa,@FechaFinEtapa, @Valor12MPI, @Semaforo12MPI
					,@ValorSASPEtapa, @SemaforoSASPEtapa,@ValorSAAEtapa,@SemaforoSAAEtapa,@ValorSASTEtapa,@SemaforoSASTEtapa,@EstatusTiempoEtapa);
					
					
					set @IdPublicacionProyectoEtapa=SCOPE_IDENTITY()
					
					--CURSOR de SUBETAPAS de Proyecto
					
					DECLARE @IdSubEtapaCursor int
					
					-- Declaración del cursor

								DECLARE cSubEtapas CURSOR FOR

								SELECT  IdSubEtapa

								FROM tbl_SubEtapas where IdEtapa=@IdEtapaCursos

								-- Apertura del cursor

								OPEN cSubEtapas

								-- Lectura de la primera fila del cursor

								FETCH cSubEtapas INTO    @IdSubEtapaCursor

								 

								WHILE (@@FETCH_STATUS = 0 )

								BEGIN
								
								DECLARE @NombreSubEtapa nvarchar(500)
								DECLARE @FechaFinSubEtapa date
								DECLARE @EstatusTiempoSubEtapa nvarchar(50)
								
								DECLARE @ValorSASPSubEtapa int
								DECLARE @SemaforoSASPSubEtapa nvarchar(50)
								
								--
								DECLARE @ValorSAASubEtapa int
								DECLARE @SemaforoSAASubEtapa nvarchar(50)
								
								--
								DECLARE @ValorSASTSubEtapa int
								DECLARE @SemaforoSASTSubEtapa nvarchar(50)
								
								select @EstatusTiempoSubEtapa=EstatusTiempo, @NombreSubEtapa=NombreSubEtapa, @FechaFinSubEtapa=FechaFin 
								from Config_FechasSubEtapasProyecto cfsp, tbl_SubEtapas se
								 where cfsp.IdSubEtapa=se.IdSubEtapa and cfsp.IdSubEtapa=@IdSubEtapaCursor and IdProyecto=@IdProyecto
								
								select @ValorSASPSubEtapa= AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta)) 
								from @FirstTable where IdSubEtapa=@IdSubEtapaCursor and IdSubsistema =2 and IdProyecto=@IdProyecto
								set @SemaforoSASPSubEtapa= dbo.fct_DevuelveSemaforoIndicador(@ValorSASPSubEtapa,'Seguimiento',100)
								--
									select @ValorSAASubEtapa= AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta)) 
								from @FirstTable where IdSubEtapa=@IdSubEtapaCursor and IdSubsistema =3 and IdProyecto=@IdProyecto
								set @SemaforoSAASubEtapa= dbo.fct_DevuelveSemaforoIndicador(@ValorSAASubEtapa,'Seguimiento',100)
								--
									select @ValorSASTSubEtapa= AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta)) 
								from @FirstTable where IdSubEtapa=@IdSubEtapaCursor and IdSubsistema =4 and IdProyecto=@IdProyecto
								set @SemaforoSASTSubEtapa= dbo.fct_DevuelveSemaforoIndicador(@ValorSASTSubEtapa,'Seguimiento',100)

								insert into Dashboard_PublicacionProyectoSubetapa 
								(IdPublicacion,IdPublicacionProyecto,IdPublicacionProyectoEtapa,NombreSubEtapa,IdSubEtapa,IdEtapa
								,Valor12MPI,Semaforo12MPI,ValorSASP,SemaforoSASP,ValorSAA,SemaforoSAA,ValorSAST,SemaforoSAST,
								EstatusTiempo,FechaFinSubEtapa)
								VALUES
								(@IdPublicacion,@IdPublicacionProyecto,@IdPublicacionProyectoEtapa,@NombreSubEtapa,@IdSubEtapaCursor,
								@IdEtapaCursos,
								@Valor12MPI,@Semaforo12MPI,@ValorSASPSubEtapa,@SemaforoSASPSubEtapa,@ValorSAASubEtapa,@SemaforoSAASubEtapa,
								@ValorSASTSubEtapa,@SemaforoSASTSubEtapa,@EstatusTiempoSubEtapa,@FechaFinSubEtapa
								)

								-- Lectura de la siguiente fila del cursor

								FETCH cSubEtapas INTO   @IdSubEtapaCursor

								END

								 

								-- Cierre del cursor

								CLOSE cSubEtapas

								-- Liberar los recursos

								DEALLOCATE cSubEtapas
					

					-- Lectura de la siguiente fila del cursor

					FETCH cEtapas INTO    @IdEtapaCursos

					END

					 

					-- Cierre del cursor

					CLOSE cEtapas

					-- Liberar los recursos

					DEALLOCATE cEtapas

 
		
		
		

		-- Lectura de la siguiente fila del cursor

		FETCH cProyectos INTO    @IdProyecto,@NombreProyecto, @IdResponsable, @FechaInicio,	@FehaFin

		END

		 

		-- Cierre del cursor

		CLOSE cProyectos

		-- Liberar los recursos

		DEALLOCATE cProyectos
	
	
END