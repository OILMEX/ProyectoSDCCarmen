-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateEstatusTiempoSubEtapaProyecto]
	-- Add the parameters for the stored procedure here
	@IdProyecto int
AS
BEGIN
	
	declare @CounElementosEnProceso int
	declare @CountTotalElementos int
	declare @CountTotalElementosConcluidos int 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Concluidos
	Update Config_FechasSubEtapasProyecto set EstatusTiempo='Concluido' 
	where IdProyecto=@IdProyecto and Estatus=1
	and FechaFin<GETDATE();
	
	UPDATE 
	Config_FechasSubEtapasProyecto 
	SET EstatusTiempo='En Proceso'
	where 
	IdProyecto=@IdProyecto 
	and FechaInicio<=GETDATE()
	and FechaFin>=GETDATE()
	and Estatus=1
	
	select @CounElementosEnProceso=COUNT(*) from Config_FechasSubEtapasProyecto 
	where EstatusTiempo='En Proceso' and Estatus=1 and IdProyecto=@IdProyecto
	
	
	
	IF @CounElementosEnProceso>0
	begin
	
	-- Declaracion de variables para el cursor

				DECLARE @IdSubEtapa int

				-- Declaración del cursor

				DECLARE cSubEtapas CURSOR FOR

				SELECT  IdSubEtapa 

				FROM Config_FechasSubEtapasProyecto where Estatus=1 and EstatusTiempo='En Proceso' and IdProyecto=@IdProyecto

				-- Apertura del cursor

				OPEN cSubEtapas

				-- Lectura de la primera fila del cursor

				FETCH cSubEtapas INTO    @IdSubEtapa

				 

				WHILE (@@FETCH_STATUS = 0 )

				BEGIN
				
					DECLARE @IdEtapa int
					
					select @IdEtapa=IdEtapa from tbl_SubEtapas where IdSubEtapa=@IdSubEtapa

					UPDATE Config_FechasEtapasProyecto set EstatusTiempo='En Proceso' 
					WHERE IdProyecto=@IdProyecto and IdEtapa=@IdEtapa 

				-- Lectura de la siguiente fila del cursor

				FETCH cSubEtapas INTO    @IdSubEtapa

				END

				 

				-- Cierre del cursor

				CLOSE cSubEtapas

				-- Liberar los recursos

				DEALLOCATE cSubEtapas

		
		UPDATE tbl_Proyectos set EstatusTiempo='En Proceso' where IdProyecto=@IdProyecto;
	end
	
	select @CountTotalElementos=COUNT(*) from Config_FechasSubEtapasProyecto 
		where IdProyecto=@IdProyecto and Estatus=1;
	select @CountTotalElementosConcluidos=COUNT(*) from Config_FechasSubEtapasProyecto 
		where IdProyecto=@IdProyecto and EstatusTiempo='Concluido' and Estatus=1;
	
	IF @CountTotalElementos>0
		begin
		if @CountTotalElementosConcluidos=@CountTotalElementos
		begin
			update tbl_Proyectos set EstatusTiempo='Concluido' where IdProyecto=@IdProyecto;
		end
		
		
		end
	
	


END