//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SyncTAT
{
    using System;
    using System.Collections.Generic;
    
    public partial class IIMPUESTO
    {
        public string LAND { get; set; }
        public string MWSKZ { get; set; }
        public string KSCHL { get; set; }
        public Nullable<decimal> KBETR { get; set; }
        public bool ACTIVO { get; set; }
    
        public virtual IMPUESTO IMPUESTO { get; set; }
        public virtual PAIS PAIS { get; set; }
    }
}
