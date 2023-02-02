-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertSemaforosdeProyecto]
	-- Add the parameters for the stored procedure here
	@IdProyecto int
AS
BEGIN
	DECLARE @Porcentaje int
	DECLARE @Semaforo nvarchar(50)
	DECLARE @IdPorcentajesProyecto int
	DECLARE @TablaPivote TABLE (ValorSaneado int, IdSubsistema int);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO @TablaPivote (ValorSaneado, IdSubsistema)
	(SELECT 
	ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,dbo.fct_DevuelveIdProximaRevision(cir.IdConfigIndicadorResponsable),i.TipoFrecuencia)),i.Meta),0),
	e.IdSubsistema
	from 
	Config_IndicadorResponsable cir, 
	tbl_Indicadores i, 
	Config_RelacionIndicadorProyecto crp, 
	Config_SubEtapaIndicador csi,
	Config_FechasSubEtapasProyecto cfsp,
	tbl_Elementos e
	where 
	cir.IdIndicador=i.IdIndicador 
	and cir.IdConfigRelacionIndicadorProyecto=crp.IdConfigRelacionIndicadorProyecto
	and crp.IdConfigSubEtapaIndicador=csi.IdConfigSubEtapaIndicador
	and csi.IdSubEtapa = cfsp.IdSubEtapa
	and i.IdElemento=e.IdElemento
	and cfsp.EstatusTiempo='En Proceso'
	and crp.IdProyecto=@IdProyecto
	and cir.IdConfigRelacionIndicadorProyecto is not null)
	
	select 
	@Porcentaje=ISNULL(AVG(ValorSaneado),0),
	@Semaforo= dbo.fct_DevuelveSemaforoIndicador(AVG(ValorSaneado),'Seguimiento',100) from @TablaPivote where IdSubsistema=2;
	
	select @IdPorcentajesProyecto=IdPorcentajesProyecto from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=2;
	IF @IdPorcentajesProyecto IS NOT NULL
	BEGIN
	UPDATE RunTime_PorcentajesProyecto SET Porcentaje=@Porcentaje, Semaforo=@Semaforo where IdPorcentajesProyecto=@IdPorcentajesProyecto;
	END
	ELSE
	BEGIN
	Insert into RunTime_PorcentajesProyecto (IdProyecto, IdSubsistema, Porcentaje, Semaforo) VALUES (@IdProyecto, 2, @Porcentaje, @Semaforo)
	END
	
	select 
	@Porcentaje=ISNULL(AVG(ValorSaneado),0),
	@Semaforo= dbo.fct_DevuelveSemaforoIndicador(AVG(ValorSaneado),'Seguimiento',100) from @TablaPivote where IdSubsistema=3;
	
	select @IdPorcentajesProyecto=IdPorcentajesProyecto from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=3;
	
	IF @IdPorcentajesProyecto IS NOT NULL
	BEGIN
	UPDATE RunTime_PorcentajesProyecto SET Porcentaje=@Porcentaje, Semaforo=@Semaforo where IdPorcentajesProyecto=@IdPorcentajesProyecto;
	END
	ELSE
	BEGIN
	Insert into RunTime_PorcentajesProyecto (IdProyecto, IdSubsistema, Porcentaje, Semaforo) VALUES (@IdProyecto, 3, @Porcentaje, @Semaforo)
	END
	
	
	select 
	@Porcentaje=ISNULL(AVG(ValorSaneado),0),
	@Semaforo= dbo.fct_DevuelveSemaforoIndicador(AVG(ValorSaneado),'Seguimiento',100) from @TablaPivote where IdSubsistema=4;
	
	select @IdPorcentajesProyecto=IdPorcentajesProyecto from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=4;
	
	IF @IdPorcentajesProyecto IS NOT NULL
	BEGIN
	UPDATE RunTime_PorcentajesProyecto SET Porcentaje=@Porcentaje, Semaforo=@Semaforo where IdPorcentajesProyecto=@IdPorcentajesProyecto;
	END
	ELSE
	BEGIN
	Insert into RunTime_PorcentajesProyecto (IdProyecto, IdSubsistema, Porcentaje, Semaforo) VALUES (@IdProyecto, 4, @Porcentaje, @Semaforo)
	END
	
	---para 12MPI
	select @Porcentaje=AVG(v.ValorSaneado),
	 @Semaforo=dbo.fct_DevuelveSemaforoIndicador(AVG(v.ValorSaneado),'Seguimiento',100) from (SELECT cir.IdConfigIndicadorResponsable, i.Prefijo, i.DescripcionCorta, '' as EstatusLlenado , i.TipoIndicador,
	i.TipoFrecuencia, i.TipoCalculo, i.Meta, cir.IdResponsable,
	dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,dbo.fct_DevuelveIdProximaRevision(cir.IdConfigIndicadorResponsable),i.TipoFrecuencia) as IdDataValoresIndicador,
	ISNULL(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,dbo.fct_DevuelveIdProximaRevision(cir.IdConfigIndicadorResponsable),i.TipoFrecuencia)), 0) as Valor,
	ISNULL(dbo.fct_DevuelveValorRealConsiderandoMeta(dbo.fct_DevuelveValor(dbo.fct_DevuelveIdValorxRIResponsable(cir.IdConfigIndicadorResponsable,dbo.fct_DevuelveIdProximaRevision(cir.IdConfigIndicadorResponsable),i.TipoFrecuencia)), i.Meta),0) as ValorSaneado,
	i.CheckReqSoporte,
	i.CheckReqComentario,
	i.CheckSoporte,
	i.CheckComentarios
	from Config_IndicadorResponsable cir, tbl_Indicadores i 
	where 
	cir.IdIndicador=i.IdIndicador
	and cir.IdConfigRelacionIndicadorProyecto is null
	 ) v
	 
	 select @IdPorcentajesProyecto=IdPorcentajesProyecto from RunTime_PorcentajesProyecto where IdProyecto=@IdProyecto and IdSubsistema=1;
	
	IF @IdPorcentajesProyecto IS NOT NULL
	BEGIN
	UPDATE RunTime_PorcentajesProyecto SET Porcentaje=@Porcentaje, Semaforo=@Semaforo where IdPorcentajesProyecto=@IdPorcentajesProyecto;
	END
	ELSE
	BEGIN
	Insert into RunTime_PorcentajesProyecto (IdProyecto, IdSubsistema, Porcentaje, Semaforo) VALUES (@IdProyecto, 1, @Porcentaje, @Semaforo)
	END

END