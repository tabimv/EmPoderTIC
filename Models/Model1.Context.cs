﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmPoderTIC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EmPoderTIC_Conexion_Oficial_WEB : DbContext
    {
        public EmPoderTIC_Conexion_Oficial_WEB()
            : base("name=EmPoderTIC_Conexion_Oficial_WEB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AREA> AREA { get; set; }
        public virtual DbSet<ASISTENCIA> ASISTENCIA { get; set; }
        public virtual DbSet<CERTIFICACION> CERTIFICACION { get; set; }
        public virtual DbSet<CERTIFICADO> CERTIFICADO { get; set; }
        public virtual DbSet<COMPETENCIA> COMPETENCIA { get; set; }
        public virtual DbSet<CONTROL_INSIGNIA> CONTROL_INSIGNIA { get; set; }
        public virtual DbSet<EVENTO> EVENTO { get; set; }
        public virtual DbSet<INFO_PERFIL> INFO_PERFIL { get; set; }
        public virtual DbSet<INSCRIPCION> INSCRIPCION { get; set; }
        public virtual DbSet<INSIGNIA> INSIGNIA { get; set; }
        public virtual DbSet<NIVEL> NIVEL { get; set; }
        public virtual DbSet<NOTIFICACION> NOTIFICACION { get; set; }
        public virtual DbSet<OTORGAR_INSIGNIA_P2> OTORGAR_INSIGNIA_P2 { get; set; }
        public virtual DbSet<OTORGAR_INSIGNIA_P3> OTORGAR_INSIGNIA_P3 { get; set; }
        public virtual DbSet<TIPO_PERFIL> TIPO_PERFIL { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
        public virtual DbSet<USUARIO_CERTIFICADO> USUARIO_CERTIFICADO { get; set; }
    }
}
