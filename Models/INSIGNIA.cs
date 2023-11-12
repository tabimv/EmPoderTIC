//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class INSIGNIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INSIGNIA()
        {
            this.CERTIFICADO = new HashSet<CERTIFICADO>();
            this.CONTROL_INSIGNIA = new HashSet<CONTROL_INSIGNIA>();
            this.NOTIFICACION = new HashSet<NOTIFICACION>();
        }
    
        public int insignia_id { get; set; }
        public string nombre { get; set; }
        public string descripción { get; set; }
        public string objetivo { get; set; }
        public string imagen_url { get; set; }
        public bool activo { get; set; }
        public int NIVEL_nivel_id { get; set; }
        public int AREA_area_id { get; set; }
        public int COMPETENCIA_competencia_id { get; set; }
        public Nullable<int> CERTIFICACION_certificacion_id { get; set; }
        public int TIPO_PERFIL_tipo_perfil_id { get; set; }
        public int EVENTO_evento_id { get; set; }
    
        public virtual AREA AREA { get; set; }
        public virtual CERTIFICACION CERTIFICACION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CERTIFICADO> CERTIFICADO { get; set; }
        public virtual COMPETENCIA COMPETENCIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTROL_INSIGNIA> CONTROL_INSIGNIA { get; set; }
        public virtual EVENTO EVENTO { get; set; }
        public virtual NIVEL NIVEL { get; set; }
        public virtual TIPO_PERFIL TIPO_PERFIL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICACION> NOTIFICACION { get; set; }
    }
}
