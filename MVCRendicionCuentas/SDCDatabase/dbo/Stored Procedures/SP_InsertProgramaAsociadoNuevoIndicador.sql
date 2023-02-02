-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertProgramaAsociadoNuevoIndicador]
	-- Add the parameters for the stored procedure here
	@IdIndicador int,
	@IdUsuario int

	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into Rel_ProgramaAsociadoIndicador (IdIndicador, UsuarioCreacion, UsuarioActualizacion, FechaCreacion, FechaActualizacion) Values (@IdIndicador,@IdUsuario,@IdUsuario,GETDATE(),GETDATE());
	
END