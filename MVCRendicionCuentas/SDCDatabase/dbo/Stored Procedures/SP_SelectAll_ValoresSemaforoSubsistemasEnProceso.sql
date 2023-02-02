-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_ValoresSemaforoSubsistemasEnProceso] 
	-- Add the parameters for the stored procedure here
	@IdResponsable int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @Valor12MPI int
	declare @ValorSASP int
	declare @ValorSAA int
	declare @ValorSAST int
	declare @ValorSSPA int=0
	declare @Contador int=0;
	
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	exec @Valor12MPI= dbo.SP_SelectPromedioRT12MPI @IdResponsable
	exec @ValorSASP = dbo.SP_SelectPromedioRTSubsistemas 2, @IdResponsable
	exec @ValorSAA = dbo.SP_SelectPromedioRTSubsistemas 3, @IdResponsable
	exec @ValorSAST = dbo.SP_SelectPromedioRTSubsistemas 4, @IdResponsable
	
	
	
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
		
	
	
	
	
	
 select * from (	
	select @Valor12MPI as Porcentaje, dbo.fct_DevuelveSemaforoIndicador(@Valor12MPI,'Seguimiento',100) as Semaforo, 1 as EstatusMejora, 1 as IdSubsistema, '12MPI' as NombreSubsistema
	union
	select @ValorSASP as Porcentaje, dbo.fct_DevuelveSemaforoIndicador(@ValorSASP,'Seguimiento',100) as Semaforo, 2 as EstatusMejora, 2 as IdSubsistema, 'SASP' as NombreSubsistema
	union
	select @ValorSAA as Porcentaje, dbo.fct_DevuelveSemaforoIndicador(@ValorSAA,'Seguimiento',100) as Semaforo,3 as EstatusMejora, 3 as IdSubsistema, 'SAA' as NombreSubsistema
	union
	select @ValorSAST as Porcentaje, dbo.fct_DevuelveSemaforoIndicador(@ValorSAST,'Seguimiento',100) as Semaforo,1 as EstatusMejora, 4 as IdSubsistema, 'SAST' as NombreSubsistema
	union
	select @ValorSSPA as Porcentaje, dbo.fct_DevuelveSemaforoIndicador(@ValorSSPA,'Seguimiento',100) as Semaforo,1 as EstatusMejora, 5 as IdSubsistema, 'SSPA' as NombreSubsistema
	) v
	order by IdSubsistema asc
	
END