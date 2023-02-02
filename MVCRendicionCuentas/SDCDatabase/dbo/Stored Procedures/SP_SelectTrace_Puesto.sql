-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectTrace_Puesto]
	-- Add the parameters for the stored procedure here
	@IdPuesto int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @IdSubdireccion int
	DECLARE @NombreSubdireccion nvarchar(500)
	DECLARE @IdGerencia int
	DECLARE @NombreGerencia nvarchar(500)
	DECLARE @IdCoordinacion int
	DECLARE @NombreCoordinacion nvarchar(500)
	DECLARE @IdSuperintendencia int
	DECLARE @NombreSuperintendencia nvarchar(500)
	DECLARE @IdElemento int
	DECLARE @TipoElemento int
	
	select @IdElemento=IdElemento, @TipoElemento=IdTipoElementoOrganigrama from tbl_PuestoOrganigrama where idPuestoOrganigrama=@IdPuesto;
	
	IF @TipoElemento=1
	BEGIN
	-- Add the T-SQL statements to compute the return value here
	SET @IdSubdireccion=@IdElemento;
	SELECT @NombreSubdireccion=NombreSubdireccion from tbl_Subdirecciones where IdSubdireccion=@IdElemento
	END
	ELSE IF @TipoElemento=2
	BEGIN
	select @IdSubdireccion=IdSubdireccion from tbl_Gerencias where IdGerencia=@IdElemento
	SET    @IdGerencia=@IdElemento
	SELECT @NombreSubdireccion=NombreSubdireccion from tbl_Subdirecciones where IdSubdireccion=@IdSubdireccion
	SELECT @NombreGerencia=NombreGerencia from tbl_Gerencias where IdGerencia=@IdGerencia
	END
	ELSE IF @TipoElemento=3
	BEGIN
	select @IdGerencia=IdGerencia from tbl_Coordinaciones where IdCoordinacion=@IdElemento
	select @IdSubdireccion=IdSubdireccion from tbl_Gerencias where IdGerencia=@IdGerencia
	SET    @IdCoordinacion=@IdElemento
	SELECT @NombreSubdireccion=NombreSubdireccion from tbl_Subdirecciones where IdSubdireccion=@IdSubdireccion
	SELECT @NombreGerencia=NombreGerencia from tbl_Gerencias where IdGerencia=@IdGerencia
	SELECT @NombreCoordinacion=NombreCoordinacion from tbl_Coordinaciones where IdCoordinacion=@IdCoordinacion
	END
	ELSE IF @TipoElemento=4
	BEGIN
	select @IdCoordinacion=IdCoordinacion from tbl_Superintendencias where IdSuperintendencia=@IdElemento
	select @IdGerencia=IdGerencia from tbl_Coordinaciones where IdCoordinacion=@IdCoordinacion
	select @IdSubdireccion=IdSubdireccion from tbl_Gerencias where IdGerencia=@IdGerencia
	set @IdSuperintendencia=@IdElemento
	SELECT @NombreSubdireccion=NombreSubdireccion from tbl_Subdirecciones where IdSubdireccion=@IdSubdireccion
	SELECT @NombreGerencia=NombreGerencia from tbl_Gerencias where IdGerencia=@IdGerencia
	SELECT @NombreCoordinacion=NombreCoordinacion from tbl_Coordinaciones where IdCoordinacion=@IdCoordinacion
	SELECT @NombreSuperintendencia=NombreSuperIntendencia from tbl_Superintendencias where IdSuperintendencia=@IdSuperintendencia
	END
	
	select @IdSubdireccion as IdSubdireccion, @NombreSubdireccion as NombreSubdireccion, 
	@IdGerencia as IdGerencia, @NombreGerencia as NombreGerencia,
	@IdCoordinacion as IdCoordinacion, @NombreCoordinacion as NombreCoordinacion,
	@IdSuperintendencia as IdSuperintendencia, @NombreSuperintendencia as NombreSuperintendencia
	
	
END