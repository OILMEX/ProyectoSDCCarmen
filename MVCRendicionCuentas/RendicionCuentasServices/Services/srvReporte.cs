using RendicionCuentasServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RendicionCuentasServices.Services
{
    public class srvReporte
    {
        dbSDCEntities db = new dbSDCEntities();
        public List<SP_DevuelveDatosGrid_Result> GetAllData()
        {
            List<SP_DevuelveDatosGrid_Result> listResult = new List<SP_DevuelveDatosGrid_Result>();
            listResult = db.SP_DevuelveDatosGrid().ToList();
            return listResult;
        }

        public List<SP_VistaparaPivotGridProyectos_Result> GetDataGeneral(int idUbicacion = 0, string EstatusTiempo= "En Proceso")
        {
            List<SP_VistaparaPivotGridProyectos_Result> listResult = new List<SP_VistaparaPivotGridProyectos_Result>();
            var rows = (idUbicacion == 0) ? db.SP_VistaparaPivotGridProyectos(0, EstatusTiempo).ToList() : db.SP_VistaparaPivotGridProyectos(0, EstatusTiempo).Where(x => x.idUbicacion == idUbicacion).ToList();
            foreach (SP_VistaparaPivotGridProyectos_Result data in rows)
            {
                data.Semaforo = (data.Valor != null) ? "Cargado" : "No Cargado";
                data.Valor = (data.Valor != null) ? data.Valor : 0;
                listResult.Add(data);
            }
            //listResult = db.SP_VistaparaPivotGridProyectos(0).ToList();
            return listResult;
        }
    }
}
