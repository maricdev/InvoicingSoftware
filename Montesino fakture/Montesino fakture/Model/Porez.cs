//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Montesino_fakture.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Porez
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Porez()
        {
            this.Artikals = new HashSet<Artikal>();
            this.PredracunStavkes = new HashSet<PredracunStavke>();
            this.PrometStavkes = new HashSet<PrometStavke>();
        }
    
        public int PS_ID { get; set; }
        public string Kod { get; set; }
        public string Naziv { get; set; }
        public decimal Vrednost { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Artikal> Artikals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PredracunStavke> PredracunStavkes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrometStavke> PrometStavkes { get; set; }
    }
}
