//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FirmaTransportowa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class KLIENT
    {
        public KLIENT()
        {
            this.REZERWACJA = new HashSet<REZERWACJA>();
        }
    
        [Key]
        public int KLI_ID { get; set; }
        public Nullable<int> NAG_ID { get; set; }
        public Nullable<int> OSO_ID { get; set; }
        public Nullable<int> KLI_PUNKTY { get; set; }
        public Nullable<int> KLI_REZ_NIEWYKORZYSTANE { get; set; }
        public Nullable<System.DateTime> KLI_BLOKADA_REZ { get; set; }
    
        public virtual NAGRODA NAGRODA { get; set; }
        public virtual OSOBA OSOBA { get; set; }
        public virtual ICollection<REZERWACJA> REZERWACJA { get; set; }
    }
}
