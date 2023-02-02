using RendicionCuentasServices.DTO;
using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class GetsIndicadores
    {
        dbSDCEntities db = new dbSDCEntities();

        public List<SP_VistaCargaIndicadores12MPIxReponsable_Result> getIndicadores12MPI(int idresponsable, bool faltantes = false)
        {
            
            List<SP_VistaCargaIndicadores12MPIxReponsable_Result> lst = new List<SP_VistaCargaIndicadores12MPIxReponsable_Result>();
            var spresult = db.SP_VistaCargaIndicadores12MPIxReponsable(idresponsable).ToList();

            if (faltantes)
            {
                spresult = spresult.Where(x => x.Valor == null).ToList();
            }

            return spresult;
        }

        public List<SP_VistaCargaIndicadores12MPIxReponsable_Result> getSoportesFaltantesIndicadores12MPI(int idresponsable)
        {
            List<SP_VistaCargaIndicadores12MPIxReponsable_Result> lst = new List<SP_VistaCargaIndicadores12MPIxReponsable_Result>();
            var spresult = db.SP_VistaCargaIndicadores12MPIxReponsable(idresponsable).Where(x => x.SemaforoSoportes == "Rojo").ToList();

            return spresult;
        }

    }
}
