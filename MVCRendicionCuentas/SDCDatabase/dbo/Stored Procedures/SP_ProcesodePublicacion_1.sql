-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ProcesodePublicacion]
	-- Add the parameters for the stored procedure here
	@Tipo nvarchar(50)
	
AS
BEGIN
	
	
	-- Declaracion de variables para el cursor
	
	DECLARE @TableUbicaciones TABLE (IdUbicacion INT);
	
	INSERT INTO @TableUbicaciones (IdUbicacion) VALUES (0);
	INSERT INTO @TableUbicaciones (IdUbicacion) SELECT DISTINCT  IdUbicacion FROM tbl_Proyectos WHERE Estatus=1 group by IdUbicacion;

	DECLARE @IdUbicacion int

	-- Declaración del cursor

	DECLARE cUbicaciones CURSOR FOR

	SELECT IdUbicacion FROM @TableUbicaciones

	-- Apertura del cursor

	OPEN cUbicaciones

	-- Lectura de la primera fila del cursor

	FETCH cUbicaciones INTO    @IdUbicacion

	 

	WHILE (@@FETCH_STATUS = 0 )

	BEGIN
	
			DECLARE @IdPublicacion int
			DECLARE @IdPublicacionMesPasado int
			DECLARE @MES int
			DECLARE @Valor12MPI int
			DECLARE @Semaforo12MPI nvarchar(50)
			DECLARE @EstatusMejora12MPI int
			
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
			
			DECLARE @Valor12MPIPublicacionPasada int
			DECLARE @ValorSASPPublicacionPasada int
			DECLARE @ValorSAAPublicacionPasada int
			DECLARE @ValorSASTPublicacionPasada int
			DECLARE @ValorSSPAPublicacionPasada int
			
			DECLARE @MesActual int
			DECLARE @anioactual int
			SET @MesActual=datepart(month,getdate());
			SET @AnioActual=datepart(YEAR,getdate());
			
			-- SET NOCOUNT ON added to prevent extra result sets from
			-- interfering with SELECT statements.
			SET NOCOUNT ON;

			-- Insert statements for procedure here
			insert into dbo.Dashboard_Publicaciones (FechaPublicacion,TipoPublicacion, IdUbicacion)
			VALUES(GETDATE(),@Tipo,@IdUbicacion);
		    
			set @IdPublicacion = SCOPE_IDENTITY();
		    
			DECLARE @IdResponsableParU int
			SELECT @IdResponsableParU=idResponsable from tbl_Responsables where IdUbicacion=@IdUbicacion and Rol='SubAdministrador'
		    
		    declare @Contador int=0;
		    
			IF @IdUbicacion>0
			BEGIN
				exec @Valor12MPI=  dbo.SP_SelectPromedioRT12MPIByUbicacion @IdUbicacion;
				exec @ValorSASP = dbo.SP_SelectPromedioRTSubsistemas 2, @IdResponsableParU
				exec @ValorSAA = dbo.SP_SelectPromedioRTSubsistemas 3, @IdResponsableParU
				exec @ValorSAST = dbo.SP_SelectPromedioRTSubsistemas 4, @IdResponsableParU
				
				
				
				if @Valor12MPI >= 0
					set @Contador=@Contador+1;
				else
					set @Valor12MPI=0;	
				if @ValorSASP >= 0
					set @Contador=@Contador+1;
				else
					set @ValorSASP=0;	
				if 	@ValorSAA >= 0
					set @Contador=@Contador+1;
				else
					set @ValorSAA=0;
				if 	@ValorSAST >= 0
					set @Contador=@Contador+1;
				else
					set @ValorSAST=0;
				
				set @ValorSSPA= (@Valor12MPI+@ValorSASP+@ValorSAA+@ValorSAST)	
				
				if @ValorSSPA is not null	
					if @Contador>0
						set @ValorSSPA=@ValorSSPA/@Contador
					 else
						set @ValorSSPA=0
				else
					set @ValorSSPA=0
		
			END
			ELSE
			BEGIN
				exec @Valor12MPI=  dbo.SP_SelectPromedioRT12MPI 0;
				exec @ValorSASP = dbo.SP_SelectPromedioRTSubsistemas 2, 0
				exec @ValorSAA = dbo.SP_SelectPromedioRTSubsistemas 3, 0
				exec @ValorSAST = dbo.SP_SelectPromedioRTSubsistemas 4, 0
				
				
				
				if @Valor12MPI >= 0
					set @Contador=@Contador+1;
				else
					set @Valor12MPI=0;	
				if @ValorSASP >= 0
					set @Contador=@Contador+1;
				else
					set @ValorSASP=0;	
				if 	@ValorSAA >= 0
					set @Contador=@Contador+1;
				else
					set @ValorSAA=0;
				if 	@ValorSAST >= 0
					set @Contador=@Contador+1;
				else
					set @ValorSAST=0;
				
				set @ValorSSPA= (@Valor12MPI+@ValorSASP+@ValorSAA+@ValorSAST)	
				
				if @ValorSSPA is not null	
					if @Contador>0
						set @ValorSSPA=@ValorSSPA/@Contador
					 else
						set @ValorSSPA=0
				else
					set @ValorSSPA=0
					
			END
			
			IF @MesActual>1
			BEGIN
			
			select top 1 @IdPublicacionMesPasado=IdPublicacion from Dashboard_Publicaciones 
			where TipoPublicacion='Produccion' and MONTH(FechaPublicacion)=@MesActual-1 and YEAR(FechaPublicacion)=@anioactual and
				  IdUbicacion=@IdUbicacion
			order by FechaPublicacion desc;
			
			SELECT @Valor12MPIPublicacionPasada=Valor FROM Dashboard_PublicacionAvanceSubsistema where IdPublicacion=@IdPublicacionMesPasado and IdSubsistema=1;
			SELECT @ValorSASPPublicacionPasada=Valor FROM Dashboard_PublicacionAvanceSubsistema where IdPublicacion=@IdPublicacionMesPasado and IdSubsistema=2;
			SELECT @ValorSAAPublicacionPasada=Valor FROM Dashboard_PublicacionAvanceSubsistema where IdPublicacion=@IdPublicacionMesPasado and IdSubsistema=3;
			SELECT @ValorSASTPublicacionPasada=Valor FROM Dashboard_PublicacionAvanceSubsistema where IdPublicacion=@IdPublicacionMesPasado and IdSubsistema=4;
			SELECT @ValorSSPAPublicacionPasada=Valor FROM Dashboard_PublicacionAvanceSubsistema where IdPublicacion=@IdPublicacionMesPasado and IdSubsistema=5;
			
			SET @EstatusMejora12MPI= dbo.fct_DevuelveEstatusMejora(@Valor12MPIPublicacionPasada,@Valor12MPI);
			SET @EstatusMejoraSASP= dbo.fct_DevuelveEstatusMejora(@ValorSASPPublicacionPasada,@ValorSASP);
			SET @EstatusMejoraSAA= dbo.fct_DevuelveEstatusMejora(@ValorSAAPublicacionPasada,@ValorSAA);
			SET @EstatusMejoraSAST= dbo.fct_DevuelveEstatusMejora(@ValorSASTPublicacionPasada,@ValorSAST);
			SET @EstatusMejoraSSPA= dbo.fct_DevuelveEstatusMejora(@ValorSSPAPublicacionPasada,@ValorSSPA);
			
			
			END
			
			
			set @Semaforo12MPI= dbo.fct_DevuelveSemaforoIndicador(@Valor12MPI,'Seguimiento',100);
			set @SemaforoSASP= dbo.fct_DevuelveSemaforoIndicador(@ValorSASP,'Seguimiento',100);
			set @SemaforoSAA= dbo.fct_DevuelveSemaforoIndicador(@ValorSAA,'Seguimiento',100);
			set @SemaforoSAST= dbo.fct_DevuelveSemaforoIndicador(@ValorSAST,'Seguimiento',100);
			set @SemaforoSSPA= dbo.fct_DevuelveSemaforoIndicador(@ValorSSPA,'Seguimiento',100);
			
			insert into dbo.Dashboard_PublicacionAvanceSubsistema (IdSubsistema,IdPublicacion,Valor,Semaforo, EstatusMejora)
			VALUES (1,@IdPublicacion,@Valor12MPI,@Semaforo12MPI, @EstatusMejora12MPI);
			--
			insert into dbo.Dashboard_PublicacionAvanceSubsistema (IdSubsistema,IdPublicacion,Valor,Semaforo, EstatusMejora)
			VALUES (2,@IdPublicacion,@ValorSASP,@SemaforoSASP, @EstatusMejoraSASP);
			--
			insert into dbo.Dashboard_PublicacionAvanceSubsistema (IdSubsistema,IdPublicacion,Valor,Semaforo, EstatusMejora)
			VALUES (3,@IdPublicacion,@ValorSAA,@SemaforoSAA, @EstatusMejoraSAA);
			--
			insert into dbo.Dashboard_PublicacionAvanceSubsistema (IdSubsistema,IdPublicacion,Valor,Semaforo, EstatusMejora)
			VALUES (4,@IdPublicacion,@ValorSAST,@SemaforoSAST, @EstatusMejoraSAST);
			--
			insert into dbo.Dashboard_PublicacionAvanceSubsistema (IdSubsistema,IdPublicacion,Valor,Semaforo, EstatusMejora)
			VALUES (5,@IdPublicacion,@ValorSSPA,@SemaforoSSPA, @EstatusMejoraSSPA);
			
			SET @MES=datepart(month,getdate());
			
			exec SP_InsertValoresdeMesesxSubsistemasxPublicacion @IdPublicacion, @IdUbicacion;
			
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
			
			insert into Dashboard_PublicacionElemento(IdSubsistema,IdPublicacion,IdElemento,NombreElemento,Valor,Semaforo, EstatusMejora)
			(select  IdSubsistema,@IdPublicacion,IdElemento,NombreElemento, 
			(Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))), 
			dbo.fct_DevuelveSemaforoIndicador((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))),'Seguimiento',100),
			dbo.fct_DevuelveEstatusMejoraElementos ((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))), @IdPublicacionMesPasado, IdElemento)  
			from @FirstTable
			where  (@IdUbicacion>0 AND IdProyecto in (select IdProyecto from tbl_Proyectos where IdUbicacion=@IdUbicacion and Estatus=1))
					OR (@IdUbicacion<1 AND IdProyecto=IdProyecto)
			
			 group by IdElemento, IdSubsistema,NombreElemento)
			
			--Insertando Información por Elemento 12MPI
			
			DECLARE @SecondTable TABLE (IdConfigIndicadorResponsable INT,Prefijo nvarchar(300), 
			DescripcionCorta nvarchar(MAX), EstatusLlenado  nvarchar(100), TipoIndicador nvarchar(100), TipoFrecuencia int, 
			TipoCalculo nvarchar(50), Meta int,	IdDataValoresIndicadores int, Valor int,
			CheckReqSoporte bit, CheckReqComentario bit, CheckSoporte bit, CheckComentarios bit, IdElemento int, 
			IdIndicador int, NombreElemento nvarchar(300), DescripcionElemento nvarchar(500), Semaforo nvarchar(100),SemaforoSoportes nvarchar(100),
			SemaforoComentarios nvarchar(100), IdResponsable int
			)
			
			insert into @SecondTable 
			( IdConfigIndicadorResponsable,
			Semaforo,
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
			DescripcionElemento,IdResponsable)
			exec dbo.SP_VistaCargaIndicadores12MPIxReponsable 0
			
			if @IdUbicacion =0
				BEGIN
					insert into Dashboard_PublicacionElemento(IdSubsistema,IdPublicacion,IdElemento,NombreElemento,Valor,Semaforo, EstatusMejora)
					(select  1,@IdPublicacion,IdElemento,NombreElemento, 
					(Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))), 
					dbo.fct_DevuelveSemaforoIndicador((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))),'Seguimiento',100) ,
					dbo.fct_DevuelveEstatusMejoraElementos ((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))), @IdPublicacionMesPasado, IdElemento)
					from @SecondTable group by IdElemento,NombreElemento)
				END
			ELSE
				BEGIN
					insert into Dashboard_PublicacionElemento(IdSubsistema,IdPublicacion,IdElemento,NombreElemento,Valor,Semaforo, EstatusMejora)
					(select  1,@IdPublicacion,IdElemento,NombreElemento, 
					(Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))), 
					dbo.fct_DevuelveSemaforoIndicador((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))),'Seguimiento',100) ,
					dbo.fct_DevuelveEstatusMejoraElementos ((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))), @IdPublicacionMesPasado, IdElemento)
					from @SecondTable where IdResponsable in (select IdResponsable from tbl_Responsables where IdUbicacion=@IdUbicacion)  group by IdElemento,NombreElemento)
				
				END
			
			
			
			
			--Sección inserción por proyectos
			
			DECLARE @IdProyecto int
			DECLARE @NombreProyecto nvarchar(500)
			DECLARE @IdResponsable int
			DECLARE @FechaInicio date
			DECLARE @FehaFin date
			DECLARE @IdPublicacionProyecto int
			DECLARE @IdElementoOrganigrama int
			DECLARE @IdTipoElementoOrganigrama int
			DECLARE @IdTipoProyecto int
			DECLARE @SemaforoProyecto nvarchar(50)
			
			
			-- Declaración del cursor

				DECLARE cProyectos CURSOR FOR

				SELECT  IdProyecto,NombreProyecto, IdResponsable, FechaInicio, 
					FechaFin, IdElementoOrganigrama, IdTipoElementoOrganigrama, IdTipoProyecto, Semaforo
				FROM tbl_Proyectos where Estatus=1 and EstatusTiempo='En Proceso' AND
				(@IdUbicacion>0 AND IdProyecto in (select IdProyecto from tbl_Proyectos where IdUbicacion=@IdUbicacion and Estatus=1))
					OR (@IdUbicacion<1 AND IdProyecto=IdProyecto AND Estatus=1)
				
				-- Apertura del cursor

				OPEN cProyectos

				-- Lectura de la primera fila del cursor

				FETCH cProyectos INTO    @IdProyecto,@NombreProyecto, @IdResponsable, @FechaInicio,	@FehaFin,
										@IdElementoOrganigrama, @IdTipoElementoOrganigrama, @IdTipoProyecto, @SemaforoProyecto

				 

				WHILE (@@FETCH_STATUS = 0 )

				BEGIN

				--Logica de inserción
				
				DECLARE @Valor12MPIProyecto int = NULL
				DECLARE @Semaforo12MPIProyecto nvarchar(50)= NULL
				
				--
				DECLARE @ValorSASPProyecto int= NULL
				DECLARE @SemaforoSASPProyecto nvarchar(50)= NULL
				
				--
				DECLARE @ValorSAAProyecto int= NULL
				DECLARE @SemaforoSAAProyecto nvarchar(50)= NULL
				
				--
				DECLARE @ValorSASTProyecto int= NULL
				DECLARE @SemaforoSASTProyecto nvarchar(50)= NULL
				
				DECLARE @NombreResponsable nvarchar(200)= NULL
				
				
				
				
				
				select @Valor12MPIProyecto=Porcentaje from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=1;
				select @Semaforo12MPIProyecto=Semaforo from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=1;
				
				select @ValorSASPProyecto=Porcentaje from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=2;
				select @SemaforoSASPProyecto=Semaforo from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=2;
				
				select @ValorSAAProyecto=Porcentaje from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=3;
				select @SemaforoSAAProyecto=Semaforo from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=3;
				
				select @ValorSASTProyecto=Porcentaje from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=4;
				select @SemaforoSASTProyecto=Semaforo from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=4;
				
				
				Select @NombreResponsable=u.Ubicacion from tbl_Responsables r, tbl_Ubicaciones u 
				where r.IdUbicacion=u.idUbicacion and idResponsable=@IdResponsable;
				
				DECLARE @Trace nvarchar(500)
				exec dbo.SP_SelectTrace_Proyecto  @IdElementoOrganigrama, @IdTipoElementoOrganigrama, @Trace OUTPUT
				
				DECLARE @IndicadoresLlenosProyecto int= NULL
				DECLARE @IndicadoresFaltantesProyecto int= NULL
				
				
					
					select @IndicadoresLlenosProyecto=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
					where Valor is not null 
					and IdProyecto = @IdProyecto
					
					select @IndicadoresFaltantesProyecto=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
					where Valor is null
					 and IdProyecto=@IdProyecto
					
				
				
				insert into Dashboard_PublicacionProyectos 
				(IdPublicacion,IdProyecto,NombreProyecto,Responsable,IdResponsable,FechaInicio,FechaFin,
				Valor12MPI,Semaforo12MPI,ValorSASP,SemaforoSASP,ValorSAA,SemaforoSAA,ValorSAST,SemaforoSAST,IdTipoProyecto,ElementoOrganigrama
				,Semaforo,IndicadoresCargados,IndicadoresNoCargados)
				VALUES(@IdPublicacion,@IdProyecto,@NombreProyecto,@NombreResponsable,@IdResponsable,@FechaInicio,@FehaFin,
				@Valor12MPIProyecto,@Semaforo12MPIProyecto,@ValorSASPProyecto,@SemaforoSASPProyecto,@ValorSAAProyecto,@SemaforoSAAProyecto
				,@ValorSASTProyecto,@SemaforoSASTProyecto,@IdTipoProyecto,@Trace,@SemaforoProyecto,@IndicadoresLlenosProyecto,@IndicadoresFaltantesProyecto)
				
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
							
							DECLARE @IdEtapa int= NULL
							declare @NombreEtapa nvarchar(500)= NULL
							declare @FechaFinEtapa date= NULL
							DECLARE @Valor12MPIEtapa int = NULL
							DECLARE @Semaforo12MPIEtapa nvarchar(50)= NULL
							DECLARE @CountEnProcesoEtapa int= NULL
							DECLARE @EstatusTiempoEtapa nvarchar(500)= NULL
							declare @IdPublicacionProyectoEtapa int= NULL
							--
							DECLARE @ValorSASPEtapa int= NULL
							DECLARE @SemaforoSASPEtapa nvarchar(50)= NULL
							
							--
							DECLARE @ValorSAAEtapa int= NULL
							DECLARE @SemaforoSAAEtapa nvarchar(50)= NULL
							
							--
							DECLARE @ValorSASTEtapa int= NULL
							DECLARE @SemaforoSASTEtapa nvarchar(50)= NULL
							
							
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
							
							DECLARE @IndicadoresLlenosEtapa int= NULL
							DECLARE @IndicadoresFaltantesEtapa int= NULL
							
							
							
							select @IndicadoresLlenosEtapa=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
							where Valor is not null 
							and IdProyecto = @IdProyecto AND IdEtapa=@IdEtapaCursos
							
							select @IndicadoresFaltantesEtapa=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
							where Valor is null
							 and IdProyecto = @IdProyecto AND IdEtapa=@IdEtapaCursos
							
							
							
							
							--Se insertan los valores para Dashboard_PublicacionProyectoEtapa
							insert into Dashboard_PublicacionProyectoEtapa 
							(IdPublicacion,IdPublicacionProyecto,IdEtapa,NombreEtapa,FechaFin,
							Valor12MPI,Semaforo12MPI,ValorSASP,SemaforoSASP,ValorSAA,SemaforoSAA,ValorSAST,SemaforoSAST,EstatusTiempo
							,IndicadoresCargados,IndicadoresNoCargados)
							VALUES(@IdPublicacion,@IdPublicacionProyecto,@IdEtapaCursos,@NombreEtapa,@FechaFinEtapa, @Valor12MPI, @Semaforo12MPI
							,@ValorSASPEtapa, @SemaforoSASPEtapa,@ValorSAAEtapa,@SemaforoSAAEtapa,@ValorSASTEtapa,@SemaforoSASTEtapa,@EstatusTiempoEtapa
							,@IndicadoresLlenosEtapa,@IndicadoresFaltantesEtapa);
							
							
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
										
										DECLARE @NombreSubEtapa nvarchar(500)= NULL
										DECLARE @FechaFinSubEtapa date= NULL
										DECLARE @EstatusTiempoSubEtapa nvarchar(50)= NULL
										
										DECLARE @ValorSASPSubEtapa int= NULL
										DECLARE @SemaforoSASPSubEtapa nvarchar(50)= NULL
										
										--
										DECLARE @ValorSAASubEtapa int= NULL
										DECLARE @SemaforoSAASubEtapa nvarchar(50)= NULL
										
										--
										DECLARE @ValorSASTSubEtapa int= NULL
										DECLARE @SemaforoSASTSubEtapa nvarchar(50)= NULL
										
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
										
										DECLARE @IndicadoresLlenosSubEtapa int = NULL
										DECLARE @IndicadoresFaltantesSubEtapa int= NULL
										
										
										select @IndicadoresLlenosSubEtapa=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
										where Valor is not null 
										and IdProyecto = @IdProyecto AND IdEtapa=@IdSubEtapaCursor AND IdSubEtapa=@IdSubEtapaCursor
										
										select @IndicadoresFaltantesSubEtapa=count(DISTINCT IdConfigIndicadorResponsable) from @FirstTable 
										where Valor is null
										 and IdProyecto = @IdProyecto AND IdEtapa=@IdSubEtapaCursor AND IdSubEtapa=@IdSubEtapaCursor



										insert into Dashboard_PublicacionProyectoSubetapa 
										(IdPublicacion,IdPublicacionProyecto,IdPublicacionProyectoEtapa,NombreSubEtapa,IdSubEtapa,IdEtapa
										,Valor12MPI,Semaforo12MPI,ValorSASP,SemaforoSASP,ValorSAA,SemaforoSAA,ValorSAST,SemaforoSAST,
										EstatusTiempo,FechaFinSubEtapa,IndicadoresCargados,IndicadoresNoCargados)
										VALUES
										(@IdPublicacion,@IdPublicacionProyecto,@IdPublicacionProyectoEtapa,@NombreSubEtapa,@IdSubEtapaCursor,
										@IdEtapaCursos,
										@Valor12MPI,@Semaforo12MPI,@ValorSASPSubEtapa,@SemaforoSASPSubEtapa,@ValorSAASubEtapa,@SemaforoSAASubEtapa,
										@ValorSASTSubEtapa,@SemaforoSASTSubEtapa,@EstatusTiempoSubEtapa,@FechaFinSubEtapa
										,@IndicadoresLlenosSubEtapa,@IndicadoresFaltantesSubEtapa
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

				FETCH cProyectos INTO    @IdProyecto,@NombreProyecto, @IdResponsable, @FechaInicio,	@FehaFin,
										@IdElementoOrganigrama, @IdTipoElementoOrganigrama, @IdTipoProyecto,@SemaforoProyecto

				END

				 

				-- Cierre del cursor

				CLOSE cProyectos

				-- Liberar los recursos

				DEALLOCATE cProyectos
			
			
		
		
FETCH cUbicaciones INTO    @IdUbicacion

END

 

-- Cierre del cursor

CLOSE cUbicaciones

-- Liberar los recursos

DEALLOCATE cUbicaciones

END