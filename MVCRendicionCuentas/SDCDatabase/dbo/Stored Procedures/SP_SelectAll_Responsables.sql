-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_Responsables] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select r.*, u.Ubicacion as NombreArea, p.NombrePuesto from dbo.tbl_Responsables r,  dbo.tbl_PuestoOrganigrama p, tbl_Ubicaciones u
	where 
	r.IdUbicacion=u.idUbicacion
	and r.IdPuesto=p.idPuestoOrganigrama  and r.Estatus=1
order by r.NombreResponsable asc;

END