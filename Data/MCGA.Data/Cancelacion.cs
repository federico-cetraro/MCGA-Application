//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MCGA.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cancelacion
    {
        public int Id { get; set; }
        public int turno_id { get; set; }
        public string detalle_cancel { get; set; }
        public Nullable<System.DateTime> fecha_cancel { get; set; }
        public int tipo_cancel_id { get; set; }
        public System.DateTime createdon { get; set; }
        public string createdby { get; set; }
        public System.DateTime changedon { get; set; }
        public string changedby { get; set; }
    
        public virtual TipoCancelacion TipoCancelacion { get; set; }
        public virtual Turno Turno { get; set; }
    }
}
