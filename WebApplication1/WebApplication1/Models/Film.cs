//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Film
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Film()
        {
            this.Wypozyczenia_Filmu = new HashSet<Wypozyczenia_Filmu>();
        }
    
        public int ID { get; set; }
        public string Tytul { get; set; }
        public Nullable<int> ID_Aktora { get; set; }
        public int Stan_Magazynowy { get; set; }
    
        public virtual Aktor Aktor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wypozyczenia_Filmu> Wypozyczenia_Filmu { get; set; }
    }
}
