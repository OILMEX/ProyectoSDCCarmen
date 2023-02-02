-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAll_IndicadoresGlobalesEnProceso]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select 65 as Porcentaje12MPI, 'Rojo' as Color12MPI, 
		   95 as PorcentajeSASP, 'Verde' as ColorSASP,
		   95 as PorcentajeSAA, 'Verde' as ColorSAA,
		   80 as PorcentajeSAST, 'Amarillo' as ColorSAST;
END