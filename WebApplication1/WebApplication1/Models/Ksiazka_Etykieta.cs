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
    
    public partial class Ksiazka_Etykieta
    {
        public int ID { get; set; }
        public int ID_Ksiazka { get; set; }
        public Nullable<int> ID_Etykieta { get; set; }
    
        public virtual Etykieta Etykieta { get; set; }
        public virtual Ksiazka Ksiazka { get; set; }
    }
}
