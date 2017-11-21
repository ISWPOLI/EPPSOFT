using System;

namespace Entidades
{
    public class CustomEntity
    {
        public int IdPrincipal { get; set; }
        public int IdSecundario { get; set; }
        public int IdTerciario { get; set; }

        public byte EnumUno { get; set; }
        public byte EnumDos { get; set; }
        public byte EnumTres { get; set; }

        public string StringPrincipal { get; set; }
        public string StringSecundario { get; set; }
        public string StringTerciario { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }

        public Guid IdUnico { get; set; }

        public object varobj { get; set; }
        public object varobjSecundario { get; set; }

        public bool Logico { get; set; }
        public bool LogicoSecundario { get; set; }
    }
}
