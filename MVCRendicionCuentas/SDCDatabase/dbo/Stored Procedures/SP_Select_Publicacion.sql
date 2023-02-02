-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	El Tipo puede ser Produccion / Prueba
-- =============================================
CREATE PROCEDURE [dbo].[SP_Select_Publicacion]
	-- Add the parameters for the stored procedure here
	@Tipo nvarchar(50) --Produccion o Prueba
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select * from Dashboard_Publicaciones where TipoPublicacion=@Tipo;
	
END