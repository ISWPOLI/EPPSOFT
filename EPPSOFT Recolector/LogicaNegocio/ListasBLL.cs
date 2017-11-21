using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class ListasBLL
    {
        static ListasDalc ObjDeptos = new ListasDalc();

        public static List<Departamento> ListarDepartamentos()
        {
            return ObjDeptos.ListarDepartamentos();
        }

        public static List<Categoria> ListarCategorias()
        {
            return ObjDeptos.ListarCategorias();
        }

        public static List<Vehiculo> ListarVehiculos()
        {
            return ObjDeptos.ListarVehiculos();
        }

        public static List<NovedadServicio> ListarNovedadesServicio()
        {
            return ObjDeptos.ListarNovedades();
        }

        public static List<ClaseProducto> ListarClaseProductos()
        {
            return ObjDeptos.ListarClaseProductos();
        }

        public static List<Usuario> ListarClientes()
        {
            return ObjDeptos.ListarCliente();
        }

    }
}
