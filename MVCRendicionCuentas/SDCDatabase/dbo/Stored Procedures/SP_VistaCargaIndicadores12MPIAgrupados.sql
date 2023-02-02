-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_VistaCargaIndicadores12MPIAgrupados] 
	-- Add the parameters for the stored procedure here
	@IdResponsable int,
	@IdUbicacionPar int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @FirstTable TABLE (
	IdConfigIndicadorResponsable INT,
	Semaforo nvarchar(100),
	Prefijo nvarchar(300), 
	DescripcionCorta nvarchar(MAX), 
	EstatusLlenado  nvarchar(100),
	TipoIndicador nvarchar(100), 
	TipoFrecuencia int, 
	TipoCalculo nvarchar(50), 
	Meta int,
	IdDataValoresIndicadores int,
	Valor int,
	CheckSoporte bit, 
	CheckComentarios bit,
	SemaforoSoportes nvarchar(100), 
	SemaforoComentarios nvarchar(100), 
	IdElemento int, 
	IdIndicador int,
	NombreElemento nvarchar(300), 
	DescripcionElemento nvarchar(500),
	IdResponsable int
	)
	
	insert into @FirstTable 
				(IdConfigIndicadorResponsable,
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
				DescripcionElemento,
				IdResponsable
				) 
				exec dbo.SP_VistaCargaIndicadores12MPIxReponsableSubadministrador @IdResponsable, @IdUbicacionPar
				
	
				DECLARE @Rol varchar(100)
				DECLARE @IdUbicacion int
				
				SELECT @IdUbicacion=IdUbicacion, @Rol=Rol from tbl_Responsables where idResponsable=@IdResponsable
				
				
				
				IF @IdResponsable=0
					SET @Rol='Administrador'
	
	
				IF @Rol='Administrador' 
					BEGIN
					SET @IdUbicacion= @IdUbicacionPar
					
						select 
						AVG(ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(IdConfigIndicadorResponsable,null,TipoFrecuencia)),Meta),0)) as ValorProcesado, 
						dbo.fct_DevuelveSemaforoIndicador(AVG(ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(IdConfigIndicadorResponsable,null,TipoFrecuencia)),Meta),0)),TipoIndicador,Meta) as Semaforo,
						Prefijo,
						DescripcionCorta,
						Meta,
						IdIndicador,
						IdElemento,
						TipoIndicador,
						TipoFrecuencia,
						TipoCalculo,
						NombreElemento,
						DescripcionElemento,
						0 as IdUbicacion
						from @FirstTable x, tbl_Responsables r
						where 
						((@IdUbicacion IS NULL AND	
						r.IdUbicacion=r.IdUbicacion)
						 OR 
						 (@IdUbicacion IS NOT NULL AND
						 r.IdUbicacion = @IdUbicacion	))
						and x.IdResponsable=r.IdResponsable
						group by 
						Prefijo,
						Meta,
						DescripcionCorta,
						IdIndicador,
						IdElemento,
						TipoIndicador,
						TipoFrecuencia,
						TipoCalculo,
						NombreElemento,
						DescripcionElemento
						order by IdElemento asc
					END
					ELSE
					BEGIN
							select 
						AVG(ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(IdConfigIndicadorResponsable,null,TipoFrecuencia)),Meta),0)) as ValorProcesado, 
						dbo.fct_DevuelveSemaforoIndicador(AVG(ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(IdConfigIndicadorResponsable,null,TipoFrecuencia)),Meta),0)),TipoIndicador,Meta) as Semaforo,
						Prefijo,
						DescripcionCorta,
						Meta,
						IdIndicador,
						IdElemento,
						TipoIndicador,
						TipoFrecuencia,
						TipoCalculo,
						NombreElemento,
						DescripcionElemento,
						r.IdUbicacion
						from @FirstTable x, tbl_Responsables r
						where x.IdResponsable=r.IdResponsable  and
						 x.IdResponsable in (select IdResponsable from tbl_Responsables where IdUbicacion=@IdUbicacion)
						 group by 
						Prefijo,
						Meta,
						DescripcionCorta,
						IdIndicador,
						IdElemento,
						TipoIndicador,
						TipoFrecuencia,
						TipoCalculo,
						NombreElemento,
						DescripcionElemento,
						r.IdUbicacion
						order by IdElemento asc
					END
END