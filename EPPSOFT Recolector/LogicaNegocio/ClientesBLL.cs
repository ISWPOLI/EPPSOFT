using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class ClientesBLL
    {
        static ClientesDALC ObjCliente = new ClientesDALC();

        public static List<Cliente> ListarClientes()
        {
            return ObjCliente.ListarClientes();
        }

        public static Cliente ValidarCreacionCliente(string cedula)
        {
            return ObjCliente.ValidarCreacionCliente(cedula);
        }

        public static void CrearUsuario(string correo, string contraseña, string Nombre, string cedula, int perfil)
        {
            ObjCliente.CrearUsuario(correo, contraseña, Nombre, cedula, perfil);
        }

        public static bool UsuarioExistente(string cedula)
        {
            return ObjCliente.UsuarioExistente(cedula);
        }

        public static Usuario DatosUsuario(string cedula)
        {
            return ObjCliente.DatosUsuario(cedula);
        }

        public static void NuevaClaveUsuario(string cedula, string clave)
        {
            ObjCliente.NuevaClaveUsuario(cedula, clave);
        }

    }
}
