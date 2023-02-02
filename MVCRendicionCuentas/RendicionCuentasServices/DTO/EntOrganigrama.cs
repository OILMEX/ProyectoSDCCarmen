using RendicionCuentasServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Model
{
    public class EntOrganigrama:tbl_Subdirecciones
    {

       public List<EntOrganigramaGerencias> Grencias { set; get; }
       public List<tbl_PuestoOrganigrama> Puestos { set; get; }

    }

        //public int IdOrganigrama { get; set; }
        //public string ClaveElemento { get; set; }
        //public string NombreElemento { get; set; }
        //public int? IdNodoPadre { get; set; }
        //public int? UsuarioCreacion { get; set; }
        //public int? UsuarioActualizacion { get; set; }
        //public List<EntOrganigrama> nodes { get; set; }

    //public static class GroupEnumerable
    //{
    //    public static IList<EntOrganigrama> CrearArbol(this IEnumerable<EntOrganigrama> source)
    //    {
    //        var grupo = source.GroupBy(i => i.IdNodoPadre);

    //        var roots = (grupo.Count() > 0) ? grupo.FirstOrDefault(g => g.Key.Value.Equals(0)).ToList() : new List<EntOrganigrama>();

    //        if (roots.Count > 0)
    //        {
    //            var dict = grupo.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
    //            for (int i = 0; i < roots.Count; i++)
    //                AddHijo(roots[i], dict);
    //        }

    //        return roots;
    //    }

    //    private static void AddHijo(EntOrganigrama nodo, IDictionary<int, List<EntOrganigrama>> source)
    //    {
    //        if (source.ContainsKey(nodo.IdOrganigrama))
    //        {
    //            nodo.nodes = source[nodo.IdOrganigrama];
    //            for (int i = 0; i < nodo.nodes.Count; i++)
    //                AddHijo(nodo.nodes[i], source);
    //        }
    //        else
    //        {
    //            nodo.nodes = new List<EntOrganigrama>();
    //        }
    //    }
   // }
}
