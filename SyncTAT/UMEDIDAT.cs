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
    
    public partial class UMEDIDAT
    {
        public string SPRAS_ID { get; set; }
        public string MSEHI { get; set; }
        public string MSEH3 { get; set; }
        public string MSEH6 { get; set; }
        public string MSEHT { get; set; }
        public string MSEHL { get; set; }
    
        public virtual UMEDIDA UMEDIDA { get; set; }
    }
}
