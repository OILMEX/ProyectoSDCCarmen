-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertIndicadoresProyecto_v2]
	-- Add the parameters for the stored procedure here
	@IdConfigIndicadorResponsable int --será el idproyecto en la prueba
AS
BEGIN

SET NOCOUNT ON;
			
			 
		
		
		-- Declaración del cursor
				DECLARE @IdConfigRelacionIndicadorProyecto int
				DECLARE @IdProyecto int
				DECLARE @IdUbicacion int
				
				select @IdConfigRelacionIndicadorProyecto=IdConfigRelacionIndicadorProyecto from Config_IndicadorResponsable where IdConfigIndicadorResponsable=@IdConfigIndicadorResponsable;
				select @IdProyecto=IdProyecto from Config_RelacionIndicadorProyecto where IdConfigRelacionIndicadorProyecto=@IdConfigRelacionIndicadorProyecto;
				select @IdUbicacion=IdUbicacion from tbl_Proyectos where IdProyecto=@IdProyecto;
				
				DECLARE @Valor12MPI int
				DECLARE @Semaforo12MPI nvarchar(50)
				--
				DECLARE @ValorSASP int
				DECLARE @SemaforoSASP nvarchar(50)
				--
				DECLARE @ValorSAA int
				DECLARE @SemaforoSAA nvarchar(50)
				--
				DECLARE @ValorSAST int
				DECLARE @SemaforoSAST nvarchar(50)
				-- SET NOCOUNT ON added to prevent extra result sets from
				-- interfering with SELECT statements.
				
				SET @Valor12MPI=NULL
				SET @Semaforo12MPI=NULL
				SET @ValorSASP=NULL
				SET @SemaforoSASP=NULL
				SET @ValorSAA=NULL
				SET @SemaforoSAA=NULL
				SET @ValorSAST=NULL
				SET @SemaforoSAST=NULL
				

				-- Insert statements for procedure here
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
				(IdConfigIndicadorResponsable,
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
				exec dbo.SP_VistaCargaIndicadoresProyectosByProyecto @IdProyecto
				
				declare @BusquedaProyecto int
				
				select @BusquedaProyecto=COUNT(*) from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto
				
				exec @Valor12MPI=  SP_SelectPromedioRT12MPIByUbicacion @IdUbicacion
				
				if @Valor12MPI = -1
				set @Valor12MPI = NULL
				
				IF @Valor12MPI is NULL
				set @Semaforo12MPI = NULL
				ELSE
				set @Semaforo12MPI = dbo.fct_DevuelveSemaforoIndicador(@Valor12MPI,'Seguimiento',100)
				
				if @BusquedaProyecto=0 
				begin
					insert into RunTime_PorcentajesProyecto
					(IdProyecto,IdSubsistema,Porcentaje,Semaforo)
					(select  
					@IdProyecto, 
					IdSubsistema,
					(Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))), 
					dbo.fct_DevuelveSemaforoIndicador((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))),'Seguimiento',100)  
					from @FirstTable where IdProyecto=@IdProyecto group by IdSubsistema)
					
					insert into RunTime_PorcentajesProyecto
					(IdProyecto,IdSubsistema,Porcentaje,Semaforo)
					VALUES
					(@IdProyecto,1,@Valor12MPI,@Semaforo12MPI);
					
					
					
				END
				ELSE
				BEGIN
				
				select @ValorSASP=ValorSaneado, @SemaforoSASP=SemaforoSaneado from (select  
				IdSubsistema,
				(Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))) as ValorSaneado, 
				dbo.fct_DevuelveSemaforoIndicador((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))),'Seguimiento',100) as SemaforoSaneado 
				from @FirstTable where IdProyecto=@IdProyecto group by IdSubsistema) v
				where IdSubsistema=2
				
				select @ValorSAA=ValorSaneado, @SemaforoSAA=SemaforoSaneado from (select  
				IdSubsistema,
				(Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))) as ValorSaneado, 
				dbo.fct_DevuelveSemaforoIndicador((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))),'Seguimiento',100) as SemaforoSaneado 
				from @FirstTable where IdProyecto=@IdProyecto group by IdSubsistema) v
				where IdSubsistema=3
				
				select @ValorSAST=ValorSaneado, @SemaforoSAST=SemaforoSaneado from (select  
				IdSubsistema,
				(Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))) as ValorSaneado, 
				dbo.fct_DevuelveSemaforoIndicador((Select AVG(dbo.fct_DevuelveValorRealConsiderandoMeta(Valor,Meta))),'Seguimiento',100) as SemaforoSaneado 
				from @FirstTable where IdProyecto=@IdProyecto group by IdSubsistema) v
				where IdSubsistema=4
				
				UPDATE RunTime_PorcentajesProyecto set Porcentaje=@Valor12MPI,Semaforo=@Semaforo12MPI where IdProyecto=@IdProyecto and IdSubsistema=1
				UPDATE RunTime_PorcentajesProyecto set Porcentaje=@ValorSASP,Semaforo=@SemaforoSASP where IdProyecto=@IdProyecto and IdSubsistema=2
				UPDATE RunTime_PorcentajesProyecto set Porcentaje=@ValorSAA,Semaforo=@SemaforoSAA where IdProyecto=@IdProyecto and IdSubsistema=3
				UPDATE RunTime_PorcentajesProyecto set Porcentaje=@ValorSAST,Semaforo=@SemaforoSAST where IdProyecto=@IdProyecto and IdSubsistema=4
				
				end
				
					declare @PromedioProyecto int
					declare @SemaforoProyecto nvarchar(50)
					
					select @PromedioProyecto=AVG(Porcentaje) from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema>1;
					set @SemaforoProyecto=dbo.fct_DevuelveSemaforoIndicador(@PromedioProyecto,'Seguimiento',100);
					
					update tbl_Proyectos set Semaforo=@SemaforoProyecto where IdProyecto=@IdProyecto;

		-- Lectura de la siguiente fila del cursor
	
END