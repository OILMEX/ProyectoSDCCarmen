-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectOne_Ubicacion]
	-- Add the parameters for the stored procedure here
	@IdUbicacion int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select u.idUbicacion, u.Ubicacion, u.Estatus, u.FechaActualizacion, u.FechaCreacion, r.NombreResponsable as UsuarioActualizacion, r2.NombreResponsable as UsuarioCreacion from tbl_Ubicaciones u, tbl_Responsables r, tbl_Responsables r2 
    where u.UsuarioActualizacion=r.idResponsable and u.UsuarioCreacion=r2.idResponsable and u.idUbicacion=@IdUbicacion
END