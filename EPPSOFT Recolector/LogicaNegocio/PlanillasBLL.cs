using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class PlanillasBLL
    {
        static PLanillaDALC ObjPlanilla = new PLanillaDALC();



        public static void CrearPlanilla(int codigoPlanilla, int generador, DateTime fecha, int vehiculo, int reportado, int recolectado,
            string recipientes, bool encontrados, bool recogidos, string usuarioEntrega, int novedad, string otrasNov, int clase)
        {
            ObjPlanilla.CrearPlanilla(codigoPlanilla, generador, fecha, vehiculo, reportado, recolectado,
            recipientes, encontrados, recogidos, usuarioEntrega, novedad, otrasNov, clase);
        }

    }
}
