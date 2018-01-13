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
    
    public partial class Ksiazka
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ksiazka()
        {
            this.Wypozyczenia_Ksiazki = new HashSet<Wypozyczenia_Ksiazki>();
        }
    
        public int ID { get; set; }
        public string Tytul { get; set; }
        public string ISBN { get; set; }
        public Nullable<int> Strony { get; set; }
        public Nullable<int> ID_Wydawcy { get; set; }
        public Nullable<int> ID_Autora { get; set; }
        public Nullable<int> ID_Aktora { get; set; }
        public Nullable<int> ID_Slowo_Kluczowe { get; set; }
    
        public virtual Autor Autor { get; set; }
        public virtual Slowo_Kluczowe Slowo_Kluczowe { get; set; }
        public virtual Wydawca Wydawca { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wypozyczenia_Ksiazki> Wypozyczenia_Ksiazki { get; set; }
    }
}