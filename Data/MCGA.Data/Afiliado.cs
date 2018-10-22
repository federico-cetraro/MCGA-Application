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
    
    public partial class Afiliado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Afiliado()
        {
            this.Turno = new HashSet<Turno>();
            this.Bono = new HashSet<Bono>();
            this.HistorialAfiliado = new HashSet<HistorialAfiliado>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int TipoDocumentoId { get; set; }
        public string Numero { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int TipoSexoId { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public int EstadoCivilId { get; set; }
        public string NumeroAfiliado { get; set; }
        public int PlanId { get; set; }
        public string Foto { get; set; }
        public bool Habilitado { get; set; }
        public System.DateTime createdon { get; set; }
        public string createdby { get; set; }
        public System.DateTime changedon { get; set; }
        public string changedby { get; set; }
        public Nullable<System.DateTime> deletedon { get; set; }
        public string deletedby { get; set; }
        public bool isdeleted { get; set; }
    
        public virtual EstadoCivil EstadoCivil { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Turno> Turno { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual TipoSexo TipoSexo { get; set; }
        public virtual Plan Plan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bono> Bono { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistorialAfiliado> HistorialAfiliado { get; set; }
    }
}