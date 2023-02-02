-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_IndicadoresLlenosFaltantesGlobalesMonitoreo]
	@IdResponsable int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	DECLARE @IndicadoresLlenos12MPI int
	DECLARE @IndicadoresFaltantes12MPI int
	DECLARE @IndicadoresLlenosSASP int
	DECLARE @IndicadoresFaltantesSASP int
	DECLARE @IndicadoresLlenosSAA int
	DECLARE @IndicadoresFaltantesSAA int
	DECLARE @IndicadoresLlenosSAST int
	DECLARE @IndicadoresFaltantesSAST int
	declare @r1 int
	declare @r2 int
	declare @r3 int
	declare @r4 int
	 
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    --Datos para 12MPI
	exec @IndicadoresLlenos12MPI = dbo.SP_SelectAllCount_IndicadoresLlenosFaltantes12MPI 1,@IdResponsable
	exec @IndicadoresFaltantes12MPI = dbo.SP_SelectAllCount_IndicadoresLlenosFaltantes12MPI 0,@IdResponsable
	set @r1=@IndicadoresLlenos12MPI+@IndicadoresFaltantes12MPI;
	-----
	--Datos para SASP
	exec @IndicadoresLlenosSASP = dbo.SP_SelectAllCount_IndicadoresLlenosFaltantesSubsistemas 2,1,@IdResponsable
	exec @IndicadoresFaltantesSASP = dbo.SP_SelectAllCount_IndicadoresLlenosFaltantesSubsistemas 2,0,@IdResponsable
	set @r2=@IndicadoresLlenosSASP+@IndicadoresFaltantesSASP;
	-----
	--Datos para SAA
	exec @IndicadoresLlenosSAA = dbo.SP_SelectAllCount_IndicadoresLlenosFaltantesSubsistemas 3,1,@IdResponsable
	exec @IndicadoresFaltantesSAA = dbo.SP_SelectAllCount_IndicadoresLlenosFaltantesSubsistemas 3,0,@IdResponsable
	set @r3=@IndicadoresLlenosSAA+@IndicadoresFaltantesSAA;
	-----
	--Datos para SAST
	exec @IndicadoresLlenosSAST = dbo.SP_SelectAllCount_IndicadoresLlenosFaltantesSubsistemas 4,1,@IdResponsable
	exec @IndicadoresFaltantesSAST = dbo.SP_SelectAllCount_IndicadoresLlenosFaltantesSubsistemas 4,0,@IdResponsable
	set @r4=@IndicadoresLlenosSAST+@IndicadoresFaltantesSAST;
	
    select @IndicadoresLlenos12MPI as IndicadoresLlenos, @r1 as IndicadoresFaltantes, 1 as IdSubsistema, (@r1-@IndicadoresLlenos12MPI) as Pendientes
	union 
	select @IndicadoresLlenosSASP as IndicadoresLlenos, @r2 as IndicadoresFaltantes, 2 as IdSubsistema, (@r2-@IndicadoresLlenosSASP) as Pendientes
	union 
	select @IndicadoresLlenosSAA as IndicadoresLlenos, @r3 as IndicadoresFaltantes, 3 as IdSubsistema, (@r3-@IndicadoresLlenosSAA) as Pendientes
	union 
	select @IndicadoresLlenosSAST as IndicadoresLlenos, @r4 as IndicadoresFaltantes, 4 as IdSubsistema, (@r4-@IndicadoresLlenosSAST) as Pendientes
	
	
END