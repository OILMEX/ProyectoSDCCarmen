using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RendicionCuentasServices.Model;

namespace RendicionCuentasServices.DTO
{
    public class EntProgramaAsociadoIndicador:SP_Select_ProgramaAsociadoxIndicador_Result
    {
        public string FechaCreacionStr { get; set; }
        public string FechaActualizacionStr { get; set; }
    }
}
