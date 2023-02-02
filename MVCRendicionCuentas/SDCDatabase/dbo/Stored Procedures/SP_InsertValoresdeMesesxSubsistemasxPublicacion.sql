-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertValoresdeMesesxSubsistemasxPublicacion]
	-- Add the parameters for the stored procedure here
	@IdPublicacion int, @IdUbicacion int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @MesActual int
	declare @anioactual int
	SET @MesActual=datepart(month,getdate());
	SET @AnioActual=datepart(YEAR,getdate());
	
	DECLARE @TablaMeses TABLE (Mes int);
	
	insert into @TablaMeses(Mes)VALUES(1);
	insert into @TablaMeses(Mes)VALUES(2);
	insert into @TablaMeses(Mes)VALUES(3);
	insert into @TablaMeses(Mes)VALUES(4);
	insert into @TablaMeses(Mes)VALUES(5);
	insert into @TablaMeses(Mes)VALUES(6);
	insert into @TablaMeses(Mes)VALUES(7);
	insert into @TablaMeses(Mes)VALUES(8);
	insert into @TablaMeses(Mes)VALUES(9);
	insert into @TablaMeses(Mes)VALUES(10);
	insert into @TablaMeses(Mes)VALUES(11);
	insert into @TablaMeses(Mes)VALUES(12);
	
	-- Declaracion de variables para el cursor

			DECLARE @MesCursor int


			-- Declaración del cursor
			
			IF @MesActual>1
			begin

			DECLARE cMeses CURSOR FOR

			SELECT  Mes	FROM @TablaMeses 

			-- Apertura del cursor

			OPEN cMeses

			-- Lectura de la primera fila del cursor

			FETCH cMeses INTO    @MesCursor

			 

			WHILE (@@FETCH_STATUS = 0 )

			BEGIN
			
			DECLARE @Valor12MPI int= NULL
			DECLARE @Semaforo12MPI nvarchar(50)= NULL
			
			--
			DECLARE @ValorSASP int= NULL
			DECLARE @SemaforoSASP nvarchar(50)= NULL
			
			--
			DECLARE @ValorSAA int= NULL
			DECLARE @SemaforoSAA nvarchar(50)= NULL
			
			--
			DECLARE @ValorSAST int= NULL
			DECLARE @SemaforoSAST nvarchar(50)= NULL
			
			--
			DECLARE @ValorSSPA int= NULL
			DECLARE @SemaforoSSPA nvarchar(50)= NULL
			Declare @idPublicacionCursor int= NULL
			
			set @idPublicacionCursor=NULL 
			
			select top 1 @idPublicacionCursor=IdPublicacion from Dashboard_Publicaciones 
			where TipoPublicacion='Produccion' and MONTH(FechaPublicacion)=@MesCursor and YEAR(FechaPublicacion)=@anioactual
			and IdUbicacion=@IdUbicacion
			order by FechaPublicacion desc
			
			set @Valor12MPI=NULL;
			set @Semaforo12MPI=NULL
			SET @ValorSASP=NULL;
			SET @SemaforoSASP=NULL;
			SET @ValorSAA=NULL;
			SET @SemaforoSAA=NULL;
			SET @ValorSAST=NULL;
			SET @SemaforoSAST=NULL;
			SET @ValorSSPA=NULL;
			SET @SemaforoSSPA=NULL;
			
			select @Valor12MPI=Valor, @Semaforo12MPI=Semaforo from Dashboard_PublicacionAvanceSubsistema where IdSubsistema=1 and IdPublicacion=@idPublicacionCursor;
			select @ValorSASP=Valor, @SemaforoSASP=Semaforo from Dashboard_PublicacionAvanceSubsistema where IdSubsistema=2 and IdPublicacion=@idPublicacionCursor;
			select @ValorSAA=Valor, @SemaforoSAA=Semaforo from Dashboard_PublicacionAvanceSubsistema where IdSubsistema=3 and IdPublicacion=@idPublicacionCursor;
			select @ValorSAST=Valor, @SemaforoSAST=Semaforo from Dashboard_PublicacionAvanceSubsistema where IdSubsistema=4 and IdPublicacion=@idPublicacionCursor;
			select @ValorSSPA=Valor, @SemaforoSSPA=Semaforo from Dashboard_PublicacionAvanceSubsistema where IdSubsistema=5 and IdPublicacion=@idPublicacionCursor;
			
			if @MesActual<>@MesCursor
			begin
			
			
			insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
			VALUES (1,@IdPublicacion,@MesCursor,@Valor12MPI,@Semaforo12MPI)
			--
			insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
			VALUES (2,@IdPublicacion,@MesCursor,@ValorSASP,@SemaforoSASP)
			--
			insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
			VALUES (3,@IdPublicacion,@MesCursor,@ValorSAA,@SemaforoSAA)
			--
			insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
			VALUES (4,@IdPublicacion,@MesCursor,@ValorSAST,@SemaforoSAST)
			--
			insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
			VALUES (5,@IdPublicacion,@MesCursor,@ValorSSPA,@SemaforoSSPA)
			
			
			end

			-- Lectura de la siguiente fila del cursor

			FETCH cMeses INTO    @MesCursor

			END

			 

			-- Cierre del cursor

			CLOSE cMeses

			-- Liberar los recursos

			DEALLOCATE cMeses
		end
		else
		begin
		
		
			DECLARE @Valor12MPI_ int= NULL
			DECLARE @Semaforo12MPI_ nvarchar(50)= NULL
			
			--
			DECLARE @ValorSASP_ int= NULL
			DECLARE @SemaforoSASP_ nvarchar(50)= NULL
			
			--
			DECLARE @ValorSAA_ int= NULL
			DECLARE @SemaforoSAA_ nvarchar(50)= NULL
			
			--
			DECLARE @ValorSAST_ int= NULL
			DECLARE @SemaforoSAST_ nvarchar(50)= NULL
			
			--
			DECLARE @ValorSSPA_ int= NULL
			DECLARE @SemaforoSSPA_ nvarchar(50)= NULL
			
			Declare @idPublicacionCursor_ int = NULL
			
			select top 1 @idPublicacionCursor_=IdPublicacion from Dashboard_Publicaciones 
			where TipoPublicacion='Produccion' and MONTH(FechaPublicacion)=12 and 
			YEAR(FechaPublicacion)=(@anioactual-1)
			and IdUbicacion=@IdUbicacion
			 order by FechaPublicacion desc
			
			insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
			VALUES (1,@IdPublicacion,1,@Valor12MPI_,@Semaforo12MPI_)
			--
			insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
			VALUES (2,@IdPublicacion,1,@ValorSASP_,@SemaforoSASP_)
			--
			insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
			VALUES (3,@IdPublicacion,1,@ValorSAA_,@SemaforoSAA_)
			--
			insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
			VALUES (4,@IdPublicacion,1,@ValorSAST_,@SemaforoSAST_)
			--
			insert into Dashboard_PublicacionTabular (IdSubsistema,IdPublicacion,Mes,Valor,Semaforo)
			VALUES (5,@IdPublicacion,1,@ValorSSPA_,@SemaforoSSPA_)
		
		end
			
	
	
END