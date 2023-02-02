using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.DTO
{
    public class clsArchivos
    {
        public byte[] Archivo { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
    }
}
