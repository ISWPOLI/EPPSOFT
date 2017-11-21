using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace AccesoDatos
{
    public class PLanillaDALC
    {
        EPPSOFTRecolectorEntities ContextoHitos;

        public void CrearPlanilla(int codigoPlanilla, int generador, DateTime fecha, int vehiculo, int reportado, int recolectado, 
            string recipientes, bool encontrados, bool recogidos, string usuarioEntrega, int novedad, string otrasNov, int clase)
        {
            

            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                Planilla planillaFinal = new Planilla
                {
                    bEncontrados = encontrados,
                    bRecogidos = recogidos,
                    dFechaRecolección = fecha,
                    iCodigoPlanilla = codigoPlanilla,
                    idClaseProducto = clase,
                    idGenerador = generador,
                    idNovedadesServicio = novedad,
                    idVehiculoRecoleccion = vehiculo,
                    iRecolectadoKg = recolectado,
                    iReportadoUsuarioKg = reportado,
                    sNombreUsuarioEntrega = usuarioEntrega,
                    sOtrasNovedades = otrasNov,
                    sRecipientes = recipientes,
                };
                ContextoHitos.Planilla.Add(planillaFinal);
                ContextoHitos.SaveChanges();
            }
            
        }
    }
}
