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
    
    public partial class Posta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Posta()
        {
            this.Predracuns = new HashSet<Predracun>();
            this.Promets = new HashSet<Promet>();
            this.Subjekats = new HashSet<Subjekat>();
        }
    
        public int Posta_ID { get; set; }
        public string Broj { get; set; }
        public string Naziv { get; set; }
        public int Drzava_ID { get; set; }
    
        public virtual Drzava Drzava { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Predracun> Predracuns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Promet> Promets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subjekat> Subjekats { get; set; }
    }
}
