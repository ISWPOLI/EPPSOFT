﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EPPSOFTRecolectorEntities : DbContext
    {
        public EPPSOFTRecolectorEntities()
            : base("name=EPPSOFTRecolectorEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Actividad> Actividad { get; set; }
        public virtual DbSet<Adjunto> Adjunto { get; set; }
        public virtual DbSet<Caso> Caso { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<ClaseProducto> ClaseProducto { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<NovedadServicio> NovedadServicio { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Planilla> Planilla { get; set; }
        public virtual DbSet<Proceso> Proceso { get; set; }
        public virtual DbSet<RegistroActividad> RegistroActividad { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }
        public virtual DbSet<Zona> Zona { get; set; }
    }
}
