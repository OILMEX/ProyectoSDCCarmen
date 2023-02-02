using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RendicionCuentasServices.Model;

namespace RendicionCuentasServices.DTO
{
    public class EntTablerosSubsistemasPublicados : Dashboard_PublicacionAvanceSubsistema
    {
        public string NombreSistema { get; set; }
        public string DescripcionLargaSubsistema { get; set; }
        public List<Dashboard_PublicacionTabular> AvancesMes { get; set; }
        public List<SP_Select_PublicacionElementoxSubsistema_Result> Elementos { get; set; }
    }
}
