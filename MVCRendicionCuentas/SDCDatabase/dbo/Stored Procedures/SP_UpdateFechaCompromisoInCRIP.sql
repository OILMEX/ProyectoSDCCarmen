﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateFechaCompromisoInCRIP]
	-- Add the parameters for the stored procedure here
	@IdConfigRelacionIndicadorProyecto int,
	@FechaCompromisoParaEmpezarAMostrarse date
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE dbo.Config_RelacionIndicadorProyecto set FechaCompromisoParaEmpezarAMostrarse=@FechaCompromisoParaEmpezarAMostrarse
	where IdConfigRelacionIndicadorProyecto=@IdConfigRelacionIndicadorProyecto;
	
END