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
    
    public partial class NOTIFICACION
    {
        public int notificacion_id { get; set; }
        public string mensaje { get; set; }
        public System.DateTime fecha { get; set; }
        public bool solicitud_aprobada { get; set; }
        public string USUARIO_rut { get; set; }
        public int INSIGNIA_insignia_id { get; set; }
    
        public virtual INSIGNIA INSIGNIA { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
