//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RendicionCuentasServices.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Predata_ValoresIndicadoresFormula
    {
        public int IdValoresIndicadoresFormula { get; set; }
        public Nullable<double> ValorProgramado { get; set; }
        public Nullable<double> ValorReal { get; set; }
        public Nullable<double> Resultado { get; set; }
        public Nullable<int> IdDataValoresIndicadores { get; set; }
    
        public virtual Data_ValoresIndicador Data_ValoresIndicador { get; set; }
    }
}