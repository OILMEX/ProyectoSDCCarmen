-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_BusquedaPuestoBeforeInsert]
	-- Add the parameters for the stored procedure here
	@IdElemento int, 
	@IdTipoElemento int,
	@NombrePuesto nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT * from tbl_PuestoOrganigrama
	where NombrePuesto=@NombrePuesto and IdTipoElementoOrganigrama=@IdTipoElemento and IdElemento=@IdElemento
	and Estatus=1

	
END