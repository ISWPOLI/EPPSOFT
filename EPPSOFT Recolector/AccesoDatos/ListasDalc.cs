using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace AccesoDatos
{
     public class ListasDalc
    {
        EPPSOFTRecolectorEntities ContextoHitos;

        public List<Departamento> ListarDepartamentos()
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = from c in ContextoHitos.Departamento
                        select c;
                return q.OrderBy(e => e.Nombre).ToList();
            }
        }


        public List<Categoria> ListarCategorias()
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = from c in ContextoHitos.Categoria
                        select c;
                return q.OrderByDescending(e => e.IdCategoria).ToList();
            }
        }

        public List<Vehiculo> ListarVehiculos()
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = from c in ContextoHitos.Vehiculo
                        select c;
                return q.OrderByDescending(e => e.iNumeroMovil).ToList();
            }
        }

        public List<NovedadServicio> ListarNovedades()
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = from c in ContextoHitos.NovedadServicio
                        select c;
                return q.OrderByDescending(e => e.sNovedadServicio).ToList();
            }
        }

        public List<ClaseProducto> ListarClaseProductos()
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = from c in ContextoHitos.ClaseProducto
                        select c;
                return q.OrderByDescending(e => e.sClaseProducto).ToList();
            }
        }

        public List<Usuario> ListarCliente()
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = from c in ContextoHitos.Usuario
                        where c.Perfil.NombrePerfil == "UsuarioCliente"
                        select c;
                return q.OrderByDescending(e => e.NombreCompleto).ToList();
            }
        }


    }
}
