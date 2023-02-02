-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_MonitoreoIndicadoresResponsables12MPI]
	@IdResponsable int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
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
	DescripcionElemento,
	IdResponsable)
	exec dbo.SP_VistaCargaIndicadores12MPIxReponsable 0
	
	DECLARE @IdUbicacion int
	DECLARE @Rol varchar(100)
				
	SELECT @IdUbicacion=IdUbicacion, @Rol=Rol from tbl_Responsables where idResponsable=@IdResponsable
				
	IF @IdResponsable=0
		SET @Rol='Administrador'
				
	IF @Rol='Administrador' 
	BEGIN
	
		select IdConfigIndicadorResponsable,
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
		s.IdResponsable,
		r.NombreResponsable
		 from @SecondTable s, tbl_Responsables r  where s.IdResponsable=r.idResponsable and Valor is null
	 END
	 ELSE
	 BEGIN
			select IdConfigIndicadorResponsable,
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
		s.IdResponsable,
		r.NombreResponsable
		 from @SecondTable s, tbl_Responsables r  
		 where s.IdResponsable=r.idResponsable and Valor is null
		 and r.IdUbicacion=@IdUbicacion
	 END
	
END