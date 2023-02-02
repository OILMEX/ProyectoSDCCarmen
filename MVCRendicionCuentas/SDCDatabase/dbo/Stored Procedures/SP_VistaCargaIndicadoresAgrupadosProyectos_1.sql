-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_VistaCargaIndicadoresAgrupadosProyectos
	-- Add the parameters for the stored procedure here
	@IdResponsable int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @FirstTable TABLE (IdConfigIndicadorResponsable INT,Prefijo nvarchar(300), 
				DescripcionCorta nvarchar(MAX), EstatusLlenado  nvarchar(100), TipoIndicador nvarchar(100), TipoFrecuencia int, 
				TipoCalculo nvarchar(50),	Meta int, IdProyecto int,IdEtapa int, IdSubEtapa int, IdDataValoresIndicadores int, Valor int,
				CheckSoporte bit, CheckComentarios bit, IdElemento int, IdIndicador int, 
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
				exec dbo.SP_VistaCargaIndicadoresProyectos 0
				
				DECLARE @IdUbicacion int
				DECLARE @Rol varchar(100)
				
				SELECT @IdUbicacion=IdUbicacion, @Rol=Rol from tbl_Responsables where idResponsable=@IdResponsable
				
				IF @IdResponsable=0
					SET @Rol='Administrador'
				
				
				IF @Rol='Administrador' 
					BEGIN
						select 
						AVG(ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(IdConfigIndicadorResponsable,dbo.fct_DevuelveIdProximaRevision(IdConfigIndicadorResponsable),TipoFrecuencia)),Meta),0)) as ValorProcesado, 
						dbo.fct_DevuelveSemaforoIndicador(AVG(ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(IdConfigIndicadorResponsable,dbo.fct_DevuelveIdProximaRevision(IdConfigIndicadorResponsable),TipoFrecuencia)),Meta),0)),TipoIndicador,Meta) as Semaforo,
						Prefijo,
						DescripcionCorta,
						Meta,
						IdIndicador,
						IdElemento,
						IdSubsistema,
						TipoIndicador,
						TipoFrecuencia,
						TipoCalculo,
						x.IdProyecto,
						IdEtapa,
						IdSubEtapa,
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
						EstatusSubEtapa,
						p.IdUbicacion
						from @FirstTable x, tbl_Proyectos p
						where x.IdProyecto=p.IdProyecto
						group by 
						Prefijo,
						Meta,
						DescripcionCorta,
						IdIndicador,
						IdElemento,
						IdSubsistema,
						TipoIndicador,
						TipoFrecuencia,
						TipoCalculo,
						x.IdProyecto,
						IdEtapa,
						IdSubEtapa,
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
						EstatusSubEtapa,
						p.IdUbicacion
						order by IdProyecto asc
					END
					ELSE
					BEGIN
							select 
						AVG(ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(IdConfigIndicadorResponsable,dbo.fct_DevuelveIdProximaRevision(IdConfigIndicadorResponsable),TipoFrecuencia)),Meta),0)) as ValorProcesado, 
						dbo.fct_DevuelveSemaforoIndicador(AVG(ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(IdConfigIndicadorResponsable,dbo.fct_DevuelveIdProximaRevision(IdConfigIndicadorResponsable),TipoFrecuencia)),Meta),0)),TipoIndicador,Meta) as Semaforo,
						Prefijo,
						DescripcionCorta,
						Meta,
						IdIndicador,
						IdElemento,
						IdSubsistema,
						TipoIndicador,
						TipoFrecuencia,
						TipoCalculo,
						x.IdProyecto,
						IdEtapa,
						IdSubEtapa,
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
						EstatusSubEtapa,
						p.IdUbicacion
						from @FirstTable x, tbl_Proyectos p
						where x.IdProyecto=p.IdProyecto  and
						 x.IdProyecto in (select IdProyecto from tbl_Proyectos where IdUbicacion=@IdUbicacion)
						group by 
						Prefijo,
						Meta,
						DescripcionCorta,
						IdIndicador,
						IdElemento,
						IdSubsistema,
						TipoIndicador,
						TipoFrecuencia,
						TipoCalculo,
						x.IdProyecto,
						IdEtapa,
						IdSubEtapa,
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
						EstatusSubEtapa,
						p.IdUbicacion
						
					END
END