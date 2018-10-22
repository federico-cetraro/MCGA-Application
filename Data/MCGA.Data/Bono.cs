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
    
    public partial class Bono
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bono()
        {
            this.Atencion = new HashSet<Atencion>();
        }
    
        public int Id { get; set; }
        public int plan_id { get; set; }
        public int afiliado_id { get; set; }
        public System.DateTime fecha_compra { get; set; }
        public Nullable<System.DateTime> fecha_impresion { get; set; }
        public bool consumido { get; set; }
    
        public virtual Afiliado Afiliado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Atencion> Atencion { get; set; }
        public virtual Plan Plan { get; set; }
    }
}