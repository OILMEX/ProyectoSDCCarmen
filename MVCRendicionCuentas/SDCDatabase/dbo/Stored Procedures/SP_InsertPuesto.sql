-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_InsertPuesto]
	-- Add the parameters for the stored procedure here
	@IdElemento int, 
	@IdTipoElemento int,
	@NombrePuesto nvarchar(500),
	@Clave nvarchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into tbl_PuestoOrganigrama(NombrePuesto, IdElemento, IdTipoElementoOrganigrama, Estatus, Clave) VALUES (@NombrePuesto, @IdTipoElemento, @IdElemento, 1, @Clave);
	
END