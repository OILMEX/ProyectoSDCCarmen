using GsgServicios.objetos;
using RendicionCuentasServices.DTO.Mail;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GsgServicios.objetos
{
    public class email
    {
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Este Campo es Requerido")]
        public string De { get; set; }
        public string Para { get; set; }
        public string CC { get; set; }
        public string CO { get; set; }
        public string Asunto { get; set; }
        public List<emailattach> Adjuntos {get; set;}
        [Display(Name = "Mensaje")]
        [Required(ErrorMessage = "Este Campo es Requerido")]
        public string Cuerpo { get; set; }
        public mailserver server{ get; set; }
        public string DisplayName { get; set; }
        public List<attimage> AttImagenes { get; set; }
    }
}