using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace AccesoDatos
{
    public class ClientesDALC
    {
        EPPSOFTRecolectorEntities ContextoHitos;

        public List<Cliente> ListarClientes()
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = from c in ContextoHitos.Cliente
                    select c;
                return q.ToList();
            }
        }

        public Cliente ValidarCreacionCliente(string cedula)
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = from c in ContextoHitos.Cliente
                        where c.NumeroIdentificacion == cedula
                        || c.NumeroIdentificacionCliente2 == cedula
                        select c;
                return q.FirstOrDefault();

            }
        }

        public Usuario DatosUsuario(string cedula)
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = from c in ContextoHitos.Usuario
                        where c.NombreUsuario == cedula
                        select c;
                return q.FirstOrDefault();
            }
        }

        public void NuevaClaveUsuario(string cedula, string clave)
        {
            string correo = "";
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = (from c in ContextoHitos.Usuario
                        where c.NombreUsuario == cedula
                        select c).FirstOrDefault();
                correo = q.CorreoRegistro;
                q.Contrasenia = clave;
                q.ContraseniaAsignada = true;
                ContextoHitos.SaveChanges();
            }

            
        }

        public bool UsuarioExistente(string cedula)
        {
            bool existente = true;
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = (from c in ContextoHitos.Usuario
                        where c.NombreUsuario == cedula
                        select c).Count();
                if (q==0)
                {
                    existente = false;
                }
            }
            return existente;
        }

        public void CrearUsuario(string correo, string contraseña, string Nombre, string cedula, int perfil)
        {
            

            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                Usuario User = new Usuario
                {
                    Contrasenia = contraseña,
                    ContraseniaAsignada = true,
                    CorreoRegistro = correo,
                    Habilitado = true,
                    idPerfil = perfil,
                    NombreCompleto = Nombre,
                    NombreUsuario = cedula,
                    
                };
                ContextoHitos.Usuario.Add(User);
                ContextoHitos.SaveChanges();
            }
            
        }
    }
}
