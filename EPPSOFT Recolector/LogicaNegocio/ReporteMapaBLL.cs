using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class ReporteMapaBLL
    {
        static ReporteMapaDalc ObjMapa = new ReporteMapaDalc();

        public static IList ListadoMapa(CustomEntity oEntity)
        {
            return ObjMapa.ListadoMapa(oEntity);
        }

        public static IList ListadoCasosDeptos(int IdDepto)
        {
            return ObjMapa.ListadoCasosDeptos(IdDepto);
        }

        public static List<Categoria> ListarCategorias()
        {
            return ObjMapa.ListarCategorias();
        }

        public static IList ListadoCasosBusqueda(Int64 NumIdentificacion)
        {
            return ObjMapa.ListadoCasosbusqueda(NumIdentificacion);
        }

        public static IList ListadoCasosBusquedaAvanzada(string NodeCaso, string IdDepto, string IdCiudad, string estado)
        {
            return ObjMapa.ListadoCasosbusquedaAvanzada(NodeCaso, IdDepto, IdCiudad, estado);
        }

        public static IList ListadohitosXCaso(string NoCaso)
        {
            return ObjMapa.ListadohitosXCaso(NoCaso);
        }

        public static IList ListadoCasosbusquedaXCaso()
        {
            return ObjMapa.ListadoCasosbusquedaXCaso();
        }
    }
}
