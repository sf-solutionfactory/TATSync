//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SyncTAT
{
    using System;
    using System.Collections.Generic;
    
    public partial class MONEDA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MONEDA()
        {
            this.SOCIEDAD = new HashSet<SOCIEDAD>();
            this.TCAMBIO = new HashSet<TCAMBIO>();
            this.TCAMBIO1 = new HashSet<TCAMBIO>();
        }
    
        public string WAERS { get; set; }
        public string ISOCD { get; set; }
        public string ALTWR { get; set; }
        public string LTEXT { get; set; }
        public string KTEXT { get; set; }
        public bool ACTIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOCIEDAD> SOCIEDAD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TCAMBIO> TCAMBIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TCAMBIO> TCAMBIO1 { get; set; }
    }
}