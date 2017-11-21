using System;
using System.Collections.Generic;

namespace Entidades
{
    public class UsuariosEntity
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string IP { get; set; }
        public Usuario uEntity { get; set; }
        public bool Habilitado { get; set; }
    }
}

